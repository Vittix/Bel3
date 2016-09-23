using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Net;
using System.Net.Sockets;

namespace Bel3
{
    
    public partial class Datepicker : Form
    {
        List<CheckIn> CheckIns;
        MySqlConnection connection;
        Bel3 b3;
        public Datepicker()
        {
            InitializeComponent();
            dateTimePicker1.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
        }

        public Datepicker(MySqlConnection Connection,int idpren,string camera,Bel3 bel3)
        {
            connection = Connection;
            InitializeComponent();
            dateTimePicker1.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            CheckIns = new List<CheckIn>();
            CheckIns.Add(new CheckIn(idpren, camera));
            b3 = bel3;
            

        }
        public Datepicker(MySqlConnection Connection,List<CheckIn> checkins, Bel3 bel3)
        {
            connection = Connection;
            InitializeComponent();
            dateTimePicker1.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            CheckIns = checkins;
            b3 = bel3;


        }
        private void Datepicker_Load(object sender, EventArgs e)
        {

        }
       

        private void button1_Click(object sender, EventArgs e)
        {
            if (Colazioni.Value == 0)
            {
                MessageBox.Show("Devi inserire almeno una persona per il calcolo delle colazioni!!!", "Attenzione", MessageBoxButtons.OK);
                return;
            }
            foreach (CheckIn c in CheckIns)
            {
                if (connection != null)
                {
                    if (connection.State != ConnectionState.Closed)
                        connection.Close();
                    if (connection.State != ConnectionState.Open)
                        connection.Open();
                   
                    MySqlCommand cmd = new MySqlCommand("UPDATE  prenotazioni SET checkin = '1',arrivo='" + dateTimePicker1.Value.ToShortDateString() + " " + dateTimePicker1.Value.ToShortTimeString() + "' where prenotazioni.id=" + c.pren + ";", connection);
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
                    cmd.CommandText += "UPDATE  Beacon SET isdbmodified = True , IP='" + localIP + "' ;";
                    cmd.ExecuteNonQuery();
                    connection.Close();
                    //   refreshprenotazioni();
                    if (connection.State != ConnectionState.Closed)
                        connection.Close();
                    if (connection.State != ConnectionState.Open)
                        connection.Open();

                    MySqlCommand cmd1 = connection.CreateCommand();
                    cmd1.CommandText = "INSERT INTO Colazioni (NumeroPrenotazione,NumeroPersone,CheckIn,CheckOut) VALUES(" + c.pren +","+Colazioni.Value+","+1+","+0+")";
                    cmd1.ExecuteNonQuery();
                   // cmd.CommandText = "select last_insert_id();";
                    //int idclient = Convert.ToInt32(cmd.ExecuteScalar());
                    cmd1.Dispose();
                    connection.Close();

                    connection.Close();
             /*   string ncam="";
                string ntype="";
                 if (connection.State != ConnectionState.Closed)
                        connection.Close();

                    if (connection.State != ConnectionState.Open)
                        connection.Open();
                    MySqlCommand cmd2 = new MySqlCommand("SELECT camera,tipostanza FROM prenotazioni  WHERE prenotazioni.id=" + c.pren+";", connection);
                     MySqlDataReader reader= cmd2.ExecuteReader();

                     while (reader.Read())
                     {
                       ncam=  reader.GetValue(0).ToString();
                       ntype = reader.GetValue(1).ToString();
                     }

                     if (connection.State != ConnectionState.Closed)
                         connection.Close();*/



                     /*  if (connection.State != ConnectionState.Open)
                         connection.Open();

                     MySqlCommand cmd3 = new MySqlCommand("UPDATE  camere SET tipo = '"+ntype+"' where camere.Name=" + ncam + ";", connection);*/
                     /*IPHostEntry host;
                     string localIP = "";
                     host = Dns.GetHostEntry(Dns.GetHostName());
                     foreach (IPAddress ip in host.AddressList)
                     {
                         if (ip.AddressFamily == AddressFamily.InterNetwork)
                         {
                             localIP = ip.ToString();
                             break;
                         }
                     }*/
                    /* cmd3.ExecuteNonQuery();
                     connection.Close();*/
                }
            }



            b3.RefreshOverview();
            b3.RefreshBook();
            b3.calctableau();
            this.Close();
        }

        private void Colazioni_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
