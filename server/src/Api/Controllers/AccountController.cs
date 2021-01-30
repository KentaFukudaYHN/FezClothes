using Api.Interfaces;
using Api.ViewReqRes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Controllers
{
    /// <summary>
    /// アカウント管理コントローラー
    /// </summary>
    public class AccountController : Controller
    {
        private ILoginViewService _loginViewService;
        
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="loginViewService"></param>
        public AccountController(ILoginViewService loginViewService)
        {
            _loginViewService = loginViewService;
        }

        /// <summary>
        /// ログイン処理
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public async Task<IActionResult> Login([FromBody] LoginViewReq req)
        {
            if(ModelState.IsValid && await _loginViewService.Login(req, HttpContext))
            {
                return Ok();
            }

            return BadRequest();
        }
    }
}
