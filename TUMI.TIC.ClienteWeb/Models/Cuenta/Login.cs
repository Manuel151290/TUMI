using System.ComponentModel.DataAnnotations;

namespace TUMI.TIC.ClienteWeb.Models.Cuenta
{
    public class Login
    {
        [Required]
        [StringLength(50, ErrorMessage = "Ingrese un usuario válido.")]
        [DataType(DataType.Text)]
        public string Usuario { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Clave { get; set; }
    }
}
