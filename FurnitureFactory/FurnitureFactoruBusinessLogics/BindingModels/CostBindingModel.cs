using System;
using System.Collections.Generic;
using System.Text;

namespace FurnitureFactoryBusinessLogics.BindingModels
{
    public class CostBindingModel
    {
        public int? Id { get; set; }

        public string PurchaseName { get; set; }

        public int Count { get; set; }

        public decimal Price { get; set; }
    }
}
