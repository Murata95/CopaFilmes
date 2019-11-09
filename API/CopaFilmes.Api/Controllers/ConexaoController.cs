using CopaFilmes.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CopaFilmes.Api.Controllers
{
    public class ConexaoController : Microsoft.AspNetCore.Mvc.Controller
    {
        public F GetProxy<F>(string json)
        {
            return JsonConvert.DeserializeObject<F>(json);
        }

        protected async Task<IActionResult> RunDefaultAsync(Func<Task<IActionResult>> predicate)
        {
            try
            {
                return await predicate();
            }
            catch (System.Exception exception)
            {
                return StatusCode(500, new Error
                {
                    Codigo = 0,
                    Mensagem = exception.Message
                });
            }
        }

    }
}
