using Microsoft.Win32;
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
        }
        #endregion

        #region Actions buttons
        private void btExtract_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(this.textBoxOutputFolder.Text))
            {
                MessageBox.Show("Vous devez choisir un dossier d'extraction", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string outputFilePath = this.textBoxOutputFolder.Text + "\\democracy3ExtractedText.ini";
            if (File.Exists(outputFilePath))
                File.Delete(outputFilePath);

            var iniData = new IniParser.Model.IniData();

            // RACINE
            string path = this.textBoxSource.Text + "\\";
            var files = Directory.GetFiles(path);
            foreach (var filePath in files)
            {
                var fileName = filePath.Replace(path, "");
                if (fileName == "strings.ini")
                {
                    ParseStringIni(filePath, fileName, iniData);
                }
                else if (fileName == "tutorial.csv")
                {
                    ParseTutorialCsv(filePath, fileName, iniData);
                }
            }

            // SIMULATION
            path = this.textBoxSource.Text + "\\simulation\\";
            files = Directory.GetFiles(path);
            foreach (var filePath in files)
            {
                var fileName = filePath.Replace(path, "");
                if (fileName == "achievements.csv")
                {
                    ParseAchievementsCsv(filePath, fileName, iniData);
                }
                else if (fileName == "policies.csv")
                {
                    ParsePoliciesCsv(filePath, fileName, iniData);
                }
                else if (fileName == "pressuregroups.csv")
                {
                    ParsePressureGroupsCsv(filePath, fileName, iniData);
                }
                else if (fileName == "simulation.csv")
                {
                    ParseSimulationCsv(filePath, fileName, iniData);
                }
                else if (fileName == "situations.csv")
                {
                    ParseSituationsCsv(filePath, fileName, iniData);
                }
                else if (fileName == "votertypes.csv")
                {
                    ParseVoterTypesCsv(filePath, fileName, iniData);
                }
            }

            // Attacks
            path = this.textBoxSource.Text + "\\simulation\\attacks\\";
            if (Directory.Exists(path))
            {
                files = Directory.GetFiles(path);
                foreach (var filePath in files)
                {
                    var fileName = filePath.Replace(path, "");
                    ParseAttacksIni(filePath, fileName, iniData);
                }
            }

            // Dilemmas
            path = this.textBoxSource.Text + "\\simulation\\dilemmas\\";
            if (Directory.Exists(path))
            {
                files = Directory.GetFiles(path);
                foreach (var filePath in files)
                {
                    var fileName = filePath.Replace(path, "");
                    ParseDilemmasIni(filePath, fileName, iniData);
                }
            }

            // Events
            path = this.textBoxSource.Text + "\\simulation\\events\\";
            if (Directory.Exists(path))
            {
                files = Directory.GetFiles(path);
                foreach (var filePath in files)
                {
                    var fileName = filePath.Replace(path, "");
                    ParseEventsIni(filePath, fileName, iniData);
                }
            }

            // Missions
            path = this.textBoxSource.Text + "\\missions\\";
            if (Directory.Exists(path))
            {
                files = Directory.GetDirectories(path);
                foreach (var filePath in files)
                {
                    var fileName = filePath.Replace(path, "") + ".txt";
                    ParseMissionsIni(filePath + "\\" + fileName, fileName, iniData);
                }
            }

            var parser = new IniParser.FileIniDataParser();
            parser.SaveFile(outputFilePath, iniData);

            MessageBox.Show("You can now send the file to transifex website\nFile path : " + outputFilePath, "Extraction finished...", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        
        private void btCompile_Click(object sender, EventArgs e)
        {
            // parcours les fichier puis essaye de retrouver la partie correspondante dans le fichier de traduction
            if (!File.Exists(this.textBoxTransifexFile.Text))
            {
                MessageBox.Show("No transifex file...", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var transifexFileParser = new IniParser.FileIniDataParser();
            var transifexInidata = transifexFileParser.LoadFile(this.textBoxTransifexFile.Text, Encoding.UTF8);
            
            // RACINE
            string path = this.textBoxSource.Text + "\\";
            var files = Directory.GetFiles(path);
            foreach (var filePath in files)
            {
                var fileName = filePath.Replace(path, "");
                // try to find file section in transifex file
                if (transifexInidata.Sections.Any(s => s.SectionName == fileName))
                {
                    var fileSection = transifexInidata.Sections.First(s => s.SectionName == fileName);

                    if (fileName == "strings.ini")
                    {                        
                        InjectStringIni(filePath, fileName, fileSection);
                    }
                    else if (fileName == "tutorial.csv")
                    {
                        InjectTutorialCsv(filePath, fileName, fileSection);
                    }  

                }
            }

            // SIMULATION
            path = this.textBoxSource.Text + "\\simulation\\";
            files = Directory.GetFiles(path);
            foreach (var filePath in files)
            {
                var fileName = filePath.Replace(path, "");
                // try to find file section in transifex file
                if (transifexInidata.Sections.Any(s => s.SectionName == fileName))
                {
                    var fileSection = transifexInidata.Sections.First(s => s.SectionName == fileName);

                    if (fileName == "achievements.csv")
                    {
                        InjectAchievementsCsv(filePath, fileName, fileSection);
                    }
                    else if (fileName == "policies.csv")
                    {
                        InjectPoliciesCsv(filePath, fileName, fileSection);
                    }
                    else if (fileName == "pressuregroups.csv")
                    {
                        InjectPressureGroupsCsv(filePath, fileName, fileSection);
                    }
                    else if (fileName == "simulation.csv")
                    {
                        InjectSimulationCsv(filePath, fileName, fileSection);
                    }
                    else if (fileName == "situations.csv")
                    {
                        InjectSituationsCsv(filePath, fileName, fileSection);
                    }
                    else if (fileName == "votertypes.csv")
                    {
                        InjectVoterTypesCsv(filePath, fileName, fileSection);
                    }
                }
            }

            // Attacks
            path = this.textBoxSource.Text + "\\simulation\\attacks\\";
            if (Directory.Exists(path))
            {
                files = Directory.GetFiles(path);
                foreach (var filePath in files)
                {
                    var fileName = filePath.Replace(path, "");
                    // try to find file section in transifex file
                    if (transifexInidata.Sections.Any(s => s.SectionName == fileName))
                    {
                        var fileSection = transifexInidata.Sections.First(s => s.SectionName == fileName);
                        InjectAttacksIni(filePath, fileName, fileSection);
                    }
                }
            }

            // Dilemmas
            path = this.textBoxSource.Text + "\\simulation\\dilemmas\\";
            if (Directory.Exists(path))
            {
                files = Directory.GetFiles(path);
                foreach (var filePath in files)
                {
                    var fileName = filePath.Replace(path, "");
                    // try to find file section in transifex file
                    if (transifexInidata.Sections.Any(s => s.SectionName == fileName))
                    {
                        var fileSection = transifexInidata.Sections.First(s => s.SectionName == fileName);
                        InjectDilemmasIni(filePath, fileName, fileSection);
                    }
                }
            }

            // Events
            path = this.textBoxSource.Text + "\\simulation\\events\\";
            if (Directory.Exists(path))
            {
                files = Directory.GetFiles(path);
                foreach (var filePath in files)
                {
                    var fileName = filePath.Replace(path, "");
                    // try to find file section in transifex file
                    if (transifexInidata.Sections.Any(s => s.SectionName == fileName))
                    {
                        var fileSection = transifexInidata.Sections.First(s => s.SectionName == fileName);
                        InjectEventsIni(filePath, fileName, fileSection);
                    }
                }
            }

            // Events
            path = this.textBoxSource.Text + "\\missions\\";
            if (Directory.Exists(path))
            {
                files = Directory.GetDirectories(path);
                foreach (var filePath in files)
                {
                    var fileName = filePath.Replace(path, "") + ".txt";
                    // try to find file section in transifex file
                    if (transifexInidata.Sections.Any(s => s.SectionName == fileName))
                    {
                        var fileSection = transifexInidata.Sections.First(s => s.SectionName == fileName);
                        InjectMissionsIni(filePath + "\\" + fileName, fileName, fileSection);
                    }
                }
            }

            MessageBox.Show("Game is now translated.", "Injection finished...", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        #endregion

        #region Ini Root
        private void InjectStringIni(string filePath, string fileName, IniParser.Model.SectionData sectionData)
        {
            var stringIniParser = new IniParser.FileIniDataParser();
            stringIniParser.Parser.Configuration.AllowDuplicateKeys = true;
            stringIniParser.Parser.Configuration.SkipInvalidLines = true;

            try
            {
                var strinInidata = stringIniParser.LoadFile(filePath);

                foreach (var sectionKey in sectionData.Keys)
                {
                    var section = sectionKey.KeyName.Split('@').First();
                    var key = sectionKey.KeyName.Split('@').Last();
                    var value = sectionKey.Value.DeleteAccentAndSpecialsChar();

                    if (strinInidata.Sections.Any(s => s.SectionName == section))
                    {
                        var iniSectionData = strinInidata.Sections.GetSectionData(section);
                        if (iniSectionData.Keys.Any(k => k.KeyName == key))
                        {
                            iniSectionData.Keys.GetKeyData(key).Value = value;
                        }
                    }
                }

                stringIniParser.SaveFile(filePath, strinInidata);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void ParseStringIni(string filePath, string fileName, IniParser.Model.IniData iniData)
        {
            iniData.Sections.AddSection(fileName);
           
            var stringIniParser = new IniParser.FileIniDataParser();
            stringIniParser.Parser.Configuration.AllowDuplicateKeys = true;

            try
            {
                var strinInidata = stringIniParser.LoadFile(filePath);

                foreach (var stringIniSection in strinInidata.Sections)
                {
                    foreach (var stringIniKey in stringIniSection.Keys)
                    {
                        var key = string.Format("{0}@{2}@{1}", stringIniSection.SectionName, stringIniKey.KeyName, fileName);
                        var value = this.SurroundWithQuotes(stringIniKey.Value.Trim());

                        iniData.Sections.GetSectionData(fileName).Keys.AddKey(key, value);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        #endregion

        #region Csv Root
        private void ParseTutorialCsv(string filePath, string fileName, IniParser.Model.IniData iniData)
        {
            ParseCsv(filePath, fileName, iniData, 2, 11);
        }
        private void InjectTutorialCsv(string filePath, string fileName, IniParser.Model.SectionData iniData)
        {
            InjectCsv(filePath, fileName, iniData, 2, 11);
        }
        #endregion

        #region Ini Attacks
        private void ParseAttacksIni(string filePath, string fileName, IniParser.Model.IniData iniData)
        {
            iniData.Sections.AddSection(fileName);

            var stringIniParser = new IniParser.FileIniDataParser();
            stringIniParser.Parser.Configuration.SkipInvalidLines = true;

            try
            {
                var strinInidata = stringIniParser.LoadFile(filePath);

                // Toujours la premiere section
                var stringIniSection = strinInidata.Sections.First();

                var stringIniKey = stringIniSection.Keys.First(k => k.KeyName == "SuccessText");
                var key = string.Format("{0}@{2}@{1}", stringIniSection.SectionName, stringIniKey.KeyName, fileName);
                var value = this.SurroundWithQuotes(stringIniKey.Value.Trim());
                iniData.Sections.GetSectionData(fileName).Keys.AddKey(key, value);

                stringIniKey = stringIniSection.Keys.First(k => k.KeyName == "GUIName");
                key = string.Format("{0}@{2}@{1}", stringIniSection.SectionName, stringIniKey.KeyName, fileName);
                value = this.SurroundWithQuotes(stringIniKey.Value.Trim());
                iniData.Sections.GetSectionData(fileName).Keys.AddKey(key, value);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void InjectAttacksIni(string filePath, string fileName, IniParser.Model.SectionData sectionData)
        {
            InjectStringIni(filePath, fileName, sectionData);
        }
        #endregion

        #region Ini Dilemmas
        private void ParseDilemmasIni(string filePath, string fileName, IniParser.Model.IniData iniData)
        {
            iniData.Sections.AddSection(fileName);

            var stringIniParser = new IniParser.FileIniDataParser();
            stringIniParser.Parser.Configuration.SkipInvalidLines = true;

            try
            {
                var strinInidata = stringIniParser.LoadFile(filePath);

                foreach (var section in strinInidata.Sections)
                {
                    if (section.Keys.Any(k => k.KeyName.ToLower().Trim() == "description"))
                    {
                        var stringIniKey = section.Keys.First(k => k.KeyName.ToLower().Trim() == "description");
                        var key = string.Format("{0}@{2}@{1}", section.SectionName, stringIniKey.KeyName, fileName);
                        var value = this.SurroundWithQuotes(stringIniKey.Value.Trim());
                        iniData.Sections.GetSectionData(fileName).Keys.AddKey(key, value);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void InjectDilemmasIni(string filePath, string fileName, IniParser.Model.SectionData sectionData)
        {
            InjectStringIni(filePath, fileName, sectionData);
        }
        #endregion

        #region Ini Events
        private void ParseEventsIni(string filePath, string fileName, IniParser.Model.IniData iniData)
        {
            iniData.Sections.AddSection(fileName);

            var stringIniParser = new IniParser.FileIniDataParser();
            stringIniParser.Parser.Configuration.SkipInvalidLines = true;
            stringIniParser.Parser.Configuration.AllowDuplicateKeys = true;

            try
            {
                var strinInidata = stringIniParser.LoadFile(filePath);

                foreach (var section in strinInidata.Sections)
                {
                    if (section.Keys.Any(k => k.KeyName.ToLower().Trim() == "description"))
                    {
                        var stringIniKey = section.Keys.First(k => k.KeyName.ToLower().Trim() == "description");
                        var key = string.Format("{0}@{2}@{1}", section.SectionName, stringIniKey.KeyName, fileName);
                        var value = this.SurroundWithQuotes(stringIniKey.Value.Trim());
                        iniData.Sections.GetSectionData(fileName).Keys.AddKey(key, value);
                    }

                    if (section.Keys.Any(k => k.KeyName.ToLower().Trim() == "guiname"))
                    {
                        var stringIniKey = section.Keys.First(k => k.KeyName.ToLower().Trim() == "guiname");
                        var key = string.Format("{0}@{2}@{1}", section.SectionName, stringIniKey.KeyName, fileName);
                        var value = this.SurroundWithQuotes(stringIniKey.Value.Trim());
                        iniData.Sections.GetSectionData(fileName).Keys.AddKey(key, value);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void InjectEventsIni(string filePath, string fileName, IniParser.Model.SectionData sectionData)
        {
            InjectStringIni(filePath, fileName, sectionData);
        }
        #endregion

        #region Ini Missions
        private void ParseMissionsIni(string filePath, string fileName, IniParser.Model.IniData iniData)
        {
            iniData.Sections.AddSection(fileName);

            var stringIniParser = new IniParser.FileIniDataParser();
            stringIniParser.Parser.Configuration.SkipInvalidLines = true;
            stringIniParser.Parser.Configuration.AllowDuplicateKeys = true;

            try
            {
                var strinInidata = stringIniParser.LoadFile(filePath);

                foreach (var section in strinInidata.Sections)
                {
                    if (section.Keys.Any(k => k.KeyName.ToLower().Trim() == "description"))
                    {
                        var stringIniKey = section.Keys.First(k => k.KeyName.ToLower().Trim() == "description");
                        var key = string.Format("{0}@{2}@{1}", section.SectionName, stringIniKey.KeyName, fileName);
                        var value = this.SurroundWithQuotes(stringIniKey.Value.Trim());
                        iniData.Sections.GetSectionData(fileName).Keys.AddKey(key, value);
                    }

                    if (section.Keys.Any(k => k.KeyName.ToLower().Trim() == "guiname"))
                    {
                        var stringIniKey = section.Keys.First(k => k.KeyName.ToLower().Trim() == "guiname");
                        var key = string.Format("{0}@{2}@{1}", section.SectionName, stringIniKey.KeyName, fileName);
                        var value = this.SurroundWithQuotes(stringIniKey.Value.Trim());
                        iniData.Sections.GetSectionData(fileName).Keys.AddKey(key, value);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void InjectMissionsIni(string filePath, string fileName, IniParser.Model.SectionData sectionData)
        {
            InjectStringIni(filePath, fileName, sectionData);
        }
        #endregion

        #region Csv Simulation
        private void ParseAchievementsCsv(string filePath, string fileName, IniParser.Model.IniData iniData)
        {
            ParseCsv(filePath, fileName, iniData, 1, 4);
        }
        private void ParsePoliciesCsv(string filePath, string fileName, IniParser.Model.IniData iniData)
        {
            ParseCsv(filePath, fileName, iniData, 1, 4);
        }
        private void ParsePressureGroupsCsv(string filePath, string fileName, IniParser.Model.IniData iniData)
        {
            ParseCsv(filePath, fileName, iniData, 1, 8);
        }
        private void ParseSimulationCsv(string filePath, string fileName, IniParser.Model.IniData iniData)
        {
            ParseCsv(filePath, fileName, iniData, 1, 3);
        }
        private void ParseSituationsCsv(string filePath, string fileName, IniParser.Model.IniData iniData)
        {
            ParseCsv(filePath, fileName, iniData, 1, 3);
        }
        private void ParseVoterTypesCsv(string filePath, string fileName, IniParser.Model.IniData iniData)
        {
            ParseCsv(filePath, fileName, iniData, 1, 8);
        }

        
        private void InjectAchievementsCsv(string filePath, string fileName, IniParser.Model.SectionData iniData)
        {
            InjectCsv(filePath, fileName, iniData, 1, 4);
        }
        private void InjectPoliciesCsv(string filePath, string fileName, IniParser.Model.SectionData iniData)
        {
            InjectCsv(filePath, fileName, iniData, 1, 4);
        }
        private void InjectPressureGroupsCsv(string filePath, string fileName, IniParser.Model.SectionData iniData)
        {
            InjectCsv(filePath, fileName, iniData, 1, 8);
        }
        private void InjectSimulationCsv(string filePath, string fileName, IniParser.Model.SectionData iniData)
        {
            InjectCsv(filePath, fileName, iniData, 1, 3);
        }
        private void InjectSituationsCsv(string filePath, string fileName, IniParser.Model.SectionData iniData)
        {
            InjectCsv(filePath, fileName, iniData, 1, 3);
        }
        private void InjectVoterTypesCsv(string filePath, string fileName, IniParser.Model.SectionData iniData)
        {
            InjectCsv(filePath, fileName, iniData, 1, 8);
        }
        #endregion

        #region Csv Global
        private void ParseCsv(string filePath, string fileName, IniParser.Model.IniData iniData, int keyIndex, int valueIndex)
        {
            iniData.Sections.AddSection(fileName);
            try
            {
                var csv = new CsvHelper.CsvReader(File.OpenText(filePath));
                
                while (csv.Read())
                {
                    var key = csv.GetField(keyIndex);
                    var value = this.SurroundWithQuotes(csv.GetField(valueIndex));
                    if (!string.IsNullOrWhiteSpace(key))
                        iniData.Sections.GetSectionData(fileName).Keys.AddKey(fileName + "@" + key, value);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void InjectCsv(string filePath, string fileName, IniParser.Model.SectionData sectionData, int keyIndex, int valueIndex)
        {
            try
            {
                //var fileWriter = File.OpenWrite(filePath + ".new");
                var writer = new StreamWriter(filePath + ".new");
                var csvWriter = new CsvHelper.CsvWriter(writer);
                var csvReader = new CsvHelper.CsvReader(File.OpenText(filePath));

                while (csvReader.Read())
                {
                    if (csvReader.Row == 2)
                    {
                        var headers = csvReader.FieldHeaders;
                        foreach (var item in headers)
                        {
                            csvWriter.WriteField(item);
                        }
                        csvWriter.NextRecord();
                    }

                    var ligne = csvReader.CurrentRecord;

                    if (ligne != null)
                    {
                        if (ligne.Length > valueIndex)
                        {
                            var key = ligne[keyIndex];
                            if (sectionData.Keys.Any(k => ExtractKeyFromString(k.KeyName) == key))
                            {
                                var keyData = sectionData.Keys.First(k => ExtractKeyFromString(k.KeyName) == key);
                                ligne[valueIndex] = RemoveSurroundedQuotes(keyData.Value).DeleteAccentAndSpecialsChar();
                            }
                        }

                        foreach (var item in ligne)
                        {
                            csvWriter.WriteField(item);
                        }
                        csvWriter.NextRecord();
                    }
                }

                csvReader.Dispose();
                writer.Close();

                // replace olde file
                File.Delete(filePath);
                File.Move(filePath + ".new", filePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        #endregion

        #region Tools
        private string ExtractKeyFromString(string value)
        {
            var parsed = value.Split('@');
            return parsed.Last();
        }
        private string SurroundWithQuotes(string value)
        {
            if (!value.StartsWith("\""))
            {
                if (value.StartsWith("'"))
                {
                    value = value.Substring(1, value.Length - 1);
                }
                value = "\"" + value;
            }

            if (!value.EndsWith("\""))
            {
                if (value.EndsWith("'"))
                {
                    value = value.Substring(0, value.Length - 1);
                }
                value = value + "\"";
            }

            return value;
        }

        private string RemoveSurroundedQuotes(string value)
        {
            if (value.StartsWith("\""))
            {                
                value = value.Substring(1);
            }

            if (value.EndsWith("\""))
            {
                value = value.Substring(0, value.Length - 1);
            }

            return value;
        }
        #endregion
    }
}
