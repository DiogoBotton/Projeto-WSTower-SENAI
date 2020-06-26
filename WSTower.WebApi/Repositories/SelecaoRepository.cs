using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WSTower.WebApi.Domains;
using WSTower.WebApi.Interfaces;

namespace WSTower.WebApi.Repositories
{
    public class SelecaoRepository : ISelecaoRepository
    {
        public WSTowerContext ctx { get; set; }

        public SelecaoRepository()
        {
            ctx = new WSTowerContext();
        }

        public List<Selecao> GetAll()
        {
            return ctx.Selecao.ToList();
        }

        public Selecao GetById(int id)
        {
            return ctx.Selecao.FirstOrDefault(x => x.Id == id);
        }
    }
}
