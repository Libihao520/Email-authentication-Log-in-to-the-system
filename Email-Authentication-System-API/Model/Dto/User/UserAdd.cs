using System.ComponentModel.DataAnnotations;

namespace Model.Dto.User;

public class UserAdd
{
    /// <summary>
    /// 用户名
    /// </summary>
    [Required]
    public string? Username { get; set; }

    /// <summary>
    /// 密码
    /// </summary>
    [Required]
    public string? Password { get; set; }

    /// <summary>
    /// 确认密码
    /// </summary>
    [Required]
    public string? RePassword { get; set; }

    /// <summary>
    /// 邮箱
    /// </summary>
    [Required]
    public string? Email { get; set; }

    /// <summary>
    /// 验证码
    /// </summary>
    [Required]
    public string? Authcode { get; set; }
}