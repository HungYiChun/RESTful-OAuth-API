using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;

namespace RESTful_API_OAuth.Models
{
    public class IdentityExtensions
    {
        public string GetNameIdentifier(IIdentity identity)
        {
            return GetClaimsValue(identity, ClaimTypes.NameIdentifier);
        }

        public string GetUsername(IIdentity identity)
        {
            return GetClaimsValue(identity, ClaimTypes.GivenName);
        }

        public string GetName(IIdentity identity)
        {
            return GetClaimsValue(identity, ClaimTypes.Name);
        }

        public string GetRole(IIdentity identity)
        {
            return GetClaimsValue(identity, ClaimTypes.Role);
        }

        public string GetEmail(IIdentity identity)
        {
            return GetClaimsValue(identity, ClaimTypes.Email);
        }

        public string GetType(IIdentity identity, string claimTypes)
        {
            return GetClaimsValue(identity, claimTypes);
        }

        private static string FindFirstValue(ClaimsIdentity identity, string claimType)
        {
            Claim first = identity.FindFirst(claimType);

            if (first != null)
            {
                return first.Value;
            }
            else
            {
                return null;
            }
        }

        private static string GetClaimsValue(IIdentity identity, string claimType)
        {
            if (identity == null)
            {
                throw new ArgumentNullException("identity");
            }

            ClaimsIdentity claimsIdentity = identity as ClaimsIdentity;

            if (claimsIdentity != null)
            {
                return FindFirstValue(claimsIdentity, claimType);
            }
            else
            {
                return null;
            }
        }
    }
}