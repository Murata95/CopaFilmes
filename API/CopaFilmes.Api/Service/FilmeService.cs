using CopaFilmes.Api.Models;
using CopaFilmes.Api.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace CopaFilmes.Api.Service
{
    public class FilmesService
    {
        private readonly IConexao _conectado;

        public FilmesService(IConexao conectado)
        {
            _conectado = conectado;
        }

        public async Task<IEnumerable<Filme>> GetAsync()
        {
            return await GetfromApi();
        }

        public async Task<IEnumerable<Filme>> GetSelectAsync(string[] ids)
        {
            var filmes = await GetfromApi();

            if (filmes == null) throw new System.Exception("Erro na API.");

            var filmesApi = filmes.Select(o => o.Id);
            if (!ids.Any(o => filmesApi.Contains(o)))
                throw new System.Exception("Invalid ids.");

            // order movies
            filmes = filmes.Where(o => ids.Contains(o.Id)).OrderBy(o => o.Titulo);

            return RodarCopa(filmes);
        }

        public async Task<IEnumerable<Filme>> GetfromApi()
        {
            using (var proxy = await _conectado.GetListaFilmes())
            {
                switch (proxy.ResponseMessage.StatusCode)
                {
                    case HttpStatusCode.OK:
                        var movies = proxy.GetContent();
                        return movies.Select(o => new Filme
                        {
                            Id = o.Id,
                            Ano = o.Ano,
                            Titulo = o.Titulo,
                            Nota = o.Nota
                        });
                    default:
                        return null;
                }
            }
        }

        public IEnumerable<Filme> RodarCopa(IEnumerable<Filme> filmes)
        {
            var lista = new List<Filme>();
            var count = filmes.Count();

            for (var i = 0; i < count / 2; i++)
            {
                var filme1 = filmes.ElementAt(i);
                var filme2 = filmes.ElementAt(count - i - 1);

                lista.Add(CompareFilmes(filme1, filme2));
            }

            while (lista.Count > 1)
            {
                filmes = lista;
                count = lista.Count();
                lista = new List<Filme>();

                for (var i = 0; i < count / 2; i++)
                {
                    var filme1 = filmes.ElementAt(i * 2);
                    var filme2 = filmes.ElementAt(i * 2 + 1);

                    lista.Add(CompareFilmes(filme1, filme2));
                }
            }

            if (lista.Count == 1)
            {
                var second = lista.ElementAt(0) == filmes.ElementAt(0) ? filmes.ElementAt(1) : filmes.ElementAt(0);
                lista.Add(second);
            }
            return lista;
        }

        public Filme CompareFilmes(Filme filme1, Filme filme2)
        {
            if (filme1.Nota > filme2.Nota)
                return filme1;
            else if (filme1.Nota < filme2.Nota)
                return filme2;
            else
                return string.Compare(filme1.Titulo, filme2.Titulo, StringComparison.Ordinal) < 0 ? filme1 : filme2;
        }
    }
}

