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
    public partial class FatturaoRicevuta : Form
    {
        private connectoToMySql mConnection;
        private string Prenotazione;
        float tariffa;
        float anticipo;
        bool forfait;
        int days;
        int numerocamere;
        int iva;
        string datear,datepar;
        string cliente;
        bool prepaid = false;
        public bool bibite = false;

        //public FatturaoRicevuta()
        //{
        //    InitializeComponent();
        //}

        public FatturaoRicevuta(connectoToMySql mCon, string p)//,string nomecliente, int numerocamere, string arrivo, string partenza, float tariffa, bool forfait, float anticipo, int iva)
        {
            iva = 10;
            // TODO: Complete member initialization
            mConnection = mCon;
            Prenotazione = p;
            InitializeComponent();
            //cliente = nomecliente;
            //this.anticipo = anticipo;
            //this.forfait = forfait;
            //this.tariffa = tariffa;
            //DateTime ar = new DateTime(DateTime.Parse(arrivo).Year, DateTime.Parse(arrivo).Month, DateTime.Parse(arrivo).Day);
            //datear = ar.ToShortDateString();
            //DateTime par = new DateTime(DateTime.Parse(partenza).Year, DateTime.Parse(partenza).Month, DateTime.Parse(partenza).Day);
            //TimeSpan t = par - ar;
            //datepar = par.ToShortDateString();
          
            //days = t.Days;
            //this.numerocamere = numerocamere;
            //this.iva = iva;

        }

        public FatturaoRicevuta(connectoToMySql mCon, string p,bool prp)//,string nomecliente, int numerocamere, string arrivo, string partenza, float tariffa, bool forfait, float anticipo, int iva)
        {
            // TODO: Complete member initialization
            mConnection = mCon;
            prepaid = prp;
            Prenotazione = p;
            InitializeComponent();
            //cliente = nomecliente;
            //this.anticipo = anticipo;
            //this.forfait = forfait;
            //this.tariffa = tariffa;
            //DateTime ar = new DateTime(DateTime.Parse(arrivo).Year, DateTime.Parse(arrivo).Month, DateTime.Parse(arrivo).Day);
            //datear = ar.ToShortDateString();
            //DateTime par = new DateTime(DateTime.Parse(partenza).Year, DateTime.Parse(partenza).Month, DateTime.Parse(partenza).Day);
            //TimeSpan t = par - ar;
            //datepar = par.ToShortDateString();

            //days = t.Days;
            //this.numerocamere = numerocamere;
            //this.iva = iva;

        }

        private void button1_Click(object sender, EventArgs e)
        {
         //   mConnection.cnMySQL.Close();
            if (!bibite)
            {
                Fattura f = new Fattura(mConnection, "Fattura", Prenotazione, prepaid);//,cliente, numerocamere,datear, datepar, tariffa, forfait,anticipo,iva);
                f.Show();
            }
            else
            {
                Fattura f = new Fattura(mConnection, "Fattura", Prenotazione, prepaid,bibite);//,cliente, numerocamere,datear, datepar, tariffa, forfait,anticipo,iva);
                f.Show();
            }
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!bibite)
            {
                Fattura f = new Fattura(mConnection, "Ricevuta", Prenotazione, prepaid);//, cliente, numerocamere, datear, datepar, tariffa, forfait, anticipo, iva);
                f.Show();
            }
            else
            {
                Fattura f = new Fattura(mConnection, "Fattura", Prenotazione, prepaid,bibite);//,cliente, numerocamere,datear, datepar, tariffa, forfait,anticipo,iva);
                f.Show();
            }
            this.Close();

        }
    }
}
