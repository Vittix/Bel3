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
    public partial class Acconti : Form
    {
        private connectoToMySql mConnection;
        private string Prenotazione;
        float tariffa;
        float anticipo;
        bool forfait;
        int days;
        int numerocamere;
        int iva;
        string datear, datepar;
        string cliente;

        public Acconti()
        {
            InitializeComponent();
        }

        public Acconti(connectoToMySql mCon, string p, string nomecliente, int numerocamere, string arrivo, string partenza, float tariffa, bool forfait, float anticipo, int iva)
        {
            // TODO: Complete member initialization
            mConnection = mCon;
            Prenotazione = p;
            InitializeComponent();
            cliente = nomecliente;
            this.anticipo = anticipo;
            this.forfait = forfait;
            this.tariffa = tariffa;
            DateTime ar = new DateTime(DateTime.Parse(arrivo).Year, DateTime.Parse(arrivo).Month, DateTime.Parse(arrivo).Day);
            datear = ar.ToShortDateString();
            DateTime par = new DateTime(DateTime.Parse(partenza).Year, DateTime.Parse(partenza).Month, DateTime.Parse(partenza).Day);
            TimeSpan t = par - ar;
            datepar = par.ToShortDateString();

            days = t.Days;
            this.numerocamere = numerocamere;
            this.iva = iva;
            comboBox1.SelectedIndex = 0;
            refreshlist();
        }

        public void refreshlist()
        {
            listView1.Items.Clear();
            if (mConnection != null)
            {
                if (mConnection.cnMySQL.State != ConnectionState.Closed)
                    mConnection.cnMySQL.Close();

                if (mConnection.cnMySQL.State != ConnectionState.Open)
                    mConnection.cnMySQL.Open();

                mConnection.cmdMySQL = new MySqlCommand("select * from acconti where acconti.idprenotazione="+Prenotazione.ToString()+"", mConnection.cnMySQL);
                mConnection.reader = mConnection.cmdMySQL.ExecuteReader();

                while (mConnection.reader.Read())
                {
                    string idunico = mConnection.reader.GetValue(0).ToString();
                    string[] thisrow=new string[4];
                    thisrow[0] = mConnection.reader.GetValue(1).ToString();
                    thisrow[1] = mConnection.reader.GetValue(2).ToString();
                    thisrow[2] = mConnection.reader.GetValue(3).ToString();
                    thisrow[3] = mConnection.reader.GetValue(4).ToString();

                    // for (int i = 0; i < mConnection.reader.FieldCount; i++)
                    //   thisrow += mConnection.reader.GetValue(i).ToString() + ",";            
                    listView1.Items.Add(idunico).SubItems.AddRange(thisrow);
                }
                mConnection.cnMySQL.Close();
            }
        }

        int nfattura;

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
             if (mConnection != null)
            {

                if (mConnection.cnMySQL.State != ConnectionState.Closed)
                    mConnection.cnMySQL.Close();

                if (mConnection.cnMySQL.State != ConnectionState.Open)
                    mConnection.cnMySQL.Open();

                mConnection.cmdMySQL = new MySqlCommand("select * from progressivi", mConnection.cnMySQL);
                mConnection.reader = mConnection.cmdMySQL.ExecuteReader();

                while (mConnection.reader.Read())
                {
                    if (comboBox1.SelectedItem.ToString() == "Fattura")
                    {
                        nfattura = Convert.ToInt16(mConnection.reader.GetValue(0));
                    }
                    else
                    {
                        nfattura = Convert.ToInt16(mConnection.reader.GetValue(1));
                    }
                }

                mConnection.cnMySQL.Close();

                if (mConnection.cnMySQL.State != ConnectionState.Closed)
                    mConnection.cnMySQL.Close();

                if (mConnection.cnMySQL.State != ConnectionState.Open)
                    mConnection.cnMySQL.Open();
                mConnection.cmdMySQL = mConnection.cnMySQL.CreateCommand();

                if (comboBox1.SelectedItem.ToString() == "Fattura")
                {
                    mConnection.cmdMySQL.CommandText = "INSERT INTO acconti (acconto,idprenotazione,fattura,tipe,data) VALUES('" +textBox1.Text+ "'," + Convert.ToInt16(Prenotazione) + "," + Convert.ToInt16(nfattura) + ",'" + comboBox1.SelectedItem.ToString() + "','" + DateTime.Now.ToShortDateString() + "')";
                }
                else
                {
                    mConnection.cmdMySQL.CommandText = "INSERT INTO acconti (acconto,idprenotazione,fattura,tipe,data) VALUES('" + textBox1.Text + "'," + Convert.ToInt16(Prenotazione) + "," + Convert.ToInt16(nfattura) + ",'" + comboBox1.SelectedItem.ToString() + "','" + DateTime.Now.ToShortDateString() + "')";
                }
                    mConnection.cmdMySQL.ExecuteNonQuery();

                mConnection.cnMySQL.Close();
                refreshlist();
                float accontototale = 0;
                foreach (ListViewItem item in listView1.Items)
                {
                    accontototale += Convert.ToSingle(item.SubItems[1].Text);
                }

                if (mConnection.cnMySQL.State != ConnectionState.Closed)
                    mConnection.cnMySQL.Close();

                if (mConnection.cnMySQL.State != ConnectionState.Open)
                    mConnection.cnMySQL.Open();
                mConnection.cmdMySQL = mConnection.cnMySQL.CreateCommand();
              mConnection.cmdMySQL.CommandText =
                                   "UPDATE Prenotazioni SET anticipo='" + (accontototale) + "' where prenotazioni.id=" + (Prenotazione) + ";";
                 

                mConnection.cmdMySQL.ExecuteNonQuery();
                
                 mConnection.cnMySQL.Close();
                
               
                //MessageBox.Show("La fonte è stata inserita con successo!",
                //"Fatto!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            //    this.Close();
            }
        
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                if (MessageBox.Show("Sei Sicuro?",
                "Attenzione!!!", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) != DialogResult.Yes)
                    return;

                foreach (ListViewItem it in listView1.SelectedItems)
                {
                    if (mConnection.cnMySQL.State != ConnectionState.Closed)
                        mConnection.cnMySQL.Close();

                    if (mConnection.cnMySQL.State != ConnectionState.Open)
                        mConnection.cnMySQL.Open();
                    mConnection.cmdMySQL = mConnection.cnMySQL.CreateCommand();


                    mConnection.cmdMySQL.CommandText = "DELETE FROM acconti WHERE acconti.idunico=" + Convert.ToInt16(it.Text) + ";";

                        mConnection.cmdMySQL.ExecuteNonQuery();

                    mConnection.cnMySQL.Close();
                    refreshlist();
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                if (comboBox1.SelectedItem.ToString() == "Fattura")
                {
                    Fattura fat = new Fattura(mConnection, "Fattura", Prenotazione, true, Convert.ToInt16(listView1.SelectedItems[0].Text));
                    fat.Show();

                    // mConnection.cmdMySQL.CommandText = "UPDATE progressivi set Fatture=" + nfattura + ";";
                }
                else
                {
                    Fattura fat = new Fattura(mConnection, "Ricevuta", Prenotazione, true, Convert.ToInt16(listView1.SelectedItems[0].Text));
                    fat.Show();
                    //mConnection.cmdMySQL.CommandText = "UPDATE progressivi set Ricevute=" + nfattura + ";";
                }
                this.Close();
            }
        }
    }
}
