using Model.Dto.User;
using Model.Other;

namespace Interface;

public interface IUserService
{
    /// <summary>
    /// 获取用户token
    /// </summary>
    /// <param name="userName"></param>
    /// <param name="passWord"></param>
    /// <returns></returns>
    UserRes GetUser(string userName, string passWord);
    /// <summary>
    /// 添加用户
    /// </summary>
    /// <param name="userAdd"></param>
    /// <returns></returns>
    Task<ApiResult>  add(UserAdd userAdd);

    /// <summary>
    /// 发送邮箱验证码
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    Task<ApiResult> SendVerificationCode(string email);
}