using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using TUMI.TIC.Dominio.Contratos;

namespace TUMI.TIC.ClienteWeb.Controllers.Web
{
    public class PortalController : Controller
    {
        #region Variables y Propiedades
        private readonly IDashBoardDominio _dashboardDominio;
        #endregion

        #region Constructor
        public PortalController(IDashBoardDominio dashboardDominio)
        {
            _dashboardDominio = dashboardDominio;
        }
        #endregion

        public IActionResult Index()
        {
            Models.Portal.PortalViewModel oModel = new Models.Portal.PortalViewModel();
            oModel.oTotales = _dashboardDominio.TraerTotales();
            return View(oModel);
        }

        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Remove("oUsuario");
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Login");
        }
    }
}
