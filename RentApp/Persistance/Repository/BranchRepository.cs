using RentApp.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace RentApp.Persistance.Repository
{
    public class BranchRepository : Repository<Branch, int>, IBranchRepository //tip servis kljuc integer
    {
        public BranchRepository(DbContext context) : base(context)
        {
        }

        public IEnumerable<Branch> GetAll(int pageIndex, int pageSize)
        {
            return DemoContext.Branches.Skip((pageIndex - 1) * pageSize).Take(pageSize);
        }

        protected RADBContext DemoContext { get { return context as RADBContext; } }
    }
}