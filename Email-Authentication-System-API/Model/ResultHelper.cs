using Model.Other;

namespace WebApi.Config;

public class ResultHelper
{
    public static ApiResult Success(string message,object res)
    {
        return new ApiResult() { code = 0,message = message, data = res };
    }

    public static ApiResult Error(string message)
    {
        return new ApiResult() { code = 1, message = message };
    }
}