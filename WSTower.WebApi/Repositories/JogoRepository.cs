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
    }
}
