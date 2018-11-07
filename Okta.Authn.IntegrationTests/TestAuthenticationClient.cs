using Okta.Authn.Models;
using Okta.Sdk;
using Okta.Sdk.Abstractions.Configuration;

namespace Okta.Authn.IntegrationTests
{
    public static class TestAuthenticationClient
    {
        public static IAuthenticationClient Create(OktaClientConfiguration configuration = null)
        {
            // Configuration is expected to be stored in environment variables on the test machine.
            // A few tests pass in a configuration object, but this is just to override and test specific things.
            return new AuthenticationClient(configuration);
        }
    }
}
