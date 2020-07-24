using System;
using System.Windows.Forms;
using System.Data.OleDb;


namespace testDiplomnaForms
{
    public partial class loginForm : Form
    {

        OleDbConnection connection;

        public loginForm()
        {
            InitializeComponent();
            ConnectToDatabaseLabel.Enabled = false;
            if (GlobalClass.FirstRun == false)//not a first run
            {
                ConnectToDatabaseLabel.Text = "Connection to Database Successful";
                connection = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + GlobalClass.FileNameAndPath);
            }
            else
            {
               //it's a first run     
                MessageBox.Show("Locate databse.", "First Run",MessageBoxButtons.OK, MessageBoxIcon.Information,MessageBoxDefaultButton.Button1,MessageBoxOptions.DefaultDesktopOnly);
                
                openFileDialog1.ShowDialog();
                while (GlobalClass.FileName != "Diplomna_Database.accdb")
                {
                    DialogResult res = MessageBox.Show("Connection Status not successful.\nPlease locate file 'Diplomna_Database.accdb'.", "Wrong selection", MessageBoxButtons.RetryCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                    if (res == DialogResult.Retry)
                    {
                        openFileDialog1.Dispose();
                        GlobalClass.ConnectionStatus = "";
                        GlobalClass.FileName = "";
                        GlobalClass.FilePath = "";
                        openFileDialog1.ShowDialog();
                    }
                    else if (res == DialogResult.Cancel)
                    {
                        MessageBox.Show("No database selected.\nExiting..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error,MessageBoxDefaultButton.Button1,MessageBoxOptions.DefaultDesktopOnly);
                        Environment.Exit(0);  
                    }
                }
                //successful selection of file
                setFileLocation();
            }     
        }

        private Form1 mainForm = null;
        public loginForm(Form callingForm)
        {
            mainForm = callingForm as Form1;
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Are you sure you want to exit?", "Exit?", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            if (dr == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private bool EmptyUsername()
        {
            if (UsernameTextBox.Text == "")
                return true;
            else
                return false;
        }
        private bool EmptyPassword()
        {
            if (PasswordTextBox.Text == "")
                return true;
            else
                return false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!EmptyUsername() || !EmptyPassword()) //if username/passwod is not empty
            {


                if (GlobalClass.ConnectionStatus == "Successful") //if it connected successfuly
                {
                    if (checkBox1.Checked == true)//its a visitor
                    {
                        
                        Form1 f1 = (Form1)this.MdiParent;
                        f1.toolStripUsernameTextBox.Text = "Visitor";
                        MessageBox.Show("Logged in as 'Visitor'", "Login Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        GlobalClass.CurrentUser = f1.toolStripUsernameTextBox.Text;
                        Close();
                    }
                    else //has to check in the database for correct/existing username and password
                    {
                        try
                        {
                            connection.Open();
                            OleDbCommand command = new OleDbCommand();
                            command.Connection = connection;
                            command.CommandText = "SELECT * FROM Users WHERE Username= '" + UsernameTextBox.Text + "';";
                            OleDbDataReader reader = command.ExecuteReader();
                            if (reader.Read())
                            {
                                if (PasswordTextBox.Text == reader["Password"].ToString())
                                {

                                    GlobalClass.FirstRun = false;
                                    //and finally change the top username textbox
                                    Form1 f1 = (Form1)this.MdiParent;
                                    f1.toolStripUsernameTextBox.Text = UsernameTextBox.Text;
                                    GlobalClass.CurrentUser = f1.toolStripUsernameTextBox.Text;
                                    string adminFullName = reader["Name"].ToString();
                                    string[] firstName = adminFullName.Split(' ');
                                    MessageBox.Show("Hello, "+firstName[0] +"!", "Login Successful", MessageBoxButtons.OK, MessageBoxIcon.Information); 
                                    Close();
                                }
                                else
                                    MessageBox.Show("Wrong Input", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else
                                MessageBox.Show("Username Not Found!", "Try again");
                            connection.Close();

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error " + ex);
                            connection.Close();
                        }
                    }
                }
                else
                {
                    DialogResult res = MessageBox.Show("Please select a valid file location!", "Aborting..", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                    if (res == DialogResult.Retry)
                    {
                        setFileLocation();
                    }
                }

            }
            else
                MessageBox.Show("Invalid input");
        }




        private void loginForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Form1 f1 = (Form1)this.MdiParent;
            f1.menuStrip2.Enabled = true;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                showPass.Enabled = false;
                UsernameTextBox.Text = "Visitor";
                PasswordTextBox.Text = "Visitor";
                UsernameTextBox.Enabled = false;
                PasswordTextBox.Enabled = false;
                label2.Enabled = false;
                label3.Enabled = false;
            }
            else
            {
                UsernameTextBox.Text = "";
                PasswordTextBox.Text = "";
                UsernameTextBox.Enabled = true;
                PasswordTextBox.Enabled = true;
                label2.Enabled = true;
                label3.Enabled = true;
            }
        }

        private void loginForm_Load(object sender, EventArgs e)
        {

        }

        private void openFileDialog1_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //setFileLocation();
            GlobalClass.FileName = openFileDialog1.SafeFileName;
            GlobalClass.FileNameAndPath = openFileDialog1.FileName;
            GlobalClass.FilePath = System.IO.Path.GetDirectoryName(GlobalClass.FileNameAndPath);
        }

        private void setFileLocation()
        {
            GlobalClass.FileName = openFileDialog1.SafeFileName;
            GlobalClass.FileNameAndPath = openFileDialog1.FileName;
            GlobalClass.FilePath= System.IO.Path.GetDirectoryName(GlobalClass.FileNameAndPath);
            connection = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + GlobalClass.FileNameAndPath);
            GlobalClass.ConnectionStatus = "Successful";
            MessageBox.Show("Connection to database status: " + GlobalClass.ConnectionStatus + "!\nConnected to: " + GlobalClass.FileName + "\nLocation: " + GlobalClass.FileNameAndPath + "\nPathOnly: " + GlobalClass.FilePath, GlobalClass.ConnectionStatus, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            GlobalClass.FirstRun = false;         
            ConnectToDatabaseLabel.Text = "Connection to Database Successful";
        }

        private void showPass_CheckedChanged(object sender, EventArgs e)
        {
            if(showPass.Checked == true)
            {
                PasswordTextBox.PasswordChar = '\0';
            }
            else if(showPass.Checked == false)
            {
                PasswordTextBox.PasswordChar = '*';
            }
        }
    }
}
