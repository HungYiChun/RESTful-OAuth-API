using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace RESTful_API_OAuth.Models
{
    public class VerifiedIdentity : IDisposable
    {
        private MLMDBEntities db = new MLMDBEntities();
        private string STATUS_NORMAL = "正常";  //使用者狀態: 正常

        public string VerifyIdentity(string Username, string Password)
        {
            ViewUserDetail user = db.ViewUserDetail.FirstOrDefault(x => x.Username == Username && x.StatusName == STATUS_NORMAL);
            if (user != null && user.Password == Password)
            {
                return user.Id;
            }
            return null;
        }

        public ClaimsIdentity IdentityAddClaim(ClaimsIdentity identity, string Username)
        {
            ViewUserDetail user = db.ViewUserDetail.FirstOrDefault(x => x.Username == Username);

            identity.AddClaim(new Claim(ClaimTypes.GivenName, user.Username));
            identity.AddClaim(new Claim(ClaimTypes.Name, user.Name));
            identity.AddClaim(new Claim(ClaimTypes.Role, user.RoleName));
            identity.AddClaim(new Claim("StatusName", user.StatusName));
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id));

            return identity;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // dispose managed resources
                if (db != null)
                {
                    db.Dispose();
                    db = null;
                }
            }
            // free native resources
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}