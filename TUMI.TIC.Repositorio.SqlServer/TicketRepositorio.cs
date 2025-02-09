using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TUMI.TIC.Data;
using TUMI.TIC.Modelo.Entidades;
using TUMI.TIC.Repositorio.Contratos;
using System.Security.Claims;
using System.Xml.Linq;
using TUMI.TIC.Modelo.ParametroSql;
using TUMI.TIC.ClienteWeb.Models.ReporteTicket;

namespace TUMI.TIC.Repositorio.SqlServer
{
    public class TicketRepositorio: ITicketRepositorio
    {
        #region Variables
        private readonly Conexion _conexion;
        #endregion

        #region Constructor
        public TicketRepositorio(Conexion conexion)
        {
            _conexion = conexion;
        }
        #endregion

        #region Métodos
        public bool RegistrarTicket(Ticket oTicket, out int codigoTicket)
        {
            using (SqlConnection conexion = _conexion.ObtenerConexion())
            {
                string sp = StoredProcedures.USP_TICKET_INSERTAR;
                DynamicParameters parametros = new DynamicParameters();
                parametros.Add("@x_cCorPerReg", oTicket.CorreoColaboradorRegistro);
                parametros.Add("@x_cCodUniOrg", oTicket.CodigoUnidadOrganica);
                parametros.Add("@x_cCodSubUni", oTicket.CodigoSubUnidadOrganica);
                parametros.Add("@x_cCodCatAre", oTicket.CodigoCategoria);
                parametros.Add("@x_cCodSubCat", oTicket.CodigoSubCategoria);
                parametros.Add("@x_cDesTicket", oTicket.Descripcion);
                parametros.Add("@x_cNumCelReg", oTicket.NumeroCelular);
                parametros.Add("@x_nCodLugInc", oTicket.CodigoLugar);
                parametros.Add("@x_cCodResAte", oTicket.CodigoResponsable);
                parametros.Add("@x_lIndSegInf", oTicket.IndicadorSeguridadInformacion);
                parametros.Add("@x_lIndConNeg", oTicket.IndicadorContinuidadNegocio);
                parametros.Add("@x_cNomArcAdj", oTicket.NombreArchivo);
                parametros.Add("@x_cCodUsuReg", oTicket.CodigoColaboradorRegistro);
                parametros.Add("@x_nCodTicket", false, dbType: DbType.Int32, direction: ParameterDirection.Output);
                
                int cantidadFilasAfectadas = conexion.Execute(sp, parametros, commandType: CommandType.StoredProcedure);
                codigoTicket = parametros.Get<int>("@x_nCodTicket");
                return true;
            }
        }
        public List<TicketDerivado> oListaticketDerivado()
        {
            List<TicketDerivado> oLista = new List<TicketDerivado>();
            string sp = StoredProcedures.USP_TICKET_LISTARNODERIVADOS;
            using (SqlConnection conexion = _conexion.ObtenerConexion())
            {
                DynamicParameters parametros = new DynamicParameters();
                oLista = (List<TicketDerivado>)conexion.Query<dynamic>(sp, parametros, commandType: CommandType.StoredProcedure)
                                            .Select(item => new TicketDerivado()
                                            {
                                                CodigoTicket = item.nCodTicket,
                                                ColaboradorRegistro = item.cNomPerson,
                                                UnidadOrganica = item.cDesUniOrg,
                                                FechaRegistro = item.dFecUsuReg,
                                                SubUnidadOrganica = item.cDesSubUni,
                                                Categoria = item.cDesCatAre,
                                                SubCategoria = item.cDesSubCat,
                                                CodigoDerivado = item.cCodDerTic
                                            }).ToList();
                return oLista;
            }
        }

        public DatoDerivacionCorreo DatosDerivacion(int codigoTicket, string codigoDerivado)
        {
            using (SqlConnection conexion = _conexion.ObtenerConexion())
            {
                string sp = StoredProcedures.USP_TICKET_LISTARDATOSDERIVADO;
                DynamicParameters parametros = new DynamicParameters();
                parametros.Add("@x_nCodTicket", codigoTicket);
                parametros.Add("@x_cCodPerson", codigoDerivado);
                DatoDerivacionCorreo oDatos = conexion.Query<dynamic>(sp, parametros, commandType: CommandType.StoredProcedure)
                                            .Select(item => new DatoDerivacionCorreo()
                                            {
                                                NombreDerivado = item.cNomPerson,
                                                CorreoDerivado = item.cCorEleIns,
                                                NombreRegistro = item.cNomPerReg,
                                                CorreoRegistro = item.cCorPerReg
                                            }).SingleOrDefault();
                return oDatos;
            }
        }

