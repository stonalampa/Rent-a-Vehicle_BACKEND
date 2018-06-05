using RentApp.Models.Entities;
using RentApp.Persistance.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentApp.Persistance.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<AppUser, int> Users { get; set; }
        IServiceRepository Services { get; set; }
        IRepository<BranchOffice, int> BranchOffices { get; set; }
        IRepository<Vehicle, int> Vehicles { get; set; }
        IRepository<Rating, int> Ratings { get; set; }

        int Complete();
    }
}
