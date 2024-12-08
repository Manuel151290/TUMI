using TUMI.TIC.Modelo.Entidades;

namespace TUMI.TIC.ClienteWeb.Models.ModeloTicket
{
    public class ViewModelTicketIndex
    {
        public Colaborador oColaborador { get; set; }
        public List<UnidadOrganica> oListaUnidadOrganica { get; set; }
        public List<SelectTipoEntero> oListaLugarIncidente { get; set; }
        public List<SelectTipo> oListaResponsable { get; set; }
        public Ticket oTicket { get; set; }
    }
}

