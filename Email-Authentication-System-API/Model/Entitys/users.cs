using System.ComponentModel.DataAnnotations;
using Model.Common;

namespace Model.Entitys;

public class users : IEntity
{
    /// <summary>
    /// 用户名
    /// </summary>
    [Required]
    public string Name { get; set; }

    /// <summary>
    /// 密码
    /// </summary>
    [Required]
    public string Password { get; set; }

    /// <summary>
    /// 邮箱
    /// </summary>
    [Required]
    public string Email { get; set; }
}