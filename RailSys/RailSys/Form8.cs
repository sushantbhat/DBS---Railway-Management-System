using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using System.IO;

using System.Web;
using System.Net.Mail;

namespace RailSys
{
    public partial class Form8 : Form
    {
        OracleConnection conn;
        OracleCommand comm;
        OracleDataAdapter da;

        DataSet ds;

        DataTable dt;

        DataRow dr;
        string uname;
        int id;

        public void connect1()
        {
            string oradb = "Data Source=SUSHANTBHAT;User ID=system;Password=mamatabhat";

            conn = new OracleConnection(oradb); // C#
            conn.Open();

        }

        private void connectToDb()
        {
            try
            {
                connect1();
                comm = new OracleCommand();
                comm.CommandText = "select username from users where userid = " + id;
                comm.CommandType = CommandType.Text;
                ds = new DataSet();
                da = new OracleDataAdapter(comm.CommandText, conn);
                da.Fill(ds, "users");
                dt = ds.Tables["users"];
               
                
                dr = dt.Rows[0];
                uname = dr["username"].ToString();
                
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public Form8(int travelid , int cost , string fromst , string tost , int uid )
        {
            InitializeComponent();
            id = uid;
            connectToDb();
            String newline = "\n";

            richTextBox1.Text = "*****************TICKET*******************";

            richTextBox1.Text += newline;
            richTextBox1.Text += newline;
            richTextBox1.Text += "NAME ";
            richTextBox1.Text += "                : ";
            richTextBox1.Text += uname;

            richTextBox1.Text += newline;
            richTextBox1.Text += newline;
            richTextBox1.Text += "USER ID ";
            richTextBox1.Text += "            : ";
            richTextBox1.Text += uid;

            richTextBox1.Text += newline;
            richTextBox1.Text += newline;
            richTextBox1.Text += "TRAVELLING FROM ";
            richTextBox1.Text += "    : ";
            richTextBox1.Text += fromst;

            richTextBox1.Text += newline;
            richTextBox1.Text += newline;
            richTextBox1.Text += "TRAVELLING TO ";
            richTextBox1.Text += "      : ";
            richTextBox1.Text += tost;

            richTextBox1.Text += newline;
            richTextBox1.Text += newline;
            richTextBox1.Text += "TOTAL AMOUNT ";
            richTextBox1.Text += "       : ";
            richTextBox1.Text += cost;

            richTextBox1.Text += newline;
            richTextBox1.Text += newline;
            richTextBox1.Text += "TRAVEL ID ";
            richTextBox1.Text += "          : ";
            richTextBox1.Text += travelid;

            richTextBox1.Text += newline;
            richTextBox1.Text += newline;
            richTextBox1.Text += "******************END********************";

            richTextBox1.ReadOnly = true;
            richTextBox1.BackColor = System.Drawing.SystemColors.Window;
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox2.Text.Length == 0)
                {
                    MessageBox.Show("invalid entries");
                    return;

                }
                MailMessage mail = new MailMessage("bhatsushant507@gmail.com", textBox2.Text, "ticket details", richTextBox1.Text);
                SmtpClient client = new SmtpClient("smtp.gmail.com");
                client.Port = 587;
                client.Credentials = new System.Net.NetworkCredential("bhatsushant507@gmail.com", "subrayvbhat");
                client.EnableSsl = true;
                client.Send(mail);
                MessageBox.Show("Email has been sent to : " + textBox2.Text);
                Form1 frm = new Form1();
                frm.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show("email not sent");
                Form1 frm = new Form1();
                frm.Show();
                this.Hide();
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                saveFileDialog1.ShowDialog();
                string fName = saveFileDialog1.FileName;
                StreamWriter sw = new StreamWriter(fName);
                sw.Write(richTextBox1.Text);
                sw.Flush();
                sw.Close();
            }
            catch (Exception ex)
            {
            }
        }

        private void fontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FontDialog fd = new FontDialog();
            fd.Font = richTextBox1.SelectionFont;
            fd.Color = richTextBox1.SelectionColor;
            if (fd.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.SelectionFont = fd.Font;
                richTextBox1.SelectionColor = fd.Color;
            }
        }
    }
}
