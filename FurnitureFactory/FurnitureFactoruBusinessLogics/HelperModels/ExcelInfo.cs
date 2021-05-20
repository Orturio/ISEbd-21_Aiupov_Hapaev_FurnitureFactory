using FurnitureFactoryBusinessLogics.ViewModels;
using System.Collections.Generic;

namespace FurnitureFactoryBusinessLogics.HelperModels
{
    class ExcelInfo
    {
        public string FileName { get; set; }

        public string Title { get; set; }

        public List<PurchaseViewModel> Purchases { get; set; }

        public List<ReportPurchaseFurnitureViewModel> Furnitures { get; set; }
    }
}
