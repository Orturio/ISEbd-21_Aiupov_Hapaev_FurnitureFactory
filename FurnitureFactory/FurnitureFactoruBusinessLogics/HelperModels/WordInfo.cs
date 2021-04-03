using FurnitureFactoryBusinessLogics.ViewModels;
using System.Collections.Generic;

namespace FurnitureFactoryBusinessLogics.HelperModels
{
    class WordInfo
    {
        public string FileName { get; set; }

        public string Title { get; set; }

        public List<FurnitureViewModel> Furnitures { get; set; }

        public List<PurchaseViewModel> Purchases { get; set; }
    }
}
