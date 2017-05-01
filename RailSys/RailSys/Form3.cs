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
//using System.Data.OracleClient;


namespace RailSys
{
    public partial class Form3 : Form
    {
        OracleConnection conn;
        OracleCommand comm;
        OracleDataAdapter da;
        DataSet ds;
        DataTable dt;
        DataRow dr;
        int i = 0;
        public Form3()
        {
            InitializeComponent();
            textBox3.Visible = false;
        }
        public void connect1()
        {
            string oradb = "Data Source=SUSHANTBHAT;User ID=system;Password=mamatabhat";

            conn = new OracleConnection(oradb); // C#
            conn.Open();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            
            
            if (checkBox1.Checked == true)
            {
                
                connectToDb2();
            }
            else
            {
                connectToDb();
            }
        }

        private void connectToDb2()
        {
            try
            {
                if (textBox3.Text != "rail@123")
                {
                    MessageBox.Show("enter correct access code");
                    return;
                }
                connect1();
                OracleCommand cm = new OracleCommand();
                cm.Connection = conn;
                cm.CommandText = "pasw_check";
                cm.CommandType = CommandType.StoredProcedure;
                OracleParameter retval = new OracleParameter("myretval", OracleDbType.Varchar2, 50);
                retval.Direction = ParameterDirection.ReturnValue;
                cm.Parameters.Add(retval);

                cm.Parameters.Add(new OracleParameter(textBox1.Text, OracleDbType.Int32)).Value = textBox1.Text;
                cm.ExecuteNonQuery();
                conn.Close();
                string psw = retval.Value.ToString();
                string pasw;
                pasw = textBox2.Text;
                if (psw == pasw)
                {
                    Form9 frm = new Form9(textBox1.Text);
                    frm.Show();
                    this.Hide();
                }
                else if(psw != pasw)
                {
                    label1.Text = "incorrect userid/password";
                }
            }
            catch (Exception e)
            {
                label1.Text = "incorrect userid/password";
            }
            
        }

        private void connectToDb()
        {
            /*using (OracleConnection conn = new OracleConnection("Data Source=SUSHANTBHAT;User ID=system;Password=mamatabhat"))
            {*/try
            {
                connect1();
                OracleCommand cm = new OracleCommand();
                cm.Connection = conn;
                cm.CommandText = "pasw_check";
                cm.CommandType = CommandType.StoredProcedure;
               OracleParameter retval = new OracleParameter("myretval",OracleDbType.Varchar2 , 50);
               retval.Direction = ParameterDirection.ReturnValue;
               cm.Parameters.Add(retval);
               
               cm.Parameters.Add(new OracleParameter(textBox1.Text , OracleDbType.Int32)).Value = textBox1.Text;
               cm.ExecuteNonQuery();
               conn.Close();
               string psw = retval.Value.ToString();
               string pasw;
               pasw = textBox2.Text;
               if (psw == pasw)
               {
                   Form5 frm = new Form5(textBox1.Text);
                   frm.Show();
                   this.Hide();
               }
               else
               {
                   label1.Text = "incorrect userid/password";
               }
            }
               catch (Exception e)
               {
                   label1.Text = "incorrect userid/password";
               }
                

            }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form1 frm = new Form1();
            this.Hide();
            frm.Show();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                textBox3.Visible = true;
            }
            else textBox3.Visible = false;
        }
        }/*
            try
            {
                connect1();
                int usrid;
                string pasw;
                pasw = textBox2.Text;
                Int32.TryParse(textBox1.Text, out usrid);
                comm = new OracleCommand();
                comm.CommandText = "select * from users where userid = " + usrid;
                comm.CommandType = CommandType.Text;
                ds = new DataSet();
                da = new OracleDataAdapter(comm.CommandText, conn);
                da.Fill(ds, "users");
                dt = ds.Tables["users"];
                dr = dt.Rows[i];
                string psw = dr["password"].ToString();
                if (psw == pasw)
                {
                    Form5 frm = new Form5();
                    frm.Show();
                    this.Hide();
                }
                else
                {
                    label1.Text = "incorrect userid/password";
                }
                conn.Close();
            }
            catch (Exception e)
            {
                label1.Text = "incorrect userid/password";
            }
        
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form1 frm = new Form1();
            frm.Show();
            this.Hide();
        }
        */

        
}

