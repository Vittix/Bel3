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
    public partial class Storico : Form
    {
        private connectoToMySql mConnection;
        public Storico(connectoToMySql mconn)
        {


            mConnection = mconn;
            InitializeComponent();
            dateTimePicker1.Value = DateTime.Now;
            string datas = dateTimePicker1.Value.Month.ToString() + "/" + dateTimePicker1.Value.Year.ToString();
            string datas2 = datas;
            if (checkBox1.Checked)
            {
                datas = dateTimePicker1.Value.ToShortDateString();
            }
            if (mConnection != null)
            {
                if (mConnection.cnMySQL.State != ConnectionState.Closed)
                    mConnection.cnMySQL.Close();

                if (mConnection.cnMySQL.State != ConnectionState.Open)
                    mConnection.cnMySQL.Open();
                // mConnection.cmdMySQL = new MySqlCommand("select * from prenotazioni where substring(arrivo ,4," + datas2.Length + ")= '" + datas2 + "'", mConnection.cnMySQL);
                mConnection.cmdMySQL = new MySqlCommand("select * from prenotazioni where LOCATE('" + datas + "',arrivo)", mConnection.cnMySQL);

                //  mConnection.cmdMySQL = new MySqlCommand("select Incasso from incassi where Mese='" + datas + "'", mConnection.cnMySQL);

                // get query results
                mConnection.reader = mConnection.cmdMySQL.ExecuteReader();
                int countCamere = 0;
                float incassitotale = 0;
                // print the CustomerID of each record
                while (mConnection.reader.Read())
                {
                    countCamere++;
                    if (mConnection.reader.GetInt32(6) == 1)
                    {
                        float incassosingolo = Convert.ToSingle(mConnection.reader.GetString(5));

                        incassitotale += incassosingolo;
                    }
                    else
                    {
                        DateTime arr = DateTime.Parse(mConnection.reader.GetString(3).Substring(0, 10));

                        DateTime par = DateTime.Parse(mConnection.reader.GetString(4).Substring(0, 10));
                        TimeSpan difference = par - arr;
                        float incassosingolo = Convert.ToSingle(mConnection.reader.GetString(5));

                        incassitotale += (incassosingolo * difference.Days);
                    }
                }
                label3.Text = "Camere Vendute: " + countCamere.ToString();
                label1.Text = "Incasso totale:" + incassitotale + "€";

                if (mConnection.cnMySQL.State != ConnectionState.Closed)
                    mConnection.cnMySQL.Close();

                if (mConnection.cnMySQL.State != ConnectionState.Open)
                    mConnection.cnMySQL.Open();

                mConnection.cmdMySQL = new MySqlCommand("select Incasso from incassi where Mese='" + datas + "'", mConnection.cnMySQL);
                // get query results
                mConnection.reader = mConnection.cmdMySQL.ExecuteReader();

                // print the CustomerID of each record
                while (mConnection.reader.Read())
                {
                    string thisrow = "";
                    thisrow = mConnection.reader.GetValue(0).ToString();
                    if (thisrow != "")
                    {
                        //   label1.Text = "Incasso totale:" + thisrow + "€";
                    }
                }

                if (mConnection.cnMySQL.State != ConnectionState.Closed)
                    mConnection.cnMySQL.Close();
                if (mConnection.cnMySQL.State != ConnectionState.Open)
                    mConnection.cnMySQL.Open();

                mConnection.cmdMySQL = new MySqlCommand("select Incasso from storicotds where Mese='" + datas2 + "'", mConnection.cnMySQL);
                // get query results
                mConnection.reader = mConnection.cmdMySQL.ExecuteReader();

                // print the CustomerID of each record
                while (mConnection.reader.Read())
                {
                    string thisrow = "";
                    thisrow = mConnection.reader.GetValue(0).ToString();
                    if (thisrow != "")
                    {
                        label2.Text = "Totale Mensile Tasse di Soggiorno:" + thisrow + "€";
                    }
                }

                if (mConnection.cnMySQL.State != ConnectionState.Closed)
                    mConnection.cnMySQL.Close();
            }

        }

        private void Storico_Load(object sender, EventArgs e)
        {

        }
        //SELECT field FROM table WHERE field LIKE "%value%" LOCATE('at',pub_name)>0;  
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            string datas = dateTimePicker1.Value.Month.ToString() + "/" + dateTimePicker1.Value.Year.ToString();
            string datas2 = datas;
            if (checkBox1.Checked)
            {
                datas = dateTimePicker1.Value.ToShortDateString();
            }
            if (mConnection != null)
            {
                if (mConnection.cnMySQL.State != ConnectionState.Closed)
                    mConnection.cnMySQL.Close();

                if (mConnection.cnMySQL.State != ConnectionState.Open)
                    mConnection.cnMySQL.Open();
                // mConnection.cmdMySQL = new MySqlCommand("select * from prenotazioni where substring(arrivo ,4," + datas2.Length + ")= '" + datas2 + "'", mConnection.cnMySQL);
                mConnection.cmdMySQL = new MySqlCommand("select * from prenotazioni where LOCATE('"+datas+"',arrivo)", mConnection.cnMySQL);

                //  mConnection.cmdMySQL = new MySqlCommand("select Incasso from incassi where Mese='" + datas + "'", mConnection.cnMySQL);

                // get query results
                mConnection.reader = mConnection.cmdMySQL.ExecuteReader();
                int countCamere = 0;
                float incassitotale = 0;
                // print the CustomerID of each record
                while (mConnection.reader.Read())
                {
                    countCamere++;
                    if (mConnection.reader.GetInt32(6) == 1)
                    {
                        float incassosingolo = Convert.ToSingle(mConnection.reader.GetString(5));

                        incassitotale += incassosingolo;
                    }
                    else
                    {
                        DateTime arr = DateTime.Parse(mConnection.reader.GetString(3).Substring(0, 10));

                        DateTime par = DateTime.Parse(mConnection.reader.GetString(4).Substring(0, 10));
                        TimeSpan difference = par - arr;
                        float incassosingolo = Convert.ToSingle(mConnection.reader.GetString(5));

                        incassitotale += (incassosingolo * difference.Days);
                    }
                }
                label3.Text = "Camere Vendute: " + countCamere.ToString();
                label1.Text = "Incasso totale:" + incassitotale + "€";

                if (mConnection.cnMySQL.State != ConnectionState.Closed)
                    mConnection.cnMySQL.Close();

                if (mConnection.cnMySQL.State != ConnectionState.Open)
                    mConnection.cnMySQL.Open();

                mConnection.cmdMySQL = new MySqlCommand("select Incasso from incassi where Mese='" + datas + "'", mConnection.cnMySQL);
                // get query results
                mConnection.reader = mConnection.cmdMySQL.ExecuteReader();

                // print the CustomerID of each record
                while (mConnection.reader.Read())
                {
                    string thisrow = "";
                    thisrow = mConnection.reader.GetValue(0).ToString();
                    if (thisrow != "")
                    {
                     //   label1.Text = "Incasso totale:" + thisrow + "€";
                    }
                }

                if (mConnection.cnMySQL.State != ConnectionState.Closed)
                    mConnection.cnMySQL.Close();
                if (mConnection.cnMySQL.State != ConnectionState.Open)
                    mConnection.cnMySQL.Open();

                mConnection.cmdMySQL = new MySqlCommand("select Incasso from storicotds where Mese='" + datas2 + "'", mConnection.cnMySQL);
                // get query results
                mConnection.reader = mConnection.cmdMySQL.ExecuteReader();

                // print the CustomerID of each record
                while (mConnection.reader.Read())
                {
                    string thisrow = "";
                    thisrow = mConnection.reader.GetValue(0).ToString();
                    if (thisrow != "")
                    {
                        label2.Text = "Totale Mensile Tasse di Soggiorno:" + thisrow + "€";
                    }
                }

                if (mConnection.cnMySQL.State != ConnectionState.Closed)
                    mConnection.cnMySQL.Close();
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            dateTimePicker1_ValueChanged(sender, e);
        }
    }
}
