using ApplicationCore.Entitys;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    /// <summary>
    /// DbContext
    /// </summary>
    public class FezClothesContext : DbContext
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="options"></param>
        public FezClothesContext(DbContextOptions options) : base(options)
        {
        }

        /// <summary>
        /// アカウント情報
        /// </summary>
        public DbSet<Account> Accounts { get; set; }
    }
}
