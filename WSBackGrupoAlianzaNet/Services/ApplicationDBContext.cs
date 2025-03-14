using Microsoft.EntityFrameworkCore;
using WSBackGrupoAlianzaNet.Models;

namespace WSBackGrupoAlianzaNet.Services
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
        }

        public required DbSet<Productos> Productos { get; set; } = null!;
        public required DbSet<Usuarios> Usuarios { get; set; } = null!;
    }
}