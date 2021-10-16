using Polly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PollyNote
{
    class PolicyWrapDemo
    {

        public class Entry
        {
            public Guid Id { get; set; }
            public string Subject { get; set; }
        }
        static IEnumerable<Entry> Call3rdApi(string srcName, int delayTime)
        {
            Task.Delay(delayTime * 1000).Wait();
            return Enumerable.Range(1, 2).Select(o =>
                 new Entry
                 {
                     Id = Guid.NewGuid(),
                     Subject = $"Data from ExtraData[{srcName}] {DateTime.Now.Ticks % 100000:00000}"
                 });
        }
        static Dictionary<string, Func<IEnumerable<Entry>>> extDataJobs =
            new Dictionary<string, Func<IEnumerable<Entry>>>
            {
                ["SrcA"] = () => Call3rdApi("A", 3),
                ["SrcB"] = () => Call3rdApi("B", 8),
                ["SrcC"] = () => { throw new ApplicationException("Error"); }
            };

        public static void Test()
        {
            PreparePolicy();
            var data = QueryData();
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            Console.WriteLine(JsonSerializer.Serialize(data, options));
        }

        static Polly.Wrap.PolicyWrap<IEnumerable<Entry>> policy = null;
        static void PreparePolicy()
        {
            /*
            用 Timeout 限定 5 秒逾時。TimeoutStrategy 有分 Optimistic(樂觀) 跟 Pessimistic(悲觀) 兩種，
            前者會補捉 OperationCanceledException 跟 CancellationTokenSource.IsCancellationRequested 判定超時，
            後者則是時間到就直接報錯。範例程式用 TimeoutStrategy.Pessimistic 讓程式碼簡單一點，
            樂觀逾時的實作細節可參考開發人員不可缺少的重試處理利器 Polly by 余小章。
            */
            var timeoutPolicy =
                Policy.Timeout(TimeSpan.FromSeconds(5), Polly.Timeout.TimeoutStrategy.Pessimistic);
            var timeoutFallbackPolicy = Policy<IEnumerable<Entry>>
                .Handle<Polly.Timeout.TimeoutRejectedException>()
                .Fallback((context) =>
                    new List<Entry>() {
                                new Entry
                                {
                                    Id = Guid.NewGuid(),
                                    Subject = $"Warning: [{context["Src"]}] API timeout"
                                }
                    }, onFallback: (ex, ctx) => { });
            var fallbackPolicy = Policy<IEnumerable<Entry>>.Handle<Exception>()
                .Fallback((context) =>
                    new List<Entry>() {
                                new Entry
                                {
                                    Id = Guid.NewGuid(),
                                    Subject = $"Warning: [{context["Src"]}] API failed"
                                }
                    }, onFallback: (ex, ctx) => { });

            policy = fallbackPolicy.Wrap(timeoutFallbackPolicy).Wrap(timeoutPolicy);
        }

        static IEnumerable<Entry> QueryData()
        {
            var list = new List<Entry>();
            //database query simulation
            list.Add(new Entry
            {
                Id = Guid.NewGuid(),
                Subject = "Data from local service"
            });

            Parallel.ForEach(extDataJobs.Keys, (src) =>
            {
                var extData = policy.Execute((context) =>
                {
                    return extDataJobs[src]();
                }, contextData: new Dictionary<string, object>
                {
                    ["Src"] = src
                });
                lock (list)
                {
                    list.AddRange(extData);
                }
            });

            return list;
        }

    }
}