using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Bel3
{
    public class turnista
    {
        public int Id = 0;
        public string Nome = "";
        public int Ore = 0;
        public bool Notturnista = false;
        public int Shift = 0;
        public int turni=5;
        public turnista(int id, string nome, bool notturn)
        {
            Id = id;
            Nome = nome;
            Notturnista = notturn;
        }
    }
    public class fontiprenotazione
    {
        public string nome;
        public Color color;
        public fontiprenotazione(string n, Color c)
        {
            nome = n;
            color = c;
        }
    }
    public class CheckIn
    {
        public int pren;
        public string room;
        public CheckIn(int idpren, string camera)
        {
            pren = idpren;
            room = camera;
        }
    }
}
