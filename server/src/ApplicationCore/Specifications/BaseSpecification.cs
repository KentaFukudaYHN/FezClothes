using ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ApplicationCore.Specifications
{
    public abstract class BaseSpecification<T> : ISpecification<T>
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="criteria"></param>
        public BaseSpecification(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }

        /// <summary>
        /// 判定基準
        /// </summary>
        public Expression<Func<T, bool>> Criteria { get; }

        /// <summary>
        /// 結合情報
        /// </summary>
        public List<Expression<Func<T, object>>> Includes { get; } = new List<Expression<Func<T, object>>>();

        /// <summary>
        /// 結合情報
        /// </summary>
        public List<string> IncludeString { get; } = new List<string>();

        /// <summary>
        /// 昇順条件
        /// </summary>
        public Expression<Func<T, object>> OrderBy { get; private set; }

        /// <summary>
        /// 降順条件
        /// </summary>
        public Expression<Func<T, object>> OrderByDescending { get; private set; }

        /// <summary>
        /// Entityの結合
        /// </summary>
        /// <param name="includeString"></param>
        protected virtual void AddIncludes(string includeString)
        {
            IncludeString.Add(includeString);
        }

        /// <summary>
        /// 昇順設定
        /// </summary>
        /// <param name="orderByExpression"></param>
        protected virtual void ApplyOrderBy(Expression<Func<T, object>> orderByExpression)
        {
            OrderBy = orderByExpression;
        }

        /// <summary>
        /// 降順設定
        /// </summary>
        /// <param name="orderByDescExpression"></param>
        protected virtual void ApplyOrderByDescending(Expression<Func<T, object>> orderByDescExpression)
        {
            OrderByDescending = orderByDescExpression;
        }
    }
}
