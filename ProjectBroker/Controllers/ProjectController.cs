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
using ProjectBroker.Models.DBModel;

namespace ProjectBroker.Controllers
{
    public class ProjectController : ApiController
    {
        private projectbrokerEntities db = new projectbrokerEntities();

        // GET: api/Project
        public IQueryable<pr_project> Getpr_project()
        {
            return db.pr_project;
        }

        // GET: api/Project/5
        [ResponseType(typeof(pr_project))]
        public IHttpActionResult Getpr_project(string id)
        {
            pr_project pr_project = db.pr_project.Find(id);
            if (pr_project == null)
            {
                return NotFound();
            }

            return Ok(pr_project);
        }

        // PUT: api/Project/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putpr_project(string id, pr_project pr_project)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != pr_project.pr_id)
            {
                return BadRequest();
            }

            db.Entry(pr_project).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!pr_projectExists(id))
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

        // POST: api/Project
        [ResponseType(typeof(pr_project))]
        public IHttpActionResult Postpr_project(pr_project pr_project)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.pr_project.Add(pr_project);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (pr_projectExists(pr_project.pr_id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = pr_project.pr_id }, pr_project);
        }

        // DELETE: api/Project/5
        [ResponseType(typeof(pr_project))]
        public IHttpActionResult Deletepr_project(string id)
        {
            pr_project pr_project = db.pr_project.Find(id);
            if (pr_project == null)
            {
                return NotFound();
            }

            db.pr_project.Remove(pr_project);
            db.SaveChanges();

            return Ok(pr_project);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool pr_projectExists(string id)
        {
            return db.pr_project.Count(e => e.pr_id == id) > 0;
        }
    }
}