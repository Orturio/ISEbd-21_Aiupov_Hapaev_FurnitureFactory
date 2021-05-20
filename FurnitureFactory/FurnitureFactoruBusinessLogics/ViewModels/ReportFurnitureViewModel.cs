using System;
using System.Collections.Generic;
using System.Text;

namespace FurnitureFactoryBusinessLogics.ViewModels
{
    public class ReportFurnitureViewModel
    {
        public DateTime DateOfCreation { get; set; }

        public string FurnitureName { get; set; }

        public string Material { get; set; }

        public decimal FurniturePrice { get; set; }

        public List<FurnitureViewModel> Purchases { get; set; }
    }
}
