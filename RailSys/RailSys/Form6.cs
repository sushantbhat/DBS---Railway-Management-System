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
    public partial class Form6 : Form
    {
        OracleConnection conn;
        OracleCommand comm;
        OracleDataAdapter da;
        OracleDataAdapter da2;
        DataSet ds;
        DataSet ds2;
        DataTable dt;
        DataTable dt2;
        DataRow dr;
        DataRow dr2;
        DataRow dr3;

        int Id;
        String name;
        int usrid;
        int age;
        String fromSt;
        String toSt;
        int st1;
        int st2;
        int totalLength = 0;

        double cost = 0;
        double ac2tier = 2;
        double ac3tier = 1.75;
        double sleeper = 1;
        double general = .33;

        public Form6(int trainID , String trainName , int usrid , String station1 , String station2)
        {
            this.usrid = usrid;
            Id = trainID;
            name = trainName;
            fromSt = station1;
            toSt = station2;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length == 0 || textBox2.Text.Length != 10)
            {
                MessageBox.Show("invalid entries");
                return;
            }


            int tempst;
            mapId();
            connect1();
            int i = 0;
            cost = 0;
            totalLength = 0;
            Int32.TryParse(textBox1.Text, out age);
            int tempid1;
            int tempid2;
            comm = new OracleCommand();
            if (st1 > st2)
            {
                tempst = st1;
                st1 = st2;
                st2 = tempst;
            }
            comm.CommandText = "with station1s(st1) as (select station1 from connects where station2 in (select stationId from arrives where stationId between " + st1 + " and " + st2 + " and trainId = " + Id + ") group by station1 having count(*) > 1) , station2s(st2) as (select station2 from connects where station1 in (select stationId from arrives where stationId between " + st1 + " and " + st2 + " and trainId = " + Id + ") group by station2 having count(*) > 1) select distinct stationId from arrives where (stationId between " + st1 + " and " + st2 + " or stationId in (select st1 from station1s) or stationId in (select st2 from station2s) ) and trainId = " + Id + " order by movementId asc";
            comm.CommandType = CommandType.Text;
            ds = new DataSet();
            da = new OracleDataAdapter(comm.CommandText, conn);
            da.Fill(ds, "arrives");
            dt = ds.Tables["arrives"];
            int t = dt.Rows.Count;
            conn.Close();
            for (i = 0; i < t - 1; i++)
            {
                dr = dt.Rows[i];
                dr2 = dt.Rows[i + 1];
                Int32.TryParse(dr["stationId"].ToString(), out tempid1);
                Int32.TryParse(dr2["stationId"].ToString(), out tempid2);
                System.Console.Write(dr["stationId"].ToString() + " ");
                if (tempid1 < tempid2)
                    totalLength += findLength(tempid1, tempid2);
                else
                    totalLength += findLength(tempid2, tempid1);

            }
            calculateCost();

            tempst = st1;
            st1 = st2;
            st2 = tempst;

            int fcost = (int) Math.Floor(cost);
            MessageBox.Show("amount payable is : " + fcost + " Rs");
            Form7 fmr = new Form7(usrid , age , textBox2.Text , Id , st1 , st2 , comboBox1.Text , fcost , fromSt , toSt);
            fmr.Show();
            this.Hide();
        }

        public void connect1()
        {
            string oradb = "Data Source=SUSHANTBHAT;User ID=system;Password=mamatabhat";

            conn = new OracleConnection(oradb); // C#
            conn.Open();

        }


        

        private void Form6_Load(object sender, EventArgs e)
        {
            label2.Text = name;
            label3.Text = Id.ToString();
            label4.Text = usrid.ToString();
            label13.Text = fromSt;
            label14.Text = toSt;
            initializeVariables();
        }

        private void initializeVariables()
        {
            comboBox1.Items.Add("AC 2 TIER");
            comboBox1.Items.Add("AC 3 TIER");
            comboBox1.Items.Add("SLEEPER ");
            comboBox1.Items.Add("GENERAL ");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length == 0 || textBox2.Text.Length == 0)
            {
                MessageBox.Show("invalid entries");
                return;
            }
            
            
            int tempst;
            mapId();
            connect1();
            int i = 0;
            cost = 0;
            totalLength = 0;
            Int32.TryParse(textBox1.Text, out age);
            int tempid1;
            int tempid2;
            comm = new OracleCommand();
            if (st1 > st2)
            {
                tempst = st1;
                st1 = st2;
                st2 = tempst;
            }
            comm.CommandText = "with station1s(st1) as (select station1 from connects where station2 in (select stationId from arrives where stationId between " + st1 + " and " + st2 + " and trainId = " + Id + ") group by station1 having count(*) > 1) , station2s(st2) as (select station2 from connects where station1 in (select stationId from arrives where stationId between " + st1 + " and " + st2 + " and trainId = " + Id + ") group by station2 having count(*) > 1) select distinct stationId from arrives where (stationId between " + st1 + " and " + st2 + " or stationId in (select st1 from station1s) or stationId in (select st2 from station2s) ) and trainId = " + Id + " order by movementId asc";
            comm.CommandType = CommandType.Text;
            ds = new DataSet();
            da = new OracleDataAdapter(comm.CommandText, conn);
            da.Fill(ds, "arrives");
            dt = ds.Tables["arrives"];
            int t = dt.Rows.Count;
            conn.Close();
            for (i = 0; i < t - 1; i++)
            {
                dr = dt.Rows[i];
                dr2 = dt.Rows[i + 1];
                Int32.TryParse(dr["stationId"].ToString(), out tempid1);
                Int32.TryParse(dr2["stationId"].ToString(), out tempid2);
                System.Console.Write(dr["stationId"].ToString() + " ");
                if (tempid1 < tempid2)
                    totalLength += findLength(tempid1, tempid2);
                else
                    totalLength += findLength(tempid2, tempid1);

            }
            calculateCost();

            tempst = st1;
            st1 = st2;
            st2 = tempst;

            int fcost = (int)Math.Floor(cost);
            MessageBox.Show("amount payable is : " + fcost + " Rs");
        }

        private void calculateCost()
        {
            
            if (comboBox1.Text == "AC 2 TIER")
            {
                cost = ac2tier;
            }
            else if (comboBox1.Text == "AC 3 TIER")
            {
                cost = ac3tier;
            }
            else if (comboBox1.Text == "SLEEPER ")
            {
                cost = sleeper;
            }
            else if (comboBox1.Text == "GENERAL ")
            {
                cost = general;
            }
            cost *= totalLength;
        }

        int findLength(int id1, int id2)
        {
            connect1();
            try
            {
                int len;
                comm = new OracleCommand();
                comm.CommandText = "select length from connects where station1 = " + id1 + " and station2 = " + id2;
                comm.CommandType = CommandType.Text;
                ds2 = new DataSet();
                da2 = new OracleDataAdapter(comm.CommandText, conn);
                da2.Fill(ds2, "connects");
                dt2 = ds2.Tables["connects"];
                dr3 = dt2.Rows[0];
                Int32.TryParse(dr3["length"].ToString(), out len);
                //System.Console.Write(len);
                return len;
            }
            catch (Exception ex)
            {
                MessageBox.Show(id1 + " " +  id2 + " " + ex.ToString());
                return 0;
            }
        }

        private void mapId()
        {
            try
            {
                connect1();
                comm = new OracleCommand();
                comm.CommandText = "select stationId from station where stationName = '" + fromSt + "'";
                comm.CommandType = CommandType.Text;
                ds = new DataSet();
                da = new OracleDataAdapter(comm.CommandText, conn);
                da.Fill(ds, "station");
                dt = ds.Tables["station"];
                dr = dt.Rows[0];
                Int32.TryParse(dr["stationid"].ToString(), out st1);


                comm = new OracleCommand();
                comm.CommandText = "select stationId from station where stationName = '" + toSt + "'";
                comm.CommandType = CommandType.Text;
                ds = new DataSet();
                da = new OracleDataAdapter(comm.CommandText, conn);
                da.Fill(ds, "station");
                dt = ds.Tables["station"];
                dr = dt.Rows[0];
                Int32.TryParse(dr["stationid"].ToString(), out st2);
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        
    }
}
