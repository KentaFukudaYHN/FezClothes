using ApplicationCore.Entitys;
using ApplicationCore.Interfaces;
using ApplicationCore.Specifications;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    /// <summary>
    /// ログインサービス
    /// </summary>
    public class LoginService : ILoginService
    {
        private readonly IAsyncRepository<Account> _accountRepository;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="accountRepository"></param>
        public LoginService(IAsyncRepository<Account> accountRepository)
        {
            _accountRepository = accountRepository;
        }

        /// <summary>
        /// ログイン処理
        /// </summary>
        /// <param name="id"></param>
        /// <param name="ps"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task<bool> Login(string name, string ps, HttpContext context)
        {
            //アカウント情報を検索
            var accountSpec = new AccountSpecification(name, ps);
            var account = (await _accountRepository.ListAsync(accountSpec)).FirstOrDefault();

            if (account == null)
                return false;

            //クレームの生成
            var claims = new List<Claim>()
            {
                new Claim(nameof(Account.Name), account.Name),
                new Claim(nameof(Account.MailAddress), account.MailAddress),
                new Claim(nameof(Account.Id), account.Id)
            };

            var claimIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new Microsoft.AspNetCore.Authentication.AuthenticationProperties
            {
                //認証セッションの更新許可
                AllowRefresh = true,
                //認証セッションの有効期限を1日に設定
                ExpiresUtc = DateTimeOffset.Now.AddDays(1),
                //認証セッションの要求間の永続化
                IsPersistent = true
            };

            await context.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimIdentity),
                authProperties
                );

            return true;
        }
    }
}
