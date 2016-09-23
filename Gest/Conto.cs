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
    public partial class Conto : Form
    {
        private connectoToMySql mConnection;
        private string Prenotazione;
        float tariffa;
        float anticipo;
        bool forfait;
        int days;
        int numerocamere;
        int iva;
        string datear;
        string cliente;
        string currentcamera;
        public Conto(connectoToMySql mCon, string idCliente, string p, int numerocamere, string arrivo, string partenza, float tariffa, bool forfait, float anticipo, int iva,string camera)
        {
            currentcamera = camera;
            mConnection = mCon;
            cliente = idCliente;
            Prenotazione = p;
            InitializeComponent();
            this.anticipo = anticipo;
            this.forfait = forfait;
            this.tariffa = tariffa;
            DateTime ar = new DateTime(DateTime.Parse(arrivo).Year, DateTime.Parse(arrivo).Month, DateTime.Parse(arrivo).Day);
            datear = ar.ToShortDateString();
            DateTime par = new DateTime(DateTime.Parse(partenza).Year, DateTime.Parse(partenza).Month, DateTime.Parse(partenza).Day);
            TimeSpan t = par - ar;
            days = t.Days;
            this.numerocamere = numerocamere;
            this.iva = iva;
            refreshlist();
            dateTimePicker1.Value = DateTime.Now;
        }
        float prezzotot = 0;
        float anticipotot = 0;
        private void refreshlist()
        {
            prezzotot = 0;
            anticipotot = 0;
            listView1.Items.Clear();
        
            if (mConnection != null)
            {
                string[] conto = new string[7];// = "";
                conto[0] = "Soggiorno";
                conto[2] = iva.ToString();
                if (forfait)
                {
                    prezzotot+= tariffa;
                    conto[1] =  String.Format("{0:0.00}",tariffa);
                    conto[4] = ((tariffa) - ((tariffa) * iva / 100)).ToString();
                    conto[5] = numerocamere.ToString();
           
                }
                else
                {
                    prezzotot += (tariffa * numerocamere * days);
                  
                    conto[1] =  String.Format("{0:0.00}",(tariffa * numerocamere * days));
                    conto[4] = ((tariffa * numerocamere * days) - ((tariffa * numerocamere * days) * iva / 100)).ToString();
                    conto[5] = numerocamere.ToString();
                }
                conto[6] =datear;
                conto[3] = "-1000";
                  
                listView1.Items.Add(Prenotazione.ToString()).SubItems.AddRange(conto);

                if (mConnection.cnMySQL.State != ConnectionState.Closed)
                    mConnection.cnMySQL.Close();

                if (mConnection.cnMySQL.State != ConnectionState.Open)
                    mConnection.cnMySQL.Open();

                mConnection.cmdMySQL = new MySqlCommand("select * from acconti where idprenotazione=" + Convert.ToInt16(Prenotazione), mConnection.cnMySQL);
                mConnection.reader = mConnection.cmdMySQL.ExecuteReader();

                while (mConnection.reader.Read())
                {
                    string[] thisrow = new string[7];// = "";                  
                    string nom =mConnection.reader.GetValue(2).ToString();  
                    thisrow[0] = "Anticipo";
                    thisrow[1] = mConnection.reader.GetValue(1).ToString();
                    thisrow[2] = "";
                    thisrow[3] = "";
                    thisrow[4] = "";
                    thisrow[5] = "";
                    thisrow[6] = mConnection.reader.GetValue(5).ToString();
                    anticipotot += Convert.ToSingle(mConnection.reader.GetValue(1).ToString());
                    thisrow[1] = String.Format("{0:0.00}",mConnection.reader.GetValue(1));
                  //  thisrow[3] = mConnection.reader.GetValue(4).ToString();
                    listView1.Items.Add(nom).SubItems.AddRange(thisrow);
                
                }
                mConnection.cnMySQL.Close();
            
                if (mConnection.cnMySQL.State != ConnectionState.Closed)
                    mConnection.cnMySQL.Close();

                if (mConnection.cnMySQL.State != ConnectionState.Open)
                    mConnection.cnMySQL.Open();

                mConnection.cmdMySQL = new MySqlCommand("select * from conti where id=" + Convert.ToInt16(Prenotazione) + " and camera='"+currentcamera+"' Order by nome ASC", mConnection.cnMySQL);
                mConnection.reader = mConnection.cmdMySQL.ExecuteReader();

                while (mConnection.reader.Read())
                {
                    string[] thisrow = new string[7];// = "";
                    string nom = mConnection.reader.GetValue(0).ToString();
                    thisrow[0] = mConnection.reader.GetValue(1).ToString();
                    thisrow[1] = mConnection.reader.GetValue(2).ToString();
                    thisrow[2] = mConnection.reader.GetValue(3).ToString();
                    thisrow[3] = mConnection.reader.GetValue(4).ToString();
                    thisrow[4] = mConnection.reader.GetValue(5).ToString();
                    thisrow[5] = mConnection.reader.GetValue(5).ToString();
                    thisrow[6] = mConnection.reader.GetValue(6).ToString();
                   // thisrow[5] = "";
                    prezzotot += Convert.ToSingle(thisrow[1]) * Convert.ToSingle(thisrow[4]);
                    thisrow[1]=String.Format("{0:0.00}", (Convert.ToSingle( thisrow[1])*Convert.ToSingle(thisrow[4])));
                    thisrow[4] = String.Format("{0:0.00}",((Convert.ToSingle(thisrow[1])) - (Convert.ToSingle(thisrow[1]) * Convert.ToSingle(thisrow[2])/100)));
                    // for (int i = 0; i < mConnection.reader.FieldCount; i++)
                    //   thisrow += mConnection.reader.GetValue(i).ToString() + ",";            
                    listView1.Items.Add(nom).SubItems.AddRange(thisrow);
                }
                mConnection.cnMySQL.Close();
                PrezzoTot.Text = "Prezzo Totale: "+ String.Format("{0:0.00}",prezzotot)+" € - "+String.Format("{0:0.00}",anticipotot)+" € di anticipo\nTotale:"+String.Format("{0:0.00}",(prezzotot-anticipotot))+" €";
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == null || textBox3.Text == null || textBox2.Text == "" || textBox3.Text == "")
            {

                MessageBox.Show("Riempi tutti i campi!", "Attenzione!!!", MessageBoxButtons.OK);
               
                return;
            }
            if (mConnection != null)
            {

                if (mConnection.cnMySQL.State != ConnectionState.Closed)
                    mConnection.cnMySQL.Close();

                if (mConnection.cnMySQL.State != ConnectionState.Open)
                    mConnection.cnMySQL.Open();
                mConnection.cmdMySQL = mConnection.cnMySQL.CreateCommand();
                
                float prezz = Convert.ToSingle(textBox2.Text);
                mConnection.cmdMySQL.CommandText = "INSERT INTO conti (id,nome,prezzo,iva,quantità,data,camera) VALUES('" + Prenotazione + "','" + textBox1.Text + "','" +textBox2.Text + "'," + Convert.ToSingle(textBox3.Text) + "," + 1 + ",'" + dateTimePicker1.Value.ToShortDateString() + "','"+currentcamera+"')";
                mConnection.cmdMySQL.ExecuteNonQuery();

                mConnection.cnMySQL.Close();
                refreshlist();

                //MessageBox.Show("La fonte è stata inserita con successo!",
                //"Fatto!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        private void Conto_Load(object sender, EventArgs e)
        {

        }

      
    }
}
