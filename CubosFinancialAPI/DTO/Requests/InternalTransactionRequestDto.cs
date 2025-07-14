using System.ComponentModel.DataAnnotations;

namespace CubosFinancialAPI.DTO.Requests
{
    public class InternalTransactionRequestDto
    {
        [Required(ErrorMessage = "O ID da conta de destino é obrigatório.")]
        public Guid ReceiverAccountId { get; set; }

        [Required(ErrorMessage = "O valor da transação é obrigatório.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "O valor deve ser maior que zero.")]
        public decimal Value { get; set; }

        [Required(ErrorMessage = "A descrição é obrigatória.")]
        [MaxLength(255, ErrorMessage = "A descrição deve ter no máximo 255 caracteres.")]
        public string Description { get; set; } = string.Empty;
    }
}
