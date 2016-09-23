using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Bel3.Gest
{
    public partial class TassaDiSoggiorno : Form
    {
        string currentcamera;
        string npren;
        bool frompan = false;
        int days=1;
        float prezzo = 1.5f;
        private connectoToMySql mConnection;
        Bel3 bel3;
        public TassaDiSoggiorno(connectoToMySql mCon, string pren, string cam, string arr, string par,Bel3 b3,bool fpan)
        {
            frompan = fpan;
            bel3 = b3;
            mConnection = mCon;
            if (mConnection != null)
            {

                if (mConnection.cnMySQL.State != ConnectionState.Closed)
                    mConnection.cnMySQL.Close();

                if (mConnection.cnMySQL.State != ConnectionState.Open)
                    mConnection.cnMySQL.Open();

                   mConnection.cmdMySQL = new MySqlCommand("select tariffa from tariffeextra where nome='Tassa di Soggiorno'", mConnection.cnMySQL);

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
                    if (thisrow != "")
                    {
                        prezzo = Convert.ToSingle(thisrow);
                    }
                }

                if (mConnection.cnMySQL.State != ConnectionState.Closed)
                    mConnection.cnMySQL.Close();
             
            }
            npren = pren;
            currentcamera = cam;
            DateTime partenza = DateTime.Parse(par);
            DateTime arrivo = DateTime.Parse(arr);
            TimeSpan daysc = partenza - arrivo;
            days= (daysc.Days);
            if (days > 4)
                days = 4;
            if (days < 1)
                days = 1;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

           
            if (mConnection != null)
            {

                if (mConnection.cnMySQL.State != ConnectionState.Closed)
                    mConnection.cnMySQL.Close();

                if (mConnection.cnMySQL.State != ConnectionState.Open)
                    mConnection.cnMySQL.Open();
                mConnection.cmdMySQL = mConnection.cnMySQL.CreateCommand();

                float prezz = /*Convert.ToSingle(npers.Value)*/prezzo*days;
                mConnection.cmdMySQL.CommandText = "INSERT INTO conti (id,nome,prezzo,iva,quantità,data,camera) VALUES('" + npren + "','Tassa Soggiorno','" + prezz.ToString() + "',0," + npers.Value + ",'" + DateTime.Now.ToShortDateString() + "','" + currentcamera + "')";
                mConnection.cmdMySQL.ExecuteNonQuery();

                mConnection.cnMySQL.Close();
                if (!frompan)
                    bel3.checkinnew();
                else
                    bel3.checkinpanoramica();
                this.Close();
            }
        }

        private void Tassa_di_soggiorno_Load(object sender, EventArgs e)
        {

        }
    }
}
