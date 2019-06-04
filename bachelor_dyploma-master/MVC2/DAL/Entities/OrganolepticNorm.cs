using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class OrganolepticNorm
    {
        public int Id { get; set; }

        public Class Class { get; set; }

        public int? Scent20 { get; set; }

        public int? Scent60 { get; set; }

        public int? Flavor { get; set; }

        public int? Chromaticity { get; set; }

        public double? Turbidity { get; set; }

        public double? Temperature { get; set; }
    }
}
