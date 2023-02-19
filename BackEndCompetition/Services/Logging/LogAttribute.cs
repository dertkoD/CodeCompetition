using Microsoft.AspNetCore.Mvc.Filters;

namespace BackEndCompetition.Services.Logging
{
	public class LogAttribute : ActionFilterAttribute, IAsyncActionFilter
	{
		public override async Task OnActionExecutionAsync(ActionExecutingContext filterContext, ActionExecutionDelegate next)
		{
			var logger = filterContext.HttpContext.RequestServices.GetRequiredService<ILogger<LogAttribute>>();
			logger.LogInformation("Action Executing: " + filterContext.ActionDescriptor.DisplayName);
			try
			{
				var result = await next();

				if (result.Exception == null)
				{
					logger.LogInformation("Action Executed: " + result.ActionDescriptor.DisplayName);
				}
				else
				{
					// Log the error
					logger.LogError(result.Exception, "Error in action: " + result.ActionDescriptor.DisplayName);
				}
			}
			catch (Exception e)
			{
				logger.LogError(e, "Error in action: " + filterContext.ActionDescriptor.DisplayName);
				throw;
			}
		}
	}
}
