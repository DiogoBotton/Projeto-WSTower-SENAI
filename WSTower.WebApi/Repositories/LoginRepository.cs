using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WSTower.WebApi.Domains;
using WSTower.WebApi.Interfaces;
using WSTower.WebApi.ViewModels;

namespace WSTower.WebApi.Repositories
{
    public class LoginRepository : ILoginRepository
    {
        public WSTowerContext ctx { get; set; }

        public LoginRepository()
        {
            ctx = new WSTowerContext();
        }

        public Usuario Login(LoginViewModel usuario)
        {
            return ctx.Usuario.FirstOrDefault(x => (x.Email == usuario.EmailOUsenha || x.Apelido == usuario.EmailOUsenha) && x.Senha == usuario.Senha);
        }
    }
}
