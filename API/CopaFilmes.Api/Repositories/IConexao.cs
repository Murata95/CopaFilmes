using RestEase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CopaFilmes.Api.Repositories
{
    [AllowAnyStatusCode]
    public interface IConexao
    {
        [Get("filmes")]
        Task<Response<IEnumerable<FilmeModel>>> GetListaFilmes();
    }
}
