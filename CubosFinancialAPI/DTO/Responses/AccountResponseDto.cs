namespace CubosFinancialAPI.DTO.Responses
{
    public class AccountResponseDto
    {
        public Guid Id { get; set; }
        public string Branch { get; set; } = string.Empty;
        public string Account { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
