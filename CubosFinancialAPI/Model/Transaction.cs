using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using CubosFinancialAPI.Enum;

namespace CubosFinancialAPI.Model
{
    public class Transaction
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Value { get; set; }

        [Required]
        public TransactionType Type { get; set; }

        [Required]
        [MaxLength(255)]
        public string Description { get; set; } = string.Empty;

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        [Required]
        public Guid AccountId { get; set; }

        [ForeignKey(nameof(AccountId))]
        public Account Account { get; set; } = null!;
    }
}
