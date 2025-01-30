using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CoreReleaseAutomation.Helpers
{
    public static class AuthenticationHelper
    {
        const string LDAP_PATH = "LDAP://" + "decisionintellect.corp";
        const string LDAP_DOMAIN = "decisionintellect.corp";

        public static bool IsLoggedIn(IHttpContextAccessor httpContextAccessor) => httpContextAccessor.HttpContext.User.Identity.IsAuthenticated;

        public static string UserId(IHttpContextAccessor httpContextAccessor)
        {
            var userName = httpContextAccessor.HttpContext.User.Identity.Name;
            
            return userName.Substring(userName.IndexOf(@"\") + 1);
        }
        

        public static bool Login(string login, string password)
        {
            using var context = new PrincipalContext(ContextType.Domain, LDAP_DOMAIN, login, password);
            return context.ValidateCredentials(login, password);
        }
    }
}
