using System;
using System.ComponentModel.DataAnnotations;
using DAL;

namespace WebApp.Models
{

    public class WaterIntakeModel
    {
        public int Id { get; set; }

        public DateTime IntakeDate { get; set; }

        public string WorkerName { get; set; }

        //many-to-1
        public int StationId { get; set; }

        public Class? State { get; set; }

        public DateTime? LaboratoryDate { get; set; }

        //many-to-1
        public int LaboratoryId { get; set; }

        public Status Status { get; set; }

        //1-to-1 Chemical
        [Range(0, 1000)]
        public double? Residue { get; set; }
        [Range(0, 1000)]
        public double? Ph { get; set; }
        [Range(0, 1000)]
        public double? Rigidity { get; set; }
        [Range(0, 1000)]
        public double? Chlorides { get; set; }
        [Range(0, 1000)]
        public double? Sulphates { get; set; }
        [Range(0, 1000)]
        public double? Iron { get; set; }
        [Range(0, 1000)]
        public double? Marhan { get; set; }
        [Range(0, 1000)]
        public double? Fluorine { get; set; }
        [Range(0, 1000)]
        public double? Nitrates { get; set; }

        //1-to-1 Organoleptic
        public int? Scent20 { get; set; } // ScentFlavor.Point
        public int? Scent60 { get; set; } // ScentFlavor.Point
        public int? Flavor { get; set; } // ScentFlavor.Point
        [Range(0, 1000)]
        public int? Chromaticity { get; set; }
        [Range(0, 1000)]
        public double? Turbidity { get; set; }
        [Range(0, 1000)]
        public double? Temperature { get; set; }
    }
}