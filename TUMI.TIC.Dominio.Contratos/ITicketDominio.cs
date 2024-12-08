using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TUMI.TIC.ClienteWeb.Models.ReporteTicket;
using TUMI.TIC.Modelo.Entidades;
using TUMI.TIC.Modelo.ParametroSql;

namespace TUMI.TIC.Dominio.Contratos
{
    public interface ITicketDominio
    {
        bool RegistrarTicket(Ticket oTicket, out int codigoTicket);
        List<TicketDerivado> oListaticketDerivado();
        DatoDerivacionCorreo DatosDerivacion(int codigoTicket, string codigoDerivado);
        bool Derivarticket(int codigoTicket, string codigoDerivado, string codigoModifica);
        List<TicketDerivado> ListarMisTickets(string codigo);
        DatosBasicos TraerDatosBasicos(int codigo);
        List<DocumentoTicket> TraerDocumentos(int codigo);
        bool InsertarDocumento(DocumentoTicket documento);
        bool EliminarDocumento(int codigo, string codigoUsuario);
        bool GuardarCalificacion(int codigoTicket, int calificacion);
        bool ActualizarCierre(ActualizarCierre oRegistro);
        //Reportes
        List<ReporteTiket> ReportePorUnidadOrganica();
        List<ReportePorFechas> ReportePorFechas(DateTime fechaInicial, DateTime fechaFinal);
        List<ReporteCalificaciones> ObtenerReporteCalificaciones(DateTime fechaInicial, DateTime fechaFinal);
    }
}
