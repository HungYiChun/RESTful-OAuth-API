using RESTful_API_OAuth.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace RESTful_API_OAuth.Controllers
{
    /// <summary>
    /// 範例API說明 - Value API Info.
    /// </summary>
    public class ValuesController : ApiController
    {
        private IdentityExtensions IE = new IdentityExtensions();

        // GET api/values
        /// <summary>
        /// 取得字串 - Get string["value1", "value2"].
        /// </summary>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/Value/Admin
        /// <summary>
        /// 限Admin權限執行 - Admin privilege execution.
        /// </summary>
        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("api/Value/Admin")]
        public IHttpActionResult GetAdmin()
        {
            ClaimsIdentity identity = (ClaimsIdentity)User.Identity;
            string Name = IE.GetName(identity);
            string Role = IE.GetRole(identity);
            string StatusName = IE.GetType(identity, "StatusName");

            return Ok("Hello, " + Name + ", 您的權限是 " + Role + "使用狀態 : " + StatusName);
        }

        // GET api/Value/Member
        /// <summary>
        /// 登入執行 - Sign in to perform.
        /// </summary>
        [HttpGet]
        [Authorize(Roles = "Member")]
        [Route("api/Value/Member")]
        public IHttpActionResult GetMember()
        {
            ClaimsIdentity identity = (ClaimsIdentity)User.Identity;
            string Name = IE.GetName(identity);
            string Role = IE.GetRole(identity);
            string StatusName = IE.GetType(identity, "StatusName");

            return Ok("Hello, " + Name + ", 您的權限是 " + Role + "使用狀態 : " + StatusName);
        }
    }
}
