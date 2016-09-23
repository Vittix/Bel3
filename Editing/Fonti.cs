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
    public partial class Fonti : Form
    {
        connectoToMySql mConnection;
        string oldname;
        public Fonti(connectoToMySql mCon)
        {
            mConnection = mCon;
            InitializeComponent();
        }

        void refreshlist()
        {
            changecolor();
            ListaStanze.Items.Clear();
            readroom();
        }

        public void readroom()
        {
            if (mConnection != null)
            {
                mConnection.cnMySQL.Open();
                mConnection.cmdMySQL = new MySqlCommand("select * from fontiprenotazione Order by nome ASC", mConnection.cnMySQL);
                mConnection.reader = mConnection.cmdMySQL.ExecuteReader();
                while (mConnection.reader.Read())
                {
                    string thisrow = "";
                    if (mConnection.reader.GetValue(0).ToString() != "None")
                    {
                        thisrow = mConnection.reader.GetValue(0).ToString();
                        ListaStanze.Items.Add(thisrow);
                    }
                }
                mConnection.cnMySQL.Close();
            }
        }
        private void Changedindex(object sender, EventArgs e)
        {
            if ( !string.IsNullOrEmpty(ListaStanze.SelectedItem.ToString()))
            {
                roomName.Text = ListaStanze.SelectedItem.ToString();
                if (mConnection != null)
                {
                    mConnection.cnMySQL.Open();
                    mConnection.cmdMySQL = new MySqlCommand("select * from fontiprenotazione where nome='" + roomName.Text + "' Order by nome ASC", mConnection.cnMySQL);
                    mConnection.reader = mConnection.cmdMySQL.ExecuteReader();
                    while (mConnection.reader.Read())
                    {
                        numericUpDown1.Value = Convert.ToInt16(mConnection.reader.GetValue(1));
                        numericUpDown2.Value = Convert.ToInt16(mConnection.reader.GetValue(2));
                        numericUpDown3.Value = Convert.ToInt16(mConnection.reader.GetValue(3));
                    }
                    mConnection.cnMySQL.Close();
                }
            }

        }
       
        private void Refresh_Click(object sender, EventArgs e)
        {
            refreshlist();
        }

        private void Fonti_Load(object sender, EventArgs e)
        {
            refreshlist();
        }

        private void Add_Click(object sender, EventArgs e)
        {
            if (mConnection != null)
            {
                if (mConnection.cnMySQL.State != ConnectionState.Open)
                    mConnection.cnMySQL.Open();
                mConnection.cmdMySQL = mConnection.cnMySQL.CreateCommand();

                mConnection.cmdMySQL.CommandText = "INSERT INTO fontiprenotazione (nome,r,g,b) VALUES('" + roomName.Text + "'," + numericUpDown1.Value + "," + numericUpDown2.Value + "," + numericUpDown3.Value + ")";
                mConnection.cmdMySQL.ExecuteNonQuery();

                mConnection.cnMySQL.Close();
                refreshlist();

                //MessageBox.Show("La fonte è stata inserita con successo!",
                //"Fatto!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        private void Modify_Click(object sender, EventArgs e)
        {
            if (mConnection != null)
            {
                if (ListaStanze.SelectedItem.ToString() != roomName.Text)
                {
                    if (mConnection.cnMySQL.State != ConnectionState.Closed)
                        mConnection.cnMySQL.Close();

                    if (mConnection.cnMySQL.State != ConnectionState.Open)
                        mConnection.cnMySQL.Open();
                    mConnection.cmdMySQL = mConnection.cnMySQL.CreateCommand();
                    mConnection.cmdMySQL.CommandText =
                                         "UPDATE Prenotazioni SET da='" + (roomName.Text) + "' where prenotazioni.da='" + (ListaStanze.SelectedItem.ToString()) + "';";


                    mConnection.cmdMySQL.ExecuteNonQuery();

                    mConnection.cnMySQL.Close();
                }
            }
            Delete_Click2(sender, e);
            Add_Click(sender, e);
            oldname = roomName.Text;
        }

        private void Delete_Click2(object sender, EventArgs e)
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
                    mConnection.cmdMySQL.CommandText = "DELETE FROM fontiprenotazione WHERE nome = '" + ListaStanze.SelectedItem.ToString() + "'";
                    mConnection.cmdMySQL.ExecuteNonQuery();
                }
                else
                {
                    for (int i = 0; i < ListaStanze.SelectedItems.Count; i++)
                    {
                        mConnection.cmdMySQL.CommandText = "DELETE FROM fontiprenotazione WHERE nome = '" + ListaStanze.SelectedItems[i].ToString() + "'";
                        mConnection.cmdMySQL.ExecuteNonQuery();
                    }
                }
                mConnection.cnMySQL.Close();
                refreshlist();


                //MessageBox.Show("La fonte è stata eliminata con successo!",
                //"Fatto!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            if (mConnection != null)
            {
                if (oldname != roomName.Text)
                {
                    if (mConnection.cnMySQL.State != ConnectionState.Closed)
                        mConnection.cnMySQL.Close();

                    if (mConnection.cnMySQL.State != ConnectionState.Open)
                        mConnection.cnMySQL.Open();
                    mConnection.cmdMySQL = mConnection.cnMySQL.CreateCommand();
                    mConnection.cmdMySQL.CommandText =
                                         "UPDATE Prenotazioni SET da='None' where prenotazioni.da='" + ListaStanze.SelectedItem.ToString() + "';";


                    mConnection.cmdMySQL.ExecuteNonQuery();

                    mConnection.cnMySQL.Close();
                }
            }
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
                    mConnection.cmdMySQL.CommandText = "DELETE FROM fontiprenotazione WHERE nome = '" + ListaStanze.SelectedItem.ToString() + "'";
                    mConnection.cmdMySQL.ExecuteNonQuery();
                }
                else
                {
                    for (int i = 0; i < ListaStanze.SelectedItems.Count; i++)
                    {
                        mConnection.cmdMySQL.CommandText = "DELETE FROM fontiprenotazione WHERE nome = '" + ListaStanze.SelectedItems[i].ToString() + "'";
                        mConnection.cmdMySQL.ExecuteNonQuery();
                    }
                }
                mConnection.cnMySQL.Close();
                refreshlist();


                //MessageBox.Show("La fonte è stata eliminata con successo!",
                //"Fatto!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void changecolor()
        {
            label5.BackColor = Color.FromArgb((int)numericUpDown1.Value, (int)numericUpDown2.Value, (int)numericUpDown3.Value);
        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            changecolor();
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            changecolor();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            changecolor();
        }

        private void ListaStanze_SelectedIndexChanged(object sender, EventArgs e)
        {
            oldname = ListaStanze.SelectedItem.ToString();
        }

      
    }
}
