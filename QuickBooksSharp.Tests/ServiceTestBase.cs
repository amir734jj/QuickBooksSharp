using System.Threading.Tasks;
using QuickBooksSharp.Authentication;

namespace QuickBooksSharp.Tests
{
    public abstract class ServiceTestBase
    {
        protected async Task<string> GetAccessTokenAsync()
        {
            var res = await new AuthenticationService().RefreshOAuthTokenAsync(TestHelper.ClientId, TestHelper.ClientSecret, TestHelper.RefreshToken);
            if (res.refresh_token != TestHelper.RefreshToken)
                TestHelper.PersistNewRefreshToken(res.refresh_token);
            return res.access_token;
        }
    }
}
