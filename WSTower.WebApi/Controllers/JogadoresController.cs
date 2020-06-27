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
using System.Security.Cryptography.X509Certificates;

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
                    bandeiraPais = selecao.Bandeira == null ? null : Tools.ToImage(selecao.Bandeira)
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
            var jogadores = jogadorRepository.GetByTeam(id);
            var selecao = selecaoRepository.GetById(id);

            if (selecao == null)
                return StatusCode(404, "Esta seleção não existe.");

            //Retorna todos os jogadores de uma seleção
            var infoJogadores = new
            {
                Selecao = new
                {
                    selecao.Nome,
                    Bandeira = selecao.Bandeira == null ? null : Tools.ToImage(selecao.Bandeira),
                },
                Jogadores = jogadores.Select(x => new
                {
                    Foto = x.Foto == null ? null : Tools.ToImage(x.Foto),
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
