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
    public partial class ModificaCliente : Form
    {
        connectoToMySql mConnection;
        int id = 0;
        private int p;
        private string p_2;
        private string p_3;
        private string p_4;
        private string p_5;
        private string p_6;

        public ModificaCliente(connectoToMySql mcon)
        {
            mConnection = mcon;
            InitializeComponent();
            button2.Hide();

            button2.Enabled = false;
            this.Show();
            this.Focus();
            // button2.Show();
        }

        public ModificaCliente(connectoToMySql mcon, int id, string nom, string cog)
        {
            mConnection = mcon;
            button1.Hide();

            button1.Enabled = false;
            //button2.Show();
            //button2.Location = button1.Location;
            cNome.Text = nom;
            cCognome.Text = cog;
            this.id = id;
           // this.arrivo.Value = DateTime.Now;
        }

        public ModificaCliente(connectoToMySql mConnection, int p, string p_2, string p_3, string p_4, string p_5, string p_6)
        {
            // TODO: Complete member initialization
            this.mConnection = mConnection;
           this.id = p;
           InitializeComponent();
           button1.Hide();
            cNome.Text = p_2;
            cCognome.Text = p_3;
            ragsociale.Text = p_4;
            codicefiscale.Text = p_5;
            telefoni.Text = p_6;
        }


        private void button1_Click(object sender, EventArgs e)
        {

            if (mConnection != null)
            {

                mConnection.cnMySQL.Open();
                mConnection.cmdMySQL = mConnection.cnMySQL.CreateCommand();
                string Nome, Cognome,Tel,ragso;
                Nome = cNome.Text.Replace("'", "\\'");
                Cognome = cCognome.Text.Replace("'", "\\'");
                Tel = telefoni.Text.Replace("'", "\\'");
                ragso = ragsociale.Text.Replace("'","\\'");
                mConnection.cmdMySQL.CommandText = "INSERT INTO clienti (nome,cognome,telefoni,codicefiscale,ragionesociale) VALUES('" + Nome + "','" + Cognome + "','" + Tel + "','" + codicefiscale.Text + "','" + ragso + "')";
                mConnection.cmdMySQL.ExecuteNonQuery();
                mConnection.cmdMySQL.CommandText = "select last_insert_id();";
                int idclient = Convert.ToInt32(mConnection.cmdMySQL.ExecuteScalar());
                mConnection.cmdMySQL.Dispose();
                mConnection.cnMySQL.Close();

                MessageBox.Show("Il Cliente è stato inserito con successo!",
                "Fatto!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                // prenotazione(idclient);
                this.Close();
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (mConnection != null)
            {

                mConnection.cnMySQL.Open();
                mConnection.cmdMySQL = mConnection.cnMySQL.CreateCommand();
                string par = p.ToString();
                string Nome, Cognome,Tel;
                Nome = cNome.Text.Replace("'", "\\'");
                Cognome = cCognome.Text.Replace("'", "\\'");
                string ragso = ragsociale.Text.Replace("'", "\\'");
                Tel = telefoni.Text.Replace("'", "\\'");
                string updatepren ;
                if(ragso==null || ragso=="")
                updatepren = "UPDATE prenotazioni set nome='" + Nome + "',cognome='" + Cognome + "' where codicecliente=" + Convert.ToInt16(id) + ";";
                else
                updatepren = "UPDATE prenotazioni set nome='" + ragso + "',cognome='" + Cognome + "' where codicecliente=" + Convert.ToInt16(id) + ";";
                
                mConnection.cmdMySQL.CommandText = "UPDATE clienti SET nome='" + Nome + "',cognome='" + Cognome + "',telefoni='" + Tel + "',codicefiscale='" + codicefiscale.Text + "',ragionesociale='" + ragso + "' where clienti.id=" + Convert.ToInt16(id) + ";"+updatepren;
                mConnection.cmdMySQL.ExecuteNonQuery();                

                mConnection.cmdMySQL.Dispose();
                mConnection.cnMySQL.Close();

                MessageBox.Show("Il Cliente è stato modificato con successo!",
                "Fatto!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                // prenotazione(idclient);
                this.Close();
            }
        }

        private void ragsociale_TextChanged(object sender, EventArgs e)
        {

        }

    }
}