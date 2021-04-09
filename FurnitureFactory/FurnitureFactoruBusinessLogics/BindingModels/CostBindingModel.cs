using System;
using System.Collections.Generic;
using System.Text;

namespace FurnitureFactoryBusinessLogics.BindingModels
{
    public class CostBindingModel
    {
        public int? Id { get; set; }

        public int UserId { get; set; }

        public string CostName { get; set; }

        public decimal Price { get; set; }
    }
}
