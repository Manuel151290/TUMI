using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OfficeOpenXml.Style;
using OfficeOpenXml;
using System.Data;
using TUMI.TIC.Dominio.Contratos;
using TUMI.TIC.Modelo.Entidades;
using TUMI.TIC.Repositorio.Contratos;
using TUMI.TIC.Repositorio.SqlServer;
using TUMI.TIC.ClienteWeb.Models.ReporteTicket;

namespace TUMI.TIC.ClienteWeb.Controllers.Web
{
    public class ReporteController : Controller
    {
        #region Variables y Propiedades
        private readonly IGenericoDominio _oGenericoDominio;
        private readonly ICargoDominio _cargoDominio;
        private readonly IAreaDominio _areaDominio;
        private readonly IOficinaDominio _oficinaDominio;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly ITicketDominio _ticketDominio;
        private readonly IColaboradorDominio _colaboradorDominio;
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;

        #endregion

        #region Constructor
        public ReporteController(IGenericoDominio oGenericoDominio, ICargoDominio cargoDominio,
                                IAreaDominio areaDominio, IOficinaDominio oficinaDominio,
                                IWebHostEnvironment hostingEnvironment, ITicketDominio ticketDominio,
                                IColaboradorDominio colaboradorDominio, IConfiguration configuration, 
                                IWebHostEnvironment env)
        {
            _oGenericoDominio = oGenericoDominio;
            _cargoDominio = cargoDominio;
            _areaDominio = areaDominio;
            _oficinaDominio = oficinaDominio;
            _hostingEnvironment = hostingEnvironment;
            _ticketDominio = ticketDominio;
            _colaboradorDominio = colaboradorDominio;
            _configuration = configuration;
            _env = env;
        }
        #endregion
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult PorUnidadOrganica()
        {
            try
            {
                List<ReporteTiket> oReporte = new List<ReporteTiket>();
                oReporte = _ticketDominio.ReportePorUnidadOrganica();

                string carpetaTemporal = _configuration["RutaTemporal"];
                string nombreArchivo = $"ReporteConsolidado_{DateTime.Now.ToString("yyyyMMddhhmmss")}.xlsx";
                string rutaTemporal = Path.Combine(carpetaTemporal, nombreArchivo);

                string directorioRaiz = _env.ContentRootPath;
                string nombreTrans = Path.Combine(directorioRaiz, "PlantillaTrans", "Ticket", "ReporteConsolidado.xlsx");
                System.IO.File.Copy(nombreTrans, rutaTemporal);
                FileInfo nuevoArchivo = new FileInfo(rutaTemporal);
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                using (ExcelPackage paqueteExcel = new ExcelPackage(nuevoArchivo))
                {
                    ExcelWorksheet hoja = paqueteExcel.Workbook.Worksheets["Reporte"];
                    int filaInicial = 7;
                    foreach (ReporteTiket item in oReporte)
                    {
                        hoja.Cells[filaInicial, 1].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                        hoja.Cells[filaInicial, 1].Value = item.AnioRegistro;
                        hoja.Cells[filaInicial, 2].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                        hoja.Cells[filaInicial, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        hoja.Cells[filaInicial, 2].Value = item.CodigoUnidadOrganica.ToString();
                        hoja.Cells[filaInicial, 3].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                        hoja.Cells[filaInicial, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        hoja.Cells[filaInicial, 3].Value = item.UnidadOrganica.ToString();
                        hoja.Cells[filaInicial, 4].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                        hoja.Cells[filaInicial, 4].Value = item.TotalTickets;
                        hoja.Cells[filaInicial, 5].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                        hoja.Cells[filaInicial, 5].Value = item.TotalAtendidos;
                        hoja.Cells[filaInicial, 6].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                        hoja.Cells[filaInicial, 6].Value = item.TotalNoAtendidos;

                        filaInicial++;
                    }
                    hoja.Cells.Worksheet.Workbook.Styles.UpdateXml();
                    var FileBytesArray = paqueteExcel.GetAsByteArray();
                    System.IO.File.Delete(rutaTemporal);
                    return File(FileBytesArray, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", nombreArchivo);
                }
            }
            catch (Exception)
            {
                return RedirectToAction("Index","Portal");
            }
        }

        public IActionResult PorFechas(string fechaInicial, string fechaFinal)
        {
            try
            {
                List<ReportePorFechas> oReporte = new List<ReportePorFechas>();
                oReporte = _ticketDominio.ReportePorFechas(Convert.ToDateTime(fechaInicial), Convert.ToDateTime(fechaFinal));

                string carpetaTemporal = _configuration["RutaTemporal"];
                string nombreArchivo = $"ReportePorFechas_{DateTime.Now.ToString("yyyyMMddhhmmss")}.xlsx";
                string rutaTemporal = Path.Combine(carpetaTemporal, nombreArchivo);

                string directorioRaiz = _env.ContentRootPath;
                string nombreTrans = Path.Combine(directorioRaiz, "PlantillaTrans", "Ticket", "ReportePorFechas.xlsx");
                System.IO.File.Copy(nombreTrans, rutaTemporal);
                FileInfo nuevoArchivo = new FileInfo(rutaTemporal);
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                using (ExcelPackage paqueteExcel = new ExcelPackage(nuevoArchivo))
                {
                    ExcelWorksheet hoja = paqueteExcel.Workbook.Worksheets["Reporte"];
                    int filaInicial = 7;
                    foreach (ReportePorFechas item in oReporte)
                    {
                        hoja.Cells[filaInicial, 1].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                        hoja.Cells[filaInicial, 1].Value = item.CodigoTicket;
                        hoja.Cells[filaInicial, 2].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                        hoja.Cells[filaInicial, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        hoja.Cells[filaInicial, 2].Value = item.Colaborador.ToString();
                        hoja.Cells[filaInicial, 3].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                        hoja.Cells[filaInicial, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        hoja.Cells[filaInicial, 3].Value = item.UnidadOrganica.ToString();
                        hoja.Cells[filaInicial, 4].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                        hoja.Cells[filaInicial, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        hoja.Cells[filaInicial, 4].Value = item.DescripcionTicket.ToString();
                        hoja.Cells[filaInicial, 5].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                        hoja.Cells[filaInicial, 5].Value = item.FechaRegistro.ToString("yyyy-MM-dd");
                        hoja.Cells[filaInicial, 6].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                        hoja.Cells[filaInicial, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        hoja.Cells[filaInicial, 6].Value = item.ColaboradorAtendio.ToString();
                        hoja.Cells[filaInicial, 7].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                        hoja.Cells[filaInicial, 7].Value = item.EstadoTicket.ToString();

                        filaInicial++;
                    }
                    hoja.Cells.Worksheet.Workbook.Styles.UpdateXml();
                    var FileBytesArray = paqueteExcel.GetAsByteArray();
                    System.IO.File.Delete(rutaTemporal);
                    return File(FileBytesArray, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", nombreArchivo);
                }
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Portal");
            }
        }
        [HttpGet]
        public JsonResult ObtenerReporteTickets(DateTime fechaInicial, DateTime fechaFinal)
        {
            try
            {
                List<ReporteCalificaciones> reporte = _ticketDominio.ObtenerReporteCalificaciones(fechaInicial, fechaFinal);
                return Json(reporte);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        public IActionResult DescargarReporteTickets(DateTime fechaInicial, DateTime fechaFinal)
        {
            try
            {
                List<ReporteCalificaciones> reporte = _ticketDominio.ObtenerReporteCalificaciones(fechaInicial, fechaFinal);

                string carpetaTemporal = _configuration["RutaTemporal"];
                string nombreArchivo = $"ReporteTickets_{DateTime.Now.ToString("yyyyMMddhhmmss")}.xlsx";
                string rutaTemporal = Path.Combine(carpetaTemporal, nombreArchivo);

                FileInfo nuevoArchivo = new FileInfo(rutaTemporal);
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                using (ExcelPackage paqueteExcel = new ExcelPackage(nuevoArchivo))
                {
                    ExcelWorksheet hoja = paqueteExcel.Workbook.Worksheets.Add("Reporte");

                    // Encabezados con diseño
                    hoja.Cells[1, 1].Value = "Nivel de Calificación";
                    hoja.Cells[1, 2].Value = "Cantidad";
                    hoja.Cells[1, 3].Value = "Porcentaje";
                    hoja.Cells[1, 1, 1, 3].Style.Font.Bold = true;
                    hoja.Cells[1, 1, 1, 3].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    hoja.Cells[1, 1, 1, 3].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.FromArgb(79, 129, 189));
                    hoja.Cells[1, 1, 1, 3].Style.Font.Color.SetColor(System.Drawing.Color.White);
                    hoja.Cells[1, 1, 1, 3].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                    int filaInicial = 2;
                    int totalTickets = reporte.Sum(r => r.Cantidad);
                    foreach (var item in reporte)
                    {
                        hoja.Cells[filaInicial, 1].Value = item.Nivel;
                        hoja.Cells[filaInicial, 2].Value = item.Cantidad;
                        hoja.Cells[filaInicial, 3].Value = ((item.Cantidad / (double)totalTickets) * 100).ToString("F2") + "%";
                        filaInicial++;
                    }

                    // Total
                    hoja.Cells[filaInicial, 1].Value = "Total Tickets";
                    hoja.Cells[filaInicial, 2].Value = totalTickets;
                    hoja.Cells[filaInicial, 3].Value = "100%";
                    hoja.Cells[filaInicial, 1, filaInicial, 3].Style.Font.Bold = true;
                    hoja.Cells[filaInicial, 1, filaInicial, 3].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    hoja.Cells[filaInicial, 1, filaInicial, 3].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.FromArgb(217, 217, 217));

                    // Ajustar el tamaño de las columnas
                    hoja.Cells.AutoFitColumns();

                    // Agregar gráfico de torta
                    var chart = hoja.Drawings.AddChart("GraficoTorta", OfficeOpenXml.Drawing.Chart.eChartType.PieExploded3D);
                    chart.Title.Text = "Distribución de Calificaciones";
                    chart.SetPosition(filaInicial + 2, 0, 0, 0); // Ubicación del gráfico
                    chart.SetSize(600, 400); // Tamaño del gráfico

                    // Rango de datos para el gráfico
                    var rangoNiveles = hoja.Cells[2, 1, filaInicial - 1, 1]; // Niveles
                    var rangoCantidad = hoja.Cells[2, 2, filaInicial - 1, 2]; // Cantidades
                    chart.Series.Add(rangoCantidad, rangoNiveles);

                    // Diseño del gráfico
                    chart.Style = OfficeOpenXml.Drawing.Chart.eChartStyle.Style18;

                    var FileBytesArray = paqueteExcel.GetAsByteArray();
                    return File(FileBytesArray, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", nombreArchivo);
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Portal");
            }
        }

    }
}
