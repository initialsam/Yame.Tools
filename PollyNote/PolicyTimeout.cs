using Polly;
using Polly.Timeout;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace PollyNote
{
    public static class PolicyTimeout
    {
        public static void Test()
        {
            var timeoutPolicy  = Policy
            .Timeout(
                TimeSpan.FromMilliseconds(1),
                TimeoutStrategy.Pessimistic,
                onTimeout: (context, time, task, ex) =>
            {
                var errorMsg = $"錯誤訊息:{ex.Message} 錯誤目標:{ex.TargetSite}";
                Console.WriteLine($"逾時時間:{time}\r\n錯誤:{errorMsg}");
            })
            .Execute(doTimeOutHTTPRequest);

            Console.WriteLine("結束退出");
        }

        public static void TestTimeoutRetry()
        {
            var timeoutPolicy = Policy
                   .Timeout(
                       TimeSpan.FromMilliseconds(1),
                       TimeoutStrategy.Pessimistic,
                       onTimeout: (context, time, task, ex) =>
                       {
                           var errorMsg = $"錯誤訊息:{ex.Message}";
                           Console.WriteLine($"逾時時間:{time}\r\n錯誤:{errorMsg}");
                       });

            var retryPolicy = Policy
          // 故障處理 : 要 handle 什麼樣的異常
          .Handle<HttpRequestException>()
          .Or<TimeoutRejectedException>()
          //或是回傳的內容怎樣算失敗
          .OrResult<string>(result => result == "Fail")
          .WaitAndRetry(
               retryCount: 3,
               sleepDurationProvider: (retryCount) =>
               {
                   return TimeSpan.FromMilliseconds(Math.Pow(2, retryCount));
               },
               onRetry: (exception, timeSpan, retryCount, context) =>
               {
                   if (exception?.Exception != null)
                   {
                       Console.WriteLine($"[App|Polly] : 呼叫 API 異常, 等 {timeSpan} 秒, Exception :{exception.Exception.Message}");
                   }
                   if (exception?.Result != null)
                   {
                       Console.WriteLine($"[App|Polly] : 呼叫 API 異常, 進行第 {retryCount} 次重試, Result :{exception.Result}");
                   }

               });
            retryPolicy.Execute(() =>
                timeoutPolicy.Execute(()=>
                    doTimeOutHTTPRequest()
                ));
            
        }

        static string doTimeOutHTTPRequest()
        {
            Console.WriteLine($"開始發送 Request");
            //Thread.Sleep(TimeSpan.FromSeconds(5));
            return "Fail";
            
        }
    }
}
