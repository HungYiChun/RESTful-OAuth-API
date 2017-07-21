using RESTful_API_OAuth.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace RESTful_API_OAuth.Areas.Auth.Controllers
{
    /// <summary>
    /// 使用者狀態API說明 - User Status API Info.
    /// </summary>
    public class StatusController : ApiController
    {
        private MLMDBEntities db = new MLMDBEntities();

        // GET: api/Status
        /// <summary>
        /// 取得 所有 狀態資料 - All Status Detail.
        /// </summary>
        [Authorize(Roles = "Root Admin, Admin")]
        public IQueryable<Status> GetStatus()
        {
            return db.Status;
        }

        // GET: api/Status/5
        /// <summary>
        /// 取得 指定 狀態資料 - Status Detail.
        /// </summary>
        [ResponseType(typeof(Status))]
        [Authorize(Roles = "Root Admin, Admin")]
        public IHttpActionResult GetStatus(int id)
        {
            Status status = db.Status.Find(id);
            if (status == null)
            {
                return NotFound();
            }

            return Ok(status);
        }

        // PUT: api/Status/5
        /// <summary>
        /// 更新 狀態資料 - Update Status.
        /// </summary>
        [ResponseType(typeof(Status))]
        [Authorize(Roles = "Root Admin, Admin")]
        public IHttpActionResult PutStatus(int id, Status status)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != status.Id)
            {
                return BadRequest();
            }

            db.Entry(status).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StatusExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(status);
        }

        // POST: api/Status
        /// <summary>
        /// 新增 狀態資料 - Create Status.
        /// </summary>
        [ResponseType(typeof(Status))]
        [Authorize(Roles = "Root Admin, Admin")]
        public IHttpActionResult PostStatus(Status status)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Status.Add(status);
            db.SaveChanges();

            return Ok(status);
        }

        // DELETE: api/Status/5
        /// <summary>
        /// 刪除 狀態資料 - Delete Status.
        /// </summary>
        [ResponseType(typeof(Status))]
        [Authorize(Roles = "Root Admin, Admin")]
        public IHttpActionResult DeleteStatus(int id)
        {
            Status status = db.Status.Find(id);
            if (status == null)
            {
                return NotFound();
            }

            db.Status.Remove(status);
            db.SaveChanges();

            return Ok(status);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool StatusExists(int id)
        {
            return db.Status.Count(e => e.Id == id) > 0;
        }
    }
}
