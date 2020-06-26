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

        
        public List<Usuario> ListarUsuario()
        {

            //Retorno uma lista de Usuarios
            return ctx.Usuario.ToList();
        }

        public void CadastrarUsuario(Usuario novoUsuario)
        {

            //Adiciona um novo Usuario
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
      

    }
}
