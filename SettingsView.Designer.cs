using System.Windows.Forms;

namespace Yuzu_Updater
{
    partial class SettingsView
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
            this.TabController = new System.Windows.Forms.TabControl();
            this.LaunchTabPage = new System.Windows.Forms.TabPage();
            this.SettingsGroupBox = new System.Windows.Forms.GroupBox();
            this.LabelAutoCloseDesc = new System.Windows.Forms.Label();
            this.LabelAdminDesc = new System.Windows.Forms.Label();
            this.LabelAutoLaunch = new System.Windows.Forms.Label();
            this.LaunchAsAdminCheckbox = new System.Windows.Forms.CheckBox();
            this.AutoCloseCheckbox = new System.Windows.Forms.CheckBox();
            this.AutoLaunchCheckbox = new System.Windows.Forms.CheckBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.DownloadSettingsGroupBox = new System.Windows.Forms.GroupBox();
            this.LabelMaxConnections = new System.Windows.Forms.Label();
            this.MaxConnectionsCounter = new System.Windows.Forms.NumericUpDown();
            this.LabelAcceleratedDownloadsDesc = new System.Windows.Forms.Label();
            this.AcceleratedDownloadsCheckbox = new System.Windows.Forms.CheckBox();
            this.WarningLabel = new System.Windows.Forms.Label();
            this.SaveManagementGroupBox = new System.Windows.Forms.GroupBox();
            this.LabelBackupSaveFilesDesc = new System.Windows.Forms.Label();
            this.BackupSaveFilesCheckbox = new System.Windows.Forms.CheckBox();
            this.SinModeCheckbox = new System.Windows.Forms.CheckBox();
            this.SinModeLabel = new System.Windows.Forms.Label();
            this.TabController.SuspendLayout();
            this.LaunchTabPage.SuspendLayout();
            this.SettingsGroupBox.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.DownloadSettingsGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MaxConnectionsCounter)).BeginInit();
            this.SaveManagementGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // TabController
            // 
            this.TabController.Controls.Add(this.LaunchTabPage);
            this.TabController.Controls.Add(this.tabPage2);
            this.TabController.HotTrack = true;
            this.TabController.Location = new System.Drawing.Point(12, 12);
            this.TabController.Name = "TabController";
            this.TabController.SelectedIndex = 0;
            this.TabController.Size = new System.Drawing.Size(523, 447);
            this.TabController.TabIndex = 0;
            // 
            // LaunchTabPage
            // 
            this.LaunchTabPage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(38)))), ((int)(((byte)(41)))));
            this.LaunchTabPage.Controls.Add(this.SettingsGroupBox);
            this.LaunchTabPage.Location = new System.Drawing.Point(4, 29);
            this.LaunchTabPage.Name = "LaunchTabPage";
            this.LaunchTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.LaunchTabPage.Size = new System.Drawing.Size(515, 356);
            this.LaunchTabPage.TabIndex = 0;
            this.LaunchTabPage.Text = "Launch";
            // 
            // SettingsGroupBox
            // 
            this.SettingsGroupBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(38)))), ((int)(((byte)(41)))));
            this.SettingsGroupBox.Controls.Add(this.LabelAutoCloseDesc);
            this.SettingsGroupBox.Controls.Add(this.LabelAdminDesc);
            this.SettingsGroupBox.Controls.Add(this.LabelAutoLaunch);
            this.SettingsGroupBox.Controls.Add(this.LaunchAsAdminCheckbox);
            this.SettingsGroupBox.Controls.Add(this.AutoCloseCheckbox);
            this.SettingsGroupBox.Controls.Add(this.AutoLaunchCheckbox);
            this.SettingsGroupBox.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SettingsGroupBox.ForeColor = System.Drawing.Color.White;
            this.SettingsGroupBox.Location = new System.Drawing.Point(6, 6);
            this.SettingsGroupBox.Name = "SettingsGroupBox";
            this.SettingsGroupBox.Size = new System.Drawing.Size(503, 257);
            this.SettingsGroupBox.TabIndex = 4;
            this.SettingsGroupBox.TabStop = false;
            this.SettingsGroupBox.Text = "Launch";
            // 
            // LabelAutoCloseDesc
            // 
            this.LabelAutoCloseDesc.ForeColor = System.Drawing.Color.Silver;
            this.LabelAutoCloseDesc.Location = new System.Drawing.Point(30, 216);
            this.LabelAutoCloseDesc.Name = "LabelAutoCloseDesc";
            this.LabelAutoCloseDesc.Size = new System.Drawing.Size(467, 27);
            this.LabelAutoCloseDesc.TabIndex = 5;
            this.LabelAutoCloseDesc.Text = "Close the updater immediately after performing an update.";
            // 
            // LabelAdminDesc
            // 
            this.LabelAdminDesc.ForeColor = System.Drawing.Color.Silver;
            this.LabelAdminDesc.Location = new System.Drawing.Point(30, 133);
            this.LabelAdminDesc.Name = "LabelAdminDesc";
            this.LabelAdminDesc.Size = new System.Drawing.Size(467, 55);
            this.LabelAdminDesc.TabIndex = 4;
            this.LabelAdminDesc.Text = "Launch Yuzu with Administrator privileges. This option is recommended to take adv" +
    "antage of \"Threaded Optimization\" in NVIDIA Control Panel.";
            // 
            // LabelAutoLaunch
            // 
            this.LabelAutoLaunch.ForeColor = System.Drawing.Color.Silver;
            this.LabelAutoLaunch.Location = new System.Drawing.Point(30, 50);
            this.LabelAutoLaunch.Name = "LabelAutoLaunch";
            this.LabelAutoLaunch.Size = new System.Drawing.Size(467, 55);
            this.LabelAutoLaunch.TabIndex = 3;
            this.LabelAutoLaunch.Text = "Launch Yuzu immediately after updating if \'Latest\' is selected. Otherwise it will" +
    " automatically launch the selected version on startup.";
            // 
            // LaunchAsAdminCheckbox
            // 
            this.LaunchAsAdminCheckbox.AutoSize = true;
            this.LaunchAsAdminCheckbox.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LaunchAsAdminCheckbox.Location = new System.Drawing.Point(6, 108);
            this.LaunchAsAdminCheckbox.Name = "LaunchAsAdminCheckbox";
            this.LaunchAsAdminCheckbox.Size = new System.Drawing.Size(123, 22);
            this.LaunchAsAdminCheckbox.TabIndex = 2;
            this.LaunchAsAdminCheckbox.Text = "Admin Mode";
            this.LaunchAsAdminCheckbox.UseVisualStyleBackColor = true;
            this.LaunchAsAdminCheckbox.CheckedChanged += new System.EventHandler(this.LaunchAsAdminCheckbox_CheckedChanged);
            // 
            // AutoCloseCheckbox
            // 
            this.AutoCloseCheckbox.AutoSize = true;
            this.AutoCloseCheckbox.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AutoCloseCheckbox.Location = new System.Drawing.Point(6, 191);
            this.AutoCloseCheckbox.Name = "AutoCloseCheckbox";
            this.AutoCloseCheckbox.Size = new System.Drawing.Size(112, 22);
            this.AutoCloseCheckbox.TabIndex = 1;
            this.AutoCloseCheckbox.Text = "Auto-Close";
            this.AutoCloseCheckbox.UseVisualStyleBackColor = true;
            this.AutoCloseCheckbox.CheckedChanged += new System.EventHandler(this.AutoCloseCheckbox_CheckedChanged);
            // 
            // AutoLaunchCheckbox
            // 
            this.AutoLaunchCheckbox.AutoSize = true;
            this.AutoLaunchCheckbox.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AutoLaunchCheckbox.Location = new System.Drawing.Point(6, 25);
            this.AutoLaunchCheckbox.Name = "AutoLaunchCheckbox";
            this.AutoLaunchCheckbox.Size = new System.Drawing.Size(121, 22);
            this.AutoLaunchCheckbox.TabIndex = 0;
            this.AutoLaunchCheckbox.Text = "Auto-Launch";
            this.AutoLaunchCheckbox.UseVisualStyleBackColor = true;
            this.AutoLaunchCheckbox.CheckedChanged += new System.EventHandler(this.AutoLaunchCheckbox_CheckedChanged);
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(38)))), ((int)(((byte)(41)))));
            this.tabPage2.Controls.Add(this.DownloadSettingsGroupBox);
            this.tabPage2.Controls.Add(this.WarningLabel);
            this.tabPage2.Controls.Add(this.SaveManagementGroupBox);
            this.tabPage2.Location = new System.Drawing.Point(4, 29);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(515, 414);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Experimental";
            // 
            // DownloadSettingsGroupBox
            // 
            this.DownloadSettingsGroupBox.Controls.Add(this.SinModeLabel);
            this.DownloadSettingsGroupBox.Controls.Add(this.SinModeCheckbox);
            this.DownloadSettingsGroupBox.Controls.Add(this.LabelMaxConnections);
            this.DownloadSettingsGroupBox.Controls.Add(this.MaxConnectionsCounter);
            this.DownloadSettingsGroupBox.Controls.Add(this.LabelAcceleratedDownloadsDesc);
            this.DownloadSettingsGroupBox.Controls.Add(this.AcceleratedDownloadsCheckbox);
            this.DownloadSettingsGroupBox.ForeColor = System.Drawing.Color.White;
            this.DownloadSettingsGroupBox.Location = new System.Drawing.Point(9, 160);
            this.DownloadSettingsGroupBox.Name = "DownloadSettingsGroupBox";
            this.DownloadSettingsGroupBox.Size = new System.Drawing.Size(500, 187);
            this.DownloadSettingsGroupBox.TabIndex = 2;
            this.DownloadSettingsGroupBox.TabStop = false;
            this.DownloadSettingsGroupBox.Text = "Downloads";
            // 
            // LabelMaxConnections
            // 
            this.LabelMaxConnections.ForeColor = System.Drawing.Color.Silver;
            this.LabelMaxConnections.Location = new System.Drawing.Point(296, 26);
            this.LabelMaxConnections.Name = "LabelMaxConnections";
            this.LabelMaxConnections.Size = new System.Drawing.Size(132, 24);
            this.LabelMaxConnections.TabIndex = 7;
            this.LabelMaxConnections.Text = "Max Connections";
            // 
            // MaxConnectionsCounter
            // 
            this.MaxConnectionsCounter.Location = new System.Drawing.Point(434, 25);
            this.MaxConnectionsCounter.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.MaxConnectionsCounter.Name = "MaxConnectionsCounter";
            this.MaxConnectionsCounter.Size = new System.Drawing.Size(60, 26);
            this.MaxConnectionsCounter.TabIndex = 6;
            this.MaxConnectionsCounter.Value = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.MaxConnectionsCounter.ValueChanged += new System.EventHandler(this.MaxConnectionsCounter_ValueChanged);
            // 
            // LabelAcceleratedDownloadsDesc
            // 
            this.LabelAcceleratedDownloadsDesc.ForeColor = System.Drawing.Color.Silver;
            this.LabelAcceleratedDownloadsDesc.Location = new System.Drawing.Point(27, 53);
            this.LabelAcceleratedDownloadsDesc.Name = "LabelAcceleratedDownloadsDesc";
            this.LabelAcceleratedDownloadsDesc.Size = new System.Drawing.Size(467, 73);
            this.LabelAcceleratedDownloadsDesc.TabIndex = 5;
            this.LabelAcceleratedDownloadsDesc.Text = "Uses parallel connections to download files at faster rates.\r\nLower numbers impro" +
    "ve stability.\r\nHigher numbers improve speed.";
            // 
            // AcceleratedDownloadsCheckbox
            // 
            this.AcceleratedDownloadsCheckbox.AutoSize = true;
            this.AcceleratedDownloadsCheckbox.Location = new System.Drawing.Point(7, 26);
            this.AcceleratedDownloadsCheckbox.Name = "AcceleratedDownloadsCheckbox";
            this.AcceleratedDownloadsCheckbox.Size = new System.Drawing.Size(203, 24);
            this.AcceleratedDownloadsCheckbox.TabIndex = 0;
            this.AcceleratedDownloadsCheckbox.Text = "Accelerated Downloads";
            this.AcceleratedDownloadsCheckbox.UseVisualStyleBackColor = true;
            this.AcceleratedDownloadsCheckbox.CheckedChanged += new System.EventHandler(this.AcceleratedDownloadsCheckbox_CheckedChanged);
            // 
            // WarningLabel
            // 
            this.WarningLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.WarningLabel.ForeColor = System.Drawing.Color.Red;
            this.WarningLabel.Location = new System.Drawing.Point(6, 350);
            this.WarningLabel.Name = "WarningLabel";
            this.WarningLabel.Size = new System.Drawing.Size(503, 61);
            this.WarningLabel.TabIndex = 1;
            this.WarningLabel.Text = "Warning\r\nExperimental settings should be used at your own risk.\r\nThe author accep" +
    "ts no responsibility for any data corruption that may occur.";
            this.WarningLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // SaveManagementGroupBox
            // 
            this.SaveManagementGroupBox.Controls.Add(this.LabelBackupSaveFilesDesc);
            this.SaveManagementGroupBox.Controls.Add(this.BackupSaveFilesCheckbox);
            this.SaveManagementGroupBox.ForeColor = System.Drawing.Color.White;
            this.SaveManagementGroupBox.Location = new System.Drawing.Point(6, 6);
            this.SaveManagementGroupBox.Name = "SaveManagementGroupBox";
            this.SaveManagementGroupBox.Size = new System.Drawing.Size(503, 147);
            this.SaveManagementGroupBox.TabIndex = 0;
            this.SaveManagementGroupBox.TabStop = false;
            this.SaveManagementGroupBox.Text = "Save Management";
            // 
            // LabelBackupSaveFilesDesc
            // 
            this.LabelBackupSaveFilesDesc.ForeColor = System.Drawing.Color.Silver;
            this.LabelBackupSaveFilesDesc.Location = new System.Drawing.Point(30, 53);
            this.LabelBackupSaveFilesDesc.Name = "LabelBackupSaveFilesDesc";
            this.LabelBackupSaveFilesDesc.Size = new System.Drawing.Size(467, 91);
            this.LabelBackupSaveFilesDesc.TabIndex = 4;
            this.LabelBackupSaveFilesDesc.Text = "Backup save files before updating to newer builds to prevent potential data loss." +
    "\r\n\r\nBackups are stored in the same directory as the updater.";
            // 
            // BackupSaveFilesCheckbox
            // 
            this.BackupSaveFilesCheckbox.AutoSize = true;
            this.BackupSaveFilesCheckbox.Location = new System.Drawing.Point(7, 26);
            this.BackupSaveFilesCheckbox.Name = "BackupSaveFilesCheckbox";
            this.BackupSaveFilesCheckbox.Size = new System.Drawing.Size(166, 24);
            this.BackupSaveFilesCheckbox.TabIndex = 0;
            this.BackupSaveFilesCheckbox.Text = "Backup Save Files";
            this.BackupSaveFilesCheckbox.UseVisualStyleBackColor = true;
            this.BackupSaveFilesCheckbox.CheckedChanged += new System.EventHandler(this.BackupSaveFilesCheckbox_CheckedChanged);
            // 
            // SinModeCheckbox
            // 
            this.SinModeCheckbox.AutoSize = true;
            this.SinModeCheckbox.Location = new System.Drawing.Point(7, 129);
            this.SinModeCheckbox.Name = "SinModeCheckbox";
            this.SinModeCheckbox.Size = new System.Drawing.Size(102, 24);
            this.SinModeCheckbox.TabIndex = 8;
            this.SinModeCheckbox.Text = "Sin Mode";
            this.SinModeCheckbox.UseVisualStyleBackColor = true;
            this.SinModeCheckbox.CheckedChanged += new System.EventHandler(this.SinModeCheckbox_CheckedChanged);
            // 
            // SinModeLabel
            // 
            this.SinModeLabel.ForeColor = System.Drawing.Color.Silver;
            this.SinModeLabel.Location = new System.Drawing.Point(27, 156);
            this.SinModeLabel.Name = "SinModeLabel";
            this.SinModeLabel.Size = new System.Drawing.Size(467, 28);
            this.SinModeLabel.TabIndex = 9;
            this.SinModeLabel.Text = "Avoids unreliable file hosting providers.";
            // 
            // SettingsView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(38)))), ((int)(((byte)(41)))));
            this.ClientSize = new System.Drawing.Size(547, 471);
            this.Controls.Add(this.TabController);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsView";
            this.ShowIcon = false;
            this.Text = "Settings";
            this.Load += new System.EventHandler(this.SettingsView_Load);
            this.TabController.ResumeLayout(false);
            this.LaunchTabPage.ResumeLayout(false);
            this.SettingsGroupBox.ResumeLayout(false);
            this.SettingsGroupBox.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.DownloadSettingsGroupBox.ResumeLayout(false);
            this.DownloadSettingsGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MaxConnectionsCounter)).EndInit();
            this.SaveManagementGroupBox.ResumeLayout(false);
            this.SaveManagementGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl TabController;
        private System.Windows.Forms.TabPage LaunchTabPage;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.GroupBox SettingsGroupBox;
        private System.Windows.Forms.CheckBox LaunchAsAdminCheckbox;
        private System.Windows.Forms.CheckBox AutoCloseCheckbox;
        private System.Windows.Forms.CheckBox AutoLaunchCheckbox;
        private System.Windows.Forms.Label LabelAutoCloseDesc;
        private System.Windows.Forms.Label LabelAdminDesc;
        private System.Windows.Forms.Label LabelAutoLaunch;
        private System.Windows.Forms.GroupBox SaveManagementGroupBox;
        private System.Windows.Forms.Label LabelBackupSaveFilesDesc;
        private System.Windows.Forms.CheckBox BackupSaveFilesCheckbox;
        private Label WarningLabel;
        private GroupBox DownloadSettingsGroupBox;
        private Label LabelAcceleratedDownloadsDesc;
        private CheckBox AcceleratedDownloadsCheckbox;
        private Label LabelMaxConnections;
        private NumericUpDown MaxConnectionsCounter;
        private Label SinModeLabel;
        private CheckBox SinModeCheckbox;
    }
}