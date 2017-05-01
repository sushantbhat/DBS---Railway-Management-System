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
    public partial class Form7 : Form
    {
        OracleConnection conn;
        OracleCommand comm;
        OracleDataAdapter da;

        DataSet ds;
 
        DataTable dt;
  
        DataRow dr;
      



        int passid;
        int age;
        String cno;
        int trnid;

        int travelId;
        int st1;
        int st2;

        String clas;
        int cost;

        string fromSt;
        string toSt;

        public Form7(int passid , int age , String cno , int trnid , int st1 , int st2 , String clas , int cost ,string stationName1 , string stationName2)
        {
            InitializeComponent();
            initializCOmbo();
            this.passid = passid;
            this.age = age;
            this.cno = cno;
            this.trnid = trnid;
            
            this.st1 = st1;
            this.st2 = st2;
            this.clas = clas;
            this.cost = cost;

            fromSt = stationName1;
            toSt = stationName2;
            label3.Text = cost.ToString();
            
        }

        private void initializCOmbo()
        {
            for (int i = 2016; i < 3000; i++)
            {
                comboBox1.Items.Add(i);
            }
            for (int i = 1; i < 13; i++)
            {
                comboBox2.Items.Add(i);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length == 0 || textBox2.Text.Length != 16 || textBox3.Text.Length == 0 || comboBox1.Text.Length == 0 || comboBox2.Text.Length == 0)
            {
                MessageBox.Show("invalid entries");
                return;
            }
            
            
            connectToDb();
        }
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
                comm.CommandText = "select * from travels";
                comm.CommandType = CommandType.Text;
                ds = new DataSet();
                da = new OracleDataAdapter(comm.CommandText, conn);
                da.Fill(ds, "travels");
                dt = ds.Tables["travels"];
                travelId = dt.Rows.Count;
                travelId++;
                OracleCommand cm = new OracleCommand();
                cm.Connection = conn;
                cm.CommandText = "insert into passenger values(" + passid + " , " + age + " , '" + cno + "' , " + trnid + ")";
                cm.CommandType = CommandType.Text;
                cm.ExecuteNonQuery();

                cm.Connection = conn;
                cm.CommandText = "insert into buys values(" + passid + " , '" + clas + "' , " + cost + ")";
                cm.CommandType = CommandType.Text;
                cm.ExecuteNonQuery();

                cm.Connection = conn;
                cm.CommandText = "insert into travels values(" + passid + " , " + travelId + " , " + st1 + " , " + st2 + ")";
                cm.CommandType = CommandType.Text;
                cm.ExecuteNonQuery();

                if (checkBox1.Checked == true)
                {
                    Form8 frm = new Form8(travelId, cost, fromSt, toSt, passid);
                    frm.Show();
                    this.Hide();
                }
                else
                {
                    Form1 frm = new Form1();
                    frm.Show();
                    this.Hide();
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }


        Boolean t1i = false;
        Boolean t2i = false;
        Boolean t3i = false;
        Boolean c1 = false;
        Boolean c2 = false;
       

        private void textBox1_MouseClick(object sender, EventArgs e)
        {
            if (!t1i)
            {
                progressBar1.Increment(25);
                t1i = true;
            }
        }

        private void textBox2_MouseEnter(object sender, EventArgs e)
        {
            
        }

        private void textBox3_MouseEnter(object sender, EventArgs e)
        {
           
        }

        private void textBox2_MouseClick(object sender, MouseEventArgs e)
        {
            if (!t2i)
            {
                progressBar1.Increment(25);
                t2i = true;
            }
            
        }

        private void textBox3_MouseClick(object sender, MouseEventArgs e)
        {
            if (!t3i)
            {
                progressBar1.Increment(25);
                t3i = true;
            }

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void comboBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (!c1)
            {
                progressBar1.Increment(12);
                c1 = true;
            }
        }

        private void comboBox2_MouseClick(object sender, MouseEventArgs e)
        {
            if (!c2)
            {
                progressBar1.Increment(13);
                c2 = true;
            }
        }

        
    }
}
