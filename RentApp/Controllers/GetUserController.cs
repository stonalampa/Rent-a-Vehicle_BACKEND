using RentApp.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Description;
using System.Web.Mvc;
using System.Web.Http;
using System.Web.Http.Results;



namespace RentApp.Controllers
{
    [System.Web.Http.RoutePrefix("Read")]
    public class GetUserController : ApiController
    {
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("ReadUser")]
        [ResponseType(typeof(AppUser))]
        public IHttpActionResult ReturnUser()
        {
            AppUser testUser = new AppUser();
            testUser.Id = 1;
            testUser.PhotoPath = "nema za sad";
            testUser.Type = UserType.ADMIN;
            testUser.FullName = "Stojan Stojanovic";
            testUser.dateOfBirth = DateTime.Now;

            if (testUser == null)
            {

                return NotFound();
            }
            return Ok(testUser);
        }


    }
}