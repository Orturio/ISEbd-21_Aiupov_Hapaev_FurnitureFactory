using System;
using System.Collections.Generic;
using System.Text;

namespace FurnitureFactoryBusinessLogics.BindingModels
{
    public class PaymentBindingModel
    {
        public int? Id { get; set; }

        public int PurchasesId { get; set; }

        public decimal Sum { get; set; }

        public DateTime? DateOfPayment { get; set; } 
    }
}
