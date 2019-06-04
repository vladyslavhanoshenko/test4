using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using DAL.Entities;

namespace DAL.EF
{
    public class GroundwaterContext : DbContext
    {
        public DbSet<Chemical> Chemicals { get; set; }
        public DbSet<Laboratory> Laboratories { get; set; }
        public DbSet<OrganolepticNorm> OrganolepticNorms { get; set; }
        public DbSet<ChemicalNorm> ChemicalNorms { get; set; }
        public DbSet<Organoleptic> Organoleptics { get; set; }
        public DbSet<ScentFlavor> ScentFlavors { get; set; }
        public DbSet<Station> Stations { get; set; }
        public DbSet<WaterIntake> WaterIntakes { get; set; }
        //public DbSet<Worker> Workers { get; set; }

        static GroundwaterContext()
        {
            Database.SetInitializer<GroundwaterContext>(new GroundwaterInitializer());
        }

        public GroundwaterContext(string connectionString) : base(connectionString)
        { }
    }
    

}
