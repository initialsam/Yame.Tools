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
           


            var localVersion = new Version("2.30.0");
            var serverVersion = new Version("2.28.12");
            if(localVersion > serverVersion)
            {
                var a = 1;
            }

            var result1 = serverVersion.CompareTo(localVersion); //-1   serverVersion小於localVersion

            var result2= localVersion.CompareTo(serverVersion); //1     localVersion大於serverVersion

            var aa = new Version("2.1");
            var bb = new Version("2.1");
            if (aa == bb)
            {
                var a1 = 1;
            }

            var result3 = aa.CompareTo(bb); //0  等於          


            var aaaa = CheckAppVersion("2.30.0", "2.29.1"); //1
            var bbbb = CheckAppVersion("2.30.0", "2.30.0"); //0
            var cccc = CheckAppVersion("2.30.0", "2.30.1"); //-1


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
