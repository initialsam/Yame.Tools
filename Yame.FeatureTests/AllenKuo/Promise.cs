using System;
using System.Collections.Generic;
using System.Text;

namespace Yame.FeatureTests.AllenKuo
{
    /// <summary>
    /// 格子樑 FB 分享的 FP - 將物件抽象化
    /// https://vimeo.com/431135966/400c8c4eb4
    /// </summary>
    public class Promise
    {
        public static Promise<TSource> CreateSuccess<TSource>(TSource source)
        {
            return new Promise<TSource>(source);
        }

        public static Promise<TSource> CreateError<TSource>(Exception error)
        {
            return new Promise<TSource>(new PromiseException(error));
        }

        public static Promise<TSource> DoTask<TSource>(Func<TSource> func)
        {
            try
            {
                TSource source = func.Invoke();
                return CreateSuccess(source);
            }
            catch (Exception ex)
            {
                return CreateError<TSource>(ex);
            }
        }
    }

    public class Promise<TSource>
    {
        private TSource source;
        private PromiseException error = null;


        public Promise(TSource source)
        {
            if (source == null) throw new ArgumentException(nameof(source));
            this.source = source;
        }

        public Promise(PromiseException error)
        {
            this.error = error ?? throw new ArgumentException(nameof(error));
        }
        private bool IsSuccess => source != null;
        private bool IsFail => source == null;

        public Promise<TSource> Then(Action<TSource> response)
        {
            if (IsFail) return this;

            response.Invoke(this.source);

            return this;
        }

        public Promise<TSource> Catch(Action<PromiseException> error)
        {
            if (IsSuccess) return this;

            error.Invoke(this.error);

            return this;
        }

        public Promise<TSource> Finally(Action action)
        {
            action.Invoke();
            return this;
        }
    }
    public class PromiseException
    {
        public string InnerMostMessage { get; private set; }
        public PromiseException(Exception ex)
        {
            this.InnerMostMessage = ex.GetBaseException().Message;
        }
      
      
      
    }
}
