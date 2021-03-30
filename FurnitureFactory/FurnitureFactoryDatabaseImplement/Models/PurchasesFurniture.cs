using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FurnitureFactoryDatabaseImplement.Models
{
    public class PurchasesFurniture
    {
        public int Id { get; set; }

        public int PurchasesId { get; set; }

        public int FurnitureId { get; set; }

        public virtual Purchases Purchases { get; set; }

        public virtual Furniture Furniture { get; set; }
    }
}
