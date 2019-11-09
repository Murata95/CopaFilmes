namespace CopaFilmes.Web.Models.Filme
{
    public class FilmeModel
    {
        public string Id { get; set; }

        public string Titulo { get; set; }

        public int Ano { get; set; }

        public double Nota { get; set; }

        public bool IsChecked { get; set; }
    }
}
