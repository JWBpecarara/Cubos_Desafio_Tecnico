namespace CubosFinancialAPI.DTO.Responses
{
    public class TransactionResponseDto
    { 
            public Guid Id { get; set; }
            public decimal Value { get; set; }
            public string Description { get; set; }
            public DateTime CreatedAt { get; set; }
            public DateTime UpdatedAt { get; set; }
    }
}
