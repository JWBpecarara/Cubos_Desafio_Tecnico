using System.ComponentModel.DataAnnotations;

namespace CubosFinancialAPI.DTO.Requests
{
    public class LoginRequestDto
    {
        [Required]
        [MinLength(11, ErrorMessage = "Documento inválido.")]
        public string Document { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
