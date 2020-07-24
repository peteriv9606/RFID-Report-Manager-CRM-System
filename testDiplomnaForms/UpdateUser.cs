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
    public partial class UpdateUser : Form
    {
        OleDbConnection connection = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + GlobalClass.FileNameAndPath);
        string selectedSex = "";
        string selectedEgn = "";
        public UpdateUser()
        {
            InitializeComponent();
            ComboBoxUpdate();
            panel3.Enabled = false;
            UpdateUserButton.Enabled = false;
        }

        private void UpdateUserCloseButton_Click(object sender, EventArgs e)
        {
            connection.Close();
            this.Hide();
        }

        private void UpdateUserButton_Click(object sender, EventArgs e)
        {
            if (UpdMaleRadioButton.Checked == true)
                selectedSex = "Male";
            else if (UpdFemaleRadioButton.Checked == true)
                selectedSex = "Female";
            else if (UpdOtherRadioButton.Checked == true)
                selectedSex = "Other";

            try
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                command.CommandText = @"UPDATE Users 
                                                             SET [Name] = '" + UpdFullNameTextBox.Text +
                                                             "', [EGN] = '" + UpdEGNTextBox.Text +
                                                             "', [Address] = '" + UpdAddressTextBox.Text +
                                                             "', [E-Mail] = '" + UpdEmailTextBox.Text +
                                                             "', [Sex] = '" + selectedSex +
                                                             "', isAdmin = " + UpdIsAdminCheckBox.Checked.ToString() +
                                                             ", [Username] = '" + UpdUsernameTextBox.Text +
                                                             "', [Password] = '" + UpdPasswordTextBox.Text +
                                                             "', isActive = " + UpdIsActiveCheckBox.Checked.ToString() +
                                                             ", [Department] = '" + UpdDepartmentTextBox.Text +
                                                             "', [Position] = '" + UpdPositionTextBox.Text +
                                                             "', Hourly_Payment = '" + UpdHourlyPaymentTextBox.Text +
                                                             "', Sign_Date = '" + UpdSignDatePicker.Value.ToShortDateString() +
                                                             "' WHERE [EGN] = '" + selectedEgn + "';";
                command.ExecuteNonQuery();
                MessageBox.Show("Data Edit Successful!", "Success", MessageBoxButtons.OK);
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex);
                connection.Close();
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
            panel3.Enabled = false;
            UpdateUserButton.Enabled = false;
            clearall();
        }

        private void button1_Click(object sender, EventArgs e)
        {


            //selects ID from combobox by spliting the string
            if (comboBox1.SelectedItem != null)
            {

                panel3.Enabled = true;
                UpdateUserButton.Enabled = true;
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
                        UpdFullNameTextBox.Text = (reader["Name"].ToString());
                        UpdEGNTextBox.Text = (reader["EGN"].ToString());
                        UpdAddressTextBox.Text = (reader["Address"].ToString());
                        UpdEmailTextBox.Text = (reader["E-Mail"].ToString());
                        if (reader["Sex"].ToString() == "Male")
                        {
                            UpdMaleRadioButton.Checked = true;
                        }
                        else if (reader["Sex"].ToString() == "Female")
                        {
                            UpdFemaleRadioButton.Checked = true;
                        }
                        else if (reader["Sex"].ToString() == "Other")
                        {
                            UpdOtherRadioButton.Checked = true;
                        }

                        if (reader["isAdmin"].ToString() == "True")
                        {
                            UpdIsAdminCheckBox.Checked = true;
                            UpdUsernameTextBox.Text = (reader["Username"].ToString());
                            UpdPasswordTextBox.Text = (reader["Password"].ToString());
                        }
                        else
                        {
                            UpdIsAdminCheckBox.Checked = false;
                            UpdUsernameTextBox.Enabled = false;
                            UpdPasswordTextBox.Enabled = false;
                        }

                        if (reader["isActive"].ToString() == "True")
                        {
                            UpdIsActiveCheckBox.Checked = true;
                        }
                        else
                        {
                            UpdIsActiveCheckBox.Checked = false;
                        }

                        UpdSignDatePicker.Text = (reader["Sign_Date"].ToString());
                        UpdHourlyPaymentTextBox.Text = (reader["Hourly_Payment"].ToString());
                        UpdDepartmentTextBox.Text = (reader["Department"].ToString());
                        UpdPositionTextBox.Text = (reader["Position"].ToString());
                        reader.Dispose();
                        UpdateUserButton.Enabled = true;
                    }
                    connection.Close();
                }
                catch (Exception exc)
                {
                    MessageBox.Show("Error " + exc);
                    UpdateUserButton.Enabled = false;
                }
            }

            else
            {
                MessageBox.Show("Please Select A Valid Input.");
                UpdateUserButton.Enabled = false;
                clearall();
            }
        }

        private void UpdIsAdminCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (UpdIsAdminCheckBox.Checked == true)
            {
                UpdUsernameTextBox.Enabled = true;
                UpdPasswordTextBox.Enabled = true;
            }
            else
            {
                UpdUsernameTextBox.Enabled = false;
                UpdPasswordTextBox.Enabled = false;
            }
        }
        private void clearall()
        {
            comboBox1.Text = "";
            comboBox1.SelectedItem = null;
            UpdFullNameTextBox.Text = "";
            UpdEGNTextBox.Text = "";
            UpdAddressTextBox.Text = "";
            UpdEmailTextBox.Text = "";
            UpdMaleRadioButton.Checked = false;
            UpdFemaleRadioButton.Checked = false;
            UpdOtherRadioButton.Checked = false;
            UpdIsAdminCheckBox.Checked = false;
            UpdUsernameTextBox.Text = "";
            UpdPasswordTextBox.Text = "";
            UpdUsernameTextBox.Enabled = false;
            UpdPasswordTextBox.Enabled = false;
            UpdIsActiveCheckBox.Checked = false;
            UpdSignDatePicker.Text = DateTime.Today.ToShortDateString();
            UpdHourlyPaymentTextBox.Text = "";
            UpdDepartmentTextBox.Text = "";
            UpdPositionTextBox.Text = "";
        }

        private void UpdMaleRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (UpdMaleRadioButton.Checked == true)
            {
                UpdFemaleRadioButton.Checked = false;
                UpdOtherRadioButton.Checked = false;
            }
        }

        private void UpdFemaleRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (UpdFemaleRadioButton.Checked == true)
            {
                UpdMaleRadioButton.Checked = false;
                UpdOtherRadioButton.Checked = false;
            }
        }

        private void UpdOtherRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (UpdOtherRadioButton.Checked == true)
            {
                UpdMaleRadioButton.Checked = false;
                UpdFemaleRadioButton.Checked = false;
            }
        }

        private void UpdShowPasswordCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if(UpdShowPasswordCheckBox.Checked == true)
            {
                UpdPasswordTextBox.PasswordChar = '\0';
            }
            else
            {
                UpdPasswordTextBox.PasswordChar = '*';
            }
        }
    }
}
