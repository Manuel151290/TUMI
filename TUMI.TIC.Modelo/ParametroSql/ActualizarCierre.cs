using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TUMI.TIC.Modelo.ParametroSql
{
    public class ActualizarCierre
    {
        public int CodigoTicket { get; set; }
        public string AccionCierre { get; set; }
        public bool EstadoCierre { get; set; }
        public string CodigoUsuario { get; set; }
        public string UsuarioSolicitante { get; set; } // Asegúrate de que esta propiedad esté definida
    }
}
