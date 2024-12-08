using TUMI.TIC.Modelo.Entidades;

namespace TUMI.TIC.ClienteWeb.Models.ModeloTicket
{
    public class ViewModelTicketAtencion
    {
        public DatosBasicos oDatosBasicos { get; set; }
        public List<SelectTipoEntero> oListaTipoEvidencia { get; set; }
        public List<DocumentoTicket> oListaDocumentos { get; set; }
    }
}
