using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WSTower.WebApi.Domains;
using WSTower.WebApi.ViewModels;

namespace WSTower.WebApi.Interfaces
{
    public interface IJogoRepository
    {
        Jogo GetById(int id);
        IEnumerable<Jogo> GetAll();
        IEnumerable<Jogo> GetByStadium(string nameStadium);
        IEnumerable<Jogo> GetByDate(DateTime date);
        IEnumerable<Jogo> GetByTeam(string teamName);
    }
}
