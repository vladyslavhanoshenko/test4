using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
   
    public class WaterIntake
    {
        public int Id { get; set; }
        public DateTime IntakeDate { get; set; }

        public string  WorkerName { get; set; }

        ////many-to-1
        //public int WorkerId { get; set; }
        //public virtual Worker Worker { get; set; }

        //public double X { get; set; }
        //public double Y { get; set; }
        //many-to-1
        public int StationId { get; set; }
        public virtual Station Station { get; set; }

        //public Class? State { get; set; }

        public DateTime? LaboratoryDate { get; set; }

        //many-to-1
        public int LaboratoryId { get; set; }
        public virtual Laboratory Laboratory { get; set; }

        public Status Status { get; set; }

        //1-to-1
        public virtual Organoleptic Organoleptic { get; set; }

        //1-to-1
        public virtual Chemical Chemical { get; set; }
    }
}
