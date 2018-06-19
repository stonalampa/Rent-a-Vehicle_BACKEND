using RentApp.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace RentApp.Persistance.Repository
{
    public class VehicleRepository : Repository<Vehicle, int>, IVehicleRepository //tip servis kljuc integer
    {
        public VehicleRepository(DbContext context) : base(context)
        {
        }

        public IEnumerable<Vehicle> GetAll(int pageIndex, int pageSize)
        {
            return DemoContext.Vehicles.Skip((pageIndex - 1) * pageSize).Take(pageSize);
        }

        protected RADBContext DemoContext { get { return context as RADBContext; } }
    }
}