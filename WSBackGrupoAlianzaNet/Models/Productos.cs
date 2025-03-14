using System.ComponentModel.DataAnnotations;

namespace WSBackGrupoAlianzaNet.Models
{
    public class Productos
    {
        [Key]
        public int idproducto { get; set; }
        public string nombre { get; set; }

        public string descripcion { get; set; }

        public int precio { get; set; }

        public int stock { get; set; }
    }
}
