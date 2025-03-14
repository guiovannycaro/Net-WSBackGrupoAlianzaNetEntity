using System.ComponentModel.DataAnnotations;

namespace WSBackGrupoAlianzaNet.Models
{
    public class UsuariosDto
    {
        [Required]
        [MaxLength(500)]
        public string nombre { get; set; }
        [Required]
        public int tipodocumento { get; set; }
        [Required]
        public int numerodocumento { get; set; }
        [Required]
        public int rol { get; set; }
        [Required]
        public string email { get; set; }
        [Required]
        public string password { get; set; }


    }
}
