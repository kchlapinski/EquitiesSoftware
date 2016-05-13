using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ubs.Equities.EntityFramework.Entities
{
    public class Found
    {
        #region Properties

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }

        [StringLength(200)]
        [Required]
        [Index(IsUnique = true)]
        public string Name { get; set; }

        public virtual ICollection<Stock> Stocks { get; set; }

        #endregion
    }
}