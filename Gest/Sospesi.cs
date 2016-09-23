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
    public partial class Sospesi : Form
    {
        connectoToMySql mConnection;
        string idpren;
        int numerocamere = 0;
        float sogiorno=0;
        string iva;
        public Sospesi(connectoToMySql mCon,string idpren)
        {
            this.idpren=idpren;
            mConnection = mCon;
            InitializeComponent();
            load();
        }

        void load()
        {
            if (mConnection != null)
            {
                listView1.Items.Clear();
                mConnection.cmdMySQL = new MySqlCommand(
                   "SELECT * FROM prenotazioni where id=" + idpren + ";", mConnection.cnMySQL);
                if (mConnection.cnMySQL.State != ConnectionState.Closed)
                    mConnection.cnMySQL.Close();
                if (mConnection.cnMySQL.State != ConnectionState.Open)
                    mConnection.cnMySQL.Open();
                mConnection.reader = mConnection.cmdMySQL.ExecuteReader();

                while (mConnection.reader.Read())
                {
                    string idPrenotazione = mConnection.reader.GetValue(0).ToString();
                    string camera = mConnection.reader.GetValue(1).ToString();
                    string Idclient = mConnection.reader.GetValue(2).ToString();
                    string arr = mConnection.reader.GetValue(3).ToString();
                    string par = mConnection.reader.GetValue(4).ToString();
                    string nome = mConnection.reader.GetValue(7).ToString();
                    string cognome = mConnection.reader.GetValue(8).ToString();
                    string tariffa = mConnection.reader.GetValue(5).ToString();
                    string forfait = mConnection.reader.GetValue(6).ToString();
                    string da = mConnection.reader.GetValue(10).ToString();

                    string pagato = mConnection.reader.GetValue(16).ToString();
                    if (pagato == "True")
                        pagato = "Pagata";
                    else
                        pagato = "No";
                    string checkin = mConnection.reader.GetValue(17).ToString();
                    if (checkin == "True")
                    {
                        checkin = "Effettuato";
                    }
                    else
                    {
                        checkin = "Non Effettuato";
                    }
                    string checkout = mConnection.reader.GetValue(18).ToString();
                    if (checkout == "True")
                    {
                        checkout = "Effettuato";
                    }
                    else
                    {
                        checkout = "Non Effettuato";
                    }
                    DateTime arrival, departure;

                    if (arr != "" && par != "")
                    {
                        arrival = new DateTime(DateTime.Parse(arr).Year,DateTime.Parse(arr).Month,DateTime.Parse(arr).Day);

                        departure = new DateTime(DateTime.Parse(par).Year, DateTime.Parse(par).Month, DateTime.Parse(par).Day);
                    }
                    else
                    {
                        arrival = new DateTime(3000, 12, 31);
                        departure = new DateTime(3000, 12, 31);
                    }
                    string tipologia = mConnection.reader.GetValue(12).ToString();
                    string tipostanza = mConnection.reader.GetValue(13).ToString();
                    string arrangiamento = mConnection.reader.GetValue(14).ToString();
                    string rif = mConnection.reader.GetValue(19).ToString();
                    iva = mConnection.reader.GetValue(9).ToString();
                    TimeSpan daysspan = departure - arrival;
                    float prezzo=Convert.ToSingle(tariffa) * daysspan.Days;
                    sogiorno +=prezzo;
                    float prezzolordo = prezzo * ((100 - Convert.ToSingle(iva)) / 100);
                    string quantità = " ";
                    numerocamere++;
                    string[] subitem = new string[6] {"soggiorno", prezzo.ToString(), iva.ToString(), prezzolordo.ToString(), quantità, arrival.ToShortDateString() };
                    listView1.Items.Add(idpren).SubItems.AddRange(subitem);
                
                }



                mConnection.cnMySQL.Close();

                 mConnection.cmdMySQL = new MySqlCommand(
                   "SELECT * FROM acconti where idprenotazione=" + idpren + ";", mConnection.cnMySQL);
                if (mConnection.cnMySQL.State != ConnectionState.Closed)
                    mConnection.cnMySQL.Close();
                if (mConnection.cnMySQL.State != ConnectionState.Open)
                    mConnection.cnMySQL.Open();
                mConnection.reader = mConnection.cmdMySQL.ExecuteReader();

                while (mConnection.reader.Read())
                {
                    float acconto = mConnection.reader.GetFloat(1);
                    string date = mConnection.reader.GetString(5);
                    sogiorno -= acconto;
                    float prezzolordo = acconto * ((100 - Convert.ToSingle(iva)) / 100);
                    string[] subitem = new string[6] { "Acconto", acconto.ToString(), iva.ToString(), prezzolordo.ToString(), "", date };
                    listView1.Items.Add(idpren).SubItems.AddRange(subitem);
            
                }
                mConnection.cnMySQL.Close();

                mConnection.cmdMySQL = new MySqlCommand(
              "SELECT * FROM conti where id=" + idpren + ";", mConnection.cnMySQL);
                if (mConnection.cnMySQL.State != ConnectionState.Closed)
                    mConnection.cnMySQL.Close();
                if (mConnection.cnMySQL.State != ConnectionState.Open)
                    mConnection.cnMySQL.Open();
                mConnection.reader = mConnection.cmdMySQL.ExecuteReader();

                while (mConnection.reader.Read())
                {
                    string nome = mConnection.reader.GetValue(1).ToString();

                    string quantità = mConnection.reader.GetValue(5).ToString();
                    float extra = mConnection.reader.GetFloat(2);
                    string date = mConnection.reader.GetString(6);
                    sogiorno += extra;
                    float ExIva = mConnection.reader.GetFloat(3);
                    float prezzolordo = extra * ((100 - Convert.ToSingle(ExIva)) / 100);
                    string[] subitem = new string[6] { nome, extra.ToString(), ExIva.ToString(), prezzolordo.ToString(), quantità, date };
                    listView1.Items.Add(idpren).SubItems.AddRange(subitem);

                }
                mConnection.cnMySQL.Close();

                PrezzoTot.Text = "Prezzo totale: "+sogiorno.ToString()+" €";

            }
            
        }
    }
}
