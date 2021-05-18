using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FurnitureFactoryDatabaseImplement.Models
{
    public class Cost
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public virtual User User { get; set; }

        [Required]
        public string CostName { get; set; }

        [Required]
        public decimal CostPrice { get; set; }

        [ForeignKey("CostId")]
        public virtual List<PurchaseCost> PurchaseCost { get; set; }
    }
}
