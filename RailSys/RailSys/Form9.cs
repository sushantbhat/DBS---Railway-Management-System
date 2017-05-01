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
    public partial class Form9 : Form
    {
        OracleConnection conn;
        OracleCommand comm;
        OracleDataAdapter da;
        DataSet ds;
        DataTable dt;
        DataRow dr;

        int aid;
        public Form9(String aid)
        {
            InitializeComponent();
            textBox1.ReadOnly = true;
            textBox1.BackColor = System.Drawing.SystemColors.Window;
            Int32.TryParse(aid, out this.aid);
            connectTOdB();
        }

        public void connect1()
        {
            string oradb = "Data Source=SUSHANTBHAT;User ID=system;Password=mamatabhat";

            conn = new OracleConnection(oradb); // C#
            conn.Open();

        }

        private void connectTOdB()
        {
            connect1();
            comm = new OracleCommand();
            comm.CommandText = "select * from connects c where c.routeId in (select routeId from monitors m where m.authorityId = " + aid + " )";
            comm.CommandType = CommandType.Text;
            ds = new DataSet();
            da = new OracleDataAdapter(comm.CommandText, conn);
            da.Fill(ds, "connects");
            dt = ds.Tables["connects"];
           
            
            dr = dt.Rows[0];
            label2.Text = dr["station1"].ToString();
            label3.Text = dr["station2"].ToString();
            label4.Text = dr["routeId"].ToString();
            label5.Text = dr["length"].ToString();
            conn.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                connect1();
                OracleCommand cm = new OracleCommand();
                cm.Connection = conn;
                cm.CommandText = "age_count";
                cm.CommandType = CommandType.StoredProcedure;
                OracleParameter retval = new OracleParameter("myretval", OracleDbType.Int32, 50);
                retval.Direction = ParameterDirection.ReturnValue;
                cm.Parameters.Add(retval);

                
                cm.ExecuteNonQuery();
                conn.Close();
                textBox1.Text = retval.Value.ToString();
                
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form1 frm = new Form1();
            frm.Show();
            this.Hide();
        }
    }
}
