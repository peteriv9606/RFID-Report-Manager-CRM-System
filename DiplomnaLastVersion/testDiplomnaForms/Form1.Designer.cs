namespace testDiplomnaForms
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.menuStrip2 = new System.Windows.Forms.MenuStrip();
            this.usersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addNewUserToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.updateExistingUserToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteUserToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewDatabaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.generateQueryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.generateReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rFIDSimulatorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cardsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutProgramToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripCurrentTime = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripUsernameTextBox = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshCardActiveStatusToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setDatabaseLocationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.logOutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.ConnectionStatusToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip2
            // 
            this.menuStrip2.BackColor = System.Drawing.SystemColors.Control;
            this.menuStrip2.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.menuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.usersToolStripMenuItem,
            this.dataToolStripMenuItem,
            this.rFIDSimulatorToolStripMenuItem,
            this.aboutToolStripMenuItem,
            this.toolStripCurrentTime,
            this.toolStripUsernameTextBox});
            this.menuStrip2.Location = new System.Drawing.Point(0, 0);
            this.menuStrip2.Name = "menuStrip2";
            this.menuStrip2.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.menuStrip2.Size = new System.Drawing.Size(984, 29);
            this.menuStrip2.TabIndex = 1;
            // 
            // usersToolStripMenuItem
            // 
            this.usersToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addNewUserToolStripMenuItem,
            this.updateExistingUserToolStripMenuItem,
            this.deleteUserToolStripMenuItem});
            this.usersToolStripMenuItem.Name = "usersToolStripMenuItem";
            this.usersToolStripMenuItem.Size = new System.Drawing.Size(65, 25);
            this.usersToolStripMenuItem.Text = "Users";
            // 
            // addNewUserToolStripMenuItem
            // 
            this.addNewUserToolStripMenuItem.Name = "addNewUserToolStripMenuItem";
            this.addNewUserToolStripMenuItem.Size = new System.Drawing.Size(238, 26);
            this.addNewUserToolStripMenuItem.Text = "Add New User";
            this.addNewUserToolStripMenuItem.Click += new System.EventHandler(this.addNewUserToolStripMenuItem_Click);
            // 
            // updateExistingUserToolStripMenuItem
            // 
            this.updateExistingUserToolStripMenuItem.Name = "updateExistingUserToolStripMenuItem";
            this.updateExistingUserToolStripMenuItem.Size = new System.Drawing.Size(238, 26);
            this.updateExistingUserToolStripMenuItem.Text = "Update Existing User";
            this.updateExistingUserToolStripMenuItem.Click += new System.EventHandler(this.updateExistingUserToolStripMenuItem_Click);
            // 
            // deleteUserToolStripMenuItem
            // 
            this.deleteUserToolStripMenuItem.Name = "deleteUserToolStripMenuItem";
            this.deleteUserToolStripMenuItem.Size = new System.Drawing.Size(238, 26);
            this.deleteUserToolStripMenuItem.Text = "Delete User";
            this.deleteUserToolStripMenuItem.Click += new System.EventHandler(this.deleteUserToolStripMenuItem_Click);
            // 
            // dataToolStripMenuItem
            // 
            this.dataToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewDatabaseToolStripMenuItem,
            this.generateQueryToolStripMenuItem,
            this.generateReportToolStripMenuItem});
            this.dataToolStripMenuItem.Name = "dataToolStripMenuItem";
            this.dataToolStripMenuItem.Size = new System.Drawing.Size(56, 25);
            this.dataToolStripMenuItem.Text = "Data";
            // 
            // viewDatabaseToolStripMenuItem
            // 
            this.viewDatabaseToolStripMenuItem.Name = "viewDatabaseToolStripMenuItem";
            this.viewDatabaseToolStripMenuItem.Size = new System.Drawing.Size(203, 26);
            this.viewDatabaseToolStripMenuItem.Text = "View Database";
            this.viewDatabaseToolStripMenuItem.Click += new System.EventHandler(this.viewDatabaseToolStripMenuItem_Click);
            // 
            // generateQueryToolStripMenuItem
            // 
            this.generateQueryToolStripMenuItem.Name = "generateQueryToolStripMenuItem";
            this.generateQueryToolStripMenuItem.Size = new System.Drawing.Size(203, 26);
            this.generateQueryToolStripMenuItem.Text = "Generate Query";
            this.generateQueryToolStripMenuItem.Click += new System.EventHandler(this.generateQueryToolStripMenuItem_Click);
            // 
            // generateReportToolStripMenuItem
            // 
            this.generateReportToolStripMenuItem.Name = "generateReportToolStripMenuItem";
            this.generateReportToolStripMenuItem.Size = new System.Drawing.Size(203, 26);
            this.generateReportToolStripMenuItem.Text = "Generate Report";
            this.generateReportToolStripMenuItem.Click += new System.EventHandler(this.generateReportToolStripMenuItem_Click);
            // 
            // rFIDSimulatorToolStripMenuItem
            // 
            this.rFIDSimulatorToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.cardsToolStripMenuItem});
            this.rFIDSimulatorToolStripMenuItem.Name = "rFIDSimulatorToolStripMenuItem";
            this.rFIDSimulatorToolStripMenuItem.Size = new System.Drawing.Size(65, 25);
            this.rFIDSimulatorToolStripMenuItem.Text = "RFID";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(173, 26);
            this.openToolStripMenuItem.Text = "Simulator";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // cardsToolStripMenuItem
            // 
            this.cardsToolStripMenuItem.Name = "cardsToolStripMenuItem";
            this.cardsToolStripMenuItem.Size = new System.Drawing.Size(173, 26);
            this.cardsToolStripMenuItem.Text = "Cards/Users";
            this.cardsToolStripMenuItem.Click += new System.EventHandler(this.cardsToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutProgramToolStripMenuItem});
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(69, 25);
            this.aboutToolStripMenuItem.Text = "About";
            // 
            // aboutProgramToolStripMenuItem
            // 
            this.aboutProgramToolStripMenuItem.Name = "aboutProgramToolStripMenuItem";
            this.aboutProgramToolStripMenuItem.Size = new System.Drawing.Size(196, 26);
            this.aboutProgramToolStripMenuItem.Text = "About Program";
            this.aboutProgramToolStripMenuItem.Click += new System.EventHandler(this.aboutProgramToolStripMenuItem_Click);
            // 
            // toolStripCurrentTime
            // 
            this.toolStripCurrentTime.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripCurrentTime.Enabled = false;
            this.toolStripCurrentTime.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.toolStripCurrentTime.Name = "toolStripCurrentTime";
            this.toolStripCurrentTime.ReadOnly = true;
            this.toolStripCurrentTime.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.toolStripCurrentTime.Size = new System.Drawing.Size(125, 25);
            this.toolStripCurrentTime.Text = "Current Time";
            this.toolStripCurrentTime.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // toolStripUsernameTextBox
            // 
            this.toolStripUsernameTextBox.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripUsernameTextBox.BackColor = System.Drawing.SystemColors.Control;
            this.toolStripUsernameTextBox.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.refreshCardActiveStatusToolStripMenuItem,
            this.setDatabaseLocationToolStripMenuItem,
            this.ConnectionStatusToolStripMenuItem,
            this.logOutToolStripMenuItem});
            this.toolStripUsernameTextBox.Name = "toolStripUsernameTextBox";
            this.toolStripUsernameTextBox.Size = new System.Drawing.Size(96, 25);
            this.toolStripUsernameTextBox.Text = "Username";
            this.toolStripUsernameTextBox.Click += new System.EventHandler(this.toolStripUsernameTextBox_Click);
            // 
            // refreshCardActiveStatusToolStripMenuItem
            // 
            this.refreshCardActiveStatusToolStripMenuItem.Name = "refreshCardActiveStatusToolStripMenuItem";
            this.refreshCardActiveStatusToolStripMenuItem.Size = new System.Drawing.Size(269, 26);
            this.refreshCardActiveStatusToolStripMenuItem.Text = "RefreshCardActiveStatus";
            this.refreshCardActiveStatusToolStripMenuItem.Click += new System.EventHandler(this.refreshCardActiveStatusToolStripMenuItem_Click);
            // 
            // setDatabaseLocationToolStripMenuItem
            // 
            this.setDatabaseLocationToolStripMenuItem.Name = "setDatabaseLocationToolStripMenuItem";
            this.setDatabaseLocationToolStripMenuItem.Size = new System.Drawing.Size(269, 26);
            this.setDatabaseLocationToolStripMenuItem.Text = "Set Database Location";
            this.setDatabaseLocationToolStripMenuItem.Click += new System.EventHandler(this.setDatabaseLocationToolStripMenuItem_Click);
            // 
            // logOutToolStripMenuItem
            // 
            this.logOutToolStripMenuItem.Name = "logOutToolStripMenuItem";
            this.logOutToolStripMenuItem.Size = new System.Drawing.Size(269, 26);
            this.logOutToolStripMenuItem.Text = "LogOut";
            this.logOutToolStripMenuItem.Click += new System.EventHandler(this.logOutToolStripMenuItem_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.DefaultExt = "accdb";
            this.openFileDialog1.Filter = "Access Database Files (*.accdb)|*.accdb";
            this.openFileDialog1.InitialDirectory = "C:\\";
            this.openFileDialog1.Title = "Locate file Diplomna_Database.accdb";
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // ConnectionStatusToolStripMenuItem
            // 
            this.ConnectionStatusToolStripMenuItem.Name = "ConnectionStatusToolStripMenuItem";
            this.ConnectionStatusToolStripMenuItem.Size = new System.Drawing.Size(269, 26);
            this.ConnectionStatusToolStripMenuItem.Text = "Connection Status";
            this.ConnectionStatusToolStripMenuItem.Click += new System.EventHandler(this.ConnectionStatusToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(984, 661);
            this.Controls.Add(this.menuStrip2);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Microsoft Tai Le", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip2;
            this.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.MinimumSize = new System.Drawing.Size(1000, 700);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Final Exam Program";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip2.ResumeLayout(false);
            this.menuStrip2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripMenuItem usersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addNewUserToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem updateExistingUserToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteUserToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rFIDSimulatorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutProgramToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewDatabaseToolStripMenuItem;
        private System.Windows.Forms.ToolStripTextBox toolStripCurrentTime;
        private System.Windows.Forms.Timer timer1;
        public System.Windows.Forms.MenuStrip menuStrip2;
        private System.Windows.Forms.ToolStripMenuItem generateQueryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem generateReportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem logOutToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem toolStripUsernameTextBox;
        private System.Windows.Forms.ToolStripMenuItem cardsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem refreshCardActiveStatusToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setDatabaseLocationToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ToolStripMenuItem ConnectionStatusToolStripMenuItem;
    }
}

