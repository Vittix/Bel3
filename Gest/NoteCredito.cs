using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Bel3
{
    public partial class notcred : Form
    {
        connectoToMySql mConnection;
        int idcliente;
        public notcred(connectoToMySql mcon,int idcliente)
        {
            mConnection = mcon;
            this.idcliente = idcliente;
            InitializeComponent();
            
        }



        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                Fattura fat = new Fattura(mConnection, "Nota Credito", idcliente.ToString(), Convert.ToSingle(textBox1.Text), true,richTextBox1.Text,Convert.ToSingle(textBox2.Text));
                fat.Show();
            }
        }
    }
}
