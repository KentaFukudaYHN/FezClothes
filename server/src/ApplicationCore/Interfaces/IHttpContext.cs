using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    /// <summary>
    /// FezHttpContextのInterface
    /// </summary>
    public interface IHttpContext
    {
        Task SignInAsync(string schema, ClaimsPrincipal principal, AuthenticationProperties properties);
    }
}
