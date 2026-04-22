using System.Net.Http;
using QuickBooksSharp.Infrastructure;

namespace QuickBooksSharp.Policies
{
    public class QuickBooksAPIResponse
    {
        internal HttpResponseMessage Response { get; private set; }

        internal QuickBooksException? Exception { get; private set; }

        public QuickBooksAPIResponse(HttpResponseMessage response, QuickBooksException? ex)
        {
            Response = response;
            Exception = ex;
        }
    }
}
