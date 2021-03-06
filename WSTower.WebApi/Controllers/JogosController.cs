﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using WSTower.WebApi.Domains;
using WSTower.WebApi.Libraries;
using WSTower.WebApi.Repositories;
using WSTower.WebApi.ViewModels;

namespace WSTower.WebApi.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class JogosController : ControllerBase
    {
        private JogoRepository _jogoRepository { get; set; }
        private SelecaoRepository _selecaoRepository { get; set; }
        private JogadorRepository _jogadorRepository { get; set; }

        public JogosController()
        {
            _selecaoRepository = new SelecaoRepository();
            _jogadorRepository = new JogadorRepository();
            _jogoRepository = new JogoRepository();
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                IEnumerable<Jogo> jogos;
                if ((jogos = _jogoRepository.GetAll()) == null)
                    return StatusCode(204, "Objeto não encontrado na base de dados");

                var jogosViewModel = jogos.Select(x => new
                {
                    Data = x.Data,
                    Estadio = x.Estadio,
                    SelecaoCasa = _selecaoRepository.GetById(x.SelecaoCasa).Nome,
                    SelecaoVisitante = _selecaoRepository.GetById(x.SelecaoVisitante).Nome,
                    PlacarFinal = $"{x.PlacarCasa} X {x.PlacarVisitante}",
                    Penaltis = (x.PlacarCasa + x.PlacarVisitante) == 0 ? "0" : $"{x.PenaltisCasa} X {x.PenaltisVisitante}"
                });

                return StatusCode(200, jogosViewModel);
            }
            catch (Exception e)
            {
                return StatusCode(400, e);
            }
        }

        [HttpGet("estadio/{nomeDoEstadio}")]
        public IActionResult GetByStadium(string nomeDoEstadio)
        {
            try
            {
                IEnumerable<Jogo> jogos;
                if ((jogos = _jogoRepository.GetByStadium(nomeDoEstadio)) == null)
                    return StatusCode(204, "Objeto não encontrado na base de dados");

                var jogosViewModel = jogos.Select(x => new
                {
                    Data = x.Data,
                    Estadio = x.Estadio,
                    SelecaoCasa = _selecaoRepository.GetById(x.SelecaoCasa).Nome,
                    SelecaoVisitante = _selecaoRepository.GetById(x.SelecaoVisitante).Nome,
                    PlacarFinal = $"{x.PlacarCasa} X {x.PlacarVisitante}",
                    Penaltis = (x.PlacarCasa + x.PlacarVisitante) == 0 ? "0" : $"{x.PenaltisCasa} X {x.PenaltisVisitante}"
                });

                return StatusCode(200, jogosViewModel);
            }
            catch (Exception e)
            {
                return StatusCode(400, e);
            }
        }

        [HttpGet("selecao/{nomeDaSelecao}")]
        public IActionResult GetByTeamName(string nomeDaSelecao)
        {
            try
            {
                IEnumerable<Jogo> jogos;
                if ((jogos = _jogoRepository.GetByTeam(nomeDaSelecao)) == null)
                    return StatusCode(204, "Objeto não encontrado na base de dados");

                var jogosViewModel = jogos.Select(x => new
                {
                    Data = x.Data,
                    Estadio = x.Estadio,
                    SelecaoCasa = _selecaoRepository.GetById(x.SelecaoCasa).Nome,
                    SelecaoVisitante = _selecaoRepository.GetById(x.SelecaoVisitante).Nome,
                    PlacarFinal = $"{x.PlacarCasa} X {x.PlacarVisitante}",
                    Penaltis = (x.PlacarCasa + x.PlacarVisitante) == 0 ? "0" : $"{x.PenaltisCasa} X {x.PenaltisVisitante}"
                }); ;

                return StatusCode(200, jogosViewModel);
            }
            catch (Exception e)
            {
                return StatusCode(400, e);
            }
        }

        [HttpGet("data/{date}")]
        public IActionResult GetByDate(string date)
        {
            try
            {
                IEnumerable<Jogo> jogos;
                if ((jogos = _jogoRepository.GetByDate(DateTime.Parse(date))) == null)
                    return StatusCode(204, "Objeto não encontrado na base de dados");

                var jogosViewModel = jogos.Select(x => new
                {
                    Data = x.Data,
                    Estadio = x.Estadio,
                    SelecaoCasa = _selecaoRepository.GetById(x.SelecaoCasa),
                    SelecaoVisitante = _selecaoRepository.GetById(x.SelecaoVisitante),
                    PlacarFinal = $"{x.PlacarCasa} X {x.PlacarVisitante}",
                    Penaltis = (x.PlacarCasa + x.PlacarVisitante) == 0 ? "0" : $"{x.PenaltisCasa} X {x.PenaltisVisitante}"
                });

                return StatusCode(200, jogosViewModel);
            }
            catch (Exception e)
            {
                return StatusCode(400, e);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                Jogo jogo;
                if ((jogo = _jogoRepository.GetById(id)) == null)
                    return StatusCode(204, "Objeto não encontrado na base de dados");

                var selecaoCasa = _selecaoRepository.GetById(jogo.SelecaoCasa);
                var selecaoVisitante = _selecaoRepository.GetById(jogo.SelecaoVisitante);

                var jogadoresCasa = _jogadorRepository.GetByTeam(selecaoCasa.Id)
                    .OrderBy(x => x.Posicao)
                    .ThenBy(x => x.NumeroCamisa);

                var jogadoresVisitante = _jogadorRepository.GetByTeam(selecaoCasa.Id)
                    .OrderBy(x => x.NumeroCamisa)
                    .ThenBy(x => x.Posicao);

                var jogoViewModel = new JogoViewModel
                {
                    SelecaoCasa = new SelecaoViewModel
                    {
                        Nome = selecaoCasa.Nome,
                        Uniforme = selecaoCasa.Uniforme,
                        Jogadores = from jogador in jogadoresCasa
                                    select new JogadorViewModel
                                    {
                                        Nome = jogador.Nome,
                                        Foto = Tools.ToImage(jogador.Foto)
                                    }
                    },
                    SelecaoVisitante = new SelecaoViewModel
                    {
                        Nome = selecaoVisitante.Nome,
                        Jogadores = from jogador in jogadoresVisitante
                                    select new JogadorViewModel
                                    {
                                        Nome = jogador.Nome,
                                        Foto = Tools.ToImage(jogador.Foto)
                                    }
                    },
                    PlacarCasa = jogo.PlacarCasa,
                    PlacarVisitante = jogo.PlacarVisitante
                };
                return StatusCode(200, jogoViewModel);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}