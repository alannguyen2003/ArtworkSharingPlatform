using System.Security.Cryptography;
using System.Text;

namespace ArtworkSharingPlatform.Repository.Repository.Utils;

public static class HashUtilization
{
    public static string HmacSha512(string key, string data)
    {
        try
        {
            if (string.IsNullOrEmpty(key) || string.IsNullOrEmpty(data))
            {
                throw new ArgumentNullException();
            }
    
            using (var hmac512 = new HMACSHA512(Encoding.UTF8.GetBytes(key)))
            {
                byte[] dataBytes = Encoding.UTF8.GetBytes(data);
                byte[] result = hmac512.ComputeHash(dataBytes);
                return BitConverter.ToString(result).Replace("-", "").ToLower();
            }
        }
        catch (Exception ex)
        {
            return string.Empty;
        }
    }   
    
    public static string HmacSha512_2(string key, string data)
    {
        try
        {
            if (key == null || data == null)
            {
                throw new ArgumentNullException();
            }

            using (HMACSHA512 hmac512 = new HMACSHA512(Encoding.UTF8.GetBytes(key)))
            {
                byte[] dataBytes = Encoding.UTF8.GetBytes(data);
                byte[] hashBytes = hmac512.ComputeHash(dataBytes);

                StringBuilder sb = new StringBuilder(hashBytes.Length * 2);
                foreach (byte b in hashBytes)
                {
                    sb.Append(b.ToString("x2"));
                }
                return sb.ToString();
            }
        }
        catch (Exception ex)
        {
            return "";
        }
    }

}