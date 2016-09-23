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

        List<turnista> TurnistiPomeridiani;
        List<turnista> TurnistiDiurni;
        List<turnista> Affiancatori;
        List<turnista> Domenicanti;
        List<turnista> DomenicantiNotturni;
        public Turni(connectoToMySql mcon)
        {
            mConnection = mcon;
            InitializeComponent();
            rnd = new Random();
            dateTimePicker1.Value = DateTime.Now;
            dateTimePicker2.Value = DateTime.Now.AddDays(1);

            
        }
        turnista def;
        private void button1_Click(object sender, EventArgs e)
        {
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
                    turnista current = new turnista(mConnection.reader.GetInt16(0), mConnection.reader.GetString(1), mConnection.reader.GetBoolean(2));
                    Turnisti.Add(current);
                    TurnistiDiurni = new List<turnista>();
                    Affiancatori = new List<turnista>();
                    TurnistiPomeridiani = new List<turnista>();
                    TurnistiNotturni = new List<turnista>();
                }

                mConnection.cnMySQL.Close();
            }

            panoramicaturni.Rows.Clear();
            DateTime DaytoSet= new DateTime(dateTimePicker1.Value.Year, dateTimePicker1.Value.Month, dateTimePicker1.Value.Day);
            DateTime DaytoReach = new DateTime(dateTimePicker2.Value.Year, dateTimePicker2.Value.Month, dateTimePicker2.Value.Day);
            TimeSpan span = DaytoReach - DaytoSet;
            int daytocalc = span.Days+1;
         
            for (int i = 0; i < daytocalc; i++)
            {

               
              
               
               
               
                if (DaytoSet.DayOfWeek != DayOfWeek.Sunday)
                {
                    if (TurnistiDiurni.Count == 0)
                    {
                        foreach (turnista tn in Turnisti)
                        {
                            if (!tn.Notturnista)
                                TurnistiDiurni.Add(tn);
                        }
                    }
                    //Mattina
                    def = null;
                    int count=rnd.Next(0, TurnistiDiurni.Count);
                    if (count >= TurnistiDiurni.Count)
                        def = TurnistiDiurni[0];
                    else
                        def = TurnistiDiurni[count];
                    foreach (turnista tn in TurnistiDiurni)
                    {
                        if (def != tn)
                        {
                                if (def.Ore >= tn.Ore)
                                {
                                    if (tn.Shift > def.Shift)
                                        def = tn;
                                }
                        }
                    }
                  
                    DataGridViewRow row = (DataGridViewRow)panoramicaturni.Rows[0].Clone();
                    row.Cells[0].Value = DaytoSet.ToShortDateString() + " " + DaytoSet.DayOfWeek;
                    row.Cells[1].Value = def.Nome;
                    def.Ore += 7;
                    def.Shift = 0;
                  
                    foreach (turnista tn in Turnisti)
                    {
                        if (tn != def)
                            tn.Shift++;
                    }
                    TurnistiPomeridiani.Remove(def);
                    def = null;
                    if (TurnistiPomeridiani.Count == 0)
                    {
                        foreach (turnista tn in Turnisti)
                        {
                            TurnistiPomeridiani.Add(tn);
                        }
                    }
                    //Pomeriggio
                    def = null;
                    count = rnd.Next(0, TurnistiPomeridiani.Count);
                    if (count >= TurnistiPomeridiani.Count)
                        def = TurnistiPomeridiani[0];
                    else
                        def = TurnistiPomeridiani[count];
                    foreach (turnista tn in TurnistiPomeridiani)
                    {
                        if (def != tn)
                        {
                            
                               if (def.Ore >= tn.Ore)
                                {
                                    if (tn.Shift > def.Shift)
                                        def = tn;
                                }
                        }
                    }
                    Affiancatori.Remove(def);
                    TurnistiNotturni.Remove(def);
                 
                    row.Cells[2].Value = def.Nome;
                    def.Ore += 7;
                    def.Shift = 0;
                  
                    foreach (turnista tn in Turnisti)
                    {
                        if (tn != def)
                            tn.Shift++;
                    }
                  
                    def = null;
                    if (Affiancatori.Count == 0)
                    {
                        foreach (turnista tn in Turnisti)
                        {
                            if (tn.Notturnista)
                                Affiancatori.Add(tn);
                        }
                    }
                    //Affiancamento
                    def = null;
                    count = rnd.Next(0, Affiancatori.Count);
                    if (count >= Affiancatori.Count)
                        def = Affiancatori[0];
                    else
                        def = Affiancatori[count];
                    foreach (turnista tn in Affiancatori)
                    {
                        if (def != tn)
                        {

                            if (def.Ore >= tn.Ore)
                            {
                                if (tn.Shift > def.Shift)
                                    def = tn;
                            }
                        }
                    }
                    TurnistiNotturni.Remove(def);
                    row.Cells[2].Value += "/"+def.Nome;
                    def.Ore += 7;
                    def.Shift = 0;
                  

                    foreach (turnista tn in Turnisti)
                    {
                        if (tn != def)
                            tn.Shift++;
                    }

                    def = null;
                    if (TurnistiNotturni.Count == 0)
                    {
                        foreach (turnista tn in Turnisti)
                        {
                            if (tn.Notturnista)
                                TurnistiNotturni.Add(tn);
                        }
                    }
                    //Notte
                    def = null;
                    count = rnd.Next(0, TurnistiNotturni.Count);
                    if (count >= TurnistiNotturni.Count)
                        def = TurnistiNotturni[0];
                    else
                        def = TurnistiNotturni[count];
                    foreach (turnista tn in TurnistiNotturni)
                    {
                        if (def != tn)
                        {

                            if (def.Ore >= tn.Ore)
                            {
                                if (tn.Shift > def.Shift)
                                    def = tn;
                            }
                        }
                    }
                    
                    TurnistiNotturni.Remove(def);

                    row.Cells[3].Value = def.Nome;
                    def.Ore += 10;
                    def.Shift = 0;
                   
                    foreach (turnista tn in Turnisti)
                    {
                        if (tn != def)
                            tn.Shift++;
                    }
                  
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
                    
                        DomenicantiNotturni = new List<turnista>();
                        foreach (turnista tn in Turnisti)
                        {
                            if(tn.Notturnista)
                            DomenicantiNotturni.Add(tn);
                        }
                    def = null;
                    //Mattina
                    int count = rnd.Next(0, Domenicanti.Count + 1);
                    if (count >= Domenicanti.Count)
                        def = Domenicanti[0];
                    else
                        def = Domenicanti[count];

                    foreach (turnista tn in Domenicanti)
                    {
                        if (def != tn)
                        {
                           
                                if (def.Ore >= tn.Ore)
                                {
                                    if (tn.Shift > def.Shift)
                                        def = tn;
                                }
                        }
                    }

                    DataGridViewRow row = (DataGridViewRow)panoramicaturni.Rows[0].Clone();
                    row.Cells[0].Value = DaytoSet.ToShortDateString() + " " + DaytoSet.DayOfWeek;
                    row.Cells[1].Value = def.Nome;
                    row.Cells[2].Value = def.Nome;
                    def.Ore += 12;
                    def.Shift = 0;
                    foreach (turnista tn in Domenicanti)
                    {
                        if (tn != def)
                            tn.Shift++;
                    }
                    Domenicanti.Remove(def);
                    DomenicantiNotturni.Remove(def);
                    def = null;
                    //Notte

                    count = rnd.Next(0, DomenicantiNotturni.Count + 1);
                    if (count >= DomenicantiNotturni.Count)
                        def = DomenicantiNotturni[0];
                    else
                        def = DomenicantiNotturni[count];

                    foreach (turnista tn in DomenicantiNotturni)
                    {
                        if (def != tn)
                        {
                          
                                if (def.Ore >= tn.Ore)
                                {
                                    if (tn.Shift > def.Shift)
                                        def = tn;
                                }
                        }
                    }

                    row.Cells[3].Value = def.Nome;
                    def.Ore += 12;
                    def.Shift = 0;
                    foreach (turnista tn in DomenicantiNotturni)
                    {
                        if (tn != def)
                            tn.Shift++;
                    }
                    def = null;
                    panoramicaturni.Rows.Add(row);
                    DaytoSet = DaytoSet.AddDays(1);
                }
            }

        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Turni_Load(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            if (dateTimePicker1.Value >= dateTimePicker2.Value)
                dateTimePicker2.Value = dateTimePicker1.Value.AddDays(1);
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            if (dateTimePicker2.Value <= dateTimePicker1.Value)
                dateTimePicker1.Value = dateTimePicker2.Value.AddDays(-1);
        }


    }
}
