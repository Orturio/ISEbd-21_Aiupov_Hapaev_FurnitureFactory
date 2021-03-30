using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FurnitureFactoryDatabaseImplement.Models
{
    public class Costs
    {
        public int Id { get; set; }

        [Required]
        public string Count { get; set; }

        [Required]
        public string Price { get; set; }

        [ForeignKey("CostsId")]
        public virtual List<Furniture> Furniture { get; set; }
    }
}
