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

namespace PollyNote
{
    public static class PolicyFallback
    {
        static int _count = 0;
        public static void Test()
        {
            var result = Policy
            // 故障處理 : 要 handle 什麼樣的異常
            .Handle<HttpRequestException>()
            //或是回傳的內容怎樣算失敗
            .OrResult<HttpResponseMessage>(result => result.StatusCode != HttpStatusCode.OK)
            .Fallback(
                 new HttpResponseMessage(HttpStatusCode.BadRequest)
            )

            // 要執行的任務
            .Execute(doMockHTTPRequest);

            Console.WriteLine($"結束退出 {result.StatusCode}");
        }

        public static void TestTimeoutRetryFallback()
        {
            var timeoutPolicy = Policy
                   .Timeout(
                       TimeSpan.FromSeconds(1),
                       TimeoutStrategy.Pessimistic,
                       onTimeout: (context, time, task, ex) =>
                       {
                           var errorMsg = $"錯誤訊息:{ex.Message}";
                           Console.WriteLine($"In Timeout 逾時時間:{time}\r\n錯誤:{errorMsg}");
                       });

            var retryPolicy = Policy
                  .Handle<HttpRequestException>()
                  .Or<TimeoutRejectedException>()
                  .OrResult<string>(result => result == "Fail")
                  .WaitAndRetry(
                       retryCount: 3,
                       sleepDurationProvider: (retryCount) =>
                       {
                           return TimeSpan.FromSeconds(Math.Pow(2, retryCount));
                       },
                       onRetry: (delegateResult, timeSpan, retryCount, context) =>
                       {
                           if (delegateResult?.Exception != null)
                           {
                               Console.WriteLine($"In WaitAndRetry Exception : 呼叫 API 異常, 等 {timeSpan} 秒, Exception :{delegateResult.Exception.Message}");
                           }
                           if (delegateResult?.Result != null)
                           {
                               Console.WriteLine($"In WaitAndRetry Result : 呼叫 API 異常, 進行第 {retryCount} 次重試, Result :{delegateResult.Result}");
                           }
                   });

            var fallbackPolicy = Policy
             .Handle<HttpRequestException>()
             .Or<TimeoutRejectedException>()
             .OrResult<string>(result => result == "Fail")
             .Fallback(
                  fallbackValue : new string("Polly Fallback"),
                  onFallback : (delegateResult, context) =>
                  {
                      if (delegateResult?.Exception != null)
                      {
                          Console.WriteLine($"In Fallback Exception : {delegateResult.Exception.Message}");
                      }
                      if (delegateResult?.Result != null)
                      {
                          Console.WriteLine($"In Fallback Result : :{delegateResult.Result}");
                      }
                  }
             );

            var result = fallbackPolicy.Execute(() =>
                retryPolicy.Execute(() =>
                    timeoutPolicy.Execute(() =>
                        doTimeOutHTTPRequest()
            )));

            Console.WriteLine($"結束退出 {result}");
        }

        static string doTimeOutHTTPRequest()
        {
            Console.WriteLine($"開始發送 Request");
            Thread.Sleep(TimeSpan.FromSeconds(5));
            return "Fail";
        }

        static HttpResponseMessage doMockHTTPRequest()
        {
            Console.WriteLine($"[App] {++_count}: 開始發送 Request");
            //throw new HttpRequestException("fake!!!!");
            HttpResponseMessage result;
            using (HttpClient client = new HttpClient())
            {
                result = client.GetAsync("http://www.mocky.io/v2/5cfb4d9b3000006e080a8b0a").Result;
            }

            return result;
        }
    }
}
