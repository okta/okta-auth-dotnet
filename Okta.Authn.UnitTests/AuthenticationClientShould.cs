using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Okta.Authn.Abstractions;
using Okta.Authn.Models;
using Okta.Authn.POCOs;
using Okta.Authn.UnitTests.Internal;
using Xunit;

namespace Okta.Authn.UnitTests
{
    public class AuthenticationClientShould
    {
        [Fact]
        public async Task ForgotPasswordWithEmailFactor()
        {
            var rawResponse = @"
            {
                ""status"": ""RECOVERY_CHALLENGE"",
                ""factorResult"": ""WAITING"",
                ""relayState"": ""/myapp/some/deep/link/i/want/to/return/to"",
                ""factorType"": ""EMAIL"",
                ""recoveryType"": ""PASSWORD""
            }";

            var mockRequestExecutor = new MockedStringRequestExecutor(rawResponse);
            var authnClient = new TesteableAuthnClient(mockRequestExecutor);

            var forgotPasswordOptions = new ForgotPasswordOptions()
            {
                FactorType = FactorType.Email,
                RelayState = "/myapp/some/deep/link/i/want/to/return/to",
                UserName = "bob-user@test.com",
            };

            var authResponse = await authnClient.ForgotPasswordAsync(forgotPasswordOptions);
            authResponse.Should().NotBeNull();
            authResponse.AuthenticationStatus.Should().Be(AuthenticationStatus.RecoveryChallenge);
            authResponse.FactorType.Should().Be(FactorType.Email);
            authResponse.RelayState.Should().Be("/myapp/some/deep/link/i/want/to/return/to");
            // TODO: Add enums for RecoveryType & FactorResult
            authResponse.RecoveryType.Should().Be("PASSWORD");
            authResponse.FactorResult.Should().Be("WAITING");

        }

        [Fact]
        public async Task ForgotPasswordWithCallFactor()
        {
            #region raw response
            var rawResponse = @"
            {
              ""stateToken"": ""00xdqXOE5qDXX8-PBR1bYv8AESqIEinDy3yul01tyh"",
              ""expiresAt"": ""2015-11-03T10:15:57.000Z"",
              ""status"": ""RECOVERY_CHALLENGE"",
              ""relayState"": ""/myapp/some/deep/link/i/want/to/return/to"",
              ""factorType"": ""call"",
              ""recoveryType"": ""PASSWORD"",
              ""_links"": {
                ""next"": {
                  ""name"": ""verify"",
                  ""href"": ""https://dotnet.oktapreview.com/api/v1/authn/recovery/factors/CALL/verify"",
                  ""hints"": {
                    ""allow"": [
                      ""POST""
                    ]
                  }
                },
                ""cancel"": {
                  ""href"": ""https://dotnet.oktapreview.com/api/v1/authn/cancel"",
                  ""hints"": {
                    ""allow"": [
                      ""POST""
                    ]
                  }
                },
                ""resend"": {
                  ""name"": ""call"",
                  ""href"": ""https://dotnet.oktapreview.com/api/v1/authn/recovery/factors/CALL/resend"",
                  ""hints"": {
                    ""allow"": [
                      ""POST""
                    ]
                  }
                }
              }
            }";
            #endregion

            var mockRequestExecutor = new MockedStringRequestExecutor(rawResponse);
            var authnClient = new TesteableAuthnClient(mockRequestExecutor);

            var forgotPasswordOptions = new ForgotPasswordOptions()
            {
                FactorType = FactorType.Call,
                RelayState = "/myapp/some/deep/link/i/want/to/return/to",
                UserName = "bob-user@test.com",
            };

            var authResponse = await authnClient.ForgotPasswordAsync(forgotPasswordOptions);
            authResponse.Should().NotBeNull();
            authResponse.AuthenticationStatus.Should().Be(AuthenticationStatus.RecoveryChallenge);
            authResponse.FactorType.Should().Be(FactorType.Call);
            authResponse.RelayState.Should().Be("/myapp/some/deep/link/i/want/to/return/to");
            authResponse.RecoveryType.Should().Be("PASSWORD");
            authResponse.StateToken.Should().Be("00xdqXOE5qDXX8-PBR1bYv8AESqIEinDy3yul01tyh");
            authResponse.Links.Should().NotBeNull();

            var nextLink = authResponse.Links.GetProperty<Resource>("next");
            nextLink.Should().NotBeNull();
            nextLink.GetProperty<string>("name").Should().Be("verify");
            nextLink.GetProperty<string>("href").Should().Be("https://dotnet.oktapreview.com/api/v1/authn/recovery/factors/CALL/verify");

            var cancelLink = authResponse.Links.GetProperty<Resource>("cancel");
            cancelLink.Should().NotBeNull();
            cancelLink.GetProperty<string>("href").Should().Be("https://dotnet.oktapreview.com/api/v1/authn/cancel");

            var resendLink = authResponse.Links.GetProperty<Resource>("resend");
            resendLink.Should().NotBeNull();
            resendLink.GetProperty<string>("name").Should().Be("call");
            resendLink.GetProperty<string>("href").Should().Be("https://dotnet.oktapreview.com/api/v1/authn/recovery/factors/CALL/resend");
        }
    }
}
