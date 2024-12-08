namespace TUMI.TIC.ClienteWeb.Models.Cuenta
{
    public class Respuesta
    {
        public int Estado { get; set; }
        public string Mensaje { get; set; }
        public string Accion { get; set; }
        public string Anterior { get; set; }
        public string MensajeAuxiliar { get; set; }
    }
}
