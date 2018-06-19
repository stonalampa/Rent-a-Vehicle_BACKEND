using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RentApp.Models.Entities
{
  public class TypeOfVehicle
  {
    public TypeOfVehicle()
    {
      this.Vehicles = new HashSet<Vehicle>();
    }
    public int Id { get; set; }
    public string Name { get; set; }

    public virtual ICollection<Vehicle> Vehicles { get; set; }
  }
}
