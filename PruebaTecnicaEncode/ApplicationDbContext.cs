using Microsoft.EntityFrameworkCore;
using PruebaTecnicaEncode.Entities;

namespace PruebaTecnicaEncode
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions options):base(options)
        {
            
        }

        public DbSet<Usuario> Usuarios { get; set; }
    }
}
