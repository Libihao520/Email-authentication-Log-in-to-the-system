using System.Text.RegularExpressions;
using AutoMapper;
using EFCoreMigrations;
using Interface;
using Microsoft.EntityFrameworkCore;
using Model.Consts;
using Model.Dto.User;
using Model.Entitys;
using Model.Other;
using Service.Utils;
using Service.Utils.RedisUtil;
using WebApi.Config;

namespace Service;

public class UserService : IUserService
{
    private readonly IMapper _mapper;

    private MyDbContext _context;

    public UserService(IMapper mapper, MyDbContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public UserRes GetUser(string userName, string passWord)
    {
        var users = _context.users.Where(u => u.Name == userName && u.Password == passWord).FirstOrDefault();
        if (users != null)
        {
            return _mapper.Map<UserRes>(users);
        }

        return new UserRes();
    }

    public async Task<ApiResult> add(UserAdd userAdd)
    {
        if (string.IsNullOrWhiteSpace(userAdd.Password) || string.IsNullOrWhiteSpace(userAdd.RePassword))
        {
            return ResultHelper.Error("密码为空!");
        }

        if (userAdd.Password != userAdd.RePassword)
        {
            return ResultHelper.Error("两次输入的密码不一致!");
        }

        if (string.IsNullOrWhiteSpace(userAdd.Email))
        {
            return ResultHelper.Error("邮箱为空！");
        }

        if (string.IsNullOrWhiteSpace(userAdd.Username))
        {
            return ResultHelper.Error("用户名为空！");
        }
        if (_context.users.Any(u => u.Name == userAdd.Username && u.IsDeleted == 0))
        {
            return ResultHelper.Error("用户名已被注册，请换一个！");
        }
        try
        {
            var password = AesUtilities.Decrypt(userAdd.Password);
            var decodeEmail = AesUtilities.Decrypt(userAdd.Email);

            if (!IsValidEmail(decodeEmail))
            {
                return ResultHelper.Error("请输入正确格式的邮箱!");
            }

            if (_context.users.Any(u => u.Email == decodeEmail && u.IsDeleted == 0))
            {
                return ResultHelper.Error("该邮箱已经注册过了！");
            }

            var s = CacheManager.Get<string>(string.Format(RedisKey.UserActiveCode, decodeEmail));
            if (string.IsNullOrWhiteSpace(s))
            {
                return ResultHelper.Error("验证码还未发送或已失效，请再发送一次！");
            }
            if(userAdd.Authcode != s)
            {
                return ResultHelper.Error("验证码错误！");
            }

            users user = new users()
            {
                Name = userAdd.Username,
                Password = userAdd.Password,
                CreateDate = DateTime.Now,
                CreateUserId = 0,
                IsDeleted = 0,
                Email = decodeEmail
            };
            _context.users.Add(user);
            _context.SaveChanges();
            return ResultHelper.Success("注册成功！", "验证码正确！已注册成功！  ");
        }
        catch (Exception e)
        {
            return ResultHelper.Error("注册失败！");
        }
    }

    /// <summary>
    /// 发送邮箱验证码
    /// </summary>
    /// <returns></returns>
    public async Task<ApiResult> SendVerificationCode(string email)
    {
        var decodeEmail = AesUtilities.Decrypt(email);
        if (!IsValidEmail(decodeEmail))
        {
            return ResultHelper.Error("请输入正确格式的邮箱!");
        }

        if (_context.users.Any(u => u.Email == decodeEmail && u.IsDeleted == 0))
        {
            return ResultHelper.Error("该邮箱已经注册过了！");
        }
        
        //查看缓存有没有这条key
        var exist = CacheManager.Exist(string.Format(RedisKey.UserActiveCode, decodeEmail));
        if (exist)
        {
            return ResultHelper.Error("该邮箱的上一条验证码还未失效,请查看您的邮箱继续激活！");
        }

        //将验证码写入缓存，并设置过期时间
        string randomId = RandomIdGenerator.GenerateRandomId(6);
        CacheManager.Set(string.Format(RedisKey.UserActiveCode, decodeEmail), randomId, TimeSpan.FromMinutes(30));

        // 发送邮箱
        EmailUtil.NetSendEmail($"欢迎注册通用管理系统,您的验证码是：{randomId},验证码有效期至-{DateTime.Now.AddMinutes(30)}", "通用管理系统注册",
            decodeEmail);

        return ResultHelper.Success("发送成功，尽快验证！", $"验证码已经发送到您的邮箱{decodeEmail}！有效期30分钟");
    }

    /// <summary>
    /// 验证邮箱格式工具
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    static bool IsValidEmail(string email)
    {
        string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
        return Regex.IsMatch(email, pattern);
    }
}