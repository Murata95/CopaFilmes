using CopaFilmes.Web.Api;
using CopaFilmes.Web.Models.Filme;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace CopaFilmes.Web.Controllers
{
    public class FilmeController : Controller
    {
        private readonly IFilmeApi _api;

        public FilmeController(IFilmeApi api)
        {
            _api = api;
        }

        public IActionResult Index()
        {
            ViewData["Title"] = "Fase de Seleção";
            ViewData["SubTitle"] = "Selecione 8 filmes que você deseja que entrem na competição e depois pressione o botão Gerar Meu Campeonato para prosseguir.";

            return View();
        }

        public async Task<ApiResponse<IEnumerable<FilmeModel>>> GetFilmes()
        {
            var result = new ApiResponse<IEnumerable<FilmeModel>>();
            try
            {
                using (var proxy = await _api.GetFilmes())
                {
                    switch (proxy.ResponseMessage.StatusCode)
                    {
                        case HttpStatusCode.OK:
                            var filmes = proxy.GetContent();
                            result.Data = filmes;
                            return result;
                        default:
                            result.Error = new ApiError("1.100", ApiError.ErrorMessage);
                            return result;
                    }
                }
            }
            catch (Exception)
            {
                result.Error = new ApiError("1.101", ApiError.ErrorMessage);
            }

            return result;
        }

        public IActionResult Vencedor()
        {
            ViewData["Title"] = "Resultado Final";
            ViewData["SubTitle"] = "Veja o resultado final do Campeonato de filmes de forma simples e rápida.";
            return View();
        }

        public async Task<ApiResponse<IEnumerable<FilmeModel>>> Copa(string[] ids)
        {
            var result = new ApiResponse<IEnumerable<FilmeModel>>();
            try
            {
                using (var proxy = await _api.GetVencedores(ids))
                {
                    switch (proxy.ResponseMessage.StatusCode)
                    {
                        case HttpStatusCode.OK:
                            var filmes = proxy.GetContent();
                            result.Data = filmes;
                            return result;
                        default:
                            result.Error = new ApiError("2.100", ApiError.ErrorMessage);
                            return result;
                    }
                }
            }
            catch (Exception)
            {
                result.Error = new ApiError("2.101", ApiError.ErrorMessage);
            }

            return result;
        }
    }
}