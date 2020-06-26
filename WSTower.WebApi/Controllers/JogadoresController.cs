using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using WSTower.WebApi.Interfaces;
using WSTower.WebApi.Repositories;
using System.Reflection.Metadata;
using WSTower.WebApi.Domains;

namespace WSTower.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JogadoresController : ControllerBase
    {
        public IJogadorRepository jogadorRepository { get; set; }
        public ISelecaoRepository selecaoRepository { get; set; }

        public JogadoresController()
        {
            jogadorRepository = new JogadorRepository();
            selecaoRepository = new SelecaoRepository();
        }

        [HttpGet("info-jogador/{id}")]
        public IActionResult InformacoesJogadorById(int id)
        {
            var jogador = jogadorRepository.GetById(id);

            if (jogador == null)
                return StatusCode(404, $"Id {id} de jogador não existe no banco de dados.");

            var selecao = selecaoRepository.GetById(jogador.SelecaoId);

            var infoJogador = new
            {
                bandeiraPais = "classe static Image não funciona, binário fica feio então vou deixar assim mesmo infelizmente... decepcionado com o visual studio",
                Selecao = selecao.Nome,
                Nome = jogador.Nome,
                Nascimento = jogador.Nascimento.ToShortDateString(),
                NumeroCamisa = jogador.NumeroCamisa,
                Posicao = jogador.Posicao,
                CartoesAmarelos = jogador.QtdecartoesAmarelo,
                CartoesVermelhos = jogador.QtdecartoesVermelho,
                Faltas = jogador.Qtdefaltas,
                Gols = jogador.Qtdegols,
                Informacoes = jogador.Informacoes,
            };

            return StatusCode(200, infoJogador);
        }

        [HttpGet("jogadores-selecao/{selecaoId}")]
        public IActionResult GetAllJogadoresBySelecaoId(int selecaoId)
        {
            var jogadores = jogadorRepository.GetAll();
            var selecoes = selecaoRepository.GetAll();

            //Retorna todos os jogadores de uma seleção
            var infoJogadores = new
            {
                Selecao = selecoes.FirstOrDefault(y => y.Id == selecaoId).Nome,
                Jogador = jogadores.Where(j => j.SelecaoId == selecaoId),
            };

            return StatusCode(200, infoJogadores);
        }
    }
}
