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
    public partial class Prenotazione : Form
    {
        MySqlConnection mConnection;
        MySqlConnection crossconnection;
        Bel3 bel3;
        connectoToMySql msqlCon;
        public Prenotazione(MySqlConnection mcon, connectoToMySql sql, Bel3 b3)
        {
            msqlCon = sql;
            mConnection = mcon;
            crossconnection = mcon.Clone();
            // crossconnection = new connectoToMySql(mcon.server, mcon.database, mcon.user, mcon.password);
            InitializeComponent();
            Prenotazione_Load();
             readroom();
            button3.Hide();
            button2.Hide();
            // button2.Show();
            // iva.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
            this.arrivo.Value = DateTime.Now;
            bel3 = b3;
            //tarforfait.Enabled = false;
        }
        int id = 0;


        public Prenotazione(connectoToMySql mysql,MySqlConnection mcon,Bel3 b3, int id, string nom, string cog, string rags, string cf, string tel)
        {
            bel3 = b3;
            mConnection = mcon;
            msqlCon = mysql;
            // crossconnection = new connectoToMySql(mcon.server, mcon.database, mcon.user, mcon.password);
            InitializeComponent();
            Prenotazione_Load();
            button1.Enabled = false;
            //readroom();
            button1.Hide();
            button2.Show();
            button3.Hide();
            button3.Enabled = false;
            button2.Location = button1.Location;
            cNome.Text = nom;
            cCognome.Text = cog;
            ragsociale.Text = rags;
            codicefiscale.Text = cf;
            telefoni.Text = tel;
            cNome.Enabled = false;
            cCognome.Enabled = false;
            ragsociale.Enabled = false;
            codicefiscale.Enabled = false;
            telefoni.Enabled = false;
            this.id = id;
            this.arrivo.Value = DateTime.Now;


        }
        List<string> origcam;
        int prenotazioneID;
        public Prenotazione(MySqlConnection mcon, int idpren, int id, string nom, string cog, string rags, string cf, string tel, List<string> cam)
        {
            prenotazioneID = idpren;
            this.origcam = cam;
            mConnection = mcon;
            // crossconnection = new connectoToMySql(mcon.server, mcon.database, mcon.user, mcon.password);
            InitializeComponent();
            Prenotazione_Load();
            button2.Enabled = false;
            button1.Hide();
            button2.Hide();
            button3.Show();
            button1.Enabled = false;
            button3.Location = button1.Location;
            cNome.Text = nom;
            cCognome.Text = cog;
            ragsociale.Text = rags;
            codicefiscale.Text = cf;
            telefoni.Text = tel;
            cNome.Enabled = false;
            cCognome.Enabled = false;
            ragsociale.Enabled = false;
            codicefiscale.Enabled = false;
            telefoni.Enabled = false;
            this.id = id;
            this.arrivo.Value = DateTime.Now;

             readroom();



        }
        bool initializated = false;
        int camcount;
        List<string> othercam;
        public Prenotazione(connectoToMySql mcon,Bel3 b3, int idpren, int id, string nom, string cog, string rags, string cf, string tel, List<string> cam, string ar, string pa, string da, string op, string ts, string ti, string arra, string nt, string tari, string anti, string iv, string forf, string rif, string pag, string note, int camcount)
        {
            bel3 = b3;
            this.camcount = camcount;
            prenotazioneID = idpren;
            this.origcam = cam;
            othercam = cam;
            msqlCon = mcon;
            mConnection = mcon.cnMySQL;
            //crossconnection = new connectoToMySql(mcon.server, mcon.database, mcon.user, mcon.password);
            InitializeComponent();
            Prenotazione_Load();
            button2.Enabled = false;


            button1.Hide();
            button2.Hide();
            button3.Show();
            button1.Enabled = false;
            button3.Location = button1.Location;
            cNome.Text = nom;
            cCognome.Text = cog;
            ragsociale.Text = rags;
            codicefiscale.Text = cf;
            telefoni.Text = tel;
            cNome.Enabled = false;
            cCognome.Enabled = false;
            ragsociale.Enabled = false;
            codicefiscale.Enabled = false;
            telefoni.Enabled = false;
            this.id = id;
            this.arrivo.Value = DateTime.Now;
            this.arrivo.Value = DateTime.Parse(ar);
            this.partenza.Value = DateTime.Parse(pa);

            this.da.Text = da;

            this.operatore.Text = op;
            this.riferimento.Text = rif;
            this.tarforfait.Text = tari;
            this.iva.Text = iv;
            this.anticipo.Text = anti;
            this.tipostanza.Text = ts;
            this.tipologia.Text = ti;
            this.arrangiamento.Text = arra;
            if (forf == "No")
            {
                this.forfait.Checked = false;

            }
            else
            {

                this.forfait.Checked = true;
            }

            if (pag == "Pagata")
            {
                this.pagata.Checked = true;

            }
            else
            {

                this.pagata.Checked = false;
            }
            this.richTextBox1.Text = note;

            //  readroom();

        }
   
        public Prenotazione(connectoToMySql MYSQL, MySqlConnection mcon,Bel3 b3, DateTime arr, DateTime par, List<string> cam)
        {
            msqlCon = MYSQL;
            bel3 = b3;
            mConnection = mcon;
            // crossconnection = new connectoToMySql(mcon.server, mcon.database, mcon.user, mcon.password);
            InitializeComponent();
            button3.Hide();
            button2.Hide();
            // button2.Show();
            iva.Enabled = false;
            button2.Enabled = false;
            this.arrivo.Value = arr;
            this.partenza.Value = par.AddDays(1);

            // readroom();

            libere.Items.Clear();
            foreach (string s in cam)
            {
                libere.Items.Add(s);
            }
            libere.BeginUpdate();
            foreach (ListViewItem i in libere.Items)
            {
                i.Selected = true;
            }
            libere.EndUpdate();
            libere.Enabled = false;
            arrivo.Enabled = false;
            partenza.Enabled = false;
            Prenotazione_Load();

        }


        public void clientload(object sender, EventArgs e)
        {

        }
        bool isbusy = false;
        public void readroom()
        {
            partenza.Value = new DateTime(partenza.Value.Year, partenza.Value.Month, partenza.Value.Day, 12, 0, 0);
            arrivo.Value = new DateTime(arrivo.Value.Year, arrivo.Value.Month, arrivo.Value.Day, 12, 0, 1);
            TimeSpan days = partenza.Value - arrivo.Value;
            Giorni.Text = "Giorno:" + (days.Days + 1);
            libere.Items.Clear();

            if (mConnection.State == ConnectionState.Open)
                mConnection.Close();
            if (mConnection.State != ConnectionState.Open)
                mConnection.Open();
            MySqlCommand cmd = new MySqlCommand("select * from camere LEFT JOIN prenotazioni on camere.Name=prenotazioni.camera ORDER BY Name ASC", mConnection);

            MySqlDataReader reader = cmd.ExecuteReader();
            bool match = false;
            string camera="",oldcamera = "";
            bool oldisbusy = false;

            while (reader.Read())
            {

                camera = reader.GetString(0);
                if (camera != oldcamera)
                  
                {
                    if(oldcamera!="")
                    if (!oldisbusy)
                        libere.Items.Add(oldcamera);
                     match = false;
                     oldisbusy = false;
                }
                    //if(cam)
                string x = reader.GetValue(7).ToString();
                   int prencurrent=-50;
                   if (x != "" && !oldisbusy)
                   {
                       prencurrent = Convert.ToInt32(x);
                       if (prencurrent == prenotazioneID)
                       {
                           if(!oldisbusy)
                           oldisbusy = false;
                       }
                       else
                       {
                           bool checkedin = Convert.ToBoolean(reader.GetValue(24));

                           bool checkedout = Convert.ToBoolean(reader.GetValue(25));
                           DateTime a = DateTime.Parse(reader.GetValue(10).ToString());
                           DateTime p = DateTime.Parse(reader.GetValue(11).ToString());



                           if (arrivo.Value <= a && partenza.Value <= a)
                           {
                                    oldisbusy = false;
                               
                                
                                   //  libere.Items.RemoveAt(libere.Items.Count - 1);
                               // libere.Items.Add(mConnection.reader.GetValue(0).ToString());


                           }
                           else
                           {
                               if (arrivo.Value >= p && partenza.Value >= p)
                               {
                                      oldisbusy = false;
                                    //   libere.Items.RemoveAt(libere.Items.Count - 1);
                                   //   libere.Items.Add(mConnection.reader.GetValue(0).ToString());
                               }
                               else
                               {

                                   //libere.Items.RemoveAt(libere.Items.Count - 1);
                                   if (!checkedout)
                                   {
                                       oldisbusy = true;
                                   }

                                        // break;
                                   //}
                                 // else
                                  // //{
                                  //     oldisbusy = false;
                                   //    //  libere.Items.RemoveAt(libere.Items.Count - 1);
                                   //    // libere.Items.Add(mConnection.reader.GetValue(0).ToString());
                                   //}
                               }
                           }


                           //libere.Items.Add(mConnection.reader.GetValue(0).ToString());

                           match = true;
                       }


                   }
                    
                    //        // for (int i = 0; i < mConnection.reader.FieldCount; i++)
                    //        //thisrow += mConnection.reader.GetValue(i).ToString(); 


                    oldcamera =camera;
                         
                    // Overview.Items.Add(mConnection.reader.GetValue(1).ToString()).SubItems.AddRange(thisrow);

                }
                mConnection.Close();
          
            /*if (mConnection != null)
            {

                if (mConnection.State == ConnectionState.Open)
                    mConnection.Close();
                if (mConnection.State != ConnectionState.Open)
                    mConnection.Open();
              MySqlCommand cmd= new MySqlCommand("select * from camere ORDER BY Name ASC", mConnection);

                //
                // 4. Use the connection
                //

                // get query results
              MySqlDataReader reader = cmd.ExecuteReader();

                // print the CustomerID of each record
                while (reader.Read())
                {
                    if (crossconnection.State == ConnectionState.Open)
                        crossconnection.Close();
                    if (crossconnection.State != ConnectionState.Open)
                        crossconnection.Open();
                    MySqlCommand cmd2 = new MySqlCommand("select * from Prenotazioni where camera='" + 
                    reader.GetValue(0).ToString() + "'", crossconnection);
               
                    bool match = false;
                    string oldcamera = "";
                    bool oldisbusy = false;

                     MySqlDataReader  reader2 = cmd2.ExecuteReader();
                     while (reader2.Read())
                    {
                        int prencurrent = Convert.ToInt32(reader2.GetValue(0).ToString());
                        if (prencurrent == prenotazioneID)
                        {
                            oldisbusy = false;
                        }
                        else
                        {
                            bool checkedin = Convert.ToBoolean(reader2.GetValue(17).ToString());

                            bool checkedout = Convert.ToBoolean(reader2.GetValue(18).ToString());
                            DateTime a = DateTime.Parse(reader2.GetValue(3).ToString());
                            DateTime p = DateTime.Parse(reader2.GetValue(4).ToString());



                            if (arrivo.Value < a && partenza.Value < a)
                            {
                                oldisbusy = false;
                                //  libere.Items.RemoveAt(libere.Items.Count - 1);
                                // libere.Items.Add(mConnection.reader.GetValue(0).ToString());


                            }
                            else
                            {
                                if (arrivo.Value > p && partenza.Value > p)
                                {

                                    oldisbusy = false;

                                    //   libere.Items.RemoveAt(libere.Items.Count - 1);
                                    //   libere.Items.Add(mConnection.reader.GetValue(0).ToString());
                                }
                                else
                                {

                                    // libere.Items.RemoveAt(libere.Items.Count - 1);
                                    if (!checkedout)
                                    {
                                        oldisbusy = true;
                                        break;
                                    }
                                    else
                                    {
                                        oldisbusy = false;
                                        //  libere.Items.RemoveAt(libere.Items.Count - 1);
                                        // libere.Items.Add(mConnection.reader.GetValue(0).ToString());
                                    }
                                }
                            }


                            //libere.Items.Add(mConnection.reader.GetValue(0).ToString());

                            oldcamera = reader.GetValue(0).ToString();
                            match = true;
                        }

                    }

                    if (!oldisbusy)
                        libere.Items.Add(reader.GetValue(0).ToString());
                    crossconnection.Close();
                    //        // for (int i = 0; i < mConnection.reader.FieldCount; i++)
                    //        //thisrow += mConnection.reader.GetValue(i).ToString(); 



                    // Overview.Items.Add(mConnection.reader.GetValue(1).ToString()).SubItems.AddRange(thisrow);

                }
                mConnection.Close();
            }
            //initializated = true;*/
        }

        private void From_ValueChanged(object sender, EventArgs e)
        {
            if (libere.Items.Count > 0)
                libere.Items.Clear();
            if (partenza.Value <= arrivo.Value)
            {
                partenza.Value = new DateTime(arrivo.Value.AddDays(1).Year, arrivo.Value.AddDays(1).Month, arrivo.Value.AddDays(1).Day, 12, 0, 0);
            }
            if (initializated)
            {
                if (origcam != null)
                {
                    if (camcount <= origcam.Count)
                        origcam = null;

                }
            }
            readroom();
        }

        private void To_ValueChanged(object sender, EventArgs e)
        {
            if (libere.Items.Count > 0)
                libere.Items.Clear();
            if (arrivo.Value >= partenza.Value)
                arrivo.Value = new DateTime(partenza.Value.AddDays(-1).Year, partenza.Value.AddDays(-1).Month, partenza.Value.AddDays(-1).Day, 12, 0, 1);
            if (initializated)
            {
                if (origcam != null)
                {

                    if (camcount <= origcam.Count)
                        origcam = null;

                }
            }
            readroom();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (tarforfait.Text == "" || tarforfait.Text == null)
            {
                MessageBox.Show("Devi inserire una tariffa!",
                  "Fatto!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }
            if (libere.SelectedItems.Count > 0)
            {
                if (mConnection != null)
                {

                    mConnection.Open();
                    // mConnection.cmdMySQL = mConnection.cnMySQL.CreateCommand();
                    string Nome, Cognome, Tel;
                    Nome = cNome.Text.Replace("'", "\\'");
                    Cognome = cCognome.Text.Replace("'", "\\'");
                    string ragso = ragsociale.Text.Replace("'", "\\'");
                    Tel = telefoni.Text.Replace("'", "\\'");
                    MySqlCommand cmd = mConnection.CreateCommand();
                    cmd.CommandText = "INSERT INTO clienti (nome,cognome,telefoni,codicefiscale,ragionesociale) VALUES('" + Nome + "','" + Cognome + "','" + Tel + "','" + codicefiscale.Text + "','" + ragso + "')";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "select last_insert_id();";
                    int idclient = Convert.ToInt32(cmd.ExecuteScalar());
                    cmd.Dispose();
                    mConnection.Close();

                    MessageBox.Show("Il Cliente è stato inserito con successo!",
                    "Fatto!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    prenotazione(idclient);

                }
            }
            else
            {
                MessageBox.Show("Seleziona una stanza per effettuare la prenotazione",
                    "Attenzione!", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }
        int idpren = 0;
        int firstid;
        void prenotazione(int customerId)
        {
            for (int i = 0; i < libere.SelectedItems.Count; i++)
            {
                mConnection.Open();
                MySqlCommand cmd = mConnection.CreateCommand();
                DateTime a = new DateTime(arrivo.Value.Year, arrivo.Value.Month, arrivo.Value.Day, 12, 0, 1);
                DateTime p = new DateTime(partenza.Value.Year, partenza.Value.Month, partenza.Value.Day, 12, 0, 0);
                string arr = a.ToString();
                string par = p.ToString();
               
                string Note = richTextBox1.Text.Replace("'", "''");
                string Nome = cNome.Text.Replace("'", "''");
                string Cognome = cCognome.Text.Replace("'", "''");
                string cameranome = libere.SelectedItems[i].Text;
                if (i > 0)
                    idpren++;
                else
                    firstid = idpren;
                //mConnection.cmdMySQL.CommandText = "UPDATE  numeroprenotazioni SET num = '" + idpren + "';INSERT INTO Prenotazioni (id,camera,codicecliente,arrivo,partenza,nome,cognome,tariffa,forfait,da,operatore,tipologia,tipostanza,arrangiamento,anticipo,pagata,iva,riferimento) VALUES('" + idpren + "','" + s.Text + "','" + customerId.ToString() + "','" + arr + "','" + par + "','" + cNome.Text + "','" + cCognome.Text + "','" + Convert.ToSingle(tarforfait) + "','" + Convert.ToInt16(forfait.Checked) + "','" + da.SelectedItem.ToString() + "','" + operatore.SelectedItem.ToString() + "','" + tipologia.SelectedItem.ToString() + "','" + tipostanza.SelectedItem.ToString() + "','" + arrangiamento.SelectedItem.ToString() + "','" + Convert.ToSingle(anticipo.Text) + "','" + Convert.ToInt16(pagata.Checked) + "','" + Convert.ToInt16(iva.Text) + "','"+riferimento.Text+"')";
                string ragso = ragsociale.Text.Replace("'", "\\'");
                if (ragso == "" || ragso == null)
                    cmd.CommandText = "UPDATE  numeroprenotazioni SET num = '" + idpren + "';INSERT INTO Prenotazioni (id,camera,codicecliente,arrivo,partenza,nome,cognome,tariffa,forfait,da,operatore,tipologia,tipostanza,arrangiamento,anticipo,pagata,iva,riferimento,note) VALUES('" + idpren + "','" + cameranome + "','" + customerId.ToString() + "','" + arr + "','" + par + "','" + Nome + "','" + Cognome + "','" + Convert.ToSingle(tarforfait.Text) + "','" + Convert.ToInt16(forfait.Checked) + "','" + da.SelectedItem.ToString() + "','" + operatore.SelectedItem.ToString() + "','" + tipologia.SelectedItem.ToString() + "','" + tipostanza.SelectedItem.ToString() + "','" + arrangiamento.SelectedItem.ToString() + "','" + anticipo.Text + "','" + Convert.ToInt16(pagata.Checked) + "','" + Convert.ToInt16(iva.Text) + "','" + riferimento.Text.ToString() + "','" + Note + "');";
                else
                    cmd.CommandText = "UPDATE  numeroprenotazioni SET num = '" + idpren + "';INSERT INTO Prenotazioni (id,camera,codicecliente,arrivo,partenza,nome,cognome,tariffa,forfait,da,operatore,tipologia,tipostanza,arrangiamento,anticipo,pagata,iva,riferimento,note) VALUES('" + idpren + "','" + cameranome + "','" + customerId.ToString() + "','" + arr + "','" + par + "','" + ragso + "','" + Cognome + "','" + Convert.ToSingle(tarforfait.Text) + "','" + Convert.ToInt16(forfait.Checked) + "','" + da.SelectedItem.ToString() + "','" + operatore.SelectedItem.ToString() + "','" + tipologia.SelectedItem.ToString() + "','" + tipostanza.SelectedItem.ToString() + "','" + arrangiamento.SelectedItem.ToString() + "','" + anticipo.Text + "','" + Convert.ToInt16(pagata.Checked) + "','" + Convert.ToInt16(iva.Text) + "','" + riferimento.Text.ToString() + "','" + Note + "');";

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

                mConnection.Close();
                if (forfait.Checked)
                {
                    tarforfait.Text = "0";
                }
            }
            /* foreach (ListViewItem s in libere.SelectedItems)
             {
                 mConnection.Open();
                 MySqlCommand cmd = mConnection.CreateCommand();
                 DateTime a = new DateTime(arrivo.Value.Year, arrivo.Value.Month, arrivo.Value.Day, 12, 0, 1);
                 DateTime p = new DateTime(partenza.Value.Year, partenza.Value.Month, partenza.Value.Day, 12, 0, 0);
                 string arr = a.ToString();
                 string par = p.ToString();

                 string Note = richTextBox1.Text.Replace("'", "''");
                 string Nome = cNome.Text.Replace("'", "''");
                 string Cognome = cCognome.Text.Replace("'", "''");
                 //mConnection.cmdMySQL.CommandText = "UPDATE  numeroprenotazioni SET num = '" + idpren + "';INSERT INTO Prenotazioni (id,camera,codicecliente,arrivo,partenza,nome,cognome,tariffa,forfait,da,operatore,tipologia,tipostanza,arrangiamento,anticipo,pagata,iva,riferimento) VALUES('" + idpren + "','" + s.Text + "','" + customerId.ToString() + "','" + arr + "','" + par + "','" + cNome.Text + "','" + cCognome.Text + "','" + Convert.ToSingle(tarforfait) + "','" + Convert.ToInt16(forfait.Checked) + "','" + da.SelectedItem.ToString() + "','" + operatore.SelectedItem.ToString() + "','" + tipologia.SelectedItem.ToString() + "','" + tipostanza.SelectedItem.ToString() + "','" + arrangiamento.SelectedItem.ToString() + "','" + Convert.ToSingle(anticipo.Text) + "','" + Convert.ToInt16(pagata.Checked) + "','" + Convert.ToInt16(iva.Text) + "','"+riferimento.Text+"')";
                 string ragso = ragsociale.Text.Replace("'", "\\'");

                 if (ragso == "" || ragso == null)
                     cmd.CommandText = "UPDATE  numeroprenotazioni SET num = '" + idpren + "';INSERT INTO Prenotazioni (id,camera,codicecliente,arrivo,partenza,nome,cognome,tariffa,forfait,da,operatore,tipologia,tipostanza,arrangiamento,anticipo,pagata,iva,riferimento,note) VALUES('" + idpren + "','" + s.Text + "','" + customerId.ToString() + "','" + arr + "','" + par + "','" + Nome + "','" + Cognome + "','" + Convert.ToSingle(tarforfait.Text) + "','" + Convert.ToInt16(forfait.Checked) + "','" + da.SelectedItem.ToString() + "','" + operatore.SelectedItem.ToString() + "','" + tipologia.SelectedItem.ToString() + "','" + tipostanza.SelectedItem.ToString() + "','" + arrangiamento.SelectedItem.ToString() + "','" + anticipo.Text + "','" + Convert.ToInt16(pagata.Checked) + "','" + Convert.ToInt16(iva.Text) + "','" + riferimento.Text.ToString() + "','" + Note + "');";
                 else
                     cmd.CommandText = "UPDATE  numeroprenotazioni SET num = '" + idpren + "';INSERT INTO Prenotazioni (id,camera,codicecliente,arrivo,partenza,nome,cognome,tariffa,forfait,da,operatore,tipologia,tipostanza,arrangiamento,anticipo,pagata,iva,riferimento,note) VALUES('" + idpren + "','" + s.Text + "','" + customerId.ToString() + "','" + arr + "','" + par + "','" + ragso + "','" + Cognome + "','" + Convert.ToSingle(tarforfait.Text) + "','" + Convert.ToInt16(forfait.Checked) + "','" + da.SelectedItem.ToString() + "','" + operatore.SelectedItem.ToString() + "','" + tipologia.SelectedItem.ToString() + "','" + tipostanza.SelectedItem.ToString() + "','" + arrangiamento.SelectedItem.ToString() + "','" + anticipo.Text + "','" + Convert.ToInt16(pagata.Checked) + "','" + Convert.ToInt16(iva.Text) + "','" + riferimento.Text.ToString() + "','" + Note + "');";

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

                 mConnection.Close();
             }*/
            MessageBox.Show("La prenotazione è stata inserita con successo!",
        "Fatto!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

            //readroom();
            if (pagata.Checked)
            {
                //mConnection.Close();
                FatturaoRicevuta f = new FatturaoRicevuta(msqlCon, firstid.ToString(), true);//,cliente, numerocamere,datear, datepar, tariffa, forfait,anticipo,iva);
                f.Show();
                f.Focus();

            }
            bel3.RefreshOverview();
            bel3.RefreshBook();
            bel3.calctableau();
            this.Close();

        }

        //oldreservation
        /*
         void prenotazione(int customerId)
        {
            for (int i = 0; i < libere.SelectedItems.Count; i++)
            {
                mConnection.Open();
                MySqlCommand cmd = mConnection.CreateCommand();
                DateTime a = new DateTime(arrivo.Value.Year, arrivo.Value.Month, arrivo.Value.Day, 12, 0, 1);
                DateTime p = new DateTime(partenza.Value.Year, partenza.Value.Month, partenza.Value.Day, 12, 0, 0);
                string arr = a.ToString();
                string par = p.ToString();

                string Note = richTextBox1.Text.Replace("'", "''");
                string Nome = cNome.Text.Replace("'", "''");
                string Cognome = cCognome.Text.Replace("'", "''");
                string cameranome = libere.SelectedItems[i].Text;
                if (i > 0)
                    idpren++;
                else
                    firstid = idpren;
                //mConnection.cmdMySQL.CommandText = "UPDATE  numeroprenotazioni SET num = '" + idpren + "';INSERT INTO Prenotazioni (id,camera,codicecliente,arrivo,partenza,nome,cognome,tariffa,forfait,da,operatore,tipologia,tipostanza,arrangiamento,anticipo,pagata,iva,riferimento) VALUES('" + idpren + "','" + s.Text + "','" + customerId.ToString() + "','" + arr + "','" + par + "','" + cNome.Text + "','" + cCognome.Text + "','" + Convert.ToSingle(tarforfait) + "','" + Convert.ToInt16(forfait.Checked) + "','" + da.SelectedItem.ToString() + "','" + operatore.SelectedItem.ToString() + "','" + tipologia.SelectedItem.ToString() + "','" + tipostanza.SelectedItem.ToString() + "','" + arrangiamento.SelectedItem.ToString() + "','" + Convert.ToSingle(anticipo.Text) + "','" + Convert.ToInt16(pagata.Checked) + "','" + Convert.ToInt16(iva.Text) + "','"+riferimento.Text+"')";
                string ragso = ragsociale.Text.Replace("'", "\\'");
                if (ragso == "" || ragso == null)
                    cmd.CommandText = "UPDATE  numeroprenotazioni SET num = '" + idpren + "';INSERT INTO Prenotazioni (id,camera,codicecliente,arrivo,partenza,nome,cognome,tariffa,forfait,da,operatore,tipologia,tipostanza,arrangiamento,anticipo,pagata,iva,riferimento,note) VALUES('" + idpren + "','" + cameranome + "','" + customerId.ToString() + "','" + arr + "','" + par + "','" + Nome + "','" + Cognome + "','" + Convert.ToSingle(tarforfait.Text) + "','" + Convert.ToInt16(forfait.Checked) + "','" + da.SelectedItem.ToString() + "','" + operatore.SelectedItem.ToString() + "','" + tipologia.SelectedItem.ToString() + "','" + tipostanza.SelectedItem.ToString() + "','" + arrangiamento.SelectedItem.ToString() + "','" + anticipo.Text + "','" + Convert.ToInt16(pagata.Checked) + "','" + Convert.ToInt16(iva.Text) + "','" + riferimento.Text.ToString() + "','" + Note + "');";
                else
                    cmd.CommandText = "UPDATE  numeroprenotazioni SET num = '" + idpren + "';INSERT INTO Prenotazioni (id,camera,codicecliente,arrivo,partenza,nome,cognome,tariffa,forfait,da,operatore,tipologia,tipostanza,arrangiamento,anticipo,pagata,iva,riferimento,note) VALUES('" + idpren + "','" + cameranome + "','" + customerId.ToString() + "','" + arr + "','" + par + "','" + ragso + "','" + Cognome + "','" + Convert.ToSingle(tarforfait.Text) + "','" + Convert.ToInt16(forfait.Checked) + "','" + da.SelectedItem.ToString() + "','" + operatore.SelectedItem.ToString() + "','" + tipologia.SelectedItem.ToString() + "','" + tipostanza.SelectedItem.ToString() + "','" + arrangiamento.SelectedItem.ToString() + "','" + anticipo.Text + "','" + Convert.ToInt16(pagata.Checked) + "','" + Convert.ToInt16(iva.Text) + "','" + riferimento.Text.ToString() + "','" + Note + "');";

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

                mConnection.Close();
                if(forfait.Checked)
                {
                    tarforfait.Text = "0";
                }
            }
          
        MessageBox.Show("La prenotazione è stata inserita con successo!",
        "Fatto!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

            //readroom();
            if (pagata.Checked)
            {
                //mConnection.Close();
                FatturaoRicevuta f = new FatturaoRicevuta(msqlCon, firstid.ToString(), true);//,cliente, numerocamere,datear, datepar, tariffa, forfait,anticipo,iva);
        f.Show();
                f.Focus();

            }
    bel3.RefreshOverview();
            bel3.RefreshBook();
            bel3.calctableau();
            this.Close();

    }*/
    //

    void prenotazioneExt(int customerId)
        {/*
            /*if (libere.SelectedItems.Count > 0)
            {
                foreach (ListViewItem s in libere.SelectedItems)
                {*/
                   /* mConnection.Open();
                    MySqlCommand cmd = mConnection.CreateCommand();
                    DateTime a = new DateTime(arrivo.Value.Year, arrivo.Value.Month, arrivo.Value.Day, 12, 0, 1);
                    DateTime p = new DateTime(partenza.Value.Year, partenza.Value.Month, partenza.Value.Day, 12, 0, 0);
                    string arr = a.ToString();
                    string par = p.ToString();

                    string Nome, Cognome, Note;
                    Nome = cNome.Text.Replace("'", "\\'");
                    Cognome = cCognome.Text.Replace("'", "\\'");
                    Note = richTextBox1.Text.Replace("'", "\\'");
                    string ragso = ragsociale.Text.Replace("'", "\\'");

                    if (ragso == "" || ragso == null)
                        cmd.CommandText = "UPDATE  numeroprenotazioni SET num = '" + idpren + "';INSERT INTO Prenotazioni (id,camera,codicecliente,arrivo,partenza,nome,cognome,tariffa,forfait,da,operatore,tipologia,tipostanza,arrangiamento,anticipo,pagata,iva,riferimento,note) VALUES('" + idpren + "','" + s.Text + "','" + customerId.ToString() + "','" + arr + "','" + par + "','" + Nome + "','" + Cognome + "','" + Convert.ToSingle(tarforfait.Text) + "','" + Convert.ToInt16(forfait.Checked) + "','" + da.SelectedItem.ToString() + "','" + operatore.SelectedItem.ToString() + "','" + tipologia.SelectedItem.ToString() + "','" + tipostanza.SelectedItem.ToString() + "','" + arrangiamento.SelectedItem.ToString() + "','" + anticipo.Text + "','" + Convert.ToInt16(pagata.Checked) + "','" + Convert.ToInt16(iva.Text) + "','" + riferimento.Text.ToString() + "','" + Note + "');";
                    else
                        cmd.CommandText = "UPDATE  numeroprenotazioni SET num = '" + idpren + "';INSERT INTO Prenotazioni (id,camera,codicecliente,arrivo,partenza,nome,cognome,tariffa,forfait,da,operatore,tipologia,tipostanza,arrangiamento,anticipo,pagata,iva,riferimento,note) VALUES('" + idpren + "','" + s.Text + "','" + customerId.ToString() + "','" + arr + "','" + par + "','" + ragso + "','" + Cognome + "','" + Convert.ToSingle(tarforfait.Text) + "','" + Convert.ToInt16(forfait.Checked) + "','" + da.SelectedItem.ToString() + "','" + operatore.SelectedItem.ToString() + "','" + tipologia.SelectedItem.ToString() + "','" + tipostanza.SelectedItem.ToString() + "','" + arrangiamento.SelectedItem.ToString() + "','" + anticipo.Text + "','" + Convert.ToInt16(pagata.Checked) + "','" + Convert.ToInt16(iva.Text) + "','" + riferimento.Text.ToString() + "','" + Note + "');";
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
                    mConnection.Close();*/

               // }
                /*MessageBox.Show("La prenotazione è stata inserita con successo!",
                    "Fatto!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                if (pagata.Checked)
                {
                    mConnection.Close();
                    FatturaoRicevuta f = new FatturaoRicevuta(msqlCon, idpren.ToString(), true);//,cliente, numerocamere,datear, datepar, tariffa, forfait,anticipo,iva);
                    f.Show();
                    f.Focus();
                }
                this.Close();*/
           // }
            /*else
            {
                MessageBox.Show("Seleziona una stanza per effettuare la prenotazione",
                  "Attenzione!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
            }*/
            //mConnection.cnMySQL.Open();

            //mConnection.cmdMySQL = mConnection.cnMySQL.CreateCommand();

            //mConnection.cmdMySQL.CommandText = "UPDATE  numeroprenotazioni SET num = '" + idpren + "'";

            //mConnection.cmdMySQL.ExecuteNonQuery();
            //mConnection.cnMySQL.Close();


            // readroom();
           // bel3.updateall();
        }
        string datetimeTodate(string day, string month, string year)
        {
            return day + "-" + month + "-" + year;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void libere_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        List<string> mPrezzo;
        List<string> mIva;
        private short p;
        private short p_2;
        private string p_3;
        private string p_4;
        private string p_5;
        private string p_6;
        private string p_7;
        private List<string> selectedroom;
        private string arrivo_2;
        private string partenza_2;
        private string da_2;
        private string operatore_2;
        private string tipostanza_2;
        private string tipoec;
        private string arrangiamento_2;
        private string note;
        private string tariffa;
        private string anticipo_2;
        private string iva_2;
        private string forfait_2;

        private void Prenotazione_Load(object sender, EventArgs e)
        {
            bel3.Hint.Location = new Point(Screen.PrimaryScreen.Bounds.Width - bel3.Hint.Bounds.Width, 64);
            bel3.Hint.label.Text = "Attenzione!!!\ndurante l'inserimento di una prenotazione\nricordati di specificare se il valore\ndella tariffa è per giorno o forfettario!";
            bel3.Hint.Focus();
            //if (mConnection != null)
            //{
            //    mConnection.cnMySQL.Open();
            //    mConnection.cmdMySQL = new MySqlCommand("select * from operatori ORDER BY nome ASC", mConnection.cnMySQL);
            //    mConnection.reader = mConnection.cmdMySQL.ExecuteReader();
            //    while (mConnection.reader.Read())
            //    {
            //        operatore.Items.Add(mConnection.reader.GetValue(0).ToString());
            //        operatore.SelectedItem = operatore.Items[0];
            //    }
            //    mConnection.cnMySQL.Close();

            //    mConnection.cnMySQL.Open();
            //    mConnection.cmdMySQL = new MySqlCommand("select * from fontiprenotazione ORDER BY nome ASC", mConnection.cnMySQL);
            //    mConnection.reader = mConnection.cmdMySQL.ExecuteReader();
            //    while (mConnection.reader.Read())
            //    {
            //        da.Items.Add(mConnection.reader.GetValue(0).ToString());
            //        da.SelectedItem = da.Items[0];
            //    }
            //    mConnection.cnMySQL.Close();

            //    mConnection.cnMySQL.Open();
            //    mConnection.cmdMySQL = new MySqlCommand("select * from tipostanza ORDER BY nome ASC", mConnection.cnMySQL);
            //    mConnection.reader = mConnection.cmdMySQL.ExecuteReader();
            //    while (mConnection.reader.Read())
            //    {
            //        tipostanza.Items.Add(mConnection.reader.GetValue(0).ToString());
            //        tipostanza.SelectedItem = tipostanza.Items[0];
            //    }
            //    mConnection.cnMySQL.Close();

            //    mConnection.cnMySQL.Open();
            //    mConnection.cmdMySQL = new MySqlCommand("select * from arrangiamento ORDER BY nome ASC", mConnection.cnMySQL);
            //    mConnection.reader = mConnection.cmdMySQL.ExecuteReader();
            //    while (mConnection.reader.Read())
            //    {
            //        arrangiamento.Items.Add(mConnection.reader.GetValue(0).ToString());

            //        arrangiamento.SelectedItem = arrangiamento.Items[0];
            //    }
            //    mConnection.cnMySQL.Close();

            //    mConnection.cnMySQL.Open();
            //    mConnection.cmdMySQL = new MySqlCommand("select * from tipologiaeconomica ORDER BY nome ASC", mConnection.cnMySQL);
            //    mConnection.reader = mConnection.cmdMySQL.ExecuteReader();
            //    while (mConnection.reader.Read())
            //    {
            //        tipologia.Items.Add(mConnection.reader.GetValue(0).ToString());

            //        tipologia.SelectedItem = tipologia.Items[0];
            //    }
            //    mConnection.cnMySQL.Close();

            //    mConnection.cnMySQL.Open();
            //    mConnection.cmdMySQL = new MySqlCommand("select * from numeroprenotazioni ORDER BY num ASC", mConnection.cnMySQL);

            //    mConnection.reader = mConnection.cmdMySQL.ExecuteReader();

            //    while (mConnection.reader.Read())
            //    {
            //        idpren = Convert.ToInt16(mConnection.reader.GetValue(0).ToString()) + 1;
            //    }
            //    // mConnection.cmdMySQL.ExecuteNonQuery();
            //    mConnection.cnMySQL.Close();

            //    mConnection.cnMySQL.Open();
            //    mConnection.cmdMySQL = new MySqlCommand("select * from tariffe ORDER BY nome ASC", mConnection.cnMySQL);

            //    mConnection.reader = mConnection.cmdMySQL.ExecuteReader();

            //    mPrezzo = new List<string>();
            //    mIva = new List<string>();
            //    while (mConnection.reader.Read())
            //    {
            //        mPrezzo.Add(mConnection.reader.GetValue(1).ToString());
            //        mIva.Add(mConnection.reader.GetValue(2).ToString());
            //    }
            //    // mConnection.cmdMySQL.ExecuteNonQuery();
            //    mConnection.cnMySQL.Close();
            //}

        }

        private void Prenotazione_Load()
        {
            if (mConnection != null)
            {
                if (mConnection.State == ConnectionState.Open)
                    mConnection.Close();
                if (mConnection.State != ConnectionState.Open)
                    mConnection.Open();

                MySqlCommand cmd = new MySqlCommand("select * from operatori ORDER BY nome ASC", mConnection);

                MySqlDataReader datareader = cmd.ExecuteReader();
                while (datareader.Read())
                {
                    operatore.Items.Add(datareader.GetValue(0).ToString());
                    operatore.SelectedItem = operatore.Items[0];
                }
                mConnection.Close();

                if (mConnection.State == ConnectionState.Open)
                    mConnection.Close();
                if (mConnection.State != ConnectionState.Open)
                    mConnection.Open();
                cmd = new MySqlCommand("select * from fontiprenotazione ORDER BY nome ASC", mConnection);
                datareader = cmd.ExecuteReader();
                while (datareader.Read())
                {
                    if (datareader.GetValue(0).ToString() != "None")
                    {
                        da.Items.Add(datareader.GetValue(0).ToString());
                        da.SelectedItem = da.Items[0];
                    }
                }
                mConnection.Close();

                if (mConnection.State == ConnectionState.Open)
                    mConnection.Close();
                if (mConnection.State != ConnectionState.Open)
                    mConnection.Open();
                cmd = new MySqlCommand("select * from tipostanza ORDER BY nome ASC", mConnection);
                datareader = cmd.ExecuteReader();
                while (datareader.Read())
                {
                    tipostanza.Items.Add(datareader.GetValue(0).ToString());
                    tipostanza.SelectedItem = tipostanza.Items[0];
                }
                mConnection.Close();

                if (mConnection.State == ConnectionState.Open)
                    mConnection.Close();
                if (mConnection.State != ConnectionState.Open)
                    mConnection.Open();
                cmd = new MySqlCommand("select * from arrangiamento ORDER BY nome ASC", mConnection);
                datareader = cmd.ExecuteReader();
                while (datareader.Read())
                {
                    arrangiamento.Items.Add(datareader.GetValue(0).ToString());

                    arrangiamento.SelectedItem = arrangiamento.Items[0];
                }
                mConnection.Close();

                if (mConnection.State == ConnectionState.Open)
                    mConnection.Close();
                if (mConnection.State != ConnectionState.Open)
                    mConnection.Open();
                cmd = new MySqlCommand("select * from tipologiaeconomica ORDER BY nome ASC", mConnection);
                datareader = cmd.ExecuteReader();
                while (datareader.Read())
                {
                    tipologia.Items.Add(datareader.GetValue(0).ToString());

                    tipologia.SelectedItem = tipologia.Items[0];
                }
                mConnection.Close();

                if (mConnection.State == ConnectionState.Open)
                    mConnection.Close();
                if (mConnection.State != ConnectionState.Open)
                    mConnection.Open();
                cmd = new MySqlCommand("select * from numeroprenotazioni ORDER BY num ASC", mConnection);

                datareader = cmd.ExecuteReader();

                while (datareader.Read())
                {
                    idpren = Convert.ToInt16(datareader.GetValue(0).ToString()) + 1;
                }
                // mConnection.cmdMySQL.ExecuteNonQuery();
                mConnection.Close();

                if (mConnection.State == ConnectionState.Open)
                    mConnection.Close();
                if (mConnection.State != ConnectionState.Open)
                    mConnection.Open();
                cmd = new MySqlCommand("select * from tariffe ORDER BY nome ASC", mConnection);

                datareader = cmd.ExecuteReader();

                mPrezzo = new List<string>();
                mIva = new List<string>();
                while (datareader.Read())
                {
                    mPrezzo.Add(datareader.GetValue(1).ToString());
                    mIva.Add(datareader.GetValue(2).ToString());
                }
                // mConnection.cmdMySQL.ExecuteNonQuery();
                mConnection.Close();
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (tarforfait.Text == "" || tarforfait.Text == null)
            {
                MessageBox.Show("Devi inserire una tariffa!",
                  "Fatto!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }
            if (libere.SelectedItems.Count > 0)
            {
                if (mConnection != null)
                {
                    //prenotazioneExt(id);
                    prenotazione(id);
                }

            }
        }

        private void operatore_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void mouseoverme(object sender, EventArgs e)
        {
            initializated = true;
        }

        private void showme(object sender, EventArgs e)
        {
            initializated = true;
        }

        private void Tariffa_SelectedIndexChanged(object sender, EventArgs e)
        {

        }



        private void button3_Click(object sender, EventArgs e)
        {
            if (tarforfait.Text == "" || tarforfait.Text == null)
            {
                MessageBox.Show("Devi inserire una tariffa!",
                  "Fatto!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }
            if (libere.SelectedItems.Count > 1)
            {
                MessageBox.Show("puoi selezionare solo una camera durante una modifica",
                "Attenzione!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }
            if (libere.SelectedItems.Count > 0)
            {
                int i = 0;
                int oldnumb = othercam.Count;
                if (libere.SelectedItems.Count != othercam.Count)
                    othercam = new List<string>();
                foreach (ListViewItem s in libere.SelectedItems)
                {
                    if (libere.SelectedItems.Count != othercam.Count)
                        othercam.Add(s.Text);
                    if (mConnection.State == ConnectionState.Open)
                        mConnection.Close();
                    if (mConnection.State != ConnectionState.Open)
                        mConnection.Open();
                    MySqlCommand cmd = mConnection.CreateCommand();
                    cmd.Connection = mConnection;
                    DateTime a = new DateTime(arrivo.Value.Year, arrivo.Value.Month, arrivo.Value.Day, 12, 0, 1);
                    DateTime p = new DateTime(partenza.Value.Year, partenza.Value.Month, partenza.Value.Day, 12, 0, 0);
                    string arr = a.ToString();
                    string par = p.ToString();
                    string Nome, Cognome, Note;
                    Nome = cNome.Text.Replace("'", "\\'");
                    Cognome = cCognome.Text.Replace("'", "\\'");
                    Note = richTextBox1.Text.Replace("'", "\\'");
                    string ragso = ragsociale.Text.Replace("'", "\\'");

                    if (ragso == "" || ragso == null)
                    {
                        if (i < oldnumb)
                            cmd.CommandText =
                                    "UPDATE Prenotazioni SET id='" + (prenotazioneID) + "', camera='" + s.Text + "',codicecliente='" + id + "',arrivo='" + arr + "',partenza='" + par + "',nome= '" + Nome + "',cognome='" + Cognome + "',tariffa='" + Convert.ToSingle(tarforfait.Text) + "',forfait='" + Convert.ToInt16(forfait.Checked) + "',da='" + da.SelectedItem.ToString() + "',operatore='" + operatore.SelectedItem.ToString() + "',tipologia='" + tipologia.SelectedItem.ToString() + "',tipostanza= '" + tipostanza.SelectedItem.ToString() + "',arrangiamento='" + arrangiamento.SelectedItem.ToString() + "',anticipo=  '" + anticipo.Text + "',pagata= '" + Convert.ToInt16(pagata.Checked) + "',iva='" + Convert.ToInt16(iva.Text) + "',riferimento='" + riferimento.Text.ToString() + "',note='" + Note + "',note='" + Note + "' where prenotazioni.id=" + (prenotazioneID) + " and prenotazioni.camera=" + othercam[i]+";";
                        else
                            cmd.CommandText = "INSERT INTO Prenotazioni (id,camera,codicecliente,arrivo,partenza,nome,cognome,tariffa,forfait,da,operatore,tipologia,tipostanza,arrangiamento,anticipo,pagata,iva,riferimento,note) VALUES('" + prenotazioneID + "','" + s.Text + "','" + id + "','" + arr + "','" + par + "','" + Nome + "','" + Cognome + "','" + Convert.ToSingle(tarforfait.Text) + "','" + Convert.ToInt16(forfait.Checked) + "','" + da.SelectedItem.ToString() + "','" + operatore.SelectedItem.ToString() + "','" + tipologia.SelectedItem.ToString() + "','" + tipostanza.SelectedItem.ToString() + "','" + arrangiamento.SelectedItem.ToString() + "','" + anticipo.Text + "','" + Convert.ToInt16(pagata.Checked) + "','" + Convert.ToInt16(iva.Text) + "','" + riferimento.Text.ToString() + "','" + Note + "');";


                    }
                    else
                    {
                        if (i < oldnumb)
                        {
                            cmd.CommandText =
                            "UPDATE Prenotazioni SET id='" + (prenotazioneID) + "', camera='" + s.Text + "',codicecliente='" + id + "',arrivo='" + arr + "',partenza='" + par + "',nome= '" + ragso + "',cognome='" + Cognome + "',tariffa='" + Convert.ToSingle(tarforfait.Text) + "',forfait='" + Convert.ToInt16(forfait.Checked) + "',da='" + da.SelectedItem.ToString() + "',operatore='" + operatore.SelectedItem.ToString() + "',tipologia='" + tipologia.SelectedItem.ToString() + "',tipostanza= '" + tipostanza.SelectedItem.ToString() + "',arrangiamento='" + arrangiamento.SelectedItem.ToString() + "',anticipo=  '" + anticipo.Text + "',pagata= '" + Convert.ToInt16(pagata.Checked) + "',iva='" + Convert.ToInt16(iva.Text) + "',riferimento='" + riferimento.Text.ToString() + "',note='" + Note + "',note='" + Note + "' where prenotazioni.id=" + (prenotazioneID) + " and prenotazioni.camera=" + othercam[i]+";";
                        }
                        else
                        {
                            cmd.CommandText = "INSERT INTO Prenotazioni (id,camera,codicecliente,arrivo,partenza,nome,cognome,tariffa,forfait,da,operatore,tipologia,tipostanza,arrangiamento,anticipo,pagata,iva,riferimento,note) VALUES('" + prenotazioneID + "','" + s.Text + "','" + id + "','" + arr + "','" + par + "','" + ragso + "','" + Cognome + "','" + Convert.ToSingle(tarforfait.Text) + "','" + Convert.ToInt16(forfait.Checked) + "','" + da.SelectedItem.ToString() + "','" + operatore.SelectedItem.ToString() + "','" + tipologia.SelectedItem.ToString() + "','" + tipostanza.SelectedItem.ToString() + "','" + arrangiamento.SelectedItem.ToString() + "','" + anticipo.Text + "','" + Convert.ToInt16(pagata.Checked) + "','" + Convert.ToInt16(iva.Text) + "','" + riferimento.Text.ToString() + "','" + Note + "');";

                        }
                    }
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
                    mConnection.Close();
                    i++;
                }

                MessageBox.Show("La prenotazione è stata inserita con successo!",
                    "Fatto!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                if (pagata.Checked)
                {
                    //mConnection.cnMySQL.Close();
                    // FatturaoRicevuta f = new FatturaoRicevuta(mConnection, prenotazioneID.ToString());//,cliente, numerocamere,datear, datepar, tariffa, forfait,anticipo,iva);
                    FatturaoRicevuta f = new FatturaoRicevuta(msqlCon, prenotazioneID.ToString(), true);//,cliente, numerocamere,datear, datepar, tariffa, forfait,anticipo,iva);

                    f.Show();
                    f.Focus();
                }
                this.Close();

            }
            else
            {
                if (origcam != null)
                {
                    if (origcam.Count > 0)
                    {
                        foreach (string s in origcam)
                        {
                            if (mConnection.State == ConnectionState.Open)
                                mConnection.Close();
                            if (mConnection.State != ConnectionState.Open)
                                mConnection.Open();
                            MySqlCommand cmd = mConnection.CreateCommand();
                            DateTime a = new DateTime(arrivo.Value.Year, arrivo.Value.Month, arrivo.Value.Day, 12, 0, 1);
                            DateTime p = new DateTime(partenza.Value.Year, partenza.Value.Month, partenza.Value.Day, 12, 0, 0);
                            string arr = a.ToString();
                            string par = p.ToString();
                            string Nome, Cognome, Note;
                            Nome = cNome.Text.Replace("'", "\\'");
                            Cognome = cCognome.Text.Replace("'", "\\'");
                            Note = richTextBox1.Text.Replace("'", "\\'");
                            string ragso = ragsociale.Text.Replace("'", "\\'");

                            if (ragso == "" || ragso == null)
                                cmd.CommandText =
                                       "UPDATE Prenotazioni SET id='" + (prenotazioneID) + "', camera='" + s + "',codicecliente='" + id + "',arrivo='" + arr + "',partenza='" + par + "',nome= '" + Nome + "',cognome='" + Cognome + "',tariffa='" + tarforfait.Text + "',forfait='" + Convert.ToInt16(forfait.Checked) + "',da='" + da.SelectedItem.ToString() + "',operatore='" + operatore.SelectedItem.ToString() + "',tipologia='" + tipologia.SelectedItem.ToString() + "',tipostanza= '" + tipostanza.SelectedItem.ToString() + "',arrangiamento='" + arrangiamento.SelectedItem.ToString() + "',anticipo=  '" + anticipo.Text + "',pagata= '" + Convert.ToInt16(pagata.Checked) + "',iva='" + Convert.ToInt16(iva.Text) + "',riferimento='" + riferimento.Text.ToString() + "', note='" + Note + "' where prenotazioni.id=" + (prenotazioneID) + " and prenotazioni.camera='" + s + "'";
                            else
                                cmd.CommandText =
                              "UPDATE Prenotazioni SET id='" + (prenotazioneID) + "', camera='" + s + "',codicecliente='" + id + "',arrivo='" + arr + "',partenza='" + par + "',nome= '" + ragso + "',cognome='" + Cognome + "',tariffa='" + tarforfait.Text + "',forfait='" + Convert.ToInt16(forfait.Checked) + "',da='" + da.SelectedItem.ToString() + "',operatore='" + operatore.SelectedItem.ToString() + "',tipologia='" + tipologia.SelectedItem.ToString() + "',tipostanza= '" + tipostanza.SelectedItem.ToString() + "',arrangiamento='" + arrangiamento.SelectedItem.ToString() + "',anticipo=  '" + anticipo.Text + "',pagata= '" + Convert.ToInt16(pagata.Checked) + "',iva='" + Convert.ToInt16(iva.Text) + "',riferimento='" + riferimento.Text.ToString() + "', note='" + Note + "' where prenotazioni.id=" + (prenotazioneID) + " and prenotazioni.camera='" + s + "'";

                            cmd.ExecuteNonQuery();
                            mConnection.Close();
                        }
                        MessageBox.Show("La prenotazione è stata inserita con successo!",
                            "Fatto!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        if (pagata.Checked)
                        {
                            //mConnection.cnMySQL.Close();
                            FatturaoRicevuta f = new FatturaoRicevuta(msqlCon, prenotazioneID.ToString(), true);//,cliente, numerocamere,datear, datepar, tariffa, forfait,anticipo,iva);
                            f.Show();
                        }
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Seleziona una o più camere",
                          "Attenzione!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Seleziona una o più camere",
                      "Attenzione!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            bel3.RefreshBook();
            bel3.RefreshOverview();
            bel3.calctableau();
        }

        private void cNome_TextChanged(object sender, EventArgs e)
        {

        }

        private void da_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (da.Text == "Expedia")
            {
                if (ragsociale.Enabled)
                    ragsociale.Text = "Expedia Virtual Card 333 108th Avenue NE Bellevue, WA, 98004 USA";
                //pagata.Checked = true;
                tarforfait.Text = "0";
            }
            if (da.Text == "Orbitz")
            {
                if (ragsociale.Enabled)
                    ragsociale.Text = "Orbitz Worldwide";
            }
        }

        private void tipologia_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tipologia.Text == "Non rimb.")
            {
                pagata.Checked = true;
            }
        }

        private void tipostanza_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void tarforfait_TextChanged(object sender, EventArgs e)
        {

        }

        private void riferimento_TextChanged(object sender, EventArgs e)
        {

        }

        private void cCognome_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void telefoni_TextChanged(object sender, EventArgs e)
        {

        }

        private void forfait_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void Giorni_Click(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void iva_TextChanged(object sender, EventArgs e)
        {

        }

        private void pagata_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void arrangiamento_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }


}
