using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TUMI.TIC.Data;
using TUMI.TIC.Modelo.Entidades;
using TUMI.TIC.Repositorio.Contratos;

namespace TUMI.TIC.Repositorio.SqlServer
{
    public class ColaboradorRepositorio : IColaboradorRepositorio
    {
        #region Variables
        private readonly Conexion _conexion;
        #endregion

        #region Constructor
        public ColaboradorRepositorio(Conexion conexion)
        {
            _conexion = conexion;   
        }
        #endregion

        #region Métodos
        public List<Colaborador> ListarColaboradores()
        {
            List<Colaborador> oLista = new List<Colaborador>();
            string sp = StoredProcedures.USP_COLABORADOR_TRAERTODOS;
            using (SqlConnection conexion = _conexion.ObtenerConexion())
            {
                DynamicParameters parametros = new DynamicParameters();
                oLista = (List<Colaborador>)conexion.Query<dynamic>(sp, parametros, commandType: CommandType.StoredProcedure)
                                            .Select(item => new Colaborador()
                                            {
                                                Codigo = item.cCodPerson,
                                                Nombre = item.cNomPerson,
                                                Correo = item.cCorEleIns,
                                                CodigoOficina = item.cCodOficin,
                                                DescripcionOficina = item.cDesOficin,
                                                CodigoArea = item.cCodUniOrg,
                                                DescripcionArea = item.cDesUniOrg,
                                                CodigoCargo = item.cCodGruPer,
                                                DescripcionCargo = item.cDesGruPer,
                                                ClaveCifrado = item.cClaEncPer,
                                                DocumentoIdentidad = item.cNumDocIde
                                            }).ToList();
                return oLista;
            }
        }

        public Colaborador ObtenerDatos(string codigo, string clave, out bool estado)
        {
            using (SqlConnection conexion = _conexion.ObtenerConexion())
            {
                string sp = StoredProcedures.USP_COLABORADOR_VALIDAR;
                DynamicParameters parametros = new DynamicParameters();
                parametros.Add("@x_cCodPerson", codigo);
                parametros.Add("@x_cClaEncPer", clave);
                parametros.Add("@x_lEstValCla", false, dbType: DbType.Boolean, direction: ParameterDirection.Output);
                Colaborador oColaborador = conexion.Query<dynamic>(sp, parametros, commandType: CommandType.StoredProcedure)
                                            .Select(item => new Colaborador()
                                            {
                                                Codigo = item.cCodPerson,
                                                Nombre = item.cNomPerson
                                            }).SingleOrDefault();
                estado = parametros.Get<bool>("@x_lEstValCla");
                return oColaborador;
            }
        }

        public Colaborador TraerUno(string codigo)
        {
            using (SqlConnection conexion = _conexion.ObtenerConexion())
            {
                string sp = StoredProcedures.USP_COLABORADOR_TRAERUNO;
                DynamicParameters parametros = new DynamicParameters();
                parametros.Add("@x_cCodPerson", codigo);
                Colaborador oColaborador = conexion.Query<dynamic>(sp, parametros, commandType: CommandType.StoredProcedure)
                                            .Select(item => new Colaborador()
                                            {
                                                Codigo = item.cCodPerson,
                                                Nombre = item.cNomPerson,
                                                Correo = item.cCorEleIns,
                                                CodigoOficina = item.cCodOficin,
                                                DescripcionOficina = item.cDesOficin,
                                                CodigoArea = item.cCodUniOrg,
                                                DescripcionArea = item.cDesUniOrg,
                                                CodigoCargo = item.cCodGruPer,
                                                DescripcionCargo = item.cDesGruPer,
                                                FechaNacimiento = item.dFecNacPer,
                                                DocumentoIdentidad = item.cNumDocIde
                                            }).SingleOrDefault();
                return oColaborador;
            }
        }

        public bool ActualizarColaborador(Colaborador oColaborador)
        {
            using (SqlConnection conexion = _conexion.ObtenerConexion())
            {
                string sp = StoredProcedures.USP_COLABORADOR_ACTUALIZAR;
                DynamicParameters parametros = new DynamicParameters();
                parametros.Add("@x_cCodPerson", oColaborador.Codigo);
                parametros.Add("@x_cNumDocIde", oColaborador.DocumentoIdentidad);
                parametros.Add("@x_cNomPerson", oColaborador.Nombre);
                parametros.Add("@x_cCorEleIns", oColaborador.Correo);
                parametros.Add("@x_dFecNacPer", oColaborador.FechaNacimiento);
                parametros.Add("@x_cCodOficin", oColaborador.CodigoOficina);
                parametros.Add("@x_cCodUniOrg", oColaborador.CodigoArea);
                parametros.Add("@x_cCodGruPer", oColaborador.CodigoCargo);
                int cantidadFilasAfectadas = conexion.Execute(sp, parametros, commandType: CommandType.StoredProcedure); ;
                return (cantidadFilasAfectadas >= 0) ? true : false;
            }
        }

        public bool ActualizarClave(string codigo, string clave)
        {
            using (SqlConnection conexion = _conexion.ObtenerConexion())
            {
                string sp = StoredProcedures.USP_COLABORADOR_ACTUALIZARCLAVE;
                DynamicParameters parametros = new DynamicParameters();
                parametros.Add("@x_cCodPerson", codigo);
                parametros.Add("@x_cClaEncPer", clave);
                
                int cantidadFilasAfectadas = conexion.Execute(sp, parametros, commandType: CommandType.StoredProcedure); ;
                return true;
            }
        }

        public bool RegistrarColaborador(Colaborador oColaborador)
        {
            using (SqlConnection conexion = _conexion.ObtenerConexion())
            {
                string sp = StoredProcedures.USP_COLABORADOR_REGISTRAR;
                DynamicParameters parametros = new DynamicParameters();
                parametros.Add("@x_cCodPerson", oColaborador.Codigo);
                parametros.Add("@x_cNumDocIde", oColaborador.DocumentoIdentidad);
                parametros.Add("@x_cNomPerson", oColaborador.Nombre);
                parametros.Add("@x_cCorEleIns", oColaborador.Correo);
                parametros.Add("@x_dFecNacPer", oColaborador.FechaNacimiento);
                parametros.Add("@x_cCodOficin", oColaborador.CodigoOficina);
                parametros.Add("@x_cCodUniOrg", oColaborador.CodigoArea);
                parametros.Add("@x_cCodGruPer", oColaborador.CodigoCargo);
                parametros.Add("@x_cClaEncPer", oColaborador.ClaveCifrado);
                int cantidadFilasAfectadas = conexion.Execute(sp, parametros, commandType: CommandType.StoredProcedure); ;
                return (cantidadFilasAfectadas >= 0) ? true : false;
            }
        }
        #endregion
    }
}
