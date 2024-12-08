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
    public class OficinaDominio : IOficinaDominio
    {
        #region Variables
        private readonly IOficinaRepositorio _repositorio;
        #endregion

        #region Constructor
        public OficinaDominio(IOficinaRepositorio repositorio)
        {
            _repositorio = repositorio;
        }
        #endregion
        public List<Oficina> ListarTodo()
        {
            List<Oficina> oLista = new List<Oficina>();
            oLista = _repositorio.ListarTodo();
            return oLista;
        }
    }
}
