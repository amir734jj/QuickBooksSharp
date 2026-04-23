using System;
using System.Linq;
using System.Net.Http;

namespace QuickBooksSharp.Infrastructure
{
    public class QuickBooksException(HttpRequestMessage request, HttpResponseMessage response, string responseContent)
        : Exception($@"QuickBooks API call to {request.RequestUri} failed with code: {response.StatusCode}
IntuiTId: {GetHeaderValue(response, "intuit_tid")},
Reason: {response.ReasonPhrase}
Content: {responseContent}")
    {
        public HttpRequestMessage Request { get; } = request;

        public HttpResponseMessage Response { get; } = response;

        public string ResponseContent { get; } = responseContent;

        /// <summary>
        /// HTTP 401
        /// </summary>
        public bool IsUnauthorized => (int)Response.StatusCode == 401;

        /// <summary>
        /// HTTP 403
        /// </summary>
        public bool IsForbidden => (int)Response.StatusCode == 403;

        /// <summary>
        /// HTTP 429
        /// </summary>
        public bool IsRateLimit => (int)Response.StatusCode == 429;

        public string? IntuitTId => GetHeaderValue(Response, "intuit_tid");

        public string? QBOVersion => GetHeaderValue(Response, "QBO-Version");

        public string? ErrorCode => GetHeaderValue(Response, "ErrorCode");

        public string? ErrorCause => GetHeaderValue(Response, "ErrorCause");

        private static string? GetHeaderValue(HttpResponseMessage r, string headerName) => r.Headers.TryGetValues(headerName, out var values) ? values.FirstOrDefault() : null;
    }
}
