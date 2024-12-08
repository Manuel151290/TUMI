using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TUMI.TIC.Modelo.ParametroSql
{
    public class RegistroTicket
    {
        public string CorreoResponsable { get; set; }
        public string CorreoColaboradorRegistro { get; set; }
        public string NombreColaboradorRegistro { get; set; }
        public string RutaArchivo { get; set; }
        public int NumeroTicket { get; set; }
        public DateTime FechaRegistroTicket { get; set; }
        public string DescripcionTicket {get;set;}
    }
}
