using AprendendoAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AprendendoAPI.Infraestrutura
{
    public class ConnectionContext : DbContext
    {
        //esqueleto base para conexão com DB
        public DbSet<Produto> Produto { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql(
                "Server=localhost;" +
                "Port=5432;Database=teste_produtos;" +
                "User Id=postgres;" +
                "Password=1234;");
    }
}
