using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace FurnitureFactoryBusinessLogics.ViewModels
{
    [DataContract]
    public class CostViewModel
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int UserId { get; set; }

        [DataMember]
        [DisplayName("Сотрудник")]
        public string UserEmail { get; set; }

        [DataMember]
        [DisplayName("Затрата")]
        public string CostName { get; set; }

        [DataMember]
        [DisplayName("Цена")]
        public decimal Price { get; set; }
    }
}
