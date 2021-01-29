using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Entitys;
using ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    /// <summary>
    /// EntityFrameWorkRepositoryクラス
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class EfRepository<T> : IAsyncRepository<T> where T : BaseEntity
    {
        /// <summary>
        /// データコンテキスト
        /// </summary>
        private readonly FezClothesContext _db;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="db"></param>
        public EfRepository(FezClothesContext db)
        {
            _db = db;
        }

        /// <summary>
        /// IDで検索
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<T> GetByIdAsync(string id)
        {
            return await _db.Set<T>().SingleOrDefaultAsync(x => x.Id == id);
        }

        /// <summary>
        /// 条件で検索
        /// </summary>
        /// <param name="spec"></param>
        /// <returns></returns>
        public async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec)
        {
            return await this.ApplySpecification(spec).ToListAsync();
        }

        /// <summary>
        /// 追加
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<T> AddAsync(T entity)
        {
            if (string.IsNullOrEmpty(entity.Id))
            {
                entity.Id = Guid.NewGuid().ToString();
            }

            _db.Set<T>().Add(entity);
            await _db.SaveChangesAsync();

            return entity;
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task UpdateAsync(T entity)
        {
            _db.Entry(entity).State = EntityState.Modified;
            await _db.SaveChangesAsync();
        }

        /// <summary>
        /// 特定のカラムの更新
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="propertys"></param>
        /// <returns></returns>
        public async Task UpdateAsyncOnlyClumn(T entity, List<string> propertys)
        {
            var props = entity.GetType().GetProperties();
            var excludeFields = props.Where(x => !propertys.Contains(x.Name)).Select(x => x.Name).ToList();
            //await this.UpdateAsyncNotUpdateColumn(entity, excludeFields);
            await this.UpdateAsyncNotUpdateColumn(entity, excludeFields);

        }

        /// <summary>
        /// 特定のカラムの更新(指定したカラムを更新しない)
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="excludeFields">更新しないカラム名</param>
        /// <returns></returns>
        public async Task UpdateAsyncNotUpdateColumn(T entity, List<string> excludeFields)
        {
            var original = await GetByIdAsync(entity.Id);
            foreach(var originalProp in original.GetType().GetProperties())
            {
                if (!excludeFields.Contains(originalProp.Name))
                {
                    var targetProp = entity.GetType().GetProperty(originalProp.Name);
                    originalProp.SetValue(original, targetProp.GetValue(entity));
                }
            }

            await _db.SaveChangesAsync();
        }

        /// <summary>
        /// 削除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteByIdAsync(string id)
        {
            var target = await _db.Set<T>().SingleOrDefaultAsync(x => x.Id == id);
            _db.Set<T>().Remove(target);
            await _db.SaveChangesAsync();
        }


        /// <summary>
        /// 削除
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task DeleteAsync(T entity)
        {
            _db.Set<T>().Remove(entity);
            await _db.SaveChangesAsync();
        }

        /// <summary>
        /// レコードの総数をカウント
        /// </summary>
        /// <returns></returns>
        public async Task<int> CountAsync()
        {
            return await _db.Set<T>().CountAsync();
        }


        /// <summary>
        /// クエリの生成
        /// </summary>
        /// <param name="spec"></param>
        /// <returns></returns>
        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(_db.Set<T>().AsQueryable(), spec);
        }
    }
}
