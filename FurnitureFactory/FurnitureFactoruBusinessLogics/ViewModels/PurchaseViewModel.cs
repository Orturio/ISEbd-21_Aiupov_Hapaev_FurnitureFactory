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
        public int? UserId { get; set; }

        [DataMember]
        [DisplayName("Название покупки")]
        public string PurchaseName { get; set; }

        [DataMember]
        [DisplayName("Сумма покупки")]
        public decimal PurchaseSum { get; set; }

        [DataMember]
        [DisplayName("Сумма покупки к оплате")]
        public decimal? PurchaseSumToPayment { get; set; }

        [DataMember]
        [DisplayName("Дата создания")]
        public DateTime DateOfCreation { get; set; }

        [DataMember]
        [DisplayName("Дата оплаты")]
        public DateTime? DateOfPayment { get; set; }

        [DataMember]
        public Dictionary<int, (string, int, decimal, decimal)> PurchaseFurniture { get; set; }

        [DataMember]
        public Dictionary<int, (string, decimal)> PurchaseCosts { get; set; }
    }
}
