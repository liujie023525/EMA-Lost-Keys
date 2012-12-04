namespace Polycom.RMX2000.EMALostKeys
{
    partial class InformationScreen
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.copyrightLabel = new DevExpress.XtraEditors.LabelControl();
            this.pictureEdit = new DevExpress.XtraEditors.PictureEdit();
            this.logoPictureEdit = new DevExpress.XtraEditors.PictureEdit();
            this.versionLabel = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.logoPictureEdit.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // copyrightLabel
            // 
            this.copyrightLabel.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.copyrightLabel.Location = new System.Drawing.Point(23, 286);
            this.copyrightLabel.Name = "copyrightLabel";
            this.copyrightLabel.Size = new System.Drawing.Size(226, 13);
            this.copyrightLabel.TabIndex = 6;
            this.copyrightLabel.Text = "Copyright © 2012 Polycom, all rights reserved.";
            // 
            // pictureEdit
            // 
            this.pictureEdit.EditValue = global::Polycom.RMX2000.EMALostKeys.Properties.Resources.International;
            this.pictureEdit.Location = new System.Drawing.Point(12, 42);
            this.pictureEdit.Name = "pictureEdit";
            this.pictureEdit.Properties.AllowFocused = false;
            this.pictureEdit.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.pictureEdit.Properties.Appearance.Options.UseBackColor = true;
            this.pictureEdit.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pictureEdit.Properties.ShowMenu = false;
            this.pictureEdit.Size = new System.Drawing.Size(426, 180);
            this.pictureEdit.TabIndex = 9;
            // 
            // logoPictureEdit
            // 
            this.logoPictureEdit.EditValue = global::Polycom.RMX2000.EMALostKeys.Properties.Resources.PolycomLogoMedium;
            this.logoPictureEdit.Location = new System.Drawing.Point(278, 268);
            this.logoPictureEdit.Name = "logoPictureEdit";
            this.logoPictureEdit.Properties.AllowFocused = false;
            this.logoPictureEdit.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.logoPictureEdit.Properties.Appearance.Options.UseBackColor = true;
            this.logoPictureEdit.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.logoPictureEdit.Properties.ShowMenu = false;
            this.logoPictureEdit.Size = new System.Drawing.Size(155, 48);
            this.logoPictureEdit.TabIndex = 8;
            // 
            // versionLabel
            // 
            this.versionLabel.Location = new System.Drawing.Point(23, 267);
            this.versionLabel.Name = "versionLabel";
            this.versionLabel.Size = new System.Drawing.Size(39, 13);
            this.versionLabel.TabIndex = 10;
            this.versionLabel.Text = "Version:";
            // 
            // InformationScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(450, 320);
            this.Controls.Add(this.versionLabel);
            this.Controls.Add(this.pictureEdit);
            this.Controls.Add(this.logoPictureEdit);
            this.Controls.Add(this.copyrightLabel);
            this.Name = "InformationScreen";
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.logoPictureEdit.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl copyrightLabel;
        private DevExpress.XtraEditors.PictureEdit logoPictureEdit;
        private DevExpress.XtraEditors.PictureEdit pictureEdit;
        private DevExpress.XtraEditors.LabelControl versionLabel;
    }
}
