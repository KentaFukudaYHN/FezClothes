using Api.ViewReqRes;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Interfaces
{
    /// <summary>
    /// ログイン管理Interface
    /// </summary>
    public interface ILoginViewService
    {
        /// <summary>
        /// ログイン処理
        /// </summary>
        /// <param name="name"></param>
        /// <param name="ps"></param>
        /// <returns></returns>
        Task<bool> Login(LoginViewReq req, HttpContext context);
    }
}
