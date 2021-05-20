using System;
using System.Runtime.Serialization;
using System.Collections.Generic;

namespace FurnitureFactoryBusinessLogics.BindingModels
{
    [DataContract]
    public class FurnitureBindingModel
    {
        [DataMember]
        public int? Id { get; set; }

        [DataMember]
        public int UserId { get; set; }

        [DataMember]
        public int? CostsId { get; set; }

        public int PurchaseId { get; set; }

        [DataMember]
        public string FurnitureName { get; set; }

        [DataMember]
        public string Material { get; set; }

        [DataMember]
        public decimal FurniturePrice { get; set; }

        [DataMember]
        public DateTime DateOfCreation { get; set; }

        [DataMember]
        public DateTime? DateFrom { get; set; }

        [DataMember]
        public DateTime? DateTo { get; set; }

        [DataMember]
        public List<int> PurchasesId { get; set; }
    }
}
