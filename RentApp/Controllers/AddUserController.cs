using RentApp.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace RentApp.Controllers
{
    [System.Web.Http.RoutePrefix("Write")]
    public class AddUserController : ApiController
    {
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("WriteUser")]
        public IHttpActionResult Index(AppUser user)
        {
            Console.WriteLine(user.FullName);
            Console.WriteLine(user.dateOfBirth);
            Console.WriteLine(user.Id);
            Console.WriteLine(user.PhotoPath);
            Console.WriteLine(user.Type);
            if (user == null)
            {
                return NotFound();
            }
            else
            {
                return Ok();
            }
        }
    }
}