using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WSTower.WebApi.Domains;
using WSTower.WebApi.Interfaces;

namespace WSTower.WebApi.Repositories
{
    public class JogadorRepository : IJogadorRepository
    {
        public WSTowerContext ctx { get; set; }

        public JogadorRepository()
        {
            ctx = new WSTowerContext();
        }

        public IEnumerable<Jogador> GetAll()
        {
            return ctx.Jogador;
        }

        public IEnumerable<Jogador> GetByTeam(int teamId)
        {
            return ctx.Jogador.Where(x => x.SelecaoId == teamId);
        }

        public Jogador GetById(int id)
        {
            return ctx.Jogador.FirstOrDefault(x => x.Id == id);
        }
    }
}
