
using System;  
using System.Text;  
namespace Service.Utils;
  
public abstract class RandomIdGenerator  
{  
    private static Random random = new Random();  
    private const string Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";  
  
    public static string GenerateRandomId(int length)  
    {  
        if (length < 1 || length > 10)  
        {  
            throw new ArgumentOutOfRangeException(nameof(length), "Length must be between 1 and 10.");  
        }  
  
        StringBuilder sb = new StringBuilder();  
        for (int i = 0; i < length; i++)  
        {  
            int index = random.Next(Chars.Length);  
            char c = Chars[index];  
            sb.Append(c);  
        }  
        return sb.ToString();  
    }  
}  
