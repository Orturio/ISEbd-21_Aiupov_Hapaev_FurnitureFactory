using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FurnitureFactoryDatabaseImplement.Models
{
    public class PurchaseFurniture
    {
        public int Id { get; set; }

        public int PurchasesId { get; set; }

        public int FurnitureId { get; set; }

        [Required]
        public int Count { get; set; }

        public virtual Purchase Purchases { get; set; }

        public virtual Furniture Furniture { get; set; }
    }
}
