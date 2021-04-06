using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FurnitureFactoryDatabaseImplement.Models
{
    public class Purchase
    {
        public int Id { get; set; }

        public int? UserId { get; set; }

        [Required]
        public string PurchaseName { get; set; }

        [Required]
        public decimal PurchaseSum { get; set; }

        [Required]
        public DateTime DateOfCreation { get; set; }

        [ForeignKey("PurchaseId")]
        public virtual List<Payment> Payment { get; set; }

        [ForeignKey("PurchasesId")]
        public virtual List<PurchaseFurniture> PurchaseFurniture { get; set; }
    }
}
