using System;
using System.Collections.Generic;
using System.Text;

namespace FurnitureFactoryBusinessLogics.ViewModels
{
    public class ReportPurchaseViewModel
    {
        public DateTime DateOfCreation { get; set; }

        public string PurchaseName { get; set; }

        public decimal PurchaseSum { get; set; }

        public decimal? PurchaseSumToPayment { get; set; }

        public List<FurnitureViewModel> Furnitures { get; set; }
    }
}
