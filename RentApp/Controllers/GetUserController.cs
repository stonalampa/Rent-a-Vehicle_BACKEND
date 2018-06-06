using RentApp.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RentApp.Controllers
{
    public class GetUserController : Controller
    {
        [HttpGet]
        public ActionResult ReturnUser()
        {
            AppUser testUser = new AppUser();
            testUser.Id = 1;
            testUser.PhotoPath = "nema za sad";
            testUser.Type = UserType.ADMIN;
            testUser.FullName = "Stojan Stojanovic";
            testUser.dateOfBirth = DateTime.Now;

            return View();
        }

        
    }
}