        public bool Derivarticket(int codigoTicket, string codigoDerivado, string codigoModifica)
        {
            using (SqlConnection conexion = _conexion.ObtenerConexion())
            {
                string sp = StoredProcedures.USP_TICKET_DERIVARTICKET;
                DynamicParameters parametros = new DynamicParameters();
                parametros.Add("@x_nCodTicket", codigoTicket);
                parametros.Add("@x_cCodPerson", codigoDerivado);
                parametros.Add("@x_cCodUsuMod", codigoModifica);

                int cantidadFilasAfectadas = conexion.Execute(sp, parametros, commandType: CommandType.StoredProcedure);
                return true;
            }
        }
        public List<TicketDerivado> ListarMisTickets(string codigo)
        {
            List<TicketDerivado> oLista = new List<TicketDerivado>();
            string sp = StoredProcedures.USP_TICKET_LISTAMISTICKETS;
            using (SqlConnection conexion = _conexion.ObtenerConexion())
            {
                DynamicParameters parametros = new DynamicParameters();
                parametros.Add("@x_cCodPerson", codigo);

                oLista = (List<TicketDerivado>)conexion.Query<dynamic>(sp, parametros, commandType: CommandType.StoredProcedure)
                                            .Select(item => new TicketDerivado()
                                            {
                                                CodigoTicket = item.nCodTicket,
                                                ColaboradorRegistro = item.cNomPerson,
                                                UnidadOrganica = item.cDesUniOrg,
                                                FechaRegistro = item.dFecUsuReg,
                                                SubUnidadOrganica = item.cDesSubUni,
                                                Categoria = item.cDesCatAre,
                                                SubCategoria = item.cDesSubCat,
                                                CodigoDerivado = item.cCodDerTic
                                            }).ToList();
                return oLista;
            }
        }

        public DatosBasicos TraerDatosBasicos(int codigo)
        {
            using (SqlConnection conexion = _conexion.ObtenerConexion())
            {
                string sp = StoredProcedures.USP_TICKET_OBTENERDATOSBASICOS;
                DynamicParameters parametros = new DynamicParameters();
                parametros.Add("@x_nCodTicket", codigo);
                DatosBasicos oDatos = conexion.Query<dynamic>(sp, parametros, commandType: CommandType.StoredProcedure)
                                            .Select(item => new DatosBasicos()
                                            {
                                                Codigo = item.nCodTicket,
                                                FechaRegistro = item.dFecUsuReg,
                                                NombreSolicitante = item.cNomPerSol,
                                                CorreoSolicitante = item.cCorPerReg,
                                                UnidadOrganicaSolicitante = item.cDesUniOrg,
                                                OficinaSolicitante = item.cDesOficin,
                                                NumeroCelularSolicitante = item.cNumCelReg,
                                                LugarIncidente = item.cDesLugInc,
                                                NombreAutoriza = item.cNomPerAut,
                                                Descripcion = item.cDesTicket,
                                                ArchivoAdjunto = item.cNomArcAdj,
                                                IndicadorSeguridadInformacion = item.lIndSegInf,
                                                IndicadorContinuidadNegocio = item.lIndConNeg,
                                                AccionCierre = item.cAccCieTic,
                                                IndicadorCierre = item.lEstAteTic,
                                            }).SingleOrDefault();
                return oDatos;
            }
        }

        public List<DocumentoTicket> TraerDocumentos(int codigo)
        {
            List<DocumentoTicket> oLista = new List<DocumentoTicket>();
            string sp = StoredProcedures.USP_TICKET_LISTARDOCUMENTOS;
            using (SqlConnection conexion = _conexion.ObtenerConexion())
            {
                DynamicParameters parametros = new DynamicParameters();
                parametros.Add("@x_nCodTicket", codigo);

                oLista = (List<DocumentoTicket>)conexion.Query<dynamic>(sp, parametros, commandType: CommandType.StoredProcedure)
                                            .Select(item => new DocumentoTicket()
                                            {
                                                CodigoDocumento = item.nCodDocTic,
                                                DescripcionTipoEvidencia = item.cDesTipEvi,
                                                NombreArchivo = item.cNomArcTic,
                                            }).ToList();
                return oLista;
            }
        }

        public bool InsertarDocumento(DocumentoTicket documento)
        {
            using (SqlConnection conexion = _conexion.ObtenerConexion())
            {
                string sp = StoredProcedures.USP_TICKET_INSERTARDOCUMENTO;
                DynamicParameters parametros = new DynamicParameters();
                parametros.Add("@x_nCodTipEvi", documento.CodigoTipoEvidencia);
                parametros.Add("@x_nCodTicket", documento.CodigoTicket);
                parametros.Add("@x_cNomArcTic", documento.NombreArchivo);
                parametros.Add("@x_cCodUsuReg", documento.CodigoUsuarioRegistro);

                int cantidadFilasAfectadas = conexion.Execute(sp, parametros, commandType: CommandType.StoredProcedure);
                return true;
            }
        }
        public bool EliminarDocumento(int codigo, string codigoUsuario)
        {
            using (SqlConnection conexion = _conexion.ObtenerConexion())
            {
                
                string sp = StoredProcedures.USP_TICKET_ELIMINARDOCUMENTO;
                DynamicParameters parametros = new DynamicParameters();
                parametros.Add("@x_nCodDocTic", codigo);
                parametros.Add("@x_cCodUsuMod", codigoUsuario);

                int cantidadFilasAfectadas = conexion.Execute(sp, parametros, commandType: CommandType.StoredProcedure);
                return true;
            }
        }

