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
        public int? EmployeeId { get; set; }

        [DataMember]
        public int? CostsId { get; set; }

        [DataMember]
        [DisplayName("Название")]
        public string Name { get; set; }

        [DataMember]
        [DisplayName("Материал")]
        public string Material { get; set; }

        [DataMember]
        [DisplayName("Цена")]
        public decimal Price { get; set; }

    }
}
