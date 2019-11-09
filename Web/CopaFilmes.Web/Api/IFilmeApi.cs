using Microsoft.AspNetCore.Mvc;
using CopaFilmes.Web.Models.Filme;
using RestEase;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CopaFilmes.Web.Api
{
    [AllowAnyStatusCode]
    public interface IFilmeApi
    {
        [Get("Filme")]
        Task<Response<List<FilmeModel>>> GetFilmes();

        [Get("Filme/copa")]
        Task<Response<List<FilmeModel>>> GetVencedores([FromQuery] string[] ids);        
    }
}
