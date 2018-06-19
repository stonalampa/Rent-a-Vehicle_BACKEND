using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using RentApp.Models.Entities;
using RentApp.Persistance;

namespace RentApp.Controllers
{
    [RoutePrefix("service")]
    public class ServicesController : ApiController
    {
        private RADBContext db = new RADBContext();
        public const string ServerUrl = "http://localhost:51680";
        public const int MaxImageSize = 1024 * 1024 * 6;

        public ServicesController(DbContext context)
        {
            db = context as RADBContext;
        }

        [HttpGet]
        [Route("services", Name = "ServiceApi")]
        public IHttpActionResult GetServices()
        {
            var l = db.Services.ToList();
            return Ok(l);
        }

        [HttpGet]
        [Route ("service/{id}")]
        [ResponseType(typeof(Service))]
        public IHttpActionResult GetService(int id)
        {
            Service service = db.Services.Find(id);
            if (service == null)
            {
                return NotFound();
            }

            return Ok(service);
        }
        [HttpPut]
        [Route("service/{id}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutService(int id, Service service)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != service.Id)
            {
                return BadRequest();
            }

            db.Entry(service).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ServiceExists(id))
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
        [HttpPost]
        [Route("service")]
        [ResponseType(typeof(Service))]
        public IHttpActionResult PostService(Service service)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Services.Add(service);
            db.SaveChanges();

            return CreatedAtRoute("ServiceApi", new { id = service.Id }, service);
        }

        // DELETE: api/Services/5
        [Authorize(Roles = "Manager,Admin")]
        [HttpDelete]
        [Route("service/{id}")]
        [ResponseType(typeof(Service))]
        public IHttpActionResult DeleteService(int id)
        {
            Service service = db.Services.Find(id);
            if (service == null)
            {
                return NotFound();
            }
            db.Services.Remove(service);
            db.SaveChanges();

            return Ok(service);
        }

        [HttpGet]
        [Route("service/logo/{id}")]
        public string GetImage(int id)
        {
            Service service = this.db.Services.FirstOrDefault(x => x.Id == id);
            if (service.Logo == null)
            {
                return null;
            }

            var filePath = service.Logo;
            var fullFilePath = HttpContext.Current.Server.MapPath("~/Content/Logos/" + Path.GetFileName(filePath));
            var relativePath = ServerUrl + "/Content/Logos/" + Path.GetFileName(filePath);

            if (File.Exists(fullFilePath))
            {
                return relativePath;
            }

            return null;
        }

        [HttpPost]
        [Route("image/upload")]
        [AllowAnonymous]
        public async Task<HttpResponseMessage> PostUserImage()
        {
            try
            {
                var httpRequest = HttpContext.Current.Request;
                var fileCount = httpRequest.Files.Count;

                if (fileCount > 0)
                {
                    for (int i = 0; i < fileCount; i++)
                    {
                        var postedFile = httpRequest.Files[i];
                        IList<string> AllowedExtension = new List<string> { ".jpg", ".gif", ".png",".jpeg" };
                        var fileExtension = postedFile.FileName.Substring(postedFile.FileName.LastIndexOf('.'));
                        var extension = fileExtension.ToLower();

                        if (!AllowedExtension.Contains(extension))
                        {
                            return Request.CreateResponse(HttpStatusCode.BadRequest, "Image extension is not valid.");
                        }
                        else if (postedFile.ContentLength > MaxImageSize)
                        {
                            return Request.CreateResponse(HttpStatusCode.BadRequest, "Image size is greater than one MB.");
                        }
                        var filePath = HttpContext.Current.Server.MapPath("~/Content/Logos/" + postedFile.FileName);
                        postedFile.SaveAs(filePath);
                        return Request.CreateResponse(HttpStatusCode.Created, "Image successfuly posted.");
                    }
                }
                return Request.CreateResponse(HttpStatusCode.NotFound, "Something wrong.");
            }
            catch (Exception)
            {

                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ServiceExists(int id)
        {
            return db.Services.Count(e => e.Id == id) > 0;
        }
    }
}
