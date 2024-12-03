using Microsoft.EntityFrameworkCore;

namespace ApiEstudante;

public class AppDbContext : DbContext
{
    public DbSet<Estudante> estudante {get; set;}
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
            // Configura a string de conexão aqui
            optionsBuilder.UseMySQL("Server=localhost;Database=ESTUDANTES;User=root;Password=Monoahri321?;");

            base.OnConfiguring(optionsBuilder); //mantem um padrao de configuração
    }
}


