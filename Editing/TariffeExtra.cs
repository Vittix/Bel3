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
    public partial class TariffeExtra : Form
    {
        private connectoToMySql mConnection;
        public TariffeExtra(connectoToMySql mCon)
        {
            mConnection = mCon;
            InitializeComponent();
            refreshlist();
        }

        void refreshlist()
        {
            tariffex.Items.Clear();
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
                mConnection.cmdMySQL = new MySqlCommand("select * from tariffeextra Order by nome ASC", mConnection.cnMySQL);

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
                    tariffex.Items.Add(thisrow);
                }
                mConnection.cnMySQL.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
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

                mConnection.cmdMySQL.CommandText = "INSERT INTO tariffeextra (nome,tariffa,iva) VALUES('" + nome.Text + "','" +  prezzo.Text + "'," + Convert.ToDecimal(iva.Text) + ")";
                mConnection.cmdMySQL.ExecuteNonQuery();

                mConnection.cnMySQL.Close();
                refreshlist();

                //MessageBox.Show("La fonte è stata inserita con successo!",
                //"Fatto!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
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
                if (tariffex.SelectedItems.Count < 2)
                {
                    mConnection.cmdMySQL.CommandText = "DELETE FROM tariffeextra WHERE nome = '" + tariffex.SelectedItem.ToString() + "'";
                    mConnection.cmdMySQL.ExecuteNonQuery();
                }
                else
                {
                    for (int i = 0; i < tariffex.SelectedItems.Count; i++)
                    {
                        mConnection.cmdMySQL.CommandText = "DELETE FROM tariffeextra WHERE nome = '" + tariffex.SelectedItems[i].ToString() + "'";
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

        private void TariffeExtra_Load(object sender, EventArgs e)
        {

        }

        private void ListaStanze_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (mConnection.cnMySQL.State != ConnectionState.Closed)
                mConnection.cnMySQL.Close();

            if (mConnection.cnMySQL.State != ConnectionState.Open)
                mConnection.cnMySQL.Open();

            mConnection.cmdMySQL = new MySqlCommand("select * from tariffeextra where nome='"+tariffex.Items[tariffex.SelectedIndex].ToString() +"' Order by nome ASC", mConnection.cnMySQL);
            mConnection.reader = mConnection.cmdMySQL.ExecuteReader();

            while (mConnection.reader.Read())
            {
                nome.Text = mConnection.reader.GetValue(0).ToString();
                prezzo.Text = mConnection.reader.GetValue(1).ToString();
                iva.Text = mConnection.reader.GetValue(2).ToString();
            }
            mConnection.cnMySQL.Close();
        }
    }
}
