using RentApp.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace RentApp.Persistance.Repository
{
    public class ServiceRepository : Repository<Service, int>, IServiceRepository //tip servis kljuc integer
    {
        public ServiceRepository(DbContext context) : base(context)
        {
        }

        public IEnumerable<Service> GetAll(int pageIndex, int pageSize)
        {
            return DemoContext.Services.Skip((pageIndex - 1) * pageSize).Take(pageSize);
        }

        protected RADBContext DemoContext { get { return context as RADBContext; } }
    }
}