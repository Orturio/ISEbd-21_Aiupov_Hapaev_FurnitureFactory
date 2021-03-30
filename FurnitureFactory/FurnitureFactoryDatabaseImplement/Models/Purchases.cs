using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace FurnitureFactoryDatabaseImplement.Models
{
    class Purchases
    {
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; }

        [Required]
        public float SumOfPurchases { get; set; }

        [Required]
        public DateTime DateOfCreation { get; set; }

        public DateTime? DateOfPayment { get; set; }

        [ForeignKey("UserId")]
        public virtual List<User> User { get; set; }
    }
}
