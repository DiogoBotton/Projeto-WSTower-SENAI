using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WSTower.WebApi.Domains;

namespace WSTower.WebApi.Interfaces
{
    public interface IJogadorRepository
    {
        Jogador GetById(int id);
        IEnumerable<Jogador> GetByTeam(int id);
        IEnumerable<Jogador> GetAll();
    }
}
