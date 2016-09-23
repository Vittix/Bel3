using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Bel3.Gest
{
    public partial class Form2 : Form
    {
        
        public Label label { get; set; }
        public Form2()
        {
            InitializeComponent();
            label = label1;
        }

        private void label1_Click(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            this.Focus();
        }
    }
}
