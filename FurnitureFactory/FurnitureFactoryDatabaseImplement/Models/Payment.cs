using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FurnitureFactoryDatabaseImplement.Models
{
    public class Payment
    {
        public int Id { get; set; }

        [Required]
        public int? PurchaseId { get; set; }

        [Required]
        public decimal PaymentSum { get; set; }

        public DateTime? DateOfPayment { get; set; }
    }
}
