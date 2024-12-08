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
    public class OficinaRepositorio :IOficinaRepositorio
    {
        #region Variables
        private readonly Conexion _conexion;
        #endregion

        #region Constructor
        public OficinaRepositorio(Conexion conexion)
        {
            _conexion = conexion;
        }
        #endregion
        public List<Oficina> ListarTodo()
        {
            using (SqlConnection conexion = _conexion.ObtenerConexion())
            {
                string sp = StoredProcedures.USP_OFICINA_TRAERTODOS;
                DynamicParameters parametros = new DynamicParameters();
                List<Oficina> oOficinas = (List<Oficina>)conexion.Query<dynamic>(sp, parametros, commandType: CommandType.StoredProcedure)
                                            .Select(item => new Oficina()
                                            {
                                                Codigo = item.cCodOficin,
                                                Descripcion = item.cDesOficin
                                            }).ToList();
                return oOficinas;
            }
        }
    }
}
