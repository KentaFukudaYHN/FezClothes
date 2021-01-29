using ApplicationCore.Entitys;
using ApplicationCore.Interfaces;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    /// <summary>
    /// Entityの抽出条件設定クラス
    /// </summary>
    public class SpecificationEvaluator<T> where T : BaseEntity
    {
        /// <summary>
        /// 抽出クエリの取得
        /// </summary>
        /// <param name="inputQuery"></param>
        /// <param name="specification"></param>
        /// <returns></returns>
        public static IQueryable<T> GetQuery(IQueryable<T> inputQuery, ISpecification<T> specification)
        {
            var query = inputQuery;

            //条件式が設定されていたら、IQueryableを書き換える
            if (specification.Criteria != null)
                query = query.Where(specification.Criteria);

            //結合
            query = specification.Includes.Aggregate(query, (current, include) => current.Include(include));

            //OrderByが設定されていれば適用
            if(specification.OrderBy != null)
            {
                query = query.OrderBy(specification.OrderBy);
            }
            else if(specification.OrderByDescending != null)
            {
                query = query.OrderByDescending(specification.OrderByDescending);
            }

            return query;
        }
    }
}
