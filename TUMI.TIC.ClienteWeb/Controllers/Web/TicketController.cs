using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Reflection;
using TUMI.TIC.BloqueDatos;
using TUMI.TIC.ClienteWeb.Models.ModeloTicket;
using TUMI.TIC.Dominio;
using TUMI.TIC.Dominio.Contratos;
using TUMI.TIC.Modelo.Entidades;
using TUMI.TIC.Modelo.ParametroSql;

namespace TUMI.TIC.ClienteWeb.Controllers.Web
{
    public class TicketController : Controller
    {
        #region Variables y Propiedades
        private readonly IGenericoDominio _oGenericoDominio;
        private readonly ICargoDominio _cargoDominio;
        private readonly IAreaDominio _areaDominio;
        private readonly IOficinaDominio _oficinaDominio;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly ITicketDominio _ticketDominio;
        private readonly IColaboradorDominio _colaboradorDominio;

        #endregion

        #region Constructor
        public TicketController(IGenericoDominio oGenericoDominio, ICargoDominio cargoDominio,
                                IAreaDominio areaDominio, IOficinaDominio oficinaDominio, 
                                IWebHostEnvironment hostingEnvironment, ITicketDominio ticketDominio, 
                                IColaboradorDominio colaboradorDominio)
        {
            _oGenericoDominio = oGenericoDominio;
            _cargoDominio = cargoDominio;
            _areaDominio = areaDominio;
            _oficinaDominio = oficinaDominio;
            _hostingEnvironment = hostingEnvironment;
            _ticketDominio = ticketDominio;
            _colaboradorDominio = colaboradorDominio;
        }
        #endregion

        #region MétodosAtencion
        public IActionResult Index()
        {
            Colaborador oColaborador = JsonConvert.DeserializeObject<Colaborador>(HttpContext.Session.GetString("oUsuario"));
            List<UnidadOrganica> oListaUnidad = _areaDominio.ListarParaIncidentes();
            List<SelectTipoEntero> oListaLugarIncidente = _oGenericoDominio.ListarLugarIncidente();
            List<SelectTipo> oListaResponsable = _oGenericoDominio.ListarResponsableAutorizacion(); 
            ViewModelTicketIndex oModelo = new ViewModelTicketIndex();
            oModelo.oColaborador = oColaborador;
            oModelo.oListaUnidadOrganica = oListaUnidad;
            oModelo.oListaLugarIncidente = oListaLugarIncidente;
            oModelo.oListaResponsable = oListaResponsable;
            return View(oModelo);
        }
        public IActionResult CalificarAtencion(int id)
        {
            var model = new CalificacionViewModel
            {
                CodigoTicket = id,
                Calificacion = 0 
            };
            return View(model);
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]
        public IActionResult CalificarAtencion(CalificacionViewModel model)
        {
            if (ModelState.IsValid)
            {
                bool resultado = _ticketDominio.GuardarCalificacion(model.CodigoTicket, model.Calificacion);
                if (resultado)
                {
                    ViewBag.Mensaje = "Gracias por su calificación.";
                }
                else
                {
                    ViewBag.Mensaje = "Ocurrió un error al guardar la calificación, por favor intente nuevamente.";
                }
            }
            else
            {
                ViewBag.Mensaje = "El modelo no es válido, por favor revise los datos ingresados.";
            }
            return View(model);
        }


        [HttpPost]
        [IgnoreAntiforgeryToken]
        public JsonResult EliminarEvidencia(string documentoJson)
        {
            try
            {
                SelectTipoEntero? oRegistro = JsonConvert.DeserializeObject<SelectTipoEntero>(documentoJson);
                Colaborador oColaborador = JsonConvert.DeserializeObject<Colaborador>(HttpContext.Session.GetString("oUsuario"));
                if (!string.IsNullOrEmpty(oRegistro.Codigo.ToString()))
                {
                    bool resultado = _ticketDominio.EliminarDocumento(oRegistro.Codigo, oColaborador.Codigo);
                    if (resultado)
                    {
                        return Json(new { mensaje = "Se eliminó el documento correctamente.", estado = "Ok" });
                    }
                    else
                    {
                        return Json(new { mensaje = "Ocurrió un error al eliminar el documento, por favor intente nuevamente.", estado = "Error" });
                    }
                }
                return Json(new { mensaje = "Ocurrió un error al eliminar el documento, por favor intente nuevamente.", estado = "Error" });
            }
            catch (Exception)
            {
                return Json(new { mensaje = "No se eliminó nungún documento, por favor intente nuevamente.", estado = "Error" });
            }
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]
        public JsonResult ActualizaCierre(string registroJson)
        {
            try
            {
                ActualizarCierre? oRegistro = JsonConvert.DeserializeObject<ActualizarCierre>(registroJson);

                if (oRegistro == null)
                {
                    return Json(new { mensaje = "Datos de registro inválidos.", estado = "Error" });
                }

                DatosBasicos oDatosBasicos = _ticketDominio.TraerDatosBasicos(oRegistro.CodigoTicket);

                if (!string.IsNullOrEmpty(oRegistro.CodigoTicket.ToString()))
                {
                    bool resultado = _ticketDominio.ActualizarCierre(oRegistro);
                    if (resultado)
                    {
                        string urlCalificacion = Url.Action("CalificarAtencion", "Ticket", new { id = oRegistro.CodigoTicket }, Request.Scheme);
                        Funciones.UtilEmail.CorreoActualizacionCierre(oDatosBasicos.CorreoSolicitante, urlCalificacion, oRegistro.CodigoTicket, oDatosBasicos.NombreSolicitante, oDatosBasicos.Descripcion, DateTime.Now);

                        return Json(new { mensaje = "Se actualizó el ticket correctamente.", estado = "Ok" });
                    }
                    else
                    {
                        return Json(new { mensaje = "Ocurrió un error al actualizar el ticket, por favor intente nuevamente.", estado = "Error" });
                    }
                }
                return Json(new { mensaje = "Ocurrió un error al actualizar el ticket, por favor intente nuevamente.", estado = "Error" });
            }
            catch (Exception ex)
            {
                return Json(new { mensaje = $"No se actualizó ningún ticket, por favor intente nuevamente. Error: {ex.Message}", estado = "Error" });
            }
        }


