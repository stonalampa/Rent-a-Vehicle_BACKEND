using RentApp.Persistance.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Unity.Attributes;
using RentApp.Models.Entities;

namespace RentApp.Persistance.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context;
      
        [Dependency]
        public IServiceRepository Services { get; set; }
        [Dependency]
        public IRepository<AppUser, int> Users { get; set; }
        [Dependency]
        public IRepository<BranchOffice, int> BranchOffices { get; set; }
        [Dependency]
        public IRepository<Vehicle, int> Vehicles { get; set; }
        [Dependency]
        public IRepository<Rating, int> Ratings { get; set; }


        public UnitOfWork(DbContext context)
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