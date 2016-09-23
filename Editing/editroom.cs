using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql;
using MySql.Data.MySqlClient;
namespace Bel3
{
    public partial class editroom : Form
    {
        connectoToMySql mConnection;

        public editroom(connectoToMySql mCon)
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
                mConnection.cmdMySQL = new MySqlCommand("select * from camere Order by Name ASC", mConnection.cnMySQL);

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

       
     

      

        private void editroom_Load(object sender, EventArgs e)
        {
            refreshlist();
        }

       
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Add_Click_1(object sender, EventArgs e)
        {
            if (mConnection != null)
            {

                mConnection.cnMySQL.Open();
                mConnection.cmdMySQL = mConnection.cnMySQL.CreateCommand();
                if (a.Value > 0)
                {
                    for (int i = (int)da.Value; i <= a.Value; i++)
                    {
                        mConnection.cmdMySQL.CommandText = "INSERT INTO camere (Name) VALUES('" + roomName.Text + i + "')";
                        mConnection.cmdMySQL.ExecuteNonQuery();
                    }
                }
                else
                {
                    mConnection.cmdMySQL.CommandText = "INSERT INTO camere (Name) VALUES('" + roomName.Text + "')";
                    mConnection.cmdMySQL.ExecuteNonQuery();
                }
                mConnection.cnMySQL.Close();
                refreshlist();

                MessageBox.Show("La stanza è stata inserita con successo!",
                "Fatto!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        private void Modify_Click_1(object sender, EventArgs e)
        {
            if (mConnection != null)
            {
                if (ListaStanze.SelectedItems.Count > 0)
                {
                    mConnection.cnMySQL.Open();
                    mConnection.cmdMySQL = mConnection.cnMySQL.CreateCommand();

                    mConnection.cmdMySQL.CommandText = "UPDATE camere SET Name = '" + roomName.Text + "' WHERE camere.Name = '" + ListaStanze.SelectedItem.ToString() + "'";
                    mConnection.cmdMySQL.ExecuteNonQuery();

                    mConnection.cnMySQL.Close();
                    refreshlist();

                    MessageBox.Show("La stanza è stata modificata con successo!",
                    "Fatto!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
            }
        }

        private void ListaStanze_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (mConnection != null)
            {
                mConnection.cnMySQL.Open();
                mConnection.cmdMySQL = new MySqlCommand("select * from camere", mConnection.cnMySQL);

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
                    mConnection.cmdMySQL.CommandText = "DELETE FROM camere WHERE Name = '" + ListaStanze.SelectedItem.ToString() + "'";
                    mConnection.cmdMySQL.ExecuteNonQuery();
                }
                else
                {
                    for (int i = 0; i < ListaStanze.SelectedItems.Count; i++)
                    {
                        mConnection.cmdMySQL.CommandText = "DELETE FROM camere WHERE Name = '" + ListaStanze.SelectedItems[i].ToString() + "'";
                        mConnection.cmdMySQL.ExecuteNonQuery();
                    }
                }
                mConnection.cnMySQL.Close();
                refreshlist();


                MessageBox.Show("La stanza è stata eliminata con successo!",
                "Fatto!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
    }
}
