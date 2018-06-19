using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RentApp.Models.Entities
{
    public class Service
    {
        public Service()
        {
            this.Branches = new HashSet<Branch>();
            this.Vehicles = new HashSet<Vehicle>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Logo { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        
        public virtual ICollection<Vehicle> Vehicles { get; set; }
        public virtual ICollection<Branch> Branches { get; set; }
    }
}
