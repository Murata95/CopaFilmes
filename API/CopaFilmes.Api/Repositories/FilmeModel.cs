using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CopaFilmes.Api.Repositories
{
    public class FilmeModel
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("titulo")]
        public string Titulo { get; set; }
        [JsonProperty("ano")]
        public int Ano { get; set; }
        [JsonProperty("nota")]
        public double Nota { get; set; }
    }
}
