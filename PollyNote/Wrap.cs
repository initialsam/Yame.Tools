using Polly;
using Polly.Fallback;
using Polly.Retry;
using Polly.Timeout;
using Polly.Wrap;
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
    public class Wrap
    {
        int _count = 0;
        private readonly FallbackPolicy<string> _fallbackPolicy;
        private readonly TimeoutPolicy<string> _timeoutPolicy;
        private readonly RetryPolicy<string> _retryPolicy;

        public Wrap()
        {
            _timeoutPolicy = Policy
                   .Timeout<string>(
                       TimeSpan.FromMilliseconds(1),
                       TimeoutStrategy.Pessimistic,
                       onTimeout: (context, time, task, ex) =>
                       {
                           var errorMsg = $"錯誤訊息:{ex.Message}";
                           Console.WriteLine($"In Timeout 逾時時間:{time}\r\n錯誤:{errorMsg}");
                       });
            var policyBuilder = Policy
                  .Handle<HttpRequestException>()
                  .Or<TimeoutRejectedException>()
                  .OrResult<string>(result => result == "Fail");

            _retryPolicy = policyBuilder
                  .WaitAndRetry(
                       retryCount: 3,
                       sleepDurationProvider: (retryCount) =>
                       {
                           return TimeSpan.FromMilliseconds(Math.Pow(2, retryCount));
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

            _fallbackPolicy = policyBuilder
             .Fallback(
                  fallbackValue: new string("Polly Fallback"),
                  onFallback: (delegateResult, context) =>
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

        }

        public void Test()
        {
            var policyWrap = Policy.Wrap<string>(_fallbackPolicy, _retryPolicy, _timeoutPolicy);

            policyWrap.Execute(doTimeOutHTTPRequest);

        }

        string doTimeOutHTTPRequest()
        {
            Console.WriteLine($"開始發送 {++_count} Request");
            Thread.Sleep(TimeSpan.FromSeconds(5));
            return "Fail";
        }

        HttpResponseMessage doMockHTTPRequest()
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
