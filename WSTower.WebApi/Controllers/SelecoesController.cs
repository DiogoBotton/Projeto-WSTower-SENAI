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
                var selecoes = _selecaoRepository.GetAll();

                if (selecoes == null)
                    return StatusCode(204, "Objeto não encontrado na base de dados");

                var jogos = new JogoRepository().GetAll();

                jogos.ForEach((x) =>
                {
                    
                });

                var jogosViewModel = selecoes.Select(x => new
                {
                    Nome = x.Nome,
                    Bandeira = Tools.ToImage(x.Bandeira),
                    Jogadores = new JogadorRepository().GetByTeam(x.Id).Select(y => new
                    {
                        Foto = y.Foto == null ? null : Tools.ToImage(y.Foto),
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
