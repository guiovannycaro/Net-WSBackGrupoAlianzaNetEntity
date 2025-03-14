using System.ComponentModel.DataAnnotations;

namespace WSBackGrupoAlianzaNet.Models
{
    public class Usuarios
    {
        [Key]
        public int idusuario { get; set; }

        [Display(Name = "Nombre")]
        public string nombre { get; set; }
        [Display(Name = "tipo Documento")]
        public int tipodocumento { get; set; }

        public int numerodocumento { get; set; }
        public int rol { get; set; }

        public string email { get; set; }
        public string password { get; set; }


    }
}
