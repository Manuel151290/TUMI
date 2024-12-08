using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TUMI.TIC.Repositorio.SqlServer
{
    public class StoredProcedures
    {
        #region Colaborador
        public const string USP_COLABORADOR_VALIDAR = "TIC_ValidarPersonal_SP";
        public const string USP_COLABORADOR_TRAERTODOS = "TIC_ObtenerColaboradoresActivos_SP";
        public const string USP_COLABORADOR_TRAERUNO = "TIC_ObtenerDatoColaborador_SP";
        public const string USP_COLABORADOR_ACTUALIZAR = "TIC_ActualizarColaborador_SP";
        public const string USP_COLABORADOR_REGISTRAR = "TIC_InsertarColaborador_SP";
        public const string USP_COLABORADOR_ACTUALIZARCLAVE = "TIC_ActualizarClaveColaborador_SP";
        #endregion

        #region Oficina
        public const string USP_OFICINA_TRAERTODOS = "TIC_ObtenerOficinas_SP";
        #endregion

        #region Cargo
        public const string USP_CARGO_TRAERTODOS = "TIC_ObtenerCargos_SP";
        #endregion

        #region Area
        public const string USP_AREA_TRAERTODOS = "TIC_ObtenerAreas_SP";
        public const string USP_AREA_TREARPORINCIDENTE = "TIC_ObtenerAreasIncidentes_SP";
        #endregion

        #region Generico
        public const string USP_GENERICO_TRAERSUBAREA = "TIC_ObtenerSubUnidad_SP";
        public const string USP_GENERICO_TRAERCATEGORIA = "TIC_ObtenerCategoria_SP";
        public const string USP_GENERICO_TRAERSUBCATEGORIA = "TIC_ObtenerSubCategoria_SP";
        public const string USP_GENERICO_TRAERLUGARINCIDENTE = "TIC_ListarLugarIncidente_SP";
        public const string USP_GENERICO_TRAERRESPONSABLEAUTORIZACION = "TIC_ListarResponsables_SP";
        public const string USP_GENERICO_TRAERTIPOEVIDENCIA = "TIC_ListarTipoEvidencia_SP";
        #endregion

        #region Ticket
        public const string USP_TICKET_INSERTAR = "TIC_RegistrarTicket_SP";
        public const string USP_TICKET_LISTARNODERIVADOS = "TIC_ListarTicketNoDerivados_SP";
        public const string USP_TICKET_LISTARDATOSDERIVADO = "TIC_ListarDatosColaboradorDerivado_SP";
        public const string USP_TICKET_DERIVARTICKET = "TIC_DerivarTicket_SP";
        public const string USP_TICKET_LISTAMISTICKETS = "TIC_ListarMisTickets_SP";
        public const string USP_TICKET_OBTENERDATOSBASICOS = "TIC_ObtenerDatosBasicosTicket_SP";
        public const string USP_TICKET_LISTARDOCUMENTOS = "TIC_ListarDocumentosTicket_SP";
        public const string USP_TICKET_ELIMINARDOCUMENTO = "TIC_EliminarDocumentoTicket_SP";
        public const string USP_TICKET_INSERTARDOCUMENTO = "TIC_InsertarDocumentoTicket_SP";
        public const string USP_TICKET_ACTUALIZARCIERRE =  "TIC_ActualizaCierreTicket_SP";
        public const string USP_TICKET_REPORTEUNIDADORGANICA = "TIC_ReportePorUnidadOrganica_SP";
        public const string USP_TICKET_REPORTEPORFECHAS = "TIC_ReportePorFechas_SP";
        public const string USP_TICKET_REPORTEPORCALIFICACIONES = "USP_TICKET_OBTENERREPORTECALIFICACIONES";
        #endregion

        #region DashBoard
        public const string USP_DASHBOARD_TRAERTOTALES = "TIC_DatosDashboardPrincipal_SP";
        #endregion
    }
}
