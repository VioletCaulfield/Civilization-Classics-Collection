namespace CivClassicLauncher
{
    partial class HostGameForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HostGameForm));
            panelStep1 = new Panel();
            panelStep2 = new Panel();
            panelStep3 = new Panel();
            panelStep4 = new Panel();
            SuspendLayout();
            // 
            // panelStep1
            // 
            panelStep1.BackColor = Color.Transparent;
            panelStep1.BackgroundImage = (Image)resources.GetObject("panelStep1.BackgroundImage");
            panelStep1.BackgroundImageLayout = ImageLayout.Stretch;
            panelStep1.Dock = DockStyle.Fill;
            panelStep1.Location = new Point(0, 0);
            panelStep1.Name = "panelStep1";
            panelStep1.Size = new Size(671, 473);
            panelStep1.TabIndex = 0;
            // 
            // panelStep2
            // 
            panelStep2.BackColor = Color.Transparent;
            panelStep2.BackgroundImage = (Image)resources.GetObject("panelStep2.BackgroundImage");
            panelStep2.BackgroundImageLayout = ImageLayout.Stretch;
            panelStep2.Dock = DockStyle.Fill;
            panelStep2.Location = new Point(0, 0);
            panelStep2.Name = "panelStep2";
            panelStep2.Size = new Size(671, 473);
            panelStep2.TabIndex = 1;
            // 
            // panelStep3
            // 
            panelStep3.BackColor = Color.Transparent;
            panelStep3.BackgroundImage = (Image)resources.GetObject("panelStep3.BackgroundImage");
            panelStep3.BackgroundImageLayout = ImageLayout.Stretch;
            panelStep3.Dock = DockStyle.Fill;
            panelStep3.Location = new Point(0, 0);
            panelStep3.Name = "panelStep3";
            panelStep3.Size = new Size(671, 473);
            panelStep3.TabIndex = 2;
            // 
            // panelStep4
            // 
            panelStep4.BackColor = Color.Transparent;
            panelStep4.BackgroundImage = (Image)resources.GetObject("panelStep4.BackgroundImage");
            panelStep4.BackgroundImageLayout = ImageLayout.Stretch;
            panelStep4.Dock = DockStyle.Fill;
            panelStep4.Location = new Point(0, 0);
            panelStep4.Name = "panelStep4";
            panelStep4.Size = new Size(671, 473);
            panelStep4.TabIndex = 3;
            // 
            // HostGameForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(671, 473);
            Controls.Add(panelStep4);
            Controls.Add(panelStep3);
            Controls.Add(panelStep2);
            Controls.Add(panelStep1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "HostGameForm";
            Text = "Host - Game Setup";
            ResumeLayout(false);
        }

        #endregion

        private Panel panelStep1;
        private Panel panelStep2;
        private Panel panelStep3;
        private Panel panelStep4;
    }
}