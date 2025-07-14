using System.Security.Cryptography;
using System.Text;

namespace CubosFinancialAPI.Infrastructure;

public class CriptografiaHelper(IConfiguration configuration)
{
    private readonly IConfiguration _Configuration = configuration;

    public string ENCODE_HMAC_SHA256_base64(string text)
    {
        try
        {
            string key = _Configuration["Hmac:Secret"]!;
            ASCIIEncoding encoding = new();

            byte[] textBytes = encoding.GetBytes(text);
            byte[] keyBytes = encoding.GetBytes(key);

            byte[] hashBytes;

            using (HMACSHA256 hash = new(keyBytes))
                hashBytes = hash.ComputeHash(textBytes);

            return Convert.ToBase64String(hashBytes);

        }
        catch (Exception)
        {
            throw;
        }
    }
}
