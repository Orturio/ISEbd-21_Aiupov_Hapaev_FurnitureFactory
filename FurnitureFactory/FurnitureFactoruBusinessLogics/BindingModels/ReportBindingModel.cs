using System;
using System.Collections.Generic;
using System.Text;

namespace FurnitureFactoryBusinessLogics.BindingModels
{
    public class ReportBindingModel
    {
        public string FileName { get; set; }

        public int UserId { get; set; }

        public DateTime? DateFrom { get; set; }

        public DateTime? DateTo { get; set; }

        public List<int> PurchaseId { get; set; }

        public List<int> FurnitureId { get; set; }
    }
}
