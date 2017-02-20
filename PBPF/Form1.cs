using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using PushbulletSharp;
using PushbulletSharp.Models.Requests;
using PushbulletSharp.Models.Responses;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;

namespace PBPF {
    public partial class Form1 : Form {
        private int lastLine = -1;
        private int lastEggLine = -1;
        private String lastName = "";
        dynamic pfSettings;

        public Form1() {
            InitializeComponent();
        }

        private void btn_pfSelector_Click(object sender, EventArgs e) {
            while (true) {
                FolderBrowserDialog folderDlg = new FolderBrowserDialog();
                DialogResult result = folderDlg.ShowDialog();

                //Break if cancelled
                if (result == DialogResult.Cancel) break;

                //Check for validity
                if (result == DialogResult.OK) {
                    if (Directory.Exists(folderDlg.SelectedPath + "/Logs")) {
                        if(File.Exists(folderDlg.SelectedPath + "/PokeFarmer.exe")) {
                            txt_pfLocation.Text = folderDlg.SelectedPath;
                            break;
                        } else {
                            MessageBox.Show("PokeFarmer.exe was not found in the selected folder!", "Invalid Folder", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    } else {
                        MessageBox.Show("No Logs folder was found in the selected folder!", "Invalid Folder", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

            }
        }

        private void Form1_Load(object sender, EventArgs e) {
            //Load settings
            txt_APIKey.Text = Properties.Settings.Default.Key;
            txt_pfLocation.Text = Properties.Settings.Default.Location;
            chk_Level.Checked = Properties.Settings.Default.LevelUp;
            chk_Egg.Checked = Properties.Settings.Default.EggHatch;
            chk_Captcha.Checked = Properties.Settings.Default.Captcha;
            chk_Bot.Checked = Properties.Settings.Default.Bot;
            chk_CPIV.Checked = Properties.Settings.Default.CPIV;
            chk_Rare.Checked = Properties.Settings.Default.Rare;
            chk_2K.Checked = Properties.Settings.Default.Egg2K;
            chk_5K.Checked = Properties.Settings.Default.Egg5K;
            chk_10K.Checked = Properties.Settings.Default.Egg10K;
            num_CP.Value = Properties.Settings.Default.CP;
            num_IV.Value = Properties.Settings.Default.IV;
        }

        private void btn_StartStop_Click(object sender, EventArgs e) {
            //Start/stop monitoring
            if (tim_Ticker.Enabled) {
                tim_Ticker.Enabled = false;
                txt_APIKey.Enabled = true;
                txt_pfLocation.Enabled = true;
                btn_pfSelector.Enabled = true;
                btn_StartStop.Text = "Start";

                //delete temp logs
                File.Delete(txt_pfLocation.Text + "/Logs/PFPB.templog.txt");
                File.Delete(txt_pfLocation.Text + "/Logs/PFPB.tempegglog.txt");
            } else {
                //sendAlert("Starting!", "PF PB Companion is starting up now!");
                tim_Ticker.Enabled = true;
                txt_APIKey.Enabled = false;
                txt_pfLocation.Enabled = false;
                btn_pfSelector.Enabled = false;
                btn_StartStop.Text = "Stop";

                //reset vars
                lastLine = -1;
                lastEggLine = -1;
                lastName = "";

                //get pf settings
                String pfSettingsText = File.ReadAllText(txt_pfLocation.Text + "/Cache/Settings.json");
                pfSettingsText = Regex.Replace(pfSettingsText, @"\.(?=[^""]*""(?:[^""]*""[^""]*"")*[^""]*$)", String.Empty);
                pfSettings = JObject.Parse(pfSettingsText);
                Console.WriteLine("Starting for " + pfSettings.farmbuddyloginusername);
            }

            //Save settings
            Properties.Settings.Default.Key = txt_APIKey.Text;
            Properties.Settings.Default.Location = txt_pfLocation.Text;
            Properties.Settings.Default.LevelUp = chk_Level.Checked;
            Properties.Settings.Default.EggHatch = chk_Egg.Checked;
            Properties.Settings.Default.Captcha = chk_Captcha.Checked;
            Properties.Settings.Default.Bot = chk_Bot.Checked;
            Properties.Settings.Default.CPIV = chk_CPIV.Checked;
            Properties.Settings.Default.Rare = chk_Rare.Checked;
            Properties.Settings.Default.Egg2K = chk_2K.Checked;
            Properties.Settings.Default.Egg5K = chk_5K.Checked;
            Properties.Settings.Default.Egg10K = chk_10K.Checked;
            Properties.Settings.Default.CP = (int) num_CP.Value;
            Properties.Settings.Default.IV = (int) num_IV.Value;
            Properties.Settings.Default.Save();
        }
     
        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e) {
            //Tray icon click  
            Show();
            WindowState = FormWindowState.Normal;
        }

        private void tim_Ticker_Tick(object sender, EventArgs e) {

            String logLoc = txt_pfLocation.Text + "/Logs/PFPB.templog.txt";
            String eggLoc = txt_pfLocation.Text + "/Logs/PFPB.tempegglog.txt";

            DirectoryInfo dir = new DirectoryInfo(txt_pfLocation.Text + "/Logs");
            FileInfo log = (from f in dir.GetFiles() where f.Name != "HatchedEggLog.txt" orderby f.LastWriteTime descending select f).First();

            //create copies to use
            try {
                File.Copy(txt_pfLocation.Text + "/Logs/" + log.Name, logLoc, true);
                File.Copy(txt_pfLocation.Text + "/Logs/HatchedEggLog.txt", eggLoc, true);
            } catch { }


            int logCount = File.ReadLines(logLoc).Count();
            int eggCount = File.ReadLines(eggLoc).Count();

            //log file changed
            if (log.Name != lastName) {
                lastLine = logCount;
                lastEggLine = eggCount;
                lastName = log.Name;
            }

            String caught = "Caught";
            String CP = "CP(";
            String IV = "IV(";
            String level = "Level up";

            //parse log lines
            IEnumerable<String> lines = File.ReadLines(logLoc).Skip(lastLine);
            
            foreach(String l in lines){
                String line = Regex.Replace(l, "<.*?>", String.Empty);
                //parse phrases

                //caught pokemon, look for cp/iv/rare
                if (line.ToLower().Contains(caught.ToLower())) {
                    //extract parts
                    String pokemon = line.Substring(line.ToLower().IndexOf(caught.ToLower()) + caught.Length).Trim();
                    pokemon = pokemon.Substring(0, pokemon.IndexOf(" "));
                    String cp = line.Substring(line.ToLower().IndexOf(CP.ToLower()) + CP.Length).Trim();
                    cp = cp.Substring(0, cp.IndexOf(")"));
                    String iv = line.Substring(line.ToLower().IndexOf(IV.ToLower()) + IV.Length).Trim();
                    iv = iv.Substring(0, iv.IndexOf(")"));

                    Boolean isRare = false;
                    //check if is rare
                    using (var webClient = new System.Net.WebClient()) {
                        //get id from name online
                        String pokemonJSonText = webClient.DownloadString("http://pokeapi.co/api/v2/pokemon/" + pokemon.ToLower());
                        dynamic pokemonJSon = JObject.Parse(pokemonJSonText);

                        string rareText = pfSettings.farmbuddybotrarepokemon;
                        foreach(String id in rareText.Split(',')) {
                            if(Int32.Parse(id) == Int32.Parse(pokemonJSon.id)) {
                                isRare = true;
                                break;
                            }
                        }
                    }

                    //send if conditions met
                    if ((chk_CPIV.Checked && (Int32.Parse(cp) >= num_CP.Value || Int32.Parse(iv) >= num_IV.Value)) || 
                        (chk_Rare.Checked && isRare)) {
                        sendAlert("Caught " + pokemon, pfSettings.farmbuddyloginusername + " just caught " + pokemon + " with CP of " + cp + " and an IV of " + iv + "!");     
                    }
                }

                //Level up
                else if (chk_Level.Checked && line.ToLower().Contains(level.ToLower())) {
                    sendAlert("Leveled up!", pfSettings.farmbuddyloginusername + " just leveled up!");
                }
            }


            //parse egg lines
            if (chk_Egg.Checked) {
                lines = File.ReadLines(eggLoc).Skip(lastEggLine);
                foreach (String l in lines) {
                    String line = Regex.Replace(l, "<.*?>", String.Empty);

                    //not hatched pokemon line
                    if (line.IndexOf("'") < 0 || line.IndexOf("KM") < 0) continue;

                    //extract parts
                    String pokemon = line.Substring(line.IndexOf("'") + 1);
                    pokemon = pokemon.Substring(0, pokemon.IndexOf(" "));
                    String iv = line.Substring(line.IndexOf("\"") + 1);
                    iv = iv.Substring(0, iv.IndexOf("\""));
                    String eggType = line.Substring(line.IndexOf("\"", line.IndexOf("KM") - 4) + 1);
                    eggType = eggType.Substring(0, eggType.IndexOf("\""));
                    
                    sendAlert(pokemon + " Hatched!", pokemon + " hatched from a " + eggType + " egg with an IV of " + iv + "!");
                }
            }

            lastLine = logCount;
            lastEggLine = eggCount;
        }

        private void sendAlert(String title, String text) {
            PushbulletClient client = new PushbulletClient(txt_APIKey.Text);

            var currentUserInformation = client.CurrentUsersInformation();

            if (currentUserInformation != null) {
                PushNoteRequest reqeust = new PushNoteRequest() {
                    Email = currentUserInformation.Email,
                    Title = title,
                    Body = text
                };

                PushResponse response = client.PushNote(reqeust);
            }
        }
    }
}
