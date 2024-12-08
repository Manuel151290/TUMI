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
    public class DashBoardRepositorio : IDashBoardRepositorio
    {
        #region Variables
        private readonly Conexion _conexion;
        #endregion

        #region Constructor
        public DashBoardRepositorio(Conexion conexion)
        {
            _conexion = conexion;
        }
        #endregion

        public TotalesDashboard TraerTotales()
        {
            using (SqlConnection conexion = _conexion.ObtenerConexion())
            {
                string sp = StoredProcedures.USP_DASHBOARD_TRAERTOTALES;
                DynamicParameters parametros = new DynamicParameters();
                TotalesDashboard oDato = conexion.Query<dynamic>(sp, parametros, commandType: CommandType.StoredProcedure)
                                            .Select(item => new TotalesDashboard()
                                            {
                                                TotalTickets = item.nCanTicReg,
                                                TotalTicketsAtendidos = item.nCanTicAte,
                                                TotalTickersNoAtendidos = item.nCanTicNoA,
                                                TotalColaboradoresActivos = item.nCanColAct
                                            }).SingleOrDefault();
                return oDato;
            }
        }
    }
}
