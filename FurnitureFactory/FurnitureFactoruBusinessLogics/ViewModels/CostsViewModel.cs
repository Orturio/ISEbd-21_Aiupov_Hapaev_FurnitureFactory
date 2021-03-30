using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace FurnitureFactoryBusinessLogics.ViewModels
{
    [DataContract]
    public class CostsViewModel
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        [DisplayName("Количество")]
        public string Count { get; set; }

        [DataMember]
        [DisplayName("Цена")]
        public string Price { get; set; }
    }
}
