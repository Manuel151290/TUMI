using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TUMI.TIC.ClienteWeb.Models.ModeloColaborador;
using TUMI.TIC.Data;
using TUMI.TIC.Dominio;
using TUMI.TIC.Dominio.Contratos;
using TUMI.TIC.Funciones;
using TUMI.TIC.Modelo.Entidades;
using TUMI.TIC.Repositorio.Contratos;
using TUMI.TIC.Repositorio.SqlServer;

namespace TUMI.TIC.ClienteWeb.Controllers.Web
{
    public class ColaboradorController : Controller
    {
        #region Variables y Propiedades
        private readonly IColaboradorDominio _oColaboradorDominio;
        private readonly ICargoDominio _cargoDominio;
        private readonly IAreaDominio _areaDominio;
        private readonly IOficinaDominio _oficinaDominio;
        #endregion

        #region Constructor
        public ColaboradorController(IColaboradorDominio oColaboradorDominio, ICargoDominio cargoDominio,
                                     IAreaDominio areaDominio, IOficinaDominio oficinaDominio)
        {
            _oColaboradorDominio = oColaboradorDominio;
            _cargoDominio = cargoDominio;
            _areaDominio = areaDominio;
            _oficinaDominio = oficinaDominio;
        }
        #endregion

        #region Métodos
        [HttpGet]
        public ActionResult Index(int status = -1)
        {
            List<Colaborador> oListaColaboradores = _oColaboradorDominio.ListarColaboradores();
            ViewBag.Status = status;
            return View(oListaColaboradores);
        }

        [HttpGet]
        public ActionResult Detalle(string id)
        {
            Colaborador oColaborador = _oColaboradorDominio.TraerUno(id);
            List<Cargo> oListaCargo = _cargoDominio.ListarTodo();
            List<Oficina> oListaOficina = _oficinaDominio.ListarTodo();
            List<UnidadOrganica> oListaArea = _areaDominio.ListarTodo();
            DetalleGetColaborador oModelColaborador = new DetalleGetColaborador();
            oModelColaborador.oColaborador = oColaborador;
            oModelColaborador.oListaCargos = oListaCargo;
            oModelColaborador.oListaAreas = oListaArea;
            oModelColaborador.oListaOficinas = oListaOficina;

            return View(oModelColaborador);
        }

        [HttpPost]
        [AllowAnonymous]
        [IgnoreAntiforgeryToken]
        public ActionResult Actualizar(Colaborador oColaborador)
        {
            bool resultado = false;
            
            resultado = _oColaboradorDominio.ActualizarColaborador(oColaborador);
            if (resultado)
            {
                return RedirectToAction("Index", "Colaborador", new { status = 1 });
            }
            else
            {
                return RedirectToAction("Index", "Colaborador", new { status = 0 });
            }
            
        }

        [HttpGet]
        public ActionResult Registrar(int status = -1)
        {
            Colaborador oColaborador = new Colaborador();
            if (TempData != null && TempData.ContainsKey("Colaborador"))
            {
                oColaborador = JsonConvert.DeserializeObject<Colaborador>(TempData["Colaborador"].ToString());
            }
            List<Cargo> oListaCargo = _cargoDominio.ListarTodo();
            List<Oficina> oListaOficina = _oficinaDominio.ListarTodo();
            List<UnidadOrganica> oListaArea = _areaDominio.ListarTodo();
            DetalleGetColaborador oModelColaborador = new DetalleGetColaborador();
            oModelColaborador.oColaborador = oColaborador;
            oModelColaborador.oListaCargos = oListaCargo;
            oModelColaborador.oListaAreas = oListaArea;
            oModelColaborador.oListaOficinas = oListaOficina;
            ViewBag.Status = status;
            return View(oModelColaborador);
        }

        [HttpPost]
        [AllowAnonymous]
        [IgnoreAntiforgeryToken]
        public ActionResult Registrar(Colaborador oColaborador)
        {
            bool resultado = false;
            Colaborador colaborador = _oColaboradorDominio.TraerUno(oColaborador.Codigo);
            Colaborador oColaboradorExistente = _oColaboradorDominio.ListarColaboradores().Where(x => x.DocumentoIdentidad == oColaborador.DocumentoIdentidad)
                                                                                          .FirstOrDefault();
            if (colaborador != null || oColaboradorExistente != null)
            {
                TempData["Colaborador"] = JsonConvert.SerializeObject(oColaborador);
                return RedirectToAction("Registrar", "Colaborador", new { status = 0 });
            }

            oColaborador.ClaveCifrado = Encriptar.ObtenerSHA256(oColaborador.ClaveCifrado);
            resultado = _oColaboradorDominio.RegistrarColaborador(oColaborador);
            if (resultado)
            {
                return RedirectToAction("Index", "Colaborador", new { status = 2 });
            }
            else
            {
                return RedirectToAction("Index", "Colaborador", new { status = -2 });
            }

        }

        public JsonResult ActualizaClave(string oColaboradorJson)
        {
            bool resultado = false;
            try
            {
                Colaborador? oColaborador = JsonConvert.DeserializeObject<Colaborador>(oColaboradorJson);
                
                if (oColaborador != null)
                {
                    string clave = Funciones.Encriptar.ObtenerSHA256(oColaborador.Codigo);
                    resultado = _oColaboradorDominio.ActualizarClave(oColaborador.Codigo, clave);
                    if (resultado)
                    {
                        return Json(new { mensaje = "Se actualizó correctamente la clave del usuario.", estado = "Ok" });
                    }
                    else
                    {
                        return Json(new { mensaje = "Ocurrió un error al actualizar.", estado = "Error" });
                    }
                }
                else
                {
                    return Json(new { mensaje = "Ocurrió un error(Referencia Nulo), se encontraron valores nulos en el controlador.", estado = "Error" });
                }
            }
            catch (Exception)
            {
                return Json(new { mensaje = "Ocurrió un error al derivar. (TryCatch)", estado = "Error" });
            }
        }
        #endregion
    }
}
