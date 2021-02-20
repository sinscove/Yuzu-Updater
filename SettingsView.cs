using Yuzu_Updater;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Yuzu_Updater
{
    public partial class SettingsView : Form
    {
        private Settings _settings = Settings.GetInstance();
        public SettingsView()
        {
            InitializeComponent();
        }

        private void AutoLaunchCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            _settings.AutoLaunch = AutoLaunchCheckbox.Checked;
            _settings.Save();
        }

        private void LaunchAsAdminCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            _settings.LaunchAsAdmin = LaunchAsAdminCheckbox.Checked;
            _settings.Save();
        }

        private void AutoCloseCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            _settings.AutoClose = AutoCloseCheckbox.Checked;
            _settings.Save();
        }

        private void BackupSaveFilesCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            _settings.BackupSaveFiles = BackupSaveFilesCheckbox.Checked;
            _settings.Save();
        }

        private void SettingsView_Load(object sender, EventArgs e)
        {
            AutoCloseCheckbox.Checked = _settings.AutoClose;
            AutoLaunchCheckbox.Checked = _settings.AutoLaunch;
            SinModeCheckbox.Checked = _settings.SinMode;
            LaunchAsAdminCheckbox.Checked = _settings.LaunchAsAdmin;
            BackupSaveFilesCheckbox.Checked = _settings.BackupSaveFiles;
            AcceleratedDownloadsCheckbox.Checked = _settings.AcceleratedDownloads;
            MaxConnectionsCounter.Value = _settings.MaxConnections;
        }

        private void AcceleratedDownloadsCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            _settings.AcceleratedDownloads = AcceleratedDownloadsCheckbox.Checked;
            _settings.Save();
        }

        private void MaxConnectionsCounter_ValueChanged(object sender, EventArgs e)
        {
            _settings.MaxConnections = (int)MaxConnectionsCounter.Value;
            _settings.Save();
        }

        private void SinModeCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            _settings.SinMode = SinModeCheckbox.Checked;
            _settings.Save();
        }
    }
}
