using RESTful_API_OAuth.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using System.Web.Http.Description;

namespace RESTful_API_OAuth.Areas.Auth.Controllers
{
    /// <summary>
    /// 使用者API說明 - User API Info.
    /// </summary>
    public class UsersController : ApiController
    {
        private int STATUS_INREVIEW = 2;  //審查中
        private MLMDBEntities db = new MLMDBEntities();
        private IdentityExtensions IE = new IdentityExtensions();

        // GET: api/Users
        /// <summary>
        /// 取得 所有 使用者資料 - All User Detail.
        /// </summary>
        [Authorize(Roles = "Root Admin, Admin")]
        public IQueryable<ViewUserDetail> GetUsers()
        {
            return db.ViewUserDetail;
        }

        // GET: api/Users/5
        /// <summary>
        /// 取得 指定 使用者資料 - Users Detail.
        /// </summary>
        [ResponseType(typeof(Users))]
        [Authorize(Roles = "Root Admin, Admin")]
        public IHttpActionResult GetUsers(string id)
        {
            ViewUserDetail viewUserDetail = db.ViewUserDetail.FirstOrDefault(x => x.Id == id);
            if (viewUserDetail == null)
            {
                return NotFound();
            }

            return Ok(viewUserDetail);
        }

        // GET: api/UserSelf/5
        /// <summary>
        /// 取得 個人 使用者資料 - User Self Detail.
        /// </summary>
        [ResponseType(typeof(Users))]
        [Authorize]
        [Route("api/UserSelf")]
        public IHttpActionResult GetUserSelf()
        {
            ClaimsIdentity identity = (ClaimsIdentity)User.Identity;
            string Id = IE.GetNameIdentifier(identity);

            ViewUserDetail viewUserDetail = db.ViewUserDetail.FirstOrDefault(x => x.Id == Id);
            if (viewUserDetail == null)
            {
                return NotFound();
            }

            return Ok(viewUserDetail);
        }



        // POST: api/Users/{roleId:int}/{statusId:int}
        /// <summary>
        /// Admin新增 使用者資料 - Admin Create User.
        /// </summary>
        [ResponseType(typeof(Users))]
        [Authorize(Roles = "Root Admin, Admin")]
        [Route("api/Users/{roleId:int}/{statusId:int}")]
        public IHttpActionResult PostAdminUsers(Users users, int roleId, int statusId)
        {
            Users existUser = db.Users.FirstOrDefault(x => x.Username == users.Username || x.Email == users.Email);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else if (existUser != null)
            {
                return Ok("Your account or email is already in use");
            }
            Roles role = db.Roles.FirstOrDefault(x => x.Id == roleId);
            Status status = db.Status.FirstOrDefault(x => x.Id == statusId);

            users.Id = Guid.NewGuid().ToString();
            users.EditTime = DateTime.Now;
            users.Roles = role;
            users.Status = status;

            db.Users.Add(users);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (UsersExists(users.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Ok(users);
        }

        // POST: api/Users
        /// <summary>
        /// 申請新增 使用者資料 - Apply Create User.
        /// </summary>
        [ResponseType(typeof(Users))]
        [AllowAnonymous]
        [Route("api/Users/{roleId:int}")]
        public IHttpActionResult PostUsers(Users users, int roleId)
        {
            Users existUser = db.Users.FirstOrDefault(x => x.Username == users.Username || x.Email == users.Email);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else if (existUser != null)
            {
                return Ok("Your account or email is already in use");
            }
            Roles role = db.Roles.FirstOrDefault(x => x.Id == roleId);
            Status status = db.Status.FirstOrDefault(x => x.Id == STATUS_INREVIEW);

            users.Id = Guid.NewGuid().ToString();
            users.EditTime = DateTime.Now;
            users.Roles = role;
            users.Status = status;

            db.Users.Add(users);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (UsersExists(users.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Ok(users);
        }



        // PUT: api/Users/5
        /// <summary>
        /// 更新 使用者資料 - Update User.
        /// </summary>
        [ResponseType(typeof(Users))]
        [Authorize(Roles = "Root Admin, Admin")]
        public IHttpActionResult PutUsers(string id, Users users)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != users.Id)
            {
                return BadRequest();
            }
            users.EditTime = DateTime.Now;

            db.Entry(users).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsersExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(users);
        }

        // PUT: api/UserRole/{userId}/{roleId:int}
        /// <summary>
        /// 更新 使用者權限 - Update UserRole.
        /// </summary>
        [HttpPut]
        [Authorize(Roles = "Root Admin, Admin")]
        [Route("api/UserRole/{userId}/{roleId:int}")]
        public IHttpActionResult PutUserRole(string userId, int roleId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Users user = db.Users.FirstOrDefault(x => x.Id == userId);
            Roles role = db.Roles.FirstOrDefault(x => x.Id == roleId);
            user.Roles = role;


            db.Entry(user).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (UsersExists(userId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Ok("Success");
        }

        // PUT: api/UserStatus/{userId}/{statusId:int}
        /// <summary>
        /// 更新 使用者狀態 - Update UserStatus.
        /// </summary>
        [HttpPut]
        [Authorize(Roles = "Root Admin, Admin")]
        [Route("api/UserStatus/{userId}/{statusId:int}")]
        public IHttpActionResult PutUserStatus(string userId, int statusId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Users user = db.Users.FirstOrDefault(x => x.Id == userId);
            Status status = db.Status.FirstOrDefault(x => x.Id == statusId);
            user.Status = status;


            db.Entry(user).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (UsersExists(userId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Ok("Success");
        }



        // DELETE: api/Users/5
        /// <summary>
        /// 刪除 使用者資料 - Delete User.
        /// </summary>
        [ResponseType(typeof(Users))]
        [Authorize(Roles = "Root Admin, Admin")]
        public IHttpActionResult DeleteUsers(string id)
        {
            Users users = db.Users.Find(id);
            if (users == null)
            {
                return NotFound();
            }

            db.Users.Remove(users);
            db.SaveChanges();

            return Ok(users);
        }



        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UsersExists(string id)
        {
            return db.Users.Count(e => e.Id == id) > 0;
        }
    }
}
