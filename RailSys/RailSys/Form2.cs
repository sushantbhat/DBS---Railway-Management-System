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


namespace RailSys
{
    public partial class Form2 : Form
    {
        OracleConnection conn;
        OracleCommand comm;
        OracleDataAdapter da;
        DataSet ds;
        DataTable dt;
        DataRow dr;
        int i = 0;
        int total;
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        public void connect1()
        {
            string oradb = "Data Source=SUSHANTBHAT;User ID=system;Password=mamatabhat";
            
            conn = new OracleConnection(oradb); // C#
            conn.Open();

        }

        private void connectToDb()
        {
            connect1();
            comm = new OracleCommand();
            comm.CommandText = "select * from users";
            comm.CommandType = CommandType.Text;
            ds = new DataSet();
            da = new OracleDataAdapter(comm.CommandText, conn);
            da.Fill(ds, "users");
            dt = ds.Tables["users"];
            total = dt.Rows.Count;
            conn.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox3.Text.Length == 0)
            {
                MessageBox.Show("invalid credentials");
                return;
            }
            connectToDb();
            connect1();
            int userid = total + 1;
            OracleCommand cm = new OracleCommand();
            cm.Connection = conn;
            cm.CommandText = "insert into users values(" + userid + ",'" + textBox2.Text + "','" + textBox3.Text + "')";
            cm.CommandType = CommandType.Text;
            cm.ExecuteNonQuery();
            MessageBox.Show("account created your userid is " + userid);
            conn.Close();
            Form5 frm = new Form5();
            frm.Show();
            this.Hide();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form1 frm = new Form1();
            this.Hide();
            frm.Show();
        }
    }
}
