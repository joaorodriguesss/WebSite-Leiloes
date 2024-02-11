namespace SiteLeiloes.Models
{
    public class Carro
    {
        public int Id { get; set; }
        public int UserId { get; set; } 
        public string Marca { get; set; }  = ""; 
        public string Modelo { get; set; } = "";
        public int Ano { get;  set; }
        public int Km { get;  set; }
        public string Condicao { get;  set; } = "";
        public string ImagemUrl { get; set; } 

        public Carro()
        {
        }
        public Carro(int id, int user_id, string marca, string modelo, int ano, int km, string condicao, string imagemUrl)
        {
            Id = id;
            UserId = user_id;
            Marca = marca;
            Modelo = modelo;
            Ano = ano;
            Km = km;
            Condicao = condicao;
            ImagemUrl = imagemUrl; 
        }
    }
}
