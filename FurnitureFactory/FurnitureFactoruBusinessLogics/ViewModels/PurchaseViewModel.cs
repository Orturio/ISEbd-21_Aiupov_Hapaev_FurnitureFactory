using System.Collections.Generic;
using System.ComponentModel;
using System;
using System.Runtime.Serialization;

namespace FurnitureFactoryBusinessLogics.ViewModels
{
    [DataContract]
    public class PurchaseViewModel
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int UserId { get; set; }

        [DataMember]
        [DisplayName("Название покупки")]
        public string Name { get; set; }

        [DataMember]
        [DisplayName("Сумма покупки")]
        public decimal Sum { get; set; }

        [DataMember]
        [DisplayName("Дата создания")]
        public DateTime DateOfCreation { get; set; }

        [DataMember]
        [DisplayName("Дата оплаты")]
        public DateTime? DateOfPayment { get; set; }

        [DataMember]
        public Dictionary<int, (string, int)> PurchaseFurniture { get; set; }
    }
}
