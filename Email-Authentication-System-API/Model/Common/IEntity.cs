using System.ComponentModel.DataAnnotations;

namespace Model.Common;

public class IEntity : IBase
{
    /// <summary>
    /// 创建人Id
    /// </summary>
    [Required]
    public long CreateUserId { get; set; }

    /// <summary>
    /// 创建日期
    /// </summary>
    [Required]
    public DateTime CreateDate { get; set; }

    /// <summary>
    /// 是否删除
    /// </summary>
    [Required]
    public int IsDeleted { get; set; }
}