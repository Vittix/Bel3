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
    public partial class Operatori : Form
    {
        connectoToMySql mConnection;

        public Operatori(connectoToMySql mCon)
        {
            mConnection = mCon;
            InitializeComponent();
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
                mConnection.cnMySQL.Open();
                mConnection.cmdMySQL = new MySqlCommand("select * from operatori Order by nome ASC", mConnection.cnMySQL);

                //
                // 4. Use the connection
                //

                // get query results
                mConnection.reader = mConnection.cmdMySQL.ExecuteReader();

                // print the CustomerID of each record
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

        private void Refresh_Click(object sender, EventArgs e)
        {
            refreshlist();
        }

        private void Operatori_Load(object sender, EventArgs e)
        {
            refreshlist();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Add_Click_1(object sender, EventArgs e)
        {
            if (mConnection != null)
            {
                if(mConnection.cnMySQL.State!=ConnectionState.Open)
                mConnection.cnMySQL.Open();
                mConnection.cmdMySQL = mConnection.cnMySQL.CreateCommand();
               
                    mConnection.cmdMySQL.CommandText = "INSERT INTO operatori (nome) VALUES('" + roomName.Text + "')";
                    mConnection.cmdMySQL.ExecuteNonQuery();
                
                mConnection.cnMySQL.Close();
                refreshlist();

                MessageBox.Show("L'operatore è stato inserito con successo!",
                "Fatto!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        private void Modify_Click(object sender, EventArgs e)
        {
            if (mConnection != null)
            {
                
                    mConnection.cnMySQL.Open();
                    mConnection.cmdMySQL = mConnection.cnMySQL.CreateCommand();

                    mConnection.cmdMySQL.CommandText = "UPDATE operatori SET nome = '" + roomName.Text + "' WHERE operatori.nome = '" + ListaStanze.SelectedItem.ToString() + "'";
                    mConnection.cmdMySQL.ExecuteNonQuery();

                    mConnection.cnMySQL.Close();
                    refreshlist();

                    MessageBox.Show("L'operatore è stato modificato con successo!",
                    "Fatto!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
               
            }
        }

        private void ListaStanze_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (mConnection != null)
            {
                mConnection.cnMySQL.Open();
                mConnection.cmdMySQL = new MySqlCommand("select * from operatori", mConnection.cnMySQL);

                //
                // 4. Use the connection
                //

                // get query results
                mConnection.reader = mConnection.cmdMySQL.ExecuteReader();

                // print the CustomerID of each record
                while (mConnection.reader.Read())
                {
                    if (ListaStanze.SelectedItems.Count < 2)
                        if (mConnection.reader.GetValue(1).ToString() == ListaStanze.SelectedItem.ToString())
                        {
                            roomName.Text = mConnection.reader.GetValue(1).ToString();
                            //roomSize.Text = mConnection.reader.GetValue(2).ToString();
                            // for (int i = 0; i < mConnection.reader.FieldCount; i++)
                            //   thisrow += mConnection.reader.GetValue(i).ToString() + ",";
                        }

                }
                mConnection.cnMySQL.Close();
            }
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            if (mConnection != null)
            {
                if (mConnection.cnMySQL.State == ConnectionState.Open)
                {
                    mConnection.cnMySQL.Close();
                }
                mConnection.cnMySQL.Open();
                mConnection.cmdMySQL = mConnection.cnMySQL.CreateCommand();
                if (ListaStanze.SelectedItems.Count < 2)
                {
                    mConnection.cmdMySQL.CommandText = "DELETE FROM operatori WHERE nome = '" + ListaStanze.SelectedItem.ToString() + "'";
                    mConnection.cmdMySQL.ExecuteNonQuery();
                }
                else
                {
                    for (int i = 0; i < ListaStanze.SelectedItems.Count; i++)
                    {
                        mConnection.cmdMySQL.CommandText = "DELETE FROM operatori WHERE nome = '" + ListaStanze.SelectedItems[i].ToString() + "'";
                        mConnection.cmdMySQL.ExecuteNonQuery();
                    }
                }
                mConnection.cnMySQL.Close();
                refreshlist();


                MessageBox.Show("L'operatore è stato eliminato con successo!",
                "Fatto!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

      
        

      

      
    }
}
