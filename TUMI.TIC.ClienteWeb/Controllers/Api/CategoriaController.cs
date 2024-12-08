using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TUMI.TIC.Dominio.Contratos;
using TUMI.TIC.Modelo.Entidades;

namespace TUMI.TIC.ClienteWeb.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        #region Variables y Propiedades
        private readonly IGenericoDominio _oDominio;
        private List<SelectTipo> oLista = new();
        #endregion

        #region Constructor
        public CategoriaController(IGenericoDominio oDominio)
        {
            _oDominio = oDominio;
        }
        #endregion

        #region Métodos
        [HttpGet]
        public List<SelectTipo> Get(string codigo)
        {
            oLista = _oDominio.ObtenerCategoriaPorSubArea(codigo);
            return oLista;
        }

        #endregion
    }
}
