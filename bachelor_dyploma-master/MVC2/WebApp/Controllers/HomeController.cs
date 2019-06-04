using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using DAL.Entities;
using DAL.EF;
using WebApp.Models;

namespace WebApp.Controllers
{
   
    public class HomeController : Controller
    {
        GroundwaterContext db = new GroundwaterContext("GroundWaterContext");
        
        #region Statistic
        
        // /Home/SearchStatisticData
        [Authorize]
        public ActionResult SearchStatisticData()
        {
            var stationsList = db.Stations.Select(l => new { StationId = l.Id, XY = "#" + l.Id + " - " + l.X + " : " + l.Y }).ToList(); 
            SelectList stations = new SelectList(stationsList, "StationId", "XY");
            ViewBag.Stations = stations;

            var indicatorList = new Dictionary<int, string> {
                { 1, "Residue" },
                { 2, "Ph" },
                { 3, "Rigidity" },
                { 4, "Chlorides" },
                { 5, "Sulphates" },
                { 6, "Iron" },
                { 7, "Marhan" },
                { 8, "Fluorine" },
                { 9, "Nitrates" },
            };
            //var indicatorList = new Dictionary<int, string> { { 1, "indicator1" }, { 2, "indicator2" }, { 3, "indicator3" } };
            SelectList indicators = new SelectList(indicatorList, "Key", "Value");
            ViewBag.Indicators = indicators;

            var startDateList = new Dictionary<int, string> {  { 3, "3 months" }, { 6, "6 months" } , { 12, "12 months" } };
            SelectList startDates = new SelectList(startDateList, "Key", "Value");
            ViewBag.StartDates = startDates;
            return View();
        }

        [HttpPost] // or ChildActionOnly
        public ActionResult DrawChart( StatisticModel model)
        {
            return PartialView("Statistic", model);
        }

        private class ListChartItem
        {
            public int Year { get; set; }
            public int Month { get; set; }
            public double Value { get; set; }
        }

