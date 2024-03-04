using EFCoreMigrations;
using Microsoft.EntityFrameworkCore;
using WebApi.Config;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Register();

//注入MyDbcontext
builder.Services.AddDbContext<MyDbContext>(p =>
{
    p.UseSqlite(builder.Configuration.GetConnectionString("SQL"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

#region 鉴权授权
//通过 ASP.NET Core 中配置的授权认证，读取客户端中的身份标识(Cookie,Token等)并解析出来，存储到 context.User 中
app.UseAuthentication();
//判断当前访问 Endpoint (Controller或Action)是否使用了 [Authorize]以及配置角色或策略，然后校验 Cookie 或 Token 是否有效
app.UseAuthorization();

#endregion

//使用跨域策略
app.UseCors("CorsPolicy");

app.MapControllers();

app.Run();