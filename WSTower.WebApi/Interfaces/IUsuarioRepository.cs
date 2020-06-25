using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WSTower.WebApi.Domains;

namespace WSTower.WebApi.Interfaces
{
    interface IUsuarioRepository
    {
        void CadastrarUsuario(Usuario novoUsuario);

        List<Usuario> ListarUsuario();

        void Atualizar(int id, Usuario usuarioAtualizado);

        Usuario BuscarPorId(int id);

        Usuario BuscarPorEmailSenha(string Email, string Senha);

    }
}
