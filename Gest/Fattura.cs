using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Net;
using System.Net.Sockets;

namespace Bel3
{
    public partial class Fattura : Form
    {
        private connectoToMySql mConnection;
        private string Prenotazione;
        float tariffa;
        float anticipo;
        
        bool forfait;
        int days;
        int numerocamere=0;
        float iva;
        string datear;
        bool prepag = false;
        string cliente;
        string fullname;
        public Fattura()
        {
            InitializeComponent();
        }
        string clienteperesteso;
        public Fattura(connectoToMySql mCon, string tipo, string p, string nomecliente, int numerocamere, string arrivo, string partenza, float tariffa, bool forfait, float anticipo, int iva)
        {
            // TODO: Complete member initialization
            InitializeComponent();
             // Intestazione.Text = "Spett.\n" + cliente;
            mConnection = mCon;
            Prenotazione = p;
            cliente = nomecliente;
         
            this.Text = tipo;
            this.anticipo = anticipo;
            this.forfait = forfait;
            this.tariffa = tariffa;
           
            this.durata.Text = "Durata: "+days+" Giorni";
            this.numerocamere = numerocamere;
            this.iva = iva;
            DateTime ar = new DateTime(DateTime.Parse(arrivo).Year, DateTime.Parse(arrivo).Month, DateTime.Parse(arrivo).Day);
          
            datear = ar.ToShortDateString();
            DateTime par = new DateTime(DateTime.Parse(partenza).Year, DateTime.Parse(partenza).Month, DateTime.Parse(partenza).Day);
            TimeSpan t = par - ar;
            this.arrivo.Value = ar;
            this.partenza.Value = par;
         
            days = t.Days;

            refresh();

            if (mConnection.cnMySQL.State != ConnectionState.Closed)
                mConnection.cnMySQL.Close();

            if (mConnection.cnMySQL.State != ConnectionState.Open)
                mConnection.cnMySQL.Open();

            mConnection.cmdMySQL = new MySqlCommand("select * from clienti where id=" + Convert.ToInt16(cliente), mConnection.cnMySQL);
            mConnection.reader = mConnection.cmdMySQL.ExecuteReader();

            while (mConnection.reader.Read())
            {
                codicefiscale.Text = mConnection.reader.GetValue(4).ToString();
                if (mConnection.reader.GetValue(5).ToString() != String.Empty)
                    clienteperesteso = mConnection.reader.GetValue(5).ToString();
                else
                    clienteperesteso = "Spett. " + mConnection.reader.GetValue(1).ToString() + " " + mConnection.reader.GetValue(2).ToString();
                if (mConnection.reader.GetValue(5).ToString() != String.Empty)
                    Intestazione.Text = mConnection.reader.GetValue(5).ToString();
                else
                    Intestazione.Text = "Spett. " + mConnection.reader.GetValue(1).ToString() + " " + mConnection.reader.GetValue(2).ToString();
                fullname = mConnection.reader.GetValue(1).ToString() + " " + mConnection.reader.GetValue(2).ToString();
            }  

            mConnection.cnMySQL.Close();
           
        }

        public Fattura(connectoToMySql mCon, string tipo, string idpren)
        {
            InitializeComponent();
            mConnection = mCon;
            Prenotazione = idpren;
            this.Text = tipo;
            load();
        }
        public Fattura(connectoToMySql mCon, string tipo, string idpren,bool pp)
        {
            prepag = pp;
            InitializeComponent();
            mConnection = mCon;
            Prenotazione = idpren;
            if (Convert.ToInt16(idpren) < 0)
                cliente = "-150";
            this.Text = tipo;
            load();
        }
        public Fattura(connectoToMySql mCon, string tipo, string idpren, bool pp,bool bibite)
        {
            prepag = pp;
            InitializeComponent();
            mConnection = mCon;
            Prenotazione = idpren;
            if (Convert.ToInt16(idpren) < 0)
                cliente = "-150";
            this.Text = tipo;
            load();
            if(bibite)
            dataGridView1.Rows[0].Cells[1].Value = "Bibite";
            else
                dataGridView1.Rows[0].Cells[1].Value = "";
          
        }
        bool loaded = false;
        public void load()
        {
            if (mConnection != null)
            {
                string mIva;
                dataGridView1.Rows.Clear();
                mConnection.cmdMySQL = new MySqlCommand(
                   "SELECT * FROM prenotazioni where id=" + Prenotazione + ";", mConnection.cnMySQL);
                if (mConnection.cnMySQL.State != ConnectionState.Closed)
                    mConnection.cnMySQL.Close();
                if (mConnection.cnMySQL.State != ConnectionState.Open)
                    mConnection.cnMySQL.Open();
                mConnection.reader = mConnection.cmdMySQL.ExecuteReader();

                while (mConnection.reader.Read())
                {
                    string idPrenotazione = mConnection.reader.GetValue(0).ToString();
                    string camera = mConnection.reader.GetValue(1).ToString();
                    cam = camera;
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
                    {
                        skipsog = true;
                        pagato = "Pagata";
                    }
                    else
                    {
                        skipsog = false;
                        pagato = "No";
                    }
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
                        arrival = new DateTime(DateTime.Parse(arr).Year, DateTime.Parse(arr).Month, DateTime.Parse(arr).Day);

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
                    mIva = mConnection.reader.GetValue(9).ToString();
                    TimeSpan daysspan = departure - arrival;
                    float prezzo = Convert.ToSingle(tariffa) * daysspan.Days;
                    //sogiorno += prezzo;
                    float prezzolordo = prezzo / ((100 + Convert.ToSingle(iva)) / 100);
                    string quantità = " ";
                   // numerocamere++;
                   // string[] subitem = new string[6] { "soggiorno", prezzo.ToString(), iva.ToString(), prezzolordo.ToString(), quantità, arrival.ToShortDateString() };
                   // dataGridView1.Items.Add(idpren).SubItems.AddRange(subitem);
                    cliente = cognome +" "+nome;

                    //this.Text = tipo;
                    //this.anticipo = anticipo;
                    this.forfait = Convert.ToBoolean(forfait);
                    this.tariffa = Convert.ToSingle(tariffa);

                    this.durata.Text = "Durata: " + days + " Giorni";
                    this.numerocamere++;
                    this.iva = Convert.ToSingle(mIva);
                    DateTime ar = new DateTime(DateTime.Parse(arr).Year, DateTime.Parse(arr).Month, DateTime.Parse(arr).Day);

                    datear = ar.ToShortDateString();
                    DateTime pa = new DateTime(DateTime.Parse(par).Year, DateTime.Parse(par).Month, DateTime.Parse(par).Day);
                    TimeSpan t = pa - ar;
                    this.arrivo.Value = ar;
                    this.partenza.Value = pa;
                    string ragsoc=mConnection.reader.GetValue(10).ToString();
                    days = t.Days;

                    //Intestazione.Text = "Spett. " + cognome+ " " + nome + "\n" + ragsoc ;
                    //codicefiscale.Text = mConnection.reader.GetValue(4).ToString();
                    //clienteperesteso = mConnection.reader.GetValue(1).ToString() + " " + mConnection.reader.GetValue(2).ToString() + "\n" + mConnection.reader.GetValue(5).ToString();
                    cliente = Idclient;
                }
                mConnection.cnMySQL.Close();     
           

            if (mConnection.cnMySQL.State != ConnectionState.Closed)
                mConnection.cnMySQL.Close();

            if (mConnection.cnMySQL.State != ConnectionState.Open)
                mConnection.cnMySQL.Open();

            mConnection.cmdMySQL = new MySqlCommand("select * from clienti where id=" + Convert.ToInt16(cliente), mConnection.cnMySQL);
            mConnection.reader = mConnection.cmdMySQL.ExecuteReader();

            while (mConnection.reader.Read())
            {
              //  Intestazione.Text = "Spett. " + mConnection.reader.GetValue(1).ToString() + " " + mConnection.reader.GetValue(2).ToString() + "\n" + mConnection.reader.GetValue(5).ToString();
                codicefiscale.Text = mConnection.reader.GetValue(4).ToString();

                if (mConnection.reader.GetValue(5).ToString() != String.Empty)
                    clienteperesteso = mConnection.reader.GetValue(5).ToString();
                else
                    clienteperesteso = "Spett. " + mConnection.reader.GetValue(1).ToString() + " " + mConnection.reader.GetValue(2).ToString();
                 Intestazione.Text = clienteperesteso;
                fullname = mConnection.reader.GetValue(1).ToString() + " " + mConnection.reader.GetValue(2).ToString();
      
            }

            mConnection.cnMySQL.Close();
 }
            loaded = true;
            refresh();
            refresh2();
        }

        bool Acc=false;
        public Fattura(connectoToMySql mCon, string tipo, string p, string nomecliente, int numerocamere, string arrivo, string partenza, float tariffa, bool forfait, float anticipo, int iva,bool acconto)
        {
            Acc = acconto;
            // TODO: Complete member initialization
            InitializeComponent();
            cliente = nomecliente;
            // Intestazione.Text = "Spett.\n" + cliente;
            mConnection = mCon;
            Prenotazione = p;

            this.Text = tipo;
            this.anticipo = anticipo;
            this.forfait = forfait;
            this.tariffa = tariffa;

            this.durata.Text = "Durata: " + days + " Giorni";
            this.numerocamere = numerocamere;
            this.iva = iva;
            DateTime ar = new DateTime(DateTime.Parse(arrivo).Year, DateTime.Parse(arrivo).Month, DateTime.Parse(arrivo).Day);

            datear = ar.ToShortDateString();
            DateTime par = new DateTime(DateTime.Parse(partenza).Year, DateTime.Parse(partenza).Month, DateTime.Parse(partenza).Day);
            TimeSpan t = par - ar;
            this.arrivo.Value = ar;
            this.partenza.Value = par;

            days = t.Days;

            refresh();

            if (mConnection.cnMySQL.State != ConnectionState.Closed)
                mConnection.cnMySQL.Close();

            if (mConnection.cnMySQL.State != ConnectionState.Open)
                mConnection.cnMySQL.Open();

            mConnection.cmdMySQL = new MySqlCommand("select * from clienti where id=" + Convert.ToInt16(cliente), mConnection.cnMySQL);
            mConnection.reader = mConnection.cmdMySQL.ExecuteReader();

            while (mConnection.reader.Read())
            {
                codicefiscale.Text = mConnection.reader.GetValue(4).ToString();
                if (mConnection.reader.GetValue(5).ToString() != String.Empty)
                    clienteperesteso = mConnection.reader.GetValue(5).ToString();
                else
                    clienteperesteso = "Spett. " + mConnection.reader.GetValue(1).ToString() + " " + mConnection.reader.GetValue(2).ToString();
            
                Intestazione.Text = "Spett. " + clienteperesteso;
                fullname = mConnection.reader.GetValue(1).ToString() + " " + mConnection.reader.GetValue(2).ToString();
            }

            mConnection.cnMySQL.Close();

        }

