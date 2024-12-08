using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TUMI.TIC.Modelo.Entidades
{
    public class ReporteTiket
    {
        public int AnioRegistro {  get; set; }
        public string CodigoUnidadOrganica { get; set; }
        public string UnidadOrganica { get; set; }
        public int TotalTickets { get; set; }
        public int TotalAtendidos { get; set; }
        public int TotalNoAtendidos { get; set; }
    }
    public class ReportePorFechas
    {
        public int CodigoTicket { get; set; }
        public string Colaborador {  get; set; }
        public string UnidadOrganica { get; set; }
        public string DescripcionTicket { get; set; }
        public string EstadoTicket { get; set; }
        public DateTime FechaRegistro { get; set; }
        public string ColaboradorAtendio {  get; set; } 
    }
}
