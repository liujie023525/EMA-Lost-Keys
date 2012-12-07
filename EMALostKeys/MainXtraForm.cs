#region References
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraTab;
using Polycom.RMX2000.EMALostKeys.TranslationAnalyzer;
#endregion

namespace Polycom.RMX2000.EMALostKeys.UI
{
    public partial class MainXtraForm : DevExpress.XtraEditors.XtraForm
    {
        #region Fields and Properties
        private Dictionary<string, List<string>> _missingKeyDictionary = null;
        private bool _isProcessing = false;
        private AnalyzeModes _currentAnalyzeMode;
        #endregion

        #region Constructors
        public MainXtraForm()
        {
            InitializeComponent();
        }
        #endregion

        #region Public Methods
        #endregion

        #region Internal Methods
        #endregion

        #region Protected Methods
        #endregion

        #region Private Methods
        private void AnalyzeUIProject()
        {
            string filePath = this.filePathTextEdit.Text.Trim();

            try
            {
                List<string> csFiles = ProjectManager.GetCSFiles(filePath);

                if (csFiles == null || csFiles.Count == 0)
                {
                    return;
                }

                this._missingKeyDictionary = new Dictionary<string, List<string>>();

                List<String> missingKeys = null;

                foreach (string csFile in csFiles)
                {
                    missingKeys = ProjectManager.GetMissingKeys(csFile);

                    if (missingKeys != null && missingKeys.Count > 0)
                    {
                        this._missingKeyDictionary.Add(new FileInfo(csFile).Name, missingKeys);
                    }
                }

                this.OutputSearchResult();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, ex.Source, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AnalyzeTranslationFile()
        {
            string filePath = this.filePathTextEdit.Text.Trim();

            if (!TranslationManager.ValidateTranslationFile(filePath))
            {
                return;
            }

            Array languageNames = Enum.GetValues(typeof(LanguageNames));
            this._missingKeyDictionary = new Dictionary<string, List<string>>();

            foreach (LanguageNames languageName in languageNames)
            {
                List<string> missingKeys = TranslationManager.GetMissingKeys(filePath, languageName);

                if (missingKeys != null && missingKeys.Count > 0)
                {
                    this._missingKeyDictionary.Add(languageName.ToString(), missingKeys);
                }
            }

            this.OutputSearchResult();
        }

        private void OutputSearchResult()
        {
            this.resultXtraTabControl.SuspendLayout();
            this.resultXtraTabControl.TabPages.Clear();

            if (this._missingKeyDictionary != null && this._missingKeyDictionary.Count > 0)
            {
                this.exportResultSimpleButton.Enabled = true;

                foreach (KeyValuePair<string, List<string>> missingKeys in this._missingKeyDictionary)
                {
                    XtraTabPage tabPage = new XtraTabPage() { Text = LanguageHelper.GetLanguageDisplayName(missingKeys.Key) };

                    tabPage.Controls.Add(new MissingKeyListUserControl(missingKeys.Value) { Dock = DockStyle.Fill });

                    this.resultXtraTabControl.TabPages.Add(tabPage);
                }
            }
            else
            {
                this.exportResultSimpleButton.Enabled = false;

                XtraMessageBox.Show("Great! No key is lost!");
            }

            this.resultXtraTabControl.ResumeLayout(false);
        }
        #endregion

        #region Delegates and Events
        private void MainXtraForm_Load(object sender, EventArgs e)
        {
            string currentThemeName = RegistryManager.GetTheme();

            this.defaultLookAndFeel.LookAndFeel.SetSkinStyle(currentThemeName);
            this.themesBarEditItem.EditValue = currentThemeName;
        }

        private void exitBarButtonItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void filePathTextEdit_EditValueChanged(object sender, System.EventArgs e)
        {
            this.runSimpleButton.Enabled = !String.IsNullOrEmpty(this.filePathTextEdit.Text);
        }

        private void selectSimpleButton_Click(object sender, EventArgs e)
        {
            this.openFileDialog.FileName = String.Empty;

            if (this.openFileDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            try
            {
                this.filePathTextEdit.Text = this.openFileDialog.FileName;

                FileInfo fileInfo = new FileInfo(this.filePathTextEdit.Text);
                string fileExtension = fileInfo.Extension;

                if (fileExtension.Equals(".xml", StringComparison.InvariantCultureIgnoreCase))
                {
                    this._currentAnalyzeMode = AnalyzeModes.TranslationFile;
                }
                else if (fileExtension.Equals(".csproj", StringComparison.InvariantCultureIgnoreCase))
                {
                    this._currentAnalyzeMode = AnalyzeModes.UIProject;

                    ProjectManager.InitializeEnglishKeys(fileInfo.Directory.Parent.FullName);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, ex.Source, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void runSimpleButton_Click(object sender, EventArgs e)
        {
            try
            {
                this._isProcessing = true;

                switch (this._currentAnalyzeMode)
                {
                    case AnalyzeModes.TranslationFile:
                        this.AnalyzeTranslationFile();
                        break;
                    case AnalyzeModes.UIProject:
                        this.AnalyzeUIProject();
                        break;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, ex.Source, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this._isProcessing = false;
            }
        }

        private void exportResultSimpleButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.saveFileDialog.FileName = String.Empty;
                DialogResult dialogResult = this.saveFileDialog.ShowDialog();

                if (dialogResult != DialogResult.OK)
                {
                    return;
                }

                string filePath = this.saveFileDialog.FileName;

                TranslationManager.Export(this._missingKeyDictionary, filePath);

                if (XtraMessageBox.Show("Export succeed, do you want to open it?", "Export", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Process.Start(filePath);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, ex.Source, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void aboutBarButtonItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            new InformationScreen().Show();
        }

        private void themesBarEditItem_EditValueChanged(object sender, EventArgs e)
        {
            string themeName = this.themesBarEditItem.EditValue.ToString();

            this.defaultLookAndFeel.LookAndFeel.SetSkinStyle(themeName);
            RegistryManager.SetTheme(themeName);
        }

        private void howToUseBarButtonItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Process.Start("https://github.com/XinwenCheng/EMA-Lost-Keys/wiki/How-To-Use");
        }

        private void gitHubBarButtonItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Process.Start("https://github.com/XinwenCheng/EMA-Lost-Keys");
        }

        private void MainXtraForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this._isProcessing
                && XtraMessageBox.Show("Application is processing task, are you sure to exit?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                e.Cancel = true;
            }
        }
        #endregion

        public List<FileInfo> FileManager { get; set; }
    }
}