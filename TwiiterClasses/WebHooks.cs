using System;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace TwiiterClasses
{
    public class WebHooks
    {
        public string CRC_Challenge(HttpRequest request, string consumerSecret)
        {
            if (request.Headers.ContainsKey("crc_token"))
            {
                return Response_Challenge(request.Headers["crc_token"], consumerSecret);
            }
            return string.Empty;
        }

        private string Response_Challenge(string crc_token, string consumerSecret)
        {
            var encoding = new ASCIIEncoding();
            byte[] key = encoding.GetBytes(consumerSecret);
            byte[] message = encoding.GetBytes(crc_token);

            using (var hmac = new HMACSHA256(key))
            {
                byte[] hash = hmac.ComputeHash(message);
                return Convert.ToBase64String(hash);
            }
        }
    }
}
