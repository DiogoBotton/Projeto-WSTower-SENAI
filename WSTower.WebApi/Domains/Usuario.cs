using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WSTower.WebApi.Domains
{
    public partial class Usuario
    {

        public int Id { get; set; }
        // Define que o nome é obrigatória
        [Required(ErrorMessage = "Informe seu Nome")]
        // Define o tipo do dado
        [DataType(DataType.Text)]
        public string Nome  { get; set; }
        // Define que o email é obrigatória
        [Required(ErrorMessage = "Informe seu email")]
        // Define o tipo do dado
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        // Define que o nome é obrigatória
        [Required(ErrorMessage = "Informe seu apelido")]
        // Define o tipo do dado
        [DataType(DataType.Text)]
        public string Apelido { get; set; }
        // Define o tipo do dado
        [DataType(DataType.ImageUrl)]
        public byte[] Foto { get; set; }
        // Define que o nome é obrigatória
        [Required(ErrorMessage = "Informe sua senha")]
        // Define o tipo do dado
        [DataType(DataType.Password)]
        public string Senha { get; set; }
    }
}
