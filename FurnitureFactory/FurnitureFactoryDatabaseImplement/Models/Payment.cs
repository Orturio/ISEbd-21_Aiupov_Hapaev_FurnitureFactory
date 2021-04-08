using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FurnitureFactoryDatabaseImplement.Models
{
    public class Payment
    {
        public int Id { get; set; }

        public int PurchaseId { get; set; }

        public int FurnitureId { get; set; }

        [Required]
        public decimal PaymentSum { get; set; }

        public DateTime? DateOfPayment { get; set; }

        public virtual Furniture Furniture { get; set; }

        public virtual User User { get; set; }
    }
}
