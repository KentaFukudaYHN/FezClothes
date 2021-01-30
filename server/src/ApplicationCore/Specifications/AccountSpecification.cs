using ApplicationCore.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Specifications
{
    /// <summary>
    /// Account検索条件設定クラス
    /// </summary>
    public class AccountSpecification : BaseSpecification<Account>
    {
        /// <summary>
        /// 名前とパスワードで検索
        /// </summary>
        /// <param name="name"></param>
        /// <param name="ps"></param>
        public AccountSpecification(string name, string ps)
            : base(a => a.Name == name && a.Password == ps) { }
    }
}
