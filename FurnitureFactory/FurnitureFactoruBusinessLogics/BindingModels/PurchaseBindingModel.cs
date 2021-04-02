using System;
using System.Collections.Generic;
using System.Text;

namespace FurnitureFactoryBusinessLogics.BindingModels
{
    public class PurchaseBindingModel
    {
        public int? Id { get; set; }

        public int? UserId { get; set; }

        public string PurchaseName { get; set; }

        public decimal PurchaseSum { get; set; }

        public DateTime DateOfCreation { get; set; }

        public DateTime? DateOfPayment { get; set; }

        public DateTime? DateFrom { get; set; }

        public DateTime? DateTo { get; set; }

        public Dictionary<int, (string, int, decimal)> PurchaseFurnitures { get; set; }
    }
}