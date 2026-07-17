using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace CivClassicLauncher
{
    public static class Launcher
    {
        private static readonly string BasePath =
            AppDomain.CurrentDomain.BaseDirectory;

        public static void StartCiv1()
        {
            string path = Path.Combine(
                BasePath,
                "Civ1",
                "LaunchCiv1.bat"
            );

            Process.Start(new ProcessStartInfo(path)
            {
                UseShellExecute = true,
                WorkingDirectory = Path.GetDirectoryName(path)
            });
        }

        public static void StartMGEOriginal()
        {
            string path = Path.Combine(
                BasePath,
                "Civ2MGE",
                "civ2.exe"
            );

            Process.Start(new ProcessStartInfo(path)
            {
                UseShellExecute = true,
                WorkingDirectory = Path.GetDirectoryName(path)
            });
        }

        public static void StartMGEEnhanced()
        {
            string path = Path.Combine(
                BasePath,
                "Civ2MGE",
                "Civ2UIALauncher.exe"
            );

            Process.Start(new ProcessStartInfo(path)
            {
                UseShellExecute = true,
                WorkingDirectory = Path.GetDirectoryName(path)
            });
        }

        public static void OpenDocumentation()
        {
            string path = Path.Combine(
                BasePath,
                "Documentation"
            );

            Process.Start(new ProcessStartInfo()
            {
                FileName = path,
                UseShellExecute = true
            });
        }

        public static void StartToT()
        {
            string path = Path.Combine(
                BasePath,
                "Civ2ToT",
                "civ2.exe"
            );

            Process.Start(new ProcessStartInfo(path)
            {
                UseShellExecute = true,
                WorkingDirectory = Path.GetDirectoryName(path)
            });
        }

        public static Process StartMGEEnhancedProcess()
        {
            string path = Path.Combine(
                BasePath,
                "Civ2MGE",
                "Civ2UIALauncher.exe"
            );

            return Process.Start(
                new ProcessStartInfo(path)
                {
                    UseShellExecute = true,
                    WorkingDirectory = Path.GetDirectoryName(path)
                }
            );
        }

        public static Process StartToTProcess()
        {
            string path = Path.Combine(
                BasePath,
                "Civ2ToT",
                "civ2.exe"
            );

            return Process.Start(
                new ProcessStartInfo(path)
                {
                    UseShellExecute = true,
                    WorkingDirectory = Path.GetDirectoryName(path)
                }
            );
        }

        public static void OpenSaves()
        {
            string path = Path.Combine(
                BasePath,
                "Saves"
            );

            if (Directory.Exists(path))
            {
                Process.Start(new ProcessStartInfo()
                {
                    FileName = path,
                    UseShellExecute = true
                });
            }
            else
            {
                MessageBox.Show(
                    "Saves folder not found.",
                    "Civilization Classics Collection",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
        }

        public static void OpenMods()
        {
            string path = Path.Combine(
                BasePath,
                "Mods"
            );

            if (Directory.Exists(path))
            {
                Process.Start(new ProcessStartInfo()
                {
                    FileName = path,
                    UseShellExecute = true
                });
            }
            else
            {
                MessageBox.Show(
                    "Mods folder not found.",
                    "Civilization Classics Collection",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
        }


        public static void Quit()
        {
            Application.Exit();
        }
    }
}