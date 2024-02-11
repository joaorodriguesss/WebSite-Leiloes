using Microsoft.EntityFrameworkCore;
using SiteLeiloes.Models;

namespace SiteLeiloes.Data
{
    public class CarenseDBContext : DbContext
    {
        public CarenseDBContext(DbContextOptions<CarenseDBContext> opt): base(opt){
        }
        public DbSet<Administrador> Administrador { get; set; }
        public DbSet<Carro> Carro { get; set; }
        public DbSet<Leilao> Leilao { get; set; }
        public DbSet<LeilaoFavorito> LeilaoFavorito { get; set; }
        public DbSet<Utilizador> Utilizador { get; set; }
        public DbSet<Venda> Venda { get; set; }
        public DbSet<Licitacao> Licitacao { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Cada Leilao está associado a um Cliente (Utilizador)
            modelBuilder.Entity<Leilao>()
                .HasOne(l => l.Cliente)
                .WithMany() 
                .HasForeignKey(l => l.ClienteId)
                .OnDelete(DeleteBehavior.Restrict); 

            // Cada Leilao está associado a um Vendedor (Utilizador)
            modelBuilder.Entity<Leilao>()
                .HasOne(l => l.Vendedor)
                .WithMany() 
                .HasForeignKey(l => l.VendedorId)
                .OnDelete(DeleteBehavior.Restrict); 

            // Cada Leilao está associado a um Carro
            modelBuilder.Entity<Leilao>()
                .HasOne(l => l.Carro)
                .WithMany() 
                .HasForeignKey(l => l.CarroId)
                .OnDelete(DeleteBehavior.Restrict);


            // Configurando as relações para LeilaoFavorito
            modelBuilder.Entity<LeilaoFavorito>()
                .HasOne(lf => lf.Leilao)
                .WithMany() 
                .HasForeignKey(lf => lf.LeilaoId)
                .OnDelete(DeleteBehavior.Restrict); 

            modelBuilder.Entity<LeilaoFavorito>()
                .HasOne(lf => lf.Utilizador)
                .WithMany() 
                .HasForeignKey(lf => lf.UtilizadorId)
                .OnDelete(DeleteBehavior.Restrict); 


            // Configurando as relações para Venda
            modelBuilder.Entity<Venda>()
                .HasOne(v => v.Vendedor)
                .WithMany() 
                .HasForeignKey(v => v.VendedorId)
                .OnDelete(DeleteBehavior.Restrict); 

            modelBuilder.Entity<Venda>()
                .HasOne(v => v.Cliente)
                .WithMany() 
                .HasForeignKey(v => v.ClienteId)
                .OnDelete(DeleteBehavior.Restrict); 

            modelBuilder.Entity<Venda>()
                .HasOne(v => v.Carro)
                .WithMany() 
                .HasForeignKey(v => v.CarroId)
                .OnDelete(DeleteBehavior.Restrict); 

            modelBuilder.Entity<Licitacao>()
                .HasOne(l => l.Leilao)
                .WithMany() 
                .HasForeignKey(l => l.LeilaoId)
                .OnDelete(DeleteBehavior.Restrict); 

            modelBuilder.Entity<Licitacao>()
                .HasOne(l => l.Utilizador)
                .WithMany() 
                .HasForeignKey(l => l.UtilizadorId)
                .OnDelete(DeleteBehavior.Restrict); 
        }

    }
}
