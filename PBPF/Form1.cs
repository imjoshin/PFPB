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
        Boolean DEBUG = true;
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
                btn_StartStop.BackColor = Color.PaleGreen;

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
                btn_StartStop.BackColor = Color.LightCoral;

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
            IEnumerable<String> lines = File.ReadLines(logLoc);//.Skip(lastLine);
            
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
                    string rareText = pfSettings.farmbuddybotrarepokemon;
                    foreach(String id in rareText.Split(',')) {
                        if(Int32.Parse(id) == getPokemonNumber(pokemon)) { 
                            isRare = true;
                            break;
                        }
                    }

                    //send if conditions met
                    if ((chk_CPIV.Checked && (Int32.Parse(cp) >= num_CP.Value || Int32.Parse(iv) >= num_IV.Value)) || 
                        (chk_Rare.Checked && isRare)) {
                        sendAlert("Caught " + pokemon, "" + pfSettings.farmbuddyloginusername + " just caught " + pokemon + " with CP of " + cp + " and an IV of " + iv + "!");     
                    }
                }

                //Level up
                else if (chk_Level.Checked && line.ToLower().Contains(level.ToLower())) {
                    sendAlert("Leveled up!", "" + pfSettings.farmbuddyloginusername + " just leveled up!");
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

                    if(eggType.CompareTo("2 KM") == 0 && chk_2K.Checked ||
                       eggType.CompareTo("5 KM") == 0 && chk_5K.Checked ||
                       eggType.CompareTo("10 KM") == 0 && chk_10K.Checked) {

                        sendAlert(pokemon + " Hatched!", "" + pfSettings.farmbuddyloginusername + " hatched a " + pokemon + " from a " + eggType + " egg with an IV of " + iv + "!");
                    }
                }
            }

            //set new positions
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

                txt_Log.AppendText(DateTime.Now.ToString("HH:mm:ss") + " - " + text + "\r\n");
                if (!DEBUG) {
                    PushResponse response = client.PushNote(reqeust);
                }
            }
        }

        private int getPokemonNumber(String name) {
            String[] pokemon = { "Bulbasaur", "Ivysaur", "Venusaur", "Charmander", "Charmeleon", "Charizard", "Squirtle", "Wartortle", "Blastoise", "Caterpie", "Metapod", "Butterfree", "Weedle", "Kakuna", "Beedrill", "Pidgey", "Pidgeotto", "Pidgeot", "Rattata", "Raticate", "Spearow", "Fearow", "Ekans", "Arbok", "Pikachu", "Raichu", "Sandshrew", "Sandslash", "Nidoran♀", "Nidorina", "Nidoqueen", "Nidoran♂", "Nidorino", "Nidoking", "Clefairy", "Clefable", "Vulpix", "Ninetales", "Jigglypuff", "Wigglytuff", "Zubat", "Golbat", "Oddish", "Gloom", "Vileplume", "Paras", "Parasect", "Venonat", "Venomoth", "Diglett", "Dugtrio", "Meowth", "Persian", "Psyduck", "Golduck", "Mankey", "Primeape", "Growlithe", "Arcanine", "Poliwag", "Poliwhirl", "Poliwrath", "Abra", "Kadabra", "Alakazam", "Machop", "Machoke", "Machamp", "Bellsprout", "Weepinbell", "Victreebel", "Tentacool", "Tentacruel", "Geodude", "Graveler", "Golem", "Ponyta", "Rapidash", "Slowpoke", "Slowbro", "Magnemite", "Magneton", "Farfetch'd", "Doduo", "Dodrio", "Seel", "Dewgong", "Grimer", "Muk", "Shellder", "Cloyster", "Gastly", "Haunter", "Gengar", "Onix", "Drowzee", "Hypno", "Krabby", "Kingler", "Voltorb", "Electrode", "Exeggcute", "Exeggutor", "Cubone", "Marowak", "Hitmonlee", "Hitmonchan", "Lickitung", "Koffing", "Weezing", "Rhyhorn", "Rhydon", "Chansey", "Tangela", "Kangaskhan", "Horsea", "Seadra", "Goldeen", "Seaking", "Staryu", "Starmie", "Mr. Mime", "Scyther", "Jynx", "Electabuzz", "Magmar", "Pinsir", "Tauros", "Magikarp", "Gyarados", "Lapras", "Ditto", "Eevee", "Vaporeon", "Jolteon", "Flareon", "Porygon", "Omanyte", "Omastar", "Kabuto", "Kabutops", "Aerodactyl", "Snorlax", "Articuno", "Zapdos", "Moltres", "Dratini", "Dragonair", "Dragonite", "Mewtwo", "Mew", "Chikorita", "Bayleef", "Meganium", "Cyndaquil", "Quilava", "Typhlosion", "Totodile", "Croconaw", "Feraligatr", "Sentret", "Furret", "Hoothoot", "Noctowl", "Ledyba", "Ledian", "Spinarak", "Ariados", "Crobat", "Chinchou", "Lanturn", "Pichu", "Cleffa", "Igglybuff", "Togepi", "Togetic", "Natu", "Xatu", "Mareep", "Flaaffy", "Ampharos", "Bellossom", "Marill", "Azumarill", "Sudowoodo", "Politoed", "Hoppip", "Skiploom", "Jumpluff", "Aipom", "Sunkern", "Sunflora", "Yanma", "Wooper", "Quagsire", "Espeon", "Umbreon", "Murkrow", "Slowking", "Misdreavus", "Unown", "Wobbuffet", "Girafarig", "Pineco", "Forretress", "Dunsparce", "Gligar", "Steelix", "Snubbull", "Granbull", "Qwilfish", "Scizor", "Shuckle", "Heracross", "Sneasel", "Teddiursa", "Ursaring", "Slugma", "Magcargo", "Swinub", "Piloswine", "Corsola", "Remoraid", "Octillery", "Delibird", "Mantine", "Skarmory", "Houndour", "Houndoom", "Kingdra", "Phanpy", "Donphan", "Porygon2", "Stantler", "Smeargle", "Tyrogue", "Hitmontop", "Smoochum", "Elekid", "Magby", "Miltank", "Blissey", "Raikou", "Entei", "Suicune", "Larvitar", "Pupitar", "Tyranitar", "Lugia", "Ho-Oh", "Celebi", "Treecko", "Grovyle", "Sceptile", "Torchic", "Combusken", "Blaziken", "Mudkip", "Marshtomp", "Swampert", "Poochyena", "Mightyena", "Zigzagoon", "Linoone", "Wurmple", "Silcoon", "Beautifly", "Cascoon", "Dustox", "Lotad", "Lombre", "Ludicolo", "Seedot", "Nuzleaf", "Shiftry", "Taillow", "Swellow", "Wingull", "Pelipper", "Ralts", "Kirlia", "Gardevoir", "Surskit", "Masquerain", "Shroomish", "Breloom", "Slakoth", "Vigoroth", "Slaking", "Nincada", "Ninjask", "Shedinja", "Whismur", "Loudred", "Exploud", "Makuhita", "Hariyama", "Azurill", "Nosepass", "Skitty", "Delcatty", "Sableye", "Mawile", "Aron", "Lairon", "Aggron", "Meditite", "Medicham", "Electrike", "Manectric", "Plusle", "Minun", "Volbeat", "Illumise", "Roselia", "Gulpin", "Swalot", "Carvanha", "Sharpedo", "Wailmer", "Wailord", "Numel", "Camerupt", "Torkoal", "Spoink", "Grumpig", "Spinda", "Trapinch", "Vibrava", "Flygon", "Cacnea", "Cacturne", "Swablu", "Altaria", "Zangoose", "Seviper", "Lunatone", "Solrock", "Barboach", "Whiscash", "Corphish", "Crawdaunt", "Baltoy", "Claydol", "Lileep", "Cradily", "Anorith", "Armaldo", "Feebas", "Milotic", "Castform", "Kecleon", "Shuppet", "Banette", "Duskull", "Dusclops", "Tropius", "Chimecho", "Absol", "Wynaut", "Snorunt", "Glalie", "Spheal", "Sealeo", "Walrein", "Clamperl", "Huntail", "Gorebyss", "Relicanth", "Luvdisc", "Bagon", "Shelgon", "Salamence", "Beldum", "Metang", "Metagross", "Regirock", "Regice", "Registeel", "Latias", "Latios", "Kyogre", "Groudon", "Rayquaza", "Jirachi", "Deoxys", "Turtwig", "Grotle", "Torterra", "Chimchar", "Monferno", "Infernape", "Piplup", "Prinplup", "Empoleon", "Starly", "Staravia", "Staraptor", "Bidoof", "Bibarel", "Kricketot", "Kricketune", "Shinx", "Luxio", "Luxray", "Budew", "Roserade", "Cranidos", "Rampardos", "Shieldon", "Bastiodon", "Burmy", "Wormadam", "Mothim", "Combee", "Vespiquen", "Pachirisu", "Buizel", "Floatzel", "Cherubi", "Cherrim", "Shellos", "Gastrodon", "Ambipom", "Drifloon", "Drifblim", "Buneary", "Lopunny", "Mismagius", "Honchkrow", "Glameow", "Purugly", "Chingling", "Stunky", "Skuntank", "Bronzor", "Bronzong", "Bonsly", "Mime Jr.", "Happiny", "Chatot", "Spiritomb", "Gible", "Gabite", "Garchomp", "Munchlax", "Riolu", "Lucario", "Hippopotas", "Hippowdon", "Skorupi", "Drapion", "Croagunk", "Toxicroak", "Carnivine", "Finneon", "Lumineon", "Mantyke", "Snover", "Abomasnow", "Weavile", "Magnezone", "Lickilicky", "Rhyperior", "Tangrowth", "Electivire", "Magmortar", "Togekiss", "Yanmega", "Leafeon", "Glaceon", "Gliscor", "Mamoswine", "Porygon-Z", "Gallade", "Probopass", "Dusknoir", "Froslass", "Rotom", "Uxie", "Mesprit", "Azelf", "Dialga", "Palkia", "Heatran", "Regigigas", "Giratina", "Cresselia", "Phione", "Manaphy", "Darkrai", "Shaymin", "Arceus", "Victini", "Snivy", "Servine", "Serperior", "Tepig", "Pignite", "Emboar", "Oshawott", "Dewott", "Samurott", "Patrat", "Watchog", "Lillipup", "Herdier", "Stoutland", "Purrloin", "Liepard", "Pansage", "Simisage", "Pansear", "Simisear", "Panpour", "Simipour", "Munna", "Musharna", "Pidove", "Tranquill", "Unfezant", "Blitzle", "Zebstrika", "Roggenrola", "Boldore", "Gigalith", "Woobat", "Swoobat", "Drilbur", "Excadrill", "Audino", "Timburr", "Gurdurr", "Conkeldurr", "Tympole", "Palpitoad", "Seismitoad", "Throh", "Sawk", "Sewaddle", "Swadloon", "Leavanny", "Venipede", "Whirlipede", "Scolipede", "Cottonee", "Whimsicott", "Petilil", "Lilligant", "Basculin", "Sandile", "Krokorok", "Krookodile", "Darumaka", "Darmanitan", "Maractus", "Dwebble", "Crustle", "Scraggy", "Scrafty", "Sigilyph", "Yamask", "Cofagrigus", "Tirtouga", "Carracosta", "Archen", "Archeops", "Trubbish", "Garbodor", "Zorua", "Zoroark", "Minccino", "Cinccino", "Gothita", "Gothorita", "Gothitelle", "Solosis", "Duosion", "Reuniclus", "Ducklett", "Swanna", "Vanillite", "Vanillish", "Vanilluxe", "Deerling", "Sawsbuck", "Emolga", "Karrablast", "Escavalier", "Foongus", "Amoonguss", "Frillish", "Jellicent", "Alomomola", "Joltik", "Galvantula", "Ferroseed", "Ferrothorn", "Klink", "Klang", "Klinklang", "Tynamo", "Eelektrik", "Eelektross", "Elgyem", "Beheeyem", "Litwick", "Lampent", "Chandelure", "Axew", "Fraxure", "Haxorus", "Cubchoo", "Beartic", "Cryogonal", "Shelmet", "Accelgor", "Stunfisk", "Mienfoo", "Mienshao", "Druddigon", "Golett", "Golurk", "Pawniard", "Bisharp", "Bouffalant", "Rufflet", "Braviary", "Vullaby", "Mandibuzz", "Heatmor", "Durant", "Deino", "Zweilous", "Hydreigon", "Larvesta", "Volcarona", "Cobalion", "Terrakion", "Virizion", "Tornadus", "Thundurus", "Reshiram", "Zekrom", "Landorus", "Kyurem", "Keldeo", "Meloetta", "Genesect", "Chespin", "Quilladin", "Chesnaught", "Fennekin", "Braixen", "Delphox", "Froakie", "Frogadier", "Greninja", "Bunnelby", "Diggersby", "Fletchling", "Fletchinder", "Talonflame", "Scatterbug", "Spewpa", "Vivillon", "Litleo", "Pyroar", "Flabébé", "Floette", "Florges", "Skiddo", "Gogoat", "Pancham", "Pangoro", "Furfrou", "Espurr", "Meowstic", "Honedge", "Doublade", "Aegislash", "Spritzee", "Aromatisse", "Swirlix", "Slurpuff", "Inkay", "Malamar", "Binacle", "Barbaracle", "Skrelp", "Dragalge", "Clauncher", "Clawitzer", "Helioptile", "Heliolisk", "Tyrunt", "Tyrantrum", "Amaura", "Aurorus", "Sylveon", "Hawlucha", "Dedenne", "Carbink", "Goomy", "Sliggoo", "Goodra", "Klefki", "Phantump", "Trevenant", "Pumpkaboo", "Gourgeist", "Bergmite", "Avalugg", "Noibat", "Noivern", "Xerneas", "Yveltal", "Zygarde", "Diancie", "Hoopa", "Volcanion" };
            for(int i = 0; i < pokemon.Length; i++) {
                if (pokemon[i].ToLower().CompareTo(name.ToLower()) == 0)
                    return i + 1;
            }
            return -1;
        }
    }
}
