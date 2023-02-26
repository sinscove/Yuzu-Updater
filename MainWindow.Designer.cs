using System.Drawing;

namespace Yuzu_Updater
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.DownloadInstallProgressBar = new System.Windows.Forms.ProgressBar();
            this.StatusLabel = new System.Windows.Forms.Label();
            this.VersionDropdown = new System.Windows.Forms.ComboBox();
            this.VersionLabel = new System.Windows.Forms.Label();
            this.InstallLaunchButton = new System.Windows.Forms.Button();
            this.SettingsButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // DownloadInstallProgressBar
            // 
            this.DownloadInstallProgressBar.Location = new System.Drawing.Point(12, 51);
            this.DownloadInstallProgressBar.Name = "DownloadInstallProgressBar";
            this.DownloadInstallProgressBar.Size = new System.Drawing.Size(454, 30);
            this.DownloadInstallProgressBar.TabIndex = 0;
            // 
            // StatusLabel
            // 
            this.StatusLabel.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StatusLabel.Location = new System.Drawing.Point(102, 84);
            this.StatusLabel.Name = "StatusLabel";
            this.StatusLabel.Size = new System.Drawing.Size(364, 76);
            this.StatusLabel.TabIndex = 1;
            this.StatusLabel.Text = "Status";
            this.StatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // VersionDropdown
            // 
            this.VersionDropdown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.VersionDropdown.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.VersionDropdown.FormattingEnabled = true;
            this.VersionDropdown.Items.AddRange(new object[] {
            "Latest"});
            this.VersionDropdown.Location = new System.Drawing.Point(102, 12);
            this.VersionDropdown.Name = "VersionDropdown";
            this.VersionDropdown.Size = new System.Drawing.Size(240, 31);
            this.VersionDropdown.TabIndex = 4;
            this.VersionDropdown.SelectedIndexChanged += new System.EventHandler(this.VersionDropdown_SelectedIndexChanged);
            // 
            // VersionLabel
            // 
            this.VersionLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(38)))), ((int)(((byte)(41)))));
            this.VersionLabel.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.VersionLabel.ForeColor = System.Drawing.Color.White;
            this.VersionLabel.Location = new System.Drawing.Point(12, 12);
            this.VersionLabel.Name = "VersionLabel";
            this.VersionLabel.Size = new System.Drawing.Size(84, 32);
            this.VersionLabel.TabIndex = 5;
            this.VersionLabel.Text = "Version:";
            this.VersionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // InstallLaunchButton
            // 
            this.InstallLaunchButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(38)))), ((int)(((byte)(41)))));
            this.InstallLaunchButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(173)))), ((int)(((byte)(233)))));
            this.InstallLaunchButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(173)))), ((int)(((byte)(233)))));
            this.InstallLaunchButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.InstallLaunchButton.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.InstallLaunchButton.ForeColor = System.Drawing.Color.White;
            this.InstallLaunchButton.Location = new System.Drawing.Point(346, 12);
            this.InstallLaunchButton.Name = "InstallLaunchButton";
            this.InstallLaunchButton.Size = new System.Drawing.Size(118, 32);
            this.InstallLaunchButton.TabIndex = 6;
            this.InstallLaunchButton.Text = "Install";
            this.InstallLaunchButton.UseVisualStyleBackColor = false;
            this.InstallLaunchButton.Click += new System.EventHandler(this.InstallLaunchButton_Click);
            // 
            // SettingsButton
            // 
            this.SettingsButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(173)))), ((int)(((byte)(233)))));
            this.SettingsButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SettingsButton.Location = new System.Drawing.Point(10, 126);
            this.SettingsButton.Name = "SettingsButton";
            this.SettingsButton.Size = new System.Drawing.Size(86, 33);
            this.SettingsButton.TabIndex = 7;
            this.SettingsButton.Text = "Settings";
            this.SettingsButton.UseVisualStyleBackColor = false;
            this.SettingsButton.Click += new System.EventHandler(this.SettingsButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(144F, 144F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(38)))), ((int)(((byte)(41)))));
            this.ClientSize = new System.Drawing.Size(478, 170);
            this.Controls.Add(this.SettingsButton);
            this.Controls.Add(this.InstallLaunchButton);
            this.Controls.Add(this.VersionLabel);
            this.Controls.Add(this.VersionDropdown);
            this.Controls.Add(this.StatusLabel);
            this.Controls.Add(this.DownloadInstallProgressBar);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.Text = "Yuzu Updater v1.5.2";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ProgressBar DownloadInstallProgressBar;
        private System.Windows.Forms.Label StatusLabel;
        private System.Windows.Forms.ComboBox VersionDropdown;
        private System.Windows.Forms.Label VersionLabel;
        private System.Windows.Forms.Button InstallLaunchButton;
        private System.Windows.Forms.Button SettingsButton;
    }
}

