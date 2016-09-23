using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Bel3.Gest
{
    public partial class PrenotazioneMultipla : Form
    {
        Prenotazione pren;
        public PrenotazioneMultipla(Prenotazione p)
        {
            
            InitializeComponent();
            pren = p;
            tipologia = pren.tipologia;
            tipostanza = pren.tipostanza;
            tarforfait = pren.tarforfait;
            arrangiamento = pren.arrangiamento;
            forfait = pren.forfait;
        }

        private void PrenotazioneMultipla_Load(object sender, EventArgs e)
        {

        }

        private void tipologia_SelectedIndexChanged(object sender, EventArgs e)
        {
            pren.tipologia = tipologia;
        }

        private void tipostanza_SelectedIndexChanged(object sender, EventArgs e)
        {
            pren.tipostanza = tipostanza;
        }

        private void arrangiamento_SelectedIndexChanged(object sender, EventArgs e)
        {
            pren.arrangiamento=arrangiamento;
        }

        private void tarforfait_TextChanged(object sender, EventArgs e)
        {
            pren.tarforfait = tarforfait;
        }

        private void iva_TextChanged(object sender, EventArgs e)
        {
            pren.iva = iva;
        }

        private void forfait_CheckedChanged(object sender, EventArgs e)
        {
            pren.forfait = forfait;
        }

        private void pagata_CheckedChanged(object sender, EventArgs e)
        {
            pren.pagata = pagata;
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
