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
    public partial class Form4 : Form
    {
        OracleConnection conn;
        OracleCommand comm;
        OracleDataAdapter da;
        DataSet ds;
        DataTable dt;
        DataRow dr;
        int index = 0;
        public Form4()
        {
            InitializeComponent();
        }

        int[ , ] dis = new int[11 , 11];
        


        private void tableSetup()
        {
            for (int i = 0; i < 11; i++)
            {
                for (int j = 0; j < 11; j++)
                {
                    if (i == 0 && j == 0)
                    {
                        dis[i , j] = 0;
                        
                    }
                    else if (i == 0)
                    {
                        dis[0 , j] = 100 + j;
                    }
                    else if (j == 0)
                    {
                        dis[i , 0] = 100 + i;
                    }
                    else dis[i , j] = 999;
                }
            }
        }
        public void connect1()
        {
            string oradb = "Data Source=SUSHANTBHAT;User ID=system;Password=mamatabhat";

            conn = new OracleConnection(oradb); // C#
            conn.Open();

        }

        
        int st1;
        string stn1;
        string stn2;
        int st2;
        int len;
        string lgnt;
        int total;

        private void connectToDb()
        {
            connect1();
            comm = new OracleCommand();
            comm.CommandText = "select * from connects";
            comm.CommandType = CommandType.Text;
            ds = new DataSet();
            da = new OracleDataAdapter(comm.CommandText, conn);
            da.Fill(ds, "connects");
            dt = ds.Tables["connects"];
            total = dt.Rows.Count;
            
            
            conn.Close();

        }

        

        void dijkstra()
        {
            tableSetup();
            retrieveData();
            sampleOutput();
            implementAlgo();
        }

        int[] find = new int[11];
        int[] safe = new int[11];

        private void implementAlgo()
        {
            string inpt = comboBox1.Text;
            int inptStn ;
            Int32.TryParse(inpt , out inptStn);
            inptStn -= 100;
	        
	        for(int i = 1 ; i<11 ; i++){
	    	    if(inptStn != i){
	    		    find[i]  = dis[inptStn , i];
	    	    }
	    	 }
	        for(int i=0 ; i<11 ; i++)
	    	    System.Console.Write(find[i]+"\t");
		    System.Console.Write('\n');
	    
		    for(int k=1 ; k<=9 ; k++)
            {
		        int min = 999;
		        int pos = 0;
		        for(int i=1 ; i<11 ; i++)
                {
		    	    if(i!=inptStn && safe[i] == 0)
                    {
		    		    if(min>find[i])
                        {
		    			    min = find[i];
		    			    pos = i;
		    		    }
		    	    }
		        }
		        safe[pos] = find[pos];
		    
		        for(int i=1 ; i<11 ; i++)
                {
		    	    if(i!=inptStn && safe[i] == 0)
                    {
		    		    for(int j=1 ; j<11 ; j++)
                        {
		    			    int dist = 0;
		    			    if(j!=inptStn && safe[j]!=0 && i != j)
                            {
		    				    if(dis[i,j] != 999){
		    					    dist += dis[i,j];
		    					    dist += safe[j];
		    					    if(dist < find[i]){
		        					    find[i] = dist;
		        				    }
		    				    }
		    				
		    			    }
		    		    }
		    	    }
		        }
		    
		        for(int i=0 ; i<11 ; i++){
		    	    System.Console.Write(safe[i] + "\t");
		        }
		        System.Console.Write('\n');
		        for(int i=0 ; i<11 ; i++){
		    	    System.Console.Write(find[i] + "\t");
		        }
		        System.Console.Write("\n\n\n");
	        }

        }

        private void retrieveData()
        {
            for (index = 0; index < total; index++)
            {
                dr = dt.Rows[index];
                stn1 = dr["station1"].ToString();
                stn2 = dr["station2"].ToString();
                lgnt = dr["length"].ToString();
                Int32.TryParse(lgnt , out len);
                Int32.TryParse(stn1, out st1);
                Int32.TryParse(stn2, out st2);
                //System.Console.Write(st1 + " " + st2);
                for (int i = 0; i < 11; i++)
                {
                    for (int j = 0; j < 11; j++)
                    {
                        if (i + 100 == st1 && j + 100 == st2)
                        {
                            dis[i, j] = len;
                            dis[j, i] = len;
                        }
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox5.Text = " ";
            for (int i = 0; i < 11; i++)
            {
                find[i] = 0;
                safe[i] = 0;
            }
            connectToDb();
            dijkstra();
            dispResult();

        }

        private void dispResult()
        {
            int input2;
            string inpt2 = comboBox2.Text;
            Int32.TryParse(inpt2, out input2);
            input2 -= 100;
            textBox5.Text = safe[input2].ToString();
        }

        private void sampleOutput()
        {
            for (int i = 0; i < 11; i++)
            {
                for (int j = 0; j < 11; j++)
                {
                    System.Console.Write(dis[i , j] + " " + " " + " " );
                }
                System.Console.Write('\n');
            }
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            for(int item = 101 ; item <= 110 ; item++){
                comboBox1.Items.Add(item);
                comboBox2.Items.Add(item);
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
