using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ubs.Equities.EntityFramework.Entities
{
    public class Stock
    {
        #region Properties

        [ForeignKey("FoundId")]
        public Found Found { get; set; }

        [Required]
        public Guid FoundId { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }

        [StringLength(200)]
        [Required]
        [Index(IsUnique = true)]
        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public long Quantity { get; set; }

        [Required]
        public StockType Type { get; set; }

        #endregion
    }
}