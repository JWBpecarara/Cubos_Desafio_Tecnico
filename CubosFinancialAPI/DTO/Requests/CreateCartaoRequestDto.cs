using CubosFinancialAPI.Enum;
using System.ComponentModel.DataAnnotations;

namespace CubosFinancialAPI.DTO.Requests
{
    public class CreateCartaoRequestDto
    {
        [Required]
        public CardType Type { get; set; }

        [Required]
        [CreditCard(ErrorMessage = "Número de cartão inválido.")]
        public string Number { get; set; }

        [Required]
        [RegularExpression(@"^\d{3}$", ErrorMessage = "CVV deve conter exatamente 3 dígitos.")]
        public string Cvv { get; set; }
    }
}
