using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TUMI.TIC.ClienteWeb.Models.ReporteTicket;
using TUMI.TIC.Dominio.Contratos;
using TUMI.TIC.Modelo.Entidades;
using TUMI.TIC.Modelo.ParametroSql;
using TUMI.TIC.Repositorio.Contratos;

namespace TUMI.TIC.Dominio
{
    public class TicketDominio : ITicketDominio
    {
        #region Variables
        private readonly ITicketRepositorio _repositorio;
        #endregion

        #region Constructor
        public TicketDominio(ITicketRepositorio repositorio)
        {
            _repositorio = repositorio;
        }
        #endregion
        public bool RegistrarTicket(Ticket oTicket, out int codigoTicket)
        {
            bool resultado = false;
            resultado = _repositorio.RegistrarTicket(oTicket, out codigoTicket);
            return resultado;
        }

        public List<TicketDerivado> oListaticketDerivado()
        {
            List<TicketDerivado> oLista = new List<TicketDerivado>() ;
            oLista = _repositorio.oListaticketDerivado();
            return oLista;
        }

        public DatoDerivacionCorreo DatosDerivacion(int codigoTicket, string codigoDerivado)
        {
            DatoDerivacionCorreo oDatos = new DatoDerivacionCorreo();
            oDatos = _repositorio.DatosDerivacion(codigoTicket, codigoDerivado);
            return oDatos;
        }

        public bool Derivarticket(int codigoTicket, string codigoDerivado, string codigoModifica)
        {
            bool resultado = false;
            resultado = _repositorio.Derivarticket(codigoTicket, codigoDerivado, codigoModifica);
            return resultado;
        }
        public List<TicketDerivado> ListarMisTickets(string codigo)
        {
            List<TicketDerivado> oLista = new List<TicketDerivado>();
            oLista = _repositorio.ListarMisTickets(codigo);
            return oLista;
        }

        public DatosBasicos TraerDatosBasicos(int codigo)
        {
            DatosBasicos oDatos = new DatosBasicos();
            oDatos  = _repositorio.TraerDatosBasicos(codigo);
            return oDatos;
        }

        public List<DocumentoTicket> TraerDocumentos(int codigo)
        {
            List<DocumentoTicket> oLista = new List<DocumentoTicket>();
            oLista = _repositorio.TraerDocumentos(codigo);
            return oLista;
        }

        public bool InsertarDocumento(DocumentoTicket documento)
        {
            bool resultado = false;
            resultado = _repositorio.InsertarDocumento(documento);
            return resultado;
        }
        public bool EliminarDocumento(int codigo, string codigoUsuario)
        {
            bool resultado = false;
            resultado = _repositorio.EliminarDocumento(codigo, codigoUsuario);
            return resultado;
        }
        public bool ActualizarCierre(ActualizarCierre oRegistro)
        {
            bool resultado = false;
            resultado = _repositorio.ActualizarCierre(oRegistro);
            return resultado;
        }
        public bool GuardarCalificacion(int codigoTicket, int calificacion)
        {
            return _repositorio.GuardarCalificacion(codigoTicket, calificacion);
        }
        //Reportes
        public List<ReporteTiket> ReportePorUnidadOrganica()
        {
            List<ReporteTiket> oLista = new List<ReporteTiket>();
            oLista = _repositorio.ReportePorUnidadOrganica();
            return oLista;
        }
        public List<ReportePorFechas> ReportePorFechas(DateTime fechaInicial, DateTime fechaFinal)
        {
            List<ReportePorFechas> oLista = new List<ReportePorFechas>();
            oLista = _repositorio.ReportePorFechas(fechaInicial, fechaFinal);
            return oLista;
        }
        public List<ReporteCalificaciones> ObtenerReporteCalificaciones(DateTime fechaInicial, DateTime fechaFinal)
        {
            List<ReporteCalificaciones> oLista = new List<ReporteCalificaciones>();
            oLista = _repositorio.ObtenerReporteCalificaciones(fechaInicial, fechaFinal);
            return oLista;
        }
    }
}
