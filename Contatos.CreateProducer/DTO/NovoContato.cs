using System.ComponentModel.DataAnnotations;

namespace Contatos.CreateProducer.DTO
{
    public class NovoContato
    {
        [Required]
        [MaxLength(1024, ErrorMessage = "O nome completo não deve exceder 1024 caracteres.")]
        public string NomeCompleto { get; set; }

        [Required]
        [Range(11, 99, ErrorMessage = "O DDD deve ser um valor entre 11 e 99.")]
        public int DDD { get; set; }

        [Required]
        public int Telefone { get; set; }

        [EmailAddress(ErrorMessage = "O endereço de e-mail deve ser informado no formato: nome@servidor.dominio")]
        [MaxLength(512, ErrorMessage = "O endereço de e-mail não deve exceder 512 caracteres.")]
        public string? Email { get; set; }

        public NovoContato(string nomeCompleto, int ddd, int telefone, string? email)
        {
            NomeCompleto = nomeCompleto;
            DDD = ddd;
            Telefone = telefone;
            Email = email;
        }
    }
}
