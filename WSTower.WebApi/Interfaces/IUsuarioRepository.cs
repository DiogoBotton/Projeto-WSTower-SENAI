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
        void Atualizar(int id, Usuario usuarioAtualizado);
        Usuario BuscarPorId(int id);
        IEnumerable<Usuario> ListarUsuario();
    }
}
