using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

//for Access DB
using System.Data.OleDb;

//for report
using Microsoft.Reporting.WinForms;

//for mail:
using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using System.IO;
using System.Globalization;

namespace testDiplomnaForms
{
    public partial class Reports : Form
    {
        OleDbConnection connection = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + GlobalClass.FileNameAndPath);

        string UserEGN = "";
        string UserEmail = "";
        string UserFullName = "";
        string FilePath = "";
        string FileName = "";


        public Reports()
        {
            InitializeComponent();

        }

        private void Reports_Load(object sender, EventArgs e)
        {
            GenerateButton.Enabled = false;
            ViewCheckBox.Enabled = false;

            //fill combobox with EGN's

            string query = @"   SELECT * FROM Users";
            comboBox1.Items.Clear();
            try
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                List<string> items = new List<string>();
                command.CommandText = query;
                OleDbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    items.Add(reader[5].ToString());
                }
                connection.Close();

                List<string> unique = items.Distinct().ToList();
                unique.Remove("");
                unique.Sort();
                comboBox1.Items.Add("*");
                for (int i = 0; i < unique.Count; i++)
                {
                    comboBox1.Items.Add(unique[i].ToString());
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error " + exc);
                connection.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (comboBox1.SelectedItem.ToString() != "*" && comboBox1.SelectedItem.ToString() != "") //is EGN
            {
                ViewCheckBox.Enabled = true;
                GenerateButton.Enabled = true;
                SaveCheckBox.Text = "View and Save";
                SaveCheckBox.Checked = false;
                ViewCheckBox.Checked = true;
                MailCheckBox.Checked = false;
                SetCurrentUser (comboBox1.SelectedItem.ToString());
            }
            else
            { //Selected all
                SaveCheckBox.Text = "Save";
                SaveCheckBox.Checked = true;
                ViewCheckBox.Enabled = false;
                GenerateButton.Enabled = true;
                MessageBox.Show("Selected All", "Selected All", MessageBoxButtons.OK);
            }
        }



