using Microsoft.Owin.Security.OAuth;
using RESTful_API_OAuth.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace RESTful_API_OAuth
{
    public class BasicAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        private VerifiedIdentity VI = new VerifiedIdentity();

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();

            return base.ValidateClientAuthentication(context);
        }

        public override Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            ClaimsIdentity identity = new ClaimsIdentity(context.Options.AuthenticationType);

            if (VI.VerifyIdentity(context.UserName, context.Password) != null)
            {
                identity = VI.IdentityAddClaim(identity, context.UserName);
                context.Validated(identity);
            }
            else
            {
                context.SetError("invalid_grant", "Provided username and password is incorrect");
            }

            return base.GrantResourceOwnerCredentials(context);
        }
    }
}