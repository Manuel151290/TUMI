using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TUMI.TIC.Modelo.Entidades
{
    [Serializable]
    public class Colaborador
    {
        public string Codigo { get;set; }
        public string Nombre { get; set; }
        public string DocumentoIdentidad { get; set; }  
        public DateTime FechaNacimiento { get; set; }
        public string Correo { get; set; }
        public string CodigoOficina { get;set; }
        public string DescripcionOficina { get; set; }  
        public string CodigoArea { get; set; }  
        public string DescripcionArea { get; set; } 
        public string CodigoCargo { get; set; }
        public string DescripcionCargo { get;set; }
        public string ClaveCifrado { get; set; }
    }
}
