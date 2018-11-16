using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Microsoft.Extensions.Logging.Abstractions;
using Okta.Sdk.Abstractions;
using Okta.Sdk.Abstractions.Configuration;

namespace Okta.Auth.Sdk.UnitTests.Internal
{
    public class TesteableAuthnClient : AuthenticationClient
    {
        public static readonly OktaClientConfiguration DefaultFakeConfiguration = new OktaClientConfiguration
        {
            OktaDomain = "https://fake.example.com",
            Token = "foobar",
        };

        public TesteableAuthnClient(IRequestExecutor requestExecutor)
            : base(
                new DefaultDataStore(requestExecutor, new DefaultSerializer(), new ResourceFactory(null, null, null),
                    NullLogger.Instance,
                    new UserAgentBuilder("test",
                        typeof(TesteableAuthnClient).GetTypeInfo().Assembly.GetName().Version)),
                DefaultFakeConfiguration,
                new RequestContext())
        {
        }
    }
}
