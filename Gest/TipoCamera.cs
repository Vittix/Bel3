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
    public partial class TipoCamera : Form
    {
        connectoToMySql mConnection;
        List<string> camere;
        public TipoCamera(connectoToMySql mcon, List<string> cam)
        {
            mConnection = mcon;
            camere = cam;
            InitializeComponent();
            if (camere.Count == 1)
            {

                if (mConnection.cnMySQL.State == ConnectionState.Closed)
                {
                    MySqlCommand cmd = new MySqlCommand(
                               "SELECT * FROM camere where Name='"+camere[0]+"';", mConnection.cnMySQL);
                    if (mConnection.cnMySQL.State != ConnectionState.Closed)
                        mConnection.cnMySQL.Close();
                    if (mConnection.cnMySQL.State != ConnectionState.Open)
                        mConnection.cnMySQL.Open();
                    MySqlDataReader dataReader = cmd.ExecuteReader();

                    // print the CustomerID of each record
                    // bool update = false;
                    while (dataReader.Read())
                    {
                        textBox1.Text = dataReader.GetString(2);
                        checkBox1.Checked = dataReader.GetBoolean(3);
                        checkBox2.Checked = dataReader.GetBoolean(4);
                    }

                    if (mConnection.cnMySQL.State != ConnectionState.Closed)
                        mConnection.cnMySQL.Close();

                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (string s in camere)
            {
                mConnection.cnMySQL.Open();
                mConnection.cmdMySQL = mConnection.cnMySQL.CreateCommand();

                mConnection.cmdMySQL.CommandText = "UPDATE camere SET tipo = '" + textBox1.Text + "',doccia = " + checkBox1.Checked + ",materassiseparati="+checkBox2.Checked+" WHERE camere.Name = '" + s + "'";
                mConnection.cmdMySQL.ExecuteNonQuery();

                mConnection.cnMySQL.Close();
              
              
            }
            MessageBox.Show("La stanza è stata modificata con successo!",
              "Fatto!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            this.Close();
        }

        private void TipoCamera_Load(object sender, EventArgs e)
        {

        }
    }
}
