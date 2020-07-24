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
using System.IO;
using iTextSharp.text.pdf;
using iTextSharp.text;
using Microsoft.Office;

namespace testDiplomnaForms
{
    public partial class Queries : Form
    {
        OleDbConnection connection = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + GlobalClass.FileNameAndPath);
        public Queries()
        {
            
            InitializeComponent();
            ComboBoxUpdate();
            disablePanels();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            connection.Close();
            this.Hide();
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            refreshDataGridView();
            if (forComboBox.SelectedItem.ToString() == "Users")
            {
                byComboBox.Enabled = true;
                byComboBox.Items.Clear();
                byComboBox.Items.Add("EGN");
                byComboBox.Items.Add("Sign Date");
                byComboBox.Items.Add("Sex");
                byComboBox.Items.Add("Admin Status");
                byComboBox.Items.Add("Department");
                byComboBox.Items.Add("Position");
                byComboBox.Items.Add("Hourly Payment");
                byComboBox.Items.Add("Active Status");
            }
            else if (forComboBox.SelectedItem.ToString() == "Cards")
            {
                byComboBox.Enabled = true;
                byComboBox.Items.Clear();
                byComboBox.Items.Add("EGN");
                byComboBox.Items.Add("Active Status");
                byComboBox.Items.Add("Active Date");
            }
            else if (forComboBox.SelectedItem.ToString() == "Logs")
            {
                byComboBox.Enabled = true;
                byComboBox.Items.Clear();
                byComboBox.Items.Add("Date");
                byComboBox.Items.Add("CardID");
            }

        }
        private void ComboBoxUpdate()
        {
            byComboBox.Enabled = false;
            forComboBox.Items.Clear();
            forComboBox.Items.Add("Users");
            forComboBox.Items.Add("Cards");
            forComboBox.Items.Add("Logs");
        }
        private void ByComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            EnablePanelFunction(forComboBox.SelectedItem.ToString(), byComboBox.SelectedItem.ToString());
        }
        private void disablePanels()
        {
            
            ExportToPDFBtn.Enabled = false;
            OneTableLayoutPanel.Enabled = false;
            OneFromCheckBox.Checked = false;
            OneToCheckBox.Checked = false;
            OneFromTextBox.Text = "";
            OneToTextBox.Text = "";
            TwoTableLayoutPanel.Enabled = false;
            TwoFemaleCheckBox.Checked = false;
            TwoMaleCheckBox.Checked = false;
            ThreeTableLayoutPanel.Enabled = false;
            ThreeActiveCheckBox.Checked = false;
            ThreeInactiveCheckBox.Checked = false;
            ThreeFreeCheckBox.Checked = false;
            FourTableLayoutPanel.Enabled = false;
            FourComboBox.Items.Clear();
            FourComboBox.Text = "";
            FiveTableLayoutPanel.Enabled = false;
            FiveFromCheckBox.Checked = false;
            FiveToCheckBox.Checked = false;
            FiveFromDateTimePicker.Text = DateTime.Now.ToShortDateString();
            FiveToDateTimePicker.Text = DateTime.Now.ToShortDateString();
        }
        private void ByComboBox_Click(object sender, EventArgs e)
        {
            disablePanels();
            refreshDataGridView();
        }
        private void SearchForComboBox_Click(object sender, EventArgs e)
        {
            byComboBox.Enabled = false;
            ThreeFreeCheckBox.Enabled = true;
            byComboBox.Text = "";
            disablePanels();
        }
        private void EnablePanelFunction(string SearchForComboBox, string SearchByComboBox)
        {
            if (SearchForComboBox == "Users")
            {
                if (SearchByComboBox == "EGN")
                {
                    FourTableLayoutPanel.Enabled = true;

                    string query = @"   SELECT        Users.*, EGN AS Expr1
                                        FROM            Users
                                        WHERE        (NOT (EGN IS NULL))";
                    FourthComboBoxUpdate(query, 5);
                }
                else if (SearchByComboBox == "Sign Date")
                {
                    FiveTableLayoutPanel.Enabled = true;
                }
                else if (SearchByComboBox == "Sex")
                {
                    TwoTableLayoutPanel.Enabled = true;
                }
                else if (SearchByComboBox == "Admin Status")
                {
                    ThreeTableLayoutPanel.Enabled = true;
                    ThreeFreeCheckBox.Enabled = false;
                }
                else if (SearchByComboBox == "Department")
                {
                    FourTableLayoutPanel.Enabled = true;
                    string query = @"   SELECT        Users.*, Department AS Expr1
                                        FROM            Users
                                        WHERE        (NOT (Department IS NULL))";
                    FourthComboBoxUpdate(query, 8);
                }
                else if (SearchByComboBox == "Position")
                {
                    FourTableLayoutPanel.Enabled = true;

                    string query = @"   SELECT        Users.*, [Position] AS Expr1
                                        FROM            Users
                                        WHERE        (NOT ([Position] IS NULL))";
                    FourthComboBoxUpdate(query, 9);
                }
                else if (SearchByComboBox == "Hourly Payment")
                {
                    OneTableLayoutPanel.Enabled = true;
                }
                else if (SearchByComboBox == "Active Status")
                {
                    ThreeTableLayoutPanel.Enabled = true;
                    ThreeFreeCheckBox.Enabled = false;
                }
            }
            else if (SearchForComboBox == "Cards")
            {
                if (SearchByComboBox == "EGN")
                {
                    FourTableLayoutPanel.Enabled = true;

                    string query = @"   SELECT        Cards.*, EGN AS Expr1
                                        FROM            Cards
                                        WHERE        (NOT (CardID IS NULL))";
                    FourthComboBoxUpdate(query, 1);
                }
                else if (SearchByComboBox == "Active Status")
                {
                    ThreeTableLayoutPanel.Enabled = true;
                    ThreeFreeCheckBox.Enabled = true;
                }
                else if (SearchByComboBox == "Active Date")
                {
                    FiveTableLayoutPanel.Enabled = true;
                }
            }
            else if (SearchForComboBox == "Logs")
            {
                if (SearchByComboBox == "Date")
                {
                    FiveTableLayoutPanel.Enabled = true;
                }
                else if (SearchByComboBox == "CardID")
                {
                    FourTableLayoutPanel.Enabled = true;
                    string query = @"   SELECT        Checkin_log.*, CardID AS Expr1
                                        FROM            Checkin_log
                                        WHERE        (NOT (CardID IS NULL))";
                    FourthComboBoxUpdate(query, 0);
                    //fill combobox with CardIDs!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                }
            }
        }
        private void FourthComboBoxUpdate(string querry, int cell)
        {
            //Koda dolu zarejda sudurjanieto na combobox-a, kato zarejda bazata danni s posledno obnovena informaciq
            FourComboBox.Items.Clear();
            try
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                List<string> items = new List<string>();
                command.CommandText = querry;
                OleDbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    items.Add(reader[cell].ToString());
                }
                connection.Close();

                List<string> unique = items.Distinct().ToList();
                unique.Remove("");
                unique.Sort();
                for (int i = 0; i < unique.Count; i++)
                {
                    FourComboBox.Items.Add(unique[i].ToString());
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error " + exc);
                connection.Close();
            }

        }
        private void ThreeFreeCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (ThreeFreeCheckBox.Checked == true)
            {
                ThreeActiveCheckBox.Checked = false;
                ThreeInactiveCheckBox.Checked = false;
                ThreeInactiveCheckBox.Enabled = false;
                ThreeActiveCheckBox.Enabled = false;
            }
            else if (ThreeFreeCheckBox.Checked == false)
            {
                ThreeInactiveCheckBox.Enabled = true;
                ThreeActiveCheckBox.Enabled = true;
            }
        }
        private void FiveFromCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (FiveFromCheckBox.Checked == true)
            {
                FiveFromDateTimePicker.Enabled = true;
            }
            if (FiveFromCheckBox.Checked == false)
            {
                FiveFromDateTimePicker.Enabled = false;
            }
        }
        private void FiveToCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (FiveToCheckBox.Checked == true)
            {
                FiveToDateTimePicker.Enabled = true;
            }
            if (FiveToCheckBox.Checked == false)
            {
                FiveToDateTimePicker.Enabled = false;
            }
        }
        private void OneFromCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (OneFromCheckBox.Checked == true)
            {
                OneFromTextBox.Enabled = true;
            }
            else if (OneFromCheckBox.Checked == false)
            {
                OneFromTextBox.Enabled = false;
            }
        }
        private void OneToCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (OneToCheckBox.Checked == true)
            {
                OneToTextBox.Enabled = true;
            }
            else if (OneToCheckBox.Checked == false)
            {
                OneToTextBox.Enabled = false;
            }
        }
        private void OneGoButton_Click(object sender, EventArgs e)
        {
            
            ExportToPDFBtn.Enabled = true;
            refreshDataGridView();
            string query = "";
            if (OneFromCheckBox.Checked == false && OneToCheckBox.Checked == false)
            {
                MessageBox.Show("Please make a valid selection.", "Error");
            }
            {
                if (OneFromCheckBox.Checked == true && OneToCheckBox.Checked == false) //from
                {
                    query = @"  SELECT        Users.*, Hourly_Payment AS Expr1
                            FROM            Users
                            WHERE        (Hourly_Payment > " + OneFromTextBox.Text + ")";
                }
                if (OneToCheckBox.Checked == true && OneFromCheckBox.Checked == false) //to
                {
                    query = @"  SELECT        Users.*, Hourly_Payment AS Expr1
                            FROM            Users
                            WHERE        (Hourly_Payment < " + OneToTextBox.Text + ")";
                }
                if (OneFromCheckBox.Checked == true && OneToCheckBox.Checked == true) //both
                {
                    query = @"  SELECT        Users.*
                            FROM            Users
                            WHERE        (Hourly_Payment BETWEEN " + OneFromTextBox.Text + " AND " + OneToTextBox.Text + ")";
                }
                try
                {
                    connection.Open();
                    OleDbCommand command = new OleDbCommand();
                    command.Connection = connection;
                    command.CommandText = query;
                    OleDbDataReader reader = command.ExecuteReader();
                    //Create a New DataTable to store the Data
                    DataTable Hourly = new DataTable("Hourly");
                    //Create the Columns in the DataTable
                    DataColumn c0 = new DataColumn("Name");
                    DataColumn c1 = new DataColumn("Sex");
                    DataColumn c2 = new DataColumn("EGN");
                    DataColumn c3 = new DataColumn("E-Mail");
                    DataColumn c4 = new DataColumn("Address");
                    DataColumn c5 = new DataColumn("Department");
                    DataColumn c6 = new DataColumn("Position");
                    DataColumn c7 = new DataColumn("Sign_Date");
                    DataColumn c8 = new DataColumn("Hourly_Payment");
                    //Add the Created Columns to the Datatable
                    Hourly.Columns.Add(c0);
                    Hourly.Columns.Add(c1);
                    Hourly.Columns.Add(c2);
                    Hourly.Columns.Add(c3);
                    Hourly.Columns.Add(c4);
                    Hourly.Columns.Add(c5);
                    Hourly.Columns.Add(c6);
                    Hourly.Columns.Add(c7);
                    Hourly.Columns.Add(c8);

                    while (reader.Read())
                    {  //Create rows
                        DataRow row;
                        row = Hourly.NewRow();
                        row["Name"] = reader[0].ToString();
                        row["Sex"] = reader[4].ToString();
                        row["EGN"] = reader[5].ToString();
                        row["E-Mail"] = reader[6].ToString();
                        row["Address"] = reader[7].ToString();
                        row["Department"] = reader[8].ToString();
                        row["Position"] = reader[9].ToString();
                        row["Sign_Date"] = reader[10].ToString();
                        row["Hourly_Payment"] = reader[11].ToString();
                        //Add rows to table       
                        Hourly.Rows.Add(row);
                    }
                    dataGridView1.DataSource = Hourly;
                    reader.Dispose();
                    connection.Close();
                }
                catch (Exception exc)
                {
                    MessageBox.Show("Error " + exc);
                    connection.Close();
                }
            }
        }    // DONE !!!!!!!!!!
        private void TwoGoButton_Click(object sender, EventArgs e)
        {
            
            ExportToPDFBtn.Enabled = true;
            refreshDataGridView();
            string query = "";
            try
            {
                if (TwoMaleCheckBox.Checked == false && TwoFemaleCheckBox.Checked == false)
                {
                    MessageBox.Show("Please make a valid selection..", "Error");
                }
                else
                {
                    //Create query from checkboxes
                    if (TwoMaleCheckBox.Checked == true && TwoFemaleCheckBox.Checked == false)//MALE
                    {
                        query = @"  SELECT        Users.*, Sex AS Expr1
                                FROM            Users
                                WHERE        (Sex = 'Male')";
                    }
                    else if (TwoFemaleCheckBox.Checked == true && TwoMaleCheckBox.Checked == false)//FEMALE
                    {
                        query = @"  SELECT        Users.*, Sex AS Expr1
                                FROM            Users
                                WHERE        (Sex = 'Female')";
                    }
                    else if (TwoMaleCheckBox.Checked == true && TwoFemaleCheckBox.Checked == true)//BOTH
                    {
                        query = @"  SELECT        Users.*, Sex AS Expr1
                                FROM            Users
                                WHERE        (Sex = 'Male') OR
                                             (Sex = 'Female')";
                    }
                    else
                    {
                        MessageBox.Show("Please select Male or Female");

                    }
                    connection.Open();
                    OleDbCommand command = new OleDbCommand();
                    command.Connection = connection;
                    command.CommandText = query;
                    OleDbDataReader reader = command.ExecuteReader();
                    //Create a New DataTable to store the Data
                    DataTable Sex = new DataTable("Sex");
                    //Create the Columns in the DataTable
                    DataColumn c0 = new DataColumn("Name");
                    DataColumn c1 = new DataColumn("Sex");
                    DataColumn c2 = new DataColumn("EGN");
                    DataColumn c3 = new DataColumn("E-Mail");
                    DataColumn c4 = new DataColumn("Address");
                    DataColumn c5 = new DataColumn("Department");
                    DataColumn c6 = new DataColumn("Position");
                    DataColumn c7 = new DataColumn("Sign_Date");
                    DataColumn c8 = new DataColumn("Hourly_Payment");
                    //Add the Created Columns to the Datatable
                    Sex.Columns.Add(c0);
                    Sex.Columns.Add(c1);
                    Sex.Columns.Add(c2);
                    Sex.Columns.Add(c3);
                    Sex.Columns.Add(c4);
                    Sex.Columns.Add(c5);
                    Sex.Columns.Add(c6);
                    Sex.Columns.Add(c7);
                    Sex.Columns.Add(c8);
                    while (reader.Read())
                    {  //Create rows
                        DataRow row;
                        row = Sex.NewRow();
                        row["Name"] = reader[0].ToString();
                        row["Sex"] = reader[4].ToString();
                        row["EGN"] = reader[5].ToString();
                        row["E-Mail"] = reader[6].ToString();
                        row["Address"] = reader[7].ToString();
                        row["Department"] = reader[8].ToString();
                        row["Position"] = reader[9].ToString();
                        row["Sign_Date"] = reader[10].ToString();
                        row["Hourly_Payment"] = reader[11].ToString();
                        //Add rows to table       
                        Sex.Rows.Add(row);
                    }
                    dataGridView1.DataSource = Sex;
                    reader.Dispose();
                    connection.Close();
                }
            }
            catch (Exception exc)
            {
                connection.Close();
                MessageBox.Show("Error " + exc);
            }
        }   // DONE !!!!!!!!!!
        private void ThreeGoButton_Click(object sender, EventArgs e)
        {
           
            ExportToPDFBtn.Enabled = true;
            string query = "";
            refreshDataGridView();

            if (ThreeActiveCheckBox.Checked == false && ThreeInactiveCheckBox.Checked == false && ThreeFreeCheckBox.Checked == false)
            {
                MessageBox.Show("Please make a valid selection.", "Error");
            }
            else
            {
                if (forComboBox.Text == "Cards" && byComboBox.Text == "Active Status")
                {
                    try
                    {
                        connection.Open();
                        OleDbCommand command = new OleDbCommand();
                        command.Connection = connection;
                        //Create query from checkboxes
                        if (ThreeActiveCheckBox.Checked == true && ThreeInactiveCheckBox.Checked == false) //only active
                        {
                            query = @"     SELECT         Cards.CardID, Cards.isActive, Users.Name
                               FROM           (Cards INNER JOIN Users ON Cards.EGN = Users.EGN)
                               WHERE          (Cards.isActive = True)";
                            command.CommandText = query;
                            OleDbDataReader reader = command.ExecuteReader();
                            //Create a New DataTable to store the Data
                            DataTable Active = new DataTable("Active");
                            //Create the Columns in the DataTable
                            DataColumn c0 = new DataColumn("CardID");
                            DataColumn c1 = new DataColumn("Name");
                            //Add the Created Columns to the Datatable
                            Active.Columns.Add(c0);
                            Active.Columns.Add(c1);
                            while (reader.Read())
                            {
                                //Create rows
                                DataRow row;
                                row = Active.NewRow();
                                row["CardID"] = reader[0].ToString();
                                row["Name"] = reader[2].ToString();
                                //Add rows to table       
                                Active.Rows.Add(row);
                            }
                            reader.Dispose();
                            connection.Close();
                            dataGridView1.DataSource = Active;
                        }
                        else if (ThreeInactiveCheckBox.Checked == true && ThreeActiveCheckBox.Checked == false) // only inactive
                        {
                            query = @"         SELECT      Cards.CardID, Cards.isActive, Users.Name
                                   FROM        (Cards INNER JOIN Users ON Cards.EGN = Users.EGN)
                                   WHERE       (Cards.isActive = False)";
                            command.CommandText = query;
                            OleDbDataReader reader = command.ExecuteReader();
                            //Create a New DataTable to store the Data
                            DataTable Inactive = new DataTable("Active");
                            //Create the Columns in the DataTable
                            DataColumn c0 = new DataColumn("CardID");
                            DataColumn c1 = new DataColumn("Name");
                            //Add the Created Columns to the Datatable
                            Inactive.Columns.Add(c0);
                            Inactive.Columns.Add(c1);
                            while (reader.Read())
                            {
                                //Create rows
                                DataRow row;
                                row = Inactive.NewRow();
                                row["CardID"] = reader[0].ToString();
                                row["Name"] = reader[2].ToString();
                                //Add rows to table       
                                Inactive.Rows.Add(row);
                            }
                            reader.Dispose();
                            connection.Close();
                            dataGridView1.DataSource = Inactive;
                        }
                        else if (ThreeActiveCheckBox.Checked == true && ThreeInactiveCheckBox.Checked == true) //both
                        {
                            query = @"         SELECT      Cards.CardID, Cards.EGN, Users.Name, Cards.isActive
                                   FROM        (Cards INNER JOIN
                                                     Users ON Cards.EGN = Users.EGN)
                                   WHERE       (Cards.isActive = True) OR
                                               (Cards.isActive = False)";
                            command.CommandText = query;
                            OleDbDataReader reader = command.ExecuteReader();
                            //Create a New DataTable to store the Data
                            DataTable Both = new DataTable("Both");
                            //Create the Columns in the DataTable
                            DataColumn c0 = new DataColumn("CardID");
                            DataColumn c1 = new DataColumn("Name");
                            DataColumn c2 = new DataColumn("Active Status");
                            //Add the Created Columns to the Datatable
                            Both.Columns.Add(c0);
                            Both.Columns.Add(c1);
                            Both.Columns.Add(c2);
                            while (reader.Read())
                            {
                                //Create rows
                                DataRow row;
                                row = Both.NewRow();
                                row["CardID"] = reader[0].ToString();
                                row["Name"] = reader[2].ToString();
                                row["Active Status"] = reader[3].ToString();
                                //Add rows to table       
                                Both.Rows.Add(row);
                            }
                            reader.Dispose();
                            connection.Close();
                            dataGridView1.DataSource = Both;
                        }
                        else if (ThreeFreeCheckBox.Checked == true) //free is checked
                        {
                            query = @"  SELECT        CardID
                            FROM          Cards
                            WHERE        (EGN IS NULL)";
                            command.CommandText = query;
                            OleDbDataReader reader = command.ExecuteReader();
                            //Create a New DataTable to store the Data
                            DataTable Free = new DataTable("Free");
                            //Create the Columns in the DataTable
                            DataColumn c0 = new DataColumn("CardID");
                            //Add the Created Columns to the Datatable
                            Free.Columns.Add(c0);
                            while (reader.Read())
                            {
                                //Create rows
                                DataRow row;
                                row = Free.NewRow();
                                row["CardID"] = reader[0].ToString();
                                //Add rows to table       
                                Free.Rows.Add(row);
                            }
                            reader.Dispose();
                            connection.Close();
                            dataGridView1.DataSource = Free;
                        }
                        connection.Close();
                    }
                    catch (Exception exc)
                    {
                        connection.Close();
                        MessageBox.Show("Error: " + exc);
                    }
                }
                else if (forComboBox.Text == "Users")
                {
                    ThreeFreeCheckBox.Checked = false;
                    connection.Open();
                    OleDbCommand command = new OleDbCommand();
                    command.Connection = connection;
                    if (byComboBox.Text == "Admin Status")
                    {
                        if (ThreeActiveCheckBox.Checked == true && ThreeInactiveCheckBox.Checked == false) // isAdmin
                        {
                            query = @"  SELECT        Users.*, isAdmin AS Expr1
                                        FROM            Users
                                        WHERE        (isAdmin = True)";
                        }
                        else if (ThreeInactiveCheckBox.Checked == true && ThreeActiveCheckBox.Checked == false) // isNOTAdmin
                        {
                            query = @"  SELECT        Users.*, isAdmin AS Expr1
                                        FROM            Users
                                        WHERE        (isAdmin = False)";
                        }
                        else if (ThreeActiveCheckBox.Checked == true && ThreeInactiveCheckBox.Checked == true) // Both
                        {
                            query = @" SELECT        Users.*, isAdmin AS Expr1
                                        FROM            Users
                                        WHERE        (isAdmin = True) OR
                                                        (isAdmin = False)";
                        }
                        try
                        {
                            command.CommandText = query;
                            OleDbDataReader reader = command.ExecuteReader();
                            //Create a New DataTable to store the Data
                            DataTable Admin_Status = new DataTable("Admin_Status");
                            //Create the Columns in the DataTable
                            DataColumn c0 = new DataColumn("Name");
                            Admin_Status.Columns.Add(c0);

                            if (ThreeActiveCheckBox.Checked == true) // isAdmin
                            {
                                DataColumn c1 = new DataColumn("Username");
                                Admin_Status.Columns.Add(c1);
                                DataColumn c2 = new DataColumn("Password");
                                Admin_Status.Columns.Add(c2);
                            }
                            DataColumn c3 = new DataColumn("Sex");
                            Admin_Status.Columns.Add(c3);
                            DataColumn c4 = new DataColumn("EGN");
                            Admin_Status.Columns.Add(c4);
                            DataColumn c5 = new DataColumn("E-Mail");
                            Admin_Status.Columns.Add(c5);
                            DataColumn c6 = new DataColumn("Address");
                            Admin_Status.Columns.Add(c6);
                            DataColumn c7 = new DataColumn("Department");
                            Admin_Status.Columns.Add(c7);
                            DataColumn c8 = new DataColumn("Position");
                            Admin_Status.Columns.Add(c8);
                            DataColumn c9 = new DataColumn("Sign_Date");
                            Admin_Status.Columns.Add(c9);
                            DataColumn c10 = new DataColumn("Hourly_Payment");
                            Admin_Status.Columns.Add(c10);
                            while (reader.Read())
                            {  //Create rows
                                DataRow row;
                                row = Admin_Status.NewRow();
                                row["Name"] = reader[0].ToString();
                                if (ThreeActiveCheckBox.Checked == true) // isAdmin
                                {
                                    row["Username"] = reader[1].ToString();
                                    row["Password"] = new string('*', reader[2].ToString().Length);
                                }
                                row["Sex"] = reader[4].ToString();
                                row["EGN"] = reader[5].ToString();
                                row["E-Mail"] = reader[6].ToString();
                                row["Address"] = reader[7].ToString();
                                row["Department"] = reader[8].ToString();
                                row["Position"] = reader[9].ToString();
                                row["Sign_Date"] = reader[10].ToString();
                                row["Hourly_Payment"] = reader[11].ToString();
                                //Add rows to table       
                                Admin_Status.Rows.Add(row);
                            }
                            dataGridView1.DataSource = Admin_Status;
                            reader.Dispose();
                            connection.Close();
                        }
                        catch (Exception exc)
                        {
                            connection.Close();
                            MessageBox.Show("Error " + exc);
                        }
                    }
                    else if (byComboBox.Text == "Active Status")
                    {
                        if (ThreeActiveCheckBox.Checked == true && ThreeInactiveCheckBox.Checked == false) //true
                        {
                            query = @"  SELECT        Users.*, isActive AS Expr1
                                    FROM            Users
                                    WHERE        (isActive = True)";
                        }
                        else if (ThreeInactiveCheckBox.Checked == true && ThreeActiveCheckBox.Checked == false) //false
                        {
                            query = @"  SELECT        Users.*, isActive AS Expr1
                                    FROM            Users
                                    WHERE        (isActive = False)";
                        }
                        else if (ThreeInactiveCheckBox.Checked == true && ThreeActiveCheckBox.Checked == true) //both
                        {
                            query = @"  SELECT        Users.*, isActive AS Expr1
                                    FROM            Users
                                    WHERE        (isActive = True) OR
                                                     (isActive = False)";
                        }
                        try
                        {
                            command.CommandText = query;
                            OleDbDataReader reader = command.ExecuteReader();
                            //Create a New DataTable to store the Data
                            DataTable Active_Status = new DataTable("Active_Status");
                            //Create the Columns in the DataTable
                            DataColumn c0 = new DataColumn("Name");
                            Active_Status.Columns.Add(c0);
                            DataColumn c1 = new DataColumn("Username");
                            Active_Status.Columns.Add(c1);
                            DataColumn c2 = new DataColumn("Password");
                            Active_Status.Columns.Add(c2);
                            DataColumn c3 = new DataColumn("Sex");
                            Active_Status.Columns.Add(c3);
                            DataColumn c4 = new DataColumn("EGN");
                            Active_Status.Columns.Add(c4);
                            DataColumn c5 = new DataColumn("E-Mail");
                            Active_Status.Columns.Add(c5);
                            DataColumn c6 = new DataColumn("Address");
                            Active_Status.Columns.Add(c6);
                            DataColumn c7 = new DataColumn("Department");
                            Active_Status.Columns.Add(c7);
                            DataColumn c8 = new DataColumn("Position");
                            Active_Status.Columns.Add(c8);
                            DataColumn c9 = new DataColumn("Sign_Date");
                            Active_Status.Columns.Add(c9);
                            DataColumn c10 = new DataColumn("Hourly_Payment");
                            Active_Status.Columns.Add(c10);
                            while (reader.Read())
                            {  //Create rows
                                DataRow row;
                                row = Active_Status.NewRow();
                                row["Name"] = reader[0].ToString();
                                row["Username"] = reader[1].ToString();
                                row["Password"] = new string('*', reader[2].ToString().Length);
                                row["Sex"] = reader[4].ToString();
                                row["EGN"] = reader[5].ToString();
                                row["E-Mail"] = reader[6].ToString();
                                row["Address"] = reader[7].ToString();
                                row["Department"] = reader[8].ToString();
                                row["Position"] = reader[9].ToString();
                                row["Sign_Date"] = reader[10].ToString();
                                row["Hourly_Payment"] = reader[11].ToString();
                                //Add rows to table       
                                Active_Status.Rows.Add(row);
                            }
                            dataGridView1.DataSource = Active_Status;

                            reader.Dispose();
                            connection.Close();
                        }
                        catch (Exception exc)
                        {
                            connection.Close();
                            MessageBox.Show("Error " + exc);
                        }
                    }
                }

            }
        } // DONE !!!!!!!!
        private void FourGoButton_Click(object sender, EventArgs e)
        {
            
            ExportToPDFBtn.Enabled = true;
            refreshDataGridView();
            string query = "";

            if (forComboBox.SelectedItem.ToString() == "Users")
            {
                if (byComboBox.SelectedItem.ToString() == "EGN")
                {
                    if (FourComboBox.Text != "")
                    {
                        query = @"          SELECT        Users.*, EGN AS Expr1
                                            FROM            Users
                                            WHERE        (EGN = '" + FourComboBox.Text + "')";
                    }
                    else
                        MessageBox.Show("Please make a valid selection.", "Error");
                }
                else if (byComboBox.SelectedItem.ToString() == "Department")
                {
                    if (FourComboBox.Text != "")
                    {
                        query = @"          SELECT        Users.*, Department AS Expr1
                                            FROM            Users
                                            WHERE        (Department = '" + FourComboBox.Text + "')";
                    }
                    else
                        MessageBox.Show("Please make a valid selection.", "Error");
                }
                else if (byComboBox.SelectedItem.ToString() == "Position")
                {
                    if (FourComboBox.Text != "")
                    {
                        query = @"          SELECT        Users.*, [Position] AS Expr2
                                            FROM            Users
                                            WHERE        ([Position] = '" + FourComboBox.Text + "')";
                    }
                    else
                        MessageBox.Show("Please make a valid selection.", "Error");
                }
                if (FourComboBox.Text != "")
                {
                    try
                    {
                        connection.Open();
                        OleDbCommand command = new OleDbCommand();
                        command.Connection = connection;
                        command.CommandText = query;
                        OleDbDataReader reader = command.ExecuteReader();
                        //Create a New DataTable to store the Data
                        DataTable USERS = new DataTable("USERS");
                        //Create the Columns in the DataTable
                        DataColumn c0 = new DataColumn("Name");
                        USERS.Columns.Add(c0);
                        DataColumn c1 = new DataColumn("Username");
                        USERS.Columns.Add(c1);
                        DataColumn c2 = new DataColumn("Password");
                        USERS.Columns.Add(c2);
                        DataColumn c3 = new DataColumn("Sex");
                        USERS.Columns.Add(c3);
                        DataColumn c4 = new DataColumn("USERS");
                        USERS.Columns.Add(c4);
                        DataColumn c5 = new DataColumn("E-Mail");
                        USERS.Columns.Add(c5);
                        DataColumn c6 = new DataColumn("Address");
                        USERS.Columns.Add(c6);
                        DataColumn c7 = new DataColumn("Department");
                        USERS.Columns.Add(c7);
                        DataColumn c8 = new DataColumn("Position");
                        USERS.Columns.Add(c8);
                        DataColumn c9 = new DataColumn("Sign_Date");
                        USERS.Columns.Add(c9);
                        DataColumn c10 = new DataColumn("Hourly_Payment");
                        USERS.Columns.Add(c10);
                        while (reader.Read())
                        {  //Create rows
                            DataRow row;
                            row = USERS.NewRow();
                            row["Name"] = reader[0].ToString();
                            row["Username"] = reader[1].ToString();
                            row["Password"] = new string('*', reader[2].ToString().Length);
                            row["Sex"] = reader[4].ToString();
                            row["USERS"] = reader[5].ToString();
                            row["E-Mail"] = reader[6].ToString();
                            row["Address"] = reader[7].ToString();
                            row["Department"] = reader[8].ToString();
                            row["Position"] = reader[9].ToString();
                            row["Sign_Date"] = reader[10].ToString();
                            row["Hourly_Payment"] = reader[11].ToString();
                            //Add rows to table       
                            USERS.Rows.Add(row);
                        }
                        dataGridView1.DataSource = USERS;
                        reader.Dispose();
                        connection.Close();
                    }
                    catch (Exception exc)
                    {
                        connection.Close();
                        MessageBox.Show("Error " + exc);
                    }
                }
            }
            else if (forComboBox.SelectedItem.ToString() == "Cards")
            {
                if (byComboBox.SelectedItem.ToString() == "EGN")
                {
                    if (FourComboBox.Text != "")
                    {
                        connection.Open();
                        OleDbCommand command = new OleDbCommand();
                        command.Connection = connection;

                        query = @"  SELECT        Cards.*, EGN AS Expr1
                                    FROM            Cards
                                    WHERE        (EGN = '" + FourComboBox.Text + "')";
                        command.CommandText = query;
                        OleDbDataReader reader = command.ExecuteReader();
                        DataTable CARDS = new DataTable("CARDS");
                        DataColumn c0 = new DataColumn("EGN");
                        CARDS.Columns.Add(c0);
                        DataColumn c1 = new DataColumn("CardID");
                        CARDS.Columns.Add(c1);
                        DataColumn c2 = new DataColumn("isActive");
                        CARDS.Columns.Add(c2);
                        DataColumn c3 = new DataColumn("ActiveDate");
                        CARDS.Columns.Add(c3);
                        while (reader.Read())
                        {
                            //Create rows
                            DataRow row;
                            row = CARDS.NewRow();
                            row["EGN"] = reader[1].ToString();
                            row["CardID"] = reader[0].ToString();
                            row["isActive"] = reader[2].ToString();
                            row["ActiveDate"] = reader[3].ToString();
                            //Add rows to table       
                            CARDS.Rows.Add(row);
                        }
                        reader.Dispose();
                        connection.Close();
                        dataGridView1.DataSource = CARDS;
                    }
                    else
                    {
                        connection.Close();
                        MessageBox.Show("Please make a valid selection.", "Error");
                    }
                }
            }
            else if (forComboBox.SelectedItem.ToString() == "Logs")
            {
                if (byComboBox.SelectedItem.ToString() == "CardID")
                {
                    if (FourComboBox.Text != "")
                    {
                        connection.Open();
                        OleDbCommand command = new OleDbCommand();
                        command.Connection = connection;

                        query = @"  SELECT        Checkin_log.*, CardID AS Expr1
                                    FROM            Checkin_log
                                    WHERE        (CardID = '" + FourComboBox.Text + "')";
                        command.CommandText = query;
                        OleDbDataReader reader = command.ExecuteReader();
                        DataTable LOGS = new DataTable("LOGS");
                        DataColumn c0 = new DataColumn("CardID");
                        LOGS.Columns.Add(c0);
                        DataColumn c1 = new DataColumn("CheckIn");
                        LOGS.Columns.Add(c1);
                        DataColumn c2 = new DataColumn("CheckOut");
                        LOGS.Columns.Add(c2);
                        DataColumn c3 = new DataColumn("ElapsedTime");
                        LOGS.Columns.Add(c3);
                        while (reader.Read())
                        {
                            //Create rows
                            DataRow row;
                            row = LOGS.NewRow();
                            row["CardID"] = reader[0].ToString();
                            row["CheckIn"] = reader[1].ToString();
                            row["CheckOut"] = reader[2].ToString();
                            row["ElapsedTime"] = reader[3].ToString();
                            //Add rows to table       
                            LOGS.Rows.Add(row);
                        }
                        reader.Dispose();
                        dataGridView1.DataSource = LOGS;
                        connection.Close();
                    }
                    else
                    {
                        MessageBox.Show("Please make a valid selection.", "Error");
                        connection.Close();
                    }
                }
            }
        } // DONE !!!!!!!!!!!
        private void FiveGoButton_Click(object sender, EventArgs e)
        {
            
            ExportToPDFBtn.Enabled = true;
            refreshDataGridView();
            string query = "";
            if (FiveFromCheckBox.Checked == false && FiveToCheckBox.Checked == false)
            {
                MessageBox.Show("Please make a valid selection.", "Error");
            }
            else
            {
                if (forComboBox.SelectedItem.ToString() == "Users")
                {
                    if (FiveFromCheckBox.Checked == true && FiveToCheckBox.Checked == false) // selected only FROM
                    {
                        query = @"  SELECT        Users.*, Sign_Date AS Expr1
                                FROM            Users
                                WHERE        (Sign_Date >= #" + FiveFromDateTimePicker.Text.ToString() + "#)";
                    }
                    else if (FiveFromCheckBox.Checked == false && FiveToCheckBox.Checked == true) // selected only TO
                    {
                        query = @"  SELECT        Users.*, Sign_Date AS Expr1
                                FROM            Users
                                WHERE        (Sign_Date <= #" + FiveToDateTimePicker.Text.ToString() + "#)";

                    }
                    else if (FiveFromCheckBox.Checked == true && FiveToCheckBox.Checked == true) // selected BOTH
                    {
                        query = @"  SELECT        Users.*, Sign_Date AS Expr1
                                FROM            Users
                                WHERE        (Sign_Date >= #" + FiveFromDateTimePicker.Text.ToString() + @"#) AND
                                                 (Sign_Date <= #" + FiveToDateTimePicker.Text.ToString() + "#)";
                    }
                    try
                    {
                        connection.Open();
                        OleDbCommand command = new OleDbCommand();
                        command.Connection = connection;
                        command.CommandText = query;
                        OleDbDataReader reader = command.ExecuteReader();
                        //Create a New DataTable to store the Data
                        DataTable USERS = new DataTable("USERS");
                        //Create the Columns in the DataTable
                        DataColumn c0 = new DataColumn("Name");
                        USERS.Columns.Add(c0);
                        DataColumn c1 = new DataColumn("Username");
                        USERS.Columns.Add(c1);
                        DataColumn c2 = new DataColumn("Password");
                        USERS.Columns.Add(c2);
                        DataColumn c3 = new DataColumn("Sex");
                        USERS.Columns.Add(c3);
                        DataColumn c4 = new DataColumn("USERS");
                        USERS.Columns.Add(c4);
                        DataColumn c5 = new DataColumn("E-Mail");
                        USERS.Columns.Add(c5);
                        DataColumn c6 = new DataColumn("Address");
                        USERS.Columns.Add(c6);
                        DataColumn c7 = new DataColumn("Department");
                        USERS.Columns.Add(c7);
                        DataColumn c8 = new DataColumn("Position");
                        USERS.Columns.Add(c8);
                        DataColumn c9 = new DataColumn("Sign_Date");
                        USERS.Columns.Add(c9);
                        DataColumn c10 = new DataColumn("Hourly_Payment");
                        USERS.Columns.Add(c10);
                        while (reader.Read())
                        {  //Create rows
                            DataRow row;
                            row = USERS.NewRow();
                            row["Name"] = reader[0].ToString();
                            row["Username"] = reader[1].ToString();
                            row["Password"] = new string('*', reader[2].ToString().Length);
                            row["Sex"] = reader[4].ToString();
                            row["USERS"] = reader[5].ToString();
                            row["E-Mail"] = reader[6].ToString();
                            row["Address"] = reader[7].ToString();
                            row["Department"] = reader[8].ToString();
                            row["Position"] = reader[9].ToString();
                            row["Sign_Date"] = reader[10].ToString();
                            row["Hourly_Payment"] = reader[11].ToString();
                            //Add rows to table       
                            USERS.Rows.Add(row);
                        }
                        dataGridView1.DataSource = USERS;

                        reader.Dispose();
                        connection.Close();
                    }
                    catch (Exception exc)
                    {
                        connection.Close();
                        MessageBox.Show("Error " + exc);
                    }
                }
                else if (forComboBox.SelectedItem.ToString() == "Cards")
                {
                    if (FiveFromCheckBox.Checked == true && FiveToCheckBox.Checked == false) // selected only FROM
                    {
                        query = @"  SELECT        Cards.*, ActiveDate AS Expr1
                                    FROM            Cards
                                    WHERE        (ActiveDate >= #" + FiveFromDateTimePicker.Text.ToString() + "#)";
                    }
                    else if (FiveFromCheckBox.Checked == false && FiveToCheckBox.Checked == true) // selected only TO
                    {
                        query = @"  SELECT        Cards.*, ActiveDate AS Expr1
                                    FROM            Cards
                                    WHERE        (ActiveDate <= #" + FiveToDateTimePicker.Text.ToString() + "#)";

                    }
                    else if (FiveFromCheckBox.Checked == true && FiveToCheckBox.Checked == true) // selected BOTH
                    {
                        query = @"  SELECT        Cards.*, ActiveDate AS Expr1
                                    FROM            Cards
                                    WHERE        (ActiveDate >= #" + FiveFromDateTimePicker.Text.ToString() + @"#) AND
                                                 (ActiveDate <= #" + FiveToDateTimePicker.Text.ToString() + "#)";
                    }
                    try
                    {
                        connection.Open();
                        OleDbCommand command = new OleDbCommand();
                        command.Connection = connection;
                        command.CommandText = query;
                        OleDbDataReader reader = command.ExecuteReader();
                        DataTable CARDS = new DataTable("CARDS");
                        DataColumn c0 = new DataColumn("CardID");
                        CARDS.Columns.Add(c0);
                        DataColumn c1 = new DataColumn("EGN");
                        CARDS.Columns.Add(c1);
                        DataColumn c2 = new DataColumn("isActive");
                        CARDS.Columns.Add(c2);
                        DataColumn c3 = new DataColumn("ActiveDate");
                        CARDS.Columns.Add(c3);

                        while (reader.Read())
                        {
                            //Create rows
                            DataRow row;
                            row = CARDS.NewRow();
                            row["EGN"] = reader[0].ToString();
                            row["CardID"] = reader[1].ToString();
                            row["isActive"] = reader[2].ToString();
                            row["ActiveDate"] = DateTime.Parse(reader[3].ToString()).ToShortDateString();
                            //Add rows to table       
                            CARDS.Rows.Add(row);
                        }
                        reader.Dispose();
                        connection.Close();
                        dataGridView1.DataSource = CARDS;

                    }
                    catch (Exception exc)
                    {
                        connection.Close();
                        MessageBox.Show("Error " + exc);
                    }
                }
                else if (forComboBox.SelectedItem.ToString() == "Logs")
                {
                    try
                    {
                        connection.Open();
                        OleDbCommand command = new OleDbCommand();
                        command.Connection = connection;

                        if (FiveFromCheckBox.Checked == true && FiveToCheckBox.Checked == false) // selected only FROM
                        {
                            query = @"  SELECT        Checkin_log.*
                                        FROM            Checkin_log
                                        WHERE        (Check_In >= #" + FiveFromDateTimePicker.Text.ToString() + "#)";
                        }
                        else if (FiveFromCheckBox.Checked == false && FiveToCheckBox.Checked == true) // selected only TO
                        {
                            query = @"  SELECT        Checkin_log.*
                                        FROM            Checkin_log
                                        WHERE        (Check_In <= #" + FiveToDateTimePicker.Text.ToString() + "#)";

                        }
                        else if (FiveFromCheckBox.Checked == true && FiveToCheckBox.Checked == true) // selected BOTH
                        {
                            query = @"  SELECT        Checkin_log.*
                                        FROM            Checkin_log
                                        WHERE        (Check_In >= #" + FiveFromDateTimePicker.Text.ToString() + "#) AND (Check_In <= #" + FiveToDateTimePicker.Text.ToString() + "#)";
                        }

                        command.CommandText = query;
                        OleDbDataReader reader = command.ExecuteReader();
                        DataTable LOGS = new DataTable("LOGS");
                        DataColumn c0 = new DataColumn("CardID");
                        LOGS.Columns.Add(c0);
                        DataColumn c1 = new DataColumn("CheckIn");
                        LOGS.Columns.Add(c1);
                        DataColumn c2 = new DataColumn("CheckOut");
                        LOGS.Columns.Add(c2);
                        DataColumn c3 = new DataColumn("ElapsedTime");
                        LOGS.Columns.Add(c3);

                        while (reader.Read())
                        {
                            //Create rows
                            DataRow row;
                            row = LOGS.NewRow();
                            row["CardID"] = reader[0].ToString();
                            row["CheckIn"] = reader[1].ToString();
                            row["CheckOut"] = reader[2].ToString();
                            row["ElapsedTime"] = reader[3].ToString();
                            //Add rows to table       
                            LOGS.Rows.Add(row);
                        }
                        reader.Dispose();
                        dataGridView1.DataSource = LOGS;
                        connection.Close();
                    }
                    catch (Exception exc)
                    {
                        connection.Close();
                        MessageBox.Show("Error " + exc);
                    }
                }
            }
        } // DONE !!!!!!!!!!
        private void refreshDataGridView()
        {
            dataGridView1.DataSource = null;
            dataGridView1.Refresh();
        }

        private void ExportToPDFBtn_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("Do you want to export query to PDF file?", "Are you sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res == DialogResult.Yes)
            {

                if (dataGridView1.Rows.Count > 0)
                {
                    SaveFileDialog sfd = new SaveFileDialog();
                    sfd.Filter = "PDF (*.pdf)|*.pdf";
                    sfd.FileName = "Query for [" + forComboBox.Text + "] by [" + byComboBox.Text + "] generated on " + DateTime.Now.ToShortDateString() + "from administrator ["+ GlobalClass.CurrentUser.ToString()+"].pdf";
                    bool fileError = false;
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        if (File.Exists(sfd.FileName))
                        {
                            try
                            {
                                File.Delete(sfd.FileName);
                            }
                            catch (IOException ex)
                            {
                                fileError = true;
                                MessageBox.Show("It wasn't possible to write the data to the disk." + ex.Message);
                            }
                        }
                        if (!fileError)
                        {
                            try
                            {
                                PdfPTable pdfTable = new PdfPTable(dataGridView1.Columns.Count);
                                pdfTable.DefaultCell.Padding = 3;
                                pdfTable.WidthPercentage = 100;
                                pdfTable.HorizontalAlignment = Element.ALIGN_LEFT;




                                foreach (DataGridViewColumn column in dataGridView1.Columns)
                                {
                                    PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText));
                                    pdfTable.AddCell(cell);
                                }

                                foreach (DataGridViewRow row in dataGridView1.Rows)
                                {
                                    foreach (DataGridViewCell cell in row.Cells)
                                    {
                                        if (cell.Value != null)
                                            pdfTable.AddCell(cell.Value.ToString());
                                    }
                                }

                                using (FileStream stream = new FileStream(sfd.FileName, FileMode.Create))
                                {
                                    Document pdfDoc = new Document(PageSize.A4, 10f, 20f, 20f, 10f);
                                    PdfWriter.GetInstance(pdfDoc, stream);
                                    pdfDoc.Open();
                                    pdfDoc.Add(pdfTable);
                                    pdfDoc.Close();
                                    stream.Close();
                                }

                                MessageBox.Show("Data Exported Successfully !!!", "Info");
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Error :" + ex.Message);
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("No Record To Export !!!", "Info");
                }
            }
            else //no
            {

            }
        }
    }
}
