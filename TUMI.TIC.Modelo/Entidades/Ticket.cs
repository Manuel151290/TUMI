using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TUMI.TIC.Modelo.Entidades
{
    public class Ticket
    {
        public int Codigo { get; set; }
        public string CodigoColaboradorRegistro { get; set; }
        public string CorreoColaboradorRegistro { get; set; }
        public string NombreColaboradorRegistro { get; set; }
        public DateTime FechaRegistro { get; set; } 
        public string CodigoUnidadOrganica { get; set; }
        public string CodigoSubUnidadOrganica { get; set; }
        public string CodigoCategoria { get; set; }
        public string CodigoSubCategoria { get; set; }
        public string Descripcion { get; set; } 
        public string NumeroCelular { get; set; }
        public int CodigoLugar { get; set; }
        public string CodigoResponsable { get; set; }
        public bool IndicadorSeguridadInformacion { get; set; }
        public bool IndicadorContinuidadNegocio { get; set; }
        public string NombreArchivo { get; set; }
        public string CodigoDerivado { get; set; }
        public string RutaArchivo { get; set; }
        public DateTime FechaFinAtencion { get; set; }
        public bool EstadoAtencion { get; set; }
    }

    public class TicketDerivado
    {
        public int CodigoTicket { get; set; }
        public DateTime FechaRegistro { get; set; }
        public string ColaboradorRegistro { get; set; }
        public string UnidadOrganica { get; set; }
        public string SubUnidadOrganica { get; set; }
        public string Categoria { get; set; }
        public string SubCategoria { get; set; }
        public string CodigoDerivado { get; set; }
    }

    public class DatoDerivacionCorreo
    {
        public string NombreDerivado { get; set; }
        public string CorreoDerivado { get; set; }
        public string NombreRegistro { get; set; }
        public string CorreoRegistro { get; set; }
    }

    public class DatosBasicos
    {
        public int Codigo { get; set; }
        public DateTime FechaRegistro { get; set; }
        public string NombreSolicitante { get; set; }
        public string CorreoSolicitante { get; set; }
        public string UnidadOrganicaSolicitante { get; set; }
        public string OficinaSolicitante { get; set; }
        public string NumeroCelularSolicitante { get; set; }
        public string LugarIncidente { get; set; }
        public string NombreAutoriza { get; set; }
        public string Descripcion { get; set; }
        public string ArchivoAdjunto { get; set; }
        public bool IndicadorSeguridadInformacion { get; set; }
        public bool IndicadorContinuidadNegocio { get; set; }
        public bool IndicadorCierre { get; set; }
        public string AccionCierre { get; set; }
    }

    public class DocumentoTicket
    {
        public int CodigoDocumento { get; set; }
        public int CodigoTipoEvidencia { get; set; }
        public string DescripcionTipoEvidencia { get; set; }
        public int CodigoTicket { get; set; }
        public string NombreArchivo { get; set; }
        public string CodigoUsuarioRegistro { get; set; }   
    }

    
}
