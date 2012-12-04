#region References
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraTab;

#endregion

namespace Polycom.RMX2000.EMALostKeys
{
    public partial class MainXtraForm : DevExpress.XtraEditors.XtraForm
    {
        #region Fields and Properties
        private Dictionary<LanguageNames, List<string>> _missingKeyDictionary = null;
        private bool _isProcessing = false;
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
        #endregion

        #region Delegates and Events
        private void MainXtraForm_Load(object sender, EventArgs e)
        {
            string currentThemeName = RegistryHelper.GetTheme();

            this.defaultLookAndFeel.LookAndFeel.SetSkinStyle(currentThemeName);
            this.themesBarEditItem.EditValue = currentThemeName;
        }

        private void exitBarButtonItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void originalFilePathTextEdit_EditValueChanged(object sender, System.EventArgs e)
        {
            this.runSimpleButton.Enabled = !String.IsNullOrEmpty(this.originalFilePathTextEdit.Text);
        }

        private void selectOriginalFileSimpleButton_Click(object sender, EventArgs e)
        {
            this.openFileDialog.FileName = String.Empty;

            if (this.openFileDialog.ShowDialog() == DialogResult.OK)
            {
                this.originalFilePathTextEdit.Text = this.openFileDialog.FileName;
            }
        }

        private void runSimpleButton_Click(object sender, EventArgs e)
        {
            try
            {
                this._isProcessing = true;

                string filePath = this.originalFilePathTextEdit.Text.Trim();

                if (!TranslationHelper.ValidateTranslationFile(filePath))
                {
                    return;
                }

                Array languageNames = Enum.GetValues(typeof(LanguageNames));
                this._missingKeyDictionary = new Dictionary<LanguageNames, List<string>>();

                foreach (LanguageNames languageName in languageNames)
                {
                    List<string> missingKeys = TranslationHelper.GetMissingKeys(filePath, languageName);

                    if (missingKeys != null && missingKeys.Count > 0)
                    {
                        this._missingKeyDictionary.Add(languageName, missingKeys);
                    }
                }

                this.resultXtraTabControl.SuspendLayout();
                this.resultXtraTabControl.TabPages.Clear();

                if (this._missingKeyDictionary.Count > 0)
                {
                    this.exportResultSimpleButton.Enabled = true;

                    foreach (KeyValuePair<LanguageNames, List<string>> missingKeys in this._missingKeyDictionary)
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

                if (dialogResult != System.Windows.Forms.DialogResult.OK)
                {
                    return;
                }

                string filePath = this.saveFileDialog.FileName;

                TranslationHelper.Export(this._missingKeyDictionary, filePath);

                if (XtraMessageBox.Show("Export succeed, do you want to open it?", "Export", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
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
            RegistryHelper.SetTheme(themeName);
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
                && XtraMessageBox.Show("Application is processing task, are you sure to exit?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
            {
                e.Cancel = true;
            }
        }
        #endregion
    }
}