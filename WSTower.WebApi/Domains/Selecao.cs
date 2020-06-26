using System;
using System.Collections.Generic;

namespace WSTower.WebApi.Domains
{
    public partial class Selecao
    {
        public Selecao()
        {

        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public byte[] Bandeira { get; set; }
        public byte[] Uniforme { get; set; }
        public string Escalacao { get; set; }
    }
}
