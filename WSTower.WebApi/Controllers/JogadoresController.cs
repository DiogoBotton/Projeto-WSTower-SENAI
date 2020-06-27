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
using WSTower.WebApi.Libraries;

namespace WSTower.WebApi.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
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

        [HttpGet("{id}")]
        public IActionResult InformacoesJogadorById(int id)
        {
            var jogador = jogadorRepository.GetById(id);

            if (jogador == null)
                return StatusCode(404, $"O objeto não existe na base de dados.");

            var selecao = selecaoRepository.GetById(jogador.SelecaoId);

            var infoJogador = new
            {
                Selecao = new
                {
                    selecao.Nome,
                    bandeiraPais = Tools.ToImage(selecao.Bandeira)
                },

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

        [HttpGet("selecao/{id}")]
        public IActionResult GetAllJogadoresBySelecaoId(int id)
        {
            var jogadores = jogadorRepository.GetAll();
            var selecoes = selecaoRepository.GetAll();

            //Retorna todos os jogadores de uma seleção
            var infoJogadores = new
            {
                Selecao = selecoes.First(y => y.Id == id).Nome,
                Jogadores = jogadores.Where(j => j.SelecaoId == id).Select(x => new
                {
                    Bandeira = Tools.ToImage((selecaoRepository.GetById(x.SelecaoId).Bandeira)),
                    Pais = selecaoRepository.GetById(x.SelecaoId).Nome,
                    Foto = Tools.ToImage(x.Foto),
                    Posicao = x.Posicao,
                    DataNascimento = x.Nascimento,
                    Idade = Tools.GetAge(x.Nascimento),
                    NumeroCamisa = x.Posicao == "Técnico" ? 0 : x.NumeroCamisa,
                    Sobre = new
                    {
                        Nome = x.Nome,
                        Sobre = x.Informacoes
                    },
                    Gols = x.Qtdegols,
                    CartoesAmarelos = x.QtdecartoesAmarelo,
                    CartoesVermelhos = x.QtdecartoesVermelho,
                    Faltas = x.Qtdefaltas
                })
            };

            return StatusCode(200, infoJogadores);
        }
    }
}
