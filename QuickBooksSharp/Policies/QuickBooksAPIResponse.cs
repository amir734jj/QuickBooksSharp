using System.Net.Http;
using QuickBooksSharp.Infrastructure;

namespace QuickBooksSharp.Policies
{
    public class QuickBooksAPIResponse(HttpResponseMessage response, QuickBooksException? ex)
    {
        internal HttpResponseMessage Response { get; private set; } = response;

        internal QuickBooksException? Exception { get; private set; } = ex;
    }
}
