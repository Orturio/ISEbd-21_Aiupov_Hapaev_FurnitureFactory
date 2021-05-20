﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace FurnitureFactoryBusinessLogics.BindingModels
{
    [DataContract]
    public class PurchaseBindingModel
    {
        [DataMember]
        public int? Id { get; set; }

        [DataMember]
        public int? UserId { get; set; }

        [DataMember]
        public string PurchaseName { get; set; }

        [DataMember]
        public decimal PurchaseSum { get; set; }

        [DataMember]
        public decimal? PurchaseSumToPayment { get; set; }

        [DataMember]
        public DateTime DateOfCreation { get; set; }

        [DataMember]
        public DateTime? DateOfPayment { get; set; }

        [DataMember]
        public DateTime? DateFrom { get; set; }

        [DataMember]
        public DateTime? DateTo { get; set; }

        [DataMember]
        public Dictionary<int, (string, int, decimal, decimal)> PurchaseFurnitures { get; set; }

        [DataMember]
        public Dictionary<int, (string, decimal)> PurchaseCosts { get; set; }

        [DataMember]
        public List<int> FurnitureId { get; set; }

        public int FurnituresId { get; set; }
    }
}