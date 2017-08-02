using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace RESTful_API_OAuth.Models
{
    public class VerifiedIdentity : IDisposable
    {
        private RESTful_API_DB_Model db = new RESTful_API_DB_Model();
        private string STATUS_NORMAL = "Normal";  //使用者狀態: 正常

        public string VerifyIdentity(string Username, string Password)
        {
            string sqlquery = @"SELECT          U.Id, U.Email, U.Username, U.Password, U.Name, U.PhoneNumber, U.EditTime, R.RoleName, S.StatusName
                                FROM            dbo.Users AS U INNER JOIN
                                                dbo.Roles AS R ON R.Id = U.Roles_Id INNER JOIN
                                                dbo.Status AS S ON S.Id = U.Status_Id
                                WHERE           Username = {0} and StatusName = {1}";
            ViewUserDetail user = db.Database.SqlQuery<ViewUserDetail>(sqlquery, Username, STATUS_NORMAL).First();
            //ViewUserDetail user = db.ViewUserDetail.FirstOrDefault(x => x.Username == Username && x.StatusName == STATUS_NORMAL);

            if (user != null && user.Password == Password)
            {
                return user.Id;
            }
            return null;
        }

        public ClaimsIdentity IdentityAddClaim(ClaimsIdentity identity, string Username)
        {
            string sqlquery = @"SELECT          U.Id, U.Email, U.Username, U.Password, U.Name, U.PhoneNumber, U.EditTime, R.RoleName, S.StatusName
                                FROM            dbo.Users AS U INNER JOIN
                                                dbo.Roles AS R ON R.Id = U.Roles_Id INNER JOIN
                                                dbo.Status AS S ON S.Id = U.Status_Id
                                WHERE           Username = {0}";
            ViewUserDetail user = db.Database.SqlQuery<ViewUserDetail>(sqlquery, Username).First();
            //ViewUserDetail user = db.ViewUserDetail.FirstOrDefault(x => x.Username == Username);

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