        private void GenerateButton_Click(object sender, EventArgs e)
        {
            int savecounter = 0;
            int mailcounter = 0;
            if (comboBox1.Text == "")
            {
                MessageBox.Show("Wrong selection.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (!comboBox1.Items.Contains(comboBox1.Text))
            {
                MessageBox.Show("Error. " + comboBox1.Text + " is not in list.", "Invalid Selection");
            }
            else if (comboBox1.Text == "*") // SELECTED ALL
            {

               
                try
                {
                    string query = @"   SELECT * FROM Users";
                    //put all users in list
                    connection.Open();
                    OleDbCommand command = new OleDbCommand();
                    command.Connection = connection;
                    List<string> items = new List<string>();
                    command.CommandText = query;
                    OleDbDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        items.Add(reader[5].ToString());
                    }
                    connection.Close();
                    //remove duplicates
                    List<string> unique = items.Distinct().ToList();
                    unique.Remove("");
                    unique.Sort();

                    for (int i = 0; i < unique.Count; i++)
                    {
                        SetCurrentUser(unique[i].ToString());
                        viewUserReport(unique[i].ToString());
                        
                        if(SaveCheckBox.Checked==true)
                        {
                            SaveReportByUser(UserEGN);
                            savecounter++;
                        }
                        if(MailCheckBox.Checked == true)
                        {
                            MailUserReport(UserFullName, UserEmail, FilePath, FileName);
                            mailcounter++;
                        }
                    }
                    MessageBox.Show("Saved reports: " + savecounter + "\nMailed reports: " + mailcounter,"Success");
                    savecounter = 0;
                    mailcounter = 0;
                    connection.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error " + ex);
                    connection.Close();
                }
                MessageBox.Show("File(s) saved to database directory\n(" + GlobalClass.FilePath + ")", "File(s) Created!", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else // SELECTED EGN
            {

                if (SaveCheckBox.Checked == true)
                {
                    SaveReportByUser(UserEGN);
                    MessageBox.Show("Report saved to:\nFile Path and Name: " + FilePath + FileName, "Name: " + UserFullName );
                }
                if (ViewCheckBox.Checked == true)
                {
                    viewUserReport(UserEGN);
                }

                if (MailCheckBox.Checked == true)
                {
                    MailUserReport(UserFullName, UserEmail, FilePath, FileName);
                    MessageBox.Show("Report sent to:\nName: " + UserFullName + "\nEmail: " + UserEmail + "\nFile Path and Name: " + FilePath + FileName,"Sent");
                }
            }
        }
        private void viewUserReport(string UEGN)
        {
            // TODO: This line of code loads data into the 'Diplomna_DatabaseDataSet.DataTable1' table. You can move, or remove it, as needed.
            
            this.DataTable1TableAdapter.Connection = connection;
            this.DataTable1TableAdapter.FillByEGNAndDate(this.Diplomna_DatabaseDataSet.DataTable1, UEGN, FromDateTimePicker.Value, ToDateTimePicker.Value);
            this.reportViewer1.RefreshReport();
        }

        private void SetCurrentUser(string ComboBoxText)
        {
            UserEGN = ComboBoxText;
            string query = @"   SELECT * FROM Users WHERE EGN = '" + UserEGN + "'";
            try
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                command.CommandText = query;
                OleDbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    UserFullName = reader[0].ToString();
                    UserEmail = reader[6].ToString();
                    UserEGN = reader[5].ToString();
                }
                connection.Close();
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error " + exc);
                connection.Close();
            }
        }

        private void SaveReportByUser(string UEGN) //UserEGN, UserFullName 
        {
            viewUserReport(UEGN);
            //save file as PDF 
            try
            {
                FileStream fs;
                Warning[] warnings;
                string[] streamids;
                string mimeType, encoding, filenameExtension;
                byte[] bytes = reportViewer1.LocalReport.Render("PDF", null, out mimeType, out encoding, out filenameExtension, out streamids, out warnings);
                FilePath = GlobalClass.FilePath + "\\" + DateTime.Now.ToShortDateString() + "\\";
                Directory.CreateDirectory(FilePath);
                FileName = "AGR_" + UEGN + ".pdf";
                using (fs = new FileStream(FilePath + FileName, FileMode.Create))
                {
                    fs.Write(bytes, 0, bytes.Length);
                }
                fs.Dispose();
                fs.Close();
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error " + exc);
            }
        }

        private void MailUserReport(string UFN, string UE, string FP, string FN) //UserFullName, UserEmail, FileName, FilePath
        {
             try
            {
                string range;
                if (FromDateTimePicker.Value.ToString("MMMM") == ToDateTimePicker.Value.ToString("MMMM"))
                {
                    //one month
                    range = "For " + FromDateTimePicker.Value.ToString("MMMM");
                }
                else
                {
                    //range between
                    range = "Between " + FromDateTimePicker.Value.ToString("MMMM") + " And " + ToDateTimePicker.Value.ToString("MMMM");
                }

                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("Accounting", "accounting@office.com"));
                message.To.Add(new MailboxAddress(UFN, UE));
                message.Subject = "Monthly Salary Report " + range;

                // create the message text
                var body = new TextPart("plain")
                {
                    Text = @"
Hello, "+UFN+@",

This is an automated message giving you the monthly personal report.
You can view your report in the attached .pdf file.

Please do not reply to this automated message.
Thank you!
"
                };

                // attach the pdf file
                var attachment = new MimePart("pdf", "pdf")
                {
                    Content = new MimeContent(File.OpenRead(FP + FN), ContentEncoding.Default),
                    ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
                    ContentTransferEncoding = ContentEncoding.Base64,
                    FileName = FN
                };

                // create the multipart/mixed container to hold the message text and the image attachment
                var multipart = new Multipart("mixed");
                multipart.Add(body);
                multipart.Add(attachment);

                // now set the multipart/mixed as the message body
                message.Body = multipart;

                //and ... send
                using (SmtpClient smtpClient = new SmtpClient())
                {
                    smtpClient.ServerCertificateValidationCallback = (s, c, h, e) => true;
                    smtpClient.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                    smtpClient.Authenticate("pr0princ3@gmail.com", "Pivelinov-1");
                    smtpClient.Send(message);
                    smtpClient.Disconnect(true);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Sending the message." + ex, "Error");
            }
        }

        private void SaveCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (SaveCheckBox.Checked == true)
                ViewCheckBox.Checked = false;
        }

        private void ViewCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (ViewCheckBox.Checked == true)
                SaveCheckBox.Checked = false;
        }

        private void MailCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (MailCheckBox.Checked == true)
            {
                SaveCheckBox.Checked = true;
                ViewCheckBox.Checked = false;
                ViewCheckBox.Enabled = false;
            }
            else if (MailCheckBox.Checked == false)
            {
                if (comboBox1.Text == "*" || comboBox1.Text == "")
                {
                    ViewCheckBox.Enabled = false;
                    SaveCheckBox.Checked = true;
                }
                else
                {
                    SaveCheckBox.Checked = false;
                    ViewCheckBox.Checked = false;
                    ViewCheckBox.Enabled = true;
                }
            }
        }
    }
}
