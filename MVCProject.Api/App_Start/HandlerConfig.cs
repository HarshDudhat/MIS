// -----------------------------------------------------------------------
// <copyright file="HandlerConfig.cs" company="ASK E-Sqaure">
// All copy rights reserved @ASK E-Sqaure.
// </copyright>
// -----------------------------------------------------------------------

namespace MVCProject.Api
{
    using System.Collections.ObjectModel;
    using System.Net.Http;

    /// <summary>
    /// Handler Configuration
    /// </summary>
    public static class HandlerConfig
    {
        /// <summary>
        /// Register Handlers
        /// </summary>
        /// <param name="handlers">collection of <see cref="DelegatingHandler"/> objects</param>
        public static void RegisterHandlers(Collection<DelegatingHandler> handlers)
        {
            handlers.Add(new HttpVerbsHandler());
        }
    }
}