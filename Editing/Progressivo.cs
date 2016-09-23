using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Bel3
{
    public partial class Progressivo : Form
    {
        private connectoToMySql mConnection;


        public Progressivo(connectoToMySql mConnection)
        {
            // TODO: Complete member initialization
            this.mConnection = mConnection;
            InitializeComponent();
            if (mConnection!=null)
            {
                if (mConnection.cnMySQL.State != ConnectionState.Closed)
                    mConnection.cnMySQL.Close();

                if (mConnection.cnMySQL.State != ConnectionState.Open)
                    mConnection.cnMySQL.Open();

                mConnection.cmdMySQL = new MySqlCommand("select * from progressivi", mConnection.cnMySQL);
                mConnection.reader = mConnection.cmdMySQL.ExecuteReader();

                while (mConnection.reader.Read())
                {
                    numericUpDown1.Value = Convert.ToInt16(mConnection.reader.GetValue(1).ToString());
                    numericUpDown2.Value = Convert.ToInt16(mConnection.reader.GetValue(0).ToString());
                    numericUpDown3.Value = Convert.ToInt16(mConnection.reader.GetValue(2).ToString());
                    tds.Value = Convert.ToInt16(mConnection.reader.GetValue(4).ToString());
                }
                mConnection.cnMySQL.Close();
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            
            if (mConnection != null)
            {
                if (MessageBox.Show("Sei sicuro di voler proseguire?", "Attenzione!!!", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    if (mConnection.cnMySQL.State != ConnectionState.Closed)
                        mConnection.cnMySQL.Close();

                    if (mConnection.cnMySQL.State != ConnectionState.Open)
                        mConnection.cnMySQL.Open();
                    mConnection.cmdMySQL = mConnection.cnMySQL.CreateCommand();

                    mConnection.cmdMySQL.CommandText = "UPDATE progressivi SET Ricevute=" + numericUpDown1.Value + ",Fatture=" + numericUpDown2.Value + ",notecredito=" + numericUpDown3.Value + ",tds="+tds.Value+" ";

                    mConnection.cmdMySQL.ExecuteNonQuery();

                    mConnection.cnMySQL.Close();
                }
            }
        }

        private void Progressivo_Load(object sender, EventArgs e)
        {

        }


    }
}