        public bool ActualizarCierre(ActualizarCierre oRegistro)
        {
            using (SqlConnection conexion = _conexion.ObtenerConexion())
            {

                string sp = StoredProcedures.USP_TICKET_ACTUALIZARCIERRE;
                DynamicParameters parametros = new DynamicParameters();
                parametros.Add("@x_nCodTicket", oRegistro.CodigoTicket);
                parametros.Add("@x_cAccCieTic", oRegistro.AccionCierre);
                parametros.Add("@x_lEstAteTic", oRegistro.EstadoCierre);
                parametros.Add("@x_cCodUsuMod", oRegistro.CodigoUsuario);

                int cantidadFilasAfectadas = conexion.Execute(sp, parametros, commandType: CommandType.StoredProcedure);
                return true;
            }
        }

        public bool GuardarCalificacion(int codigoTicket, int calificacion)
        {
            using (SqlConnection conexion = _conexion.ObtenerConexion())
            {
                string sp = "USP_TICKET_GUARDARCALIFICACION"; 
                DynamicParameters parametros = new DynamicParameters();
                parametros.Add("@x_nCodTicket", codigoTicket);
                parametros.Add("@x_nCalificacion", calificacion);

                int cantidadFilasAfectadas = conexion.Execute(sp, parametros, commandType: CommandType.StoredProcedure);
                return cantidadFilasAfectadas > 0;
            }
        }

        //Reportes
        public List<ReporteTiket> ReportePorUnidadOrganica()
        {
            List<ReporteTiket> oLista = new List<ReporteTiket>();
            string sp = StoredProcedures.USP_TICKET_REPORTEUNIDADORGANICA;
            using (SqlConnection conexion = _conexion.ObtenerConexion())
            {
                DynamicParameters parametros = new DynamicParameters();
                oLista = (List<ReporteTiket>)conexion.Query<dynamic>(sp, parametros, commandType: CommandType.StoredProcedure)
                                            .Select(item => new ReporteTiket()
                                            {
                                                AnioRegistro = item.nAniPerReg,
                                                CodigoUnidadOrganica = item.cCodUniOrg,
                                                UnidadOrganica = item.cDesUniOrg,
                                                TotalTickets = item.nTotTicket,
                                                TotalAtendidos = item.nTotTicAte,
                                                TotalNoAtendidos = item.nTotTicNoA
                                            }).ToList();
                return oLista;
            }
        }

        public List<ReportePorFechas> ReportePorFechas(DateTime fechaInicial, DateTime fechaFinal)
        {
            List<ReportePorFechas> oLista = new List<ReportePorFechas>();
            string sp = StoredProcedures.USP_TICKET_REPORTEPORFECHAS;
            using (SqlConnection conexion = _conexion.ObtenerConexion())
            {
                DynamicParameters parametros = new DynamicParameters();
                parametros.Add("@x_dFecIniRan", fechaInicial);
                parametros.Add("@x_dFecFinRan", fechaFinal);
                oLista = (List<ReportePorFechas>)conexion.Query<dynamic>(sp, parametros, commandType: CommandType.StoredProcedure)
                                            .Select(item => new ReportePorFechas()
                                            {
                                                CodigoTicket = item.nCodTicket,
                                                Colaborador = item.cNomPerson,
                                                UnidadOrganica = item.cDesUniOrg,
                                                DescripcionTicket = item.cDesTicket,
                                                FechaRegistro = item.dFecUsuReg,
                                                EstadoTicket = item.cEstTicket,
                                                ColaboradorAtendio = item.cNomPerAte
                                            }).ToList();
                return oLista;
            }
        }
        string prototo= "";
        public List<ReporteCalificaciones> ObtenerReporteCalificaciones(DateTime fechaInicial, DateTime fechaFinal)
        {
            List<ReporteCalificaciones> oLista = new List<ReporteCalificaciones>();
            string sp = StoredProcedures.USP_TICKET_REPORTEPORCALIFICACIONES;
            using (SqlConnection conexion = _conexion.ObtenerConexion())
            {
                DynamicParameters parametros = new DynamicParameters();
                parametros.Add("@FechaInicial", fechaInicial);
                parametros.Add("@FechaFinal", fechaFinal);
                oLista = (List<ReporteCalificaciones>)conexion.Query<dynamic>(sp, parametros, commandType: CommandType.StoredProcedure)
                                            .Select(item => new ReporteCalificaciones()
                                            {
                                                Nivel = item.Nivel,
                                                Cantidad = item.Cantidad
                                            }).ToList();
                return oLista;
            }
        }
        #endregion
    }
}

