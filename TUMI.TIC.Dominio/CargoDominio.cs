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
    public class CargoDominio : ICargoDominio
    {
        #region Variables
        private readonly ICargoRepositorio _repositorio;
        #endregion

        #region Constructor
        public CargoDominio(ICargoRepositorio repositorio)
        {
            _repositorio = repositorio;
        }
        #endregion
        public List<Cargo> ListarTodo()
        {
            List<Cargo> oLista = new List<Cargo>();
            oLista = _repositorio.ListarTodo();
            return oLista;
        }
    }
}
