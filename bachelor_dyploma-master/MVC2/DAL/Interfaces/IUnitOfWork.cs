using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;

namespace DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Chemical> Chemicals { get; }
        IRepository<Organoleptic> Organoleptics { get; }
        IRepository<Laboratory> Laboratories { get; }
        IRepository<ScentFlavor> ScentFlavors { get; }
        //IRepository<Station> Stations { get; }
        IRepository<WaterIntake> WaterIntakes { get; }
        //IRepository<Worker> Workers { get; }
        void Save();
    }
}
