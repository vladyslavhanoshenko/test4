using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class Chemical
    {
        [Key]
        [ForeignKey("WaterIntake")]
        public int Id { get; set; }

        public double? Residue { get; set; }
        [Range(1, 14)]
        public double? Ph { get; set; }
        public double? Rigidity { get; set; }
        public double? Chlorides { get; set; }
        public double? Sulphates { get; set; }
        public double? Iron { get; set; }
        public double? Marhan { get; set; }
        public double? Fluorine { get; set; }
        public double? Nitrates { get; set; }


        //1-to-1
        public virtual WaterIntake WaterIntake { get; set; }

       
    }
}
