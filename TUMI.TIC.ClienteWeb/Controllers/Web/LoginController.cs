using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TUMI.TIC.ClienteWeb.Models.Cuenta;
using TUMI.TIC.Dominio.Contratos;
using TUMI.TIC.Funciones;
using TUMI.TIC.Modelo.Entidades;


namespace TUMI.TIC.ClienteWeb.Controllers.Web
{
    public class LoginController : Controller
    {
        #region Variables y Propiedades
        private readonly IColaboradorDominio _oColaboradorDominio;
        #endregion

        #region Constructor
        public LoginController(IColaboradorDominio oColaboradorDominio)
        {
            _oColaboradorDominio = oColaboradorDominio;
        }
        #endregion

        [HttpGet]
        public IActionResult Index()
        {
            ViewData["RespuestaMensaje"] = "";
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public IActionResult Index(Login model)
        {
            model.Usuario = model.Usuario;
            model.Clave = model.Clave;

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            Respuesta oRespuesta = new Respuesta();

            oRespuesta = ValidaIngreso(model.Usuario, Encriptar.ObtenerSHA256(model.Clave));
            if (oRespuesta.Estado == 1)
            {
                return RedirectToAction("Index", "Portal");

            }
            else
            {
                ModelState.AddModelError("", oRespuesta.Mensaje);
                ViewData["RespuestaMensaje"] =  (oRespuesta.Mensaje == "") ? "Usuario o clave invalidos. Intente nuevamente.": oRespuesta.Mensaje;
                return View(model);
            }
        }

        private Respuesta ValidaIngreso(string usuario, string clave)
        {
            string mensaje = "";
            bool respuestaValidacion = false;

            Respuesta oRespuesta = new Respuesta
            {
                Estado = 0,
                Mensaje = "",
            };

            try
            {
                Colaborador oUsuarioDato = _oColaboradorDominio.ObtenerDatos(usuario.ToUpper(), clave, out respuestaValidacion);

                if (respuestaValidacion != false)
                {
                    oRespuesta.Estado = 1;
                    oRespuesta.Mensaje = mensaje;
                    Colaborador oColaboradorInicioSesion = _oColaboradorDominio.TraerUno(usuario.ToUpper());
                    CrearVariablesSesion(oColaboradorInicioSesion);
                }
                else
                {
                    oRespuesta.Mensaje = mensaje;
                }
            }
            catch (Exception ex)
            {
                oRespuesta.Mensaje = $"Ocurrió un error en inicio de sesión. Error: {ex.Message}";
            }
            return oRespuesta;
        }

        private void CrearVariablesSesion(Colaborador oUsuario)
        {
            string colaboradorJson = JsonConvert.SerializeObject(oUsuario);
            HttpContext.Session.SetString("oUsuario", colaboradorJson);
        }
    }
}
