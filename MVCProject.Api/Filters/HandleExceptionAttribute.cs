// <copyright file="HandleExceptionAttribute.cs" company="ASK E-Sqaure">
// All copy rights reserved @ASK E-Sqaure.
// </copyright>
// -----------------------------------------------------------------------
namespace MVCProject.Api.Filters
{
    using System.Net;
    using System.Net.Http;
    using System.Web;
    using System.Web.Http.Filters;
    using System.Web.Mvc;
    using Elmah;
    using MVCProject.Api.Models;
    using MVCProject.Api.Utilities;
    using MVCProject.Common.Resources;

    /// <summary>
    /// Custom filter to handle exceptions.
    /// </summary>
    public sealed class HandleExceptionAttribute : System.Web.Http.Filters.ExceptionFilterAttribute
    {
        /// <summary>
        /// override OnException method
        /// </summary>
        /// <param name="actionExecutedContext">Http Action Executed Context</param>
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            base.OnException(actionExecutedContext);
            if (actionExecutedContext.Exception != null)
            {
                AppUtility.ElmahErrorLog(actionExecutedContext.Exception);
            }

            var exception = actionExecutedContext.Exception as HttpException;
            HttpStatusCode code = exception != null ? (HttpStatusCode)exception.GetHttpCode() : HttpStatusCode.InternalServerError;
            ApiResponse responseToSend = ApiHttpUtility.CreateResponse(true, MessageTypes.Error, Resource.AnErrorHasOccurred);
            actionExecutedContext.Response = actionExecutedContext.Request.CreateResponse(code, responseToSend);
        }

        /// <summary>
        /// Try to Raise Error Signal
        /// </summary>
        /// <param name="context">Exception Context</param>
        /// <returns>true or false</returns>
        private static bool TryRaiseErrorSignal(ExceptionContext context)
        {
            var httpContext = GetHttpContextImpl(context.HttpContext);
            if (httpContext == null)
            {
                return false;
            }

            var signal = ErrorSignal.FromContext(httpContext);
            if (signal == null)
            {
                return false;
            }

            signal.Raise(context.Exception, httpContext);
            return true;
        }

        /// <summary>
        /// Check Is Filtered or not
        /// </summary>
        /// <param name="context">Exception Context</param>
        /// <returns>true or false</returns>
        private static bool IsFiltered(ExceptionContext context)
        {
            var config = context.HttpContext.GetSection("elmah/errorFilter")
                            as ErrorFilterConfiguration;

            if (config == null)
            {
                return false;
            }

            var testContext = new ErrorFilterModule.AssertionHelperContext(
                                  context.Exception,
                                  GetHttpContextImpl(context.HttpContext));
            return config.Assertion.Test(testContext);
        }

        /// <summary>
        /// Log Exception
        /// </summary>
        /// <param name="context">Exception Context</param>
        private static void LogException(ExceptionContext context)
        {
            var httpContext = GetHttpContextImpl(context.HttpContext);
            var error = new Error(context.Exception, httpContext);
            ErrorLog.GetDefault(httpContext).Log(error);
        }

        /// <summary>
        /// Get Http Context
        /// </summary>
        /// <param name="context">Http Context Base</param>
        /// <returns>Http Context</returns>
        private static HttpContext GetHttpContextImpl(HttpContextBase context)
        {
            return context.ApplicationInstance.Context;
        }
    }
}