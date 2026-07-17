using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CivClassicLauncher
{
    public partial class UpdateForm : Form
    {
        private readonly string _downloadUrl;
        private readonly string _archiveUrl;
        private readonly string _releaseDate;

        public UpdateForm(
            Version currentVersion,
            Version latestVersion,
            string releaseDate,
            string changelog,
            string downloadUrl,
            string archiveUrl)
        {
            InitializeComponent();

            _downloadUrl = downloadUrl;
            _archiveUrl = archiveUrl;

            labelCurrent.Text = "Current Version: " + currentVersion;
            labelLatest.Text = "Latest Version: " + latestVersion;
            _releaseDate = releaseDate;

            labelDate.Text = "Release Date: " + releaseDate;

            textBoxChanges.Text = changelog;
        }

        private void buttonLater_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonDownload_Click(object sender, EventArgs e)
        {
            Process.Start(new ProcessStartInfo()
            {
                FileName = _downloadUrl,
                UseShellExecute = true
            });

            Close();
        }

        private void buttonArchive_Click(object sender, EventArgs e)
        {
            Process.Start(new ProcessStartInfo()
            {
                FileName = _archiveUrl,
                UseShellExecute = true
            });

            Close();
        }

        private void textBoxChanges_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
