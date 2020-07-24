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
using System.Data.Sql;
namespace testDiplomnaForms
{

    public partial class Cards_Users : Form
    {
        OleDbConnection connection = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + GlobalClass.FileNameAndPath);
        List<string> Usage = new List<string>(); 
        string selectedCardId = "";
        string selectedEgn = "";
        string selectedName = "";
        string validDate = "";
        bool selectedStatus;
        string temp = "";
              
        
        public Cards_Users()
        {
            InitializeComponent();
            ComboBoxUpdate();
            ValidDateDateTimePicker.Enabled = false;

        }

        private void Cards_Users_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            connection.Close();
            this.Hide();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            clearAll();
            if (textBox1.TextLength == textBox1.MaxLength)
            {
                actionTime();
            }
        }

        private void ActiveRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (ActiveRadioButton.Checked == true)
            {
                InActiveRadioButton.Checked = false;
                selectedStatus = true;
            }
        }

        private void InActiveRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (InActiveRadioButton.Checked == true)
            {
                ActiveRadioButton.Checked = false;
                selectedStatus = false;
            }
        }

        private void clearAll()
        {
            textBox2.Text = "";
            textBox3.Text = "";
            comboBox1.Text = "";
            comboBox2.Text = "";
            comboBox2.SelectedItem = null;
            ActiveRadioButton.Checked = false;
            InActiveRadioButton.Checked = false;
            selectedEgn = "";
            selectedName = "";
            LastUsedTextBox.Text = "";
            ValidDateDateTimePicker.ResetText();
            temp = "";
            ValidDateDateTimePicker.Enabled = false;
            AssingCheckBox.Checked = false;
            ChangeActiveDateCheckBox.Checked = false;
        }

        private void AssingCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (AssingCheckBox.Checked == true)
            {
                comboBox2.Enabled = true;
                textBox3.Text = "select a new user from below";
            }
            else
            {
                comboBox2.Enabled = false;
                textBox3.Text = temp;
                comboBox2.Text = "";
            }
        }
        private void ComboBoxUpdate()
        {

            //Koda dolu zarejda sudurjanieto na combobox-a, kato zarejda bazata danni s posledno obnovena informaciq

            comboBox1.Items.Clear();
            comboBox2.Items.Clear();
            comboBox2.Items.Add("-=Free Card From User=-");
            try
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                string querry = "SELECT * FROM Users";
                string querry2 = "SELECT * FROM Cards";
                command.CommandText = querry;
                OleDbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    comboBox2.Items.Add(reader[5].ToString() + " " + reader[0].ToString());
                }
                reader.Dispose();
                command.CommandText = querry2;
                OleDbDataReader reader2 = command.ExecuteReader();
                while (reader2.Read())
                {
                    comboBox1.Items.Add(reader2[0].ToString());
                }
                reader2.Dispose();
                connection.Close();
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error " + exc);
                connection.Close();
            }
        }

        private void actionTime()
        {
            if (textBox1.Text != "")
                selectedCardId = textBox1.Text;
            else
                selectedCardId = comboBox1.SelectedItem.ToString();
            textBox1.Text = "";
            textBox2.Text = selectedCardId;

            try
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                command.CommandText = "SELECT * FROM Cards WHERE CardID= '" + selectedCardId + "';";
                OleDbDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    selectedEgn = (reader["EGN"].ToString());

                    if (reader["ActiveDate"].ToString() != "")
                    {
                        validDate = DateTime.Parse(reader["ActiveDate"].ToString()).ToShortDateString();
                        ValidDateDateTimePicker.Text = validDate;
                    }
                   


                    //record found
                    if (reader["isActive"].ToString() == "True")
                    {
                        ActiveRadioButton.Checked = true;
                        reader.Dispose();
                    }
                    else
                    {
                        InActiveRadioButton.Checked = true;
                    }
                   

                    OleDbCommand command2 = new OleDbCommand();
                    command2.Connection = connection;
                    command2.CommandText = "SELECT * FROM Users WHERE EGN= '" + selectedEgn + "';";
                    OleDbDataReader reader2 = command2.ExecuteReader();
                    if (reader2.Read())
                    {
                        selectedName = (reader2["Name"].ToString());
                        textBox3.Text = selectedEgn + " " + selectedName;
                        temp = textBox3.Text;
                        reader2.Dispose();
                    }
                    //if (ActiveRadioButton.Checked == true)
                   // {
                        //LAST USED
                        
                        OleDbCommand command3 = new OleDbCommand();
                        command3.Connection = connection;
                        command3.CommandText = "SELECT * FROM Checkin_log WHERE CardID = '" + selectedCardId + "';";
                        OleDbDataReader reader3 = command3.ExecuteReader();
                        while (reader3.Read())
                        {
                            Usage.Add(reader3["Check_Out"].ToString());
                        }
                        reader3.Dispose();
                          
                        if(Usage.Count != 0) // not zero
                        {
                            
                            LastUsedTextBox.Text = Usage[Usage.Count - 1];
                        }
                        else if (Usage.Count == 0) //zero
                            {
                                LastUsedTextBox.Text = "Not Used";
                            }
                        else if (Usage.Count == 1) //has 2 uses
                        {
                            LastUsedTextBox.Text = Usage[Usage.Count];
                        }

                            
                        Usage.Clear();
                 //   }

                }
                else
                    textBox2.Text = "Card Not Found In Records";
                reader.Dispose();
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex);
                connection.Close();
            }
        }

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            clearAll();
            actionTime();
        }

        private void comboBox1_Click(object sender, EventArgs e)
        {
            clearAll();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (AssingCheckBox.Checked == true)
            {

                if (comboBox2.SelectedItem.ToString() != "-=Free Card From User=-")
                {
                    string temp = comboBox2.SelectedItem.ToString();
                    string[] words = temp.Split(' ');
                    selectedEgn = words[0];
                    try
                    {
                        if (words[2] != null)
                            selectedName = words[1] + " " + words[2];
                    }
                    catch (IndexOutOfRangeException)
                    {
                        selectedName = words[1];
                    }
                }
                else
                {
                    selectedName = null;
                    selectedEgn = null;
                }
            }

            DialogResult res = MessageBox.Show("Card ID: " + selectedCardId + "\n-=Card Status Update=-\nName: " + selectedName + "\nEGN: " + selectedEgn + "\nCard Active Status: " + selectedStatus + ".", "Confirm Changes", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res == DialogResult.Yes)
            {
                try
                {
                    connection.Open();
                    OleDbCommand command = new OleDbCommand();
                    command.Connection = connection;
                    if (ChangeActiveDateCheckBox.Checked == true)
                    {
                        command.CommandText = @"UPDATE Cards SET isActive = " + selectedStatus.ToString() + ", [EGN] = '" + selectedEgn + "', [ActiveDate] = '" + validDate + "' WHERE [CardID] = '" + selectedCardId + "';";
                    }
                    else
                        command.CommandText = @"UPDATE Cards SET isActive = " + selectedStatus.ToString() + ", [EGN] = '" + selectedEgn + "' WHERE [CardID] = '" + selectedCardId + "';";
                    
                    
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
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                command.CommandText = @"INSERT INTO Cards ([CardID]) VALUES ('" + selectedCardId + "');";
                
                command.ExecuteNonQuery();
                MessageBox.Show("Card Successfully Added.");
                connection.Close();
                textBox2.Text = selectedCardId;
                InActiveRadioButton.Checked = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex);
                connection.Close();
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text == "Card Not Found In Records")
            {
                registerCardButton.Visible = true;
                DeleteCardButton.Visible = false;
            }
            else if (textBox2.Text == "")
            {
                registerCardButton.Visible = false;
                DeleteCardButton.Visible = false;
            }
            else
            {
                registerCardButton.Visible = false;
                DeleteCardButton.Visible = true;
                
            }
        }

        private void DeleteCardButton_Click(object sender, EventArgs e)
        {
            DialogResult r = MessageBox.Show("Do you want to delete this record?", "Confirm", MessageBoxButtons.YesNo);
            if (r == DialogResult.Yes)
            {
                try
                {
                    connection.Open();
                    OleDbCommand command = new OleDbCommand();
                    command.Connection = connection;
                    //command.CommandText = "DELETE FROM Cards WHERE [CardID] = '" + selectedCardId + "'";
                    MessageBox.Show(command.CommandText);
                    command.ExecuteNonQuery();
                    MessageBox.Show("Card Successfully Deleted.");
                    textBox2.Text = "";
                    connection.Close();
                    clearAll();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex);
                    connection.Close();
                }
            }
        }

        private void ValidDateDateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            validDate = ValidDateDateTimePicker.Value.ToShortDateString();
        }

        private void ChangeActiveDateCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if(ChangeActiveDateCheckBox.Checked == true)
            {
                //checked 
                ValidDateDateTimePicker.Enabled = true;
            }
            else if(ChangeActiveDateCheckBox.Checked==false)
            {
                //unchecked 
                ValidDateDateTimePicker.Enabled = false;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
