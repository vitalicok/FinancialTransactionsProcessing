using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinancialTransactionsProcessing.Models
{
    [Table("Transaction")]
    public class Transaction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid TransactionId { get; set; }
        [ForeignKey("UserId")]
        public Guid UserId { get; set; }
        [Column("Date")]
        public DateTime Date { get; set; }
        [Column("Amount")]
        public decimal Amount { get; set; }
        [Column("Category")]
        public string Category { get; set; }
        [Column("Description")]
        public string Description { get; set; }
        [Column("Merchant")]
        public string Merchant { get; set; }
    }
}
