using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FurnitureFactoryDatabaseImplement.Models
{
    public class PurchaseCost
    {
        public int Id { get; set; }

        public int PurchasesId { get; set; }

        public int CostId { get; set; }

        [Required]
        public decimal Price { get; set; }

        public virtual Purchase Purchases { get; set; }

        public virtual Cost Cost { get; set; }
    }
}
