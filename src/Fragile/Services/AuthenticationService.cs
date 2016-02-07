using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Fragile.Models;
using Microsoft.AspNet.Http;
using System.Security.Cryptography;

namespace Fragile.Services
{
    public class AuthenticationService
    {
        public static RandomNumberGenerator Rng { get; private set; } = RandomNumberGenerator.Create();

        public ApplicationDbContext DbContext
        {
            get;
        }

        public HttpContext HttpContext
        {
            get;
        }

        private TeamMember authorizedMember;

        public TeamMember AuthorizedMember
        {
            get
            {
                if (authorizedMember != null)
                    return authorizedMember;

                var SessionKey = HttpContext.Request.Cookies.ContainsKey("Session") ? 
                    HttpContext.Request.Cookies["Session"].ToString() : null;

                if(SessionKey != null)
                {
                    authorizedMember = DbContext.TeamMember.Where(s => s.SessionKey == SessionKey).FirstOrDefault();
                }

                return authorizedMember;
            }
            set
            {
                if (value != null)
                {
                    var SessionKey = Rng.GetBase64String(256);

                    HttpContext.Response.Cookies.Append("Session", SessionKey);
                    value.SessionKey = SessionKey;
                    DbContext.TeamMember.Update(value);
                    DbContext.SaveChanges();
                }
                else
                {
                    HttpContext.Response.Cookies.Append("Session", string.Empty);
                }
            }
        }

        public AuthenticationService(ApplicationDbContext dbContext, IHttpContextAccessor httpContext)
        {
            DbContext = dbContext;
            HttpContext = httpContext.HttpContext;
        }
    }

    static class Extensions
    {
        public static string GetBase64String(this RandomNumberGenerator rng, int length)
        {
            var randomData = new byte[length];
            rng.GetBytes(randomData);
            return Convert.ToBase64String(randomData);
        }

        public static string GetHexString(this RandomNumberGenerator rng, int length)
        {
            var randomData = new byte[length];
            rng.GetBytes(randomData);
            return BitConverter.ToString(randomData).Replace("-", "");
        }
    }
}
