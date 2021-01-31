using ApplicationCore.Entitys;
using ApplicationCore.Interfaces;
using ApplicationCore.Services;
using Microsoft.AspNetCore.Authentication;
using Moq;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.ApplicationCore.Services
{
    /// <summary>
    /// ログインServiceテスト
    /// </summary>
    public class LoginServiceTest
    {
        [Fact]
        public async Task 登録されているアカウントはログインできる ()
        {
            var repository = new Mock<IAsyncRepository<Account>>();
            var loginService = new LoginService(repository.Object);
            var context = new Mock<IHttpContext>();

            //アカウント情報検索の結果設定
            IReadOnlyList<Account> result = new List<Account>()
            {
                new Account()
                {
                    Id = "0000",
                    Name = "太郎",
                    Password = "1111",
                    MailAddress = "test@test.coom"
                }
            };

            repository
                .Setup(x => x.ListAsync(It.IsAny<ISpecification<Account>>()))
                .Returns(Task.FromResult(result));

            //ログイン処理実行
            await loginService.Login("太郎", "0000", context.Object);

            //アカウント情報検索が呼ばれた事を確認
            repository.Verify(x => x.ListAsync(It.IsAny<ISpecification<Account>>()));

            //ログイン処理が呼ばれた事を確認
            context.Verify(x => x.SignInAsync(It.IsAny<string>(), It.IsAny<ClaimsPrincipal>(), It.IsAny<AuthenticationProperties>()));
        }

        [Fact]
        public async Task 登録されてないアカウントはログインできない()
        {
            var repository = new Mock<IAsyncRepository<Account>>();
            var loginService = new LoginService(repository.Object);
            var context = new Mock<IHttpContext>();

            //アカウント情報検索の結果設定
            IReadOnlyList<Account> result = new List<Account>();
            repository
                .Setup(x => x.ListAsync(It.IsAny<ISpecification<Account>>()))
                .Returns(Task.FromResult(result));

            //ログイン処理実行
            await loginService.Login("太郎", "0000", context.Object);

            //アカウント情報検索が呼ばれた事を確認
            repository.Verify(x => x.ListAsync(It.IsAny<ISpecification<Account>>()));

            //ログイン処理が呼ばれてないことを確認
            context.Verify(
                x => x.SignInAsync(
                    It.IsAny<string>(),
                    It.IsAny<ClaimsPrincipal>(),
                    It.IsAny<AuthenticationProperties>()
                ),
                Times.Never);
        }

        [Fact]
        public void 名前を空にしてログインするとArgumentExceptionがthrowされてログインできない()
        {
            var repository = new Mock<IAsyncRepository<Account>>();
            var loginService = new LoginService(repository.Object);
            var context = new Mock<IHttpContext>();

            //アカウント情報検索の結果設定
            IReadOnlyList<Account> result = new List<Account>();
            repository
                .Setup(x => x.ListAsync(It.IsAny<ISpecification<Account>>()))
                .Returns(Task.FromResult(result));

            //ログイン処理実行するとArgumentExeptionがthrowされるはず
            var ex = Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await loginService.Login("", "000", context.Object);
            });

            //アカウント情報検索が呼ばれてない事を確認
            repository.Verify(
                x => x.ListAsync(It.IsAny<ISpecification<Account>>()),
                Times.Never
                );

            //ログイン処理が呼ばれてない事を確認
            context.Verify(
                x => x.SignInAsync(
                    It.IsAny<string>(),
                    It.IsAny<ClaimsPrincipal>(),
                    It.IsAny<AuthenticationProperties>()
                ),
                Times.Never);
        }

        [Fact]
        public void パスワードを空にしてログインするとArgumentExceptionがthrowされてログインできない()
        {
            var repository = new Mock<IAsyncRepository<Account>>();
            var loginService = new LoginService(repository.Object);
            var context = new Mock<IHttpContext>();

            //アカウント情報検索の結果設定
            IReadOnlyList<Account> result = new List<Account>();
            repository
                .Setup(x => x.ListAsync(It.IsAny<ISpecification<Account>>()))
                .Returns(Task.FromResult(result));

            //ログイン処理実行するとArgumentExeptionがthrowされるはず
            var ex = Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await loginService.Login("太郎", "", context.Object);
            });

            //アカウント情報検索が呼ばれてない事を確認
            repository.Verify(
                x => x.ListAsync(It.IsAny<ISpecification<Account>>()),
                Times.Never
                );

            //ログイン処理が呼ばれてない事を確認
            context.Verify(
                x => x.SignInAsync(
                    It.IsAny<string>(),
                    It.IsAny<ClaimsPrincipal>(),
                    It.IsAny<AuthenticationProperties>()
                ),
                Times.Never);
        }
    }
}
