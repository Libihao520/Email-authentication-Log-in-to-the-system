using System.Text;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Model.Other;

namespace WebApi.Config;

public static class HostBuiderExtend
{
    public static void Register(this WebApplicationBuilder app)
    {
        app.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
        app.Host.ConfigureContainer<ContainerBuilder>(builder =>
        {

            //注册接口和实现层
            builder.RegisterModule(new AutofacModuleRegister());
        });
        //Automapper映射
        app.Services.AddAutoMapper(typeof(AutoMapperConfigs));
        //读取appsettings的JWTTokenOptions，注册JWT
        app.Services.Configure<JWTTokenOptions>(app.Configuration.GetSection("JWTTokenOptions"));

        #region JWT校验

        //第二步，增加鉴权逻辑
        JWTTokenOptions tokenOptions = new JWTTokenOptions();
        app.Configuration.Bind("JWTTokenOptions", tokenOptions);
        app.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme) //Scheme
            .AddJwtBearer(options => //这里是配置的鉴权的逻辑
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    //JWT有一些默认的属性，就是给鉴权时就可以筛选了
                    ValidateIssuer = true, //是否验证Issuer
                    ValidateAudience = true, //是否验证Audience
                    ValidateLifetime = true, //是否验证失效时间
                    ValidateIssuerSigningKey = true, //是否验证SecurityKey
                    ValidAudience = tokenOptions.Audience, //
                    ValidIssuer = tokenOptions.Issuer, //Issuer，这两项和前面签发jwt的设置一致
                    IssuerSigningKey =
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenOptions.SecurityKey)) //拿到SecurityKey 
                };
            });
        #endregion
        //添加跨域策略
        app.Services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy", opt => opt.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().WithExposedHeaders("X-Pagination"));
        });
    }
}