using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var a = new List<string>() { "A-1", "B-1", "C-1", "A-2" };


            var b = new List<string>() { "A", "C"};

            var c = a.Where(x => b.Any(i=>x.StartsWith(i))).ToList();

        }


        public static int CheckAppVersion(string limitVersion, string appVersion)
        {
            //如果傳入的 AccountID or appVersion 不正確，則直接傳回 false
            if (string.IsNullOrEmpty(appVersion))
            {
                return -1;
            }
            //比較版本，若目前APP版本小於limitVersion則回傳true
            Version Version_limitVersion = new Version(limitVersion); //這支API想判斷的APP版本
            Version Version_appVersion = new Version(appVersion);     //目前用戶的APP版本

            //Version_limitVersion == Version_appVersion => 0 等於
            //Version_limitVersion >  Version_appVersion => 1 大於
            //Version_limitVersion <  Version_appVersion => -1 小於
            return Version_limitVersion.CompareTo(Version_appVersion);
        }
    }
}
