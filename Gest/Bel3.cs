using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql;
using MySql.Data;
using MySql.Data.MySqlClient;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Net;
using System.Net.Sockets;
using Bel3.Gest;
namespace Bel3
{


    public partial class Bel3 : Form
    {
        List<fontiprenotazione> Colors;
        DatabaseSettings mDatabaseSettings;

        public Form2 Hint;
        private string server;
        private string database;
        private string uid;
        private string password;
        public int inarrivo = 0;
        public int camerelibere = 0;
        // private MySqlConnection cnMySQL;
        connectoToMySql mConnection;
        string roomname = "", oldroomname = "";
        bool isbusy = false;

        public Bel3()
        {

            InitializeComponent();
            mDatabaseSettings = new DatabaseSettings();

            server = mDatabaseSettings.mServer;
            database = mDatabaseSettings.mDatabase;
            uid = mDatabaseSettings.mUser;
            password = mDatabaseSettings.mPassword;
            string cnMySQLString;
            cnMySQLString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
            mConnection = new connectoToMySql(server, database, uid, password);
            //cnMySQL = new MySqlConnection(cnMySQLString);
            Colors = new List<fontiprenotazione>();
            readcolors();
            createoverview();
            createtableau();
            updateall();
            dateTimePicker1.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

            IPHostEntry host;
            string localIP = "";
            host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    myip = ip.ToString();
                    break;
                }
            }
            InitTimer();
            InitTimer2();
        }

        string myip;

        void recreateall()
        {

            tableauhascreated = false;
            TableauCreated = false;
            createoverview();
            adjusttableau();
            Colors = new List<fontiprenotazione>();
            readcolors();
            updateall();
        }

        void adjusttableau()
        {
            DateTime oldyear = new DateTime(DateTime.Now.Year - 1, DateTime.Now.Month, DateTime.Now.Day);

            DateTime newyear = new DateTime(DateTime.Now.Year + 1, DateTime.Now.Month, DateTime.Now.Day);
            TimeSpan span = newyear - oldyear;
            for (int i = 0; i < span.Days; i++)
            {
                DateTime currentday = oldyear.AddDays(i);
                string day = currentday.DayOfWeek.ToString();
                string wday = day.Substring(0, 3);
                tableau_grid.Rows[i].HeaderCell.Value = currentday.ToShortDateString() + "\n" + wday;


            }
            TableauCreated = true;
            tableauhascreated = true;
        }

        public void updateall()
        {
            // Panoramica.Rows.Clear();
            Prenotazioni.Rows.Clear();
            ListaClienti.Rows.Clear();
         

            RefreshOverview();
            RefreshBook();
            calctableau();
            refreshlistaclienti();
            Sospesi.Rows.Clear();
            refreshsospesi();
        }

        void refreshsospesi()
        {
            //sospesi.Items.Clear();

            if (mConnection != null)
            {
                if (!checkBox3.Checked && !checkBox4.Checked)
                    mConnection.cmdMySQL = new MySqlCommand(
                           "SELECT id, nome, cognome, pagata FROM prenotazioni where prenotazioni.checkin=0 ORDER BY prenotazioni.arrivo,prenotazioni.partenza ASC;", mConnection.cnMySQL);
                else
                {
                    if (checkBox3.Checked )
                    mConnection.cmdMySQL = new MySqlCommand(
                      "SELECT id, nome, cognome, pagata FROM prenotazioni where prenotazioni.checkout=1 ORDER BY prenotazioni.arrivo,prenotazioni.partenza ASC;", mConnection.cnMySQL);
                    if (checkBox4.Checked)
                        mConnection.cmdMySQL = new MySqlCommand(
                          "SELECT id, nome, cognome, pagata FROM prenotazioni where prenotazioni.checkin=1 ORDER BY prenotazioni.arrivo,prenotazioni.partenza ASC;", mConnection.cnMySQL);
             
                }
                if (mConnection.cnMySQL.State != ConnectionState.Closed)
                    mConnection.cnMySQL.Close();
                if (mConnection.cnMySQL.State != ConnectionState.Open)
                    mConnection.cnMySQL.Open();
                mConnection.reader = mConnection.cmdMySQL.ExecuteReader();

                // print the CustomerID of each record

                while (mConnection.reader.Read())
                {
                    bool pagata = mConnection.reader.GetBoolean(3);
                    string nome = mConnection.reader.GetString(2) + " " + mConnection.reader.GetString(1);
                    string pren = mConnection.reader.GetValue(0).ToString();
                    if (!pagata)
                    {
                        DataGridViewRow row = (DataGridViewRow)Sospesi.Rows[0].Clone();
                        row.Cells[0].Value = pren.ToString();
                        row.Cells[1].Value = nome.ToString();
                        Sospesi.Rows.Add(row);
                    }
                }
                mConnection.cnMySQL.Close();
            }
        }


        public void readcolors()
        {

            if (Colors.Count > 0)
            {
                Colors.Clear();
            }
            if (mConnection != null)
            {
                try
                {
                    if (mConnection.cnMySQL.State != ConnectionState.Closed)
                        mConnection.cnMySQL.Close();

                    if (mConnection.cnMySQL.State != ConnectionState.Open)
                        mConnection.cnMySQL.Open();
                    mConnection.cmdMySQL = new MySqlCommand(
                                "SELECT * FROM fontiprenotazione;", mConnection.cnMySQL);
                    mConnection.reader = mConnection.cmdMySQL.ExecuteReader();

                    while (mConnection.reader.Read())
                    {
                        Color c = Color.FromArgb(Convert.ToInt16(mConnection.reader.GetValue(1)), Convert.ToInt16(mConnection.reader.GetValue(2)), Convert.ToInt16(mConnection.reader.GetValue(3)));
                        Colors.Add(new fontiprenotazione(mConnection.reader.GetValue(0).ToString(), c));
                    }
                    mConnection.cnMySQL.Close();
                }
                catch
                {
                }
            }
        }
        private void colorme(string da, int date, int room)
        {
            foreach (fontiprenotazione f in Colors)
            {
                if (da == f.nome)
                {
                    tableau_grid.Rows[date].Cells[room].Style.BackColor = f.color;
                    return;
                }
            }




            if (da == "lenzuola")
            {
                Color c = Color.FromArgb(128, 128, 128);
                tableau_grid.Rows[date].Cells[room].Style.BackColor = c;
            }
            else if (da == "default")
            {
                Color c = Color.FromArgb(255, 255, 192);
                tableau_grid.Rows[date].Cells[room].Style.BackColor = c;

            }
            else
                tableau_grid.Rows[date].Cells[room].Style.BackColor = Color.White;

        }

        void refreshlistaclienti()
        {
            ListaClienti.Rows.Clear();
            if (mConnection != null)
            {
                if (mConnection.cnMySQL.State != ConnectionState.Closed)
                    mConnection.cnMySQL.Close();

                if (mConnection.cnMySQL.State != ConnectionState.Open)
                    mConnection.cnMySQL.Open();
                mConnection.cmdMySQL = new MySqlCommand("select * from clienti Order by cognome ASC", mConnection.cnMySQL);

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

                    // for (int i = 0; i < mConnection.reader.FieldCount; i++)
                    //   thisrow += mConnection.reader.GetValue(i).ToString() + ",";  
                    DataGridViewRow row = new DataGridViewRow();
                    string[] values = new string[6];
                    values[0] = mConnection.reader.GetValue(0).ToString();
                    values[1] = mConnection.reader.GetValue(1).ToString();
                    values[2] = mConnection.reader.GetValue(2).ToString();
                    values[3] = mConnection.reader.GetValue(3).ToString();
                    values[4] = mConnection.reader.GetValue(4).ToString();
                    values[5] = mConnection.reader.GetValue(5).ToString();
                    row.CreateCells(ListaClienti, values);
                    ListaClienti.Rows.Add(row);
                }
                mConnection.cnMySQL.Close();
            }
        }

        public void calctableau()
        {
          
            foreach (DataGridViewRow row in tableau_grid.Rows)
            {
                foreach (DataGridViewCell c in row.Cells)
                {
                    c.Value = "";
                    c.Style.BackColor = Color.FromArgb(255, 255, 192);
                }
            }

            if (mConnection.cnMySQL.State != ConnectionState.Closed)
                mConnection.cnMySQL.Close();

            if (mConnection.cnMySQL.State != ConnectionState.Open)
                mConnection.cnMySQL.Open();
            mConnection.cmdMySQL = new MySqlCommand("SELECT prenotazioni.camera, prenotazioni.arrivo,prenotazioni.partenza,prenotazioni.nome,prenotazioni.cognome,prenotazioni.da FROM prenotazioni;", mConnection.cnMySQL);

            mConnection.reader = mConnection.cmdMySQL.ExecuteReader();

            // print the CustomerID of each record
            while (mConnection.reader.Read())
            {
                string camera = mConnection.reader.GetValue(0).ToString();
                string arr = mConnection.reader.GetValue(1).ToString();
                string par = mConnection.reader.GetValue(2).ToString();
                string nome = mConnection.reader.GetValue(3).ToString();
                string cognome = mConnection.reader.GetValue(4).ToString();
                string da = mConnection.reader.GetValue(5).ToString();

                DateTime arrival, departure;
                DateTime oldyear = new DateTime(DateTime.Now.Year - 1, DateTime.Now.Month, DateTime.Now.Day);
                DateTime newyear = new DateTime(DateTime.Now.Year + 1, DateTime.Now.Month, DateTime.Now.Day);

                if (arr != "" && par != "")
                {
                    arrival = new DateTime(DateTime.Parse(arr).Year, DateTime.Parse(arr).Month, DateTime.Parse(arr).Day);
                    departure = new DateTime(DateTime.Parse(par).Year, DateTime.Parse(par).Month, DateTime.Parse(par).Day);
                    int room = 0;
                    for (int i = 0; i < tableau_grid.ColumnCount; i++)
                    {
                        if (tableau_grid.Columns[i].Name == camera)
                        {
                            room = i;
                            break;
                        }
                    }

                    TimeSpan a = arrival - oldyear;
                    int ar = a.Days;
                    TimeSpan p = departure - arrival;
                    TimeSpan alltime = newyear - oldyear;
                    int pa = a.Days + p.Days;
                    if (pa > 0)
                    {
                        while (ar < 0)
                        {
                            ar++;
                        }

                        while (pa > alltime.Days)
                        {
                            pa--;
                        }

                        for (int date = ar; date < pa; date++)
                        {
                            tableau_grid.Rows[date].Cells[room].Value = cognome + " " + nome;
                            colorme(da, date, room);
                        }

                    }


                }
                else
                {
                    arrival = new DateTime(3000, 12, 31);
                    departure = new DateTime(3000, 12, 31);
                }


            }
            mConnection.cnMySQL.Close();

            if (mConnection.cnMySQL.State != ConnectionState.Closed)
                mConnection.cnMySQL.Close();

            if (mConnection.cnMySQL.State != ConnectionState.Open)
                mConnection.cnMySQL.Open();
            mConnection.cmdMySQL = new MySqlCommand("SELECT * FROM cambiolenzuola;", mConnection.cnMySQL);

            mConnection.reader = mConnection.cmdMySQL.ExecuteReader();

            // print the CustomerID of each record
            while (mConnection.reader.Read())
            {

                string arr = mConnection.reader.GetValue(1).ToString().Substring(0, 10);

                DateTime arrival = new DateTime(DateTime.Parse(arr).Year, DateTime.Parse(arr).Month, DateTime.Parse(arr).Day);
                // departure = new DateTime(DateTime.Parse(par).Year, DateTime.Parse(par).Month, DateTime.Parse(par).Day);
                int room = Convert.ToInt32(mConnection.reader.GetValue(0).ToString());
                for (int i = 0; i < tableau_grid.ColumnCount - 1; i++)
                {
                    if (tableau_grid.Columns[i].Name == room.ToString())
                    {
                        room = i;
                        break;
                    }
                }
                int date = 0;
                for (int i = 0; i < tableau_grid.Rows.Count - 1; i++)
                {
                    if (tableau_grid.Rows[i].HeaderCell.Value.ToString().Substring(0, 10) == mConnection.reader.GetValue(1).ToString().Substring(0, 10))
                    {
                        date = i;
                        break;
                    }
                }




                colorme("lenzuola", date, room);

            }
            mConnection.cnMySQL.Close();

            foreach (DataGridViewRow row in tableau_grid.Rows)
            {
                int stanzelibere = row.Cells.Count - 1;
                for (int i = 1; i < row.Cells.Count; i++)
                {
                    if (row.Cells[i].Value != "")
                    {
                        stanzelibere--;
                    }
                }
                row.Cells[0].Value = stanzelibere - 7;
            }
          //  tableau_grid.FirstDisplayedScrollingRowIndex = currenttablauindex;
            foreach (DataGridViewRow r in tableau_grid.Rows)
            {
                if (DateTime.Now.ToShortDateString() == r.HeaderCell.Value.ToString().Substring(0, 10))
                {
                    if (currenttablauindex < -0)
                        currenttablauindex = r.Index;

                    tableau_grid.FirstDisplayedScrollingRowIndex = currenttablauindex;
                }
            }

        }
        int currenttablauindex =-100;
        void createtableau()
        {
            if (mConnection != null)
            {
                tableau_grid.Columns.Clear();

                tableau_grid.Rows.Clear();
                System.Drawing.Font f = new System.Drawing.Font("Arial", 8, FontStyle.Bold);
                tableau_grid.Font = f;
                tableau_grid.Columns.Add("CamereLibere", "Camere\rLibere");
                tableau_grid.Columns[0].Width = 64;
                tableau_grid.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
                if (mConnection.cnMySQL.State != ConnectionState.Closed)
                    mConnection.cnMySQL.Close();

                if (mConnection.cnMySQL.State != ConnectionState.Open)
                    mConnection.cnMySQL.Open();

                mConnection.cmdMySQL = new MySqlCommand("SELECT camere.Name,camere.doccia FROM camere;", mConnection.cnMySQL);
                mConnection.reader = mConnection.cmdMySQL.ExecuteReader();
                int c = 1;

                // print the CustomerID of each record
                while (mConnection.reader.Read())
                {
                    string doccia = mConnection.reader.GetValue(1).ToString();
                    string sdoc = "v";
                    if (doccia == "True")
                        sdoc = "d";
                    tableau_grid.Columns.Add(mConnection.reader.GetValue(0).ToString(), mConnection.reader.GetValue(0).ToString()+" "+sdoc);
                    tableau_grid.Columns[c].Width = 64;
                    tableau_grid.Columns[c].SortMode = DataGridViewColumnSortMode.NotSortable;
                    c++;
                }
                mConnection.cnMySQL.Close();
                DateTime oldyear = new DateTime(DateTime.Now.Year - 1, DateTime.Now.Month, DateTime.Now.Day);

                DateTime newyear = new DateTime(DateTime.Now.Year + 1, DateTime.Now.Month, DateTime.Now.Day);
                TimeSpan span = newyear - oldyear;
                for (int i = 0; i < span.Days; i++)
                {
                    if (i == 729)
                    {
                        int pollo = 0;
                    }
                    DataGridViewRow row = new DataGridViewRow();
                    row.Visible = true;
                    row.MinimumHeight = 40;
                    row.Height = 40;
                    DateTime currentday = oldyear.AddDays(i);
                    string day = currentday.DayOfWeek.ToString();
                    string wday = day.Substring(0, 3);
                    row.HeaderCell.Value = currentday.ToShortDateString() + "\n" + wday;

                    // row.SortMode = DataGridViewColumnSortMode.NotSortable;
                    tableau_grid.Rows.Add(row);
                }
                tableau_grid.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
                tableau_grid.RowHeadersVisible = true;
                TableauCreated = true;
                tableauhascreated = true;
            }
        }

        void colorme(DataGridViewRow row, int r, int g, int b)
        {
            row.DefaultCellStyle.BackColor = Color.FromArgb(255, r, g, b);
        }
        public void RefreshBook()
        {
            mConnection.cnMySQL.Open();
            string query = "";
            if (riferimentotext.Text == "" && CognomeP.Text == "" && NomeP.Text == "" && datarr.Text == "" && data.Text == "")
            {
                query = "SELECT * FROM prenotazioni LEFT JOIN fontiprenotazione ON prenotazioni.da=fontiprenotazione.nome ORDER BY str_to_date(arrivo, '%d/%m/%Y') asc";
            }
            else
            {

                if (CognomeP.Text != "" && NomeP.Text != "" && datarr.Text != "" && data.Text != "")
                {
                    query = "SELECT * FROM prenotazioni LEFT JOIN fontiprenotazione ON prenotazioni.da=fontiprenotazione.nome where substring(prenotazioni.nome ,1," + NomeP.Text.Length + ")= '" + NomeP.Text + "' AND substring(cognome ,1," + CognomeP.Text.Length + ")= '" + CognomeP.Text + "' AND substring(arrivo ,1," + datarr.Text.Length + ")= '" + datarr.Text + "' AND substring(partenza ,1," + data.Text.Length + ")= '" + data.Text + "' ORDER BY prenotazioni.cognome ASC;";

                }
                else
                {
                    if (CognomeP.Text != "" && datarr.Text != "" && data.Text != "")
                    {
                        query = "SELECT * FROM prenotazioni LEFT JOIN fontiprenotazione ON prenotazioni.da=fontiprenotazione.nome where substring(cognome ,1," + CognomeP.Text.Length + ")= '" + CognomeP.Text + "' AND substring(arrivo ,1," + datarr.Text.Length + ")= '" + datarr.Text + "' AND substring(partenza ,1," + data.Text.Length + ")= '" + data.Text + "' ORDER BY prenotazioni.cognome ASC;";

                    }
                    else
                    {

                        if (datarr.Text != "")
                        {
                            query = "SELECT * FROM prenotazioni LEFT JOIN fontiprenotazione ON prenotazioni.da=fontiprenotazione.nome where substring(arrivo ,1," + datarr.Text.Length + ")= '" + datarr.Text + "' ORDER BY prenotazioni.camera ASC;";

                        }
                        else
                        {
                            if (data.Text != "")
                            {
                                query = "SELECT * FROM prenotazioni LEFT JOIN fontiprenotazione ON prenotazioni.da=fontiprenotazione.nome where prenotazioni.checkout=0 and substring(partenza ,1," + data.Text.Length + ")= '" + data.Text + "' ORDER BY prenotazioni.camera ASC;";
                            }
                            else
                            {
                                if (riferimentotext.Text != "")
                                {
                                    query = "SELECT * FROM prenotazioni LEFT JOIN fontiprenotazione ON prenotazioni.da=fontiprenotazione.nome where substring(riferimento ,1," + riferimentotext.Text.Length + ")= '" + riferimentotext.Text + "' ORDER BY prenotazioni.cognome ASC;";
                                }
                                if (NomeP.Text != "" && CognomeP.Text != "")
                                {
                                    query = "SELECT * FROM prenotazioni LEFT JOIN fontiprenotazione ON prenotazioni.da=fontiprenotazione.nome where substring(prenotazioni.nome ,1," + NomeP.Text.Length + ")= '" + NomeP.Text + "' AND substring(cognome ,1," + CognomeP.Text.Length + ")= '" + CognomeP.Text + "' ORDER BY prenotazioni.cognome ASC;";
                                }
                                else
                                {
                                    if (NomeP.Text != "")
                                    {
                                        query = "SELECT * FROM prenotazioni LEFT JOIN fontiprenotazione ON prenotazioni.da=fontiprenotazione.nome where substring(prenotazioni.nome ,1," + NomeP.Text.Length + ")= '" + NomeP.Text + "' ORDER BY prenotazioni.cognome ASC;";
                                    }
                                    if (CognomeP.Text != "")
                                    {
                                        query = "SELECT * FROM prenotazioni LEFT JOIN fontiprenotazione ON prenotazioni.da=fontiprenotazione.nome where substring(cognome ,1," + CognomeP.Text.Length + ")= '" + CognomeP.Text + "' ORDER BY prenotazioni.cognome ASC;";
                                    }
                                }
                            }
                        }
                    }
                }
            }
            //Open cnMySQL
            if (mConnection.cnMySQL.State == ConnectionState.Open)
            {
                Prenotazioni.Rows.Clear();
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, mConnection.cnMySQL);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    DataGridViewRow row = (DataGridViewRow)Prenotazioni.Rows[0].Clone();

                    row.Cells[0].Value = dataReader.GetString(1);
                    row.Cells[1].Value = dataReader.GetValue(8).ToString() + " " + dataReader.GetValue(7).ToString();
                    row.Cells[2].Value = dataReader.GetString(19);
                    row.Cells[3].Value = dataReader.GetString(3).Substring(0, 10);
                    row.Cells[4].Value = dataReader.GetString(4).Substring(0, 10);
                    row.Cells[5].Value = dataReader.GetString(5);
                    row.Cells[6].Value = dataReader.GetBoolean(6) ? "Forfait" : "No";
                    row.Cells[7].Value = dataReader.GetBoolean(16)?"Pagata":"No";
                    row.Cells[8].Value = dataReader.GetBoolean(17)?"Effettuato":"No";
                    row.Cells[9].Value = dataReader.GetBoolean(18)?"Effettuato":"No";
                    row.Cells[10].Value = dataReader.GetString(12);
                    row.Cells[11].Value = dataReader.GetString(13);
                    row.Cells[12].Value = dataReader.GetString(14);
                    row.Cells[13].Value = dataReader.GetString(20);
                    row.Cells[14].Value = dataReader.GetString(9);
                    row.Cells[15].Value = dataReader.GetString(15);
                    row.Cells[16].Value = dataReader.GetString(11);
                    row.Cells[17].Value = dataReader.GetString(10);
                    row.Cells[18].Value = dataReader.GetString(0);
                    row.Cells[19].Value = dataReader.GetString(2);
                    var prova = dataReader.GetInt16(22);
                    var prova2 = dataReader.GetInt16(23);
                    var prova3 = dataReader.GetInt16(24);
                    colorme(row, dataReader.GetInt16(22),  dataReader.GetInt16(23), dataReader.GetInt16(24));
                    if (checkBox2.Checked)
                        Prenotazioni.Rows.Add(row);
                    else
                    {
                        if (!dataReader.GetBoolean(17))
                        {
                            Prenotazioni.Rows.Add(row);
                        }
                    }
                }
                //close Data Reader
                dataReader.Close();

                //close Connection
                mConnection.cnMySQL.Close();
            }
            mConnection.cnMySQL.Close();
        }
        public void createoverview()
        {
            mConnection.cnMySQL.Open();

            string query = "SELECT * FROM camere";

            if (mConnection.cnMySQL.State == ConnectionState.Open)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, mConnection.cnMySQL);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();
                //Open cnMySQL
                while (dataReader.Read())
                {
                    DataGridViewRow row = (DataGridViewRow)Panoramica.Rows[0].Clone();
                    row.Cells[0].Value = dataReader.GetString(0);
                    Panoramica.Rows.Add(row);
                }
                mConnection.cnMySQL.Close();
            }

        }
        public void RefreshOverview()
        {
            oldroomname = "";
            if (mConnection.cnMySQL.State == ConnectionState.Closed)
                mConnection.cnMySQL.Open();

            string colquery = "SELECT NumeroPersone FROM colazioni";
            if (mConnection.cnMySQL.State == ConnectionState.Open)
            {
                // Panoramica.Rows.Clear();
                //Create Command
                MySqlCommand colcmd = new MySqlCommand(colquery, mConnection.cnMySQL);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = colcmd.ExecuteReader();
                int colcounter = 0;
                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    colcounter += dataReader.GetInt32(0);
                }
                //close Data Reader
                dataReader.Close();

                colcmd.Dispose();
                label13.Text = "Colazioni:" + colcounter;
            }
            mConnection.cnMySQL.Close();


            if (mConnection.cnMySQL.State == ConnectionState.Closed)
                mConnection.cnMySQL.Open();

            string query = "SELECT * FROM camere LEFT JOIN prenotazioni ON camere.Name=prenotazioni.camera LEFT JOIN fontiprenotazione ON da=fontiprenotazione.nome ORDER BY Name;";
            //  "SELECT * FROM camere LEFT JOIN prenotazioni ON camere.Name=prenotazioni.camera ORDER BY Name ;";// WHERE cast((prenotazioni.arrivo)as datetime) >=cast((" + DateTime.Now.ToShortDateString() + ") as datetime) AND cast((prenotazioni.partenza)as datetime) <=cast((" + DateTime.Now.ToShortDateString() + ") as datetime); ";



            //Open cnMySQL
            if (mConnection.cnMySQL.State == ConnectionState.Open)
            {
                // Panoramica.Rows.Clear();
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, mConnection.cnMySQL);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();
                int counter = 0;
                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    roomname = dataReader.GetString(0);
                    if (oldroomname == "")
                        oldroomname = roomname;
                    if (roomname == oldroomname)
                    {
                        string arrivo = dataReader.GetValue(10).ToString();
                        string partenza = dataReader.GetValue(11).ToString();
                        DateTime arr, par;

                        arr = DateTime.Parse(arrivo);
                        par = DateTime.Parse(partenza);
                        if (!isbusy)
                        {
                            //  DataGridViewPanoramica.Rows[counter] Panoramica.Rows[counter] = (DataGridViewPanoramica.Rows[counter])Panoramica.Panoramica.Rows[counter]s[0].Clone();
                            Panoramica.Rows[counter].Cells[0].Value = dataReader.GetString(0);


                            if (arrivo != "" && partenza != "")
                            {




                                if (checkBox1.Checked)
                                {
                                    if (new DateTime(dateTimePicker1.Value.Year, dateTimePicker1.Value.Month, dateTimePicker1.Value.Day, 23, 59, 59) >= arr)
                                    {

                                        if (dataReader.GetInt16(24) == 1 && dataReader.GetInt16(25) == 0)
                                        {
                                            Panoramica.Rows[counter].Cells[1].Value = "Occupata";

                                            colorme(Panoramica.Rows[counter], dataReader.GetInt16(29), dataReader.GetInt16(30), dataReader.GetInt16(31));

                                            //  Panoramica.Panoramica.Rows[counter]s.Add(Panoramica.Rows[counter]);
                                            Panoramica.Rows[counter].Cells[3].Value = arrivo.Substring(0, 10);
                                            Panoramica.Rows[counter].Cells[4].Value = partenza.Substring(0, 10);
                                            Panoramica.Rows[counter].Cells[5].Value = dataReader.GetString(12);
                                            Panoramica.Rows[counter].Cells[7].Value = dataReader.GetBoolean(13) ? "Forfait" : "no";
                                            Panoramica.Rows[counter].Cells[8].Value = dataReader.GetString(22);
                                            Panoramica.Rows[counter].Cells[9].Value = dataReader.GetString(16);
                                            Panoramica.Rows[counter].Cells[10].Value = dataReader.GetString(9);
                                            Panoramica.Rows[counter].Cells[11].Value = dataReader.GetString(2);
                                            Panoramica.Rows[counter].Cells[12].Value = dataReader.GetBoolean(3) ? "Si" : "No";
                                            Panoramica.Rows[counter].Cells[13].Value = dataReader.GetBoolean(4) ? "Si" : "No";
                                            Panoramica.Rows[counter].Cells[14].Value = dataReader.GetString(27);
                                            Panoramica.Rows[counter].Cells[15].Value = dataReader.GetString(7);

                                            Panoramica.Rows[counter].Cells[16].Value = dataReader.GetBoolean(6) ? "Vero":"Falso";
                                            Panoramica.Rows[counter].Cells[2].Value = dataReader.GetValue(15).ToString() + " " + dataReader.GetValue(14).ToString();
                                            Panoramica.Rows[counter].Cells[6].Value = dataReader.GetBoolean(23) ? "Pagata" : "no";

                                            isbusy = true;
                                        }
                                    }
                                }
                                else
                                {
                                    #region regionuncheck

                                    if (new DateTime(dateTimePicker1.Value.Year, dateTimePicker1.Value.Month, dateTimePicker1.Value.Day, 23, 59, 59) >= arr)
                                    {
                                        //if (dataReader.GetInt16(24) == 1)
                                        //{
                                        //    if (dataReader.GetInt16(25) == 0)
                                        //    {
                                        //        Panoramica.Rows[counter].Cells[1].Value = "Occupata";


                                        //        //  Panoramica.Panoramica.Rows[counter]s.Add(Panoramica.Rows[counter]);
                                        //        Panoramica.Rows[counter].Cells[3].Value = arrivo.Substring(0, 10);
                                        //        Panoramica.Rows[counter].Cells[4].Value = partenza.Substring(0, 10);
                                        //        Panoramica.Rows[counter].Cells[5].Value = dataReader.GetString(12);
                                        //        Panoramica.Rows[counter].Cells[7].Value = dataReader.GetString(13);
                                        //        Panoramica.Rows[counter].Cells[8].Value = dataReader.GetString(22);
                                        //        Panoramica.Rows[counter].Cells[9].Value = dataReader.GetString(16);
                                        //        Panoramica.Rows[counter].Cells[10].Value = dataReader.GetString(9);
                                        //        Panoramica.Rows[counter].Cells[11].Value = dataReader.GetString(2);
                                        //        Panoramica.Rows[counter].Cells[12].Value = dataReader.GetString(3);
                                        //        Panoramica.Rows[counter].Cells[13].Value = dataReader.GetString(4);
                                        //        Panoramica.Rows[counter].Cells[14].Value = dataReader.GetString(27);
                                        //        Panoramica.Rows[counter].Cells[15].Value = dataReader.GetString(7);

                                        //        Panoramica.Rows[counter].Cells[16].Value = dataReader.GetBoolean(6);
                                        //        Panoramica.Rows[counter].Cells[2].Value = dataReader.GetValue(15).ToString() + " " + dataReader.GetValue(14).ToString();
                                        //        Panoramica.Rows[counter].Cells[6].Value = dataReader.GetBoolean(23);
                                        //        isbusy = true;
                                        //        colorme(Panoramica.Rows[counter], dataReader.GetInt16(29), dataReader.GetInt16(30), dataReader.GetInt16(31));

                                        //        //  Panoramica.Panoramica.Rows[counter]s.Add(Panoramica.Rows[counter]);
                                        //    }
                                        //}
                                        //else
                                        //{
                                        if (new DateTime(dateTimePicker1.Value.Year, dateTimePicker1.Value.Month, dateTimePicker1.Value.Day, 23, 59, 59) <= par)
                                        {

                                            if (dataReader.GetInt16(24) == 0)
                                            {
                                                Panoramica.Rows[counter].Cells[1].Value = "Checkin da Effettuare";
                                            }
                                            else
                                            {
                                                Panoramica.Rows[counter].Cells[1].Value = "Occupata";
                                            }
                                            // if (!dataReader.GetBoolean(6))                                       
                                            // inarrivo++;



                                            //  Panoramica.Panoramica.Rows[counter]s.Add(Panoramica.Rows[counter]);
                                            Panoramica.Rows[counter].Cells[3].Value = arrivo.Substring(0, 10);
                                            Panoramica.Rows[counter].Cells[4].Value = partenza.Substring(0, 10);
                                            Panoramica.Rows[counter].Cells[5].Value = dataReader.GetString(12);
                                            Panoramica.Rows[counter].Cells[7].Value = dataReader.GetBoolean(13) ? "Forfait" : "no";
                                            Panoramica.Rows[counter].Cells[8].Value = dataReader.GetString(22);
                                            Panoramica.Rows[counter].Cells[9].Value = dataReader.GetString(16);
                                            Panoramica.Rows[counter].Cells[10].Value = dataReader.GetString(9);
                                            Panoramica.Rows[counter].Cells[11].Value = dataReader.GetString(2);
                                            Panoramica.Rows[counter].Cells[12].Value = dataReader.GetBoolean(3) ? "Si" : "No";
                                            Panoramica.Rows[counter].Cells[13].Value = dataReader.GetBoolean(4) ? "Si" : "No";
                                            Panoramica.Rows[counter].Cells[14].Value = dataReader.GetString(27);
                                            Panoramica.Rows[counter].Cells[15].Value = dataReader.GetString(7);

                                            Panoramica.Rows[counter].Cells[16].Value = dataReader.GetBoolean(6) ? "Vero" : "Falso";
                                            Panoramica.Rows[counter].Cells[2].Value = dataReader.GetValue(15).ToString() + " " + dataReader.GetValue(14).ToString();
                                            Panoramica.Rows[counter].Cells[6].Value = dataReader.GetBoolean(23) ? "Pagata" : "no";
                                            isbusy = true;
                                            colorme(Panoramica.Rows[counter], dataReader.GetInt16(29), dataReader.GetInt16(30), dataReader.GetInt16(31));
                                            // }
                                            //  Panoramica.Panoramica.Rows[counter]s.Add(Panoramica.Rows[counter]);


                                            //}
                                        }
                                      
                                        //}
                                    }
                                    #endregion
                                }

                            }
                        }
                        else
                        {
                            if (!checkBox1.Checked)
                            {
                                #region regionuncheck3
                                // Panoramica.Rows[counter].Cells[0].Value = dataReader.GetString(0);

                                //if (new DateTime(dateTimePicker1.Value.Year, dateTimePicker1.Value.Month, dateTimePicker1.Value.Day, 23, 59, 59) >= arr)
                                //{
                                //    if (dataReader.GetInt16(24) == 1)
                                //    {
                                //        if (dataReader.GetInt16(25) == 0)
                                //        {
                                //            Panoramica.Rows[counter].Cells[1].Value = "Occupata";


                                //            //  Panoramica.Panoramica.Rows[counter]s.Add(Panoramica.Rows[counter]);
                                //            Panoramica.Rows[counter].Cells[3].Value = arrivo.Substring(0, 10);
                                //            Panoramica.Rows[counter].Cells[4].Value = partenza.Substring(0, 10);
                                //            Panoramica.Rows[counter].Cells[5].Value = dataReader.GetString(12);
                                //            Panoramica.Rows[counter].Cells[7].Value = dataReader.GetString(13);
                                //            Panoramica.Rows[counter].Cells[8].Value = dataReader.GetString(22);
                                //            Panoramica.Rows[counter].Cells[9].Value = dataReader.GetString(16);
                                //            Panoramica.Rows[counter].Cells[10].Value = dataReader.GetString(9);
                                //            Panoramica.Rows[counter].Cells[11].Value = dataReader.GetString(2);
                                //            Panoramica.Rows[counter].Cells[12].Value = dataReader.GetString(3);
                                //            Panoramica.Rows[counter].Cells[13].Value = dataReader.GetString(4);
                                //            Panoramica.Rows[counter].Cells[14].Value = dataReader.GetString(27);
                                //            Panoramica.Rows[counter].Cells[15].Value = dataReader.GetString(7);

                                //            Panoramica.Rows[counter].Cells[16].Value = dataReader.GetBoolean(6);
                                //            Panoramica.Rows[counter].Cells[2].Value = dataReader.GetValue(15).ToString() + " " + dataReader.GetValue(14).ToString();
                                //            Panoramica.Rows[counter].Cells[6].Value = dataReader.GetBoolean(23);
                                //            isbusy = true;
                                //            colorme(Panoramica.Rows[counter], dataReader.GetInt16(29), dataReader.GetInt16(30), dataReader.GetInt16(31));

                                //            //  Panoramica.Panoramica.Rows[counter]s.Add(Panoramica.Rows[counter]);
                                //        }
                                //    }

                                //}
                                #endregion
                            }
                        }


                    }
                    else
                    {
                        isbusy = false;
                        counter++;

                        //  DataGridViewPanoramica.Rows[counter] Panoramica.Rows[counter] = (DataGridViewPanoramica.Rows[counter])Panoramica.Panoramica.Rows[counter]s[0].Clone();
                        Panoramica.Rows[counter].Cells[0].Value = dataReader.GetString(0);
                        //Panoramica.Rows[counter].Cells[2].Value = dataReader.GetValue(15).ToString() + " " + dataReader.GetValue(14).ToString();
                        string arrivo = dataReader.GetValue(10).ToString();
                        string partenza = dataReader.GetValue(11).ToString();
                        if (arrivo != "" && partenza != "")
                        {

                            DateTime arr, par;

                            arr = DateTime.Parse(arrivo);
                            par = DateTime.Parse(partenza);



                            if (checkBox1.Checked)
                            {
                                if (new DateTime(dateTimePicker1.Value.Year, dateTimePicker1.Value.Month, dateTimePicker1.Value.Day, 23, 59, 59) >= arr)
                                {

                                    if (dataReader.GetInt16(24) == 1 && dataReader.GetInt16(25) == 0)
                                    {
                                        Panoramica.Rows[counter].Cells[1].Value = "Occupata";

                                        //  Panoramica.Panoramica.Rows[counter]s.Add(Panoramica.Rows[counter]);
                                        Panoramica.Rows[counter].Cells[3].Value = arrivo.Substring(0, 10);
                                        Panoramica.Rows[counter].Cells[4].Value = partenza.Substring(0, 10);
                                        Panoramica.Rows[counter].Cells[5].Value = dataReader.GetString(12);
                                        Panoramica.Rows[counter].Cells[7].Value = dataReader.GetBoolean(13) ? "Forfait" : "no";
                                        Panoramica.Rows[counter].Cells[8].Value = dataReader.GetString(22);
                                        Panoramica.Rows[counter].Cells[9].Value = dataReader.GetString(16);
                                        Panoramica.Rows[counter].Cells[10].Value = dataReader.GetString(9);
                                        Panoramica.Rows[counter].Cells[11].Value = dataReader.GetString(2);
                                        Panoramica.Rows[counter].Cells[12].Value = dataReader.GetBoolean(3) ? "Si" : "No";
                                        Panoramica.Rows[counter].Cells[13].Value = dataReader.GetBoolean(4) ? "Si" : "No";
                                        Panoramica.Rows[counter].Cells[14].Value = dataReader.GetString(27);
                                        Panoramica.Rows[counter].Cells[15].Value = dataReader.GetString(7);

                                        Panoramica.Rows[counter].Cells[16].Value = dataReader.GetBoolean(6) ? "Vero" : "Falso";
                                        Panoramica.Rows[counter].Cells[2].Value = dataReader.GetValue(15).ToString() + " " + dataReader.GetValue(14).ToString();
                                        Panoramica.Rows[counter].Cells[6].Value = dataReader.GetBoolean(23) ? "Pagata" : "no";
                                        colorme(Panoramica.Rows[counter], dataReader.GetInt16(29), dataReader.GetInt16(30), dataReader.GetInt16(31));

                                        //  Panoramica.Panoramica.Rows[counter]s.Add(Panoramica.Rows[counter]);

                                        isbusy = true;
                                    }
                                }
                            }
                            else
                            {
                                #region regionuncheck2
                                if (new DateTime(dateTimePicker1.Value.Year, dateTimePicker1.Value.Month, dateTimePicker1.Value.Day, 23, 59, 59) >= arr)
                                {
                                    //if (dataReader.GetInt16(24) == 1)
                                    //{
                                    //    if (dataReader.GetInt16(25) == 0)
                                    //    {
                                    //        Panoramica.Rows[counter].Cells[1].Value = "Occupata";


                                    //        //  Panoramica.Panoramica.Rows[counter]s.Add(Panoramica.Rows[counter]);
                                    //        Panoramica.Rows[counter].Cells[3].Value = arrivo.Substring(0, 10);
                                    //        Panoramica.Rows[counter].Cells[4].Value = partenza.Substring(0, 10);
                                    //        Panoramica.Rows[counter].Cells[5].Value = dataReader.GetString(12);
                                    //        Panoramica.Rows[counter].Cells[7].Value = dataReader.GetString(13);
                                    //        Panoramica.Rows[counter].Cells[8].Value = dataReader.GetString(22);
                                    //        Panoramica.Rows[counter].Cells[9].Value = dataReader.GetString(16);
                                    //        Panoramica.Rows[counter].Cells[10].Value = dataReader.GetString(9);
                                    //        Panoramica.Rows[counter].Cells[11].Value = dataReader.GetString(2);
                                    //        Panoramica.Rows[counter].Cells[12].Value = dataReader.GetString(3);
                                    //        Panoramica.Rows[counter].Cells[13].Value = dataReader.GetString(4);
                                    //        Panoramica.Rows[counter].Cells[14].Value = dataReader.GetString(27);
                                    //        Panoramica.Rows[counter].Cells[15].Value = dataReader.GetString(7);

                                    //        Panoramica.Rows[counter].Cells[16].Value = dataReader.GetBoolean(6);
                                    //        Panoramica.Rows[counter].Cells[2].Value = dataReader.GetValue(15).ToString() + " " + dataReader.GetValue(14).ToString();
                                    //        Panoramica.Rows[counter].Cells[6].Value = dataReader.GetBoolean(23);
                                    //        isbusy = true;
                                    //        colorme(Panoramica.Rows[counter], dataReader.GetInt16(29), dataReader.GetInt16(30), dataReader.GetInt16(31));

                                    //        //  Panoramica.Panoramica.Rows[counter]s.Add(Panoramica.Rows[counter]);
                                    //    }
                                    //}
                                    //else
                                    //{
                                    if (new DateTime(dateTimePicker1.Value.Year, dateTimePicker1.Value.Month, dateTimePicker1.Value.Day, 23, 59, 59) <= par)
                                    {

                                        if (dataReader.GetInt16(24) == 0)

                                            Panoramica.Rows[counter].Cells[1].Value = "Checkin da Effettuare";
                                        else
                                            Panoramica.Rows[counter].Cells[1].Value = "Occupata";
                                        // if (!dataReader.GetBoolean(6))                                       
                                        // inarrivo++;



                                        //  Panoramica.Panoramica.Rows[counter]s.Add(Panoramica.Rows[counter]);
                                        Panoramica.Rows[counter].Cells[3].Value = arrivo.Substring(0, 10);
                                        Panoramica.Rows[counter].Cells[4].Value = partenza.Substring(0, 10);
                                        Panoramica.Rows[counter].Cells[5].Value = dataReader.GetString(12);
                                        Panoramica.Rows[counter].Cells[7].Value = dataReader.GetString(13);
                                        Panoramica.Rows[counter].Cells[8].Value = dataReader.GetString(22);
                                        Panoramica.Rows[counter].Cells[9].Value = dataReader.GetString(16);
                                        Panoramica.Rows[counter].Cells[10].Value = dataReader.GetString(9);
                                        Panoramica.Rows[counter].Cells[11].Value = dataReader.GetString(2);
                                        Panoramica.Rows[counter].Cells[12].Value = dataReader.GetBoolean(3)?"Si":"No";
                                        Panoramica.Rows[counter].Cells[13].Value = dataReader.GetBoolean(4) ? "Si" : "No";
                                        Panoramica.Rows[counter].Cells[14].Value = dataReader.GetString(27);
                                        Panoramica.Rows[counter].Cells[15].Value = dataReader.GetString(7);

                                        Panoramica.Rows[counter].Cells[16].Value = dataReader.GetBoolean(6) ? "Vero" : "Falso";
                                        Panoramica.Rows[counter].Cells[2].Value = dataReader.GetValue(15).ToString() + " " + dataReader.GetValue(14).ToString();
                                        Panoramica.Rows[counter].Cells[6].Value = dataReader.GetBoolean(23) ? "Pagata" : "no";
                                        isbusy = true;
                                        colorme(Panoramica.Rows[counter], dataReader.GetInt16(29), dataReader.GetInt16(30), dataReader.GetInt16(31));
                                        // }
                                        //  Panoramica.Panoramica.Rows[counter]s.Add(Panoramica.Rows[counter]);


                                        // }
                                    }
                                    // }
                                }
                                #endregion
                            }

                        }
                        if (!isbusy)
                        {
                            if (checkBox1.Checked)
                            {
                                foreach (DataGridViewCell c in Panoramica.Rows[counter].Cells)
                                    c.Value = "";

                                Panoramica.Rows[counter].Cells[0].Value = roomname;
                                Panoramica.Rows[counter].Cells[1].Value = "Libera";
                                Panoramica.Rows[counter].Cells[16].Value = dataReader.GetBoolean(6) ? "Vero" : "Falso";
                                Panoramica.Rows[counter].Cells[11].Value = dataReader.GetString(2);
                                Panoramica.Rows[counter].Cells[12].Value = dataReader.GetBoolean(3) ? "Si" : "No";

                                Panoramica.Rows[counter].Cells[13].Value = dataReader.GetBoolean(4) ? "Si" : "No";
                                //Panoramica.Rows[counter].Cells[2] = "";
                                //if (!dataReader.GetBoolean(6))
                                //   camerelibere++;
                                //  Panoramica.Rows[counter]s.Add(Panoramica.Rows[counter]);
                                colorme(Panoramica.Rows[counter], 255, 255, 255);
                            }
                            else
                            {
                                foreach (DataGridViewCell c in Panoramica.Rows[counter].Cells)
                                    c.Value = "";

                                Panoramica.Rows[counter].Cells[0].Value = roomname;
                                Panoramica.Rows[counter].Cells[1].Value = "Libera";
                                Panoramica.Rows[counter].Cells[11].Value = dataReader.GetString(2);
                                Panoramica.Rows[counter].Cells[15].Value = dataReader.GetBoolean(6) ? "Vero" : "Falso";
                                Panoramica.Rows[counter].Cells[12].Value = dataReader.GetBoolean(3) ? "Si" : "No";

                                Panoramica.Rows[counter].Cells[13].Value = dataReader.GetBoolean(4) ? "Si" : "No";
                                //Panoramica.Rows[counter].Cells[2] = "";
                                //if (!dataReader.GetBoolean(6))
                                //    camerelibere++;
                                //  Panoramica.Rows[counter]s.Add(Panoramica.Rows[counter]);
                                colorme(Panoramica.Rows[counter], 255, 255, 255);
                            }
                        }


                    }

                    if (!isbusy)
                    {
                        if (checkBox1.Checked)
                        {
                            foreach (DataGridViewCell c in Panoramica.Rows[counter].Cells)
                                c.Value = "";

                            Panoramica.Rows[counter].Cells[0].Value = roomname;
                            Panoramica.Rows[counter].Cells[1].Value = "Libera";
                            Panoramica.Rows[counter].Cells[16].Value = dataReader.GetBoolean(6) ? "Vero" : "Falso";
                            Panoramica.Rows[counter].Cells[11].Value = dataReader.GetString(2);
                            Panoramica.Rows[counter].Cells[12].Value = dataReader.GetBoolean(3) ? "Si" : "No";

                            Panoramica.Rows[counter].Cells[13].Value = dataReader.GetBoolean(4) ? "Si" : "No";
                            //Panoramica.Rows[counter].Cells[2] = "";
                            //if (!dataReader.GetBoolean(6))
                            //   camerelibere++;
                            //  Panoramica.Rows[counter]s.Add(Panoramica.Rows[counter]);
                            colorme(Panoramica.Rows[counter], 255, 255, 255);
                        }
                        else
                        {
                            foreach (DataGridViewCell c in Panoramica.Rows[counter].Cells)
                                c.Value = "";

                            Panoramica.Rows[counter].Cells[0].Value = roomname;
                            Panoramica.Rows[counter].Cells[1].Value = "Libera";
                            Panoramica.Rows[counter].Cells[11].Value = dataReader.GetString(2);

                            Panoramica.Rows[counter].Cells[16].Value = dataReader.GetBoolean(6) ? "Vero" : "Falso";
                            Panoramica.Rows[counter].Cells[12].Value = dataReader.GetBoolean(3) ? "Si" : "No";

                            Panoramica.Rows[counter].Cells[13].Value = dataReader.GetBoolean(4) ? "Si" : "No";
                            //Panoramica.Rows[counter].Cells[2] = "";
                            //if (!dataReader.GetBoolean(6))
                            //    camerelibere++;
                            //  Panoramica.Rows[counter]s.Add(Panoramica.Rows[counter]);
                            colorme(Panoramica.Rows[counter], 255, 255, 255);
                        }
                    }

                    oldroomname = roomname;

                }

                if (!isbusy)
                {
                    if (checkBox1.Checked)
                    {
                        foreach (DataGridViewCell c in Panoramica.Rows[counter].Cells)
                            Panoramica.Rows[counter].Cells[c.ColumnIndex].Value = "";

                        Panoramica.Rows[counter].Cells[0].Value = roomname;
                        Panoramica.Rows[counter].Cells[1].Value = "Libera";
                        Panoramica.Rows[counter].Cells[11].Value = dataReader.GetString(2);
                        Panoramica.Rows[counter].Cells[16].Value = dataReader.GetBoolean(6) ? "Vero" : "Falso";
                        Panoramica.Rows[counter].Cells[12].Value = dataReader.GetBoolean(3) ? "Si" : "No";

                        Panoramica.Rows[counter].Cells[13].Value = dataReader.GetBoolean(4) ? "Si" : "No";
                        colorme(Panoramica.Rows[counter], 255, 255, 255);
                        //  Panoramica.Rows[counter]s.Add(Panoramica.Rows[counter]);
                    }
                    else
                    {
                        foreach (DataGridViewCell c in Panoramica.Rows[counter].Cells)
                            Panoramica.Rows[counter].Cells[c.ColumnIndex].Value = "";

                        Panoramica.Rows[counter].Cells[0].Value = roomname;
                        Panoramica.Rows[counter].Cells[1].Value = "Libera";
                        Panoramica.Rows[counter].Cells[11].Value = dataReader.GetString(2);

                        Panoramica.Rows[counter].Cells[16].Value = dataReader.GetBoolean(6) ? "Vero" : "Falso";
                        Panoramica.Rows[counter].Cells[12].Value = dataReader.GetBoolean(3) ? "Si" : "No";

                        Panoramica.Rows[counter].Cells[13].Value = dataReader.GetBoolean(4) ? "Si" : "No";
                        //Panoramica.Rows[counter].Cells[2] = "";

                        //  Panoramica.Rows[counter]s.Add(Panoramica.Rows[counter]);
                        colorme(Panoramica.Rows[counter], 255, 255, 255);
                    }
                }
                else
                {
                    isbusy = false;
                }


                //close Data Reader
                dataReader.Close();

                //close Connection
                mConnection.cnMySQL.Close();
                camerelibere = 0;
                inarrivo = 0;
                label1.Text = "in arrivo:";
                label2.Text = "Camere Libere:";
                foreach (DataGridViewRow r in Panoramica.Rows)
                {
                    if (r.Cells[16].Value != null)
                        if (r.Cells[16].Value.ToString() == "Falso")
                        {
                            if (r.Cells[1].Value.ToString() == "Libera")
                            {
                                camerelibere++;
                            }
                            if (r.Cells[1].Value.ToString() == "Checkin da Effettuare")
                            {
                                inarrivo++;
                            }
                        }
                }
                label1.Text += inarrivo.ToString();
                label2.Text += camerelibere.ToString();


            }


            mConnection.cnMySQL.Close();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void Panoramica_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (mConnection.cnMySQL.State == ConnectionState.Closed)
            {
                updateall();
            }
        }

        private Timer timer1;
        public void InitTimer()
        {
            timer1 = new Timer();
            timer1.Tick += new EventHandler(timer1_Tick);
            timer1.Interval = 10000; // in miliseconds
            timer1.Start();
        }

        private Timer timer2;
        public void InitTimer2()
        {
            timer1 = new Timer();
            timer1.Tick += new EventHandler(everyminute_Tick);
            timer1.Interval = 60000; // in miliseconds
            timer1.Start();
        }
        private void everyminute_Tick(object sender, EventArgs e)
        {
            if (DateTime.Now.Hour == 0 && DateTime.Now.Minute == 0)
            {
                MessageBox.Show("il tableau sta per essere ricalcolato attendi 2 minuti per riprendere a lavorare", "Attenzione!!!",
    MessageBoxButtons.OK, MessageBoxIcon.Warning);
         
                tableau_grid.Rows.Clear();
                tableau_grid.Columns.Clear();
                createtableau();
                calctableau();
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (mConnection.cnMySQL.State == ConnectionState.Closed)
            {
                MySqlCommand cmd = new MySqlCommand(
                           "SELECT * FROM Beacon;", mConnection.cnMySQL);
                if (mConnection.cnMySQL.State != ConnectionState.Closed)
                    mConnection.cnMySQL.Close();
                if (mConnection.cnMySQL.State != ConnectionState.Open)
                    mConnection.cnMySQL.Open();
                MySqlDataReader dataReader = cmd.ExecuteReader();

                // print the CustomerID of each record
                bool update = false;
                while (dataReader.Read())
                {
                    if (dataReader.GetBoolean(0) && dataReader.GetString(1) != myip)
                    {
                        update = dataReader.GetBoolean(0);

                    }
                }

                if (mConnection.cnMySQL.State != ConnectionState.Closed)
                    mConnection.cnMySQL.Close();
                if (update)
                {

                    if (mConnection.cnMySQL.State != ConnectionState.Open)
                        mConnection.cnMySQL.Open();
                    cmd = new MySqlCommand("UPDATE  Beacon SET isdbmodified = False;", mConnection.cnMySQL);
                    cmd.ExecuteNonQuery();

                    mConnection.cnMySQL.Close();

                    updateall();
                    update = false;
                }

                //InitTimer();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            CognomeP.Text = "";
            NomeP.Text = "";
            data.Text = "";
            datarr.Text = "";
            if (mConnection.cnMySQL.State == ConnectionState.Closed)
            {
                updateall();
            }
        }

        public void checkinpanoramica()
        {
            if (Panoramica.SelectedRows.Count > 0)
            {
                if (MessageBox.Show("Sei sicuro di voler proseguire?", "Attenzione!!!", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {

                    if (Panoramica.SelectedRows.Count == 1)
                    {
                        Datepicker pick = new Datepicker(mConnection.cnMySQL, Convert.ToInt16(Panoramica.SelectedRows[0].Cells[15].Value), Panoramica.SelectedRows[0].Cells[0].Value.ToString(), this);
                        DateTime datetoset = DateTime.Now;
                        pick.Show();
                        //Call some methods to delete the item you want to remove.
                    }
                    else
                    {
                        List<CheckIn> cins = new List<CheckIn>();
                        for (int i = 0; i < Panoramica.SelectedRows.Count; i++)
                        {
                            if (Panoramica.SelectedRows[i].Cells[15].Value.ToString() != "")
                                cins.Add(new CheckIn(Convert.ToInt16(Panoramica.SelectedRows[i].Cells[15].Value), Panoramica.SelectedRows[i].Cells[0].Value.ToString()));
                        }
                        Datepicker pick = new Datepicker(mConnection.cnMySQL, cins, this);
                        DateTime datetoset = DateTime.Now;
                        pick.Show();
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (Panoramica.SelectedRows.Count > 0)
            {
                TassaDiSoggiorno tds;
                if (MessageBox.Show("Inserire tassa di soggiorno?", "Attenzione!!!", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    if (Panoramica.SelectedRows.Count == 1)
                    {
                        tds = new TassaDiSoggiorno(mConnection, Panoramica.SelectedRows[0].Cells[15].Value.ToString(), Panoramica.SelectedRows[0].Cells[0].Value.ToString(), Panoramica.SelectedRows[0].Cells[3].Value.ToString(), Panoramica.SelectedRows[0].Cells[4].Value.ToString(), this, true);
                        tds.Show();
                        tds.Focus();

                    }
                }
                else
                {
                    checkinpanoramica();
                }
            }    
               
        }



        private void button4_Click(object sender, EventArgs e)
        {
            if (Panoramica.SelectedRows.Count > 0)
            {
                if (MessageBox.Show("Sei sicuro di voler proseguire?", "Attenzione!!!", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {

                    for (int i = 0; i < Panoramica.SelectedRows.Count; i++)
                    {

                        if (Panoramica.SelectedRows[i].Cells[1].Value.ToString() == "Occupata")
                        {

                            if (mConnection.cnMySQL != null)
                            {
                                if (mConnection.cnMySQL.State != ConnectionState.Closed)
                                    mConnection.cnMySQL.Close();
                                if (mConnection.cnMySQL.State != ConnectionState.Open)
                                    mConnection.cnMySQL.Open();


                                MySqlCommand cmd = new MySqlCommand("UPDATE  camere SET pronta = false where Name='" + Panoramica.SelectedRows[i].Cells[0].Value.ToString() + "';", mConnection.cnMySQL);
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

                                mConnection.cnMySQL.Close();

                                if (mConnection.cnMySQL.State != ConnectionState.Closed)
                                    mConnection.cnMySQL.Close();
                                if (mConnection.cnMySQL.State != ConnectionState.Open)
                                    mConnection.cnMySQL.Open();

                                cmd = new MySqlCommand("UPDATE  prenotazioni SET checkout = '1',partenza='" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + "' where prenotazioni.id=" + Convert.ToInt16(Panoramica.SelectedRows[i].Cells[15].Value.ToString()) + ";", mConnection.cnMySQL);
                                cmd.ExecuteNonQuery();

                                mConnection.cnMySQL.Close();

                                mConnection.cnMySQL.Close();

                                if (mConnection.cnMySQL.State != ConnectionState.Closed)
                                    mConnection.cnMySQL.Close();
                                if (mConnection.cnMySQL.State != ConnectionState.Open)
                                    mConnection.cnMySQL.Open();

                                bool check = false;

                                mConnection.cmdMySQL = new MySqlCommand(
                             "SELECT * FROM colazioni WHERE NumeroPrenotazione=" + Convert.ToInt16(Panoramica.SelectedRows[i].Cells[15].Value.ToString()) + ";", mConnection.cnMySQL);
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
                                    mConnection.cmdMySQL.CommandText = "DELETE FROM colazioni where colazioni.NumeroPrenotazione=" + Convert.ToInt16(Panoramica.SelectedRows[i].Cells[15].Value.ToString()) + ";";
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
                }
                updateall();
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

            updateall();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Prenotazione p = new Prenotazione(mConnection.cnMySQL, mConnection, this);
            p.Show();
        }

        private void menuStrip1_ItemClicked_1(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (Panoramica.SelectedRows.Count == 1)
            {
                SpeseExtra spextra = new SpeseExtra(mConnection.cnMySQL, Panoramica.SelectedRows[0].Cells[15].Value.ToString(), Panoramica.SelectedRows.Count, Panoramica.SelectedRows[0].Cells[3].Value.ToString(), Panoramica.SelectedRows[0].Cells[4].Value.ToString(), Convert.ToSingle(Panoramica.SelectedRows[0].Cells[5].Value), Convert.ToBoolean(Panoramica.SelectedRows[0].Cells[7].Value.ToString()=="no"?false:true), Convert.ToSingle(Panoramica.SelectedRows[0].Cells[8].Value), Convert.ToInt16(Panoramica.SelectedRows[0].Cells[9].Value), Panoramica.SelectedRows[0].Cells[0].Value.ToString());
                spextra.Show();
            }
        }

        private void checkBox1_CheckedChanged_1(object sender, EventArgs e)
        {
            updateall();
        }
        Conto conto;

        private void button12_Click(object sender, EventArgs e)
        {
            if (Panoramica.SelectedRows.Count > 0)
            {
                if (Panoramica.SelectedRows[0].Cells[1].Value.ToString() == "Occupata")
                {
                    int numerostanze = Panoramica.SelectedRows.Count;
                    if (conto == null)
                    {
                        conto = new Conto(mConnection, Panoramica.SelectedRows[0].Cells[10].Value.ToString(), Panoramica.SelectedRows[0].Cells[15].Value.ToString(), Panoramica.SelectedRows.Count, Panoramica.SelectedRows[0].Cells[3].Value.ToString(), Panoramica.SelectedRows[0].Cells[4].Value.ToString(), Convert.ToSingle(Panoramica.SelectedRows[0].Cells[5].Value.ToString()), Convert.ToBoolean(Panoramica.SelectedRows[0].Cells[7].Value.ToString()=="no"?false:true), Convert.ToSingle(Panoramica.SelectedRows[0].Cells[8].Value.ToString()), Convert.ToInt16(Panoramica.SelectedRows[0].Cells[9].Value.ToString()), Panoramica.SelectedRows[0].Cells[0].Value.ToString());
                        conto.Show();
                    }
                    else if (conto.IsDisposed)
                    {
                        conto = new Conto(mConnection, Panoramica.SelectedRows[0].Cells[10].Value.ToString(), Panoramica.SelectedRows[0].Cells[15].Value.ToString(), Panoramica.SelectedRows.Count, Panoramica.SelectedRows[0].Cells[3].Value.ToString(), Panoramica.SelectedRows[0].Cells[4].Value.ToString(), Convert.ToSingle(Panoramica.SelectedRows[0].Cells[5].Value.ToString()), Convert.ToBoolean(Panoramica.SelectedRows[0].Cells[7].Value.ToString() == "no" ? false : true), Convert.ToSingle(Panoramica.SelectedRows[0].Cells[8].Value.ToString()), Convert.ToInt16(Panoramica.SelectedRows[0].Cells[9].Value.ToString()), Panoramica.SelectedRows[0].Cells[0].Value.ToString());
                        conto.Show();
                    }


                    conto.Focus();
                }
            }
        }
        Acconti acconti;
        private void button13_Click(object sender, EventArgs e)
        {
            if (Panoramica.SelectedRows.Count > 0)
            {
                if (Panoramica.SelectedRows[0].Cells[1].Value.ToString() == "Occupata")
                {
                    int numerostanze = Panoramica.SelectedRows.Count;

                    if (acconti == null)
                    {
                        acconti = new Acconti(mConnection, Panoramica.SelectedRows[0].Cells[15].Value.ToString(), Panoramica.SelectedRows[0].Cells[2].Value.ToString(), Panoramica.SelectedRows.Count, Panoramica.SelectedRows[0].Cells[3].Value.ToString(), Panoramica.SelectedRows[0].Cells[4].Value.ToString(), Convert.ToSingle(Panoramica.SelectedRows[0].Cells[5].Value.ToString()), Convert.ToBoolean(Panoramica.SelectedRows[0].Cells[7].Value.ToString()), Convert.ToSingle(Panoramica.SelectedRows[0].Cells[8].Value.ToString()), Convert.ToInt16(Panoramica.SelectedRows[0].Cells[9].Value.ToString()));
                        acconti.Show();
                    }
                    else if (acconti.IsDisposed)
                    {
                        acconti = new Acconti(mConnection, Panoramica.SelectedRows[0].Cells[15].Value.ToString(), Panoramica.SelectedRows[0].Cells[2].Value.ToString(), Panoramica.SelectedRows.Count, Panoramica.SelectedRows[0].Cells[3].Value.ToString(), Panoramica.SelectedRows[0].Cells[4].Value.ToString(), Convert.ToSingle(Panoramica.SelectedRows[0].Cells[5].Value.ToString()), Convert.ToBoolean(Panoramica.SelectedRows[0].Cells[7].Value.ToString()), Convert.ToSingle(Panoramica.SelectedRows[0].Cells[8].Value.ToString()), Convert.ToInt16(Panoramica.SelectedRows[0].Cells[9].Value.ToString()));
                        acconti.Show();
                    }

                }
            }
        }
        TipoCamera tc;
        private void button21_Click(object sender, EventArgs e)
        {
            if (Panoramica.SelectedRows.Count > 0)
            {
                List<string> cam = new List<string>();
                foreach (DataGridViewRow it in Panoramica.SelectedRows)
                {
                    cam.Add(it.Cells[0].Value.ToString());
                }
                if (tc == null)
                {
                    tc = new TipoCamera(mConnection, cam);
                    tc.Show();
                }
                else if (tc.IsDisposed)
                {
                    tc = new TipoCamera(mConnection, cam);
                    tc.Show();
                }


                tc.Focus();
            }
        }

        private void button20_Click(object sender, EventArgs e)
        {
            Document sitgiorn = new Document(PageSize.A4, 10, 10, 80, 10);

            // sitgiorn.SetPageSize(iTextSharp.text.PageSize.A4.Rotate());
            PdfWriter writer;
            string filepath = "./Situazione Giornaliera.pdf";
            writer = PdfWriter.GetInstance(sitgiorn, new FileStream(filepath, FileMode.Create));
            sitgiorn.Open();
            sitgiorn.AddTitle(dateTimePicker1.Value.ToShortDateString());
            Paragraph date = new Paragraph(dateTimePicker1.Value.ToShortDateString());
            PdfPTable table = new PdfPTable(4) { TotalWidth = 280 };
            //table.DefaultCell.Chunks[0].

            var colWidthPercentages = new[] { 15, 15, 40, 30 };
            table.SetWidths(colWidthPercentages);

            Paragraph p = new Paragraph("Data");
            PdfPCell cell = new PdfPCell(p);
            p.Font.Size = 10;
            table.AddCell(cell);
            // table.DefaultCell.Width = 50;
            p = new Paragraph(""); p.Font.Size = 10;
            table.AddCell(p);
            p = new Paragraph(dateTimePicker1.Value.ToShortDateString());
            p.Font.Size = 10;
            table.AddCell(p);
            p = new Paragraph("");
            p.Font.Size = 10;
            table.AddCell(p);
            p = new Paragraph("Camera");
            cell = new PdfPCell(p);
            p.Font.Size = 10;
            table.AddCell(cell);
            // table.DefaultCell.Width = 50;
            p = new Paragraph("Tipo"); p.Font.Size = 10;
            table.AddCell(p);
            p = new Paragraph("Da");
            p.Font.Size = 10;
            table.AddCell(p);
            p = new Paragraph("Note");
            p.Font.Size = 10;
            table.AddCell(p);
            //table.AddCell("Doccia");
            //table.AddCell("Materassi Separati");         

            int counter = 0;
            bool generatetohercolumns = false;
            string oldroom = Panoramica.Rows[0].Cells[0].Value.ToString();
            for (int i = 0; i < Panoramica.Rows.Count; i++)
            {

                if (Panoramica.Rows[i].Cells[0].Value != null)
                {
                    if (Panoramica.Rows[i].Cells[0].Value.ToString().Substring(0, 1) != oldroom.Substring(0, 1))
                    {
                        p = new Paragraph("   ");
                        p.Font.Size = 10;
                        PdfPCell c = new PdfPCell(p);
                        c.Border = PdfPCell.NO_BORDER;
                        table.AddCell(c);
                        p = new Paragraph("   ");
                        p.Font.Size = 10;
                        c = new PdfPCell(p);
                        c.Border = PdfPCell.NO_BORDER;
                        table.AddCell(c);
                        p = new Paragraph("  ");
                        p.Font.Size = 10;
                        c = new PdfPCell(p);
                        c.Border = PdfPCell.NO_BORDER;
                        table.AddCell(c);
                        p = new Paragraph("   ");
                        p.Font.Size = 10;
                        c = new PdfPCell(p);
                        c.Border = PdfPCell.NO_BORDER;
                        table.AddCell(c);
                        oldroom = Panoramica.Rows[i].Cells[0].Value.ToString();
                        i--;
                    }
                    else
                    {
                        p = new Paragraph(Panoramica.Rows[i].Cells[0].Value.ToString()); p.Font.Size = 10;
                        table.AddCell(p);
                        p = new Paragraph(Panoramica.Rows[i].Cells[11].Value.ToString()); p.Font.Size = 10;
                        table.AddCell(p);
                        p = new Paragraph(Panoramica.Rows[i].Cells[2].Value.ToString()); p.Font.Size = 10;
                        table.AddCell(p);
                        p = new Paragraph("");
                        p.Font.Size = 10;
                        table.AddCell(p);
                        oldroom = Panoramica.Rows[i].Cells[0].Value.ToString();
                        if (i == Panoramica.Rows.Count / 2)
                        {
                            table.WriteSelectedRows(0, -1, sitgiorn.Left, sitgiorn.Top, writer.DirectContent);  //footer
                            generatetohercolumns = true;
                            table = new PdfPTable(4) { TotalWidth = 280 };
                            //table.DefaultCell.Chunks[0].

                            colWidthPercentages = new[] { 15, 15, 40, 30 };
                            table.SetWidths(colWidthPercentages);

                            p = new Paragraph("  ");
                            cell = new PdfPCell(p);
                            p.Font.Size = 10;
                            table.AddCell(cell);
                            // table.DefaultCell.Width = 50;
                            p = new Paragraph("  "); p.Font.Size = 10;
                            table.AddCell(p);
                            p = new Paragraph("  ");
                            p.Font.Size = 10;
                            table.AddCell(p);
                            p = new Paragraph("  ");
                            p.Font.Size = 10;
                            table.AddCell(p);
                            p = new Paragraph("Camera");
                            cell = new PdfPCell(p);
                            p.Font.Size = 10;
                            table.AddCell(cell);
                            // table.DefaultCell.Width = 50;
                            p = new Paragraph("Tipo"); p.Font.Size = 10;
                            table.AddCell(p);
                            p = new Paragraph("Da");
                            p.Font.Size = 10;
                            table.AddCell(p);
                            p = new Paragraph("Note");
                            p.Font.Size = 10;
                            table.AddCell(p);
                        }
                    }

                }

            }
            if (generatetohercolumns)
                table.WriteSelectedRows(0, -1, sitgiorn.Left + 290, sitgiorn.Top, writer.DirectContent);  //footer

            sitgiorn.Close();
            string spath = System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "/Situazione Giornaliera.pdf";
            System.Diagnostics.Process.Start(spath);

        }
        FatturaoRicevuta fatoric;
        private void stampafat_Click(object sender, EventArgs e)
        {
            if (Panoramica.SelectedRows.Count > 0)
            {
                if (Panoramica.SelectedRows[0].Cells[1].Value.ToString() == "Occupata")
                {
                    int numerostanze = Panoramica.SelectedRows.Count;

                    if (fatoric == null)
                    {
                        fatoric = new FatturaoRicevuta(mConnection, Panoramica.SelectedRows[0].Cells[15].Value.ToString());//, Panoramica.SelectedRows[0].SubItems[11].Text, numerostanze, Panoramica.SelectedRows[0].SubItems[3].Text, Panoramica.SelectedRows[0].SubItems[4].Text, Convert.ToSingle(Panoramica.SelectedRows[0].SubItems[5].Text), Convert.ToBoolean(Panoramica.SelectedRows[0].SubItems[6].Text), Convert.ToSingle(Panoramica.SelectedRows[0].SubItems[9].Text), Convert.ToInt16(Panoramica.SelectedRows[0].SubItems[10].Text));
                        fatoric.Show();
                    }
                    else if (fatoric.IsDisposed)
                    {
                        fatoric = new FatturaoRicevuta(mConnection, Panoramica.SelectedRows[0].Cells[15].Value.ToString());//, Panoramica.SelectedRows[0].SubItems[11].Text, numerostanze, Panoramica.SelectedRows[0].SubItems[3].Text, Panoramica.SelectedRows[0].SubItems[4].Text, Convert.ToSingle(Panoramica.SelectedRows[0].SubItems[5].Text), Convert.ToBoolean(Panoramica.SelectedRows[0].SubItems[6].Text), Convert.ToSingle(Panoramica.SelectedRows[0].SubItems[9].Text), Convert.ToInt16(Panoramica.SelectedRows[0].SubItems[10].Text));
                        fatoric.Show();
                    }

                }
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            Prenotazioni.Rows.Clear();
            RefreshBook();
        }

        private void riferimentotext_TextChanged(object sender, EventArgs e)
        {
            Prenotazioni.Rows.Clear();
            RefreshBook();
        }

        private void NomeP_TextChanged(object sender, EventArgs e)
        {
            Prenotazioni.Rows.Clear();
            RefreshBook();
        }

        private void CognomeP_TextChanged(object sender, EventArgs e)
        {
            Prenotazioni.Rows.Clear();
            RefreshBook();
        }

        private void datarr_TextChanged(object sender, EventArgs e)
        {
            Prenotazioni.Rows.Clear();
            RefreshBook();
        }

        private void data_TextChanged(object sender, EventArgs e)
        {
            Prenotazioni.Rows.Clear();
            RefreshBook();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            CognomeP.Text = "";
            NomeP.Text = "";
            datarr.Text = DateTime.Now.ToShortDateString();
            checkBox2.Checked = false;
            data.Text = "";
            Prenotazioni.Rows.Clear();
            RefreshBook();
        }

        private void button25_Click(object sender, EventArgs e)
        {
            CognomeP.Text = "";
            NomeP.Text = "";
            data.Text = DateTime.Now.ToShortDateString();
            checkBox2.Checked = true;
            datarr.Text = "";
            Prenotazioni.Rows.Clear();
            RefreshBook();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Sei sicuro di voler proseguire?", "Attenzione!!!", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {

                foreach (DataGridViewRow I in Prenotazioni.SelectedRows)
                {
                    if (mConnection != null)
                    {
                        if (mConnection.cnMySQL.State != ConnectionState.Closed)
                            mConnection.cnMySQL.Close();
                        if (mConnection.cnMySQL.State != ConnectionState.Open)
                            mConnection.cnMySQL.Open();
                        mConnection.cmdMySQL = mConnection.cnMySQL.CreateCommand();

                        mConnection.cmdMySQL.CommandText = "DELETE FROM prenotazioni where prenotazioni.id=" + Convert.ToInt16(I.Cells[18].Value.ToString()) + " and prenotazioni.camera='" + I.Cells[0].Value.ToString() + "';";

                        mConnection.cmdMySQL.ExecuteNonQuery();
                        mConnection.cnMySQL.Close();

                        if (mConnection.cnMySQL.State != ConnectionState.Closed)
                            mConnection.cnMySQL.Close();
                        if (mConnection.cnMySQL.State != ConnectionState.Open)
                            mConnection.cnMySQL.Open();

                        bool check = false;

                        mConnection.cmdMySQL = new MySqlCommand(
                        "SELECT * FROM colazioni WHERE NumeroPrenotazione=" + Convert.ToInt16(I.Cells[18].Value.ToString()) + ";", mConnection.cnMySQL);
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
                            mConnection.cmdMySQL.CommandText = "DELETE FROM colazioni where colazioni.NumeroPrenotazione=" + Convert.ToInt16(I.Cells[18].Value.ToString()) + ";";
                            mConnection.cmdMySQL.ExecuteNonQuery();
                            mConnection.cnMySQL.Close();
                        }
                        else
                        {

                        }

                    }

                }
                RefreshBook();
                RefreshOverview();
                calctableau();
            }
        }

        public void GetCreateMyFolder()
        {
            var now = new DateTime(DateTime.Now.Year, 1, 1);
            var daysofyear=365;
                if(DateTime.IsLeapYear(now.Year))
                    daysofyear=366;
            for (int i = 0; i < daysofyear; i++)
            {
                var yearName = now.ToString("yyyy");
                var monthName = now.ToString("MMMM");
                var dayName = now.ToString("dd");

                var folder = Path.Combine("Anno",
                               Path.Combine(yearName,
                                 Path.Combine(monthName,
                                   dayName)));

                Directory.CreateDirectory(folder);
                now=now.AddDays(1);
            }
        }

        private void button24_Click(object sender, EventArgs e)
        {
            if (Prenotazioni.SelectedRows.Count > 0)
            {

                int numerostanze = Prenotazioni.SelectedRows.Count;

                if (acconti == null)
                {
                    acconti = new Acconti(mConnection, Prenotazioni.SelectedRows[0].Cells[18].Value.ToString(), Prenotazioni.SelectedRows[0].Cells[1].Value.ToString(), numerostanze, Prenotazioni.SelectedRows[0].Cells[3].Value.ToString(), Prenotazioni.SelectedRows[0].Cells[4].Value.ToString(), Convert.ToSingle(Prenotazioni.SelectedRows[0].Cells[5].Value.ToString()), Convert.ToBoolean(Prenotazioni.SelectedRows[0].Cells[6].Value.ToString() == "no" ? false : true), Convert.ToSingle(Prenotazioni.SelectedRows[0].Cells[15].Value.ToString()), Convert.ToInt16(Prenotazioni.SelectedRows[0].Cells[14].Value.ToString()));
                    acconti.Show();
                }
                else if (acconti.IsDisposed)
                {
                    acconti = new Acconti(mConnection, Prenotazioni.SelectedRows[0].Cells[18].Value.ToString(), Prenotazioni.SelectedRows[0].Cells[1].Value.ToString(), numerostanze, Prenotazioni.SelectedRows[0].Cells[3].Value.ToString(), Prenotazioni.SelectedRows[0].Cells[4].Value.ToString(), Convert.ToSingle(Prenotazioni.SelectedRows[0].Cells[5].Value.ToString()), Convert.ToBoolean(Prenotazioni.SelectedRows[0].Cells[6].Value.ToString() == "no" ? false : true), Convert.ToSingle(Prenotazioni.SelectedRows[0].Cells[15].Value.ToString()), Convert.ToInt16(Prenotazioni.SelectedRows[0].Cells[14].Value.ToString()));
                    acconti.Show();
                }

            }
        }

        public void checkinnew()
        {
            if (Prenotazioni.SelectedRows.Count > 0)
            {
             if (MessageBox.Show("Sei sicuro di voler proseguire?", "Attenzione!!!", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {

                    if (Prenotazioni.SelectedRows.Count == 1)
                    {
                        Datepicker pick = new Datepicker(mConnection.cnMySQL, Convert.ToInt16(Prenotazioni.SelectedRows[0].Cells[18].Value), Prenotazioni.SelectedRows[0].Cells[0].Value.ToString(), this);
                        DateTime datetoset = DateTime.Now;
                        pick.Show();
                        //Call some methods to delete the item you want to remove.
                    }
                    else
                    {
                        List<CheckIn> cins = new List<CheckIn>();
                        for (int i = 0; i < Prenotazioni.SelectedRows.Count; i++)
                        {
                            if (Prenotazioni.SelectedRows[i].Cells[18].Value != "")
                                cins.Add(new CheckIn(Convert.ToInt16(Prenotazioni.SelectedRows[i].Cells[18].Value), Prenotazioni.SelectedRows[i].Cells[0].Value.ToString()));
                        }
                        Datepicker pick = new Datepicker(mConnection.cnMySQL, cins, this);
                        DateTime datetoset = DateTime.Now;
                        pick.Show();
                    }
                }
            }
        }
        private void ChekinBTN_Click(object sender, EventArgs e)
        {
            if (Prenotazioni.SelectedRows.Count > 0)
            {
                TassaDiSoggiorno tds;
                if (MessageBox.Show("Inserire tassa di soggiorno?", "Attenzione!!!", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    if (Prenotazioni.SelectedRows.Count == 1)
                    {
                        tds = new TassaDiSoggiorno(mConnection, Prenotazioni.SelectedRows[0].Cells[18].Value.ToString(), Prenotazioni.SelectedRows[0].Cells[0].Value.ToString(), Prenotazioni.SelectedRows[0].Cells[3].Value.ToString(), Prenotazioni.SelectedRows[0].Cells[4].Value.ToString(), this,false);
                        tds.Show();
                        tds.Focus();

                    }
                }
                else
                {
                    checkinnew();
                }
               }    
               
        }

        private void CheckOutBTN_Click(object sender, EventArgs e)
        {
            if (Prenotazioni.SelectedRows.Count > 0)
            {
                if (MessageBox.Show("Sei sicuro di voler proseguire?", "Attenzione!!!", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {

                    for (int i = 0; i < Prenotazioni.SelectedRows.Count; i++)
                    {

                        if (Prenotazioni.SelectedRows[i].Cells[8].Value.ToString() == "Effettuato")
                        {

                            if (mConnection.cnMySQL != null)
                            {
                                if (mConnection.cnMySQL.State != ConnectionState.Closed)
                                    mConnection.cnMySQL.Close();
                                if (mConnection.cnMySQL.State != ConnectionState.Open)
                                    mConnection.cnMySQL.Open();


                                MySqlCommand cmd = new MySqlCommand("UPDATE  camere SET pronta = false where Name='" + Prenotazioni.SelectedRows[i].Cells[0].Value.ToString() + "';", mConnection.cnMySQL);
                                cmd.ExecuteNonQuery();

                                mConnection.cnMySQL.Close();

                                if (mConnection.cnMySQL.State != ConnectionState.Closed)
                                    mConnection.cnMySQL.Close();
                                if (mConnection.cnMySQL.State != ConnectionState.Open)
                                    mConnection.cnMySQL.Open();

                                cmd = new MySqlCommand("UPDATE  prenotazioni SET checkout = '1',partenza='" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + "' where prenotazioni.id=" + Convert.ToInt16(Prenotazioni.SelectedRows[i].Cells[18].Value.ToString()) + ";", mConnection.cnMySQL);
                                cmd.ExecuteNonQuery();

                                mConnection.cnMySQL.Close();

                                mConnection.cnMySQL.Close();
                           
                                if (mConnection.cnMySQL.State != ConnectionState.Closed)
                                    mConnection.cnMySQL.Close();
                                if (mConnection.cnMySQL.State != ConnectionState.Open)
                                    mConnection.cnMySQL.Open();

                                bool check = false;

                                mConnection.cmdMySQL = new MySqlCommand(
                             "SELECT * FROM colazioni WHERE NumeroPrenotazione=" + Convert.ToInt16(Prenotazioni.SelectedRows[i].Cells[18].Value.ToString()) + ";", mConnection.cnMySQL);
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
                                    mConnection.cmdMySQL.CommandText = "DELETE FROM colazioni where colazioni.NumeroPrenotazione=" + Convert.ToInt16(Prenotazioni.SelectedRows[i].Cells[18].Value.ToString()) + ";";
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
                }
                RefreshOverview();
                RefreshBook();
                calctableau();
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (Prenotazioni.SelectedRows.Count > 0 && Prenotazioni.SelectedRows.Count<=1)
            {
                if (mConnection != null)
                {
                    if (mConnection.cnMySQL.State != ConnectionState.Closed)
                        mConnection.cnMySQL.Close();

                    if (mConnection.cnMySQL.State != ConnectionState.Open)
                        mConnection.cnMySQL.Open();
                    mConnection.cmdMySQL = new MySqlCommand("select * from clienti where id=" + Prenotazioni.SelectedRows[0].Cells[19].Value.ToString() + " Order by cognome ASC", mConnection.cnMySQL);


                    mConnection.reader = mConnection.cmdMySQL.ExecuteReader();
                    // print the CustomerID of each record
                    while (mConnection.reader.Read())
                    {
                        string[] values = new string[6];
                        string arrivo = "", pag = "", partenza = "", riferimento = "", tariffa = "", iva = "", anticipo = "", tipoec = "", tipostanza = "", forfait = "", arrangiamento = "", note = "", operatore = "", da = "";
                        List<string> selectedroom = new List<string>();
                        foreach (DataGridViewRow it in Prenotazioni.SelectedRows)
                        {
                            selectedroom.Add(it.Cells[0].Value.ToString());
                            arrivo = it.Cells[3].Value.ToString();
                            partenza = it.Cells[4].Value.ToString();
                            riferimento = it.Cells[2].Value.ToString();
                            tariffa = it.Cells[5].Value.ToString();
                            iva = it.Cells[14].Value.ToString();
                            forfait = it.Cells[6].Value.ToString();
                            anticipo = it.Cells[15].Value.ToString();


                            tipostanza = it.Cells[11].Value.ToString();
                            tipoec = it.Cells[10].Value.ToString();
                            arrangiamento = it.Cells[12].Value.ToString();
                            note = it.Cells[13].Value.ToString();
                            operatore = it.Cells[16].Value.ToString();
                            da = it.Cells[17].Value.ToString();
                            pag = it.Cells[7].Value.ToString();
                        }

                        values[0] = mConnection.reader.GetValue(0).ToString();
                        values[1] = mConnection.reader.GetValue(1).ToString();
                        values[2] = mConnection.reader.GetValue(2).ToString();
                        values[3] = mConnection.reader.GetValue(3).ToString();
                        values[4] = mConnection.reader.GetValue(4).ToString();
                        values[5] = mConnection.reader.GetValue(5).ToString();
                        if (values[0] == Prenotazioni.SelectedRows[0].Cells[19].Value.ToString())
                        {
                            Prenotazione p = new Prenotazione(mConnection, this, Convert.ToInt16(Prenotazioni.SelectedRows[0].Cells[18].Value.ToString()), Convert.ToInt16(values[0]), values[1], values[2], values[5], values[4], values[3], selectedroom, arrivo, partenza, da, operatore, tipostanza, tipoec, arrangiamento, note, tariffa, anticipo, iva, forfait, riferimento, pag, note, selectedroom.Count);
                            p.Show();
                            p.Focus();
                            mConnection.cnMySQL.Close();
                            break;
                        }

                    }
                    mConnection.cnMySQL.Close();
                }
            }

        }


        public bool TableauCreated = false;

        private void button22_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow r in tableau_grid.Rows)
            {
                if (DateTime.Now.ToShortDateString() == r.HeaderCell.Value.ToString().Substring(0, 10))
                {
                    currenttablauindex = r.Index;
                    tableau_grid.FirstDisplayedScrollingRowIndex = currenttablauindex;
                }
            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewCell c in tableau_grid.SelectedCells)
            {
                if (mConnection != null)
                {
                    if (mConnection.cnMySQL.State != ConnectionState.Closed)
                        mConnection.cnMySQL.Close();
                    if (mConnection.cnMySQL.State != ConnectionState.Open)
                        mConnection.cnMySQL.Open();
                    mConnection.cmdMySQL = mConnection.cnMySQL.CreateCommand();
                    string x = c.OwningRow.HeaderCell.Value.ToString().Substring(0, 10);

                    mConnection.cmdMySQL.CommandText = "DELETE FROM cambiolenzuola WHERE camera='" + c.OwningColumn.Name.ToString() + "' AND data='" + x + "'";

                    mConnection.cmdMySQL.ExecuteNonQuery();
                    mConnection.cnMySQL.Close();
                    colorme("default", c.OwningRow.Index, c.OwningColumn.Index);

                }
            }
            calctableau();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewCell c in tableau_grid.SelectedCells)
            {
                if (mConnection != null)
                {
                    if (mConnection.cnMySQL.State != ConnectionState.Closed)
                        mConnection.cnMySQL.Close();
                    if (mConnection.cnMySQL.State != ConnectionState.Open)
                        mConnection.cnMySQL.Open();
                    mConnection.cmdMySQL = mConnection.cnMySQL.CreateCommand();

                    string x = c.OwningRow.HeaderCell.Value.ToString().Substring(0, 10);
                    // mConnection.cmdMySQL.CommandText = "insert into cambiolenzuola camera='" + c.OwningColumn.HeaderCell.Value.ToString().Substring(0,10) + "', data='" +x+"');";
                    mConnection.cmdMySQL.CommandText = "INSERT INTO cambiolenzuola (camera,data) VALUES('" + c.OwningColumn.Name.ToString() + "','" + x + "');";
                    mConnection.cmdMySQL.ExecuteNonQuery();
                    mConnection.cnMySQL.Close();


                }
            }
            calctableau();
        }

        int rowindextoreturn = 0;
        private void button6_Click(object sender, EventArgs e)
        {  
            if (tableau_grid.SelectedCells[0].Value != null)
            {

                rowindextoreturn = tableau_grid.SelectedCells[0].RowIndex;
                currenttablauindex = tableau_grid.SelectedCells[0].RowIndex;

                string nomeT = tableau_grid.SelectedCells[0].Value.ToString().Replace("'", "\\'");
                string cameraT = tableau_grid.Columns[tableau_grid.SelectedCells[0].ColumnIndex].Name;
                DateTime currentDate = DateTime.Parse(tableau_grid.Rows[tableau_grid.SelectedCells[0].RowIndex].HeaderCell.Value.ToString());

                if (mConnection != null)
                {
                    if (mConnection.cnMySQL.State != ConnectionState.Closed)
                        mConnection.cnMySQL.Close();

                    if (mConnection.cnMySQL.State != ConnectionState.Open)
                        mConnection.cnMySQL.Open();
                    mConnection.cmdMySQL = new MySqlCommand("SELECT prenotazioni.camera, prenotazioni.arrivo,prenotazioni.partenza,prenotazioni.nome,prenotazioni.cognome,prenotazioni.da FROM prenotazioni;", mConnection.cnMySQL);

                    mConnection.reader = mConnection.cmdMySQL.ExecuteReader();

                    // print the CustomerID of each record
                    while (mConnection.reader.Read())
                    {
                        string nome = mConnection.reader.GetValue(3).ToString().Replace("'", "\\'");
                        string cognome = mConnection.reader.GetValue(4).ToString().Replace("'", "\\'");
                        string camera = mConnection.reader.GetValue(0).ToString();
                        string arr = mConnection.reader.GetValue(1).ToString();
                        string par = mConnection.reader.GetValue(2).ToString();

                        DateTime arrival = new DateTime(DateTime.Parse(arr).Year, DateTime.Parse(arr).Month, DateTime.Parse(arr).Day);
                        DateTime departure = new DateTime(DateTime.Parse(par).Year, DateTime.Parse(par).Month, DateTime.Parse(par).Day);

                        if (cognome + " " + nome == nomeT)
                        {


                            if (cameraT == camera)
                            {
                                if (currentDate >= arrival && currentDate <= departure)
                                {
                                    tabControl1.SelectTab(1);


                                    mConnection.cnMySQL.Close();
                                    NomeP.Text = nome;
                                    CognomeP.Text = cognome;
                                    data.Text = par.Substring(0, 10);
                                    datarr.Text = arr.Substring(0, 10);
                                    return;
                                }
                            }
                        }
                    }
                    mConnection.cnMySQL.Close();
                }
            }
        }
        Prenotazione insertCustomer;
        List<string> column_names = new List<string>();
        private void button8_Click(object sender, EventArgs e)
        {
               
            for (int i = 0; i < tableau_grid.SelectedCells.Count; i++)
            {
                if (tableau_grid.SelectedCells[i].Value.ToString() != null && tableau_grid.SelectedCells[i].Value.ToString() != "")
                    return;
            }
            DateTime d1, d2;
            List<string> cam = new List<string>();
            // object x = tableau_grid.Columns[tableau_grid.SelectedCells[0].ColumnIndex].HeaderCell.Value.ToString();
            //tableau_grid.SelectedCells
            int index = -1;
            DataGridViewCell oldc = tableau_grid.SelectedCells[0];
            rowindextoreturn = tableau_grid.SelectedCells[0].RowIndex;
           
            //  int col=tableau_grid.SelectedCells[0].;
            //foreach (DataGridViewCell c in tableau_grid.SelectedCells)
            //{

            //if (oldc.RowIndex == c.RowIndex)
            //{
            //    if (index != c.ColumnIndex)
            //    {
            //        index = c.ColumnIndex;
            //        cam.Add(tableau_grid.Columns[index].HeaderCell.Value.ToString());
            //    }

            //    oldc = c;
            //}
            //else
            //{
            //    break;
            //}

            //}

            foreach (string head in column_names)
            {
                cam.Add(head);
            }

            d1 = DateTime.Parse(tableau_grid.Rows[tableau_grid.SelectedCells[0].RowIndex].HeaderCell.Value.ToString());

            d2 = DateTime.Parse(tableau_grid.Rows[tableau_grid.SelectedCells[tableau_grid.SelectedCells.Count - 1].RowIndex].HeaderCell.Value.ToString());
            if (d1 < d2)
                insertCustomer = new Prenotazione(mConnection, mConnection.cnMySQL, this, d1, d2, cam);
            else
                insertCustomer = new Prenotazione(mConnection, mConnection.cnMySQL, this, d2, d1, cam);

            insertCustomer.Show();
        }

        private void tableau_grid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        HashSet<int> column_indicies = new HashSet<int>();
        DataGridViewCell oldcell;
        int number_of_columns = 0;
        bool tableauhascreated = false;
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (tableauhascreated)
            {
                column_indicies.Clear();
                column_names.Clear();

                int indexrow = 0;
                oldcell = null;
                foreach (DataGridViewCell cell in tableau_grid.SelectedCells)
                {
                    if (oldcell == null)
                    {
                        // Set of column indicies
                        column_indicies.Add(cell.OwningColumn.Index);
                        // Set of column names
                        column_names.Add(cell.OwningColumn.Name);
                        indexrow = cell.OwningRow.Index;
                    }
                    else
                    {
                        if (cell.OwningRow.Index == indexrow)
                        {
                            // Set of column indicies
                            column_indicies.Add(cell.OwningColumn.Index);
                            // Set of column names
                            column_names.Add(cell.OwningColumn.Name);
                        }
                    }
                    //    if (column_names.Count > 1)
                    //{
                    //    if (oldcell.RowIndex == cell.RowIndex)
                    //    {
                    //        //if (column_names[column_names.Count - 1] == column_names[column_names.Count - 2])
                    //        //{
                    //            column_names.RemoveAt(column_names.Count - 1);
                    //        //}
                    //    }
                    //}
                    oldcell = cell;
                }
                // Number of columns the selection ranges over
                number_of_columns = column_indicies.Count;
            }
        }

        private void button23_Click(object sender, EventArgs e)
        {
            if (mConnection.cnMySQL.State == ConnectionState.Closed)
                updateall();
        }

        private void search_Click(object sender, EventArgs e)
        {
            if (ListaClienti.SelectedRows.Count != 0)
            {
                DataGridViewRow srow = ListaClienti.SelectedRows[0];

                if (srow.Cells[0].Value.ToString() != "")
                {
                    Prenotazione pren = new Prenotazione(mConnection,mConnection.cnMySQL, this, Convert.ToInt32(srow.Cells[0].Value.ToString()), srow.Cells[1].Value.ToString(), srow.Cells[2].Value.ToString(), srow.Cells[5].Value.ToString(), srow.Cells[4].Value.ToString(), srow.Cells[3].Value.ToString());
                    pren.Show();
                    pren.Focus();
                }
            }
            else
            {

                if (ListaClienti.SelectedCells[0] != null)
                {
                    DataGridViewRow srow = ListaClienti.Rows[ListaClienti.SelectedCells[0].RowIndex];
                    if (srow.Cells[0].Value.ToString() != "")
                    {
                        Prenotazione pren = new Prenotazione(mConnection,mConnection.cnMySQL, this, Convert.ToInt32(srow.Cells[0].Value.ToString()), srow.Cells[1].Value.ToString(), srow.Cells[2].Value.ToString(), srow.Cells[5].Value.ToString(), srow.Cells[4].Value.ToString(), srow.Cells[3].Value.ToString());
                        pren.Show();
                        pren.Focus();
                    }
                }

            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            bool canremove = true;
            if (MessageBox.Show("Sei sicuro di voler proseguire?", "Attenzione!!!", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                foreach (DataGridViewRow d in ListaClienti.SelectedRows)
                {
                    if (d.Cells[0].Value.ToString() != "")
                    {

                        if (mConnection != null)
                        {

                            canremove = true;
                            if (mConnection.cnMySQL.State != ConnectionState.Closed)
                                mConnection.cnMySQL.Close();
                            if (mConnection.cnMySQL.State != ConnectionState.Open)
                                mConnection.cnMySQL.Open();
                            mConnection.cmdMySQL = mConnection.cnMySQL.CreateCommand();

                            mConnection.cmdMySQL.CommandText = "SELECT pagata,checkin,checkout FROM Prenotazioni where prenotazioni.codicecliente=" + Convert.ToInt16(d.Cells[0].Value.ToString()) + ";";
                            mConnection.reader = mConnection.cmdMySQL.ExecuteReader();

                            while (mConnection.reader.Read())
                            {
                                int pagata = mConnection.reader.GetInt16(0);
                                int checkin = mConnection.reader.GetInt16(1);
                                int checkout = mConnection.reader.GetInt16(2);
                                if (pagata == 0 || checkin == 0 || checkout == 0)
                                {
                                    //MessageBox.Show("Il Cliente non può essere cancellato!", "Attenzione!", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                                    //mConnection.cnMySQL.Close();
                                    //return;
                                    canremove = false;
                                }
                            }
                            mConnection.cnMySQL.Close();

                            if (canremove)
                            {
                                if (mConnection.cnMySQL.State != ConnectionState.Closed)
                                    mConnection.cnMySQL.Close();
                                if (mConnection.cnMySQL.State != ConnectionState.Open)
                                    mConnection.cnMySQL.Open();
                                mConnection.cmdMySQL = mConnection.cnMySQL.CreateCommand();

                                mConnection.cmdMySQL.CommandText = "DELETE FROM clienti where clienti.id=" + Convert.ToInt16(d.Cells[0].Value.ToString()) + ";";

                                mConnection.cmdMySQL.ExecuteNonQuery();
                                mConnection.cnMySQL.Close();
                            }

                        }
                    }
                }
                updateall();
            }
        }
        notcred notc;
        private void button19_Click(object sender, EventArgs e)
        {
            if (ListaClienti.SelectedRows.Count != 0)
            {
                DataGridViewRow srow = ListaClienti.SelectedRows[0];

                if (srow.Cells[0].Value.ToString() != "")
                {
                    notc = new notcred(mConnection, Convert.ToInt32(srow.Cells[0].Value.ToString()));
                    notc.Show();
                    notc.Focus();
                }
            }
            else
            {

                if (ListaClienti.SelectedCells[0] != null)
                {
                    DataGridViewRow srow = ListaClienti.Rows[ListaClienti.SelectedCells[0].RowIndex];
                    if (srow.Cells[0].Value.ToString() != "")
                    {
                        notc = new notcred(mConnection, Convert.ToInt32(srow.Cells[0].Value.ToString()));
                        notc.Show();
                        notc.Focus();
                    }
                }

            }
        }
        ModificaCliente modclienti;
        private void button17_Click(object sender, EventArgs e)
        {
            modclienti = new ModificaCliente(mConnection);
        }

        private void button18_Click(object sender, EventArgs e)
        {
            if (ListaClienti.SelectedCells[0] != null)
            {
                DataGridViewRow srow = ListaClienti.Rows[ListaClienti.SelectedCells[0].RowIndex];
                if (srow.Cells[0].Value.ToString() != "")
                {
                    modclienti = new ModificaCliente(mConnection, Convert.ToInt32(srow.Cells[0].Value.ToString()), srow.Cells[1].Value.ToString(), srow.Cells[2].Value.ToString(), srow.Cells[5].Value.ToString(), srow.Cells[4].Value.ToString(), srow.Cells[3].Value.ToString());
                    modclienti.Show();
                    modclienti.Focus();
                }
            }
        }

        private void ragsc_TextChanged(object sender, EventArgs e)
        {

            refreshsearch();
        }

        private void name_TextChanged(object sender, EventArgs e)
        {

            refreshsearch();
        }

        private void surname_TextChanged(object sender, EventArgs e)
        {
            refreshsearch();
        }
        void refreshsearch()
        {
            if (surname.Text == "" && name.Text == "" && ragsc.Text == "")
            {
                refreshlistaclienti();
                return;
            }

            if (surname.Text != "" && name.Text != "")
            {
                // SUBSTRING (my_column FROM 1 FOR 1)

                ListaClienti.Rows.Clear();
                if (mConnection != null)
                {
                    if (mConnection.cnMySQL.State != ConnectionState.Closed)
                        mConnection.cnMySQL.Close();

                    if (mConnection.cnMySQL.State != ConnectionState.Open)
                        mConnection.cnMySQL.Open();
                    mConnection.cmdMySQL = new MySqlCommand("select * from clienti where substring(nome ,1," + name.Text.Length + ")= '" + name.Text + "' and substring(cognome ,1," + surname.Text.Length + ")= '" + surname.Text + "'  Order by cognome ASC", mConnection.cnMySQL);

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

                        // for (int i = 0; i < mConnection.reader.FieldCount; i++)
                        //   thisrow += mConnection.reader.GetValue(i).ToString() + ",";  
                        DataGridViewRow row = new DataGridViewRow();
                        string[] values = new string[6];
                        values[0] = mConnection.reader.GetValue(0).ToString();
                        values[1] = mConnection.reader.GetValue(1).ToString();
                        values[2] = mConnection.reader.GetValue(2).ToString();
                        values[3] = mConnection.reader.GetValue(3).ToString();
                        values[4] = mConnection.reader.GetValue(4).ToString();
                        values[5] = mConnection.reader.GetValue(5).ToString();
                        row.CreateCells(ListaClienti, values);
                        ListaClienti.Rows.Add(row);
                    }
                    mConnection.cnMySQL.Close();
                }
                return;
            }

            if (name.Text != "")
            {
                // SUBSTRING (my_column FROM 1 FOR 1)

                ListaClienti.Rows.Clear();
                if (mConnection != null)
                {
                    if (mConnection.cnMySQL.State != ConnectionState.Closed)
                        mConnection.cnMySQL.Close();

                    if (mConnection.cnMySQL.State != ConnectionState.Open)
                        mConnection.cnMySQL.Open();
                    mConnection.cmdMySQL = new MySqlCommand("select * from clienti where substring(nome ,1," + name.Text.Length + ")= '" + name.Text + "' Order by cognome ASC", mConnection.cnMySQL);

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

                        // for (int i = 0; i < mConnection.reader.FieldCount; i++)
                        //   thisrow += mConnection.reader.GetValue(i).ToString() + ",";  
                        DataGridViewRow row = new DataGridViewRow();
                        string[] values = new string[6];
                        values[0] = mConnection.reader.GetValue(0).ToString();
                        values[1] = mConnection.reader.GetValue(1).ToString();
                        values[2] = mConnection.reader.GetValue(2).ToString();
                        values[3] = mConnection.reader.GetValue(3).ToString();
                        values[4] = mConnection.reader.GetValue(4).ToString();
                        values[5] = mConnection.reader.GetValue(5).ToString();
                        row.CreateCells(ListaClienti, values);
                        ListaClienti.Rows.Add(row);
                    }
                    mConnection.cnMySQL.Close();
                }

            }


            if (surname.Text != "")
            {
                // SUBSTRING (my_column FROM 1 FOR 1)

                ListaClienti.Rows.Clear();
                if (mConnection != null)
                {
                    if (mConnection.cnMySQL.State != ConnectionState.Closed)
                        mConnection.cnMySQL.Close();

                    if (mConnection.cnMySQL.State != ConnectionState.Open)
                        mConnection.cnMySQL.Open();
                    mConnection.cmdMySQL = new MySqlCommand("select * from clienti where substring(cognome ,1," + surname.Text.Length + ")= '" + surname.Text + "' Order by cognome ASC", mConnection.cnMySQL);

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

                        // for (int i = 0; i < mConnection.reader.FieldCount; i++)
                        //   thisrow += mConnection.reader.GetValue(i).ToString() + ",";  
                        DataGridViewRow row = new DataGridViewRow();
                        string[] values = new string[6];
                        values[0] = mConnection.reader.GetValue(0).ToString();
                        values[1] = mConnection.reader.GetValue(1).ToString();
                        values[2] = mConnection.reader.GetValue(2).ToString();
                        values[3] = mConnection.reader.GetValue(3).ToString();
                        values[4] = mConnection.reader.GetValue(4).ToString();
                        values[5] = mConnection.reader.GetValue(5).ToString();
                        row.CreateCells(ListaClienti, values);
                        ListaClienti.Rows.Add(row);
                    }
                    mConnection.cnMySQL.Close();
                }

            }
            if (ragsc.Text != "")
            {
                // SUBSTRING (my_column FROM 1 FOR 1)

                ListaClienti.Rows.Clear();
                if (mConnection != null)
                {
                    if (mConnection.cnMySQL.State != ConnectionState.Closed)
                        mConnection.cnMySQL.Close();

                    if (mConnection.cnMySQL.State != ConnectionState.Open)
                        mConnection.cnMySQL.Open();
                    mConnection.cmdMySQL = new MySqlCommand("select * from clienti where substring(ragionesociale ,1," + ragsc.Text.Length + ")= '" + ragsc.Text + "' Order by cognome ASC", mConnection.cnMySQL);

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

                        // for (int i = 0; i < mConnection.reader.FieldCount; i++)
                        //   thisrow += mConnection.reader.GetValue(i).ToString() + ",";  
                        DataGridViewRow row = new DataGridViewRow();
                        string[] values = new string[6];
                        values[0] = mConnection.reader.GetValue(0).ToString();
                        values[1] = mConnection.reader.GetValue(1).ToString();
                        values[2] = mConnection.reader.GetValue(2).ToString();
                        values[3] = mConnection.reader.GetValue(3).ToString();
                        values[4] = mConnection.reader.GetValue(4).ToString();
                        values[5] = mConnection.reader.GetValue(5).ToString();
                        row.CreateCells(ListaClienti, values);
                        ListaClienti.Rows.Add(row);
                    }
                    mConnection.cnMySQL.Close();
                }

            }

        }
        Sospesi sosp;
        private void button28_Click(object sender, EventArgs e)
        {
            if (Sospesi.SelectedRows.Count > 0)
            {


                mConnection.cnMySQL.Close();

                if (sosp == null)
                {
                    sosp = new Sospesi(mConnection, Sospesi.SelectedRows[0].Cells[0].Value.ToString());
                    sosp.Show();
                }
                else if (sosp.IsDisposed)
                {
                    sosp = new Sospesi(mConnection, Sospesi.SelectedRows[0].Cells[0].Value.ToString());
                    sosp.Show();
                }


                sosp.Focus();
            }
        }

        private void button26_Click(object sender, EventArgs e)
        {
            if (Sospesi.SelectedRows.Count > 0)
            {
                if (MessageBox.Show("Sei sicuro?", "Attenzione!!!", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                {
                    return;
                }

                foreach (DataGridViewRow it in Sospesi.SelectedRows)
                {
                    mConnection.cnMySQL.Open();
                    mConnection.cmdMySQL = mConnection.cnMySQL.CreateCommand();

                    mConnection.cmdMySQL.CommandText =
                            "UPDATE Prenotazioni SET pagata = true where prenotazioni.id=" + Convert.ToInt16(it.Cells[0].Value.ToString());

                    mConnection.cmdMySQL.ExecuteNonQuery();
                    mConnection.cnMySQL.Close();

                }
                Sospesi.Rows.Clear();
                refreshsospesi();
            }
        }

        private void button27_Click(object sender, EventArgs e)
        {
            if (Sospesi.SelectedRows.Count > 0)
            {


                if (fatoric == null)
                {
                    fatoric = new FatturaoRicevuta(mConnection, Sospesi.SelectedRows[0].Cells[0].Value.ToString());//, Overview.SelectedItems[0].SubItems[11].Text, numerostanze, Overview.SelectedItems[0].SubItems[3].Text, Overview.SelectedItems[0].SubItems[4].Text, Convert.ToSingle(Overview.SelectedItems[0].SubItems[5].Text), Convert.ToBoolean(Overview.SelectedItems[0].SubItems[6].Text), Convert.ToSingle(Overview.SelectedItems[0].SubItems[9].Text), Convert.ToInt16(Overview.SelectedItems[0].SubItems[10].Text));
                    fatoric.Show();
                }
                else if (fatoric.IsDisposed)
                {
                    fatoric = new FatturaoRicevuta(mConnection, Sospesi.SelectedRows[0].Cells[0].Value.ToString());//, Overview.SelectedItems[0].SubItems[11].Text, numerostanze, Overview.SelectedItems[0].SubItems[3].Text, Overview.SelectedItems[0].SubItems[4].Text, Convert.ToSingle(Overview.SelectedItems[0].SubItems[5].Text), Convert.ToBoolean(Overview.SelectedItems[0].SubItems[6].Text), Convert.ToSingle(Overview.SelectedItems[0].SubItems[9].Text), Convert.ToInt16(Overview.SelectedItems[0].SubItems[10].Text));
                    fatoric.Show();
                }

            }
        }

        private void textBox31_TextChanged(object sender, EventArgs e)
        {
            Sospesi.Rows.Clear();
            if (textBox32.Text == "")
            {
                if (mConnection != null)
                {
                    mConnection.cmdMySQL = new MySqlCommand(
                           "SELECT id, nome, cognome, pagata FROM prenotazioni where substring(nome ,1," + textBox31.Text.Length + ")= '" + textBox31.Text + "' ORDER BY prenotazioni.arrivo,prenotazioni.partenza ASC;", mConnection.cnMySQL);
                    if (mConnection.cnMySQL.State != ConnectionState.Closed)
                        mConnection.cnMySQL.Close();
                    if (mConnection.cnMySQL.State != ConnectionState.Open)
                        mConnection.cnMySQL.Open();
                    mConnection.reader = mConnection.cmdMySQL.ExecuteReader();

                    // print the CustomerID of each record

                    while (mConnection.reader.Read())
                    {
                        bool pagata = mConnection.reader.GetBoolean(3);
                        string nome = mConnection.reader.GetString(2) + " " + mConnection.reader.GetString(1);
                        string pren = mConnection.reader.GetValue(0).ToString();
                        if (!pagata)
                        {
                            DataGridViewRow row = (DataGridViewRow)Sospesi.Rows[0].Clone();
                            row.Cells[0].Value = pren.ToString();
                            row.Cells[1].Value = nome.ToString();
                            Sospesi.Rows.Add(row);
                        }
                    }
                    mConnection.cnMySQL.Close();
                }
            }
            else
            {
                if (mConnection != null)
                {
                    mConnection.cmdMySQL = new MySqlCommand(
                           "SELECT id, nome, cognome, pagata FROM prenotazioni where substring(cognome ,1," + textBox32.Text.Length + ")= '" + textBox32.Text + "' and substring(nome ,1," + textBox31.Text.Length + ")= '" + textBox31.Text + "' ORDER BY prenotazioni.arrivo,prenotazioni.partenza ASC;", mConnection.cnMySQL);
                    if (mConnection.cnMySQL.State != ConnectionState.Closed)
                        mConnection.cnMySQL.Close();
                    if (mConnection.cnMySQL.State != ConnectionState.Open)
                        mConnection.cnMySQL.Open();
                    mConnection.reader = mConnection.cmdMySQL.ExecuteReader();

                    // print the CustomerID of each record

                    while (mConnection.reader.Read())
                    {
                        bool pagata = mConnection.reader.GetBoolean(3);
                        string nome = mConnection.reader.GetString(2) + " " + mConnection.reader.GetString(1);
                        string pren = mConnection.reader.GetValue(0).ToString();
                        if (!pagata)
                        {
                            DataGridViewRow row = (DataGridViewRow)Sospesi.Rows[0].Clone();
                            row.Cells[0].Value = pren.ToString();
                            row.Cells[1].Value = nome.ToString();
                            Sospesi.Rows.Add(row);
                        }
                    }
                    mConnection.cnMySQL.Close();
                }
            }
        }

        private void textBox32_TextChanged(object sender, EventArgs e)
        {
            Sospesi.Rows.Clear();
            if (textBox31.Text == "")
            {
                if (mConnection != null)
                {
                    mConnection.cmdMySQL = new MySqlCommand(
                           "SELECT id, nome, cognome, pagata FROM prenotazioni where substring(cognome ,1," + textBox32.Text.Length + ")= '" + textBox32.Text + "' ORDER BY prenotazioni.arrivo,prenotazioni.partenza ASC;", mConnection.cnMySQL);
                    if (mConnection.cnMySQL.State != ConnectionState.Closed)
                        mConnection.cnMySQL.Close();
                    if (mConnection.cnMySQL.State != ConnectionState.Open)
                        mConnection.cnMySQL.Open();
                    mConnection.reader = mConnection.cmdMySQL.ExecuteReader();

                    // print the CustomerID of each record

                    while (mConnection.reader.Read())
                    {
                        bool pagata = mConnection.reader.GetBoolean(3);
                        string nome = mConnection.reader.GetString(2) + " " + mConnection.reader.GetString(1);
                        string pren = mConnection.reader.GetValue(0).ToString();
                        if (!pagata)
                        {
                            DataGridViewRow row = (DataGridViewRow)Sospesi.Rows[0].Clone();
                            row.Cells[0].Value = pren.ToString();
                            row.Cells[1].Value = nome.ToString();
                            Sospesi.Rows.Add(row);
                        }
                    }
                    mConnection.cnMySQL.Close();
                }
            }
            else
            {
                if (mConnection != null)
                {
                    mConnection.cmdMySQL = new MySqlCommand(
                           "SELECT id, nome, cognome, pagata FROM prenotazioni where substring(cognome ,1," + textBox32.Text.Length + ")= '" + textBox32.Text + "' and substring(nome ,1," + textBox31.Text.Length + ")= '" + textBox31.Text + "' ORDER BY prenotazioni.arrivo,prenotazioni.partenza ASC;", mConnection.cnMySQL);
                    if (mConnection.cnMySQL.State != ConnectionState.Closed)
                        mConnection.cnMySQL.Close();
                    if (mConnection.cnMySQL.State != ConnectionState.Open)
                        mConnection.cnMySQL.Open();
                    mConnection.reader = mConnection.cmdMySQL.ExecuteReader();

                    // print the CustomerID of each record

                    while (mConnection.reader.Read())
                    {
                        bool pagata = mConnection.reader.GetBoolean(3);
                        string nome = mConnection.reader.GetString(2) + " " + mConnection.reader.GetString(1);
                        string pren = mConnection.reader.GetValue(0).ToString();
                        if (!pagata)
                        {
                            DataGridViewRow row = (DataGridViewRow)Sospesi.Rows[0].Clone();
                            row.Cells[0].Value = pren.ToString();
                            row.Cells[1].Value = nome.ToString();
                            Sospesi.Rows.Add(row);
                        }
                    }
                    mConnection.cnMySQL.Close();
                }
            }
        }

        private void stampaFatturaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fatoric = new FatturaoRicevuta(mConnection, "-100");//, Panoramica.SelectedRows[0].SubItems[11].Text, numerostanze, Panoramica.SelectedRows[0].SubItems[3].Text, Panoramica.SelectedRows[0].SubItems[4].Text, Convert.ToSingle(Panoramica.SelectedRows[0].SubItems[5].Text), Convert.ToBoolean(Panoramica.SelectedRows[0].SubItems[6].Text), Convert.ToSingle(Panoramica.SelectedRows[0].SubItems[9].Text), Convert.ToInt16(Panoramica.SelectedRows[0].SubItems[10].Text));
            fatoric.Show();
        }
        About about;
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (about == null)
            {
                about = new About();
                about.Show();
            }
            else if (about.IsDisposed)
            {
                about = new About();
                about.Show();
            }


            about.Focus();
        }

        private void fattureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string path = System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\Fatture\\";

            System.Diagnostics.Process.Start("explorer.exe", path);
        }

        private void noteCreditoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string path = System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\NoteCredito\\";

            System.Diagnostics.Process.Start("explorer.exe", path);
        }

        private void ricevuteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string path = System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\Ricevute\\";

            System.Diagnostics.Process.Start("explorer.exe", path);
        }

        private void disconnettiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mConnection != null)
            {
                mConnection.cnMySQL.Close();
                mConnection = null;
                Panoramica.Rows.Clear();
                //  tableau_grid.Rows.Clear();
                // tableau_grid.Columns.Clear();
                ListaClienti.Rows.Clear();
                Sospesi.Rows.Clear();
                Prenotazioni.Rows.Clear();
                label1.Text = "Disconnesso";
                //label2.Text = "Database Status: " + mConnection.cnMySQL.State.ToString();
            }
        }

        private void connettiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //this.InitializeComponent();
            mDatabaseSettings = new DatabaseSettings();

            server = mDatabaseSettings.mServer;
            database = mDatabaseSettings.mDatabase;
            uid = mDatabaseSettings.mUser;
            password = mDatabaseSettings.mPassword;
            string cnMySQLString;
            cnMySQLString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
            mConnection = new connectoToMySql(server, database, uid, password);

            TableauCreated = false;
            tableauhascreated = false;
            recreateall();
            //updateall();
        }

        private void impostazioniDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mDatabaseSettings.Show();
        }

        Arrangiamento arrangiamento;
        private void arrangiamentoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (arrangiamento == null)
            {
                arrangiamento = new Arrangiamento(mConnection);
                arrangiamento.Show();
            }
            else if (arrangiamento.IsDisposed)
            {
                arrangiamento = new Arrangiamento(mConnection);
                arrangiamento.Show();
            }

            arrangiamento.Focus();
        }

        Fonti fonti;
        private void fontiDiPrenotazioneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (fonti == null)
            {
                fonti = new Fonti(mConnection);
                fonti.Show();
            }
            else if (fonti.IsDisposed)
            {
                fonti = new Fonti(mConnection);
                fonti.Show();
            }

            fonti.Focus();
        }

        Operatori operatori;
        private void operatoriToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (operatori == null)
            {
                operatori = new Operatori(mConnection);
                operatori.Show();
            }
            else if (operatori.IsDisposed)
            {
                operatori = new Operatori(mConnection);
                operatori.Show();
            }

            operatori.Focus();
        }

        editroom ediroom;
        private void stanzeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ediroom == null)
            {
                ediroom = new editroom(mConnection);
                ediroom.Show();
            }
            else if (ediroom.IsDisposed)
            {
                ediroom = new editroom(mConnection);
                ediroom.Show();
            }

            ediroom.Focus();
        }

        Tariffe tariffe;

        private void tariffeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tariffe == null)
            {
                tariffe = new Tariffe(mConnection);
                tariffe.Show();
            }
            else if (tariffe.IsDisposed)
            {
                tariffe = new Tariffe(mConnection);
                tariffe.Show();
            }

            tariffe.Focus();
        }

        TariffeExtra tariffeExt;

        private void tariffeExtraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tariffeExt == null)
            {
                tariffeExt = new TariffeExtra(mConnection);
                tariffeExt.Show();
            }
            else if (tariffeExt.IsDisposed)
            {
                tariffeExt = new TariffeExtra(mConnection);
                tariffeExt.Show();
            }

            tariffeExt.Focus();
        }

        Tipologiaeconomica tipec;
        private void tipoClasseEconomicaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tipec == null)
            {
                tipec = new Tipologiaeconomica(mConnection);
                tipec.Show();
            }
            else if (tipec.IsDisposed)
            {
                tipec = new Tipologiaeconomica(mConnection);
                tipec.Show();
            }

            tipec.Focus();
        }

        Tipostanze tipes;

        private void tipoStanzeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tipes == null)
            {
                tipes = new Tipostanze(mConnection);
                tipes.Show();
            }
            else if (tipes.IsDisposed)
            {
                tipes = new Tipostanze(mConnection);
                tipes.Show();
            }

            tipes.Focus();
        }
        Progressivo progressivo;
        private void progressiviToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (progressivo == null)
            {
                progressivo = new Progressivo(mConnection);
                progressivo.Show();
            }
            else if (progressivo.IsDisposed)
            {
                progressivo = new Progressivo(mConnection);
                progressivo.Show();
            }
        }

        private void stampaFatturaToolStripMenuItem_Click_1(object sender, EventArgs e)
        {

        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void aboutToolStripMenuItem_Click_1(object sender, EventArgs e)
        {

        }

        private void impostazioniDatabaseToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            impostazioniDatabaseToolStripMenuItem_Click(sender, e);
        }

        private void fattureToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            fattureToolStripMenuItem_Click(sender, e);
        }

        private void noteCreditoToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            noteCreditoToolStripMenuItem_Click(sender, e);
        }

        private void ricevuteToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            ricevuteToolStripMenuItem_Click(sender, e);
        }

        private void stampaFatturaToolStripMenuItem_Click_2(object sender, EventArgs e)
        {
            stampaFatturaToolStripMenuItem_Click(sender, e);
        }

        private void aboutToolStripMenuItem_Click_2(object sender, EventArgs e)
        {
            aboutToolStripMenuItem_Click(sender, e);
        }

        private void arrangiamentoToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            arrangiamentoToolStripMenuItem_Click(sender, e);
        }

        private void fontiDiPrenotazioneToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            fontiDiPrenotazioneToolStripMenuItem_Click(sender, e);
        }

        private void operatoriToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            /*operatori = new Operatori(mConnection);
            operatori.Show();
            operatori.Focus();*/
            operatoriToolStripMenuItem_Click(sender, e);
        }

        private void stanzeToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            stanzeToolStripMenuItem_Click(sender, e);
        }

        private void tariffeToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            tariffeToolStripMenuItem_Click(sender, e);
        }

        private void tariffeExtraToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            tariffeExtraToolStripMenuItem_Click(sender, e);
        }

        private void tipoClasseEconomicaToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            tipoClasseEconomicaToolStripMenuItem_Click(sender, e);
        }

        private void tipoStanzeToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            tipoStanzeToolStripMenuItem_Click(sender, e);
        }

        private void progressiviToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            progressiviToolStripMenuItem_Click(sender, e);
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked)
            {
                checkBox4.Checked = false;
            }
            Sospesi.Rows.Clear();
            refreshsospesi();
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
            {
                checkBox3.Checked = false;
            }
            Sospesi.Rows.Clear();
            refreshsospesi();
        }

        private void turniToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Turni t = new Turni(mConnection);
            t.Show();
            t.Focus();
        }

        private void creaCartellaCronologicaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetCreateMyFolder();
        }

        private void button29_Click(object sender, EventArgs e)
        {
            tableau_grid.FirstDisplayedScrollingRowIndex = rowindextoreturn;
        }

        private void button30_Click(object sender, EventArgs e)
        {
            MessageBox.Show("il tableau sta per essere ricalcolato attendi 2 minuti per riprendere a lavorare", "Attenzione!!!",
MessageBoxButtons.OK, MessageBoxIcon.Warning);
            tableau_grid.Rows.Clear();
            tableau_grid.Columns.Clear();
            createtableau();
            calctableau();
     
        }

        private void bibiteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Fattura f = new Fattura(mConnection, "Ricevuta", "-100", false, true);//,cliente, numerocamere,datear, datepar, tariffa, forfait,anticipo,iva);
            f.Show();
        }

        private void button30_Click_1(object sender, EventArgs e)
        {
            Fattura f = new Fattura(mConnection, "Ricevuta", "-100", false, false);//,cliente, numerocamere,datear, datepar, tariffa, forfait,anticipo,iva);
            f.Show();
        }

        private void storicoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Storico st = new Storico(mConnection);
            st.Show();
            st.Focus();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if(Hint==null|| Hint.IsDisposed)
            {
                Hint = new Form2();
                Hint.StartPosition = FormStartPosition.Manual;
                Hint.Location = new Point(Screen.PrimaryScreen.Bounds.Width - Hint.Bounds.Width, 64);
                Hint.label.Text = "Ciao Sono il tuo assistente personale,\nogni volta che compierai una nuova azione\nio sarò qui per aiutarti!\nQuando hai qualche dubbio premi\nil tasto ? in alto a destra!";
            }
            Hint.Show();
            Hint.Focus();
        }

        private void Bel3_Load(object sender, EventArgs e)
        {
            Hint = new Form2();
            Hint.StartPosition = FormStartPosition.Manual;
            Hint.Location = new Point(Screen.PrimaryScreen.Bounds.Width - Hint.Bounds.Width, 64);
           /* Hint.Show();
            Hint.Focus();*/
        }

      





    }
}
