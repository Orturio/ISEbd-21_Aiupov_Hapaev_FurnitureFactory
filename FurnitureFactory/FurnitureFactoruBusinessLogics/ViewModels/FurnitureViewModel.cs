using System.Collections.Generic;
using System.ComponentModel;
using System;
using System.Runtime.Serialization;

namespace FurnitureFactoryBusinessLogics.ViewModels
{
    [DataContract]
    public class FurnitureViewModel
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int? UserId { get; set; }

        [DataMember]
        public int? CostId { get; set; }

        [DataMember]
        [DisplayName("Название")]
        public string FurnitureName { get; set; }

        [DataMember]
        [DisplayName("Материал")]
        public string Material { get; set; }

        [DataMember]
        [DisplayName("Цена")]
        public decimal FurniturePrice { get; set; }

        [DataMember]
        [DisplayName("Дата создания")]
        public DateTime DateOfCreation { get; set; }

    }
}
