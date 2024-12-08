using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.Design;
using TUMI.TIC.Dominio.Contratos;
using TUMI.TIC.Modelo.Entidades;

namespace TUMI.TIC.ClienteWeb.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubAreaController : ControllerBase
    {
        #region Variables y Propiedades
        private readonly IGenericoDominio _oDominio;
        private List<SelectTipo> oLista = new();
        #endregion

        #region Constructor
        public SubAreaController(IGenericoDominio oDominio)
        {
            _oDominio = oDominio;
        }
        #endregion

        #region Métodos
        [HttpGet]
        public List<SelectTipo> Get(string codigo)
        {
            oLista = _oDominio.ObtenerSubAreaPorUnidad(codigo);
            return oLista;
        }

        #endregion
    }
}
