namespace CivClassicLauncher
{
    partial class UpdateForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UpdateForm));
            labelTitle = new Label();
            labelCurrent = new Label();
            labelLatest = new Label();
            textBoxChanges = new TextBox();
            buttonDownload = new Button();
            buttonLater = new Button();
            label1 = new Label();
            labelDate = new Label();
            buttonArchive = new Button();
            SuspendLayout();
            // 
            // labelTitle
            // 
            labelTitle.AutoSize = true;
            labelTitle.BackColor = Color.Transparent;
            labelTitle.Font = new Font("Book Antiqua", 15.75F, FontStyle.Bold | FontStyle.Underline, GraphicsUnit.Point, 0);
            labelTitle.Location = new Point(78, 21);
            labelTitle.Name = "labelTitle";
            labelTitle.Size = new Size(390, 26);
            labelTitle.TabIndex = 0;
            labelTitle.Text = "Civilization Classics Collection Update";
            // 
            // labelCurrent
            // 
            labelCurrent.AutoSize = true;
            labelCurrent.BackColor = Color.Transparent;
            labelCurrent.Font = new Font("Book Antiqua", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelCurrent.Location = new Point(12, 80);
            labelCurrent.Name = "labelCurrent";
            labelCurrent.Size = new Size(132, 21);
            labelCurrent.TabIndex = 1;
            labelCurrent.Text = "Current Version:";
            // 
            // labelLatest
            // 
            labelLatest.AutoSize = true;
            labelLatest.BackColor = Color.Transparent;
            labelLatest.Font = new Font("Book Antiqua", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelLatest.Location = new Point(26, 111);
            labelLatest.Name = "labelLatest";
            labelLatest.Size = new Size(118, 21);
            labelLatest.TabIndex = 2;
            labelLatest.Text = "Latest Version:";
            // 
            // textBoxChanges
            // 
            textBoxChanges.BackColor = Color.NavajoWhite;
            textBoxChanges.BorderStyle = BorderStyle.FixedSingle;
            textBoxChanges.Location = new Point(283, 85);
            textBoxChanges.Multiline = true;
            textBoxChanges.Name = "textBoxChanges";
            textBoxChanges.ReadOnly = true;
            textBoxChanges.ScrollBars = ScrollBars.Vertical;
            textBoxChanges.Size = new Size(239, 186);
            textBoxChanges.TabIndex = 3;
            textBoxChanges.TextChanged += textBoxChanges_TextChanged;
            // 
            // buttonDownload
            // 
            buttonDownload.BackColor = Color.NavajoWhite;
            buttonDownload.FlatAppearance.BorderColor = Color.Orange;
            buttonDownload.FlatAppearance.BorderSize = 2;
            buttonDownload.FlatStyle = FlatStyle.Flat;
            buttonDownload.Font = new Font("Book Antiqua", 9F, FontStyle.Bold);
            buttonDownload.Location = new Point(26, 181);
            buttonDownload.Name = "buttonDownload";
            buttonDownload.Size = new Size(154, 52);
            buttonDownload.TabIndex = 4;
            buttonDownload.Text = "Download Update";
            buttonDownload.UseVisualStyleBackColor = false;
            buttonDownload.Click += buttonDownload_Click;
            // 
            // buttonLater
            // 
            buttonLater.BackColor = Color.NavajoWhite;
            buttonLater.FlatAppearance.BorderColor = Color.Orange;
            buttonLater.FlatStyle = FlatStyle.Flat;
            buttonLater.Font = new Font("Book Antiqua", 9F, FontStyle.Bold);
            buttonLater.Location = new Point(437, 277);
            buttonLater.Name = "buttonLater";
            buttonLater.Size = new Size(85, 33);
            buttonLater.TabIndex = 5;
            buttonLater.Text = "Later";
            buttonLater.UseVisualStyleBackColor = false;
            buttonLater.Click += buttonLater_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Book Antiqua", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(349, 63);
            label1.Name = "label1";
            label1.Size = new Size(101, 19);
            label1.TabIndex = 6;
            label1.Text = "What's New?";
            // 
            // labelDate
            // 
            labelDate.AutoSize = true;
            labelDate.BackColor = Color.Transparent;
            labelDate.Font = new Font("Book Antiqua", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelDate.Location = new Point(36, 144);
            labelDate.Name = "labelDate";
            labelDate.Size = new Size(108, 21);
            labelDate.TabIndex = 7;
            labelDate.Text = "Release Date:";
            // 
            // buttonArchive
            // 
            buttonArchive.BackColor = Color.NavajoWhite;
            buttonArchive.FlatAppearance.BorderColor = Color.Orange;
            buttonArchive.FlatAppearance.BorderSize = 2;
            buttonArchive.FlatStyle = FlatStyle.Flat;
            buttonArchive.Font = new Font("Book Antiqua", 9F, FontStyle.Bold);
            buttonArchive.Location = new Point(26, 239);
            buttonArchive.Name = "buttonArchive";
            buttonArchive.Size = new Size(154, 50);
            buttonArchive.TabIndex = 8;
            buttonArchive.Text = "View Archive";
            buttonArchive.UseVisualStyleBackColor = false;
            // 
            // UpdateForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(546, 322);
            Controls.Add(buttonArchive);
            Controls.Add(labelDate);
            Controls.Add(label1);
            Controls.Add(buttonLater);
            Controls.Add(buttonDownload);
            Controls.Add(textBoxChanges);
            Controls.Add(labelLatest);
            Controls.Add(labelCurrent);
            Controls.Add(labelTitle);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "UpdateForm";
            Text = "Update Avaliable";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label labelTitle;
        private Label labelCurrent;
        private Label labelLatest;
        private TextBox textBoxChanges;
        private Button buttonDownload;
        private Button buttonLater;
        private Label label1;
        private Label labelDate;
        private Button buttonArchive;
    }
}