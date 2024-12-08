using System.Collections.Generic;
using TUMI.TIC.Dominio.Contratos;
using TUMI.TIC.Modelo.Entidades;
using TUMI.TIC.Repositorio.Contratos;
using System.Web;


namespace TUMI.TIC.Dominio
{
    public class ColaboradorDominio : IColaboradorDominio
    {
        #region Variables
        private readonly IColaboradorRepositorio _repositorio;
        #endregion

        #region Constructor
        public ColaboradorDominio(IColaboradorRepositorio repositorio)
        {
            _repositorio = repositorio;
        }
        #endregion

        #region Métodos
        public List<Colaborador> ListarColaboradores()
        {
            List<Colaborador> oLista = new List<Colaborador>();
            oLista = _repositorio.ListarColaboradores();
            return oLista;
        }

        public Colaborador ObtenerDatos(string codigo, string clave, out bool estado)
        {
            Colaborador oColaborador = new Colaborador();
            oColaborador = _repositorio.ObtenerDatos(codigo, clave, out estado);
            return oColaborador;
        }
        public Colaborador TraerUno(string codigo) 
        { 
            Colaborador oColaborador = new Colaborador();  
            oColaborador = _repositorio.TraerUno(codigo);
            return oColaborador;
        }

        public bool ActualizarColaborador(Colaborador oColaborador)
        {
            bool resultado = false;
            resultado = _repositorio.ActualizarColaborador(oColaborador);
            return resultado;
        }
        public bool RegistrarColaborador(Colaborador oColaborador)
        {
            bool resultado = false;
            resultado = _repositorio.RegistrarColaborador(oColaborador);
            return resultado;
        }

        public bool ActualizarClave(string codigo, string clave)
        {
            bool resultado = false;
            resultado = _repositorio.ActualizarClave(codigo, clave);
            return resultado;
        }
        #endregion
    }
}
