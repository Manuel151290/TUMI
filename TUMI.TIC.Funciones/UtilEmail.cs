using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using TUMI.TIC.BloqueDatos;
using Microsoft.AspNetCore.Hosting;
using TUMI.TIC.Modelo.ParametroSql;
using TUMI.TIC.Modelo.Entidades;

namespace TUMI.TIC.Funciones
{
    public class UtilEmail
    {
        
        public static void CorreoRegistroTicket(RegistroTicket oDatos)
        {
            string rutaArchivoHTML = Path.Combine("EmailTheme", "RegistroTicket.html");
            MailMessage mensajeMail = new MailMessage();
            SmtpClient clienteSMTP = new SmtpClient();
            mensajeMail.Subject = $"Registro de Ticket N° {oDatos.NumeroTicket.ToString()}";
            mensajeMail.From = new MailAddress(Constantes.CorreoEnvioCorreo, Constantes.NombreEmpresaComercial);
            mensajeMail.To.Add(new MailAddress(oDatos.CorreoResponsable));
            mensajeMail.To.Add(new MailAddress(oDatos.CorreoColaboradorRegistro));
            string html = System.IO.File.ReadAllText(rutaArchivoHTML);
            html = html.Replace("{nombre}", oDatos.NombreColaboradorRegistro);
            html = html.Replace("{nombre_empresa}", Constantes.NombreEmpresaComercial);
            html = html.Replace("{numero_ticket}", oDatos.NumeroTicket.ToString());
            html = html.Replace("{descripcion_ticket}", oDatos.DescripcionTicket);
            html = html.Replace("{fecha_ticket}", oDatos.FechaRegistroTicket.ToString("yyyy-MM-dd hh:mm:ss"));
            AlternateView htmlVista = AlternateView.CreateAlternateViewFromString(html, Encoding.UTF8, System.Net.Mime.MediaTypeNames.Text.Html);
            mensajeMail.AlternateViews.Add(htmlVista);
            if (oDatos.RutaArchivo.Length != 0)
            {
                mensajeMail.Attachments.Add(new Attachment(oDatos.RutaArchivo));
            }
            clienteSMTP.Host = "smtp.gmail.com";
            clienteSMTP.Port = 587;
            clienteSMTP.Credentials = new NetworkCredential(Constantes.CorreoEnvioCorreo, Constantes.CredencialesEnvioCorreo);
            clienteSMTP.EnableSsl = true;
            clienteSMTP.Send(mensajeMail);
        }

        public static void CorreoDerivacionTicket(DatoDerivacionCorreo oDatos, int numeroTicket)
        {
            string rutaArchivoHTML = Path.Combine("EmailTheme", "DerivacionTicket.html");
            MailMessage mensajeMail = new MailMessage();
            SmtpClient clienteSMTP = new SmtpClient();
            mensajeMail.Subject = $"Derivación de Ticket N° {numeroTicket.ToString()}";
            mensajeMail.From = new MailAddress(Constantes.CorreoEnvioCorreo, Constantes.NombreEmpresaComercial);
            mensajeMail.To.Add(new MailAddress(oDatos.CorreoRegistro));
            mensajeMail.To.Add(new MailAddress(oDatos.CorreoDerivado));
            string html = System.IO.File.ReadAllText(rutaArchivoHTML);
            html = html.Replace("{nombre}", oDatos.NombreRegistro);
            html = html.Replace("{nombre_derivado}", oDatos.NombreDerivado);
            html = html.Replace("{nombre_empresa}", Constantes.NombreEmpresaComercial);
            html = html.Replace("{numero_ticket}", numeroTicket.ToString());
            AlternateView htmlVista = AlternateView.CreateAlternateViewFromString(html, Encoding.UTF8, System.Net.Mime.MediaTypeNames.Text.Html);
            mensajeMail.AlternateViews.Add(htmlVista);
            
            clienteSMTP.Host = "smtp.gmail.com";
            clienteSMTP.Port = 587;
            clienteSMTP.Credentials = new NetworkCredential(Constantes.CorreoEnvioCorreo, Constantes.CredencialesEnvioCorreo);
            clienteSMTP.EnableSsl = true;
            clienteSMTP.Send(mensajeMail);
        }
        public static void CorreoActualizacionCierre(string correoDestino, string urlCalificacion, int codigoTicket, string usuarioSolicitante, string Descripcion, DateTime fechaActual)
        {
            string rutaArchivoHTML = Path.Combine("EmailTheme", "ActualizacionCierre.html");
            if (File.Exists(rutaArchivoHTML))
            {
                try
                {
                    MailMessage mensajeMail = new MailMessage();
                    SmtpClient clienteSMTP = new SmtpClient();
                    mensajeMail.Subject = "Actualización de Cierre de Ticket";
                    mensajeMail.From = new MailAddress(Constantes.CorreoEnvioCorreo, Constantes.NombreEmpresaComercial);
                    mensajeMail.To.Add(new MailAddress(correoDestino));
                    string html = File.ReadAllText(rutaArchivoHTML);
                    html = html.Replace("{url_calificacion}", urlCalificacion);
                    html = html.Replace("{nombre_empresa}", Constantes.NombreEmpresaComercial);
                    html = html.Replace("{numero_ticket}", codigoTicket.ToString());
                    html = html.Replace("{nombre}", usuarioSolicitante);
                    html = html.Replace("{fecha_ticket}", fechaActual.ToString("yyyy-MM-dd HH:mm:ss"));
                    html = html.Replace("{descripcion_ticket}", Descripcion);
                    AlternateView htmlVista = AlternateView.CreateAlternateViewFromString(html, Encoding.UTF8, System.Net.Mime.MediaTypeNames.Text.Html);
                    mensajeMail.AlternateViews.Add(htmlVista);

                    clienteSMTP.Host = "smtp.gmail.com";
                    clienteSMTP.Port = 587;
                    clienteSMTP.Credentials = new NetworkCredential(Constantes.CorreoEnvioCorreo, Constantes.CredencialesEnvioCorreo);
                    clienteSMTP.EnableSsl = true;
                    clienteSMTP.Send(mensajeMail);
                }
                catch (Exception ex)
                {
                    // Manejo de excepciones
                    throw new Exception($"Error al enviar el correo de actualización de cierre: {ex.Message}", ex);
                }
            }
            else
            {
                throw new FileNotFoundException($"El archivo {rutaArchivoHTML} no fue encontrado.");
            }
        }

    }
}
