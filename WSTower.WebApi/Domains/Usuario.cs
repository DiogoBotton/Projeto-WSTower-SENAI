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
        [StringLength(30, MinimumLength = 3, ErrorMessage = "O Nome deve conter entre 3 e 30 caracteres.")]
        public string Nome  { get; set; }
        // Define que o email é obrigatória
        [Required(ErrorMessage = "Informe seu email")]
        // Define o tipo do dado
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        // Define que o apelido é obrigatório
        [Required(ErrorMessage = "Informe seu apelido")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "O Apelido deve conter entre 3 e 30 caracteres.")]
        public string Apelido { get; set; }
        // Define que a foto é obrigatória
        [Required(ErrorMessage = "Informe sua foto")]
        public byte[] Foto { get; set; }
        // Define que a senha é obrigatória
        [Required(ErrorMessage = "Informe sua senha")]
        // Define o tipo do dado
        [DataType(DataType.Password)]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "A senha deve conter entre 3 e 30 caracteres.")]
        public string Senha { get; set; }
        
    }

}
