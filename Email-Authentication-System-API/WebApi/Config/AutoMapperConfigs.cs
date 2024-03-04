using AutoMapper;
using Model.Dto.User;
using Model.Entitys;

namespace WebApi.Config;

public class AutoMapperConfigs : Profile
{
    public AutoMapperConfigs()
    {
        //左往右
        //用户
        
        CreateMap<users, UserRes>();
        
    }
}