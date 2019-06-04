using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class ChemicalNorm
    {
        public int Id { get; set; }
        public Class Class { get; set; }
        public double? Residue { get; set; }
        public double? Ph { get; set; }
        public double? Rigidity { get; set; }
        public double? Chlorides { get; set; }
        public double? Sulphates { get; set; }
        public double? Iron { get; set; }
        public double? Marhan { get; set; }
        public double? Fluorine { get; set; }
        public double? Nitrates { get; set; }
    }
}
