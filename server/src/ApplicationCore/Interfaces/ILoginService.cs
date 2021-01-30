using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    /// <summary>
    /// ログインサービス
    /// </summary>
    public interface ILoginService
    {
        Task<bool> Login(string name, string ps, HttpContext context);
    }
}
