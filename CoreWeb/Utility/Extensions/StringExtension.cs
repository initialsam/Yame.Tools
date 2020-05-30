using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreWeb.Utility.Extensions
{
    public static class StringExtension
    {
        public static string ToSha1(this String input)
        {
            var sha1 = System.Security.Cryptography.SHA1.Create();
            var crypto = sha1.ComputeHash(Encoding.Default.GetBytes(input));
            string sha1String = Convert.ToBase64String(crypto);

            return sha1String;
        }
    }
}
