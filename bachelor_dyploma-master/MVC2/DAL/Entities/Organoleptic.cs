using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace DAL.Entities
{
    public class Organoleptic
    {
        [Key]
        [ForeignKey("WaterIntake")]
        public int Id { get; set; }

        
        public int? Scent20 { get; set; } // ScentFlavor.Point

        public int? Scent60 { get; set; } // ScentFlavor.Point

        public int? Flavor { get; set; } // ScentFlavor.Point

        public int? Chromaticity { get; set; }

        public double? Turbidity { get; set; }

        public double? Temperature { get; set; }

        //1-to-1
        public virtual WaterIntake WaterIntake { get; set; }

        
    }
}
