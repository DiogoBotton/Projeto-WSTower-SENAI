using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WSTower.WebApi.Domains;
using WSTower.WebApi.Interfaces;
using WSTower.WebApi.Repositories;

namespace WSTower.WebApi.Controllers
{
    // Define que o tipo de resposta da API será no formato JSON
    [Produces("application/json")]

    // Define que a rota de uma requisição será no formato domínio/api/NomeController
    [Route("api/[controller]")]

    // Define que é um controlador de API
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private IUsuarioRepository _usuarioRepository;

        public UsuarioController()
        {
            _usuarioRepository = new UsuarioRepository();
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_usuarioRepository.ListarUsuario());
        }

        [HttpPost]
        public IActionResult Post(Usuario novoUsuario)
        {
            _usuarioRepository.CadastrarUsuario(novoUsuario);

            // Retorna um status code
            return StatusCode(201);
        }
        [HttpPut("{id}")]
        public IActionResult Put(int id, Usuario usuarioAtualizado)
        {
            // Faz a chamada para o método
            _usuarioRepository.Atualizar(id, usuarioAtualizado);

            // Retorna um status code
            return StatusCode(204);
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            // Retora a resposta da requisição fazendo a chamada para o método
            return Ok(_usuarioRepository.BuscarPorId(id));
        }

    }
}
