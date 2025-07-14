using CubosFinancialAPI.Infrastructure.Repository.Interface;
using System.Security.Cryptography;
using System.Text;

namespace CubosFinancialAPI.Infrastructure
{
    public class CriptografiaHelper
    {
        private readonly IConfiguration _Configuration;

        public CriptografiaHelper(IConfiguration configuration)
        {
            _Configuration = configuration;
        }

        public string ENCODE_HMAC_SHA256_base64(string text)
        {
            try
            {
                string key = _Configuration["Jwt:Secret"]!;
                ASCIIEncoding encoding = new ASCIIEncoding();

                byte[] textBytes = encoding.GetBytes(text);
                byte[] keyBytes = encoding.GetBytes(key);

                byte[] hashBytes;

                using (HMACSHA256 hash = new HMACSHA256(keyBytes))
                    hashBytes = hash.ComputeHash(textBytes);

                return Convert.ToBase64String(hashBytes);

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
