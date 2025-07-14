using System.ComponentModel.DataAnnotations;

namespace CubosFinancialAPI.DTO.Requests
{
    public class AccountRequestDto
    {
        [Required]
        [RegularExpression(@"^\d{3}$", ErrorMessage = "A agência deve conter exatamente 3 dígitos.")]
        public string Branch { get; set; } = string.Empty;

        [Required]
        [RegularExpression(@"^\d{7}-\d{1}$", ErrorMessage = "A conta deve estar no formato XXXXXXX-X.")]
        public string Account { get; set; } = string.Empty;
    }
}
