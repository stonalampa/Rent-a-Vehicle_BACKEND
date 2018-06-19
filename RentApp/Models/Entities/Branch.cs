using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RentApp.Models.Entities
{
  public class Branch
  {
    public int Id { get; set; }
    public string Logo { get; set; }
    public string Address { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    
    [ForeignKey ("Service")]
    public int Service_Id { get; set; }
    public virtual Service Service { get; set; }

  }
}
