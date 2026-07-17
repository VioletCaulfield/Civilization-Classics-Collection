namespace CivClassicLauncher
{
    partial class FormMultiplayer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMultiplayer));
            labelStatus = new Label();
            listViewServers = new ListView();
            buttonHost = new Button();
            buttonJoin = new Button();
            buttonRefresh = new Button();
            buttonClose = new Button();
            groupBoxSelectedGame = new GroupBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            columnGame = new ColumnHeader();
            columnPlayers = new ColumnHeader();
            columnPing = new ColumnHeader();
            columnScenario = new ColumnHeader();
            SuspendLayout();
            // 
            // labelStatus
            // 
            labelStatus.AutoSize = true;
            labelStatus.BackColor = Color.Transparent;
            labelStatus.Font = new Font("Book Antiqua", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelStatus.Location = new Point(24, 567);
            labelStatus.Name = "labelStatus";
            labelStatus.Size = new Size(116, 21);
            labelStatus.TabIndex = 0;
            labelStatus.Text = "Status: Offline";
            // 
            // listViewServers
            // 
            listViewServers.BackColor = Color.BurlyWood;
            listViewServers.BorderStyle = BorderStyle.FixedSingle;
            listViewServers.Columns.AddRange(new ColumnHeader[] { columnGame, columnScenario, columnPlayers, columnPing });
            listViewServers.Font = new Font("Bookman Old Style", 9.75F);
            listViewServers.FullRowSelect = true;
            listViewServers.GridLines = true;
            listViewServers.Location = new Point(221, 135);
            listViewServers.MultiSelect = false;
            listViewServers.Name = "listViewServers";
            listViewServers.Size = new Size(335, 343);
            listViewServers.TabIndex = 1;
            listViewServers.UseCompatibleStateImageBehavior = false;
            listViewServers.View = View.Details;
            listViewServers.SelectedIndexChanged += listViewServers_SelectedIndexChanged;
            // 
            // buttonHost
            // 
            buttonHost.BackColor = Color.Transparent;
            buttonHost.BackgroundImage = (Image)resources.GetObject("buttonHost.BackgroundImage");
            buttonHost.BackgroundImageLayout = ImageLayout.Stretch;
            buttonHost.FlatAppearance.BorderSize = 0;
            buttonHost.FlatStyle = FlatStyle.Flat;
            buttonHost.Font = new Font("Bookman Old Style", 9.75F);
            buttonHost.Location = new Point(33, 155);
            buttonHost.Name = "buttonHost";
            buttonHost.Size = new Size(167, 86);
            buttonHost.TabIndex = 2;
            buttonHost.Text = "Host Multiplayer";
            buttonHost.UseVisualStyleBackColor = false;
            // 
            // buttonJoin
            // 
            buttonJoin.BackColor = Color.Transparent;
            buttonJoin.BackgroundImage = (Image)resources.GetObject("buttonJoin.BackgroundImage");
            buttonJoin.BackgroundImageLayout = ImageLayout.Stretch;
            buttonJoin.FlatAppearance.BorderSize = 0;
            buttonJoin.FlatStyle = FlatStyle.Flat;
            buttonJoin.Font = new Font("Bookman Old Style", 9.75F);
            buttonJoin.Location = new Point(33, 269);
            buttonJoin.Name = "buttonJoin";
            buttonJoin.Size = new Size(167, 86);
            buttonJoin.TabIndex = 3;
            buttonJoin.Text = "Join Selected";
            buttonJoin.UseVisualStyleBackColor = false;
            // 
            // buttonRefresh
            // 
            buttonRefresh.BackColor = Color.Transparent;
            buttonRefresh.BackgroundImage = (Image)resources.GetObject("buttonRefresh.BackgroundImage");
            buttonRefresh.BackgroundImageLayout = ImageLayout.Stretch;
            buttonRefresh.FlatAppearance.BorderSize = 0;
            buttonRefresh.FlatStyle = FlatStyle.Flat;
            buttonRefresh.Font = new Font("Bookman Old Style", 9.75F);
            buttonRefresh.ForeColor = Color.Transparent;
            buttonRefresh.Location = new Point(539, 114);
            buttonRefresh.Name = "buttonRefresh";
            buttonRefresh.Size = new Size(17, 18);
            buttonRefresh.TabIndex = 4;
            buttonRefresh.UseVisualStyleBackColor = false;
            buttonRefresh.Click += buttonRefresh_Click;
            // 
            // buttonClose
            // 
            buttonClose.BackColor = Color.Transparent;
            buttonClose.BackgroundImage = (Image)resources.GetObject("buttonClose.BackgroundImage");
            buttonClose.BackgroundImageLayout = ImageLayout.Stretch;
            buttonClose.FlatAppearance.BorderSize = 0;
            buttonClose.FlatStyle = FlatStyle.Flat;
            buttonClose.Font = new Font("Bookman Old Style", 9.75F);
            buttonClose.Location = new Point(33, 385);
            buttonClose.Name = "buttonClose";
            buttonClose.Size = new Size(167, 86);
            buttonClose.TabIndex = 5;
            buttonClose.Text = "Close";
            buttonClose.UseVisualStyleBackColor = false;
            // 
            // groupBoxSelectedGame
            // 
            groupBoxSelectedGame.BackColor = Color.BurlyWood;
            groupBoxSelectedGame.Font = new Font("Bookman Old Style", 9.75F);
            groupBoxSelectedGame.Location = new Point(320, 509);
            groupBoxSelectedGame.Name = "groupBoxSelectedGame";
            groupBoxSelectedGame.Size = new Size(236, 68);
            groupBoxSelectedGame.TabIndex = 6;
            groupBoxSelectedGame.TabStop = false;
            groupBoxSelectedGame.Text = "groupBox1";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Book Antiqua", 18F, FontStyle.Bold | FontStyle.Underline, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.Black;
            label1.Location = new Point(115, 36);
            label1.Name = "label1";
            label1.Size = new Size(356, 56);
            label1.TabIndex = 7;
            label1.Text = "Civilization Classics Collection\r\nMultiplayer\r\n";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            label1.Click += label1_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.Font = new Font("Book Antiqua", 12F, FontStyle.Bold);
            label2.ForeColor = Color.Black;
            label2.Location = new Point(320, 111);
            label2.Name = "label2";
            label2.Size = new Size(134, 21);
            label2.TabIndex = 8;
            label2.Text = "Available Games";
            label2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.Transparent;
            label3.Font = new Font("Book Antiqua", 12F, FontStyle.Bold);
            label3.ForeColor = Color.Black;
            label3.Location = new Point(376, 485);
            label3.Name = "label3";
            label3.Size = new Size(122, 21);
            label3.TabIndex = 9;
            label3.Text = "Selected Server";
            label3.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // columnGame
            // 
            columnGame.Text = "Game";
            columnGame.Width = 70;
            // 
            // columnPlayers
            // 
            columnPlayers.Text = "Players";
            columnPlayers.TextAlign = HorizontalAlignment.Center;
            // 
            // columnPing
            // 
            columnPing.Text = "Ping";
            columnPing.TextAlign = HorizontalAlignment.Center;
            columnPing.Width = 50;
            // 
            // columnScenario
            // 
            columnScenario.Text = "Scenario";
            columnScenario.TextAlign = HorizontalAlignment.Center;
            columnScenario.Width = 150;
            // 
            // FormMultiplayer
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(583, 622);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(groupBoxSelectedGame);
            Controls.Add(buttonClose);
            Controls.Add(buttonRefresh);
            Controls.Add(buttonJoin);
            Controls.Add(buttonHost);
            Controls.Add(listViewServers);
            Controls.Add(labelStatus);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "FormMultiplayer";
            Text = "CivClassicCollection Multiplayer";
            Load += FormMultiplayer_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label labelStatus;
        private ListView listViewServers;
        private Button buttonHost;
        private Button buttonJoin;
        private Button buttonRefresh;
        private Button buttonClose;
        private GroupBox groupBoxSelectedGame;
        private Label label1;
        private Label label2;
        private Label label3;
        private ColumnHeader columnGame;
        private ColumnHeader columnPlayers;
        private ColumnHeader columnPing;
        private ColumnHeader columnScenario;
    }
}