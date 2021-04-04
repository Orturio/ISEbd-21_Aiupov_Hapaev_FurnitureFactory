using System;

namespace FurnitureFactoryBusinessLogics.BindingModels
{
    public class FurnitureBindingModel
    {
        public int? Id { get; set; }

        public int UserId { get; set; }

        public int? CostsId { get; set; }

        public string FurnitureName { get; set; }

        public string Material { get; set; }

        public decimal FurniturePrice { get; set; }

        public DateTime DateOfCreation { get; set; }

        public DateTime? DateFrom { get; set; }

        public DateTime? DateTo { get; set; }
    }
}
