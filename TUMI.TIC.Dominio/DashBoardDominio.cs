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
    public class DashBoardDominio : IDashBoardDominio
    {
        #region Variables
        private readonly IDashBoardRepositorio _repositorio;
        #endregion

        #region Constructor
        public DashBoardDominio(IDashBoardRepositorio repositorio)
        {
            _repositorio = repositorio;
        }
        #endregion
        public TotalesDashboard TraerTotales()
        {
            TotalesDashboard oDato = new TotalesDashboard();
            oDato = _repositorio.TraerTotales();
            return oDato;
        }
    }
}
