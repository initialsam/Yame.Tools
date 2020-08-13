using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.ExceptionHandling;

namespace WebApi2.Infrastructure.GlobalException
{
	public class GlobalExceptionLogger : ExceptionLogger
	{
		private static NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();

		public override void Log(ExceptionLoggerContext context)
		{
			
			_logger.Info(context.Exception);
			_logger.Error(context.Exception);
		}
	}
}