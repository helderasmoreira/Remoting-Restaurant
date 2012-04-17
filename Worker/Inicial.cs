using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Worker
{
    public partial class Inicial : Form
    {
        public int op;
        public Inicial(ref int op)
        {
            InitializeComponent();
            this.op = op;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            op = 1;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            op = 2;
            this.Close();
        }

        
    }
}
