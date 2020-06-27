using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using WSTower.WebApi.Interfaces;
using WSTower.WebApi.Repositories;
using WSTower.WebApi.ViewModels;

namespace WSTower.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        public ILoginRepository loginRepository;

        public LoginController()
        {
            loginRepository = new LoginRepository();
        }

        [HttpGet]
        public IActionResult Login(LoginViewModel usuario)
        {
            try
            {
                var retorno = loginRepository.Login(usuario);

                if (retorno == null)
                    return StatusCode(404, "Usuário ou senha inválidos.");

                var informacoesUsuario = new[]
                {
                     new Claim(JwtRegisteredClaimNames.Email, retorno.Email),
                     new Claim(JwtRegisteredClaimNames.UniqueName, retorno.Apelido),
                     new Claim(JwtRegisteredClaimNames.Jti, retorno.Id.ToString()),
                };

                //Define a chave de acesso ao token
                var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("wstower-chave-autentificacao"));

                //Define as credenciais do token
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                //Gera o token
                var token = new JwtSecurityToken(
                    issuer: "WSTower.WebApi",
                    audience: "WSTower.WebApi",
                    claims: informacoesUsuario,
                    expires: DateTime.Now.AddMinutes(30),
                    signingCredentials: creds
                );

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token)
                });

            }
            catch (Exception)
            {
                return StatusCode(404, "Ocorreu um erro na tentativa de login do usuário.");
            }
        }
    }
}
