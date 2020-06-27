using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using WSTower.WebApi.Domains;
using WSTower.WebApi.Interfaces;
using WSTower.WebApi.Libraries;
using WSTower.WebApi.ViewModels;

namespace WSTower.WebApi.Repositories
{
    public class JogoRepository : IJogoRepository
    {
        private WSTowerContext ctx { get; set; }

        public JogoRepository()
        {
            ctx = new WSTowerContext();
        }

        public IEnumerable<Jogo> GetAll()
        {
            return ctx.Jogo;
        }

        public Jogo GetById(int id)
        {
            return ctx.Jogo.First(x => x.Id == id);
        }

        public IEnumerable<Jogo> GetByStadium(string nameStadium)
        {
            return ctx.Jogo.Where(x => x.Estadio.ToUpper() == nameStadium.ToUpper());
        }

        public IEnumerable<Jogo> GetByDate(DateTime date)
        {
            return ctx.Jogo.Where(x => x.Data == date);
        }

        public IEnumerable<Jogo> GetByTeam(string teamName)
        {
            var _selecaoRepository = new SelecaoRepository();
            return ctx.Jogo.Where(x => _selecaoRepository.GetById(x.SelecaoVisitante).Nome.ToUpper() == teamName.ToUpper() ||
                                            _selecaoRepository.GetById(x.SelecaoVisitante).Nome.ToUpper() == teamName.ToUpper())
                                            .ToList();
        }
    }
}
