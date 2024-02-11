namespace SiteLeiloes.Models
{
    public class LeilaoViewModel
    {
        public int Id { get; set; }
        public float Preco_minimo { get; set; }
        public int ClienteId { get; set; }
        public float Valor { get; set; }
        public int VendedorId { get; set; }
        public int CarroId { get; set; }
        public DateTime Data_de_inicio { get; set; }
        public DateTime Data_de_fim { get; set; }
        public string ImagemUrl { get; set; }
        public List<int> Licitadores { get; set; }


        public string MarcaCarro { get; set; }
        public string ModeloCarro { get; set; }
        public int AnoCarro { get; set; }
        public int KmCarro { get; set; }
        public string CondicaoCarro { get; set; }
        public string ImagemCarro { get; set; }

    }
}
