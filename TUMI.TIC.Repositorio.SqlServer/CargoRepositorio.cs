using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TUMI.TIC.Data;
using TUMI.TIC.Modelo.Entidades;
using TUMI.TIC.Repositorio.Contratos;

namespace TUMI.TIC.Repositorio.SqlServer
{
    public class CargoRepositorio : ICargoRepositorio
    {
        #region Variables
        private readonly Conexion _conexion;
        #endregion

        #region Constructor
        public CargoRepositorio(Conexion conexion)
        {
            _conexion = conexion;
        }
        #endregion

        public List<Cargo> ListarTodo()
        {
            using (SqlConnection conexion = _conexion.ObtenerConexion())
            {
                string sp = StoredProcedures.USP_CARGO_TRAERTODOS;
                DynamicParameters parametros = new DynamicParameters();
                List<Cargo> oCargo = (List<Cargo>)conexion.Query<dynamic>(sp, parametros, commandType: CommandType.StoredProcedure)
                                            .Select(item => new Cargo()
                                            {
                                                Codigo = item.cCodGruPer,
                                                Descripcion = item.cDesGruPer
                                            }).ToList();
                return oCargo;
            }
        }
    }
}
