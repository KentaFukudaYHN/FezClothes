using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ApplicationCore.Context
{
    /// <summary>
    /// HttpContext
    /// </summary>
    public class FezHttpContext : IHttpContext
    {
        private readonly HttpContext _context;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="context"></param>
        public FezHttpContext(HttpContext context)
        {
            _context = context;
        }

        /// <summary>
        /// ログイン処理
        /// </summary>
        /// <param name="schema"></param>
        /// <param name="principal"></param>
        /// <param name="properties"></param>
        /// <returns></returns>
        public Task SignInAsync(string schema, ClaimsPrincipal principal, AuthenticationProperties properties)
        {
            return _context.SignInAsync(schema, principal, properties);
        }
    }
}
