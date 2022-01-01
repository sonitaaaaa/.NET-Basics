using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BlogsDEMO.Application.Helpers
{
    public class HashKeyHelper
    {
        public static string EnCodeSha1(string pass)
        {
            var sha1 = new SHA1CryptoServiceProvider();

            var bs = sha1.ComputeHash(Encoding.UTF8.GetBytes(pass));

            var s = new StringBuilder();

            foreach (var b in bs)
            {
                s.Append(b.ToString("x1").ToLower());
            }

            return s.ToString();
        }

        public static string EncodeUrl(string url)
        {
            try
            {
                var Uri = new Uri(url);
                return Uri.AbsoluteUri;
            }
            catch
            {
                return "";
            }
        }
    }
}
