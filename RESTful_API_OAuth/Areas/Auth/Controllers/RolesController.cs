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
    /// 權限API說明 - Role API Info.
    /// </summary>
    public class RolesController : ApiController
    {
        private RESTful_API_DB_Model db = new RESTful_API_DB_Model();

        // GET: api/Roles
        /// <summary>
        /// 取得 所有 權限資料 - All Roles Detail.
        /// </summary>
        [Authorize(Roles = "Admin")]
        public IQueryable<Roles> GetRoles()
        {
            return db.Roles;
        }

        // GET: api/Roles/5
        /// <summary>
        /// 取得 指定 權限資料 - Role Detail.
        /// </summary>
        [ResponseType(typeof(Roles))]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult GetRoles(int id)
        {
            Roles roles = db.Roles.Find(id);
            if (roles == null)
            {
                return NotFound();
            }

            return Ok(roles);
        }

        // PUT: api/Roles/5
        /// <summary>
        /// 更新 權限資料 - Update Role.
        /// </summary>
        [ResponseType(typeof(Roles))]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult PutRoles(int id, Roles roles)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != roles.Id)
            {
                return BadRequest();
            }

            db.Entry(roles).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RolesExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(roles);
        }

        // POST: api/Roles
        /// <summary>
        ///  新增 權限資料 - Create Role.
        /// </summary>
        [ResponseType(typeof(Roles))]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult PostRoles(Roles roles)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Roles.Add(roles);
            db.SaveChanges();

            return Ok(roles);
        }

        // DELETE: api/Roles/5
        /// <summary>
        /// 刪除 權限資料 - Delete Role.
        /// </summary>
        [ResponseType(typeof(Roles))]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult DeleteRoles(int id)
        {
            Roles roles = db.Roles.Find(id);
            if (roles == null)
            {
                return NotFound();
            }

            db.Roles.Remove(roles);
            db.SaveChanges();

            return Ok(roles);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RolesExists(int id)
        {
            return db.Roles.Count(e => e.Id == id) > 0;
        }
    }
}
