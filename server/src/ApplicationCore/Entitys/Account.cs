using System;

namespace ApplicationCore.Entitys
{
    /// <summary>
    /// アカウント情報
    /// </summary>
    public class Account: BaseEntity
    {
        /// <summary>
        /// メールアドレス
        /// </summary>
        public string MailAddress { get; set; }

        /// <summary>
        /// パスワード
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 登録日時
        /// </summary>
        public DateTime RegistDateTime { get; set; }
    }
}
