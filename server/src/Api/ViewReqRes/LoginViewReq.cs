using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Api.ViewReqRes
{
    /// <summary>
    /// ログイン要求クラス
    /// </summary>
    public class LoginViewReq
    {
        /// <summary>
        /// 名前
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// メールアドレス
        /// </summary>
        [Required]
        public string Password { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LoginViewReq() { }
    }
}
