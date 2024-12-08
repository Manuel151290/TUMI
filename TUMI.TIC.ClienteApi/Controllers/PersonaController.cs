using Microsoft.AspNetCore.Mvc;
using TUMI.TIC.Modelo.Entidades;

namespace TUMI.TIC.ClienteApi.Controllers
{
    [Produces("application/json")]
    [Route("api/Persona")]
    public class PersonaController : Controller
    {
        public Colaborador Get()
        {
            Colaborador oColaborador = new Colaborador();
            oColaborador.CodigoColaborador = "CQUINP";
            oColaborador.NombreColaborador = "QUINTO/PUMAR,Carlos Salomón";

            return oColaborador;
        }
    }
}
