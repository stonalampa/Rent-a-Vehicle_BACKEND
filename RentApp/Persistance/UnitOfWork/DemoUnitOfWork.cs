using RentApp.Persistance.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Unity.Attributes;

namespace RentApp.Persistance.UnitOfWork
{
    public class DemoUnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context;

        [Dependency]
        public IServiceRepository Services { get; set; }
        //dodala sam 
        [Dependency]
        public IAppUserRepository AppUsers { get; set; }
        [Dependency]
        public IBranchRepository Branches { get; set; }
        [Dependency]
        public IRentRepository Rents { get; set; }
        [Dependency]
        public ITypeOfVehicleRepository Types { get; set; }
        [Dependency]
        public IVehicleRepository Vehicles { get; set; }
        //kraj
        public DemoUnitOfWork(DbContext context)
        {
            _context = context;
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}