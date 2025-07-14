using CubosFinancialAPI.Enum;

namespace CubosFinancialAPI.DTO.Responses
{
    public class CartaoResponseDto
    {
        public Guid Id { get; set; }
        public CardType Type  { get; set; }
        public string Number { get; set; } 
        public string Cvv { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
