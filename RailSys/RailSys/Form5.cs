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
    public partial class Form5 : Form
    {
        OracleConnection conn;
        OracleCommand comm;
        OracleDataAdapter da;
        DataSet ds;
        DataTable dt;
        DataRow dr;
        int index = 0;
        int total;
        int total_stations;
        int srcHr, srcMin;
        int destHr, destMin;
        int srcStId, destStId;
        int usrid;
        public Form5(String usrid)
        {
            Int32.TryParse(usrid, out this.usrid);
            InitializeComponent();
        }
        public Form5()
        {
           
            InitializeComponent();
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            label1.Visible = false;
            label2.Visible = false;
            comboBox1.Visible = false;
            comboBox2.Visible = false;
            label6.Visible = false;
            label7.Visible = false;

            connectToDb2();

            for (int item = 0; item <= 23; item++)
            {
                comboBox1.Items.Add(item);
                comboBox2.Items.Add(item);
            }
            
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            initializeVariables();
            connectToDb();
        }

        private void initializeVariables()
        {
            dataGridView1.Rows.Clear();
            textBox1.Text = " ";
            //richTextBox1.Text = " ";
            if (radioButton2.Checked == true)
            {
                
                Int32.TryParse(comboBox1.Text, out srcHr);
                //Int32.TryParse(comboBox5.Text, out srcMin);
                Int32.TryParse(comboBox2.Text, out destHr);
                //Int32.TryParse(comboBox6.Text, out destMin);
            }
            else if (radioButton1.Checked == true)
            {
                srcHr = 0;
                srcMin = 0;
                destHr = 23;
                destMin = 59;
            }

            fetchStationId();
  
        }

        private void fetchStationId()
        {
            connect1();
            comm = new OracleCommand();
            comm.CommandText = " select * from station where stationName = '" + comboBox3.Text + "'";
            comm.CommandType = CommandType.Text;
            ds = new DataSet();
            da = new OracleDataAdapter(comm.CommandText, conn);
            da.Fill(ds, "station");
            dt = ds.Tables["station"];
            dr = dt.Rows[0];
            Int32.TryParse(dr["stationId"].ToString() , out srcStId);

            comm = new OracleCommand();
            comm.CommandText = " select * from station where stationName = '" + comboBox4.Text + "'";
            comm.CommandType = CommandType.Text;
            ds = new DataSet();
            da = new OracleDataAdapter(comm.CommandText, conn);
            da.Fill(ds, "station");
            dt = ds.Tables["station"];
            dr = dt.Rows[0];
            Int32.TryParse(dr["stationId"].ToString(), out destStId);

            
            conn.Close();
        }

        private void connectToDb2()
        {
            connect1();
            comm = new OracleCommand();
            comm.CommandText = " select * from station";
            comm.CommandType = CommandType.Text;
            ds = new DataSet();

            da = new OracleDataAdapter(comm.CommandText, conn);
            da.Fill(ds, "station");


            dt = ds.Tables["station"];
            total_stations = dt.Rows.Count;
            for (int index = 0; index < total_stations; index++)
            {

                dr = dt.Rows[index];
                comboBox3.Items.Add(dr["stationName"].ToString());
                comboBox4.Items.Add(dr["stationName"].ToString());
            }

            conn.Close();
        }

        public void connect1()
        {
            string oradb = "Data Source=SUSHANTBHAT;User ID=system;Password=mamatabhat";

            conn = new OracleConnection(oradb); // C#
            conn.Open();

        }

        string[,] ids;

        private void connectToDb()
        {
            connect1();
            comm = new OracleCommand();
            comm.CommandText = " select * from arrives a where stationId = " + srcStId + " and arrivalHr between " + srcHr + " and " + (destHr - 1) + " and arrivalMin between 0 and 59 and a.trainId in (select trainId from arrives ar where stationId = " + destStId + " and ar.movementId > a.movementId)";
            comm.CommandType = CommandType.Text;
            ds = new DataSet();
            da = new OracleDataAdapter(comm.CommandText, conn);

            da.Fill(ds, "arrives");
            dt = ds.Tables["arrives"];
            total = dt.Rows.Count;
            ids = new string[3 , total];
            for(int index = 0 ; index<total ; index++)
            {
                dr = dt.Rows[index];
                ids[0 , index] = dr["trainId"].ToString();
                ids[1, index] = dr["arrivalHr"].ToString();
                ids[2, index] = dr["arrivalMin"].ToString();
            }
            conn.Close();
            for (int index = 0; index < total; index++)
            {
                string trnName = connectTodb3(ids[0, index]);
                dataGridView1.Rows.Add(ids[0, index], trnName ,  ids[1, index] + ":" + ids[2, index]);
                
                /*richTextBox1.Text += '\n';
                string trnName = connectTodb3(ids[0 , index]);
                richTextBox1.Text += ids[0 , index];
                richTextBox1.Text += "  ";
                richTextBox1.Text += trnName;
                richTextBox1.Text += "  ";
                richTextBox1.Text += ids[1 , index];
                richTextBox1.Text += ":";
                richTextBox1.Text += ids[2 , index];*/
                
            }
        }

        private string connectTodb3(string trnId)
        {
            connect1();
            int id;
            Int32.TryParse(trnId, out id);
            comm = new OracleCommand();
            comm.CommandText = " select * from train where trainId = " + id;
            comm.CommandType = CommandType.Text;
            ds = new DataSet();

            da = new OracleDataAdapter(comm.CommandText, conn);
            da.Fill(ds, "train");

            dt = ds.Tables["train"];
            dr = dt.Rows[0];

            string trnName = dr["trainName"].ToString();
            conn.Close();
            return trnName;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked == true)
            {
                label1.Visible = true;
                label2.Visible = true;
                comboBox1.Visible = true;
                comboBox2.Visible = true;
                label6.Visible = true;
                label7.Visible = true;
                //comboBox5.Visible = true;
                //comboBox6.Visible = true;
            }
            else
            {
                label1.Visible = false;
                label2.Visible = false;
                comboBox1.Visible = false;
                comboBox2.Visible = false;
                label6.Visible = false;
                label7.Visible = false;

             }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form1 frm = new Form1();
            frm.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
           
            if (textBox1.Text.Length == 0)
            {
                MessageBox.Show("select id of the train");
            }
            else
            {
                int trainId1;
                int trainId2;
                Int32.TryParse(textBox1.Text , out trainId1);
                String name = " ";
                Boolean found = false;

                for (int index = 0; index < total; index++)
                {
                    Int32.TryParse(ids[0, index], out trainId2);
                    if (trainId1 == trainId2)
                    {
                        name = connectTodb3(ids[0, index]);
                        MessageBox.Show(name);
                        found = true;
                        break;
                    }
                    
                }
                if (found)
                {
                    Form6 frm = new Form6(trainId1, name , usrid , comboBox3.Text , comboBox4.Text);
                    frm.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("please select id from displayed items");
                }
            }
        }
    }
}
