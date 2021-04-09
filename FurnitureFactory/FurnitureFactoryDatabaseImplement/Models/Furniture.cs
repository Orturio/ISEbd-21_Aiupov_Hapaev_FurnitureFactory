using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FurnitureFactoryDatabaseImplement.Models
{
    public class Furniture
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public virtual User User { get; set; }

        [Required]
        public string FurnitureName { get; set; }

        [Required]
        public decimal FurniturePrice { get; set; }

        [Required]
        public string Material { get; set; }

        [Required]
        public DateTime DateOfCreation { get; set; }

        [ForeignKey("FurnitureId")]
        public virtual List<Payment> Payment { get; set; }

        [ForeignKey("FurnitureId")]
        public virtual List<PurchaseFurniture> PurchaseFurniture { get; set; }
    }
}
