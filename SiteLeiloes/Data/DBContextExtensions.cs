using SiteLeiloes.Data;
using SiteLeiloes.Migrations;
using SiteLeiloes.Models;
using System;
using System.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace SiteLeiloes.Extensions
{
    public static class DbContextExtensions
    {
        public static void EnsureSeedData(this CarenseDBContext context)
        {
            if (!context.Utilizador.Any())
            {
                context.Utilizador.AddRange(
                    new Utilizador("user1", "user1mail", BCrypt.Net.BCrypt.HashPassword("user1pass")),
                    new Utilizador("user2", "user2mail", BCrypt.Net.BCrypt.HashPassword("user2pass")),
                    new Utilizador("user3", "user3mail", BCrypt.Net.BCrypt.HashPassword("user3pass"))
                );
            }

            context.SaveChanges();

            if (!context.Carro.Any())
            {
                var user1Id = context.Utilizador.Single(u => u.Username == "user1").Id;
                var user2Id = context.Utilizador.Single(u => u.Username == "user2").Id;
                var user3Id = context.Utilizador.Single(u => u.Username == "user3").Id;

                context.Carro.AddRange(
                    new Carro { UserId = user2Id, Marca = "Lamborghini", Modelo = "Urus V8", Ano = 2019, Km = 16600, Condicao = "novo", ImagemUrl = "/images/lamboUrus.png" },
                    new Carro { UserId = user2Id, Marca = "Nissan", Modelo = "GT-R", Ano = 2010, Km = 42000, Condicao = "usado", ImagemUrl = "/images/gtr.png" },
                    new Carro { UserId = user1Id, Marca = "Lamborghini", Modelo = "Huracan", Ano = 2019, Km = 15159, Condicao = "novo", ImagemUrl = "/images/lamboHuracan.png" },
                    new Carro { UserId = user3Id, Marca = "Ferrari", Modelo = "SF90", Ano = 2023, Km = 500, Condicao = "novo", ImagemUrl = "/images/ferrari.png" },
                    new Carro { UserId = user3Id, Marca = "Ferrari", Modelo = "456", Ano = 1994, Km = 83000, Condicao = "usado", ImagemUrl = "/images/ferrari456.png" },
                    new Carro { UserId = user2Id, Marca = "Austin", Modelo = "Mini", Ano = 1963, Km = 1000, Condicao = "novo", ImagemUrl = "/images/austinMini.png" },
                    new Carro { UserId = user2Id, Marca = "Porsche", Modelo = "911", Ano = 1985, Km = 153000, Condicao = "usado", ImagemUrl = "/images/porsche911.jpg" },
                    new Carro { UserId = user1Id, Marca = "BMW", Modelo = "E36 M3", Ano = 1991, Km = 123000, Condicao = "usado", ImagemUrl = "/images/BMW-E36-M3.jpg" },
                    new Carro { UserId = user1Id, Marca = "Mercedes", Modelo = "190evo", Ano = 1989, Km = 23000, Condicao = "usado", ImagemUrl = "/images/190evo.jpg" },
                    new Carro { UserId = user1Id, Marca = "Bugatti", Modelo = "Chiron", Ano = 2023, Km = 3000, Condicao = "novo", ImagemUrl = "/images/bugatti.jpg" },
                    new Carro { UserId = user1Id, Marca = "Opel", Modelo = "Corsa B", Ano = 1989, Km = 500000, Condicao = "usado", ImagemUrl = "/images/corsab.jpg" },
                    new Carro { UserId = user2Id, Marca = "BMW", Modelo = "e30", Ano = 1989, Km = 123000, Condicao = "usado", ImagemUrl = "/images/e30.jpg" },
                    new Carro { UserId = user1Id, Marca = "Ferrari", Modelo = "348", Ano = 1989, Km = 30000, Condicao = "usado", ImagemUrl = "/images/ferrari348.jpg" },
                    new Carro { UserId = user2Id, Marca = "Lancia", Modelo = "Integrale", Ano = 1989, Km = 30000, Condicao = "usado", ImagemUrl = "/images/lancia.jpg" }
                    );
            }

            context.SaveChanges();

            if (!context.Leilao.Any())
            {
                var user1Id = context.Utilizador.Single(u => u.Username == "user1").Id;
                var user2Id = context.Utilizador.Single(u => u.Username == "user2").Id;
                var user3Id = context.Utilizador.Single(u => u.Username == "user3").Id;

                var carro1Id = context.Carro.Single(c => c.Modelo == "Urus V8").Id;
                var carro2Id = context.Carro.Single(c => c.Modelo == "GT-R").Id;
                var carro3Id = context.Carro.Single(c => c.Modelo == "Huracan").Id;
                var carro4Id = context.Carro.Single(c => c.Modelo == "SF90").Id;
                var carro5Id = context.Carro.Single(c => c.Modelo == "456").Id;
                var carro6Id = context.Carro.Single(c => c.Modelo == "Mini").Id;
                var carro7Id = context.Carro.Single(c => c.Modelo == "911").Id;
                var carro8Id = context.Carro.Single(c => c.Modelo == "E36 M3").Id;

                

                context.Leilao.AddRange(
                    new Leilao
                    {
                        Preco_minimo = 150000.00f,
                        ClienteId = user3Id,
                        Valor = 150000.00f,
                        VendedorId = user3Id,
                        CarroId = carro4Id,
                        Data_de_inicio = DateTime.Now,
                        Data_de_fim = DateTime.Now.AddHours(24),
                        ImagemUrl = "/images/ferrari.png"
                    },
                    new Leilao
                    {
                        Preco_minimo = 200000.00f,
                        ClienteId = user3Id,
                        Valor = 200000.00f,
                        VendedorId = user3Id,
                        CarroId = carro5Id,
                        Data_de_inicio = DateTime.Now,
                        Data_de_fim = DateTime.Now.AddHours(24),
                        ImagemUrl = "/images/ferrari456.png"
                    },
                    new Leilao
                    {
                        Preco_minimo = 500.00f,
                        ClienteId = user2Id,
                        Valor = 600.00f,
                        VendedorId = user1Id,
                        CarroId = carro8Id,
                        Data_de_inicio = DateTime.Now,
                        Data_de_fim = DateTime.Now.AddHours(24),
                        ImagemUrl = "/images/BMW-E36-M3.jpg"
                    },
                    new Leilao
                    {
                        Preco_minimo = 500.00f,
                        ClienteId = user3Id,
                        Valor = 600.00f,
                        VendedorId = user2Id,
                        CarroId = carro1Id,
                        Data_de_inicio = DateTime.Now,
                        Data_de_fim = DateTime.Now.AddHours(24),
                        ImagemUrl = "/images/lamboUrus.png"
                    },
                    new Leilao
                    {
                        Preco_minimo = 500.00f,
                        ClienteId = user1Id,
                        Valor = 600.00f,
                        VendedorId = user2Id,
                        CarroId = carro2Id,
                        Data_de_inicio = DateTime.Now,
                        Data_de_fim = DateTime.Now.AddHours(24),
                        ImagemUrl = "/images/gtr.png"
                    },
                    new Leilao
                    {
                        Preco_minimo = 500.00f,
                        ClienteId = user3Id,
                        Valor = 600.00f,
                        VendedorId = user2Id,
                        CarroId = carro6Id,
                        Data_de_inicio = DateTime.Now.AddHours(40),
                        Data_de_fim = DateTime.Now.AddHours(60),
                        ImagemUrl = "/images/austinMini.png"
                    },
                    new Leilao
                    {
                        Preco_minimo = 500.00f,
                        ClienteId = user1Id,
                        Valor = 600.00f,
                        VendedorId = user2Id,
                        CarroId = carro7Id,
                        Data_de_inicio = DateTime.Now.AddHours(2),
                        Data_de_fim = DateTime.Now.AddHours(24),
                        ImagemUrl = "/images/porsche911.jpg"
                    }
                );
            }

            context.SaveChanges();

            if (!context.LeilaoFavorito.Any())
            {
                var user1Id = context.Utilizador.Single(u => u.Username == "user1").Id;
                var user2Id = context.Utilizador.Single(u => u.Username == "user2").Id;
                var user3Id = context.Utilizador.Single(u => u.Username == "user3").Id;

                var leilao3Id = context.Leilao.Single(l => l.ImagemUrl == "/images/BMW-E36-M3.jpg").Id;
                var leilao7Id = context.Leilao.Single(l => l.ImagemUrl == "/images/austinMini.png").Id;              

                context.LeilaoFavorito.AddRange(
                    new LeilaoFavorito(leilao3Id, user1Id),
                    new LeilaoFavorito(leilao7Id, user1Id)
                );
            }
            context.SaveChanges();

            if (!context.Licitacao.Any())
            {
                var user1Id = context.Utilizador.Single(u => u.Username == "user1").Id;
                var leilao2Id = context.Leilao.Single(l => l.ImagemUrl == "/images/lamboUrus.png").Id;
                var leilao3Id = context.Leilao.Single(l => l.ImagemUrl == "/images/gtr.png").Id;

                context.Licitacao.AddRange(
                    new Licitacao(leilao2Id, user1Id),
                    new Licitacao(leilao3Id, user1Id)
                );
            }
            
            context.SaveChanges();

            if(!context.Venda.Any())
            {
                var user1Id = context.Utilizador.Single(u => u.Username == "user1").Id;
                var user2Id = context.Utilizador.Single(u => u.Username == "user2").Id;

                var car1 = context.Carro.Single(c => c.Modelo == "e30").Id;
                var car2 = context.Carro.Single(c => c.Modelo == "Chiron").Id;

                context.Venda.AddRange(
                    new Venda(DateTime.Now, 160000.00f, user1Id, user2Id, car1),
                    new Venda(DateTime.Now, 90000.00f, user2Id, user1Id, car2));
            }

            if (!context.Administrador.Any())
            {
                context.Administrador.AddRange(
                    new Administrador("admin", "admin")
                );
            }

            context.SaveChanges();
        }
    }
}
