using System;

namespace QuickBooksSharp.Policies
{
    public class RateLimitEvent(long? realmId, Uri requestUri)
    {
        public long? RealmId { get; } = realmId;

        public Uri RequestUri { get; } = requestUri;
    }
}
