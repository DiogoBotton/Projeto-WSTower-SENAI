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
            return ctx.Usuario.ToList();
        }

        public void CadastrarUsuario(Usuario novoUsuario)
        {
            ctx.Usuario.Add(novoUsuario);

            ctx.SaveChanges();
        }
        public Usuario BuscarPorId(int id)
        {
            
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
