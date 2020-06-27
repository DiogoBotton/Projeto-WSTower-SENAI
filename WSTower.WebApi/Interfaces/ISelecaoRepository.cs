using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WSTower.WebApi.Domains;

namespace WSTower.WebApi.Interfaces
{
    public interface ISelecaoRepository
    {
        Selecao GetById(int id);
        IEnumerable<Selecao> GetAll();
    }
}
