using Microsoft.AspNetCore.Mvc;
using TUMI.TIC.Dominio.Contratos;
using TUMI.TIC.Modelo.Entidades;

namespace TUMI.TIC.ClienteWeb.Controllers.Api
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class ColaboradorController : ControllerBase
    {
        #region Variables y Propiedades
        private readonly IColaboradorDominio _oColaboradorDominio;
        private List<Colaborador> oListaColaboradores = new();
        #endregion

        #region Constructor
        public ColaboradorController(IColaboradorDominio oColaboradorDominio)
        {
            _oColaboradorDominio = oColaboradorDominio;
        }
        #endregion

        #region Métodos
        [HttpGet]
        public List<Colaborador> Get() {
            oListaColaboradores = _oColaboradorDominio.ListarColaboradores();
            return oListaColaboradores;
        }

        #endregion
    }
}
