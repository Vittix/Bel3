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
    public partial class SpeseExtra : Form
    {
        private MySqlConnection mConnection;
        private string Prenotazione;
        float tariffa;
        float anticipo;
        bool forfait;
        int days;
        int numerocamere;
        int iva;
        string currentcamera;
        public SpeseExtra()
        {
            InitializeComponent();
        }

        public SpeseExtra(MySqlConnection mCon, string p,int numerocamere,string arrivo,string partenza,float tariffa,bool forfait,float anticipo,int iva,string camere)
        {
            currentcamera = camere;
            mConnection = mCon;
            Prenotazione = p;
            InitializeComponent();
            this.anticipo = anticipo;
            this.forfait = forfait;
            this.tariffa = tariffa;
            DateTime ar = new DateTime(DateTime.Parse(arrivo).Year, DateTime.Parse(arrivo).Month, DateTime.Parse(arrivo).Day);
            DateTime par = new DateTime(DateTime.Parse(partenza).Year, DateTime.Parse(partenza).Month, DateTime.Parse(partenza).Day);
            TimeSpan t = par - ar;
            days = t.Days;
            this.numerocamere = numerocamere;
            this.iva = iva;
            refreshlist();
            dateTimePicker1.Value = DateTime.Now;
        }

        private void refreshlist()
        {
            listView1.Items.Clear();
            listView2.Items.Clear();
            if (mConnection != null)
            {
                string[] conto = new string[4];// = "";
                conto[0] = "Soggiorno";
                if (forfait)
                {
                    conto[1] = tariffa.ToString();
                }
                else
                {
                    conto[1] = (tariffa * numerocamere * days).ToString() ;
                }
                conto[2] = iva.ToString();
                conto[3] = "-1000";
                listView1.Items.Add(Prenotazione.ToString()).SubItems.AddRange(conto);

               
                    if (mConnection.State != ConnectionState.Closed)
                        mConnection.Close();

                    if (mConnection.State != ConnectionState.Open)
                        mConnection.Open();

                   MySqlCommand cmd = new MySqlCommand("select * from conti where id=" + Convert.ToInt16(Prenotazione) + " and camera='"+currentcamera+"' Order by nome ASC", mConnection);
                   MySqlDataReader datareader = cmd.ExecuteReader();

                   while (datareader.Read())
                    {
                        string[] thisrow = new string[6];// = "";
                        string nom = datareader.GetValue(0).ToString();
                        thisrow[0] = datareader.GetValue(1).ToString();
                        thisrow[1] = datareader.GetValue(2).ToString();
                        thisrow[2] = datareader.GetValue(3).ToString();
                        thisrow[3] = datareader.GetValue(4).ToString();
                        thisrow[4] = datareader.GetValue(5).ToString();
                        thisrow[5] = datareader.GetValue(6).ToString();

                        // for (int i = 0; i < mConnection.reader.FieldCount; i++)
                        //   thisrow += mConnection.reader.GetValue(i).ToString() + ",";            
                        listView1.Items.Add(nom).SubItems.AddRange(thisrow);
                    }
                    mConnection.Close();
                
                if (mConnection.State != ConnectionState.Closed)
                    mConnection.Close();

                if (mConnection.State != ConnectionState.Open)
                    mConnection.Open();

                cmd = new MySqlCommand("select * from tariffeextra Order by nome ASC", mConnection);
                datareader = cmd.ExecuteReader();

                while (datareader.Read())
                {
                    string[] thisrow = new string[2];// = "";
                    string nom = datareader.GetValue(0).ToString();
                    thisrow[0] = datareader.GetValue(1).ToString();
                    thisrow[1] = datareader.GetValue(2).ToString();

                    // for (int i = 0; i < mConnection.reader.FieldCount; i++)
                    //   thisrow += mConnection.reader.GetValue(i).ToString() + ",";            
                    listView2.Items.Add(nom).SubItems.AddRange(thisrow);
                }
                mConnection.Close();
            }
        }

        private void listView2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (listView2.SelectedItems.Count > 0)
            {
                if (mConnection != null)
                {
                    
                    if (mConnection.State != ConnectionState.Closed)
                        mConnection.Close();

                    if (mConnection.State != ConnectionState.Open)
                        mConnection.Open();
                    MySqlCommand cmd = new MySqlCommand("INSERT INTO conti (id,nome,prezzo,iva,quantità,data,camera) VALUES('" + Prenotazione + "','" + listView2.SelectedItems[0].Text + "','" + listView2.SelectedItems[0].SubItems[1].Text + "'," + Convert.ToSingle(listView2.SelectedItems[0].SubItems[2].Text) + "," + numericUpDown1.Value + ",'" +dateTimePicker1.Value.ToShortDateString() + "','" + currentcamera + "')", mConnection);
                 

                    cmd.ExecuteNonQuery();

                    mConnection.Close();
                    refreshlist();

                    //MessageBox.Show("La fonte è stata inserita con successo!",
                    //"Fatto!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                if (listView1.SelectedItems[0].SubItems[3] == null)
                    return;

                if (mConnection != null)
                {

                    if (mConnection.State != ConnectionState.Closed)
                        mConnection.Close();

                    if (mConnection.State != ConnectionState.Open)
                        mConnection.Open();
                    MySqlCommand cmd = new MySqlCommand("delete from conti where idunico=" + Convert.ToInt16(listView1.SelectedItems[0].SubItems[4].Text), mConnection);
                 
                   // mConnection.cmdMySQL.CommandText = "delete from conti where idunico="+Convert.ToInt16(listView1.SelectedItems[0].SubItems[4].Text);
                    cmd.ExecuteNonQuery();

                    mConnection.Close();
                    refreshlist();

                    //MessageBox.Show("La fonte è stata inserita con successo!",
                    //"Fatto!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
