using System;
using System.Collections.Generic;
using System.Text;

namespace FurnitureFactoryBusinessLogics.ViewModels
{
    public class ReportPurchaseFurnitureViewModel
    {
        public string PurchaseName { get; set; }

        public string FurnitureName { get; set; }

        public int TotalCount { get; set; }

        public List<Tuple<string, int>> Purchases { get; set; }

        public List<Tuple<string, int>> Furnitures { get; set; }
    }
}
