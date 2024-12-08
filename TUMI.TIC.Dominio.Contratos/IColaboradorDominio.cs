using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TUMI.TIC.Modelo.Entidades;

namespace TUMI.TIC.Dominio.Contratos
{
    public  interface IColaboradorDominio
    {
        List<Colaborador> ListarColaboradores();
        Colaborador ObtenerDatos(string codigo, string clave, out bool estado);
        Colaborador TraerUno(string codigo);
        bool ActualizarColaborador(Colaborador oColaborador);
        bool RegistrarColaborador(Colaborador oColaborador);
        bool ActualizarClave(string codigo, string clave);
    }
}
