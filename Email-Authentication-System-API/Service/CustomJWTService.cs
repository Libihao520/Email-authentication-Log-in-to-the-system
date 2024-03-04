using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Interface;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Model.Dto.User;
using Model.Other;

namespace Service;

public class CustomJWTService:ICustomJWTService
{
    private readonly JWTTokenOptions _jwtTokenOptions;


    public CustomJWTService(IOptionsMonitor<JWTTokenOptions> jwtTokenOptions)
    {
        _jwtTokenOptions = jwtTokenOptions.CurrentValue;
    }

    public string GetToken(UserRes user)
    {
        #region 有效载荷，想写多少写多少，但尽量避免敏感信息
        var claims = new[]
        {
            new Claim("Id",user.Id.ToString()),
            new Claim("Name",user.Name),
        };

        //需要加密：需要加密key:
        //Nuget引入：Microsoft.IdentityModel.Tokens
        SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtTokenOptions.SecurityKey));

        SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        //Nuget引入：System.IdentityModel.Tokens.Jwt
        JwtSecurityToken token = new JwtSecurityToken(
            issuer: _jwtTokenOptions.Issuer,
            audience: _jwtTokenOptions.Audience,
            claims: claims,
            expires: DateTime.Now.AddMinutes(10),//10分钟有效期
            signingCredentials: creds);

        string returnToken = new JwtSecurityTokenHandler().WriteToken(token);
        return returnToken;
        #endregion
    }
}