using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RailSys
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
           /* this.BackgroundImage = Properties.Resources.image1;

            

            Timer tm = new Timer();
            tm.Interval = 1000;
            tm.Tick += new EventHandler(changeImage);
            tm.Start();*/
        }

        private void changeImage(object sender, EventArgs e)
        {
            List<Bitmap> bi = new List<Bitmap>();
            bi.Add(Properties.Resources.image1);
            bi.Add(Properties.Resources.image2);
            bi.Add(Properties.Resources.image3);
            bi.Add(Properties.Resources.image4);
            int index = DateTime.Now.Second % 4;
            this.BackgroundImage = bi[index];

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form2 frm = new Form2();
            frm.Show();
            this.Hide();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form3 frm = new Form3();
            this.Hide();
            frm.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form4 frm = new Form4();
            frm.Show();
            this.Hide();
        }
    }
}
