using Polly;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PollyNote
{
    public static class PolicyRetry
    {
        static int _count = 0;
        public static void Test()
        {
            Policy
           // 故障處理 : 要 handle 什麼樣的異常
           .Handle<HttpRequestException>()
           //或是回傳的內容怎樣算失敗
           .OrResult<HttpResponseMessage>(result => result.StatusCode != HttpStatusCode.OK)
           .WaitAndRetry(
                retryCount : 3,
                sleepDurationProvider : (retryCount) => 
                {
                    return TimeSpan.FromMilliseconds(Math.Pow(2, retryCount));
                },
                onRetry : (exception, timeSpan, retryCount, context) =>
                {
                    if(exception?.Exception != null)
                    {
                        Console.WriteLine($"[App|Polly] : 呼叫 API 異常, 等 {timeSpan} 秒, Exception :{exception.Exception}");
                    }
                    if (exception?.Result != null)
                    {
                        Console.WriteLine($"[App|Polly] : 呼叫 API 異常, 進行第 {retryCount} 次重試, Error :{exception.Result.StatusCode}");
                    }
                    
                }
           )
           //// 重試策略 : 異常發生時要進行的重試次數及重試機制
           //.Retry(3, onRetry: (exception, retryCount) =>
           //{
           //    var a = exception.Exception;
           //    var b = exception.Result;

           //    Console.WriteLine($"[App|Polly] : 呼叫 API 異常, 進行第 {retryCount} 次重試, Error :{exception.Result.StatusCode}");
           //})
           // 要執行的任務
           .Execute(doMockHTTPRequest);

            Console.WriteLine("結束退出");
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
