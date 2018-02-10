namespace WebApplication.Models.Utils
{
    using System;
    using System.Security.Cryptography;
    using System.Text;
    public static class  HashPassword
    {
       public static Guid ConvertToMd5HashGUID(string value)
        {
            if (value == null)
                value = string.Empty;

            var bytes = Encoding.Default.GetBytes(value);

            MD5 md5Hasher = MD5.Create();
            byte[] data = md5Hasher.ComputeHash(bytes);

            return new Guid(data);
        }
    }
}