        [HttpPost]
        [IgnoreAntiforgeryToken]
        public JsonResult AdjuntarEvidencia(string oArchivoJson, IFormFile ArchivoAdjunto)
        {
            try
            {
                AnexarEvidenciaTicket? oEvidencia = JsonConvert.DeserializeObject<AnexarEvidenciaTicket>(oArchivoJson);
                Colaborador oColaborador = JsonConvert.DeserializeObject<Colaborador>(HttpContext.Session.GetString("oUsuario"));

                if (ArchivoAdjunto != null)
                {
                    string extensionArchivo = Path.GetExtension(ArchivoAdjunto.FileName);
                    string nombreArchivo = $"TK_{DateTime.Now.Year.ToString()}{DateTime.Now.Month.ToString()}{DateTime.Now.Day.ToString()}{DateTime.Now.ToString("hhmmss")}_{oEvidencia.CodigoTicket}{extensionArchivo}";
                    string rutaGuardar = Path.Combine(_hostingEnvironment.WebRootPath, "atenciones", nombreArchivo);

                    DocumentoTicket oDocumento = new DocumentoTicket
                    {
                        CodigoTicket = oEvidencia.CodigoTicket,
                        CodigoTipoEvidencia = oEvidencia.CodigoTipoEvidencia,
                        NombreArchivo = nombreArchivo,
                        CodigoUsuarioRegistro = oColaborador.Codigo
                    };

                    bool resultado = _ticketDominio.InsertarDocumento(oDocumento);
                    if (resultado)
                    {
                        using (var stream = new FileStream(rutaGuardar, FileMode.Create))
                        {
                            oEvidencia.NombreArchivo = nombreArchivo;
                            ArchivoAdjunto.CopyTo(stream);
                        }

                        HttpContext.Session.SetString("oEvidencia", JsonConvert.SerializeObject(oEvidencia));

                        return Json(new { mensaje = "Se registró correctamente el archivo.", estado = "Ok" });
                    }
                    else
                    {
                        return Json(new { mensaje = "Ocurrió un error al adjuntar el archivo.", estado = "Error" });
                    }
                }
                else
                {
                    return Json(new { mensaje = "No se cargo ningún archivo, por favor intente nuevamente.", estado = "Error" });
                }
            }
            catch (Exception)
            {
                return Json(new { mensaje = "No se cargo ningún archivo, por favor intente nuevamente.", estado = "Error" });
            }
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]
        public JsonResult RegistroJson(string oTicketJson, IFormFile ArchivoAdjunto)
        {
            bool resultadoEjecucion = false;
            try
            {
                Ticket? oTicket = JsonConvert.DeserializeObject<Ticket>(oTicketJson);
                int codigoTicket = 0;
                if (oTicket != null)
                {
                    oTicket.NombreArchivo = (ArchivoAdjunto != null) ? Path.GetFileName(ArchivoAdjunto.FileName) : "";
                    oTicket.RutaArchivo = string.Empty;
                    oTicket.CorreoColaboradorRegistro = oTicket.CorreoColaboradorRegistro;

                    if (ArchivoAdjunto != null)
                    {
                        string extensionArchivo = Path.GetExtension(ArchivoAdjunto.FileName);
                        string nombreArchivo = $"TK_{DateTime.Now.Year.ToString()}{DateTime.Now.Month.ToString()}{DateTime.Now.Day.ToString()}{DateTime.Now.ToString("hhmmss")}_{oTicket.CodigoColaboradorRegistro}{extensionArchivo}";
                        string rutaGuardar = Path.Combine(_hostingEnvironment.WebRootPath, "evidencias", nombreArchivo);

                        using (var stream = new FileStream(rutaGuardar, FileMode.Create))
                        {
                            oTicket.NombreArchivo = nombreArchivo;
                            oTicket.RutaArchivo = rutaGuardar;
                            ArchivoAdjunto.CopyTo(stream);
                        }
                    }
                    resultadoEjecucion = _ticketDominio.RegistrarTicket(oTicket, out codigoTicket);
                    if (resultadoEjecucion)
                    {
                        RegistroTicket oDatosticket = new RegistroTicket
                        {
                            CorreoColaboradorRegistro = oTicket.CorreoColaboradorRegistro,
                            NombreColaboradorRegistro = oTicket.NombreColaboradorRegistro,
                            RutaArchivo = oTicket.RutaArchivo,
                            CorreoResponsable = Constantes.CorreoResponsableIncidente,
                            NumeroTicket = codigoTicket,
                            FechaRegistroTicket = DateTime.Now,
                            DescripcionTicket = oTicket.Descripcion,
                        };
                        Funciones.UtilEmail.CorreoRegistroTicket(oDatosticket);

                        HttpContext.Session.SetString("oTicket", JsonConvert.SerializeObject(oTicket));

                        return Json(new { mensaje = "Se registró correctamente el ticket.", estado = "Ok", ticket = codigoTicket });
                    }
                    else
                    {
                        return Json(new { mensaje = "Ocurrió un error al guardar el registro(SQL). Por favor intente nuevamente.", estado = "Error" });
                    }
                }
                else
                {
                    return Json(new { mensaje = "Ocurrió un error(Referencia Nulo), se encontraron valores nulos en el controlador.", estado = "Error" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { mensaje = $"Ocurrió un error al procesar su solicitud(TryCatch). Error: {ex.Message}", estado = "Error" });
            }
        }


        public IActionResult Derivacion()
        {   
            List<TicketDerivado> oLista = new List<TicketDerivado>();
            List<Colaborador> oListaColaboradores = new List<Colaborador>();
            oListaColaboradores = _colaboradorDominio.ListarColaboradores();
            oLista = _ticketDominio.oListaticketDerivado();
            ViewBag.Colaboradores = oListaColaboradores;
            return View(oLista);
        }

        public JsonResult DerivacionJson(string oTicketJson)
        {
            bool resultado = false;
            try
            {
                Ticket? oTicket = JsonConvert.DeserializeObject<Ticket>(oTicketJson);
                Colaborador oColaborador = JsonConvert.DeserializeObject<Colaborador>(HttpContext.Session.GetString("oUsuario"));
                if (oTicket != null)
                {
                    DatoDerivacionCorreo oDatos = _ticketDominio.DatosDerivacion(oTicket.Codigo, oTicket.CodigoDerivado);
                    resultado = _ticketDominio.Derivarticket(oTicket.Codigo, oTicket.CodigoDerivado, oColaborador.Codigo);
                    if (resultado)
                    {
                        Funciones.UtilEmail.CorreoDerivacionTicket(oDatos, oTicket.Codigo);
                        return Json(new { mensaje = "Se registró correctamente el ticket.", estado = "Ok" });
                    }
                    else
                    {
                        return Json(new { mensaje = "Ocurrió un error al derivar.", estado = "Error" });
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

        public IActionResult MisTickets()
        {
            Colaborador oColaborador = JsonConvert.DeserializeObject<Colaborador>(HttpContext.Session.GetString("oUsuario"));
            List<TicketDerivado> oLista = new List<TicketDerivado>();
            oLista = _ticketDominio.ListarMisTickets(oColaborador.Codigo);
            return View(oLista);
        }

        public ActionResult Atencion(int id = -1)
        {
            if (id == -1)
            {
                return RedirectToAction("Index", "Portal");
            }
            else
            {
                try
                {
                    Models.ModeloTicket.ViewModelTicketAtencion oModel = new ViewModelTicketAtencion();
                    oModel.oDatosBasicos = _ticketDominio.TraerDatosBasicos(id);
                    oModel.oListaTipoEvidencia = _oGenericoDominio.ListarTipoEvidencia();
                    oModel.oListaDocumentos = _ticketDominio.TraerDocumentos(id);
                    return View(oModel);
                }
                catch (Exception)
                {
                    return RedirectToAction("Index", "Portal");
                }
            }
        }

        public IActionResult ListarDocumentos(int id)
        {
            try
            {
                Models.ModeloTicket.ViewModelTicketAtencion oModel = new ViewModelTicketAtencion();
                oModel.oListaDocumentos = _ticketDominio.TraerDocumentos(id);
                return PartialView(oModel);
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Portal");
            }
        }
        #endregion
    }
}
