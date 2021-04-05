using System;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace FurnitureFactoryBusinessLogics.ViewModels
{
    [DataContract]
    public class PaymentViewModel
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int? PurchaseId { get; set; }

        [DataMember]
        [DisplayName("Сумма оплаты")]
        public decimal PaymentSum { get; set; }

        [DataMember]
        [DisplayName("Дата оплаты")]
        public DateTime? DateOfPayment { get; set; }
    }
}
