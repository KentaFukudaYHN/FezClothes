using Api.ViewReqRes;
using Api.ViewServices;
using ApplicationCore.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.Api.ViewServices
{
    /// <summary>
    /// ログインViewServiceテスト
    /// </summary>
    public class LoginViewServiceTest
    {
        [Fact]
        public async Task ログインServiceのLoginメソッドに引数の名前とパスワードをパラメータで渡して実行している()
        {
            var loginService = new Mock<ILoginService>();
            var context = new Mock<IHttpContext>();
            var loginViewService = new LoginViewService(loginService.Object);

            var req = new LoginViewReq()
            {
                Name = "太郎",
                Password = "0000"
            };

            //ログイン処理実行
            await loginViewService.Login(req, context.Object);

            //ログインServiceのログインメソッドが実行されたか確認
            loginService.Verify(
                x => x.Login(
                    It.Is<string>(x => x == req.Name),
                    It.Is<string>(x => x == req.Password), 
                    It.Is<IHttpContext>(x => x == context.Object)
                )
            );
        }
    }
}
