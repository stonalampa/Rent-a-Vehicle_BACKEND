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
    [RoutePrefix("branch")]
    public class BranchesController : ApiController
    {
        private RADBContext db = new RADBContext();
        public const string ServerUrl = "http://localhost:51680";
        [HttpGet]
        [Route("branches", Name = "BranchApi")]
        public IHttpActionResult GetBranches()
        {
            var l = db.Branches.ToList();
            return Ok(l);
        }
        [HttpGet]
        [Route("branch/{id}")]
        [ResponseType(typeof(Branch))]
        public IHttpActionResult GetBranch(int id)
        {
            Branch branch = db.Branches.Find(id);
            if (branch == null)
            {
                return NotFound();
            }

            return Ok(branch);
        }
        [Authorize(Roles = "Manager,Admin")]
        [HttpPut]
        [Route("branch/{id}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutBranch(int id, Branch branch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != branch.Id)
            {
                return BadRequest();
            }

            db.Entry(branch).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BranchExists(id))
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
        [Authorize(Roles = "Manager")]
        [HttpPost]
        [Route("branch")]
        [ResponseType(typeof(Branch))]
        public IHttpActionResult PostBranch(Branch branch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Branches.Add(branch);
            db.SaveChanges();

            return CreatedAtRoute("BranchApi", new { id = branch.Id }, branch);
        }

        // DELETE: api/Branches/5
        [Authorize(Roles = "Manager,Admin")]
        [HttpDelete]
        [Route("branch/{id}")]
        [ResponseType(typeof(Branch))]
        public IHttpActionResult DeleteBranch(int id)
        {
            Branch branch = db.Branches.Find(id);
            if (branch == null)
            {
                return NotFound();
            }

            db.Branches.Remove(branch);
            db.SaveChanges();

            return Ok(branch);
        }
        [HttpGet]
        [Route("branch/logo/{id}")]
        public string GetImage(int id)
        {
            Branch branch = this.db.Branches.FirstOrDefault(x => x.Id == id);
            if (branch.Logo == null)
            {
                return null;
            }

            var filePath = branch.Logo;
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

        private bool BranchExists(int id)
        {
            return db.Branches.Count(e => e.Id == id) > 0;
        }
    }
}