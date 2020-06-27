using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WSTower.WebApi.ViewModels
{
    public class JogoViewModel
    {
        public SelecaoViewModel SelecaoCasa { get; set; }
        public SelecaoViewModel SelecaoVisitante { get; set; }
        public int PlacarCasa { get; set; }
        public int PlacarVisitante { get; set; }
    }
}
