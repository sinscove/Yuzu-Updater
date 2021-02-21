using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yuzu_Updater
{
    public sealed class Settings
    {
        public bool AutoLaunch { get; set; }
        public bool AutoClose { get; set; }

        public bool LaunchAsAdmin { get; set; }

        public bool BackupSaveFiles { get; set; }
        public bool AcceleratedDownloads { get; set; }

        public int MaxConnections { get; set; }
        public bool SinMode { get; set; }
        public string Version { get; set; }

        public string DesiredVersion { get; set; }

        private static readonly Lazy<Settings> _settings = new Lazy<Settings>(() => new Settings());
        public static Settings GetInstance()
        {
            return _settings.Value;
        }
        private Settings()
        {
            try
            {
                string[] lines = File.ReadAllLines(Directory.GetCurrentDirectory() + "/settings.ini");

                foreach (String line in lines)
                {
                    int splitIndex = line.IndexOf('=');
                    if (splitIndex > -1)
                    {
                        String key = line.Substring(0, splitIndex);
                        String value = line.Substring(splitIndex + 1);

                        switch (key)
                        {
                            case "AutoLaunch":
                                try
                                {
                                    this.AutoLaunch = Boolean.Parse(value);
                                }
                                catch (Exception)
                                {
                                    this.AutoLaunch = false;
                                }
                                break;
                            case "LaunchAsAdmin":
                                try
                                {
                                    this.LaunchAsAdmin = Boolean.Parse(value);
                                }
                                catch (Exception)
                                {
                                    this.LaunchAsAdmin = false;
                                }
                                break;
                            case "AutoClose":
                                try
                                {
                                    this.AutoClose = Boolean.Parse(value);
                                }
                                catch (Exception)
                                {
                                    this.AutoClose = false;
                                }
                                break;
                            case "Version":
                                this.Version = value;
                                break;
                            case "DesiredVersion":
                                this.DesiredVersion = value;
                                break;
                            case "BackupSaveFiles":
                                this.BackupSaveFiles = Boolean.Parse(value);
                                break;
                            case "AcceleratedDownloads":
                                this.AcceleratedDownloads = Boolean.Parse(value);
                                break;
                            case "MaxConnections":
                                this.MaxConnections = int.Parse(value);
                                break;
                            case "SinMode":
                                this.SinMode = Boolean.Parse(value);
                                break;
                            default: break;
                        }

                        if(this.Version == null)
                        {
                            this.Version = "";
                        }
                        if (this.DesiredVersion == null)
                        {
                            this.DesiredVersion = "Latest";
                        }
                        if(this.MaxConnections == 0)
                        {
                            this.MaxConnections = 4;
                        }
                    }

                }
            }
            catch (Exception)
            {
                this.AutoLaunch = false;
                this.AutoClose = false;
                this.LaunchAsAdmin = false;
                this.BackupSaveFiles = false;
                this.AcceleratedDownloads = true;
                this.DesiredVersion = "Latest";
                this.Version = "";
                this.MaxConnections = 4;
                this.SinMode = false;
            }
        }

        public void Save()
        {
            try
            {
                File.WriteAllText(Directory.GetCurrentDirectory() + "/settings.ini", $"AutoClose={this.AutoClose}\r\nAutoLaunch={this.AutoLaunch}\r\nLaunchAsAdmin={this.LaunchAsAdmin}\r\nVersion={this.Version}\r\nDesiredVersion={this.DesiredVersion}\r\nBackupSaveFiles={this.BackupSaveFiles}\r\nAcceleratedDownloads={this.AcceleratedDownloads}\r\nMaxConnections={this.MaxConnections}\r\nSinMode={this.SinMode}");
            }
            catch (Exception)
            {
                Console.WriteLine("Couldn't save settings!");
            }
        }
    }
}
