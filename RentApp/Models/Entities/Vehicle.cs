using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RentApp.Models.Entities
{
  public class Vehicle
  {
    public int Id { get; set; }
    public string CarModel { get; set; }
    public string Manufactor { get; set; }
    public int Year { get; set; }
    public string Description { get; set; }
    public decimal PricePerHour { get; set; }
    public bool Unavailable { get; set; }

    public string Image { get; set; }

    [ForeignKey("TypeOfVehicle")]
    public int TypeOfVehicle_Id { get; set; }
    public virtual TypeOfVehicle TypeOfVehicle { get; set; }

    [ForeignKey("Service")]
    public int Service_Id { get; set; }
    public virtual Service Service { get; set; }

  }
}
