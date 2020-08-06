using System;
using System.Collections.Generic;
using System.Text;

namespace Yame.Tools.Extensions
{
    public static class ExceptionExtensions
    {
        public static IEnumerable<Exception> InnerExceptions(this Exception exception)
        {
            Exception currentEx = exception;
            while (currentEx.InnerException != null)
            {
                yield return exception.InnerException;
                exception = exception.InnerException;
            }
            yield break;
        }
    }
}
