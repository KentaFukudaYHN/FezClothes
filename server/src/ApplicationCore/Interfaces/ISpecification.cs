using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ApplicationCore.Interfaces
{
    /// <summary>
    /// データ取得の仕様Interface
    /// </summary>
    public interface ISpecification<T>
    {
        /// <summary>
        /// 判定基準
        /// </summary>
        Expression<Func<T, bool>> Criteria { get; }

        /// <summary>
        /// 統合情報
        /// </summary>
        List<Expression<Func<T, object>>> Includes { get; }

        List<string> IncludeString { get; }

        /// <summary>
        /// 昇順条件
        /// </summary>
        Expression<Func<T, object>> OrderBy { get; }

        /// <summary>
        /// 降順条件
        /// </summary>
        Expression<Func<T, object>> OrderByDescending { get; }
    }
}
