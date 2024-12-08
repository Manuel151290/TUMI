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
    public class GenericoRepositorio : IGenericoRepositorio
    {
        #region Variables
        private readonly Conexion _conexion;
        #endregion

        #region Constructor
        public GenericoRepositorio(Conexion conexion)
        {
            _conexion = conexion;
        }
        #endregion

        public List<SelectTipo> ObtenerSubAreaPorUnidad(string codigoUnidad)
        {
            using (SqlConnection conexion = _conexion.ObtenerConexion())
            {
                string sp = StoredProcedures.USP_GENERICO_TRAERSUBAREA;
                DynamicParameters parametros = new DynamicParameters();
                parametros.Add("@x_cCodUniOrg", codigoUnidad);
                List<SelectTipo> oLista = (List<SelectTipo>)conexion.Query<dynamic>(sp, parametros, commandType: CommandType.StoredProcedure)
                                            .Select(item => new SelectTipo()
                                            {
                                                Codigo = item.cCodSubUni,
                                                Descripcion = item.cDesSubUni
                                            }).ToList();
                return oLista;
            }
        }
        public List<SelectTipo> ObtenerCategoriaPorSubArea(string codigoSubArea)
        {
            using (SqlConnection conexion = _conexion.ObtenerConexion())
            {
                string sp = StoredProcedures.USP_GENERICO_TRAERCATEGORIA;
                DynamicParameters parametros = new DynamicParameters();
                parametros.Add("@x_cCodSubUni", codigoSubArea);
                List<SelectTipo> oLista = (List<SelectTipo>)conexion.Query<dynamic>(sp, parametros, commandType: CommandType.StoredProcedure)
                                            .Select(item => new SelectTipo()
                                            {
                                                Codigo = item.cCodCatAre,
                                                Descripcion = item.cDesCatAre
                                            }).ToList();
                return oLista;
            }
        }
        public List<SelectTipo> ObtenerSubCategoriaPorCategoria(string codigoCategoria)
        {
            using (SqlConnection conexion = _conexion.ObtenerConexion())
            {
                string sp = StoredProcedures.USP_GENERICO_TRAERSUBCATEGORIA;
                DynamicParameters parametros = new DynamicParameters();
                parametros.Add("@x_cCodCatAre", codigoCategoria);
                List<SelectTipo> oLista = (List<SelectTipo>)conexion.Query<dynamic>(sp, parametros, commandType: CommandType.StoredProcedure)
                                            .Select(item => new SelectTipo()
                                            {
                                                Codigo = item.cCodSubCat,
                                                Descripcion = item.cDesSubCat
                                            }).ToList();
                return oLista;
            }
        }

        public List<SelectTipoEntero> ListarLugarIncidente()
        {
            using (SqlConnection conexion = _conexion.ObtenerConexion())
            {
                string sp = StoredProcedures.USP_GENERICO_TRAERLUGARINCIDENTE;
                DynamicParameters parametros = new DynamicParameters();
                List<SelectTipoEntero> oLista = (List<SelectTipoEntero>)conexion.Query<dynamic>(sp, parametros, commandType: CommandType.StoredProcedure)
                                            .Select(item => new SelectTipoEntero()
                                            {
                                                Codigo = item.nCodLugInc,
                                                Descripcion = item.cDesLugInc
                                            }).ToList();
                return oLista;
            }
        }

        public List<SelectTipo> ListarResponsableAutorizacion()
        {
            using (SqlConnection conexion = _conexion.ObtenerConexion())
            {
                string sp = StoredProcedures.USP_GENERICO_TRAERRESPONSABLEAUTORIZACION;
                DynamicParameters parametros = new DynamicParameters();
                List<SelectTipo> oLista = (List<SelectTipo>)conexion.Query<dynamic>(sp, parametros, commandType: CommandType.StoredProcedure)
                                            .Select(item => new SelectTipo()
                                            {
                                                Codigo = item.cCodPerson,
                                                Descripcion = item.cNomPerson
                                            }).ToList();
                return oLista;
            }
        }

        public List<SelectTipoEntero> ListarTipoEvidencia()
        {
            using (SqlConnection conexion = _conexion.ObtenerConexion())
            {
                string sp = StoredProcedures.USP_GENERICO_TRAERTIPOEVIDENCIA;
                DynamicParameters parametros = new DynamicParameters();
                List<SelectTipoEntero> oLista = (List<SelectTipoEntero>)conexion.Query<dynamic>(sp, parametros, commandType: CommandType.StoredProcedure)
                                            .Select(item => new SelectTipoEntero()
                                            {
                                                Codigo = item.nCodTipEvi,
                                                Descripcion = item.cDesTipEvi
                                            }).ToList();
                return oLista;
            }
        }
    }
}
