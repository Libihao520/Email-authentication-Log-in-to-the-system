using System.ComponentModel.DataAnnotations;

namespace Model.Dto.User;

public class UserReq
{
    /// <summary>
    /// 用户名
    /// </summary>
    [Required]
    public string username { get; set; }

    /// <summary>
    /// 密码
    /// </summary>
    [Required]
    public string Password { get; set; }
}