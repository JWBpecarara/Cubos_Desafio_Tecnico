using CubosFinancialAPI.Enum;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using CubosFinancialAPI.Migrations;

namespace CubosFinancialAPI.Model
{
    public class Card
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public CardType Type { get; set; } 

        [Required]
        public string Number { get; set; } = string.Empty;

        [Required]
        public string Cvv { get; set; } = string.Empty;

        [Required]
        public Guid AccountId { get; set; }

        [ForeignKey(nameof(AccountId))]
        public Account Account { get; set; } = null!;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
