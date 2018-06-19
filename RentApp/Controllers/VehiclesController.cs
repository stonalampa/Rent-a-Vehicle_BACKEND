using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using RentApp.Models.Entities;
using RentApp.Persistance;

namespace RentApp.Controllers
{
    [RoutePrefix("vehicle")]
    public class VehiclesController : ApiController
    {
        private RADBContext db = new RADBContext();
        public const string ServerUrl = "http://localhost:51680";
        [HttpGet]
        [Route("vehicles", Name = "VehicleApi")]
        public IHttpActionResult GetVehicles()
        {
            var l = db.Vehicles.ToList();
            return Ok(l);
        }

        [HttpGet]
        [Route("vehicle/{id}")]
        [ResponseType(typeof(Vehicle))]
        public IHttpActionResult GetVehicle(int id)
        {
            Vehicle vehicle = db.Vehicles.Find(id);
            if (vehicle == null)
            {
                return NotFound();
            }

            return Ok(vehicle);
        }
        [Authorize (Roles ="Manager,Admin")]
        [HttpPut]
        [Route("vehicle/{id}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutVehicle(int id, Vehicle vehicle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != vehicle.Id)
            {
                return BadRequest();
            }

            db.Entry(vehicle).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VehicleExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }
        [Authorize (Roles ="Manager")]
        [HttpPost]
        [Route("vehicle")]
        [ResponseType(typeof(Vehicle))]
        public IHttpActionResult PostVehicle(Vehicle vehicle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Vehicles.Add(vehicle);
            db.SaveChanges();

            return CreatedAtRoute("VehicleApi", new { id = vehicle.Id }, vehicle);
        }
        [Authorize(Roles = "Manager,Admin")]
        [HttpDelete]
        [Route("vehicle/{id}")]
        [ResponseType(typeof(Vehicle))]
        public IHttpActionResult DeleteVehicle(int id)
        {
            Vehicle vehicle = db.Vehicles.Find(id);
            if (vehicle == null)
            {
                return NotFound();
            }

            db.Vehicles.Remove(vehicle);
            db.SaveChanges();

            return Ok(vehicle);
        }
        [HttpGet]
        [Route("vehicle/logo/{id}")]
        public string GetImage(int id)
        {
            Vehicle vehicle = this.db.Vehicles.FirstOrDefault(x => x.Id == id);
            if (vehicle.Image == null)
            {
                return null;
            }

            var filePath = vehicle.Image;
            var fullFilePath = HttpContext.Current.Server.MapPath("~/Content/Logos/" + Path.GetFileName(filePath));
            var relativePath = ServerUrl + "/Content/Logos/" + Path.GetFileName(filePath);

            if (File.Exists(fullFilePath))
            {
                return relativePath;

            }
            return null;
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool VehicleExists(int id)
        {
            return db.Vehicles.Count(e => e.Id == id) > 0;
        }
    }
}
