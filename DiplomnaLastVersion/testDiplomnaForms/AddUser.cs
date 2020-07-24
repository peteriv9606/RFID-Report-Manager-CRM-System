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
    public partial class AddUser : Form
    {
        OleDbConnection connection = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + GlobalClass.FileNameAndPath);
        string selectedSex = "";
        bool existing = false;
        public AddUser()
        {
            InitializeComponent();
            label9.Enabled = false;
            label10.Enabled = false;
            AddUsernameTextBox.Enabled = false;
            AddPasswordTextBox.Enabled = false;
            AddSignDatePicker.Text = DateTime.Now.ToShortDateString();
        }

        private void AddUsrAddUserButton_Click(object sender, EventArgs e)
        {
            foreach (Control c in tableLayoutPanel1.Controls)
            {
                if (c is TextBox)
                {
                    TextBox textBox = c as TextBox;
                    if (textBox.ForeColor == Color.Red)
                    {
                        existing = true;
                    }
                }
            }
            if (AddMaleRadioButton.Checked == false && AddFemaleRadioButton.Checked == false && AddOtherRadioButton.Checked == false)
            {
                MessageBox.Show("Please select gender.", "You missed something", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            if (AddFullNameTextBox.Text == "" ||
                     AddEGNTextBox.Text == "" ||
                     AddAddressTextBox.Text == "" ||
                     AddEmailTextBox.Text == "" ||
                     AddHourlyPaymentTextBox.Text == "" ||
                     AddDepartmentTextBox.Text == "" ||
                     AddPositionTextBox.Text == "")
            {
                MessageBox.Show("Please fill all fields.", "You missed something", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            if (AddIsAdminCheckBox.Checked == true)
            {
                if (AddUsernameTextBox.Text == "" || AddPasswordTextBox.Text == "")
                    MessageBox.Show("Please fill out Username and/or Password section,\nor remove administrator rights.", "Error in admin section", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }


            if (!existing)
            {
                try
                {
                    connection.Open();
                    OleDbCommand command = new OleDbCommand();
                    command.Connection = connection;
                    command.CommandText = @"INSERT INTO Users ([Name], [Username], [Password], isAdmin, [Sex], [EGN], [E-Mail], [Address], [Department], [Position], Sign_Date, Hourly_Payment, isActive) 
                VALUES ('" + AddFullNameTextBox.Text + "', '" + AddUsernameTextBox.Text + "', '" + AddPasswordTextBox.Text + "', " + AddIsAdminCheckBox.Checked.ToString() + ", '" + selectedSex + "', '" + AddEGNTextBox.Text + "', '" + AddEmailTextBox.Text + "', '" + AddAddressTextBox.Text + "', '" + AddDepartmentTextBox.Text + "', '" + AddPositionTextBox.Text + "', '" + AddSignDatePicker.Value.ToShortDateString() + "', '" + AddHourlyPaymentTextBox.Text + "', " + AddIsActiveCheckBox.Checked.ToString() + ");";
                    command.ExecuteNonQuery();
                    MessageBox.Show("Name: " + AddFullNameTextBox.Text, "Record Created!");
                    connection.Close();
                    foreach (Control c in tableLayoutPanel1.Controls)
                    {
                        if (c is TextBox)
                        {
                            TextBox textBox = c as TextBox;
                            if (textBox.Text != "")
                            {
                                textBox.Text = "";
                            }
                        }
                        else if (c is CheckBox)
                        {
                            CheckBox checkBox = c as CheckBox;
                            if (checkBox.Checked == true)
                            {
                                checkBox.Checked = false;
                            }
                        }
                        else if (c is DateTimePicker)
                        {
                            DateTimePicker dateTimePicker = c as DateTimePicker;
                            if (dateTimePicker.Text.ToString() != DateTime.Now.ToShortDateString())
                            {
                                dateTimePicker.Text = DateTime.Now.ToShortDateString();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error " + ex);
                    connection.Close();
                }
            }
            else MessageBox.Show("Check fields for duplicate data", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void AddUsrCloseButton_click(object sender, EventArgs e)
        {
            connection.Close();
            this.Close();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (AddIsAdminCheckBox.Checked == true)
            {
                label9.Enabled = true;
                label10.Enabled = true;
                AddUsernameTextBox.Enabled = true;
                AddPasswordTextBox.Enabled = true;
            }
            else
            {
                label9.Enabled = false;
                label10.Enabled = false;
                AddUsernameTextBox.Text = "";
                AddUsernameTextBox.Enabled = false;
                AddPasswordTextBox.Text = "";
                AddPasswordTextBox.Enabled = false;
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            AddFemaleRadioButton.Checked = false;
            AddOtherRadioButton.Checked = false;
            selectedSex = "Male";
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            AddMaleRadioButton.Checked = false;
            AddOtherRadioButton.Checked = false;
            selectedSex = "Female";
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            AddFemaleRadioButton.Checked = false;
            AddMaleRadioButton.Checked = false;
            selectedSex = "Other";
        }

        private void AddEGNTextBox_TextChanged(object sender, EventArgs e)
        {
            //check if EGN is taken or not

            //search if user exists
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
                    if (reader[5].ToString() == AddEGNTextBox.Text)
                    {
                        AddEGNTextBox.ForeColor = Color.Red;
                        MessageBox.Show("User with this data already exists.", "Error");
                        existing = true;
                        break;
                    }
                    else
                    {
                        AddEGNTextBox.ForeColor = Color.Green;
                        existing = false;
                    }
                }
                connection.Close();
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error " + exc);
                connection.Close();
            }
        }

        private void AddEmailTextBox_TextChanged(object sender, EventArgs e)
        {
            //check if EMAIl is taken or not

            //search if user exists
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
                    if (reader[6].ToString() == AddEmailTextBox.Text) // CHANGE READER TO EMAIL
                    {
                        AddEmailTextBox.ForeColor = Color.Red;
                        MessageBox.Show("User with this data already exists.", "Error");
                        existing = true;
                        break;
                    }
                    else
                    {
                        AddEmailTextBox.ForeColor = Color.Green;
                        existing = false;
                    }
                }
                connection.Close();
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error " + exc);
                connection.Close();
            }
        }

        private void AddUsernameTextBox_TextChanged(object sender, EventArgs e)
        {
            //check if Username is taken or not

            //search if user exists
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
                    if (reader[1].ToString() == AddUsernameTextBox.Text) // CHANGE READER TO Username
                    {
                        AddUsernameTextBox.ForeColor = Color.Red;
                        MessageBox.Show("User with this data already exists.", "Error");
                        existing = true;
                        break;
                    }
                    else
                    {
                        AddUsernameTextBox.ForeColor = Color.Green;
                        existing = false;
                    }
                }
                connection.Close();
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error " + exc);
                connection.Close();
            }
        }

        private void AddSignDatePicker_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
