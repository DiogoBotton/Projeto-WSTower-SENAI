using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WSTower.WebApi.Libraries;
using WSTower.WebApi.Repositories;

namespace WSTower.WebApi.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class SelecoesController : ControllerBase
    {
        private SelecaoRepository _selecaoRepository { get; set; }
        public SelecoesController()
        {
            _selecaoRepository = new SelecaoRepository();
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var jogos = _selecaoRepository.GetAll();

                if (jogos == null)
                    return StatusCode(204, "Objeto não encontrado na base de dados");

                var jogosViewModel = jogos.Select(x => new
                {
                    Nome = x.Nome,
                    Bandeira = Tools.ToImage(x.Bandeira),
                    Jogadores = new JogadorRepository().GetByTeam(x.Id).Select(y => new
                    {
                        Foto = Tools.ToImage(y.Foto),
                        Nome = y.Nome,
                        Posicao = y.Posicao
                    })
                });

                return StatusCode(200, jogosViewModel);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
