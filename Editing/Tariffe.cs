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
    public partial class Tariffe : Form
    {
        private connectoToMySql mConnection;
        public Tariffe(connectoToMySql mCon)
        {
            mConnection = mCon;
            InitializeComponent();
            refreshlist();
        }

        void refreshlist()
        {
            ListaStanze.Items.Clear();
            readroom();
        }

        public void readroom()
        {
            if (mConnection != null)
            {
                if (mConnection.cnMySQL.State != ConnectionState.Closed)
                    mConnection.cnMySQL.Close();

                if (mConnection.cnMySQL.State != ConnectionState.Open)
                mConnection.cnMySQL.Open();

                mConnection.cmdMySQL = new MySqlCommand("select * from tariffe Order by nome ASC", mConnection.cnMySQL);            
                mConnection.reader = mConnection.cmdMySQL.ExecuteReader();
               
                while (mConnection.reader.Read())
                {
                    string thisrow = "";
                    thisrow = mConnection.reader.GetValue(0).ToString();

                    // for (int i = 0; i < mConnection.reader.FieldCount; i++)
                    //   thisrow += mConnection.reader.GetValue(i).ToString() + ",";            
                    ListaStanze.Items.Add(thisrow);
                }
                mConnection.cnMySQL.Close();
            }
        }

        private void Add_Click(object sender, EventArgs e)
        {
            if (mConnection != null)
            {
                if (mConnection.cnMySQL.State != ConnectionState.Closed)
                    mConnection.cnMySQL.Close();

                if (mConnection.cnMySQL.State != ConnectionState.Open)
                    mConnection.cnMySQL.Open();
                mConnection.cmdMySQL = mConnection.cnMySQL.CreateCommand();

                mConnection.cmdMySQL.CommandText = "INSERT INTO tariffe (nome,tariffa,iva) VALUES('" + name.Text + "'," + Convert.ToDecimal(prezzo.Text) + "," + Convert.ToDecimal(iva.Text)+")";
                mConnection.cmdMySQL.ExecuteNonQuery();

                mConnection.cnMySQL.Close();
                refreshlist();

                //MessageBox.Show("La fonte è stata inserita con successo!",
                //"Fatto!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            if (mConnection != null)
            {
                if (mConnection.cnMySQL.State != ConnectionState.Closed)
                    mConnection.cnMySQL.Close();

                if (mConnection.cnMySQL.State != ConnectionState.Open)
                    mConnection.cnMySQL.Open();
                mConnection.cmdMySQL = mConnection.cnMySQL.CreateCommand();
                if (ListaStanze.SelectedItems.Count < 2)
                {
                    mConnection.cmdMySQL.CommandText = "DELETE FROM tariffe WHERE nome = '" + ListaStanze.SelectedItem.ToString() + "'";
                    mConnection.cmdMySQL.ExecuteNonQuery();
                }
                else
                {
                    for (int i = 0; i < ListaStanze.SelectedItems.Count; i++)
                    {
                        mConnection.cmdMySQL.CommandText = "DELETE FROM tariffe WHERE nome = '" + ListaStanze.SelectedItems[i].ToString() + "'";
                        mConnection.cmdMySQL.ExecuteNonQuery();
                    }
                }
                mConnection.cnMySQL.Close();
                refreshlist();


                //MessageBox.Show("La fonte è stata eliminata con successo!",
                //"Fatto!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        private void Modify_Click(object sender, EventArgs e)
        {
            Delete_Click(sender, e);
            Add_Click(sender, e);
        }

        private void ListaStanze_SelectedIndexChanged(object sender, EventArgs e)
        {
             if (mConnection.cnMySQL.State != ConnectionState.Closed)
                    mConnection.cnMySQL.Close();

                if (mConnection.cnMySQL.State != ConnectionState.Open)
                mConnection.cnMySQL.Open();

                mConnection.cmdMySQL = new MySqlCommand("select * from tariffe Order by nome ASC", mConnection.cnMySQL);            
                mConnection.reader = mConnection.cmdMySQL.ExecuteReader();

                while (mConnection.reader.Read())
                {
                    name.Text = mConnection.reader.GetValue(0).ToString();
                    prezzo.Text = mConnection.reader.GetValue(1).ToString();
                    iva.Text = mConnection.reader.GetValue(2).ToString();
                }
                mConnection.cnMySQL.Close();
        }
    }
}
