using System;
using System.Data.OleDb;
using System.Windows.Forms;

namespace testDiplomnaForms
{
    public partial class DeleteUser : Form
    {
        OleDbConnection connection = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + GlobalClass.FileNameAndPath);
        string selectedEgn = "";

        public DeleteUser()
        {
            InitializeComponent();
            ComboBoxUpdate();
            DelUserButton.Enabled = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            connection.Close();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null)
            {
                DelUserButton.Enabled = true;
                string temp = comboBox1.SelectedItem.ToString();
                string[] words = temp.Split(' ');
                selectedEgn = words[0];
                clearall();
                //fill textboxes
                try
                {
                    connection.Open();
                    OleDbCommand command = new OleDbCommand();
                    command.Connection = connection;
                    command.CommandText = "SELECT * FROM Users WHERE EGN = '" + selectedEgn + "';";
                    OleDbDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        //record found
                        DelFullNameLabel.Text = (reader["Name"].ToString());
                        DelEGNLabel.Text = (reader["EGN"].ToString());
                        DelAddressLabel.Text = (reader["Address"].ToString());
                        DelEmailLabel.Text = (reader["E-Mail"].ToString());

                        if (reader["isAdmin"].ToString() == "True")
                        {
                            DelIsAdminCheckBox.Checked = true;
                            DelUsernameLabel.Text = (reader["Username"].ToString());
                            DelUsernameLabel.Enabled = true;
                        }
                        else
                        {
                            DelIsAdminCheckBox.Checked = false;
                            DelUsernameLabel.Enabled = false;
                        }
                        reader.Dispose();
                        DelUserButton.Enabled = true;
                    }
                    connection.Close();
                }
                catch (Exception exc)
                {
                    connection.Close();
                    MessageBox.Show("Error " + exc);
                    DelUserButton.Enabled = false;
                }
            }

            else
            {
                MessageBox.Show("Please Select A Valid Input.");
                DelUserButton.Enabled = false;
                clearall();
            }
        }
        private void ComboBoxUpdate()
        {

            //Koda dolu zarejda sudurjanieto na combobox-a, kato zarejda bazata danni s posledno obnovena informaciq

            comboBox1.Items.Clear();
            try
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                string querry = "SELECT * FROM Users";
                command.CommandText = querry;
                OleDbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    comboBox1.Items.Add(reader[5].ToString() + " " + reader[0].ToString());
                }
                connection.Close();
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error " + exc);
                connection.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            comboBox1.Text = "";
            comboBox1.SelectedItem = null;
            DelUserButton.Enabled = false;
            clearall();
        }
        private void clearall()
        {
            comboBox1.Text = "";
            comboBox1.SelectedItem = null;
            DelUsernameLabel.Text = "----";
            DelIsAdminCheckBox.Checked = false;
            DelFullNameLabel.Text = "----";
            DelAddressLabel.Text = "----";
            DelEmailLabel.Text = "----";
            DelEGNLabel.Text = "----";
        }

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
           
            
        }

        private void deleteUser()
        {
            try
            {
                connection.Open();
                int count = 0;
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                command.CommandText = "SELECT * FROM Users WHERE [EGN] = '" + selectedEgn + "'";
                OleDbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                    count++;
                reader.Dispose();

                if (count == 1)
                {
                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        if (reader["isLocked"].ToString() == "False")
                        {
                            DialogResult res = MessageBox.Show("Are you sure you want to delete the record?", "Confirm", MessageBoxButtons.YesNo);
                            if (res == DialogResult.Yes)
                            {
                                reader.Dispose();
                                command.CommandText = "DELETE FROM Users WHERE [EGN] = '" + selectedEgn + "'";
                                command.ExecuteNonQuery();
                                command.CommandText = "DELETE FROM Cards WHERE [EGN] = '" + selectedEgn + "'";
                                command.ExecuteNonQuery();
                                MessageBox.Show("Record Successfuly Deleted!");
                                clearall();
                                connection.Close();
                                ComboBoxUpdate();
                            }
                        }
                        else
                        {
                            reader.Dispose();
                            MessageBox.Show("Cannot delete master administrator!", "Nice try", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            clearall();
                            connection.Close();
                            ComboBoxUpdate();
                        }
                        break;
                    }
                }
                else if (count > 1)
                {
                    DialogResult resDup = MessageBox.Show("Duplicate Records Found. \nAre you sure you want to delete the selected record?", "Duplicates found", MessageBoxButtons.YesNo);
                    if (resDup == DialogResult.Yes)
                    {
                        reader.Dispose();
                        command.CommandText = "DELETE FROM Users WHERE [EGN] = '" + selectedEgn + "'";
                        command.ExecuteNonQuery();
                        command.CommandText = "DELETE FROM Cards WHERE [EGN] = '" + selectedEgn + "'";
                        command.ExecuteNonQuery();
                        MessageBox.Show("Record Successfuly Deleted!");
                        clearall();
                        connection.Close();
                        ComboBoxUpdate();
                    }
                    else
                        MessageBox.Show("Record NOT deleted.", "Canceled");
                }
                else
                    MessageBox.Show("Record not found. \nNothing to delete.", "Error");
                connection.Close();
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error " + exc);
                connection.Close();
            }
        }

        private void DelUserButton_Click(object sender, EventArgs e)
        {
            if (DelUsernameLabel.Text == GlobalClass.CurrentUser)
            {
                MessageBox.Show("Cannot delete self.", "Dont..");
                DelUserButton.Enabled = false;
            }
            else
                deleteUser();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void comboBox1_Click(object sender, EventArgs e)
        {
            DelUsernameLabel.Text = "----";
            DelIsAdminCheckBox.Checked = false;
            DelFullNameLabel.Text = "----";
            DelAddressLabel.Text = "----";
            DelEmailLabel.Text = "----";
            DelEGNLabel.Text = "----";
            DelUserButton.Enabled = false;
        }
    }
}
