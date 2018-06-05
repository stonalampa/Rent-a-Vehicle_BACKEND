using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RentApp.Models.Entities
{
    //grade the quality of service
    public class Rating
    {
        public int Id { get; set; }
        public DateTime timestamp { get; set; }
        public int Grade { get; set; }
        public string Comment { get; set; }
        public int ServiceID { get; set; }
    }
}