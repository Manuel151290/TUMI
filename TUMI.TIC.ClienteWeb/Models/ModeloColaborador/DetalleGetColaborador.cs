using TUMI.TIC.Modelo.Entidades;

namespace TUMI.TIC.ClienteWeb.Models.ModeloColaborador
{
    public class DetalleGetColaborador
    {
        public Colaborador oColaborador { get;set; }
        public List<Oficina> oListaOficinas { get; set;}
        public List<Cargo> oListaCargos { get; set;} 
        public List<UnidadOrganica> oListaAreas { get;set; }

    }
}
