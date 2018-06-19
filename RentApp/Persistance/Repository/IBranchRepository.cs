using RentApp.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentApp.Persistance.Repository
{
    public interface IBranchRepository : IRepository<Branch, int>
    {
        IEnumerable<Branch> GetAll(int pageIndex, int pageSize); //pokupi mi samo onoliko koliko ja kazem
    }
}
