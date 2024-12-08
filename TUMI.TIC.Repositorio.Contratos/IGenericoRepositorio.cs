using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TUMI.TIC.Modelo.Entidades;

namespace TUMI.TIC.Repositorio.Contratos
{
    public interface IGenericoRepositorio
    {
        List<SelectTipo> ObtenerSubAreaPorUnidad(string codigoUnidad);
        List<SelectTipo> ObtenerCategoriaPorSubArea(string codigoSubArea);
        List<SelectTipo> ObtenerSubCategoriaPorCategoria(string codigoCategoria);
        List<SelectTipoEntero> ListarLugarIncidente();
        List<SelectTipo> ListarResponsableAutorizacion();
        List<SelectTipoEntero> ListarTipoEvidencia();
    }
}
