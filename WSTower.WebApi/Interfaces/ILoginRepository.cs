using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WSTower.WebApi.Domains;
using WSTower.WebApi.ViewModels;

namespace WSTower.WebApi.Interfaces
{
    public interface ILoginRepository
    {
        Usuario Login(LoginViewModel usuario);
    }
}
