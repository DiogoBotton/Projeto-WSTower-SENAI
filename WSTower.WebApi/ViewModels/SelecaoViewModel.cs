using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WSTower.WebApi.Controllers;
using WSTower.WebApi.Domains;

namespace WSTower.WebApi.ViewModels
{
    public class SelecaoViewModel
    {
        public string Nome { get; set; }
        public byte[] Uniforme { get; set; }
        public IEnumerable<JogadorViewModel> Jogadores { get; set; }
    }
}
