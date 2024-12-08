using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TUMI.TIC.Modelo.Entidades
{
    public class TotalesDashboard
    {
        public int TotalTickets { get; set; }
        public int TotalTicketsAtendidos { get; set; }
        public int TotalTickersNoAtendidos { get; set; } 
        public int TotalColaboradoresActivos { get; set; }
    }
}
