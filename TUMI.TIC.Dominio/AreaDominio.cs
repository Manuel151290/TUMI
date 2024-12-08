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

    public class AreaDominio : IAreaDominio
    {
        #region Variables
        private readonly IAreaRepositorio _repositorio;
        #endregion

        #region Constructor
        public AreaDominio(IAreaRepositorio repositorio)
        {
            _repositorio = repositorio;
        }
        #endregion
        public List<UnidadOrganica> ListarTodo()
        {
            List<UnidadOrganica> oLista = new List<UnidadOrganica>();
            oLista = _repositorio.ListarTodo();
            return oLista;
        }

        public List<UnidadOrganica> ListarParaIncidentes()
        {
            List<UnidadOrganica> oLista = new List<UnidadOrganica>();
            oLista = _repositorio.ListarParaIncidentes();
            return oLista;
        }
        
    }
}
