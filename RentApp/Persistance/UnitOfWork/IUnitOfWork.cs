using RentApp.Persistance.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentApp.Persistance.UnitOfWork
{
    public interface IUnitOfWork:IDisposable
    {
        IServiceRepository Services { get; set; } //u sebi ima sve servise, moze da radi sta hoce sa servisima, prosiriti sa ostalim entitetima
        //dodala sam
        IAppUserRepository AppUsers { get; set; }
        IBranchRepository Branches { get; set; }
        IRentRepository Rents { get; set; }
        ITypeOfVehicleRepository Types { get; set; }
        IVehicleRepository Vehicles { get; set; }
        //kraj
        int Complete();
    }
}
