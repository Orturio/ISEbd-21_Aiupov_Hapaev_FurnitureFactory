﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace FurnitureFactoryDatabaseImplement.Models
{
    public class Purchases
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public float SumOfPurchases { get; set; }

        [Required]
        public DateTime DateOfCreation { get; set; }

        public DateTime? DateOfPayment { get; set; }

        [ForeignKey("PurchasesId")]
        public virtual List<Payment> Payment { get; set; }

        [ForeignKey("PurchasesId")]
        public virtual List<PurchasesFurniture> PurchasesFurniture { get; set; }
    }
}
