using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Data.OleDb;


namespace testDiplomnaForms
{
    public partial class RFIDSimulator : Form
    {
        OleDbConnection connection = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + GlobalClass.FileNameAndPath);
        public class Users
        {
            public string CardId { get; set; }
            public string EGN { get; set; }
            public string Name { get; set; }
            public string checkin { get; set; }
            public string checkout { get; set; }
        }

        List<Users> active_users_list = new List<Users>();

        string selectedCardId = "";
        string selectedName = "";
        string selectedEgn = "";
        //bool selectedCheckBoxState;

        public RFIDSimulator()
        {
            InitializeComponent();
        }


        private void textBox1_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
        }



        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            label2.Text = "-=name=-";
            if (textBox1.TextLength == textBox1.MaxLength)
            {
                selectedCardId = textBox1.Text;
                textBox1.Text = "";
                ClearAll();

               
                try
                {
                    connection.Open();
                    OleDbCommand command = new OleDbCommand();
                    command.Connection = connection;
                    command.CommandText = "SELECT * FROM Cards WHERE CardID= '" + selectedCardId + "';";
                    OleDbDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        //record found
                        label2.Text = (reader["EGN"].ToString());
                        selectedEgn = (reader["EGN"].ToString());
                       
                        OleDbCommand command2 = new OleDbCommand();
                        command2.Connection = connection;
                        command2.CommandText = "SELECT * FROM Users WHERE EGN= '" + selectedEgn + "';";
                        OleDbDataReader reader2 = command2.ExecuteReader();
                        if (reader2.Read())
                        {
                            selectedName = (reader2["Name"].ToString());
                            reader2.Dispose();
                        }

                        if (reader["isActive"].ToString() == "True")
                        {
                            checkBox1.Checked = true; 
                            active(selectedCardId);
                        }
                        else { 
                            //Leave this like this... stupid program buggs.....                      
                            label2.Text = "Card Not Active";
                            checkBox1.Checked = false;
                        }  
                        reader.Dispose();
                    }
                    else
                    {
                        MessageBox.Show("Card Not Found");
                    }
                    connection.Close();
 
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error " + ex);
                    connection.Close();
                }
            }
        }

       

        private void button3_Click_1(object sender, EventArgs e)
        {
            connection.Close();
            this.Hide();
        }
        private void active(string CardId)
        {

            //check if user is active
            if (active_users_list.Count != 0)//if list is not empty
            {
                for (int i = 0; i < active_users_list.Count; i++)//search all  for checkout
                {
                    if (CardId == active_users_list[i].CardId)
                    {

                        DialogResult res = MessageBox.Show("User with CardID#: " + active_users_list[i].CardId + " is currently active.\nDo you wish to check out?", "Check out?", MessageBoxButtons.YesNo);
                        if (res == DialogResult.Yes)
                        {
                            active_users_list[i].checkout = DateTime.Now.ToString();

                            DateTime cin = Convert.ToDateTime(active_users_list[i].checkin);
                            DateTime cout = Convert.ToDateTime(active_users_list[i].checkout);
                            TimeSpan timeSpent = cout - cin;
                            double seconds = TimeSpan.Parse(timeSpent.ToString()).TotalSeconds;
                            MessageBox.Show("CardID#: " + active_users_list[i].CardId + "\nName: " + active_users_list[i].Name +"\nCheckIN: " + active_users_list[i].checkin + "\nCheckOUT: " + active_users_list[i].checkout + "\n\nTimeSpent: " + seconds + " seconds.");

                            try
                            {
                                
                                OleDbCommand command = new OleDbCommand();
                                command.Connection = connection;
                                command.CommandText = "INSERT INTO Checkin_log ([CardID], [Check_In], [Check_Out], [Elapsed_Time]) VALUES ('" + active_users_list[i].CardId + "','" + active_users_list[i].checkin + "','" + active_users_list[i].checkout + "','" + seconds + "')";
                                command.ExecuteNonQuery();
                                MessageBox.Show("Timer Stopped. Record Added!");
                                connection.Close();
                            }
                            catch (Exception exc)
                            {
                                MessageBox.Show("Error " + exc);
                                connection.Close();
                            }

                            listBox1.Items.Remove(active_users_list[i].CardId + " " + active_users_list[i].Name);
                            listBox2.Items.Add(active_users_list[i].CardId + " " + active_users_list[i].Name);
                            active_users_list.Remove(active_users_list[i]);

                            if (active_users_list.Count == 0)
                            {
                                MessageBox.Show("List is empty. Scan a card to start.");
                                listBox1.Enabled = false;
                                textBox1.Text = "";
                            }
                            CardId = "0";
                            ClearAll();
                            break;
                        }
                    }
                }

                for (int i = 0; i < active_users_list.Count; i++)//search all 
                {

                    if (CardId != "0")
                    {
                        if (active_users_list[i].CardId != CardId) //if not found
                        {
                            MessageBox.Show("Adding User to Active User List.", "User Not Found");
                            Users newUser = new Users()
                            {
                                CardId = CardId,
                                EGN = selectedEgn,
                                Name = selectedName,
                                checkin = DateTime.Now.ToString(),
                                checkout = ""
                            };
                            listBox1.Items.Add(newUser.CardId + " " + newUser.Name);
                            active_users_list.Add(newUser);
                            MessageBox.Show("ID#: " + newUser.CardId + "\nName: " + newUser.Name + "\nEGN: " + newUser.EGN + "\nCheckIN: " + newUser.checkin, "Added User in Active List");
                            ClearAll();
                            break;
                        }
                    }
                }

            }
            else//if list is empty
            {
                MessageBox.Show("List Empty. Adding User to Active User List.", "List Empty");
                Users newUser = new Users()
                {
                    CardId = CardId,
                    EGN=selectedEgn,
                    Name = selectedName,
                    checkin = DateTime.Now.ToString(),
                    checkout = ""
                };
                listBox1.Items.Add(newUser.CardId +" " + newUser.Name);
                active_users_list.Add(newUser);
                MessageBox.Show("ID#: " + newUser.CardId + "\nName: " + newUser.Name + "\nEGN: " + newUser.EGN + "\nCheckIN: " + newUser.checkin, "Added User in Active List");
                ClearAll();
            }

        }
        private void ClearAll()
        {
            label2.Text = "-=name=-";
            checkBox1.Checked = false;
        }
    }

}
