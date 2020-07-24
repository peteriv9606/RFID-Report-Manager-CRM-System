using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace testDiplomnaForms
{
    public partial class Form1 : Form
    {

        Form activeForm = new Form();
        OleDbConnection connection;
        string outOfDateCardId = "";
        public Form1()
        {
            InitializeComponent();
            menuStrip2.Enabled = false;
            timer1.Start();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            
            activeForm.Hide();
            loginForm loginForm = new loginForm();
            loginForm.MdiParent = this;
            loginForm.WindowState = FormWindowState.Maximized;
            activeForm = loginForm;
            loginForm.Show();
            connection = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + GlobalClass.FileNameAndPath);

        }
        private void aboutProgramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            activeForm.Hide();
            About aboutForm = new About();
            aboutForm.MdiParent = this;
            aboutForm.WindowState = FormWindowState.Maximized;
            activeForm = aboutForm;
            aboutForm.Show();
           
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.toolStripCurrentTime.Text = DateTime.Now.ToString();
        }		
        

        private void addNewUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            activeForm.Hide();
            AddUser addUsr = new AddUser();
            addUsr.MdiParent = this;
            addUsr.WindowState = FormWindowState.Maximized;
            activeForm = addUsr;
            addUsr.Show();
            if (toolStripUsernameTextBox.Text == "Visitor")
            { 
                addUsr.Enabled = false; 
            }
            else
            {
                addUsr.Enabled = true;
            }
        }

        private void updateExistingUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            activeForm.Hide();
            UpdateUser updUsr = new UpdateUser();
            updUsr.MdiParent = this;
            updUsr.WindowState = FormWindowState.Maximized;
            activeForm = updUsr;
            updUsr.Show();
            if (toolStripUsernameTextBox.Text == "Visitor")
            {
                updUsr.Enabled = false;
            }
            else
            {
                updUsr.Enabled = true;
            }
        }

        private void deleteUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            activeForm.Hide();
            DeleteUser delUsr = new DeleteUser();
            delUsr.MdiParent = this;
            delUsr.WindowState = FormWindowState.Maximized;
            activeForm = delUsr;
            delUsr.Show();
            if (toolStripUsernameTextBox.Text == "Visitor")
            {
                delUsr.Enabled = false;
            }
            else
            {
                delUsr.Enabled = true;
            }
        }
        public void successful_login()
        {
            this.menuStrip2.Enabled = true;
            refreshCardActiveStatus();
        }
        public void successful_logout()
        {
            
            activeForm.Hide();
            loginForm loginForm = new loginForm();
            loginForm.MdiParent = this;
            loginForm.WindowState = FormWindowState.Maximized;
            activeForm = loginForm;
            loginForm.Show();
            this.toolStripUsernameTextBox.Text = "Username";
            this.menuStrip2.Enabled = false;
        }

        private void viewDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            activeForm.Hide();
            Database db = new Database();
            db.MdiParent = this;
            db.WindowState = FormWindowState.Maximized;
            activeForm = db;
            db.Show();
            if (toolStripUsernameTextBox.Text == "Visitor")
            {
                db.Enabled = false;
            }
            else
            {
                db.Enabled = true;
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            activeForm.Hide();
            RFIDSimulator sim = new RFIDSimulator();
            sim.MdiParent = this;
            sim.WindowState = FormWindowState.Maximized;
            activeForm = sim;
            sim.Show();
            if (toolStripUsernameTextBox.Text == "Visitor")
            {
                sim.Enabled = false;
            }
            else
            {
                sim.Enabled = true;
            }
        }

      

        private void generateReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            activeForm.Hide();
            Reports reports = new Reports();
            reports.MdiParent = this;
            reports.WindowState = FormWindowState.Maximized;
            activeForm = reports;
            reports.Show();
            if (toolStripUsernameTextBox.Text == "Visitor")
            {
                reports.Enabled = false;
            }
            else
            {
                reports.Enabled = true;
            }
        }

        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            successful_logout();
        }

        private void cardsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            activeForm.Hide();
            Cards_Users cu = new Cards_Users();
            cu.MdiParent = this;
            cu.WindowState = FormWindowState.Maximized;
            activeForm = cu;
            cu.Show();
            if (toolStripUsernameTextBox.Text == "Visitor")
            {
                cu.Enabled = false;
            }
            else
            {
                cu.Enabled = true;
            }
        }

        private void generateQueryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            activeForm.Hide();
            Queries q = new Queries();
            q.MdiParent = this;
            q.WindowState = FormWindowState.Maximized;
            activeForm = q;
            q.Show();
            if(toolStripUsernameTextBox.Text == "Visitor")
            {
                q.Enabled = false;
            }
            else
            {
                q.Enabled = true;
            }
        }

        private void refreshCardActiveStatus()
        {
            //refresh card active status

            int count = 0;
            try
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                OleDbCommand updcommand = new OleDbCommand();
                command.Connection = connection;
                updcommand.Connection = connection;
                command.CommandText = "SELECT * FROM Cards";
                OleDbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    if (reader["ActiveDate"].ToString() != "")
                    {
                        if (DateTime.Compare(DateTime.Parse(reader["ActiveDate"].ToString()), DateTime.Now) < 0)
                        {
                            if (reader["isActive"].ToString() == "True")
                            //out of date cards
                            {
                                count++;
                                outOfDateCardId = reader["CardID"].ToString();
                                try
                                {
                                    updcommand.CommandText = @"UPDATE Cards SET isActive = False WHERE [CardID] = '" + outOfDateCardId + "';";
                                    updcommand.ExecuteNonQuery();
                                    
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show("Error " + ex);
                                    connection.Close();
                                } 
                            }
                        }
                    }
                }
                reader.Dispose();
                connection.Close();
                if (count == 0)
                {
                    MessageBox.Show("All cards are up to date!","Info",MessageBoxButtons.OK,MessageBoxIcon.Information);
                }
                else if (count == 1)
                {
                    MessageBox.Show("Found 1 card that is out of date. Status updated.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if(count>1)
                {
                    MessageBox.Show("Found " + count + " cards that are out of date. Status updated.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex);
                connection.Close();
            }
        }

        private void refreshCardActiveStatusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("Are you sure you want to update card status?\nCurrent form will close!", "Are you sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res == DialogResult.Yes)
            {
                refreshCardActiveStatus();
                activeForm.Hide();
            }
        }

        private void setDatabaseLocationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult change = MessageBox.Show("Do you wish to change the database?\nYou will be logged-out!", "Are you sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(change==DialogResult.Yes)
            {
                string currFileName = GlobalClass.FileName;
                string currFilePath = GlobalClass.FilePath;
                string currFileNameAndPath = GlobalClass.FileNameAndPath;
                string currConnectionStatus = GlobalClass.ConnectionStatus;

                openFileDialog1.ShowDialog();

                while (GlobalClass.FileName != "Diplomna_Database.accdb") //check for valid file name --IF NOT diplomna_database
                {
                    DialogResult res = MessageBox.Show("Connection Status not successful.\nPlease locate file 'Diplomna_Database.accdb'.", "Wrong selection", MessageBoxButtons.RetryCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                    if (res == DialogResult.Retry)
                    {
                        openFileDialog1.Dispose();
                        GlobalClass.FileName = "";
                        GlobalClass.FilePath = "";
                        GlobalClass.FileNameAndPath = "";
                        GlobalClass.ConnectionStatus = "";
                        openFileDialog1.FileName = "";
                        openFileDialog1.ShowDialog();
                    }
                    else if (res == DialogResult.Cancel)
                    {
                        MessageBox.Show("No Change in Database.\nUsing old database settings.", "No Change", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                        GlobalClass.FileName = currFileName;
                        GlobalClass.FilePath = currFilePath;
                        GlobalClass.FileNameAndPath = currFileNameAndPath;
                        GlobalClass.ConnectionStatus = currConnectionStatus;
                    }
                }
                if (currFileName != GlobalClass.FileName || currFileNameAndPath != GlobalClass.FileNameAndPath)
                {
                    //successful CHANGE of file
                    GlobalClass.FileName = openFileDialog1.SafeFileName;
                    GlobalClass.FileNameAndPath = openFileDialog1.FileName;
                    GlobalClass.FilePath = System.IO.Path.GetDirectoryName(GlobalClass.FileNameAndPath);
                    connection = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + GlobalClass.FileNameAndPath);
                    GlobalClass.ConnectionStatus = "Successful";
                    MessageBox.Show("Connection to database status: " + GlobalClass.ConnectionStatus + "!\nConnected to: " + GlobalClass.FileName + "\nLocation: " + GlobalClass.FileNameAndPath + "\nPathOnly: " + GlobalClass.FilePath + "\n\nLogging out..", GlobalClass.ConnectionStatus, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    successful_logout();
                }
                else
                {
                    MessageBox.Show("No Change in Database.\nUsing old database settings.", "No Change", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                    GlobalClass.FileName = currFileName;
                    GlobalClass.FilePath = currFilePath;
                    GlobalClass.FileNameAndPath = currFileNameAndPath;
                    GlobalClass.ConnectionStatus = currConnectionStatus;
                }

            }
            
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            GlobalClass.FileName = openFileDialog1.SafeFileName;
            GlobalClass.FileNameAndPath = openFileDialog1.FileName;  
        }

        private void toolStripUsernameTextBox_Click(object sender, EventArgs e)
        {
            if (toolStripUsernameTextBox.Text == "Visitor")
            { 
                setDatabaseLocationToolStripMenuItem.Enabled = false;
                refreshCardActiveStatusToolStripMenuItem.Enabled = false;
            }
            else
            {
                setDatabaseLocationToolStripMenuItem.Enabled = true;
                refreshCardActiveStatusToolStripMenuItem.Enabled = true;
            }
        }

        private void ConnectionStatusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Connection to database status: " + GlobalClass.ConnectionStatus + "!\nConnected to: " + GlobalClass.FileName + "\n\nLocation: " + GlobalClass.FileNameAndPath, "Connection Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
