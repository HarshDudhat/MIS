// -----------------------------------------------------------------------
// <copyright file="AuthenticationHandler.cs" company="ASK-EHS">
// All copy rights reserved @ASK-EHS.
// </copyright>
// ----------------------------------------------------------------------- 
namespace MVCProject.Api.Handlers
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web;
    using MVCProject.Api.Utilities;
    using MVCProject.Common.Resources;

    /// <summary>
    /// Authentication Handler for web API
    /// </summary>
    public class AuthenticationHandler : DelegatingHandler
    {
        /// <summary>
        /// Holds request's header name which will contains token.
        /// </summary>
        private const string SecurityToken = "__RequestAuthToken";

        /// <summary>
        /// Default overridden method which performs authentication.
        /// </summary>
        /// <param name="request">Http request message.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Returns http response message of type <see cref="HttpResponseMessage"/> class asynchronously.</returns>
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (request.Headers.Contains(SecurityToken))
            {
                // Authorize securityToken
                bool authorized = this.Authorize(request);
                if (!authorized)
                {
                    // Not Authorized
                    return ApiHttpUtility.FromResult(request, false, HttpStatusCode.Unauthorized, MessageTypes.Error, Resource.TokenInvalid);
                }
            }
            else
            {
                // SecurityToken not exists
                return ApiHttpUtility.FromResult(request, false, HttpStatusCode.BadRequest, MessageTypes.Error, Resource.AuthenticationFailedDueToBadRequest);
            }

            // SendAsync without Authentication
            return base.SendAsync(request, cancellationToken);
        }

        /// <summary>
        /// Authorize user by validating token.
        /// </summary>
        /// <param name="requestMessage">Authorization context.</param>
        /// <returns>Returns a value indicating whether current request is authenticated or not.</returns>
        private bool Authorize(HttpRequestMessage requestMessage)
        {
            try
            {
                HttpRequest request = HttpContext.Current.Request;
                string token = request.Headers[SecurityToken];
                return SecurityUtility.IsTokenValid(token, request.UserAgent, requestMessage);
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}