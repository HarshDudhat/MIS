// -----------------------------------------------------------------------
// <copyright file="HttpVerbsHandler.cs" company="ASK E-Sqaure">
// All copy rights reserved @ASK E-Sqaure.
// </copyright>
// -----------------------------------------------------------------------
namespace MVCProject.Api
{
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Http Verbs Handler for AccessControl
    /// </summary>
    public class HttpVerbsHandler : DelegatingHandler
    {
        // Initialization

        /// <summary>
        /// Request Origin
        /// </summary>
        private const string Origin = "Origin";

        /// <summary>
        /// Access Control Request Method
        /// </summary>
        private const string AccessControlRequestMethod = "Access-Control-Request-Method";

        /// <summary>
        /// Access Control Request Headers
        /// </summary>
        private const string AccessControlRequestHeaders = "Access-Control-Request-Headers";

        /// <summary>
        /// Access Control Allow Origin
        /// </summary>
        private const string AccessControlAllowOrigin = "Access-Control-Allow-Origin";

        /// <summary>
        /// Access Control Allow Methods
        /// </summary>
        private const string AccessControlAllowMethods = "Access-Control-Allow-Methods";

        /// <summary>
        /// Access Control Allow Headers
        /// </summary>
        private const string AccessControlAllowHeaders = "Access-Control-Allow-Headers";

        /// <summary>
        /// override SendAsync method
        /// </summary>
        /// <param name="request">Http Request Message</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>Http Response Message</returns>
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return request.Headers.Contains(Origin) ?
                this.ProcessCorsRequest(request, ref cancellationToken) :
                base.SendAsync(request, cancellationToken);
        }

        /// <summary>
        /// Add CORS ResponseHeaders
        /// </summary>
        /// <param name="request">Http Request Message</param>
        /// <param name="response">Http Response Message</param>
        private static void AddCorsResponseHeaders(HttpRequestMessage request, HttpResponseMessage response)
        {
            response.Headers.Add(AccessControlAllowOrigin, request.Headers.GetValues(Origin).First());

            string accessControlRequestMethod = request.Headers.GetValues(AccessControlRequestMethod).FirstOrDefault();
            if (accessControlRequestMethod != null)
            {
                response.Headers.Add(AccessControlAllowMethods, accessControlRequestMethod);
            }

            string requestedHeaders = string.Empty;
            if (request.Headers.Contains(AccessControlRequestHeaders))
            {
                requestedHeaders = string.Join(", ", request.Headers.GetValues(AccessControlRequestHeaders));
            }

            if (!string.IsNullOrEmpty(requestedHeaders))
            {
                response.Headers.Add(AccessControlAllowHeaders, requestedHeaders);
            }
        }

        /// <summary>
        /// Process CORS Request
        /// </summary>
        /// <param name="request">Http Request Message</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>Http Response Message </returns>
        private Task<HttpResponseMessage> ProcessCorsRequest(HttpRequestMessage request, ref CancellationToken cancellationToken)
        {
            if (request.Method == HttpMethod.Options)
            {
                return Task.Factory.StartNew<HttpResponseMessage>(
                () =>
                {
                    HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                    AddCorsResponseHeaders(request, response);
                    return response;
                },
                cancellationToken);
            }
            else
            {
                return base.SendAsync(request, cancellationToken).ContinueWith<HttpResponseMessage>(task =>
                {
                    HttpResponseMessage resp = task.Result;
                    resp.Headers.Add(AccessControlAllowOrigin, request.Headers.GetValues(Origin).First());
                    return resp;
                });
            }
        }
    }
}