        bool notacred = false;
        string DescrizioneNc;
        public Fattura(connectoToMySql mCon, string tipo, string idcliente, float quanto, bool nc,string desc,float iva)
        {
            DescrizioneNc = desc;
            notacred = nc;
            // TODO: Complete member initialization
            InitializeComponent();
            cliente = idcliente;
            // Intestazione.Text = "Spett.\n" + cliente;
            this.mConnection = mCon;
            this.Text = p;
          //  this.Prenotazione = Prenotazione;
            //acconto = p_2;
            arrivo.Enabled = false;
            partenza.Enabled = false;
            this.Text = tipo;
            float notacredito = quanto;

            //refresh();

            if (mConnection.cnMySQL.State != ConnectionState.Closed)
                mConnection.cnMySQL.Close();

            if (mConnection.cnMySQL.State != ConnectionState.Open)
                mConnection.cnMySQL.Open();

            mConnection.cmdMySQL = new MySqlCommand("select * from clienti where id=" + Convert.ToInt16(cliente), mConnection.cnMySQL);
            mConnection.reader = mConnection.cmdMySQL.ExecuteReader();

            while (mConnection.reader.Read())
            {
               codicefiscale.Text = mConnection.reader.GetValue(4).ToString();
               if (mConnection.reader.GetValue(5).ToString() != String.Empty)
                   clienteperesteso = mConnection.reader.GetValue(5).ToString();
               else
                   clienteperesteso = "Spett. " + mConnection.reader.GetValue(1).ToString() + " " + mConnection.reader.GetValue(2).ToString();
               Intestazione.Text = "Spett. " + clienteperesteso;
               fullname = mConnection.reader.GetValue(1).ToString() + " " + mConnection.reader.GetValue(2).ToString();
            }

            mConnection.cnMySQL.Close();

          

                if (mConnection.cnMySQL.State != ConnectionState.Closed)
                    mConnection.cnMySQL.Close();

                if (mConnection.cnMySQL.State != ConnectionState.Open)
                    mConnection.cnMySQL.Open();

                mConnection.cmdMySQL = new MySqlCommand("select * from progressivi", mConnection.cnMySQL);
                mConnection.reader = mConnection.cmdMySQL.ExecuteReader();

                while (mConnection.reader.Read())
                {
                        progressivo.Text = mConnection.reader.GetValue(2).ToString();                  
                }

                string[] thisrow = new string[4];// = "";                  
                string dataacc = DateTime.Now.ToShortDateString();
                thisrow[0] = DescrizioneNc;// + mConnection.reader.GetValue(4).ToString() + " rif." + mConnection.reader.GetValue(3).ToString();
                thisrow[1] = iva.ToString(); ;
                thisrow[2] =quanto.ToString();
                thisrow[3] = "" ;
            if(iva==10)
                Totale10 += Convert.ToSingle(thisrow[2]);
            else
                totale21 += Convert.ToSingle(thisrow[2]);

            float impostatot = 0;
            if(iva==10)
            impostatot = Convert.ToSingle(thisrow[2]) -( Convert.ToSingle(thisrow[2])/((100+ (float)iva) / 100));
             
                //  thisrow[3] = mConnection.reader.GetValue(4).ToString();
            DataGridViewRow row = (DataGridViewRow)dataGridView1.Rows[0].Clone();
            row.Cells[0].Value = dataacc;
            row.Cells[1].Value = thisrow[0];
            row.Cells[2].Value = thisrow[1];
            row.Cells[3].Value = thisrow[2];
            dataGridView1.Rows.Add(row);
                // dataGridView1.Items.Add(dataacc).SubItems.AddRange(thisrow);

                thisrow = new string[3];// = "";                  
                string impostas=(String.Format("{0:0.00}",quanto-(quanto / ((100 + iva) / 100))));
            string impon=String.Format("{0:0.00}",(quanto / ((100+iva) / 100)));
            string tot=String.Format("{0:0.00}",quanto);
           // impon=String.Format("{0:0.00}", Convert.ToSingle(impon));
                thisrow[0] = impon;// + mConnection.reader.GetValue(4).ToString() + " rif." + mConnection.reader.GetValue(3).ToString();
                thisrow[2] = String.Format("{0:0.00}",tot);
                thisrow[1] = String.Format("{0:0.00}",impostas);
                listView2.Items.Add(iva.ToString()).SubItems.AddRange(thisrow);
                itemcount++;
                iva = 10;
        }
        bool acconto = false;
       // bool notacredito = false;
        int idunico;
        public Fattura(connectoToMySql mConnection, string p, string Prenotazione, bool p_2,int idunico)
        {
            this.idunico=idunico;
            InitializeComponent();
            checkBox1.Checked = false;
            checkBox1.Enabled = false;

            // TODO: Complete member initialization
            this.mConnection = mConnection;
            this.Text = p;
            this.Prenotazione = Prenotazione;
            acconto = p_2;
            arrivo.Enabled = false;
            partenza.Enabled = false;

            loadacconti();
           
        }
      
        private void loadacconti()
        {
            string mIva=" ";
              
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
                    if (this.Text == "Fattura")
                    {
                        progressivo.Text = mConnection.reader.GetValue(0).ToString();
                    }
                    else
                    {
                        progressivo.Text = mConnection.reader.GetValue(1).ToString();
                    }
                }

                dataGridView1.Rows.Clear();
                mConnection.cmdMySQL = new MySqlCommand(
                   "SELECT * FROM prenotazioni where id=" + Prenotazione + ";", mConnection.cnMySQL);
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
                    {
                        skipsog = true;
                        pagato = "Pagata";
                    }
                    else
                    {
                        skipsog = false;
                        pagato = "No";
                    }
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
                        arrival = new DateTime(DateTime.Parse(arr).Year, DateTime.Parse(arr).Month, DateTime.Parse(arr).Day);

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
                    mIva = mConnection.reader.GetValue(9).ToString();
                    TimeSpan daysspan = departure - arrival;
                    float prezzo = Convert.ToSingle(tariffa) * daysspan.Days;
                    //sogiorno += prezzo;
                    float prezzolordo = prezzo * ((100 - Convert.ToSingle(iva)) / 100);
                    string quantità = " ";
                    // numerocamere++;
                    // string[] subitem = new string[6] { "soggiorno", prezzo.ToString(), iva.ToString(), prezzolordo.ToString(), quantità, arrival.ToShortDateString() };
                    // dataGridView1.Items.Add(idpren).SubItems.AddRange(subitem);
                    cliente = cognome + " " + nome;

                    //this.Text = tipo;
                    //this.anticipo = anticipo;
                    this.forfait = Convert.ToBoolean(forfait);
                    this.tariffa = Convert.ToSingle(tariffa);

                    this.durata.Text = "Durata: " + days + " Giorni";
                    this.numerocamere++;
                    this.iva = Convert.ToSingle(mIva);
                    DateTime ar = new DateTime(DateTime.Parse(arr).Year, DateTime.Parse(arr).Month, DateTime.Parse(arr).Day);

                    datear = ar.ToShortDateString();
                    DateTime pa = new DateTime(DateTime.Parse(par).Year, DateTime.Parse(par).Month, DateTime.Parse(par).Day);
                    TimeSpan t = pa - ar;
                    this.arrivo.Value = ar;
                    this.partenza.Value = pa;
                    string ragsoc = mConnection.reader.GetValue(10).ToString();
                    days = t.Days;

