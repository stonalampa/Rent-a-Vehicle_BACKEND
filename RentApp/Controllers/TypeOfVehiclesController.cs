using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using RentApp.Models.Entities;
using RentApp.Persistance;

namespace RentApp.Controllers

{
    [RoutePrefix("vehicleType")]
    public class TypeOfVehiclesController : ApiController
    {
        private RADBContext db = new RADBContext();

        [HttpGet]
        [Route("vehicleTypes", Name = "VehicleTypesApi")]
        public IHttpActionResult GetTypes()
        {
            var l = this.db.Types.ToList();
            return Ok(l);
        }

        [HttpGet]
        [Route("vehicleType/{id}")]
        [ResponseType(typeof(TypeOfVehicle))]
        public IHttpActionResult GetTypeOfVehicle(int id)
        {
            TypeOfVehicle typeOfVehicle = db.Types.Find(id);
            if (typeOfVehicle == null)
            {
                return NotFound();
            }

            return Ok(typeOfVehicle);
        }
        [Authorize(Roles = "Admin")]
        [HttpPut]
        [Route("vehicleType/{id}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTypeOfVehicle(int id, TypeOfVehicle typeOfVehicle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != typeOfVehicle.Id)
            {
                return BadRequest();
            }

            db.Entry(typeOfVehicle).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TypeOfVehicleExists(id))
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
        [Authorize(Roles  = "Admin")]
        [HttpPost]
        [Route("vehicleType")]
        [ResponseType(typeof(TypeOfVehicle))]
        public IHttpActionResult PostTypeOfVehicle(TypeOfVehicle typeOfVehicle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Types.Add(typeOfVehicle);
            db.SaveChanges();

            return CreatedAtRoute("VehicleTypesApi", new { id = typeOfVehicle.Id }, typeOfVehicle);
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete]
        [Route("vehicleType/{id}")]
        [ResponseType(typeof(TypeOfVehicle))]
        public IHttpActionResult DeleteTypeOfVehicle(int id)
        {
            TypeOfVehicle typeOfVehicle = db.Types.Find(id);
            if (typeOfVehicle == null)
            {
                return NotFound();
            }

            db.Types.Remove(typeOfVehicle);
            db.SaveChanges();

            return Ok(typeOfVehicle);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TypeOfVehicleExists(int id)
        {
            return db.Types.Count(e => e.Id == id) > 0;
        }
    }
}