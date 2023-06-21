// -----------------------------------------------------------------------
// <copyright file="CompressContentAttribute.cs" company="ASK-EHS">
// All copy rights reserved @ASK-EHS.
// </copyright>
// ----------------------------------------------------------------------- 

namespace MVCProject.Api
{
    using System;
    using System.Linq;
    using System.Web.Configuration;
    using System.Web.Http.Filters;
    using MVCProject.Api.Utilities;

    /// <summary>
    /// Attribute that can be added to controller methods to force content
    /// to be GZip encoded if the client supports it
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class CompressContentAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// run this method after PAI action executed
        /// </summary>
        /// <param name="context">Object of<see cref="HttpActionExecutedContext"/>class </param>
        public override void OnActionExecuted(HttpActionExecutedContext context)
        {
            if (Convert.ToBoolean(WebConfigurationManager.AppSettings["IsCompressContent"].ToString()))
            {
                if (context != null && context.Response != null && context.Response.RequestMessage != null && context.Response.RequestMessage.Headers != null && context.Response.RequestMessage.Headers.AcceptEncoding.Any())
                {
                    var acceptedEncoding = context.Response.RequestMessage.Headers.AcceptEncoding.First().Value;

                    if (!acceptedEncoding.Equals("gzip", StringComparison.InvariantCultureIgnoreCase)
                        && !acceptedEncoding.Equals("deflate", StringComparison.InvariantCultureIgnoreCase))
                    {
                        return;
                    }

                    context.Response.Content = new CompressedContent(context.Response.Content, acceptedEncoding);
                }
            }
        }
    }
}