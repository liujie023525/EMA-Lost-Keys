namespace Polycom.RMX2000.TranslationManager.UI
{
    partial class MissingKeyListUserControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.listBoxControl = new DevExpress.XtraEditors.ListBoxControl();
            ((System.ComponentModel.ISupportInitialize)(this.listBoxControl)).BeginInit();
            this.SuspendLayout();
            // 
            // listBoxControl
            // 
            this.listBoxControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxControl.Location = new System.Drawing.Point(0, 0);
            this.listBoxControl.Name = "listBoxControl";
            this.listBoxControl.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listBoxControl.Size = new System.Drawing.Size(602, 335);
            this.listBoxControl.TabIndex = 0;
            // 
            // MissingKeyListUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.listBoxControl);
            this.Name = "MissingKeyListUserControl";
            this.Size = new System.Drawing.Size(602, 335);
            ((System.ComponentModel.ISupportInitialize)(this.listBoxControl)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.ListBoxControl listBoxControl;
    }
}
