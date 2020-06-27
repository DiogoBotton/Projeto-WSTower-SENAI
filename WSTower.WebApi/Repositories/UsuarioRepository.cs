using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WSTower.WebApi.Domains;
using WSTower.WebApi.Interfaces;

namespace WSTower.WebApi.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        WSTowerContext ctx = new WSTowerContext();


        public IEnumerable<Usuario> ListarUsuario()

        {

            //Retorno uma lista de Usuarios
            return ctx.Usuario.ToList();
        }

        public void CadastrarUsuario(Usuario novoUsuario)
        {
            if (UserExists(novoUsuario))
                throw new ArgumentException("Email ou apelido já existe no sistema");


            if (!UserIsValid(novoUsuario))
                throw new ArgumentException("Nome, apelido e senha devem ter ao menos 3 caracteres.");
                
            ctx.Usuario.Add(novoUsuario);

            //Salva as informacoes
            ctx.SaveChanges();
        }


        public Usuario BuscarPorId(int id)
        {


            //Busca o usuario pelo seu ID
            return ctx.Usuario.FirstOrDefault(j => j.Id == id);
        }

        //O usuário poderá alterar os dados do seu perfil.
        public void Atualizar(int id, Usuario usuarioAtualizado)
        {
            if (UserExists(usuarioAtualizado))
                throw new ArgumentException("Email ou apelido já existe no sistema");

            if (!UserIsValid(usuarioAtualizado))
                throw new ArgumentException("Nome, apelido e senha devem ter ao menos 3 caracteres.");

            Usuario usuarioBuscado = ctx.Usuario.Find(id);

            // Atribui os novos valores ao campos existentes
            usuarioBuscado.Nome = usuarioAtualizado.Nome;
            usuarioBuscado.Apelido = usuarioAtualizado.Apelido;
            usuarioBuscado.Email = usuarioAtualizado.Email;
            usuarioBuscado.Foto = usuarioAtualizado.Foto;
            usuarioBuscado.Senha = usuarioAtualizado.Senha;

            ctx.Usuario.Update(usuarioBuscado);

            ctx.SaveChanges();
        }

        private bool UserExists(Usuario novoUsuario)
        {
            if (ctx.Usuario.Where(x => x.Email == novoUsuario.Email || x.Apelido == novoUsuario.Apelido).Count() == 0)
                return false;

            return true;
        }

        private bool UserIsValid(Usuario user)
        {
            if (user.Nome.Length > 3 && user.Apelido.Length > 3 && user.Senha.Length > 3)
                return true;

            return false;
        }
    }
}
