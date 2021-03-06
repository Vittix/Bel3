﻿using System;
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
    public partial class Arrangiamento : Form
    {
        private connectoToMySql mConnection;
        public Arrangiamento(connectoToMySql mCon)
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
                mConnection.cmdMySQL = new MySqlCommand("select * from arrangiamento Order by nome ASC", mConnection.cnMySQL);

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

        private void Add_Click(object sender, EventArgs e)
        {
            if (mConnection != null)
            {
                if (mConnection.cnMySQL.State != ConnectionState.Open)
                    mConnection.cnMySQL.Open();
                mConnection.cmdMySQL = mConnection.cnMySQL.CreateCommand();

                mConnection.cmdMySQL.CommandText = "INSERT INTO arrangiamento (nome) VALUES('" + roomName.Text + "')";
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

                mConnection.cnMySQL.Open();
                mConnection.cmdMySQL = mConnection.cnMySQL.CreateCommand();

                mConnection.cmdMySQL.CommandText = "UPDATE arrangiamento SET nome = '" + roomName.Text + "' WHERE operatori.nome = '" + ListaStanze.SelectedItem.ToString() + "'";
                mConnection.cmdMySQL.ExecuteNonQuery();

                mConnection.cnMySQL.Close();
                refreshlist();

                //MessageBox.Show("La fonte è modificata con successo!",
                //"Fatto!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

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
                    mConnection.cmdMySQL.CommandText = "DELETE FROM arrangiamento WHERE nome = '" + ListaStanze.SelectedItem.ToString() + "'";
                    mConnection.cmdMySQL.ExecuteNonQuery();
                }
                else
                {
                    for (int i = 0; i < ListaStanze.SelectedItems.Count; i++)
                    {
                        mConnection.cmdMySQL.CommandText = "DELETE FROM arrangiamento WHERE nome = '" + ListaStanze.SelectedItems[i].ToString() + "'";
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

        private void Arrangiamento_Load(object sender, EventArgs e)
        {
            refreshlist();
        }
    }
}
