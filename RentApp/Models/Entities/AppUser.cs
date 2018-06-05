using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RentApp.Models.Entities
{
    public enum UserType { ADMIN = 0, MANAGER = 1, USER =2 };

    public class AppUser
    {
        [Key]
        public int Id { get; set; }
        public string FullName { get; set; }
        
        public Nullable<DateTime> dateOfBirth { get; set; }
        public string PhotoPath { get; set; }
        public UserType Type { get; set; }
        

        
        
    }
}