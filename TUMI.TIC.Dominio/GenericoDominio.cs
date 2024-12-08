using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TUMI.TIC.Dominio.Contratos;
using TUMI.TIC.Modelo.Entidades;
using TUMI.TIC.Repositorio.Contratos;

namespace TUMI.TIC.Dominio
{
    public class GenericoDominio : IGenericoDominio
    {
        #region Variables
        private readonly IGenericoRepositorio _repositorio;
        #endregion

        #region Constructor
        public GenericoDominio(IGenericoRepositorio repositorio)
        {
            _repositorio = repositorio;
        }
        #endregion
        public List<SelectTipo> ObtenerSubAreaPorUnidad(string codigoUnidad)
        {
            List<SelectTipo> oLista = new List<SelectTipo>();
            oLista = _repositorio.ObtenerSubAreaPorUnidad(codigoUnidad);
            return oLista;
        }
        public List<SelectTipo> ObtenerCategoriaPorSubArea(string codigoSubArea)
        {
            List<SelectTipo> oLista = new List<SelectTipo>();
            oLista = _repositorio.ObtenerCategoriaPorSubArea(codigoSubArea);
            return oLista;
        }
        public List<SelectTipo> ObtenerSubCategoriaPorCategoria(string codigoCategoria)
        {
            List<SelectTipo> oLista = new List<SelectTipo>();
            oLista = _repositorio.ObtenerSubCategoriaPorCategoria(codigoCategoria);
            return oLista;
        }

        public List<SelectTipoEntero> ListarLugarIncidente()
        {
            List<SelectTipoEntero> oLista = new List<SelectTipoEntero>();
            oLista = _repositorio.ListarLugarIncidente();
            return oLista;
        }
        public List<SelectTipo> ListarResponsableAutorizacion()
        {
            List<SelectTipo> oLista = new List<SelectTipo>();
            oLista = _repositorio.ListarResponsableAutorizacion();
            return oLista;
        }
        public List<SelectTipoEntero> ListarTipoEvidencia()
        {
            List<SelectTipoEntero> oLista = new List<SelectTipoEntero>();
            oLista = _repositorio.ListarTipoEvidencia();
            return oLista;
        }
    }
}
