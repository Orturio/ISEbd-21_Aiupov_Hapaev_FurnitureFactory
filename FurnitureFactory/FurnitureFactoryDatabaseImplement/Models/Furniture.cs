using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FurnitureFactoryDatabaseImplement.Models
{
    public class Furniture
    {
        public int Id { get; set; }

        public int? UserId { get; set; }

        public int? CostsId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public string Material { get; set; }

        [ForeignKey("FurnitureId")]
        public virtual List<PurchaseFurniture> PurchaseFurniture { get; set; }
    }
}
