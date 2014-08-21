using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Rebilly
{
    public class RebillySignature
    {
        public const int NONCE_LENGTH = 30;

        /// <summary>
        /// Generate signature
        /// </summary>
        /// <param name="apiUser"></param>
        /// <param name="apiKey"></param>
        /// <returns></returns>
        public string generateSignature(string apiUser, string apiKey)
        {
            string nonce = generateNonce(NONCE_LENGTH);
            DateTime time = DateTime.UtcNow;
            string data = apiUser + nonce + time;

            Encoding enc = Encoding.UTF8;
            HMACSHA1 hmac = new HMACSHA1(enc.GetBytes(apiKey));
            hmac.Initialize();

            byte[] buffer = enc.GetBytes(data);
            string signature = BitConverter.ToString(hmac.ComputeHash(buffer)).Replace("-", "").ToLower();

            JObject obj = new JObject(
                new JProperty("REB-APIUSER", apiUser),
                new JProperty("REB-NONCE", nonce),
                new JProperty("REB-TIMESTAMP", time.ToString()),
                new JProperty("REB-SIGNATURE", signature)
            );

            return Convert.ToBase64String(enc.GetBytes(obj.ToString()));
        }
        
        /// <summary>
        /// Generate random string
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        private string generateNonce(int length)
        {
            RandomNumberGenerator random = new RNGCryptoServiceProvider();
            byte[] tokenData = new byte[length];
            random.GetBytes(tokenData);

            return Convert.ToBase64String(tokenData).Substring(0, length);
        }
    }
}
