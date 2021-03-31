using System;
using System.Collections.Generic;
using System.Text;

namespace FurnitureFactoryBusinessLogics.BindingModels
{
    public class PurchaseBindingModel
    {
        public int? Id { get; set; }

        public int UserId { get; set; }

        public string Name { get; set; }

        public decimal Sum { get; set; }

        public DateTime DateOfCreation { get; set; }

        public DateTime? DateOfPayment { get; set; }

        public DateTime? DateFrom { get; set; }

        public DateTime? DateTo { get; set; }
    }
}
