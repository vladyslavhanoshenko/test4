using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using DAL.Entities;

namespace DAL.EF
{
    public class GroundwaterInitializer : DropCreateDatabaseAlways<GroundwaterContext>
    {
        protected override void Seed(GroundwaterContext db)
        {

            Laboratory lab1 = new Laboratory { Name = "ТОВ «УКРРЕСУРСИ-2011»", Address = "ТОВ «УКРРЕСУРСИ-2011»" };
            Laboratory lab2 = new Laboratory { Name = "ДП „ХЦЗПС”»", Address = "ДП „ХЦЗПС”" };
            Laboratory lab3 = new Laboratory { Name = "КП Нікопольське виробниче управління водопровідно-каналізаційного господарства Нікопольської міської ради ", Address = "КП Нікопольське виробниче управління водопровідно-каналізаційного господарства Нікопольської міської ради " };
            Laboratory lab4 = new Laboratory { Name = "Львівводоканал", Address = "Львівводоканал" };

            List<Laboratory> listLaboratory = new List<Laboratory> { lab1, lab2, lab3, lab4 };
            db.Laboratories.AddRange(listLaboratory);

            ScentFlavor zero = new ScentFlavor { Point = 0, Text = "is not detected" };
            ScentFlavor one = new ScentFlavor { Point = 1, Text = "is detected only by experienced tasters" };
            ScentFlavor two = new ScentFlavor { Point = 2, Text = "is detected by consumers" };
            ScentFlavor three = new ScentFlavor { Point = 3, Text = "is detected easily and cause complaints" };
            ScentFlavor four = new ScentFlavor { Point = 4, Text = "is so strong that makes drinking impossible" };
            ScentFlavor five = new ScentFlavor { Point = 5, Text = "is such intensive that makes water undrinkable" };

            List<ScentFlavor> listScentFlavor = new List<ScentFlavor> { zero, one, two, three, four, five };
            db.ScentFlavors.AddRange(listScentFlavor);

            db.OrganolepticNorms.Add(new OrganolepticNorm { Class = Class.first, Scent20 = 1, Scent60 = 1, Flavor = 1, Chromaticity = 20, Turbidity = 1.5, Temperature = 8 });
            db.OrganolepticNorms.Add(new OrganolepticNorm { Class = Class.second, Scent20 = 2, Scent60 = 2, Flavor = 2, Chromaticity = 30, Turbidity = 2.5, Temperature = 12 });
            db.OrganolepticNorms.Add(new OrganolepticNorm { Class = Class.third, Scent20 = 3, Scent60 = 3, Flavor = 3, Chromaticity = 50, Turbidity = 10 });

            db.ChemicalNorms.Add(new ChemicalNorm { Class = Class.first, Residue = 1000, Ph = 6, Rigidity = 7, Chlorides = 350, Sulphates = 500, Iron = 0.3, Marhan = 0.1, Fluorine = 1.5, Nitrates = 45 });
            db.ChemicalNorms.Add(new ChemicalNorm { Class = Class.second, Residue = 1500, Ph = 9, Rigidity = 10, Iron = 10, Marhan = 1.0, Fluorine = 2.5 });
            db.ChemicalNorms.Add(new ChemicalNorm { Class = Class.third, Iron = 20, Marhan = 2.0, Fluorine = 5.0 });

            db.Organoleptics.Add(new Organoleptic { Id = 1, Scent20 = 2, Scent60 = 2, Flavor = 2, Chromaticity = 20, Turbidity = 1.5, Temperature = 8 });
            db.Organoleptics.Add(new Organoleptic { Id = 2, Scent20 = 2, Scent60 = 2, Flavor = 2, Chromaticity = 20, Turbidity = 3.5, Temperature = 12 });
            db.Organoleptics.Add(new Organoleptic { Id = 3, Scent20 = 2, Scent60 = 2, Flavor = 2, Chromaticity = 50, Turbidity = 10, Temperature = 12 });
            db.Organoleptics.Add(new Organoleptic { Id = 4, Scent20 = 2, Scent60 = 2, Flavor = 2, Chromaticity = 50, Turbidity = 10, Temperature = 12 });

            db.Chemicals.Add(new Chemical { Id = 1, Residue = 1000, Ph = 6, Rigidity = 7, Chlorides = 350, Sulphates = 500, Iron = 0.3, Marhan = 0.1, Fluorine = 1.5, Nitrates = 45 });
            db.Chemicals.Add(new Chemical { Id = 2, Residue = 1500, Ph = 9, Rigidity = 10, Chlorides = 350, Sulphates = 500, Iron = 10, Marhan = 1.0, Fluorine = 1.5, Nitrates = 45 });
            db.Chemicals.Add(new Chemical { Id = 3, Residue = 1500, Ph = 9, Rigidity = 10, Chlorides = 350, Sulphates = 500, Iron = 20, Marhan = 2.0, Fluorine = 5.0, Nitrates = 45 });
            db.Chemicals.Add(new Chemical { Id = 4, Residue = 1500, Ph = 9, Rigidity = 10, Chlorides = 350, Sulphates = 500, Iron = 20, Marhan = 2.0, Fluorine = 5.0, Nitrates = 45 });

            Station station1 = new Station { X = 50.7447992, Y = 27.8605985, Class = Class.first };
            Station station2 = new Station { X = 50.7976271, Y = 26.3863113, Class = Class.second };
            Station station3 = new Station { X = 50.7280071, Y = 27.1178113, Class = Class.third };
            Station station4 = new Station { X = 50.7280071, Y = 27.1178113, Class = Class.undrinkable };
            Station station5 = new Station { X = 50.5234014, Y = 27.3005768, Class = Class.second };

            List<Station> listStation = new List<Station> { station1, station2, station3, station4, station5 };
            db.Stations.AddRange(listStation);

            db.WaterIntakes.Add(new WaterIntake { Id = 1, IntakeDate = new DateTime(2017, 01, 12), WorkerName = "user1@mail.com", Station = station1, LaboratoryDate = new DateTime(2017, 04, 12), Laboratory = lab1, Status = Status.investigated });
            db.WaterIntakes.Add(new WaterIntake { Id = 4, IntakeDate = new DateTime(2017, 04, 12), WorkerName = "user1@mail.com", Station = station1, LaboratoryDate = new DateTime(2017, 04, 12), Laboratory = lab3, Status = Status.investigated });
            db.WaterIntakes.Add(new WaterIntake { Id = 3, IntakeDate = new DateTime(2017, 03, 12), WorkerName = "user1@mail.com", Station = station1, LaboratoryDate = new DateTime(2017, 04, 12), Laboratory = lab4, Status = Status.investigated });
            db.WaterIntakes.Add(new WaterIntake { Id = 2, IntakeDate = new DateTime(2017, 02, 12), WorkerName = "user1@mail.com", Station = station1, LaboratoryDate = new DateTime(2017, 04, 12), Laboratory = lab1, Status = Status.investigated });

            db.SaveChanges();
        }
    }
}
