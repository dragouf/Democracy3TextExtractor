﻿using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Democracy3TextExtractor
{
    public partial class MainForm : Form
    {
        private DemocracyStringHandling _extractor;
        private DemocracyStringHandling Extractor
        {
            get
            {
                if(_extractor == null)
                    _extractor = new DemocracyStringHandling(removeSpecialChars:false);

                _extractor.TransifexFilePath = this.textBoxTransifexFile.Text;
                _extractor.OutputExtractFolderPath = this.textBoxOutputFolder.Text;
                _extractor.GameFolderPath = this.textBoxSource.Text;

                return _extractor;
            }
        }

        public MainForm()
        {
            InitializeComponent();
            try
            {
                string InstallPath = (string)Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\GOG.com\GOGDEMOCRACY3", "PATH", null);
                if (!string.IsNullOrEmpty(InstallPath))
                    this.textBoxSource.Text = InstallPath + "data";

                string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                this.textBoxOutputFolder.Text = path;

                string pathUser = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                string pathDownload = Path.Combine(pathUser, "Downloads");
                this.openFileDialogTransifex.InitialDirectory = pathDownload;

                this.tabControl1.SelectedIndex = 1;
            }
            catch 
            {

            }
        }

        #region Browse buttons
        private void btBrowseSource_Click(object sender, EventArgs e)
        {            
            this.folderBrowserDialogSource.SelectedPath = this.textBoxSource.Text;           

            var result = this.folderBrowserDialogSource.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.OK)
            {
                this.textBoxSource.Text = this.folderBrowserDialogSource.SelectedPath;
            }
        }

        private void btOutput_Click(object sender, EventArgs e)
        {
            this.folderBrowserDialogSource.SelectedPath = this.textBoxOutputFolder.Text;

            var result = this.folderBrowserDialogSource.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.OK)
            {
                this.textBoxOutputFolder.Text = this.folderBrowserDialogSource.SelectedPath;
            }
        }

        private void btTransifexFile_Click(object sender, EventArgs e)
        {
            var result = this.openFileDialogTransifex.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.OK)
            {
                this.textBoxTransifexFile.Text = this.openFileDialogTransifex.FileName;
            }

            // Verifie le format du fichier.
            var type = DemocracyStringHandling.DetectFileType(this.textBoxTransifexFile.Text);
            this.labelFileType.Text = type.ToString();
            if(type == FileType.None)
                MessageBox.Show("Wrong file format", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning);

        }
        #endregion

        #region Actions buttons Extract
        private void btExtractMain_Click(object sender, EventArgs e)
        {
            try
            {
                this.Extractor.ExtractMainSentences();
                MessageBox.Show("Main sentences extracted", "success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void buttonExtractTitles_Click(object sender, EventArgs e)
        {
            try
            {
                this.Extractor.ExtractTitlesAndButtonsText();
                MessageBox.Show("titles and buttons text extracted", "success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        #endregion

        #region Actions buttons Inject
        private void btCompile_Click(object sender, EventArgs e)
        {
            this.Extractor.InjectTransifexFile();   
        }
        #endregion
    }
}