                    //Intestazione.Text = "Spett. " + cognome+ " " + nome + "\n" + ragsoc ;
                    //codicefiscale.Text = mConnection.reader.GetValue(4).ToString();
                    //clienteperesteso = mConnection.reader.GetValue(1).ToString() + " " + mConnection.reader.GetValue(2).ToString() + "\n" + mConnection.reader.GetValue(5).ToString();
                    cliente = Idclient;
                }
                mConnection.cnMySQL.Close();
            }

            if (mConnection.cnMySQL.State != ConnectionState.Closed)
                mConnection.cnMySQL.Close();

            if (mConnection.cnMySQL.State != ConnectionState.Open)
                mConnection.cnMySQL.Open();

            mConnection.cmdMySQL = new MySqlCommand("select * from clienti where id=" + Convert.ToInt16(cliente), mConnection.cnMySQL);
            mConnection.reader = mConnection.cmdMySQL.ExecuteReader();

            while (mConnection.reader.Read())
            {
                codicefiscale.Text = mConnection.reader.GetValue(4).ToString();
                 if (mConnection.reader.GetValue(5).ToString() != String.Empty)
                    clienteperesteso = mConnection.reader.GetValue(5).ToString();
                else
                    clienteperesteso = "Spett. " + mConnection.reader.GetValue(1).ToString() + " " + mConnection.reader.GetValue(2).ToString();
            Intestazione.Text = "Spett. " + clienteperesteso;
                fullname = mConnection.reader.GetValue(1).ToString() + " " + mConnection.reader.GetValue(2).ToString();
            }

            mConnection.cnMySQL.Close();

            if (mConnection.cnMySQL.State != ConnectionState.Closed)
                mConnection.cnMySQL.Close();

            if (mConnection.cnMySQL.State != ConnectionState.Open)
                mConnection.cnMySQL.Open();

            mConnection.cmdMySQL = new MySqlCommand("select * from acconti where idprenotazione=" + Convert.ToInt16(Prenotazione) + " and idunico=" + idunico, mConnection.cnMySQL);
            mConnection.reader = mConnection.cmdMySQL.ExecuteReader();

            while (mConnection.reader.Read())
            {
                string[] thisrow = new string[4];// = "";                  
                string dataacc = mConnection.reader.GetValue(5).ToString();
                thisrow[0] = "Acconto Soggiorno da " + arrivo.Value.ToShortDateString() + " al " + partenza.Value.ToShortDateString() + " " + days + "GG";// + mConnection.reader.GetValue(4).ToString() + " rif." + mConnection.reader.GetValue(3).ToString();
                thisrow[1] = mIva.ToString(); ;
                thisrow[2] =String.Format("{0:0.00}",Convert.ToSingle( mConnection.reader.GetValue(1).ToString()));
                thisrow[3] = "";
                if(Convert.ToSingle(thisrow[1])==10)
                Totale10 += Convert.ToSingle(thisrow[2]);
                else
                    totale21+= Convert.ToSingle(thisrow[2]);
              
                // impostatot += Convert.ToInt16(thisrow[2]) * (float)iva / 100;

                //  thisrow[3] = mConnection.reader.GetValue(4).ToString();
                    DataGridViewRow row = (DataGridViewRow)dataGridView1.Rows[0].Clone();
               row.Cells[0].Value=dataacc;
               row.Cells[1].Value = thisrow[0];
                row.Cells[2].Value=thisrow[1];
                row.Cells[3].Value = thisrow[2];
               dataGridView1.Rows.Add(row);
                thisrow = new string[3];// = "";                  
               
                thisrow[0] = " ";// + mConnection.reader.GetValue(4).ToString() + " rif." + mConnection.reader.GetValue(3).ToString();
                thisrow[2] =String.Format("{0:0.00}",Convert.ToSingle(mConnection.reader.GetValue(1).ToString()));
                thisrow[1] = "";
                listView2.Items.Add(mIva).SubItems.AddRange(thisrow);
                itemcount++;
            }

            mConnection.cnMySQL.Close();

            loaded = true;
            refresh2();
        }

        private void partenza_ValueChanged(object sender, EventArgs e)
        {
            if (loaded)
            {
               refresh();
            }
        }

        int itemcount = 0;
        private void arrivo_ValueChanged(object sender, EventArgs e)
        {
            if (loaded)
            {
               refresh();
            }
        }
        float Totale10 = 0;
        float totale21 = 0;
        int oldiva = 0;
        float extratot2=0;
        float impostatot2=0;
        void refresh2()
        {
            TimeSpan t = partenza.Value - arrivo.Value;
            days = t.Days;
            this.durata.Text = "Durata: " + days + " Giorni";
            totale0 = 0;
           // dataGridView1.Rows.Clear();
            listView2.Items.Clear();
            float prezzosog = 0;
            float anticipotot = 0;
            float extratot = 0;
            float impostatot = 0;
            float imponibiletot = 0;
            if (iva == 0)
                iva = 10;
             Totale10 = 0;
         totale21 = 0;
         oldiva = 0;
         extratot2=0;
         extratot3 = 0;
         impostatot2=0;

            string[] conto = new string[3];
            string Dataogg = arrivo.Value.ToShortDateString();
            conto[1] = iva.ToString();
            float imponibiletot2;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells[1].Value != null)
                {
                    if (row.Cells[1].Value.ToString().Contains("Anticipo"))
                    {
                        int I = 0;
                        anticipotot += Convert.ToSingle(row.Cells[3].Value);
                    }
                    else
                    {
                        if (row.Cells[3].Value != null && row.Cells[2].Value != null)
                        {
                            if (row.Cells[2].Value.ToString() == "10")
                            {
                                Totale10 += Convert.ToSingle(row.Cells[3].Value);
                                // impostatot += (Convert.ToSingle(row.Cells[3].Value) - (Convert.ToSingle(row.Cells[3].Value)/1.10f));
                            }
                            else
                            {
                                if (row.Cells[2].Value.ToString() == "22")
                                {
                                    totale21 += Convert.ToSingle(row.Cells[3].Value);
                                }
                                if (row.Cells[2].Value.ToString() == "0")
                                {
                                    totale0 += Convert.ToSingle(row.Cells[3].Value);
                                }
                                // impostatot2 += (Convert.ToSingle(row.Cells[3].Value) - (Convert.ToSingle(row.Cells[3].Value) / 1.21f));
                            }
                        }
                    }
                }

            }
            Totale10 +=   extratot-anticipotot;
            totale21 +=   extratot2;
            impostatot = (Totale10) - (Totale10 / 1.10f);

            impostatot2 = (totale21) - (totale21 / 1.22f);
            imponibiletot = Totale10 - impostatot;
            imponibiletot2 = totale21 - impostatot2;
           
                listView2.Items.Add("10");
            
                string item = String.Format("{0:0.00}", imponibiletot);
            listView2.Items[0].SubItems.Add(item);
            item = String.Format("{0:0.00}", impostatot);
            listView2.Items[0].SubItems.Add(item);
            if (iva == 10)
                listView2.Items[0].SubItems.Add(String.Format("{0:0.00}", Totale10 + totale21 + totale0));
            else
                listView2.Items[0].SubItems.Add("");
            
                listView2.Items.Add("22");
                string item2 = String.Format("{0:0.00}", imponibiletot2);
                listView2.Items[1].SubItems.Add(item2);
                item2 = String.Format("{0:0.00}", impostatot2);
                listView2.Items[1].SubItems.Add(item2);
                if (iva == 22)
                    listView2.Items[1].SubItems.Add(String.Format("{0:0.00}", totale21 + Totale10));
                else
                    listView2.Items[1].SubItems.Add("");
            
            mConnection.cnMySQL.Close();

            itemcount = dataGridView1.Rows.Count;
        }


        void refresh()
        {
            TimeSpan t = partenza.Value - arrivo.Value;
            days = t.Days;
            this.durata.Text = "Durata: " + days + " Giorni";

            dataGridView1.Rows.Clear();
            listView2.Items.Clear();
            float prezzosog = 0;
            float anticipotot = 0;
            float extratot = 0;
            float impostatot = 0;
            float imponibiletot = 0;
            string[] conto = new string[3];
            string Dataogg = arrivo.Value.ToShortDateString();
            conto[1] = iva.ToString();
            if (!skipsog || prepag)
            {
                if (forfait)
                {
                    prezzosog += tariffa;
                    conto[2] = tariffa.ToString();


                }
                else
                {
                    prezzosog += ((tariffa) * numerocamere * days);

                    conto[2] = String.Format("{0:0.00}", (tariffa * numerocamere * days));

                }

                conto[0] = "Soggiorno da " + arrivo.Value.ToShortDateString() + " al " + partenza.Value.ToShortDateString() + " " + days + "GG";
                if (iva == 10)
                    impostatot += prezzosog - (prezzosog / (((float)iva + 100) / 100));
                else                  
                        impostatot2 += prezzosog - (prezzosog / (((float)iva + 100) / 100));
         
                itemcount++;

                // dataGridView1.Items.Add(Dataogg).SubItems.AddRange(conto);
                DataGridViewRow row = (DataGridViewRow)dataGridView1.Rows[0].Clone();
                row.Cells[0].Value = Dataogg;
                row.Cells[1].Value = conto[0];
                row.Cells[2].Value = conto[1];
                row.Cells[3].Value = conto[2];
                dataGridView1.Rows.Add(row);
            }
            if (mConnection.cnMySQL.State != ConnectionState.Closed)
                mConnection.cnMySQL.Close();

            if (mConnection.cnMySQL.State != ConnectionState.Open)
                mConnection.cnMySQL.Open();

            mConnection.cmdMySQL = new MySqlCommand("select * from progressivi", mConnection.cnMySQL);
            mConnection.reader = mConnection.cmdMySQL.ExecuteReader();

            while (mConnection.reader.Read())
            {
                if (this.Text == "Fattura")
                {
                    progressivo.Text = mConnection.reader.GetValue(0).ToString();
                }
                else
                {
                    progressivo.Text = mConnection.reader.GetValue(1).ToString();
                }
            }

            mConnection.cnMySQL.Close();

            if (mConnection.cnMySQL.State != ConnectionState.Closed)
                mConnection.cnMySQL.Close();

            if (mConnection.cnMySQL.State != ConnectionState.Open)
                mConnection.cnMySQL.Open();

            mConnection.cmdMySQL = new MySqlCommand("select * from acconti where idprenotazione=" + Convert.ToInt16(Prenotazione), mConnection.cnMySQL);
            mConnection.reader = mConnection.cmdMySQL.ExecuteReader();

            while (mConnection.reader.Read())
            {
                string[] thisrow = new string[4];// = "";                  
                string dataacc = mConnection.reader.GetValue(5).ToString();
                thisrow[0] = "Anticipo " + mConnection.reader.GetValue(4).ToString() + " rif." + mConnection.reader.GetValue(3).ToString();
                thisrow[1] = iva.ToString();
                thisrow[2] = String.Format("{0:0.00}", Convert.ToSingle(mConnection.reader.GetValue(1).ToString()));
                thisrow[3] = "";

                anticipotot += -Convert.ToSingle(thisrow[2]);
                // impostatot += Convert.ToInt16(thisrow[2]) * (float)iva / 100;

                //  thisrow[3] = mConnection.reader.GetValue(4).ToString();
                //dataGridView1.Items.Add(dataacc).SubItems.AddRange(thisrow);
                DataGridViewRow row = (DataGridViewRow)dataGridView1.Rows[0].Clone();
                row.Cells[0].Value = dataacc;
                row.Cells[1].Value = thisrow[0];
                row.Cells[2].Value = thisrow[1];
                row.Cells[3].Value = thisrow[2];
                dataGridView1.Rows.Add(row);
                itemcount++;
            }

            mConnection.cnMySQL.Close();

            if (mConnection.cnMySQL.State != ConnectionState.Closed)
                mConnection.cnMySQL.Close();

            if (mConnection.cnMySQL.State != ConnectionState.Open)
                mConnection.cnMySQL.Open();

            mConnection.cmdMySQL = new MySqlCommand("select * from conti where id=" + Convert.ToInt16(Prenotazione), mConnection.cnMySQL);
            mConnection.reader = mConnection.cmdMySQL.ExecuteReader();

            float alternateiva = 10;
            string[] Thisrow = new string[4];// = "";                  

            while (mConnection.reader.Read())
            {

                if (oldiva == 0)
                    oldiva = mConnection.reader.GetUInt16(3);
                string dataext = mConnection.reader.GetValue(6).ToString();
                Thisrow[0] = "(" + mConnection.reader.GetValue(5).ToString() + ") " + mConnection.reader.GetValue(1).ToString();

                /*if (mConnection.reader.GetValue(3).ToString() == "0")
                { 
                   // tds +=Convert.ToSingle(mConnection.reader.GetValue(2).ToString()) * Convert.ToSingle(mConnection.reader.GetValue(5).ToString());
                }*/
           
                Thisrow[1] = mConnection.reader.GetValue(3).ToString();
                Thisrow[2] = String.Format("{0:0.00}", Convert.ToSingle(mConnection.reader.GetValue(2).ToString()) * Convert.ToSingle(mConnection.reader.GetValue(5).ToString()));
                Thisrow[3] = "";
                if (mConnection.reader.GetValue(3).ToString() == "10")
                {
                    extratot += Convert.ToSingle(Thisrow[2]);
                    //  thisrow[3] = mConnection.reader.GetValue(4).ToString();
                    float miva = Convert.ToSingle(Thisrow[1]);
                    float costtot = Convert.ToSingle(Thisrow[2]);
                    float singlecost = costtot / (((100 + miva / 100)));
                    impostatot += singlecost;
                    // dataGridView1.Items.Add(dataext).SubItems.AddRange(Thisrow);
                    DataGridViewRow row = (DataGridViewRow)dataGridView1.Rows[0].Clone();
                    row.Cells[0].Value = dataext;
                    row.Cells[1].Value = Thisrow[0];
                    row.Cells[2].Value = Thisrow[1];
                    row.Cells[3].Value = Thisrow[2];
                    dataGridView1.Rows.Add(row);
                    itemcount++;

                }
                else
                {
                    if (mConnection.reader.GetValue(3).ToString() == "22")
                    {
                        extratot2 += Convert.ToSingle(Thisrow[2]);
                        //  thisrow[3] = mConnection.reader.GetValue(4).ToString();
                        impostatot2 += Convert.ToSingle(Thisrow[2]) - (Convert.ToSingle(Thisrow[2]) / (((100 + Convert.ToSingle(Thisrow[1])) / 100)));
                        DataGridViewRow row = (DataGridViewRow)dataGridView1.Rows[0].Clone();
                        row.Cells[0].Value = dataext;
                        row.Cells[1].Value = Thisrow[0];
                        row.Cells[2].Value = Thisrow[1];
                        row.Cells[3].Value = Thisrow[2];
                        dataGridView1.Rows.Add(row);
                        itemcount++;

                        //generate21row = true;
                    }

                    if (mConnection.reader.GetValue(3).ToString() == "0")
                    {
                        extratot3 += Convert.ToSingle(Thisrow[2]);
                        //  thisrow[3] = mConnection.reader.GetValue(4).ToString();
                       // impostatot2 += Convert.ToSingle(Thisrow[2]) - (Convert.ToSingle(Thisrow[2]) / (((100 + Convert.ToSingle(Thisrow[1])) / 100)));
                        DataGridViewRow row = (DataGridViewRow)dataGridView1.Rows[0].Clone();
                        row.Cells[0].Value = dataext;
                        row.Cells[1].Value = Thisrow[0];
                        row.Cells[2].Value = Thisrow[1];
                        row.Cells[3].Value = Thisrow[2];
                        dataGridView1.Rows.Add(row);
                        itemcount++;

                        //generate21row = true;
                    }
                }
                if (oldiva != Convert.ToSingle(Thisrow[1]))
                {
                    generate21row = true;
                    alternateiva = Convert.ToSingle(Thisrow[1]);
                }
                oldiva = Convert.ToInt32(Thisrow[1]);
                generate21row = true;

            }

            if (iva == 10)
            {
                Totale10 = prezzosog;
            }
            else
            {
                if (iva == 22)
                {
                    totale21 = prezzosog;
                }
            }
            float imponibiletot2;
            Totale10 += anticipotot + extratot;
            totale21 += extratot2;
            imponibiletot = Totale10 - impostatot;
            imponibiletot2 = totale21 - impostatot2;

            listView2.Items.Add("10");

            string item = String.Format("{0:0.00}", imponibiletot);
            listView2.Items[0].SubItems.Add(item);
            item = String.Format("{0:0.00}", impostatot);
            listView2.Items[0].SubItems.Add(item);
            if (iva == 10)
                listView2.Items[0].SubItems.Add(String.Format("{0:0.00}", Totale10 + totale21));
            else
                listView2.Items[0].SubItems.Add("");

            listView2.Items.Add("22");
            string item2 = String.Format("{0:0.00}", imponibiletot2);
            listView2.Items[1].SubItems.Add(item2);
            item2 = String.Format("{0:0.00}", impostatot2);
            listView2.Items[1].SubItems.Add(item2);
            if (iva == 22)
                listView2.Items[1].SubItems.Add(String.Format("{0:0.00}", totale21 + Totale10));
            else
                listView2.Items[1].SubItems.Add("");

            mConnection.cnMySQL.Close();

            itemcount = dataGridView1.Rows.Count;
            foreach (DataGridViewRow r in dataGridView1.Rows)
            {
                if (r.Cells[2].Value!= null)
                if (r.Cells[2].Value.ToString() =="0")
                {
                  //  MessageBox.Show("bravo ci sono delle tasse di soggiorno", "Arcipigna!", MessageBoxButtons.OK);

                        tds += Convert.ToSingle(r.Cells[3].Value.ToString());
                }
            }
        }


        int pages = 1;
        int itempoint1=0;

        int itempoint2 = 0;
        private string p;
        private bool p_2;
        private string cam;
        private bool skipsog;
        private bool generate21row;
        private float tds=0;
        private float extratot3;
        private float totale0;

        private void button1_Click(object sender, EventArgs e)
        {
            refresh2();
            if (!acconto && !notacred)
            {
                if (MessageBox.Show("Hai controllato le telefonate?", "Attenzione!!!", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                {
                    return;
                }

                if (MessageBox.Show("Sei sicuro?", "Attenzione!!!", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                {
                    return;
                }
                if (partenza.Value <= DateTime.Now)
                {
                    if (MessageBox.Show("Il Cliente lascia la camera", "Attenzione!!!", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {

                        //foreach (ListViewItem I in dataGridView1.SelectedItems)
                        //{

                        //    if (I.SubItems[8].Text == "Effettuato")
                        //    {

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
                                if (this.Text == "Fattura")
                                {
                                    progressivo.Text = mConnection.reader.GetValue(0).ToString();
                                }
                                if (this.Text == "Ricevuta")
                                {
                                    progressivo.Text = mConnection.reader.GetValue(1).ToString();
                                }
                            }

                            mConnection.cnMySQL.Close();

                            if (mConnection.cnMySQL.State != ConnectionState.Closed)
                                mConnection.cnMySQL.Close();
                            if (mConnection.cnMySQL.State != ConnectionState.Open)
                                mConnection.cnMySQL.Open();
                            mConnection.cmdMySQL = mConnection.cnMySQL.CreateCommand();

                            mConnection.cmdMySQL.CommandText = "UPDATE  camere SET pronta = false where Name='" + cam + "';";

                            mConnection.cmdMySQL.ExecuteNonQuery();
                            mConnection.cnMySQL.Close();
                            // refreshprenotazioni();

                            if (mConnection.cnMySQL.State != ConnectionState.Closed)
                                mConnection.cnMySQL.Close();
                            if (mConnection.cnMySQL.State != ConnectionState.Open)
                                mConnection.cnMySQL.Open();
                            mConnection.cmdMySQL = mConnection.cnMySQL.CreateCommand();

                            mConnection.cmdMySQL.CommandText = "UPDATE  prenotazioni SET checkout = '1',partenza='" + partenza.Value.ToShortDateString() + " " + partenza.Value.ToShortTimeString() + "' where prenotazioni.id=" + Convert.ToInt16(Prenotazione) + ";";
                            IPHostEntry host;
                            string localIP = "";
                            host = Dns.GetHostEntry(Dns.GetHostName());
                            foreach (IPAddress ip in host.AddressList)
                            {
                                if (ip.AddressFamily == AddressFamily.InterNetwork)
                                {
                                    localIP = ip.ToString();
                                    break;
                                }
                            }
                            mConnection.cmdMySQL.CommandText += "UPDATE  Beacon SET isdbmodified = True , IP='" + localIP + "' ;";
                            mConnection.cmdMySQL.ExecuteNonQuery();
                            mConnection.cnMySQL.Close();
                            //   refreshprenotazioni();
                            // }
                            //}


                            if (mConnection.cnMySQL.State != ConnectionState.Closed)
                                mConnection.cnMySQL.Close();
                            if (mConnection.cnMySQL.State != ConnectionState.Open)
                                mConnection.cnMySQL.Open();

                            bool check = false;

                            mConnection.cmdMySQL = new MySqlCommand(
                         "SELECT * FROM colazioni WHERE NumeroPrenotazione=" + Convert.ToInt16(Prenotazione) + ";", mConnection.cnMySQL);
                            mConnection.reader = mConnection.cmdMySQL.ExecuteReader();

                            while (mConnection.reader.Read())
                            {
                                check = true;
                            }
                            mConnection.cnMySQL.Close();

                            mConnection.cnMySQL.Close();



                            if (check)
                            {
                                if (mConnection.cnMySQL.State != ConnectionState.Closed)
                                    mConnection.cnMySQL.Close();
                                if (mConnection.cnMySQL.State != ConnectionState.Open)
                                    mConnection.cnMySQL.Open();
                                //checkout colazioni
                                mConnection.cmdMySQL.CommandText = "DELETE FROM colazioni where colazioni.NumeroPrenotazione=" + Convert.ToInt16(Prenotazione) + ";";
                                mConnection.cmdMySQL.ExecuteNonQuery();
                                mConnection.cnMySQL.Close();
                            }
                            else
                            {
                                MessageBox.Show("Non hai inserito persone per questa prenotazione!", "Attenzione!!!", MessageBoxButtons.OK);
                            }

                        }
                    }
                }
                if (checkBox1.Checked)
                {

                    string datas = DateTime.Now.Month.ToString() + "/" + DateTime.Now.Year.ToString();
                    bool isthereamonthalready=false;
                    if (mConnection != null)
                    {
                        if (mConnection.cnMySQL.State != ConnectionState.Closed)
                            mConnection.cnMySQL.Close();
                        if (mConnection.cnMySQL.State != ConnectionState.Open)
                            mConnection.cnMySQL.Open();
                        mConnection.cmdMySQL = new MySqlCommand("select * from incassi where Mese='" + datas + "'", mConnection.cnMySQL);
                        // get query results
                        mConnection.reader = mConnection.cmdMySQL.ExecuteReader();

                        // print the CustomerID of each record
                        while (mConnection.reader.Read())
                        {
                            string thisrow = "";
                            thisrow = mConnection.reader.GetValue(0).ToString();
                            if (thisrow != "")
                            {
                                isthereamonthalready = true;
                            }
                        }
                        mConnection.cnMySQL.Close();
                    }
                    if (!isthereamonthalready)
                    {

                        mConnection.cnMySQL.Open();
                        // mConnection.cmdMySQL = mConnection.cnMySQL.CreateCommand();

                        MySqlCommand cmd = mConnection.cnMySQL.CreateCommand();
                        cmd.CommandText = "INSERT INTO incassi (Mese,Incasso) VALUES('" + datas + "',0)";
                        cmd.ExecuteNonQuery();
                        cmd.Dispose();
                        mConnection.cnMySQL.Close();
                    }
                    bool isthereamonthalready2 = false;
                    if (mConnection != null)
                    {
                        if (mConnection.cnMySQL.State != ConnectionState.Closed)
                            mConnection.cnMySQL.Close();
                        if (mConnection.cnMySQL.State != ConnectionState.Open)
                            mConnection.cnMySQL.Open();
                        mConnection.cmdMySQL = new MySqlCommand("select * from storicotds where Mese='" + datas + "'", mConnection.cnMySQL);
                        // get query results
                        mConnection.reader = mConnection.cmdMySQL.ExecuteReader();

                        // print the CustomerID of each record
                        while (mConnection.reader.Read())
                        {
                            string thisrow = "";
                            thisrow = mConnection.reader.GetValue(0).ToString();
                            if (thisrow != "")
                            {
                                isthereamonthalready2 = true;
                            }
                        }
                        mConnection.cnMySQL.Close();
                    }
                    if (!isthereamonthalready2)
                    {

                        mConnection.cnMySQL.Open();
                        // mConnection.cmdMySQL = mConnection.cnMySQL.CreateCommand();

                        MySqlCommand cmd = mConnection.cnMySQL.CreateCommand();
                        cmd.CommandText = "INSERT INTO storicotds (Mese,Incasso) VALUES('" + datas + "',0)";
                        cmd.ExecuteNonQuery();
                        cmd.Dispose();
                        mConnection.cnMySQL.Close();
                    }
                     mConnection.cnMySQL.Open();
                        // mConnection.cmdMySQL = mConnection.cnMySQL.CreateCommand();

                        MySqlCommand mcmd = mConnection.cnMySQL.CreateCommand();
                        float totalesup = totale21 + Totale10;
                        mcmd.CommandText = "update incassi set Incasso= Incasso+'"+totalesup+"' where Mese='"+datas+"' ;";
                        mcmd.ExecuteNonQuery();
                        mcmd.Dispose();
                        mConnection.cnMySQL.Close();

                        mConnection.cnMySQL.Open();
                    
                        MySqlCommand mcmd2 = mConnection.cnMySQL.CreateCommand();
                   
                        mcmd2.CommandText = "update storicotds set Incasso= Incasso+'" + tds + "' where Mese='" + datas + "' ;";
                        mcmd2.ExecuteNonQuery();
                        mcmd2.Dispose();
                        mConnection.cnMySQL.Close();
                    if (MessageBox.Show("Vuoi eliminare il cliente?", "Attenzione!!!", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        if (mConnection != null)
                        {
                            if (mConnection.cnMySQL.State != ConnectionState.Closed)
                                mConnection.cnMySQL.Close();
                            if (mConnection.cnMySQL.State != ConnectionState.Open)
                                mConnection.cnMySQL.Open();
                            mConnection.cmdMySQL = mConnection.cnMySQL.CreateCommand();

                            mConnection.cmdMySQL.CommandText = "DELETE FROM clienti where id=" + cliente + ";";

                            mConnection.cmdMySQL.ExecuteNonQuery();
                            mConnection.cnMySQL.Close();
                     
                        }
                    }
                }
            }
            else
            {
                if (MessageBox.Show("Sei sicuro?", "Attenzione!!!", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                {
                    return;
                }
            }

            Document FatturaP = new Document(PageSize.A4, 15f, 15f, 142, 15f);
            FatturaP.SetPageSize(iTextSharp.text.PageSize.A4.Rotate());
           
            PdfWriter writer ;
            if (Text == "Fattura")
            {

                string filepath = "./Fatture/" + this.Text.ToString() + " " + progressivo.Text.ToString() + " " + fullname + " " + DateTime.Now.Day + "-" + DateTime.Now.Month + "-" + DateTime.Now.Year + ".pdf";
                writer =  PdfWriter.GetInstance(FatturaP, new FileStream(filepath, FileMode.Create));
            }
            else
            {
                if (Text == "Ricevuta")
                {
                    string filepath = "./Ricevute/" + this.Text.ToString() + " " + progressivo.Text.ToString() + " " + fullname + " " + DateTime.Now.Day + "-" + DateTime.Now.Month + "-" + DateTime.Now.Year + ".pdf";
                    writer = PdfWriter.GetInstance(FatturaP, new FileStream(filepath, FileMode.Create));
                }
                else
                {
                    string filepath = "./NoteCredito/" + this.Text.ToString() + " " + progressivo.Text.ToString() + " " + fullname + " " + DateTime.Now.Day + "-" + DateTime.Now.Month + "-" + DateTime.Now.Year + ".pdf";
                    writer = PdfWriter.GetInstance(FatturaP, new FileStream(filepath, FileMode.Create));
           
                }
            }
            
            FatturaP.Open();
            pages = 1;
            for (int j = 0; j < itemcount; j++)
            {
                if (j % 10 == 0 && j != 0)
                {
                    pages++;
                }
            }
            itempoint1 = 0;
            itempoint2 = 0;
            for(int i=0;i<pages;i++)
            {
             FatturaP.NewPage();
             int itemcounter = 0;

             Paragraph pIntestazione = new Paragraph(Text.ToString() + " N°" + progressivo.Text.ToString() + "         Data:" + DateTime.Now.ToShortDateString() + "\n" + Intestazione.Text.ToString() + "\nP.iva:" + codicefiscale.Text);

             if (this.Text == "Fattura")
             {
                 pIntestazione = new Paragraph(Text.ToString() + " N°" + progressivo.Text.ToString() + "         Data:" + DateTime.Now.ToShortDateString() + "\n" + Intestazione.Text.ToString() + "\nP.iva:" + codicefiscale.Text);
             }
             else
             {
                 if (this.Text == "Ricevuta")
                     pIntestazione = new Paragraph(Text.ToString() + " N°" + progressivo.Text.ToString() + "         Data:" + DateTime.Now.ToShortDateString() + "\n" + Intestazione.Text.ToString() + "\nC.fisc:" + codicefiscale.Text);

             }
                pIntestazione.Font.Size = 10;
                Paragraph vuoto=new Paragraph(" ");
                vuoto.Font.Size=10;
                PdfPCell spacing = new PdfPCell(vuoto);
                spacing.BorderColorTop = BaseColor.WHITE;
                spacing.BorderColorBottom = BaseColor.WHITE;

                Paragraph seg = new Paragraph("segue a pagina" + i.ToString());
                seg.Font.Size = 10;
             PdfPCell segue = new PdfPCell(seg);
             spacing.Colspan = 4;
             segue.Colspan = 4;
             segue.BorderColorTop = BaseColor.WHITE;
             segue.BorderColorBottom = BaseColor.WHITE;
             pIntestazione.Font.SetStyle(1);
             pIntestazione.Alignment = Element.ALIGN_LEFT;

             PdfPTable table = new PdfPTable(4) {WidthPercentage=100 };
             table.TotalWidth = 390;
             table.LockedWidth = true;
             var colWidthPercentages = new[] { 16, 54, 10,20 };
             table.SetWidths(colWidthPercentages);
             PdfPCell cell = new PdfPCell(pIntestazione);
             cell.Colspan = 4;
             cell.HorizontalAlignment = 0;
             cell.BorderWidth = 0.1f;
           
                spacing.BorderWidth=0.1f;
                table.AddCell(cell);
                table.AddCell(spacing);
                Paragraph currentCell = new Paragraph("Data");
                currentCell.Font.Size = 10;
                PdfPCell definedcell = new PdfPCell(currentCell);
                definedcell.BorderWidth = 0.1f;
             table.AddCell(definedcell);
             currentCell = new Paragraph("Descrizione");
             currentCell.Font.Size = 10;
             definedcell = new PdfPCell(currentCell);
             table.AddCell(definedcell);
             currentCell = new Paragraph("Iva");
             currentCell.Font.Size = 10;
             definedcell = new PdfPCell(currentCell);
             //definedcell.Width=44;
             table.AddCell(definedcell);
             currentCell = new Paragraph("Prezzo");
             currentCell.Font.Size = 10;
             definedcell = new PdfPCell(currentCell);
             table.AddCell(definedcell);
             for (int t = itempoint1; t < itemcount; t++)
             {
                 if (itemcounter > 20)
                     break;
                 if (dataGridView1.Rows[t].Cells[0].Value != null)
                 {
                     currentCell = new Paragraph(dataGridView1.Rows[t].Cells[0].Value.ToString());
                     currentCell.Font.Size = 10;
                     definedcell = new PdfPCell(currentCell);
                     definedcell.BorderWidth = 0.1f;
                     definedcell.BorderWidthTop = 0;
                     definedcell.BorderWidthBottom = 0;
                     table.AddCell(definedcell);

                     currentCell = new Paragraph(dataGridView1.Rows[t].Cells[1].Value.ToString());
                     currentCell.Font.Size = 10;
                     definedcell = new PdfPCell(currentCell);
                     definedcell.BorderWidth = 0.1f;
                     definedcell.BorderWidthTop = 0;
                     definedcell.BorderWidthBottom = 0;
                     table.AddCell(definedcell);

                     currentCell = new Paragraph(dataGridView1.Rows[t].Cells[2].Value.ToString());
                     currentCell.Font.Size = 10;
                     definedcell = new PdfPCell(currentCell);
                     definedcell.BorderWidth = 0.1f;
                     definedcell.BorderWidthTop = 0;
                     definedcell.BorderWidthBottom = 0;
                     table.AddCell(definedcell);

                     currentCell = new Paragraph(dataGridView1.Rows[t].Cells[3].Value.ToString());
                     currentCell.Font.Size = 10;
                     definedcell = new PdfPCell(currentCell);
                     definedcell.BorderWidth = 0.1f;
                     definedcell.BorderWidthTop = 0;
                     definedcell.BorderWidthBottom = 0;
                     table.AddCell(definedcell);


                     itempoint1++;
                     itemcounter++;
                 }
             }
             //if (itempoint1 < 10*(i+1))
             //{
             for (int t = itempoint1; t < 20 * (i + 1); t++)
                 {
                     definedcell = new PdfPCell(vuoto);
                     definedcell.BorderWidth = 0.1f;
                     definedcell.BorderWidthTop = 0;
                     definedcell.BorderWidthBottom = 0;
                     table.AddCell(definedcell);
                     table.AddCell(definedcell);
                     table.AddCell(definedcell);
                     table.AddCell(definedcell);

                     
                 }
             //}
             if (i > 0)
             {
                 table.AddCell(segue);
             }
             else
             {
                 
             segue.BorderColorTop = BaseColor.BLACK;
             segue.BorderColorBottom = BaseColor.BLACK;
              //   table.AddCell(spacing);
             }
             float h=table.TotalHeight;
             table.WriteSelectedRows(0, -1, FatturaP.Left, FatturaP.Top, writer.DirectContent);  //footer
             table = new PdfPTable(4);
             table.TotalWidth = 390;
             table.LockedWidth = true;
                colWidthPercentages = new[] { 10, 30, 30, 30 };
             table.SetWidths(colWidthPercentages);

             currentCell = new Paragraph("Iva");
             currentCell.Font.Size = 10;
             definedcell = new PdfPCell(currentCell);
             table.AddCell(definedcell);
             currentCell = new Paragraph("Imponibile");
             currentCell.Font.Size = 10;
             definedcell = new PdfPCell(currentCell);
             table.AddCell(definedcell);
             currentCell = new Paragraph("Imposta");
             currentCell.Font.Size = 10;
             definedcell = new PdfPCell(currentCell);
             table.AddCell(definedcell);
             currentCell = new Paragraph("Totale Euro");
             currentCell.Font.Size = 10;
             definedcell = new PdfPCell(currentCell);
             table.AddCell(definedcell);
             currentCell = new Paragraph(listView2.Items[0].Text);
             currentCell.Font.Size = 10;
             definedcell = new PdfPCell(currentCell);
             table.AddCell(definedcell);
             Paragraph Totalepar = new Paragraph(listView2.Items[0].SubItems[3].Text);
                
             if (this.Text == "Fattura" || this.Text=="Nota Credito")
             {
                 currentCell = new Paragraph(listView2.Items[0].SubItems[1].Text);
                 currentCell.Font.Size = 10;
                 definedcell = new PdfPCell(currentCell);
                 table.AddCell(definedcell);
                 currentCell = new Paragraph(listView2.Items[0].SubItems[2].Text);
                 currentCell.Font.Size = 10;
                 definedcell = new PdfPCell(currentCell);
                 
                 table.AddCell(definedcell);
                 if (iva == 10)
                 {
                     Totalepar = new Paragraph(listView2.Items[0].SubItems[3].Text);
                     Totalepar.Font.SetStyle(1);
                     Totalepar.Font.Size = 10;
                     table.AddCell(Totalepar);
                 }
                 else
                 {
                     Totalepar = new Paragraph("");
                     Totalepar.Font.SetStyle(1);
                     Totalepar.Font.Size = 10;
                     table.AddCell(Totalepar);
                 }
                 if (listView2.Items.Count > 1)
                 {
                     currentCell = new Paragraph(listView2.Items[1].SubItems[0].Text);
                     currentCell.Font.Size = 10;
                     definedcell = new PdfPCell(currentCell);
                     table.AddCell(definedcell);
                     currentCell = new Paragraph(listView2.Items[1].SubItems[1].Text);
                     currentCell.Font.Size = 10;
                     definedcell = new PdfPCell(currentCell);
                     table.AddCell(definedcell);
                     currentCell = new Paragraph(listView2.Items[1].SubItems[2].Text);
                     currentCell.Font.Size = 10;
                     definedcell = new PdfPCell(currentCell);
                     table.AddCell(definedcell);
                     if (iva == 22)
                     {
                         Totalepar = new Paragraph(listView2.Items[1].SubItems[3].Text);
                         Totalepar.Font.SetStyle(1);
                         Totalepar.Font.Size = 10;
                         table.AddCell(Totalepar);
                     }
                     else
                     {
                         Totalepar = new Paragraph("");
                         Totalepar.Font.SetStyle(1);
                         Totalepar.Font.Size = 10;
                         table.AddCell(Totalepar);
                     }
                 }
             }
             else
             {
                 table.AddCell(vuoto);
                 table.AddCell(vuoto);
                 Totalepar = new Paragraph(listView2.Items[0].SubItems[3].Text);
                 Totalepar.Font.SetStyle(1);
                 Totalepar.Font.Size = 10;
                 table.AddCell(Totalepar);
             }
                  
             table.WriteSelectedRows(0, -1, FatturaP.Left, FatturaP.Top - h, writer.DirectContent);  //footer

             itemcounter = 0;
             table = new PdfPTable(4);
             table.TotalWidth = 390;
              colWidthPercentages = new[] { 16, 54, 10, 20 };
             table.SetWidths(colWidthPercentages);
             cell = new PdfPCell(pIntestazione);
             cell.Colspan = 4;
             cell.HorizontalAlignment = 0;
             cell.BorderWidth = 0.1f;

             spacing.BorderWidth = 0.1f;
             table.AddCell(cell);
             table.AddCell(spacing);
             currentCell = new Paragraph("Data");
             currentCell.Font.Size = 10;
             definedcell = new PdfPCell(currentCell);
             definedcell.BorderWidth = 0.1f;
             table.AddCell(definedcell);
             currentCell = new Paragraph("Descrizione");
             currentCell.Font.Size = 10;
             definedcell = new PdfPCell(currentCell);
             table.AddCell(definedcell);
             currentCell = new Paragraph("Iva");
             currentCell.Font.Size = 10;
             definedcell = new PdfPCell(currentCell);
             //definedcell.Width=44;
             table.AddCell(definedcell);
             currentCell = new Paragraph("Prezzo");
             currentCell.Font.Size = 10;
             definedcell = new PdfPCell(currentCell);
             table.AddCell(definedcell);
             for (int t = itempoint2; t < itemcount; t++)
             {
                 if (itemcounter > 20)
                     break;
                 if (dataGridView1.Rows[t].Cells[0].Value != null)
                 {
                     currentCell = new Paragraph(dataGridView1.Rows[t].Cells[0].Value.ToString());
                     currentCell.Font.Size = 10;
                     definedcell = new PdfPCell(currentCell);
                     definedcell.BorderWidth = 0.1f;
                     definedcell.BorderWidthTop = 0;
                     definedcell.BorderWidthBottom = 0;
                     table.AddCell(definedcell);

                     currentCell = new Paragraph(dataGridView1.Rows[t].Cells[1].Value.ToString());
                     currentCell.Font.Size = 10;
                     definedcell = new PdfPCell(currentCell);
                     definedcell.BorderWidth = 0.1f;
                     definedcell.BorderWidthTop = 0;
                     definedcell.BorderWidthBottom = 0;
                     table.AddCell(definedcell);

                     currentCell = new Paragraph(dataGridView1.Rows[t].Cells[2].Value.ToString());
                     currentCell.Font.Size = 10;
                     definedcell = new PdfPCell(currentCell);
                     definedcell.BorderWidth = 0.1f;
                     definedcell.BorderWidthTop = 0;
                     definedcell.BorderWidthBottom = 0;
                     table.AddCell(definedcell);

                     currentCell = new Paragraph(dataGridView1.Rows[t].Cells[3].Value.ToString());
                     currentCell.Font.Size = 10;
                     definedcell = new PdfPCell(currentCell);
                     definedcell.BorderWidth = 0.1f;
                     definedcell.BorderWidthTop = 0;
                     definedcell.BorderWidthBottom = 0;
                     table.AddCell(definedcell);


                     itempoint2++;
                     itemcounter++;
                 }
             }
             //if (itempoint1 < 10*(i+1))
             //{
             for (int t = itempoint2; t < 20 * (i + 1); t++)
             {
                 definedcell = new PdfPCell(vuoto);
                 definedcell.BorderWidth = 0.1f;
                 definedcell.BorderWidthTop = 0;
                 definedcell.BorderWidthBottom = 0;
                 table.AddCell(definedcell);
                 table.AddCell(definedcell);
                 table.AddCell(definedcell);
                 table.AddCell(definedcell);


             }
             //}
             if (i > 0)
             {
                 table.AddCell(segue);
             }
             else
             {
             //    table.AddCell(spacing);
             }
            // h = table.TotalHeight;
             table.WriteSelectedRows(0, -1, FatturaP.Right - 395, FatturaP.Top, writer.DirectContent);  //footer
     
             table = new PdfPTable(4);
             table.TotalWidth = 390;
             table.LockedWidth = true;
             colWidthPercentages = new[] { 10, 30, 30, 30 };
             table.SetWidths(colWidthPercentages);

             currentCell = new Paragraph("Iva");
             currentCell.Font.Size = 10;
             definedcell = new PdfPCell(currentCell);
             table.AddCell(definedcell);
             currentCell = new Paragraph("Imponibile");
             currentCell.Font.Size = 10;
             definedcell = new PdfPCell(currentCell);
             table.AddCell(definedcell);
             currentCell = new Paragraph("Imposta");
             currentCell.Font.Size = 10;
             definedcell = new PdfPCell(currentCell);
             table.AddCell(definedcell);
             currentCell = new Paragraph("Totale Euro");
             currentCell.Font.Size = 10;
             definedcell = new PdfPCell(currentCell);
             table.AddCell(definedcell);
             currentCell = new Paragraph(listView2.Items[0].Text);
             currentCell.Font.Size = 10;
             definedcell = new PdfPCell(currentCell);
             table.AddCell(definedcell);
              Totalepar = new Paragraph(listView2.Items[0].SubItems[3].Text);

             if (this.Text == "Fattura" || this.Text == "Nota Credito")
             {
                 currentCell = new Paragraph(listView2.Items[0].SubItems[1].Text);
                 currentCell.Font.Size = 10;
                 definedcell = new PdfPCell(currentCell);
                 table.AddCell(definedcell);
                 currentCell = new Paragraph(listView2.Items[0].SubItems[2].Text);
                 currentCell.Font.Size = 10;
                 definedcell = new PdfPCell(currentCell);

                 table.AddCell(definedcell);
                 if (iva == 10)
                 {
                     Totalepar = new Paragraph(listView2.Items[0].SubItems[3].Text);
                     Totalepar.Font.SetStyle(1);
                     Totalepar.Font.Size = 10;
                     table.AddCell(Totalepar);
                 }
                 else
                 {
                     Totalepar = new Paragraph("");
                     Totalepar.Font.SetStyle(1);
                     Totalepar.Font.Size = 10;
                     table.AddCell(Totalepar);
                 }

                 if (listView2.Items.Count > 1)
                 {
                     currentCell = new Paragraph(listView2.Items[1].SubItems[0].Text);
                     currentCell.Font.Size = 10;
                     definedcell = new PdfPCell(currentCell);
                     table.AddCell(definedcell);
                     currentCell = new Paragraph(listView2.Items[1].SubItems[1].Text);
                     currentCell.Font.Size = 10;
                     definedcell = new PdfPCell(currentCell);
                     table.AddCell(definedcell);
                     currentCell = new Paragraph(listView2.Items[1].SubItems[2].Text);
                     currentCell.Font.Size = 10;
                     definedcell = new PdfPCell(currentCell);
                     table.AddCell(definedcell);
                     if (iva == 22)
                     {
                         Totalepar = new Paragraph(listView2.Items[1].SubItems[3].Text);
                         Totalepar.Font.SetStyle(1);
                         Totalepar.Font.Size = 10;
                         table.AddCell(Totalepar);
                     }
                     else
                     {
                         Totalepar = new Paragraph("");
                         Totalepar.Font.SetStyle(1);
                         Totalepar.Font.Size = 10;
                         table.AddCell(Totalepar);
                     }
                 }
             }
             else
             {
                 table.AddCell(vuoto);
                 table.AddCell(vuoto);
                 Totalepar = new Paragraph(listView2.Items[0].SubItems[3].Text);
                 Totalepar.Font.SetStyle(1);
                 Totalepar.Font.Size = 10;
                 table.AddCell(Totalepar);
             }


             table.WriteSelectedRows(0, -1, FatturaP.Right - 395, FatturaP.Top-h, writer.DirectContent);  //footer
             //FatturaP.Add(table);
            }
         


            FatturaP.Close();
               string path;
             if (Text == "Fattura")
            {

                path = "./Fatture/" + this.Text.ToString() + " " + progressivo.Text.ToString() + " " + fullname + " " + DateTime.Now.Day + "-" + DateTime.Now.Month + "-" + DateTime.Now.Year + ".pdf";
             }
            else
            {
                if (this.Text == "Ricevuta")
                {
                    path = "./Ricevute/" + this.Text.ToString() + " " + progressivo.Text.ToString() + " " + fullname + " " + DateTime.Now.Day + "-" + DateTime.Now.Month + "-" + DateTime.Now.Year + ".pdf";
                }
                else
                {
                    path = "./NoteCredito/" + this.Text.ToString() + " " + progressivo.Text.ToString() + " " + fullname + " " + DateTime.Now.Day + "-" + DateTime.Now.Month + "-" + DateTime.Now.Year + ".pdf";

                }
             }

           Byte[] file= File.ReadAllBytes(path); //test files
   
            PdfReader reader = new PdfReader(file);
            int pageCount = reader.NumberOfPages;
            iTextSharp.text.Rectangle pageSize = reader.GetPageSize(1);

            Document pdf = new Document(pageSize);
           // pdf.SetPageSize(iTextSharp.text.PageSize.A4.Rotate());
            PdfWriter writer2 = PdfWriter.GetInstance(pdf, new FileStream(path, FileMode.Create));
            pdf.Open();

            //This action leads directly to printer dialogue
            PdfAction jAction = PdfAction.JavaScript("this.print(true);\r", writer2);
            writer2.AddJavaScript(jAction);
            //Omitting this loop and simply adding some text to the file produces the behavior I want.
            for (int i = 0; i < pageCount; i++)
            {
                pdf.NewPage();
                PdfImportedPage page = writer2.GetImportedPage(reader, i + 1);
                writer2.DirectContent.AddTemplate(page, 0, 0);
            }
            pdf.Close();

            string spath;
            if (Text == "Fattura")
            {
                if (mConnection != null)
                {
                    if (mConnection.cnMySQL.State != ConnectionState.Closed)
                        mConnection.cnMySQL.Close();

                    if (mConnection.cnMySQL.State != ConnectionState.Open)
                        mConnection.cnMySQL.Open();
                    mConnection.cmdMySQL = mConnection.cnMySQL.CreateCommand();

                    mConnection.cmdMySQL.CommandText = "UPDATE progressivi SET Fatture=" + (Convert.ToInt16(progressivo.Text) + 1);

                    mConnection.cmdMySQL.ExecuteNonQuery();

                    mConnection.cnMySQL.Close();
                    if (checkBox1.Checked && !Acc)
                    {

                        if (mConnection.cnMySQL.State != ConnectionState.Closed)
                            mConnection.cnMySQL.Close();

                        if (mConnection.cnMySQL.State != ConnectionState.Open)
                            mConnection.cnMySQL.Open();
                        mConnection.cmdMySQL = mConnection.cnMySQL.CreateCommand();

                        mConnection.cmdMySQL.CommandText = "DELETE FROM conti WHERE id=" + (Convert.ToInt16(Prenotazione)) + ";DELETE FROM acconti WHERE idprenotazione=" + (Convert.ToInt16(Prenotazione));

                        mConnection.cmdMySQL.ExecuteNonQuery();

                        mConnection.cnMySQL.Close();
                    }
                }
                spath = System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "/Fatture/" + this.Text.ToString() + " " + progressivo.Text.ToString() + " " + fullname + " " + DateTime.Now.Day + "-" + DateTime.Now.Month + "-" + DateTime.Now.Year + ".pdf";
            }
            else
            {
                if (this.Text == "Ricevuta")
                {
                    if (mConnection != null)
                    {
                        if (mConnection.cnMySQL.State != ConnectionState.Closed)
                            mConnection.cnMySQL.Close();

                        if (mConnection.cnMySQL.State != ConnectionState.Open)
                            mConnection.cnMySQL.Open();
                        mConnection.cmdMySQL = mConnection.cnMySQL.CreateCommand();

                        mConnection.cmdMySQL.CommandText = "UPDATE progressivi SET Ricevute=" + (Convert.ToInt16(progressivo.Text) + 1);

                        mConnection.cmdMySQL.ExecuteNonQuery();

                        mConnection.cnMySQL.Close();

                        if (checkBox1.Checked && !Acc)
                        {
                            if (mConnection.cnMySQL.State != ConnectionState.Closed)
                                mConnection.cnMySQL.Close();

                            if (mConnection.cnMySQL.State != ConnectionState.Open)
                                mConnection.cnMySQL.Open();
                            mConnection.cmdMySQL = mConnection.cnMySQL.CreateCommand();

                            mConnection.cmdMySQL.CommandText = "DELETE FROM conti WHERE id=" + (Convert.ToInt16(Prenotazione)) + ";DELETE FROM acconti WHERE idprenotazione=" + (Convert.ToInt16(Prenotazione));

                            mConnection.cmdMySQL.ExecuteNonQuery();

                            mConnection.cnMySQL.Close();
                        }
                    }
                    spath = System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "/Ricevute/" + this.Text.ToString() + " " + progressivo.Text.ToString() + " " + fullname + " " + DateTime.Now.Day + "-" + DateTime.Now.Month + "-" + DateTime.Now.Year + ".pdf";
                }
                else
                {
                    if (mConnection != null)
                    {
                        if (mConnection.cnMySQL.State != ConnectionState.Closed)
                            mConnection.cnMySQL.Close();

                        if (mConnection.cnMySQL.State != ConnectionState.Open)
                            mConnection.cnMySQL.Open();
                        mConnection.cmdMySQL = mConnection.cnMySQL.CreateCommand();

                        mConnection.cmdMySQL.CommandText = "UPDATE progressivi SET notecredito=" + (Convert.ToInt16(progressivo.Text) + 1);

                        mConnection.cmdMySQL.ExecuteNonQuery();

                        mConnection.cnMySQL.Close();
                    }
                    spath = System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "/NoteCredito/" + this.Text.ToString() + " " + progressivo.Text.ToString() + " " + fullname + " " + DateTime.Now.Day + "-" + DateTime.Now.Month + "-" + DateTime.Now.Year + ".pdf";
                }
            }
            System.Diagnostics.Process.Start(spath);

            if (checkBox1.Checked && this.Text != "Nota Credito" && !Acc)
            {
                mConnection.cnMySQL.Open();
                mConnection.cmdMySQL = mConnection.cnMySQL.CreateCommand();

                mConnection.cmdMySQL.CommandText = "UPDATE prenotazioni SET pagata = True WHERE id = " + Prenotazione;
                mConnection.cmdMySQL.ExecuteNonQuery();

                mConnection.cnMySQL.Close();
                // refreshlist();
            }
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {

           
               

               
               
          

            Document FatturaP = new Document(PageSize.A4, 15f, 15f, 142, 15f);
            FatturaP.SetPageSize(iTextSharp.text.PageSize.A4.Rotate());

            PdfWriter writer;
            if (Text == "Fattura")
            {

                string filepath = "./Fatture/" + this.Text.ToString() + " " + progressivo.Text.ToString() + " " + fullname + " " + DateTime.Now.Day + "-" + DateTime.Now.Month + "-" + DateTime.Now.Year + ".pdf";
                writer = PdfWriter.GetInstance(FatturaP, new FileStream(filepath, FileMode.Create));
            }
            else
            {
                if (Text == "Ricevuta")
                {
                    string filepath = "./Ricevute/" + this.Text.ToString() + " " + progressivo.Text.ToString() + " " + fullname + " " + DateTime.Now.Day + "-" + DateTime.Now.Month + "-" + DateTime.Now.Year + ".pdf";
                    writer = PdfWriter.GetInstance(FatturaP, new FileStream(filepath, FileMode.Create));
                }
                else
                {
                    string filepath = "./NoteCredito/" + this.Text.ToString() + " " + progressivo.Text.ToString() + " " + fullname + " " + DateTime.Now.Day + "-" + DateTime.Now.Month + "-" + DateTime.Now.Year + ".pdf";
                    writer = PdfWriter.GetInstance(FatturaP, new FileStream(filepath, FileMode.Create));
                    iva = 10;
                }
            }

            FatturaP.Open();
            pages = 1;
            for (int j = 0; j < itemcount; j++)
            {
                if (j % 10 == 0 && j != 0)
                {
                    pages++;
                }
            }
            itempoint1 = 0;
            itempoint2 = 0;
            for (int i = 0; i < pages; i++)
            {
                FatturaP.NewPage();
                int itemcounter = 0;

                Paragraph pIntestazione = new Paragraph(Text.ToString() + " N°" + progressivo.Text.ToString() + "         Data:" + DateTime.Now.ToShortDateString() + "\n" + Intestazione.Text.ToString() + "\nP.iva:" + codicefiscale.Text);

                if (this.Text == "Fattura")
                {
                    pIntestazione = new Paragraph(Text.ToString() + " N°" + progressivo.Text.ToString() + "         Data:" + DateTime.Now.ToShortDateString() + "\n" + Intestazione.Text.ToString() + "\nP.iva:" + codicefiscale.Text);
                }
                else
                {
                    if (this.Text == "Ricevuta")
                        pIntestazione = new Paragraph(Text.ToString() + " N°" + progressivo.Text.ToString() + "         Data:" + DateTime.Now.ToShortDateString() + "\n" + Intestazione.Text.ToString() + "\nC.fisc:" + codicefiscale.Text);

                }
                pIntestazione.Font.Size = 10;
                Paragraph vuoto = new Paragraph(" ");
                vuoto.Font.Size = 10;
                PdfPCell spacing = new PdfPCell(vuoto);
                spacing.BorderColorTop = BaseColor.WHITE;
                spacing.BorderColorBottom = BaseColor.WHITE;

                Paragraph seg = new Paragraph("segue a pagina" + i.ToString());
                seg.Font.Size = 10;
                PdfPCell segue = new PdfPCell(seg);
                spacing.Colspan = 4;
                segue.Colspan = 4;
                segue.BorderColorTop = BaseColor.WHITE;
                segue.BorderColorBottom = BaseColor.WHITE;
                pIntestazione.Font.SetStyle(1);
                pIntestazione.Alignment = Element.ALIGN_LEFT;

                PdfPTable table = new PdfPTable(4) { WidthPercentage = 100 };
                table.TotalWidth = 390;
                table.LockedWidth = true;
                var colWidthPercentages = new[] { 16, 54, 10, 20 };
                table.SetWidths(colWidthPercentages);
                PdfPCell cell = new PdfPCell(pIntestazione);
                cell.Colspan = 4;
                cell.HorizontalAlignment = 0;
                cell.BorderWidth = 0.1f;

                spacing.BorderWidth = 0.1f;
                table.AddCell(cell);
                table.AddCell(spacing);
                Paragraph currentCell = new Paragraph("Data");
                currentCell.Font.Size = 10;
                PdfPCell definedcell = new PdfPCell(currentCell);
                definedcell.BorderWidth = 0.1f;
                table.AddCell(definedcell);
                currentCell = new Paragraph("Descrizione");
                currentCell.Font.Size = 10;
                definedcell = new PdfPCell(currentCell);
                table.AddCell(definedcell);
                currentCell = new Paragraph("Iva");
                currentCell.Font.Size = 10;
                definedcell = new PdfPCell(currentCell);
                //definedcell.Width=44;
                table.AddCell(definedcell);
                currentCell = new Paragraph("Prezzo");
                currentCell.Font.Size = 10;
                definedcell = new PdfPCell(currentCell);
                table.AddCell(definedcell);
                for (int t = itempoint1; t < itemcount; t++)
                {
                    if (itemcounter > 20)
                        break;
                    if (dataGridView1.Rows[t].Cells[0].Value != null)
                    {
                        currentCell = new Paragraph(dataGridView1.Rows[t].Cells[0].Value.ToString());
                        currentCell.Font.Size = 10;
                        definedcell = new PdfPCell(currentCell);
                        definedcell.BorderWidth = 0.1f;
                        definedcell.BorderWidthTop = 0;
                        definedcell.BorderWidthBottom = 0;
                        table.AddCell(definedcell);

                        currentCell = new Paragraph(dataGridView1.Rows[t].Cells[1].Value.ToString());
                        currentCell.Font.Size = 10;
                        definedcell = new PdfPCell(currentCell);
                        definedcell.BorderWidth = 0.1f;
                        definedcell.BorderWidthTop = 0;
                        definedcell.BorderWidthBottom = 0;
                        table.AddCell(definedcell);

                        currentCell = new Paragraph(dataGridView1.Rows[t].Cells[2].Value.ToString());
                        currentCell.Font.Size = 10;
                        definedcell = new PdfPCell(currentCell);
                        definedcell.BorderWidth = 0.1f;
                        definedcell.BorderWidthTop = 0;
                        definedcell.BorderWidthBottom = 0;
                        table.AddCell(definedcell);

                        currentCell = new Paragraph(dataGridView1.Rows[t].Cells[3].Value.ToString());
                        currentCell.Font.Size = 10;
                        definedcell = new PdfPCell(currentCell);
                        definedcell.BorderWidth = 0.1f;
                        definedcell.BorderWidthTop = 0;
                        definedcell.BorderWidthBottom = 0;
                        table.AddCell(definedcell);


                        itempoint1++;
                        itemcounter++;
                    }
                }
                //if (itempoint1 < 10*(i+1))
                //{
                for (int t = itempoint1; t < 20 * (i + 1); t++)
                {
                    definedcell = new PdfPCell(vuoto);
                    definedcell.BorderWidth = 0.1f;
                    definedcell.BorderWidthTop = 0;
                    definedcell.BorderWidthBottom = 0;
                    table.AddCell(definedcell);
                    table.AddCell(definedcell);
                    table.AddCell(definedcell);
                    table.AddCell(definedcell);


                }
                //}
                if (i > 0)
                {
                    table.AddCell(segue);
                }
                else
                {

                    segue.BorderColorTop = BaseColor.BLACK;
                    segue.BorderColorBottom = BaseColor.BLACK;
                    //   table.AddCell(spacing);
                }
                float h = table.TotalHeight;
                table.WriteSelectedRows(0, -1, FatturaP.Left, FatturaP.Top, writer.DirectContent);  //footer
                table = new PdfPTable(4);
                table.TotalWidth = 390;
                table.LockedWidth = true;
                colWidthPercentages = new[] { 10, 30, 30, 30 };
                table.SetWidths(colWidthPercentages);

                currentCell = new Paragraph("Iva");
                currentCell.Font.Size = 10;
                definedcell = new PdfPCell(currentCell);
                table.AddCell(definedcell);
                currentCell = new Paragraph("Imponibile");
                currentCell.Font.Size = 10;
                definedcell = new PdfPCell(currentCell);
                table.AddCell(definedcell);
                currentCell = new Paragraph("Imposta");
                currentCell.Font.Size = 10;
                definedcell = new PdfPCell(currentCell);
                table.AddCell(definedcell);
                currentCell = new Paragraph("Totale Euro");
                currentCell.Font.Size = 10;
                definedcell = new PdfPCell(currentCell);
                table.AddCell(definedcell);
                currentCell = new Paragraph(listView2.Items[0].Text);
                currentCell.Font.Size = 10;
                definedcell = new PdfPCell(currentCell);
                table.AddCell(definedcell);
                Paragraph Totalepar = new Paragraph(listView2.Items[0].SubItems[3].Text);

                if (this.Text == "Fattura" || this.Text == "Nota Credito")
                {
                    currentCell = new Paragraph(listView2.Items[0].SubItems[1].Text);
                    currentCell.Font.Size = 10;
                    definedcell = new PdfPCell(currentCell);
                    table.AddCell(definedcell);
                    currentCell = new Paragraph(listView2.Items[0].SubItems[2].Text);
                    currentCell.Font.Size = 10;
                    definedcell = new PdfPCell(currentCell);

                    table.AddCell(definedcell);
                    if (iva == 10)
                    {
                        Totalepar = new Paragraph(listView2.Items[0].SubItems[3].Text);
                        Totalepar.Font.SetStyle(1);
                        Totalepar.Font.Size = 10;
                        table.AddCell(Totalepar);
                    }
                    else
                    {
                        Totalepar = new Paragraph("");
                        Totalepar.Font.SetStyle(1);
                        Totalepar.Font.Size = 10;
                        table.AddCell(Totalepar);
                    }
                    if (listView2.Items.Count > 1)
                    {
                        currentCell = new Paragraph(listView2.Items[1].SubItems[0].Text);
                        currentCell.Font.Size = 10;
                        definedcell = new PdfPCell(currentCell);
                        table.AddCell(definedcell);
                        currentCell = new Paragraph(listView2.Items[1].SubItems[1].Text);
                        currentCell.Font.Size = 10;
                        definedcell = new PdfPCell(currentCell);
                        table.AddCell(definedcell);
                        currentCell = new Paragraph(listView2.Items[1].SubItems[2].Text);
                        currentCell.Font.Size = 10;
                        definedcell = new PdfPCell(currentCell);
                        table.AddCell(definedcell);
                    }
                    if (iva == 22)
                    {
                        Totalepar = new Paragraph(listView2.Items[1].SubItems[3].Text);
                        Totalepar.Font.SetStyle(1);
                        Totalepar.Font.Size = 10;
                        table.AddCell(Totalepar);
                    }
                    else
                    {
                        Totalepar = new Paragraph("");
                        Totalepar.Font.SetStyle(1);
                        Totalepar.Font.Size = 10;
                        table.AddCell(Totalepar);
                    }
                }
                else
                {
                    table.AddCell(vuoto);
                    table.AddCell(vuoto);
                    Totalepar = new Paragraph(listView2.Items[0].SubItems[3].Text);
                    Totalepar.Font.SetStyle(1);
                    Totalepar.Font.Size = 10;
                    table.AddCell(Totalepar);
                }

                table.WriteSelectedRows(0, -1, FatturaP.Left, FatturaP.Top - h, writer.DirectContent);  //footer

                itemcounter = 0;
                table = new PdfPTable(4);
                table.TotalWidth = 390;
                colWidthPercentages = new[] { 16, 54, 10, 20 };
                table.SetWidths(colWidthPercentages);
                cell = new PdfPCell(pIntestazione);
                cell.Colspan = 4;
                cell.HorizontalAlignment = 0;
                cell.BorderWidth = 0.1f;

                spacing.BorderWidth = 0.1f;
                table.AddCell(cell);
                table.AddCell(spacing);
                currentCell = new Paragraph("Data");
                currentCell.Font.Size = 10;
                definedcell = new PdfPCell(currentCell);
                definedcell.BorderWidth = 0.1f;
                table.AddCell(definedcell);
                currentCell = new Paragraph("Descrizione");
                currentCell.Font.Size = 10;
                definedcell = new PdfPCell(currentCell);
                table.AddCell(definedcell);
                currentCell = new Paragraph("Iva");
                currentCell.Font.Size = 10;
                definedcell = new PdfPCell(currentCell);
                //definedcell.Width=44;
                table.AddCell(definedcell);
                currentCell = new Paragraph("Prezzo");
                currentCell.Font.Size = 10;
                definedcell = new PdfPCell(currentCell);
                table.AddCell(definedcell);
                for (int t = itempoint2; t < itemcount; t++)
                {
                    if (itemcounter > 20)
                        break;
                    if (dataGridView1.Rows[t].Cells[0].Value != null)
                    {
                        currentCell = new Paragraph(dataGridView1.Rows[t].Cells[0].Value.ToString());
                        currentCell.Font.Size = 10;
                        definedcell = new PdfPCell(currentCell);
                        definedcell.BorderWidth = 0.1f;
                        definedcell.BorderWidthTop = 0;
                        definedcell.BorderWidthBottom = 0;
                        table.AddCell(definedcell);

                        currentCell = new Paragraph(dataGridView1.Rows[t].Cells[1].Value.ToString());
                        currentCell.Font.Size = 10;
                        definedcell = new PdfPCell(currentCell);
                        definedcell.BorderWidth = 0.1f;
                        definedcell.BorderWidthTop = 0;
                        definedcell.BorderWidthBottom = 0;
                        table.AddCell(definedcell);

                        currentCell = new Paragraph(dataGridView1.Rows[t].Cells[2].Value.ToString());
                        currentCell.Font.Size = 10;
                        definedcell = new PdfPCell(currentCell);
                        definedcell.BorderWidth = 0.1f;
                        definedcell.BorderWidthTop = 0;
                        definedcell.BorderWidthBottom = 0;
                        table.AddCell(definedcell);

                        currentCell = new Paragraph(dataGridView1.Rows[t].Cells[3].Value.ToString());
                        currentCell.Font.Size = 10;
                        definedcell = new PdfPCell(currentCell);
                        definedcell.BorderWidth = 0.1f;
                        definedcell.BorderWidthTop = 0;
                        definedcell.BorderWidthBottom = 0;
                        table.AddCell(definedcell);


                        itempoint2++;
                        itemcounter++;
                    }
                }
                //if (itempoint1 < 10*(i+1))
                //{
                for (int t = itempoint2; t < 20 * (i + 1); t++)
                {
                    definedcell = new PdfPCell(vuoto);
                    definedcell.BorderWidth = 0.1f;
                    definedcell.BorderWidthTop = 0;
                    definedcell.BorderWidthBottom = 0;
                    table.AddCell(definedcell);
                    table.AddCell(definedcell);
                    table.AddCell(definedcell);
                    table.AddCell(definedcell);


                }
                //}
                if (i > 0)
                {
                    table.AddCell(segue);
                }
                else
                {
                    //    table.AddCell(spacing);
                }
                // h = table.TotalHeight;
                table.WriteSelectedRows(0, -1, FatturaP.Right - 395, FatturaP.Top, writer.DirectContent);  //footer

                table = new PdfPTable(4);
                table.TotalWidth = 390;
                table.LockedWidth = true;
                colWidthPercentages = new[] { 10, 30, 30, 30 };
                table.SetWidths(colWidthPercentages);

                currentCell = new Paragraph("Iva");
                currentCell.Font.Size = 10;
                definedcell = new PdfPCell(currentCell);
                table.AddCell(definedcell);
                currentCell = new Paragraph("Imponibile");
                currentCell.Font.Size = 10;
                definedcell = new PdfPCell(currentCell);
                table.AddCell(definedcell);
                currentCell = new Paragraph("Imposta");
                currentCell.Font.Size = 10;
                definedcell = new PdfPCell(currentCell);
                table.AddCell(definedcell);
                currentCell = new Paragraph("Totale Euro");
                currentCell.Font.Size = 10;
                definedcell = new PdfPCell(currentCell);
                table.AddCell(definedcell);
                currentCell = new Paragraph(listView2.Items[0].Text);
                currentCell.Font.Size = 10;
                definedcell = new PdfPCell(currentCell);
                table.AddCell(definedcell);
                Totalepar = new Paragraph(listView2.Items[0].SubItems[3].Text);

                if (this.Text == "Fattura" || this.Text == "Nota Credito")
                {
                    currentCell = new Paragraph(listView2.Items[0].SubItems[1].Text);
                    currentCell.Font.Size = 10;
                    definedcell = new PdfPCell(currentCell);
                    table.AddCell(definedcell);
                    currentCell = new Paragraph(listView2.Items[0].SubItems[2].Text);
                    currentCell.Font.Size = 10;
                    definedcell = new PdfPCell(currentCell);

                    table.AddCell(definedcell);
                    if (iva == 10)
                    {
                        Totalepar = new Paragraph(listView2.Items[0].SubItems[3].Text);
                        Totalepar.Font.SetStyle(1);
                        Totalepar.Font.Size = 10;
                        table.AddCell(Totalepar);
                    }
                    else
                    {
                        Totalepar = new Paragraph("");
                        Totalepar.Font.SetStyle(1);
                        Totalepar.Font.Size = 10;
                        table.AddCell(Totalepar);
                    }
                    if (listView2.Items.Count > 1)
                    {
                        currentCell = new Paragraph(listView2.Items[1].SubItems[0].Text);
                        currentCell.Font.Size = 10;
                        definedcell = new PdfPCell(currentCell);
                        table.AddCell(definedcell);
                        currentCell = new Paragraph(listView2.Items[1].SubItems[1].Text);
                        currentCell.Font.Size = 10;
                        definedcell = new PdfPCell(currentCell);
                        table.AddCell(definedcell);
                        currentCell = new Paragraph(listView2.Items[1].SubItems[2].Text);
                        currentCell.Font.Size = 10;
                        definedcell = new PdfPCell(currentCell);
                        table.AddCell(definedcell);
                    }
                    if (iva == 22)
                    {
                        Totalepar = new Paragraph(listView2.Items[1].SubItems[3].Text);
                        Totalepar.Font.SetStyle(1);
                        Totalepar.Font.Size = 10;
                        table.AddCell(Totalepar);
                    }
                    else
                    {
                        Totalepar = new Paragraph("");
                        Totalepar.Font.SetStyle(1);
                        Totalepar.Font.Size = 10;
                        table.AddCell(Totalepar);
                    }
                }
                else
                {
                    table.AddCell(vuoto);
                    table.AddCell(vuoto);
                    Totalepar = new Paragraph(listView2.Items[0].SubItems[3].Text);
                    Totalepar.Font.SetStyle(1);
                    Totalepar.Font.Size = 10;
                    table.AddCell(Totalepar);
                }


                table.WriteSelectedRows(0, -1, FatturaP.Right - 395, FatturaP.Top - h, writer.DirectContent);  //footer
                //FatturaP.Add(table);
            }



            FatturaP.Close();
            string path;
            if (Text == "Fattura")
            {

                path = "./Fatture/" + this.Text.ToString() + " " + progressivo.Text.ToString() + " " + fullname + " " + DateTime.Now.Day + "-" + DateTime.Now.Month + "-" + DateTime.Now.Year + ".pdf";
            }
            else
            {
                if (this.Text == "Ricevuta")
                {
                    path = "./Ricevute/" + this.Text.ToString() + " " + progressivo.Text.ToString() + " " + fullname + " " + DateTime.Now.Day + "-" + DateTime.Now.Month + "-" + DateTime.Now.Year + ".pdf";
                }
                else
                {
                    path = "./NoteCredito/" + this.Text.ToString() + " " + progressivo.Text.ToString() + " " + fullname + " " + DateTime.Now.Day + "-" + DateTime.Now.Month + "-" + DateTime.Now.Year + ".pdf";

                }
            }

            Byte[] file = File.ReadAllBytes(path); //test files

            PdfReader reader = new PdfReader(file);
            int pageCount = reader.NumberOfPages;
            iTextSharp.text.Rectangle pageSize = reader.GetPageSize(1);

            Document pdf = new Document(pageSize);
            // pdf.SetPageSize(iTextSharp.text.PageSize.A4.Rotate());
            PdfWriter writer2 = PdfWriter.GetInstance(pdf, new FileStream(path, FileMode.Create));
            pdf.Open();

            //This action leads directly to printer dialogue
            PdfAction jAction = PdfAction.JavaScript("this.print(true);\r", writer2);
            writer2.AddJavaScript(jAction);
            //Omitting this loop and simply adding some text to the file produces the behavior I want.
            for (int i = 0; i < pageCount; i++)
            {
                pdf.NewPage();
                PdfImportedPage page = writer2.GetImportedPage(reader, i + 1);
                writer2.DirectContent.AddTemplate(page, 0, 0);
            }
            pdf.Close();

            string spath;
            if (Text == "Fattura")
            {
                spath = System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "/Fatture/" + this.Text.ToString() + " " + progressivo.Text.ToString() + " " + fullname + " " + DateTime.Now.Day + "-" + DateTime.Now.Month + "-" + DateTime.Now.Year + ".pdf";
          
                path = "./Fatture/" + this.Text.ToString() + " " + progressivo.Text.ToString() + " " + fullname + " " + DateTime.Now.Day + "-" + DateTime.Now.Month + "-" + DateTime.Now.Year + ".pdf";
            }
            else
            {
                if (this.Text == "Ricevuta")
                {
                    spath = System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "/Ricevute/" + this.Text.ToString() + " " + progressivo.Text.ToString() + " " + fullname + " " + DateTime.Now.Day + "-" + DateTime.Now.Month + "-" + DateTime.Now.Year + ".pdf";
          
                    path = "./Ricevute/" + this.Text.ToString() + " " + progressivo.Text.ToString() + " " + fullname + " " + DateTime.Now.Day + "-" + DateTime.Now.Month + "-" + DateTime.Now.Year + ".pdf";
                }
                else
                {
                    spath = System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "/NoteCredito/" + this.Text.ToString() + " " + progressivo.Text.ToString() + " " + fullname + " " + DateTime.Now.Day + "-" + DateTime.Now.Month + "-" + DateTime.Now.Year + ".pdf";
          
                    path = "./NoteCredito/" + this.Text.ToString() + " " + progressivo.Text.ToString() + " " + fullname + " " + DateTime.Now.Day + "-" + DateTime.Now.Month + "-" + DateTime.Now.Year + ".pdf";

                }
            }
            System.Diagnostics.Process.Start(spath);

        }

        private void Fattura_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
         
        }

        private void prova(Object sender,
    DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Rows[e.RowIndex].Cells[2].Value !=null && dataGridView1.Rows[e.RowIndex].Cells[3].Value !=null)
            {
               refresh2();
            }
        }

        private void prova2(Object sender,
   DataGridViewRowsRemovedEventArgs e)
        {           
            
                refresh2();        
        }

    }
}
