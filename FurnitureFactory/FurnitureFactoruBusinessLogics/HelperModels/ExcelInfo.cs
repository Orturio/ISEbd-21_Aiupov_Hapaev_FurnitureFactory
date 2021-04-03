using FurnitureFactoryBusinessLogics.ViewModels;
using System.Collections.Generic;

namespace FurnitureFactoryBusinessLogics.HelperModels
{
    class ExcelInfo
    {
        public string FileName { get; set; }

        public string Title { get; set; }

        public List<ReportPurchaseFurnitureViewModel> PurchaseFurnitures { get; set; }
    }
}
