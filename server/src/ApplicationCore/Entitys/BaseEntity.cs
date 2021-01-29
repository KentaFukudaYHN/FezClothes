using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Entitys
{
    /// <summary>
    /// EntityのBaseクラス
    /// </summary>
    public abstract class BaseEntity
    {
        [Key]
        public virtual string Id { get; set; }
    }
}
