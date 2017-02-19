﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PBPF {
    public partial class Form1 : Form {
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
            } else {
                tim_Ticker.Enabled = true;
                txt_APIKey.Enabled = false;
                txt_pfLocation.Enabled = false;
                btn_pfSelector.Enabled = false;
                btn_StartStop.Text = "Stop";
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

        private void tim_Ticker_Tick(object sender, EventArgs e) {

        }
    }
}