using Api.Interfaces;
using Api.ViewReqRes;
using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Api.ViewServices
{
    /// <summary>
    /// ログイン管理ViewService
    /// </summary>
    public class LoginViewService : ILoginViewService
    {
        private readonly ILoginService _loginService;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LoginViewService(ILoginService loginService)
        {
            _loginService = loginService;
        }

        /// <summary>
        /// ログイン処理
        /// </summary>
        /// <param name="name"></param>
        /// <param name="ps"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public Task<bool> Login(LoginViewReq req, HttpContext context)
        {
            return _loginService.Login(req.Name, req.Password, context);
        }
    }
}
