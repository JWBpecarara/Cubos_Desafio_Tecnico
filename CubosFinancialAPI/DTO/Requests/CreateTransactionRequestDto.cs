using System.ComponentModel.DataAnnotations;

namespace CubosFinancialAPI.DTO.Requests
{
    public class CreateTransactionRequestDto
    {
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "O valor deve ser maior que zero.")]
        public decimal Value { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
