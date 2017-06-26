using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JWT;
using Newtonsoft.Json;

namespace UM_LOGISTIC_V1.Security
{
    public static class TokenService
    {
        public static string GenerateToken(string user, long role)
        {
            byte[] secretKey = JWT.JsonWebToken.Base64UrlDecode(user);
            var issued = DateTime.Now;
            var expire = DateTime.Now.AddHours(24);
            var payload = new Dictionary<string, object>()
            {
              {"user", user},
              {"iat", ToUnixTime(issued).ToString()},
              {"exp", ToUnixTime(expire).ToString()},
              {"role", (role - 1).ToString()}
            };
            string token = JWT.JsonWebToken.Encode(payload, secretKey, JWT.JwtHashAlgorithm.HS256);
            return token;
        }

        public static bool ValidateToken(string user, string token)
        {
            byte[] secretKey;
            var validTokenString = String.Empty;
            try
            {
                secretKey = JWT.JsonWebToken.Base64UrlDecode(user);
                validTokenString = JWT.JsonWebToken.Decode(token, secretKey);
            }
            catch(Exception)
            {
                return false;
            }
            var validObject = JsonConvert.DeserializeObject<ValidClass>(validTokenString);
            return validObject.user == user;
        }

        public static int GetRole(string user, string token)
        {
            byte[] secretKey;
            var validTokenString = String.Empty;
            try
            {
                secretKey = JWT.JsonWebToken.Base64UrlDecode(user);
                validTokenString = JWT.JsonWebToken.Decode(token, secretKey);
            }
            catch (Exception)
            {
                return 0;
            }
            var validObject = JsonConvert.DeserializeObject<ValidClass>(validTokenString);
            return validObject.role;
        }

        public static long ToUnixTime(DateTime dateTime)
        {
            return (int)(dateTime.ToUniversalTime().Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
        }

        public static DateTime ToDateTime(double unixTimeStamp)
        {
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }

        public class ValidClass
        {
            public string user { get; set; }
            public string iat { get; set; }
            public string exp { get; set; }
            public int role { get; set; }
        }
    }
}