        public JsonResult GetLineChartStatistic(int station, int monthAmount, int indicator)
        {
            //see http://www.c-sharpcorner.com/UploadFile/4d9083/how-to-create-google-charts-with-mvc-4/

            DateTime startDate = DateTime.Now.AddMonths((-1) * monthAmount);
            var intakesList = db.WaterIntakes.Where(w => w.StationId == station).Where(w => w.IntakeDate >= startDate);

            // get  data from DB
            //var result = intakesList.Select(l => new { Year = l.IntakeDate.Year, Month = l.IntakeDate.Month, Value = l.Chemical.Ph }).ToList();
            //switch (indicator)
            //{
            //    case 1:
            //        result = intakesList.Select(l => new { Year = l.IntakeDate.Year, Month = l.IntakeDate.Month, Value = l.Chemical.Residue }).ToList();
            //        break;
            //    case 2:
            //        result = intakesList.Select(l => new { Year = l.IntakeDate.Year, Month = l.IntakeDate.Month, Value = l.Chemical.Ph }).ToList();
            //        break;
            //    case 3:
            //        result = intakesList.Select(l => new { Year = l.IntakeDate.Year, Month = l.IntakeDate.Month, Value = l.Chemical.Rigidity }).ToList();
            //        break;
            //    case 4:
            //        result = intakesList.Select(l => new { Year = l.IntakeDate.Year, Month = l.IntakeDate.Month, Value = l.Chemical.Chlorides }).ToList();
            //        break;
            //    case 5:
            //        result = intakesList.Select(l => new { Year = l.IntakeDate.Year, Month = l.IntakeDate.Month, Value = l.Chemical.Sulphates }).ToList();
            //        break;
            //    case 6:
            //        result = intakesList.Select(l => new { Year = l.IntakeDate.Year, Month = l.IntakeDate.Month, Value = l.Chemical.Iron }).ToList();
            //        break;
            //    case 7:
            //        result = intakesList.Select(l => new { Year = l.IntakeDate.Year, Month = l.IntakeDate.Month, Value = l.Chemical.Marhan }).ToList();
            //        break;
            //    case 8:
            //        result = intakesList.Select(l => new { Year = l.IntakeDate.Year, Month = l.IntakeDate.Month, Value = l.Chemical.Fluorine }).ToList();
            //        break;
            //    case 9:
            //        result = intakesList.Select(l => new { Year = l.IntakeDate.Year, Month = l.IntakeDate.Month, Value = l.Chemical.Nitrates }).ToList();
            //        break;
            //}

            // get static data
            var result = new List<ListChartItem>();
            result.Add(new ListChartItem { Year = 2000, Month = 4, Value = 33.207 });
            result.Add(new ListChartItem { Year = 2001, Month = 4, Value = 35.207 });
            result.Add(new ListChartItem { Year = 2002, Month = 4, Value = 45.207 });
            result.Add(new ListChartItem { Year = 2003, Month = 4, Value = 50.207 });

            return Json(new { Values = result }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Maps

        // /Home/Maps
        public ActionResult Maps()
        {
            ViewBag.Title = "Google Maps";
            return View();
        }
      
        private class MapMarkerItem
        {
            public double X { get; set; }
            public double Y { get; set; }
            public string Color { get; set; }
            public string Text { get; set; }
        }

        public JsonResult GetMap()
        {
            // get data from DB
            var allStations = db.Stations;
            var result = new List<MapMarkerItem>();

            foreach (var st in allStations)
            {
                string color = "black";
                if (st.Class == DAL.Class.first)
                    color = "green";
                else
                    if (st.Class == DAL.Class.second)
                    color = "yellow";
                else
                    if (st.Class == DAL.Class.third)
                    color = "red";

                result.Add(new MapMarkerItem { X = st.X, Y = st.Y, Color = color, Text = string.Format("Id: {0}, X: {1}, Y: {2}, Class: {3}", st.Id, st.X, st.Y, st.Class) });
            }

            // get static data
            //var result = new List<MapMarkerItem>();
            //result.Add(new MapMarkerItem { X = 52.364, Y = 33.207, Text = "marker1" });
            //result.Add(new MapMarkerItem { X = 55.364, Y = 33.207, Text = "marker2" });
            //result.Add(new MapMarkerItem { X = 55.364, Y = 38.207, Text = "marker3" });
            //result.Add(new MapMarkerItem { X = 52.364, Y = 38.207, Text = "marker4" });

            return Json(new { Values = result }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region WaterIntakes      

        private DAL.Class CheckingClass(Chemical ch, Organoleptic or)
        {
            if ((or.Scent20 <= 2 && or.Scent60 <= 2 && or.Flavor <= 2 && or.Chromaticity <= 20 && or.Turbidity <= 1.5 && or.Temperature >= 8 && or.Temperature <= 12) &&
                (ch.Residue <= 1000 && ch.Ph <= 9 && ch.Ph >= 6 && ch.Rigidity <= 7 && ch.Chlorides <= 350 && ch.Sulphates <= 500 &&
                ch.Iron <= 0.3 && ch.Marhan <= 0.1 && ch.Fluorine <= 1.5 && ch.Nitrates <= 45))
                return DAL.Class.first;
            else
            if ((or.Scent20 <= 2 && or.Scent60 <= 2 && or.Flavor <= 2 && or.Chromaticity <= 20 && or.Turbidity <= 3.5 && or.Temperature >= 8 && or.Temperature <= 12) &&
                (ch.Residue <= 1500 && ch.Ph <= 9 && ch.Ph >= 6 && ch.Rigidity <= 10 && ch.Chlorides <= 350 && ch.Sulphates <= 500 &&
                ch.Iron <= 10 && ch.Marhan <= 1.0 && ch.Fluorine <= 1.5 && ch.Nitrates <= 45))
                return DAL.Class.second;
            else
            if ((or.Scent20 <= 2 && or.Scent60 <= 2 && or.Flavor <= 2 && or.Chromaticity <= 50 && or.Turbidity <= 3.5 && or.Temperature >= 8 && or.Temperature <= 12) &&
                (ch.Residue <= 1500 && ch.Ph <= 9 && ch.Ph >= 6 && ch.Rigidity <= 10 && ch.Chlorides <= 350 && ch.Sulphates <= 500 &&
                ch.Iron <= 20 && ch.Marhan <= 2.0 && ch.Fluorine <= 5 && ch.Nitrates <= 45))
                return DAL.Class.third;
            else
                return DAL.Class.undrinkable;
        }

        // /Home/WaterIntakes
        [Authorize]
        public ActionResult WaterIntakes()
        {
            ViewBag.Title = "WaterIntakes";
            return View(db.WaterIntakes.Include(p=> p.Laboratory).Include(r=>r.Station));

        }

        [HttpGet]
        [Authorize]
        public ActionResult CreateWaterIntake()
        {
            WaterIntakeViewBag();
            ViewBag.IntakeDate = DateTime.Now;
            ViewBag.LaboratoryDate = DateTime.Now;
            return View();
        }

        private void WaterIntakeViewBag()
        {
            ViewBag.WorkerName = User.Identity.Name;

            var laboratoriesList = db.Laboratories.Select(l => new { LaboratoryId = l.Id, Name = l.Name }).ToList();
            SelectList laboratories = new SelectList(laboratoriesList, "LaboratoryId", "Name");
            ViewBag.Laboratories = laboratories;

            var stationsList = db.Stations.Select(l => new { StationId = l.Id, XY = "#"+l.Id+ " - "+l.X+" : "+l.Y}).ToList(); //String.Format("({0}, {1})", l.X, l.Y)
            SelectList stations = new SelectList(stationsList, "StationId", "XY");
            ViewBag.Stations = stations;

            var scentFlavorList = db.ScentFlavors.Select(l => new { Point = l.Point, Text = l.Text }).ToList();
            SelectList scentFlavors = new SelectList(scentFlavorList, "Point", "Text");
            ViewBag.ScentFlavors = scentFlavors;

            ViewBag.Status = DAL.Status.taken;

        }

        private WaterIntake WaterIntakeModelToWaterIntakeMap(WaterIntakeModel wtmodel)
        {
            WaterIntake wt = new WaterIntake()
            {
                Id = 100,
                LaboratoryId = wtmodel.LaboratoryId,
                IntakeDate = wtmodel.IntakeDate,
                LaboratoryDate = wtmodel.LaboratoryDate,
                WorkerName = wtmodel.WorkerName,
                StationId = wtmodel.StationId
            };
            return wt;
        }

        private Chemical WaterIntakeModelToChemical(WaterIntakeModel wtmodel)
        {
            Chemical ch = new Chemical()
            {
                Id = 100,
                Residue = wtmodel.Residue,
                Ph = wtmodel.Ph,
                Rigidity = wtmodel.Rigidity,
                Chlorides = wtmodel.Chlorides,
                Sulphates = wtmodel.Sulphates,
                Iron = wtmodel.Iron,
                Marhan = wtmodel.Marhan,
                Fluorine = wtmodel.Fluorine,
                Nitrates = wtmodel.Nitrates
            };
            return ch;
        }

        private Organoleptic WaterIntakeModelToOrganoleptic(WaterIntakeModel wtmodel)
        {
            Organoleptic or = new Organoleptic()
            {
                Id = 100,
                Scent20 = wtmodel.Scent20,
                Scent60 = wtmodel.Scent60,
                Flavor = wtmodel.Flavor,
                Chromaticity = wtmodel.Chromaticity,
                Turbidity = wtmodel.Turbidity,
                Temperature = wtmodel.Temperature
            };
            return or;
        }

        [HttpPost]
        public ActionResult CreateWaterIntake(WaterIntakeModel wtmodel)
        {
            
            Laboratory b = db.Laboratories.Find(wtmodel.LaboratoryId);
            Station s = db.Stations.Find(wtmodel.StationId);

            if (b == null)
            {
                throw new OperationCanceledException("Laboratory not found");
            }

            if (s == null)
            {
                throw new OperationCanceledException("Station not found");
            }

            WaterIntake wt = WaterIntakeModelToWaterIntakeMap(wtmodel);
            wt.Laboratory = b;
            wt.Station = s;

            Chemical ch = WaterIntakeModelToChemical(wtmodel);
            Organoleptic or = WaterIntakeModelToOrganoleptic(wtmodel);

            if (ModelState.IsValid)
            {
                if (wtmodel.LaboratoryDate == null)
                {
                    wt.Status = DAL.Status.taken;
                }
                else
                {
                    wt.Status = DAL.Status.investigated;
                }

                s.Class = CheckingClass(ch, or);

                db.WaterIntakes.Add(wt);
                db.Chemicals.Add(ch);
                db.Organoleptics.Add(or);
                db.SaveChanges();

                return RedirectToAction("WaterIntakes");
            }
            else
            {
                WaterIntakeViewBag();

                if (wtmodel.IntakeDate == default(DateTime))
                    ViewBag.IntakeDate = DateTime.Now;
                else
                    ViewBag.IntakeDate = wtmodel.IntakeDate;

                if (wtmodel.LaboratoryDate == default(DateTime))
                    ViewBag.LaboratoryDate = DateTime.Now;
                else
                    ViewBag.LaboratoryDate = wtmodel.LaboratoryDate;

                return View(wtmodel);
            }           
        }

        [HttpGet]
        [Authorize]
        public ActionResult EditWaterIntake(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            WaterIntake wt = db.WaterIntakes.Find(id);

            if (wt == null)
            {
                return HttpNotFound();
            }

            Chemical ch = wt.Chemical;
            Organoleptic or = wt.Organoleptic;

            WaterIntakeModel wtmodel = new WaterIntakeModel()
            {
                Id = wt.Id,
                LaboratoryId = wt.LaboratoryId,
                IntakeDate = wt.IntakeDate,
                LaboratoryDate = wt.LaboratoryDate,
                WorkerName = wt.WorkerName,
                StationId = wt.StationId,
                Status = wt.Status,

                Residue = ch.Residue,
                Ph = ch.Ph,
                Rigidity = ch.Rigidity,
                Chlorides = ch.Chlorides,
                Sulphates = ch.Sulphates,
                Iron = ch.Iron,
                Marhan = ch.Marhan,
                Fluorine = ch.Fluorine,
                Nitrates = ch.Nitrates,


                Scent20 = or.Scent20,
                Scent60 = or.Scent60,
                Flavor = or.Flavor,
                Chromaticity = or.Chromaticity,
                Turbidity = or.Turbidity,
                Temperature = or.Temperature
            };

            WaterIntakeViewBag();
            return View(wtmodel);
        }

        [HttpPost]
        public ActionResult EditWaterIntake(WaterIntakeModel wtmodel)
        {
            if (ModelState.IsValid)
            {
                WaterIntake wt = new WaterIntake() {
                    Id = wtmodel.Id,
                    LaboratoryId = wtmodel.LaboratoryId,
                    IntakeDate = wtmodel.IntakeDate,
                    LaboratoryDate = wtmodel.LaboratoryDate,
                    WorkerName = wtmodel.WorkerName,
                    StationId = wtmodel.StationId,
                };
                Chemical ch = new Chemical() {
                    Id = wtmodel.Id,
                    Residue = wtmodel.Residue,
                    Ph = wtmodel.Ph,
                    Rigidity = wtmodel.Rigidity,
                    Chlorides = wtmodel.Chlorides,
                    Sulphates = wtmodel.Sulphates,
                    Iron = wtmodel.Iron,
                    Marhan = wtmodel.Marhan,
                    Fluorine = wtmodel.Fluorine,
                    Nitrates = wtmodel.Nitrates
                };
                Organoleptic or = new Organoleptic()
                {
                    Id = wtmodel.Id,
                    Scent20 = wtmodel.Scent20,
                    Scent60 = wtmodel.Scent60,
                    Flavor = wtmodel.Flavor,
                    Chromaticity = wtmodel.Chromaticity,
                    Turbidity = wtmodel.Turbidity,
                    Temperature = wtmodel.Temperature
                };

                if (wtmodel.LaboratoryDate == null)
                {
                    wt.Status = DAL.Status.taken;
                }
                else
                {
                    wt.Status = DAL.Status.investigated;
                }

                db.Entry(wt).State = EntityState.Modified;
                db.Entry(ch).State = EntityState.Modified;
                db.Entry(or).State = EntityState.Modified;
                var k = db.WaterIntakes.OrderByDescending(a => a.Id).First();
                Station s = db.Stations.Find(wtmodel.StationId);
                if (wtmodel.Id == k.Id)
                {
                    s.Class = CheckingClass(ch, or);
                }
                db.Entry(s).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("WaterIntakes");
            }

            WaterIntakeViewBag();
            return View(wtmodel);
        }

        [HttpGet]
        [Authorize]
        public ActionResult DeleteWaterIntake(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            WaterIntake wt = db.WaterIntakes.Find(id);
            if (wt == null)
            {
                return HttpNotFound();
            }
            return View(wt);
        }

        [HttpPost, ActionName("DeleteWaterIntake")]
        public ActionResult DeleteWaterIntakeConfirmed(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            WaterIntake wt = db.WaterIntakes.Find(id);
            if (wt == null)
            {
                return HttpNotFound();
            }

            Organoleptic or = db.Organoleptics.Find(id);
            Chemical ch = db.Chemicals.Find(id);
            db.Organoleptics.Remove(or);
            db.Chemicals.Remove(ch);
            db.WaterIntakes.Remove(wt);
            db.SaveChanges();
            return RedirectToAction("WaterIntakes");
        }

        public ActionResult WaterIntakeDetails(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            WaterIntake wt = db.WaterIntakes.Find(id);

            if (wt == null)
            {
                return HttpNotFound();
            }

            Chemical ch = wt.Chemical;
            Organoleptic or = wt.Organoleptic;

            WaterIntakeModel wtmodel = new WaterIntakeModel()
            {
                Id = wt.Id,
                LaboratoryId = wt.LaboratoryId,
                IntakeDate = wt.IntakeDate,
                LaboratoryDate = wt.LaboratoryDate,
                WorkerName = wt.WorkerName,
                StationId = wt.StationId,

                Residue = ch.Residue,
                Ph = ch.Ph,
                Rigidity = ch.Rigidity,
                Chlorides = ch.Chlorides,
                Sulphates = ch.Sulphates,
                Iron = ch.Iron,
                Marhan = ch.Marhan,
                Fluorine = ch.Fluorine,
                Nitrates = ch.Nitrates,

                Scent20 = or.Scent20,
                Scent60 = or.Scent60,
                Flavor = or.Flavor,
                Chromaticity = or.Chromaticity,
                Turbidity = or.Turbidity,
                Temperature = or.Temperature
            };

            return View(wtmodel);
        }

        #endregion

        #region Stations

        // /Home/Stations
        [Authorize]
        public ActionResult Stations()
        {
            ViewBag.Title = "Stations";
            return View(db.Stations);

        }

        public ActionResult StationDetails(int id)
        {
            Station st = db.Stations.FirstOrDefault(b => b.Id == id);
            if (st != null)
                return PartialView(st);
            return HttpNotFound();
        }

        [HttpGet]
        [Authorize]
        public ActionResult EditStation(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Station st = db.Stations.Find(id);

            if (st == null)
            {
                return HttpNotFound();
            }
            return View(st);
        }

        [HttpPost]
        public ActionResult EditStation(Station st)
        {
            if (ModelState.IsValid)
            {
                db.Entry(st).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Stations");
            }

            return View(st);
        }

                [HttpGet]
                [Authorize]
                public ActionResult CreateStation()
                {
                    return View();
                }
        [HttpPost]
        public ActionResult CreateStation(Station st)
        {
            if (ModelState.IsValid)
            {
                db.Stations.Add(st);
                db.SaveChanges();

                return RedirectToAction("Stations");
            }

            return View(st);
        }

        [HttpGet]
        [Authorize]
        public ActionResult DeleteStation(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Station st = db.Stations.Find(id);
            if (st == null)
            {
                return HttpNotFound();
            }
            if (st.WaterIntakes.Count > 0)
            { return null; }
            return View(st);
        }

        [HttpPost, ActionName("DeleteStation")]
        public ActionResult DeleteStationConfirmed(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Station st = db.Stations.Find(id);
            if (st == null)
            {
                return HttpNotFound();
            }
            db.Stations.Remove(st);
            db.SaveChanges();
            return RedirectToAction("Stations");
        }

        #endregion

        #region Laboratories

        // /Home/Laboratories
        [Authorize]
        public ActionResult Laboratories()
        {
            ViewBag.Title = "Laboratories";
            return View(db.Laboratories);

        }
       
        public ActionResult LaboratoryDetails(int id)
        {
            Laboratory lab = db.Laboratories.FirstOrDefault(b => b.Id == id);
            if (lab != null)
                return PartialView(lab);
            return HttpNotFound();
        }

        [HttpGet]
        [Authorize]
        public ActionResult EditLaboratory(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Laboratory lab = db.Laboratories.Find(id);

            if (lab == null)
            {
                return HttpNotFound();
            }
            return View(lab);
        }

        [HttpPost]
        public ActionResult EditLaboratory(Laboratory lab)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lab).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Laboratories");
            }

            return View(lab);
        }

        [HttpGet]
        [Authorize]
        public ActionResult CreateLaboratory()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateLaboratory(Laboratory lab)
        {
            if (ModelState.IsValid)
            {
                db.Laboratories.Add(lab);
                db.SaveChanges();

                return RedirectToAction("Laboratories");
            }

            return View(lab);
        }

        [HttpGet]
        [Authorize]
        public ActionResult DeleteLaboratory(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Laboratory b = db.Laboratories.Find(id);
            if (b == null)
            {
                return HttpNotFound();
            }
            return View(b);
        }
        
        [HttpPost, ActionName("DeleteLaboratory")]
        public ActionResult DeleteLaboratoryConfirmed(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Laboratory b = db.Laboratories.Find(id);
            if (b == null)
            {
                return HttpNotFound();
            }
            db.Laboratories.Remove(b);
            db.SaveChanges();
            return RedirectToAction("Laboratories");
        }
        #endregion

        // /Home/
        public ActionResult Index()
        {
            ViewBag.Title = "GroundWater";
            return View();
        }

        // /Home/About
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        // /Home/Contact
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}