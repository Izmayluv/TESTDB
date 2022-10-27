using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string[,] arrayDiscount = new string[,]
            {
                { "0", "100" },
                { "0", "10" },
                { "10", "15" },
                { "15", "100" }
            };

            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    textBox1.Text += arrayDiscount[j, i];
                }
                textBox1.Text += "\n"+
                Environment.NewLine;
            }
        }
    }
}
