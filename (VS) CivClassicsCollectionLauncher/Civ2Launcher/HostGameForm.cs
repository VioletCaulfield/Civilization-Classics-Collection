using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CivClassicLauncher
{
    public partial class HostGameForm : Form
    {
        public HostGameForm()
        {
            InitializeComponent();
        }

        private void ShowStep(int step)
        {
            panelStep1.Visible = step == 1;
            panelStep2.Visible = step == 2;
            panelStep3.Visible = step == 3;
            panelStep4.Visible = step == 4;
        }
    }
}
