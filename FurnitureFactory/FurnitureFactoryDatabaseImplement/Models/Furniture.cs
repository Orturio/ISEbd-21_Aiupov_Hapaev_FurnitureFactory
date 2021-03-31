﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FurnitureFactoryDatabaseImplement.Models
{
    public class Furniture
    {
        public int Id { get; set; }

        public int EmployeeId { get; set; }

        public int CostsId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public string Material { get; set; }

        [ForeignKey("FurnitureId")]
        public virtual List<PurchaseFurniture> PurchasesFurniture { get; set; }
    }
}
