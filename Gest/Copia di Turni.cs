using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Bel3
{

    public partial class Turni : Form
    {
        connectoToMySql mConnection;
        Random rnd;
     
        List<turnista> Turnisti;   
        List<turnista> TurnistiNotturni;
        List<turnista> TurnistiDiurni;
        List<turnista> Domenicanti;
        public Turni(connectoToMySql mcon)
        {
            mConnection = mcon;
            InitializeComponent();
            rnd = new Random();
        }
        turnista def;
        private void button1_Click(object sender, EventArgs e)
        {

            DateTime DaytoSet = DateTime.Now;
            int daytoadd=8- (int)DaytoSet.DayOfWeek;
            int found = 0;
          DaytoSet=  DaytoSet.AddDays(daytoadd);
            TurnistiNotturni = new List<turnista>();
            TurnistiDiurni = new List<turnista>();
            Turnisti = new List<turnista>();
            if (mConnection.cnMySQL.State == ConnectionState.Open)
                mConnection.cnMySQL.Close();

            if (mConnection.cnMySQL.State != ConnectionState.Open)
                mConnection.cnMySQL.Open();
            if (mConnection.cnMySQL.State == ConnectionState.Open)
            {
                mConnection.cmdMySQL.CommandText = "Select * From Turnista";
                mConnection.reader = mConnection.cmdMySQL.ExecuteReader();
                while (mConnection.reader.Read())
                {
                    turnista current=new turnista(mConnection.reader.GetInt16(0), mConnection.reader.GetString(1), mConnection.reader.GetBoolean(2));
                    Turnisti.Add(current);

                    if (mConnection.reader.GetBoolean(2))

                        TurnistiNotturni.Add(current);
                    else
                        TurnistiDiurni.Add(current);

                  
                }

                mConnection.cnMySQL.Close();
            }
            for (int i = 0; i < 7; i++)
            {

               
                if (DaytoSet.DayOfWeek != DayOfWeek.Sunday)
                {
                    //Mattina
                    def = null;
                    int count=rnd.Next(0, TurnistiDiurni.Count+1);
                    if (count >= TurnistiDiurni.Count)
                        def = TurnistiDiurni[0];
                    else
                        def = TurnistiDiurni[count];
                    foreach (turnista tdi in TurnistiDiurni)
                    {
                        if (def != tdi)
                        {
                            if (def.Ore >= tdi.Ore)
                            {
                                    if (def.Shift < tdi.Shift && tdi.Shift>2)
                                        def = tdi;
                            }
                        }
                    }
                    found = TurnistiDiurni.IndexOf(def);
                    TurnistiDiurni[found].Ore += 7;
                    TurnistiDiurni[found].Shift = 0;
                    for (int c = 0; c < TurnistiDiurni.Count; c++)
                    {
                        if (c != found)
                        {
                            TurnistiDiurni[c].Shift++;
                        }
                    }

                    //if (mConnection.cnMySQL.State == ConnectionState.Open)
                    //    mConnection.cnMySQL.Close();

                    //if (mConnection.cnMySQL.State != ConnectionState.Open)
                    //    mConnection.cnMySQL.Open();
                    //if (mConnection.cnMySQL.State == ConnectionState.Open)
                    //{
                    //    mConnection.cmdMySQL.CommandText = "INSERT INTO Turno values(0,'" + DaytoSet.ToShortDateString() + " " + DaytoSet.DayOfWeek + "',0,'" + def.Nome + "');";
                    //    mConnection.cmdMySQL.ExecuteNonQuery();
                    //    mConnection.cnMySQL.Close();
                    //}

                    DataGridViewRow row = (DataGridViewRow)panoramicaturni.Rows[0].Clone();
                    row.Cells[0].Value = DaytoSet.ToShortDateString() + " " + DaytoSet.DayOfWeek;
                    row.Cells[1].Value = def.Nome;

                    def = null;
                  
                    //Pomeriggio
                    count = rnd.Next(0, Turnisti.Count + 1);
                    if (count >= Turnisti.Count)
                        def = Turnisti[0];
                    else
                        def = Turnisti[count];
                    foreach (turnista tdi in Turnisti)
                    {
                       
                            if (def.Ore >= tdi.Ore)
                            {
                                if (def.Shift < tdi.Shift && tdi.Shift > 2)
                                    def = tdi;
                            }
                    }
                    found = Turnisti.IndexOf(def);
                    Turnisti[found].Ore += 7;
                    Turnisti[found].Shift = 0;

                    for (int c = 0; c < Turnisti.Count; c++)
                    {
                        if (c != found)
                        {
                            Turnisti[c].Shift++;
                        }
                    }

                    //if (mConnection.cnMySQL.State == ConnectionState.Open)
                    //    mConnection.cnMySQL.Close();

                    //if (mConnection.cnMySQL.State != ConnectionState.Open)
                    //    mConnection.cnMySQL.Open();
                    //if (mConnection.cnMySQL.State == ConnectionState.Open)
                    //{
                    //    mConnection.cmdMySQL.CommandText = "INSERT INTO Turno values(0,'" + DaytoSet.ToShortDateString() + " " + DaytoSet.DayOfWeek + "',1,'" + def.Nome + "');";
                    //    mConnection.cmdMySQL.ExecuteNonQuery();
                    //    mConnection.cnMySQL.Close();
                    //}
                    row.Cells[2].Value = def.Nome;
                    List<turnista> affiancatori = new List<turnista>();
                    foreach (turnista tn in TurnistiNotturni)
                    {
                        if(tn!=def)
                        affiancatori.Add(tn);
                    }


                    def = null;
                    
                    //Notte
                    count = rnd.Next(0, TurnistiNotturni.Count + 1);
                    if (count >= TurnistiNotturni.Count)
                        def = TurnistiNotturni[0];
                    else
                        def = TurnistiNotturni[count];
                    //def = TurnistiNotturni[rnd.Next(0, TurnistiNotturni.Count)];
                    foreach (turnista tdi in TurnistiNotturni)
                    {
                       
                            if (def.Ore >= tdi.Ore)
                            {
                                if (def.Shift < tdi.Shift && tdi.Shift > 2)
                                    def = tdi;
                            }
                    }
                    found = TurnistiNotturni.IndexOf(def);
                    TurnistiNotturni[found].Ore += 10;
                    TurnistiNotturni[found].Shift = -1;
                    for (int c = 0; c < TurnistiNotturni.Count; c++)
                    {
                        if (c != found)
                        {
                            TurnistiNotturni[c].Shift++;
                        }
                    }

                    //if (mConnection.cnMySQL.State == ConnectionState.Open)
                    //    mConnection.cnMySQL.Close();

                    //if (mConnection.cnMySQL.State != ConnectionState.Open)
                    //    mConnection.cnMySQL.Open();
                    //if (mConnection.cnMySQL.State == ConnectionState.Open)
                    //{
                    //    mConnection.cmdMySQL.CommandText = "INSERT INTO Turno values(0,'" + DaytoSet.ToShortDateString() + " " + DaytoSet.DayOfWeek + "',3,'" + def.Nome + "');";
                    //    mConnection.cmdMySQL.ExecuteNonQuery();
                    //    mConnection.cnMySQL.Close();
                    //}

                    row.Cells[3].Value = def.Nome;
                  
                            affiancatori.Remove(def);
                   

                    def = null;
                    //Affiancamento
                    count = rnd.Next(0, affiancatori.Count + 1);
                    if (count >= affiancatori.Count)
                        def = affiancatori[0];
                    else
                        def = affiancatori[count];
                    foreach (turnista tdi in affiancatori)
                    {
                        
                            if (def.Ore >= tdi.Ore)
                            {
                                if (def.Shift < tdi.Shift && tdi.Shift > 2)
                                    def = tdi;
                            }
                          
                    }
                    found = affiancatori.IndexOf(def);
                    affiancatori[found].Ore += 7;
                    affiancatori[found].Shift = 0;
                    for (int c = 0; c < affiancatori.Count; c++)
                    {
                        if (c != found)
                        {
                            affiancatori[c].Shift++;
                        }
                    }

                    //if (mConnection.cnMySQL.State == ConnectionState.Open)
                    //    mConnection.cnMySQL.Close();

                    //if (mConnection.cnMySQL.State != ConnectionState.Open)
                    //    mConnection.cnMySQL.Open();
                    //if (mConnection.cnMySQL.State == ConnectionState.Open)
                    //{
                    //    mConnection.cmdMySQL.CommandText = "INSERT INTO Turno values(0,'" + DaytoSet.ToShortDateString() + " " + DaytoSet.DayOfWeek + "',2,'" + def.Nome + "');";
                    //    mConnection.cmdMySQL.ExecuteNonQuery();
                    //    mConnection.cnMySQL.Close();
                    //}
                    row.Cells[2].Value +="/"+ def.Nome;
                    def = null;
              
                    panoramicaturni.Rows.Add(row);
                   DaytoSet= DaytoSet.AddDays(1);
                }
                else
                {
                    if (Domenicanti == null)
                    {
                        Domenicanti = new List<turnista>();
                    }
                    if (Domenicanti.Count == 0)
                    {
                        foreach (turnista tn in Turnisti)
                            Domenicanti.Add(tn);
                    }
                    def = null;
                    //Mattina
                    int count = rnd.Next(0, Domenicanti.Count + 1);
                    if (count >= Domenicanti.Count)
                        def = Domenicanti[0];
                    else
                        def = Domenicanti[count];
                    foreach (turnista tdi in Domenicanti)
                    {
                       
                            if (def.Ore >= tdi.Ore)
                            {
                                if (def.Shift < tdi.Shift && tdi.Shift > 2)
                                    def = tdi;
                            }
                           
                    }
                    found = Domenicanti.IndexOf(def);
                    Domenicanti[found].Ore += 12;
                    Domenicanti[found].Shift = 0;
                    for (int c = 0; c < Domenicanti.Count; c++)
                    {
                        if (c != found)
                        {
                            Domenicanti[c].Shift++;
                        }
                    }

                    //if (mConnection.cnMySQL.State == ConnectionState.Open)
                    //    mConnection.cnMySQL.Close();

                    //if (mConnection.cnMySQL.State != ConnectionState.Open)
                    //    mConnection.cnMySQL.Open();
                    //if (mConnection.cnMySQL.State == ConnectionState.Open)
                    //{
                    //    mConnection.cmdMySQL.CommandText = "INSERT INTO Turno values(0,'" + DaytoSet.ToShortDateString() + " " + DaytoSet.DayOfWeek + "',0,'" + def.Nome + "');INSERT INTO Turno values(0,'" + DaytoSet.ToShortDateString() + " " + DaytoSet.DayOfWeek + "',1,'" + def.Nome + "');";
                    //    mConnection.cmdMySQL.ExecuteNonQuery();
                    //    mConnection.cnMySQL.Close();
                    //}
                    DataGridViewRow row = (DataGridViewRow)panoramicaturni.Rows[0].Clone();
                    row.Cells[0].Value = DaytoSet.ToShortDateString() + " " + DaytoSet.DayOfWeek;
                    row.Cells[1].Value = def.Nome;
                    row.Cells[2].Value = def.Nome;
                    Domenicanti.Remove(def);
                    def = null;
                    //Notte
                    count = rnd.Next(0, TurnistiNotturni.Count + 1);
                    if (count >= TurnistiNotturni.Count)
                        def = TurnistiNotturni[0];
                    else
                        def = TurnistiNotturni[count];
                    foreach (turnista tdi in TurnistiNotturni)
                    {
                       
                            if (def.Ore >= tdi.Ore)
                            {
                                if (def.Shift < tdi.Shift && tdi.Shift > 2)
                                    def = tdi;
                            }
                           
                    }
                    found = TurnistiNotturni.IndexOf(def);
                    TurnistiNotturni[found].Ore += 12;
                    TurnistiNotturni[found].Shift = -1;
                    for (int c = 0; c < TurnistiNotturni.Count; c++)
                    {
                        if (c != found)
                        {
                            TurnistiNotturni[c].Shift++;
                        }
                    }

                    //if (mConnection.cnMySQL.State == ConnectionState.Open)
                    //    mConnection.cnMySQL.Close();

                    //if (mConnection.cnMySQL.State != ConnectionState.Open)
                    //    mConnection.cnMySQL.Open();
                    //if (mConnection.cnMySQL.State == ConnectionState.Open)
                    //{
                    //    mConnection.cmdMySQL.CommandText = "INSERT INTO Turno values(0,'" + DaytoSet.ToShortDateString() + " " + DaytoSet.DayOfWeek + "',3,'" + def.Nome + "');";
                    //    mConnection.cmdMySQL.ExecuteNonQuery();
                    //    mConnection.cnMySQL.Close();
                    //}
                    row.Cells[3].Value = def.Nome;
                    def = null;
                    panoramicaturni.Rows.Add(row);
                    DaytoSet = DaytoSet.AddDays(1);
                }
            }

        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }


    }
}
