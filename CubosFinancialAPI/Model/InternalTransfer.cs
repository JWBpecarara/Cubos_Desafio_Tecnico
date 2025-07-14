using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CubosFinancialAPI.Model
{
    public class InternalTransfer
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid OriginTransactionId { get; set; }

        [Required]
        public Guid DestinationTransactionId { get; set; }

        [ForeignKey(nameof(OriginTransactionId))]
        public Transaction OriginTransaction { get; set; } = null!;

        [ForeignKey(nameof(DestinationTransactionId))]
        public Transaction DestinationTransaction { get; set; } = null!;

        [NotMapped]
        public decimal OriginValue { get; set; }

        [NotMapped]
        public decimal DestinationValue { get; set; }

        [NotMapped]
        public Guid OriginAccountId { get; set; }

        [NotMapped]
        public Guid DestinationAccountId { get; set; }
    }
}
