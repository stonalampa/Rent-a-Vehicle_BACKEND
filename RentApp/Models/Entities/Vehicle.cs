using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RentApp.Models.Entities
{
    public class Vehicle
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Driver { get; set; }
        public bool Available { get; set; }
        public string BranchOffice { get; set; }
        public int PricePerHour { get; set; }

    }
}