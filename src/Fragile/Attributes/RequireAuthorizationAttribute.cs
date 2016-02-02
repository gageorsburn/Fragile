using Microsoft.AspNet.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Fragile.Services;
using Microsoft.AspNet.Mvc;

namespace Fragile.Attributes
{
    public class RequireAuthenticationAttribute : Attribute, IFilterFactory, IOrderedFilter
    {
        public int Order { get; set; }

        public IFilterMetadata CreateInstance(IServiceProvider serviceProvider)
        {
            var authenticationService = serviceProvider.GetRequiredService<AuthenticationService>();
            var urlHelper = serviceProvider?.GetRequiredService<IUrlHelper>();

            return new RequireAuthenticationAuthorizationFilter(authenticationService, urlHelper);
        }
    }

    public class RequireAuthenticationAuthorizationFilter : IAuthorizationFilter
    {
        private AuthenticationService AuthenticationService { get; }
        private IUrlHelper Url { get; }

        public RequireAuthenticationAuthorizationFilter(AuthenticationService authenticationService, IUrlHelper urlHelper)
        {
            AuthenticationService = authenticationService;
            Url = urlHelper;
        }

        public virtual void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext == null)
                throw new ArgumentNullException(nameof(filterContext));

            if (AuthenticationService.AuthorizedMember == null)
                HandleNotAuthorizedRequest(filterContext);
        }

        protected virtual void HandleNotAuthorizedRequest(AuthorizationContext filterContext)
        {
            var request = filterContext.HttpContext.Request;
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("return", string.Concat(request.PathBase.ToUriComponent(), request.Path.ToUriComponent()));
            filterContext.Result = new RedirectToActionResult("SignIn", "Member", parameters);
        }
    }
}
