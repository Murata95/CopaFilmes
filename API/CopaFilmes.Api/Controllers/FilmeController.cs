using CopaFilmes.Api.Models;
using CopaFilmes.Api.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CopaFilmes.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ProducesResponseType(500)]
    public class FilmeController : ConexaoController
    {

        private readonly FilmesService _service;

        public FilmeController(FilmesService service)
        {
            _service = service;
        }

        [ProducesResponseType(typeof(IEnumerable<Filme>), 200)]
        [HttpGet]
        public Task<IActionResult> GetAll()
        {
            return RunDefaultAsync(async () => Ok(await _service.GetAsync()));
        }

        [ProducesResponseType(typeof(IEnumerable<Filme>), 200)]
        [HttpGet("copa")]
        public Task<IActionResult> GetWinners([FromQuery] string[] ids)
        {
            return RunDefaultAsync(async () => Ok(await _service.GetSelectAsync(ids)));
        }

    }
}
