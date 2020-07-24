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
    public partial class Database : Form
    {
        OleDbConnection connection = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + GlobalClass.FileNameAndPath);
        int hoverCounter = 0;
        bool hidePass = true;

        public Database()
        {
            InitializeComponent();
            ComboBoxUpdate();
            button1.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            connection.Close();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();
            button1.Enabled = false;
           
            Load_Table(comboBox1.SelectedItem.ToString());
            
        }

        private void ComboBoxUpdate()
        {
            connection.Open();
            try
            {
                DataTable schema = connection.GetSchema("Tables");
                foreach (DataRow row in schema.Rows)
                {
                    if (!row.Field<string>("TABLE_NAME").Contains("MSys"))
                    {
                        comboBox1.Items.Add(row.Field<string>("TABLE_NAME"));
                    }
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex);
                connection.Close();
            }
        }

        private void Load_Table(string tableName)
        {
            try
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                string querry = "SELECT * FROM " + tableName + "";
                command.CommandText = querry;
                OleDbDataAdapter da = new OleDbDataAdapter(command);
                DataTable dt = new DataTable();
                this.dataGridView1.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 15);
               
                da.Fill(dt);
                dataGridView1.DataSource = dt;
                connection.Close();
            }

            catch (Exception exp)
            {
                MessageBox.Show("Error " + exp);
                connection.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();
            comboBox1.Text = "";
            comboBox1.SelectedItem = null;
            button1.Enabled = false;
            hidePass = true;
        }

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            button1.Enabled = true;
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (hidePass == true)
            {
                if (comboBox1.SelectedItem.ToString() == "Users")
                {
                    if (e.ColumnIndex == 2)
                    {
                        if (e.Value != null)
                        {
                            e.Value = new string('*', e.Value.ToString().Length);
                        }
                        else
                            e.Value = "Null";
                    }
                }else if (comboBox1.SelectedItem.ToString() == "Checkin_log")
                {
                    if(e.ColumnIndex == 1 || e.ColumnIndex == 2)
                    {
                        dataGridView1.Columns[1].DefaultCellStyle.Format = "dd/MM/yyyy hh:mm:ss";
                        dataGridView1.Columns[2].DefaultCellStyle.Format = "dd/MM/yyyy hh:mm:ss";
                    }
                }
            }
        }

        private void button1_MouseHover(object sender, EventArgs e)
        {
            hoverCounter++;
            if (hoverCounter==5){
                DialogResult res = MessageBox.Show("Show Passwords?","Show?",MessageBoxButtons.YesNo);
                if(res == DialogResult.Yes)
                {
                    hidePass = false;
                    button1.PerformClick();
                    
                }
                else if( res == DialogResult.No)
                {
                    hidePass = true;
                    button1.PerformClick();
                }
                hoverCounter = 0;
            }
            
        }
    }
}
