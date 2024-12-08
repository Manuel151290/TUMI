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

namespace TUMI.TIC.Repositorio.SqlServer
{
    public class AreaRepositorio : IAreaRepositorio
    {
        #region Variables
        private readonly Conexion _conexion;
        #endregion

        #region Constructor
        public AreaRepositorio(Conexion conexion)
        {
            _conexion = conexion;
        }
        #endregion

        public List<UnidadOrganica> ListarTodo()
        {
            using (SqlConnection conexion = _conexion.ObtenerConexion())
            {
                string sp = StoredProcedures.USP_AREA_TRAERTODOS;
                DynamicParameters parametros = new DynamicParameters();
                List<UnidadOrganica> oAreas = (List<UnidadOrganica>)conexion.Query<dynamic>(sp, parametros, commandType: CommandType.StoredProcedure)
                                            .Select(item => new UnidadOrganica()
                                            {
                                                Codigo = item.cCodUniOrg,
                                                Descripcion = item.cDesUniOrg
                                            }).ToList();
                return oAreas;
            }
        }

        public List<UnidadOrganica> ListarParaIncidentes()
        {
            using (SqlConnection conexion = _conexion.ObtenerConexion())
            {
                string sp = StoredProcedures.USP_AREA_TREARPORINCIDENTE;
                DynamicParameters parametros = new DynamicParameters();
                List<UnidadOrganica> oAreas = (List<UnidadOrganica>)conexion.Query<dynamic>(sp, parametros, commandType: CommandType.StoredProcedure)
                                            .Select(item => new UnidadOrganica()
                                            {
                                                Codigo = item.cCodUniOrg,
                                                Descripcion = item.cDesUniOrg
                                            }).ToList();
                return oAreas;
            }
        }
    }
}
