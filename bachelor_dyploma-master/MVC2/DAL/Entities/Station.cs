using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Station
    {
        public int Id { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public Class Class { get; set; }

        //1-to-many
        public virtual ICollection<WaterIntake> WaterIntakes { get; set; }
        public Station()
        {
            WaterIntakes = new List<WaterIntake>();
        }
    }
}
