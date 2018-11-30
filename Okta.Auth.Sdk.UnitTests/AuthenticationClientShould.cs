using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using NSubstitute;
using Okta.Auth.Sdk.UnitTests.Internal;
using Okta.Sdk.Abstractions;
using Xunit;

namespace Okta.Auth.Sdk.UnitTests
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
            authResponse.GetProperty<FactorType>("factorType").Should().Be(FactorType.Email);
            authResponse.RelayState.Should().Be("/myapp/some/deep/link/i/want/to/return/to");
            authResponse.GetProperty<RecoveryType>("recoveryType").Should().Be(RecoveryType.Password);
            authResponse.GetProperty<FactorResult>("factorResult").Should().Be(FactorResult.Waiting);

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
            authResponse.GetProperty<FactorType>("factorType").Should().Be(FactorType.Call);
            authResponse.RelayState.Should().Be("/myapp/some/deep/link/i/want/to/return/to");
            authResponse.GetProperty<RecoveryType>("recoveryType").Should().Be(RecoveryType.Password);
            authResponse.StateToken.Should().Be("00xdqXOE5qDXX8-PBR1bYv8AESqIEinDy3yul01tyh");
            authResponse.Links.Should().NotBeNull();

            var nextLink = authResponse.Links.GetProperty<Resource>("next");
            nextLink.Should().NotBeNull();
            nextLink.GetProperty<string>("name").Should().Be("verify");
            nextLink.GetProperty<string>("href").Should()
                .Be("https://dotnet.oktapreview.com/api/v1/authn/recovery/factors/CALL/verify");

            var cancelLink = authResponse.Links.GetProperty<Resource>("cancel");
            cancelLink.Should().NotBeNull();
            cancelLink.GetProperty<string>("href").Should().Be("https://dotnet.oktapreview.com/api/v1/authn/cancel");

            var resendLink = authResponse.Links.GetProperty<Resource>("resend");
            resendLink.Should().NotBeNull();
            resendLink.GetProperty<string>("name").Should().Be("call");
            resendLink.GetProperty<string>("href").Should()
                .Be("https://dotnet.oktapreview.com/api/v1/authn/recovery/factors/CALL/resend");
        }

        [Fact]
        public async Task ResetPassword()
        {
            #region raw response

            var rawResponse = @"
            {
                ""expiresAt"": ""2015-11-03T10:15:57.000Z"",
                ""status"": ""SUCCESS"",
                ""relayState"": ""/myapp/some/deep/link/i/want/to/return/to"",
                ""sessionToken"": ""00t6IUQiVbWpMLgtmwSjMFzqykb5QcaBNtveiWlGeM"",
                ""_embedded"": {
                    ""user"": {
                        ""id"": ""00ub0oNGTSWTBKOLGLNR"",
                        ""passwordChanged"": ""2015-11-08T20:14:45.000Z"",
                        ""profile"": {
                            ""login"": ""dade.murphy@example.com"",
                            ""firstName"": ""Dade"",
                            ""lastName"": ""Murphy"",
                            ""locale"": ""en_US"",
                            ""timeZone"": ""America/Los_Angeles""
                        }
                    }
                }
            }";

            #endregion

            var mockRequestExecutor = new MockedStringRequestExecutor(rawResponse);
            var authnClient = new TesteableAuthnClient(mockRequestExecutor);

            var resetPasswordOptions = new ResetPasswordOptions()
            {
                NewPassword = "Okta1234!",
                StateToken = "00xdqXOE5qDXX8-PBR1bYv8AESqIEinDy3yul01tyh",
            };

            var authResponse = await authnClient.ResetPasswordAsync(resetPasswordOptions);
            authResponse.Should().NotBeNull();
            authResponse.AuthenticationStatus.Should().Be(AuthenticationStatus.Success);
            authResponse.SessionToken.Should().Be("00t6IUQiVbWpMLgtmwSjMFzqykb5QcaBNtveiWlGeM");
            authResponse.ExpiresAt.Value.Date.Should().Be(new DateTime(2015, 11, 3));

            var user = authResponse.Embedded.GetProperty<Resource>("user");
            user.Should().NotBeNull();
            user.GetProperty<string>("id").Should().Be("00ub0oNGTSWTBKOLGLNR");
            user.GetProperty<DateTimeOffset?>("passwordChanged").Value.Date.Should().Be(new DateTime(2015, 11, 8));

            var profile = user.GetProperty<Resource>("profile");
            profile.Should().NotBeNull();
            profile.GetProperty<string>("login").Should().Be("dade.murphy@example.com");
            profile.GetProperty<string>("firstName").Should().Be("Dade");
            profile.GetProperty<string>("lastName").Should().Be("Murphy");
        }

        [Fact]
        public async Task AddXDeviceFingerprintToRequest()
        {
            var authOptions = new AuthenticateOptions()
            {
                Username = "foo",
                Password = "bar",
                DeviceFingerprint = "baz",
            };

            var mockRequestExecutor = Substitute.For<IRequestExecutor>();
            mockRequestExecutor
                .PostAsync(Arg.Any<string>(), Arg.Any<IEnumerable<KeyValuePair<string, string>>>(), Arg.Any<string>(), Arg.Any<CancellationToken>())
                .Returns(new HttpResponse<string>() { StatusCode = 200 });

            var authnClient = new TesteableAuthnClient(mockRequestExecutor);

            await authnClient.AuthenticateAsync(authOptions);

            await mockRequestExecutor.Received().PostAsync(
                "/api/v1/authn",
                Arg.Is<IEnumerable<KeyValuePair<string, string>>>(headers => headers.Any(kvp => kvp.Key == "X-Device-Fingerprint" && kvp.Value == "baz")),
                "{\"username\":\"foo\",\"password\":\"bar\"}",
                CancellationToken.None);
        }

        [Fact]
        public async Task SendTokenWhenAuthenticatingWithActivationToken()
        {
            var authOptions = new AuthenticateWithActivationTokenOptions()
            {
                 ActivationToken = "o7AFoTGE9xjQiHQK6dAa",
            };

            var mockRequestExecutor = Substitute.For<IRequestExecutor>();
            mockRequestExecutor
                .PostAsync(Arg.Any<string>(), Arg.Any<IEnumerable<KeyValuePair<string, string>>>(), Arg.Any<string>(), Arg.Any<CancellationToken>())
                .Returns(new HttpResponse<string>() { StatusCode = 200 });

            var authnClient = new TesteableAuthnClient(mockRequestExecutor);

            await authnClient.AuthenticateAsync(authOptions);

            await mockRequestExecutor.Received().PostAsync(
                "/api/v1/authn",
                Arg.Any<IEnumerable<KeyValuePair<string, string>>>(),
                "{\"token\":\"o7AFoTGE9xjQiHQK6dAa\"}",
                CancellationToken.None);
        }

        [Fact]
        public async Task SendWellStructuredRequestForPrimaryAuthentication()
        {
            var authOptions = new AuthenticateOptions()
            {
                Username = "foo",
                Password = "bar",
                RelayState = "/myapp/some/deep/link/i/want/to/return/to",
                MultiOptionalFactorEnroll = false,
                WarnBeforePasswordExpired = false,
                DeviceToken = "baz",
            };

            var mockRequestExecutor = Substitute.For<IRequestExecutor>();
            mockRequestExecutor
                .PostAsync(Arg.Any<string>(), Arg.Any<IEnumerable<KeyValuePair<string, string>>>(), Arg.Any<string>(), Arg.Any<CancellationToken>())
                .Returns(new HttpResponse<string>() { StatusCode = 200 });

            var authnClient = new TesteableAuthnClient(mockRequestExecutor);

            await authnClient.AuthenticateAsync(authOptions);

            await mockRequestExecutor.Received().PostAsync(
                "/api/v1/authn",
                Arg.Any<IEnumerable<KeyValuePair<string, string>>>(),
                "{\"username\":\"foo\",\"password\":\"bar\",\"relayState\":\"/myapp/some/deep/link/i/want/to/return/to\",\"options\":{\"multiOptionalFactorEnroll\":false,\"warnBeforePasswordExpired\":false},\"context\":{\"deviceToken\":\"baz\"}}",
                CancellationToken.None);
        }

        [Fact]
        public async Task SendWellStructuredRequestForIdpInitiatedAuthentication()
        {
            var authOptions = new AuthenticateOptions()
            {
                Username = "foo",
                Password = "bar",
                Audience = "aud",
                MultiOptionalFactorEnroll = false,
                WarnBeforePasswordExpired = true,
            };

            var mockRequestExecutor = Substitute.For<IRequestExecutor>();
            mockRequestExecutor
                .PostAsync(Arg.Any<string>(), Arg.Any<IEnumerable<KeyValuePair<string, string>>>(), Arg.Any<string>(), Arg.Any<CancellationToken>())
                .Returns(new HttpResponse<string>() { StatusCode = 200 });

            var authnClient = new TesteableAuthnClient(mockRequestExecutor);

            await authnClient.AuthenticateAsync(authOptions);

            await mockRequestExecutor.Received().PostAsync(
                "/api/v1/authn",
                Arg.Any<IEnumerable<KeyValuePair<string, string>>>(),
                "{\"username\":\"foo\",\"password\":\"bar\",\"audience\":\"aud\",\"options\":{\"multiOptionalFactorEnroll\":false,\"warnBeforePasswordExpired\":true}}",
                CancellationToken.None);
        }

        [Fact]
        public async Task SendWellStructuredRequestForSpInitiatedAuthenticationWithoutOktaSession()
        {
            var authOptions = new AuthenticateOptions()
            {
                Username = "foo",
                Password = "bar",
                StateToken = "baz"
            };

            var mockRequestExecutor = Substitute.For<IRequestExecutor>();
            mockRequestExecutor
                .PostAsync(Arg.Any<string>(), Arg.Any<IEnumerable<KeyValuePair<string, string>>>(), Arg.Any<string>(), Arg.Any<CancellationToken>())
                .Returns(new HttpResponse<string>() { StatusCode = 200 });

            var authnClient = new TesteableAuthnClient(mockRequestExecutor);

            await authnClient.AuthenticateAsync(authOptions);

            await mockRequestExecutor.Received().PostAsync(
                "/api/v1/authn",
                Arg.Any<IEnumerable<KeyValuePair<string, string>>>(),
                "{\"username\":\"foo\",\"password\":\"bar\",\"stateToken\":\"baz\"}",
                CancellationToken.None);
        }

        [Fact]
        public async Task SendWellStructuredRequestForSpInitiatedAuthenticationWithOktaSession()
        {
            var authOptions = new AuthenticateOptions()
            {
                StateToken = "foo",
            };

            var mockRequestExecutor = Substitute.For<IRequestExecutor>();
            mockRequestExecutor
                .PostAsync(Arg.Any<string>(), Arg.Any<IEnumerable<KeyValuePair<string, string>>>(), Arg.Any<string>(), Arg.Any<CancellationToken>())
                .Returns(new HttpResponse<string>() { StatusCode = 200 });

            var authnClient = new TesteableAuthnClient(mockRequestExecutor);

            await authnClient.AuthenticateAsync(authOptions);

            await mockRequestExecutor.Received().PostAsync(
                "/api/v1/authn",
                Arg.Any<IEnumerable<KeyValuePair<string, string>>>(),
                "{\"stateToken\":\"foo\"}",
                CancellationToken.None);
        }

        [Fact]
        public async Task SendWellStructuredRequestForEnrollSecurityQuestionFactor()
        {
            var enrollOptions = new EnrollSecurityQuestionFactorOptions()
            {
                StateToken = "foo",
                Question = "disliked_food",
                Answer = "mayo",
            };

            var mockRequestExecutor = Substitute.For<IRequestExecutor>();
            mockRequestExecutor
                .PostAsync(Arg.Any<string>(), Arg.Any<IEnumerable<KeyValuePair<string, string>>>(), Arg.Any<string>(), Arg.Any<CancellationToken>())
                .Returns(new HttpResponse<string>() { StatusCode = 200 });

            var authnClient = new TesteableAuthnClient(mockRequestExecutor);

            await authnClient.EnrollFactorAsync(enrollOptions);

            await mockRequestExecutor.Received().PostAsync(
                "/api/v1/authn/factors",
                Arg.Any<IEnumerable<KeyValuePair<string, string>>>(),
                "{\"stateToken\":\"foo\",\"factorType\":\"question\",\"provider\":\"OKTA\",\"profile\":{\"question\":\"disliked_food\",\"answer\":\"mayo\"}}",
                CancellationToken.None);
        }

        [Fact]
        public async Task SendWellStructuredRequestForEnrollSmsFactor()
        {
            var enrollOptions = new EnrollSmsFactorOptions()
            {
                StateToken = "foo",
                PhoneNumber = "+1-555-415-1337",
            };

            var mockRequestExecutor = Substitute.For<IRequestExecutor>();
            mockRequestExecutor
                .PostAsync(Arg.Any<string>(), Arg.Any<IEnumerable<KeyValuePair<string, string>>>(), Arg.Any<string>(), Arg.Any<CancellationToken>())
                .Returns(new HttpResponse<string>() { StatusCode = 200 });

            var authnClient = new TesteableAuthnClient(mockRequestExecutor);

            await authnClient.EnrollFactorAsync(enrollOptions);

            await mockRequestExecutor.Received().PostAsync(
                "/api/v1/authn/factors",
                Arg.Any<IEnumerable<KeyValuePair<string, string>>>(),
                "{\"stateToken\":\"foo\",\"factorType\":\"sms\",\"provider\":\"OKTA\",\"profile\":{\"phoneNumber\":\"+1-555-415-1337\"}}",
                CancellationToken.None);
        }

        [Fact]
        public async Task SendWellStructuredRequestForEnrollCallFactor()
        {
            var enrollOptions = new EnrollCallFactorOptions()
            {
                StateToken = "foo",
                PhoneNumber = "+1-555-415-1337",
            };

            var mockRequestExecutor = Substitute.For<IRequestExecutor>();
            mockRequestExecutor
                .PostAsync(Arg.Any<string>(), Arg.Any<IEnumerable<KeyValuePair<string, string>>>(), Arg.Any<string>(), Arg.Any<CancellationToken>())
                .Returns(new HttpResponse<string>() { StatusCode = 200 });

            var authnClient = new TesteableAuthnClient(mockRequestExecutor);

            await authnClient.EnrollFactorAsync(enrollOptions);

            await mockRequestExecutor.Received().PostAsync(
                "/api/v1/authn/factors",
                Arg.Any<IEnumerable<KeyValuePair<string, string>>>(),
                "{\"stateToken\":\"foo\",\"factorType\":\"call\",\"provider\":\"OKTA\",\"profile\":{\"phoneNumber\":\"+1-555-415-1337\"}}",
                CancellationToken.None);
        }

        [Fact]
        public async Task SendWellStructuredRequestForEnrollTotpFactor()
        {
            var enrollOptions = new EnrollTotpFactorOptions()
            {
                StateToken = "foo",
                Provider = OktaDefaults.GoogleProvider,
            };

            var mockRequestExecutor = Substitute.For<IRequestExecutor>();
            mockRequestExecutor
                .PostAsync(Arg.Any<string>(), Arg.Any<IEnumerable<KeyValuePair<string, string>>>(), Arg.Any<string>(), Arg.Any<CancellationToken>())
                .Returns(new HttpResponse<string>() { StatusCode = 200 });

            var authnClient = new TesteableAuthnClient(mockRequestExecutor);

            await authnClient.EnrollFactorAsync(enrollOptions);

            await mockRequestExecutor.Received().PostAsync(
                "/api/v1/authn/factors",
                Arg.Any<IEnumerable<KeyValuePair<string, string>>>(),
                "{\"stateToken\":\"foo\",\"factorType\":\"token:software:totp\",\"provider\":\"GOOGLE\"}",
                CancellationToken.None);
        }

        [Fact]
        public async Task SendWellStructuredRequestForEnrollPushFactor()
        {
            var enrollOptions = new EnrollPushFactorOptions()
            {
                StateToken = "foo",
            };

            var mockRequestExecutor = Substitute.For<IRequestExecutor>();
            mockRequestExecutor
                .PostAsync(Arg.Any<string>(), Arg.Any<IEnumerable<KeyValuePair<string, string>>>(), Arg.Any<string>(), Arg.Any<CancellationToken>())
                .Returns(new HttpResponse<string>() { StatusCode = 200 });

            var authnClient = new TesteableAuthnClient(mockRequestExecutor);

            await authnClient.EnrollFactorAsync(enrollOptions);

            await mockRequestExecutor.Received().PostAsync(
                "/api/v1/authn/factors",
                Arg.Any<IEnumerable<KeyValuePair<string, string>>>(),
                "{\"stateToken\":\"foo\",\"factorType\":\"push\",\"provider\":\"OKTA\"}",
                CancellationToken.None);
        }

        [Fact]
        public async Task SendWellStructuredRequestForEnrollRsaFactor()
        {
            var enrollOptions = new EnrollRsaFactorOptions()
            {
                StateToken = "foo",
                CredentialId = "dade.murphy@example.com",
                PassCode = "bar",
            };

            var mockRequestExecutor = Substitute.For<IRequestExecutor>();
            mockRequestExecutor
                .PostAsync(Arg.Any<string>(), Arg.Any<IEnumerable<KeyValuePair<string, string>>>(), Arg.Any<string>(), Arg.Any<CancellationToken>())
                .Returns(new HttpResponse<string>() { StatusCode = 200 });

            var authnClient = new TesteableAuthnClient(mockRequestExecutor);

            await authnClient.EnrollFactorAsync(enrollOptions);

            await mockRequestExecutor.Received().PostAsync(
                "/api/v1/authn/factors",
                Arg.Any<IEnumerable<KeyValuePair<string, string>>>(),
                "{\"stateToken\":\"foo\",\"factorType\":\"token\",\"provider\":\"RSA\",\"passCode\":\"bar\",\"profile\":{\"credentialId\":\"dade.murphy@example.com\"}}",
                CancellationToken.None);
        }

        [Fact]
        public async Task SendWellStructuredRequestForEnrollSymantecFactor()
        {
            var enrollOptions = new EnrollSymantecFactorOptions()
            {
                StateToken = "foo",
                CredentialId = "dade.murphy@example.com",
                PassCode = "bar",
                NextPassCode = "baz",
            };

            var mockRequestExecutor = Substitute.For<IRequestExecutor>();
            mockRequestExecutor
                .PostAsync(Arg.Any<string>(), Arg.Any<IEnumerable<KeyValuePair<string, string>>>(), Arg.Any<string>(), Arg.Any<CancellationToken>())
                .Returns(new HttpResponse<string>() { StatusCode = 200 });

            var authnClient = new TesteableAuthnClient(mockRequestExecutor);

            await authnClient.EnrollFactorAsync(enrollOptions);

            await mockRequestExecutor.Received().PostAsync(
                "/api/v1/authn/factors",
                Arg.Any<IEnumerable<KeyValuePair<string, string>>>(),
                "{\"stateToken\":\"foo\",\"factorType\":\"token\",\"provider\":\"SYMANTEC\",\"passCode\":\"bar\",\"nextPassCode\":\"baz\",\"profile\":{\"credentialId\":\"dade.murphy@example.com\"}}",
                CancellationToken.None);
        }

        [Fact]
        public async Task SendWellStructuredRequestForEnrollYubikeyFactor()
        {
            var enrollOptions = new EnrollYubiKeyFactorOptions()
            {
                PassCode = "bar",
                StateToken = "foo",
            };

            var mockRequestExecutor = Substitute.For<IRequestExecutor>();
            mockRequestExecutor
                .PostAsync(Arg.Any<string>(), Arg.Any<IEnumerable<KeyValuePair<string, string>>>(), Arg.Any<string>(), Arg.Any<CancellationToken>())
                .Returns(new HttpResponse<string>() { StatusCode = 200 });

            var authnClient = new TesteableAuthnClient(mockRequestExecutor);

            await authnClient.EnrollFactorAsync(enrollOptions);

            await mockRequestExecutor.Received().PostAsync(
                "/api/v1/authn/factors",
                Arg.Any<IEnumerable<KeyValuePair<string, string>>>(),
                "{\"stateToken\":\"foo\",\"factorType\":\"token:hardware\",\"provider\":\"YUBICO\",\"passCode\":\"bar\"}",
                CancellationToken.None);
        }

        [Fact]
        public async Task SendWellStructuredRequestForEnrollDuoFactor()
        {
            var enrollOptions = new EnrollDuoFactorOptions()
            {
                StateToken = "foo",
            };

            var mockRequestExecutor = Substitute.For<IRequestExecutor>();
            mockRequestExecutor
                .PostAsync(Arg.Any<string>(), Arg.Any<IEnumerable<KeyValuePair<string, string>>>(), Arg.Any<string>(), Arg.Any<CancellationToken>())
                .Returns(new HttpResponse<string>() { StatusCode = 200 });

            var authnClient = new TesteableAuthnClient(mockRequestExecutor);

            await authnClient.EnrollFactorAsync(enrollOptions);

            await mockRequestExecutor.Received().PostAsync(
                "/api/v1/authn/factors",
                Arg.Any<IEnumerable<KeyValuePair<string, string>>>(),
                "{\"stateToken\":\"foo\",\"factorType\":\"web\",\"provider\":\"DUO\"}",
                CancellationToken.None);
        }

        [Fact]
        public async Task SendWellStructuredRequestForEnrollU2fFactor()
        {
            var enrollOptions = new EnrollU2FFactorOptions()
            {
                StateToken = "foo",
            };

            var mockRequestExecutor = Substitute.For<IRequestExecutor>();
            mockRequestExecutor
                .PostAsync(Arg.Any<string>(), Arg.Any<IEnumerable<KeyValuePair<string, string>>>(), Arg.Any<string>(), Arg.Any<CancellationToken>())
                .Returns(new HttpResponse<string>() { StatusCode = 200 });

            var authnClient = new TesteableAuthnClient(mockRequestExecutor);

            await authnClient.EnrollFactorAsync(enrollOptions);

            await mockRequestExecutor.Received().PostAsync(
                "/api/v1/authn/factors",
                Arg.Any<IEnumerable<KeyValuePair<string, string>>>(),
                "{\"stateToken\":\"foo\",\"factorType\":\"u2f\",\"provider\":\"FIDO\"}",
                CancellationToken.None);
        }

        [Fact]
        public async Task SendWellStructuredRequestForActivateFactor()
        {
            var activateFactorOptions = new ActivateFactorOptions()
            {
                FactorId = "ostf1fmaMGJLMNGNLIVG",
                StateToken = "foo",
                PassCode = "bar",
            };

            var mockRequestExecutor = Substitute.For<IRequestExecutor>();
            mockRequestExecutor
                .PostAsync(Arg.Any<string>(), Arg.Any<IEnumerable<KeyValuePair<string, string>>>(), Arg.Any<string>(), Arg.Any<CancellationToken>())
                .Returns(new HttpResponse<string>() { StatusCode = 200 });

            var authnClient = new TesteableAuthnClient(mockRequestExecutor);

            await authnClient.ActivateFactorAsync(activateFactorOptions);

            await mockRequestExecutor.Received().PostAsync(
                "/api/v1/authn/factors/ostf1fmaMGJLMNGNLIVG/lifecycle/activate",
                Arg.Any<IEnumerable<KeyValuePair<string, string>>>(),
                "{\"stateToken\":\"foo\",\"passCode\":\"bar\"}",
                CancellationToken.None);
        }

        [Fact]
        public async Task SendWellStructuredRequestForActivateU2fFactor()
        {
            var activateFactorOptions = new ActivateU2fFactorOptions()
            {
                FactorId = "ostf1fmaMGJLMNGNLIVG",
                StateToken = "foo",
                RegistrationData = "bar",
                ClientData = "baz",
            };

            var mockRequestExecutor = Substitute.For<IRequestExecutor>();
            mockRequestExecutor
                .PostAsync(Arg.Any<string>(), Arg.Any<IEnumerable<KeyValuePair<string, string>>>(), Arg.Any<string>(), Arg.Any<CancellationToken>())
                .Returns(new HttpResponse<string>() { StatusCode = 200 });

            var authnClient = new TesteableAuthnClient(mockRequestExecutor);

            await authnClient.ActivateFactorAsync(activateFactorOptions);

            await mockRequestExecutor.Received().PostAsync(
                "/api/v1/authn/factors/ostf1fmaMGJLMNGNLIVG/lifecycle/activate",
                Arg.Any<IEnumerable<KeyValuePair<string, string>>>(),
                "{\"stateToken\":\"foo\",\"clientData\":\"baz\",\"registrationData\":\"bar\"}",
                CancellationToken.None);
        }

        [Fact]
        public async Task SendWellStructuredRequestForActivatePushFactor()
        {
            var activateFactorOptions = new ActivatePushFactorOptions()
            {
                FactorId = "ostf1fmaMGJLMNGNLIVG",
                StateToken = "foo",
            };

            var mockRequestExecutor = Substitute.For<IRequestExecutor>();
            mockRequestExecutor
                .PostAsync(Arg.Any<string>(), Arg.Any<IEnumerable<KeyValuePair<string, string>>>(), Arg.Any<string>(), Arg.Any<CancellationToken>())
                .Returns(new HttpResponse<string>() { StatusCode = 200 });

            var authnClient = new TesteableAuthnClient(mockRequestExecutor);

            await authnClient.ActivateFactorAsync(activateFactorOptions);

            await mockRequestExecutor.Received().PostAsync(
                "/api/v1/authn/factors/ostf1fmaMGJLMNGNLIVG/lifecycle/activate",
                Arg.Any<IEnumerable<KeyValuePair<string, string>>>(),
                "{\"stateToken\":\"foo\"}",
                CancellationToken.None);
        }

        [Fact]
        public async Task SendWellStructuredRequestForVerifySecurityQuestionFactor()
        {
            var verifyFactorOptions = new VerifySecurityQuestionFactorOptions()
            {
                FactorId = "ostf1fmaMGJLMNGNLIVG",
                StateToken = "foo",
                Answer = "bar",
            };

            var mockRequestExecutor = Substitute.For<IRequestExecutor>();
            mockRequestExecutor
                .PostAsync(Arg.Any<string>(), Arg.Any<IEnumerable<KeyValuePair<string, string>>>(), Arg.Any<string>(), Arg.Any<CancellationToken>())
                .Returns(new HttpResponse<string>() { StatusCode = 200 });

            var authnClient = new TesteableAuthnClient(mockRequestExecutor);

            await authnClient.VerifyFactorAsync(verifyFactorOptions);

            await mockRequestExecutor.Received().PostAsync(
                "/api/v1/authn/factors/ostf1fmaMGJLMNGNLIVG/verify",
                Arg.Any<IEnumerable<KeyValuePair<string, string>>>(),
                "{\"stateToken\":\"foo\",\"answer\":\"bar\"}",
                CancellationToken.None);
        }

        [Fact]
        public async Task SendWellStructuredRequestForVerifySmsFactor()
        {
            var verifyFactorOptions = new VerifySmsFactorOptions()
            {
                FactorId = "ostf1fmaMGJLMNGNLIVG",
                StateToken = "foo",
                PassCode = "bar",
                RememberDevice = true,
            };

            var mockRequestExecutor = Substitute.For<IRequestExecutor>();
            mockRequestExecutor
                .PostAsync(Arg.Any<string>(), Arg.Any<IEnumerable<KeyValuePair<string, string>>>(), Arg.Any<string>(), Arg.Any<CancellationToken>())
                .Returns(new HttpResponse<string>() { StatusCode = 200 });

            var authnClient = new TesteableAuthnClient(mockRequestExecutor);

            await authnClient.VerifyFactorAsync(verifyFactorOptions);

            await mockRequestExecutor.Received().PostAsync(
                "/api/v1/authn/factors/ostf1fmaMGJLMNGNLIVG/verify",
                Arg.Any<IEnumerable<KeyValuePair<string, string>>>(),
                "{\"stateToken\":\"foo\",\"passCode\":\"bar\",\"rememberDevice\":true}",
                CancellationToken.None);
        }

        [Fact]
        public async Task SendWellStructuredRequestForVerifyTotpFactor()
        {
            var verifyFactorOptions = new VerifyTotpFactorOptions()
            {
                FactorId = "ostf1fmaMGJLMNGNLIVG",
                StateToken = "foo",
                PassCode = "bar",
                RememberDevice = true,
            };

            var mockRequestExecutor = Substitute.For<IRequestExecutor>();
            mockRequestExecutor
                .PostAsync(Arg.Any<string>(), Arg.Any<IEnumerable<KeyValuePair<string, string>>>(), Arg.Any<string>(), Arg.Any<CancellationToken>())
                .Returns(new HttpResponse<string>() { StatusCode = 200 });

            var authnClient = new TesteableAuthnClient(mockRequestExecutor);

            await authnClient.VerifyFactorAsync(verifyFactorOptions);

            await mockRequestExecutor.Received().PostAsync(
                "/api/v1/authn/factors/ostf1fmaMGJLMNGNLIVG/verify",
                Arg.Any<IEnumerable<KeyValuePair<string, string>>>(),
                "{\"stateToken\":\"foo\",\"passCode\":\"bar\",\"rememberDevice\":true}",
                CancellationToken.None);
        }

        [Fact]
        public async Task SendWellStructuredRequestForVerifyPushFactor()
        {
            var verifyFactorOptions = new VerifyPushFactorOptions()
            {
                FactorId = "ostf1fmaMGJLMNGNLIVG",
                StateToken = "foo",
                AutoPush = true,
                RememberDevice = true,
            };

            var mockRequestExecutor = Substitute.For<IRequestExecutor>();
            mockRequestExecutor
                .PostAsync(Arg.Any<string>(), Arg.Any<IEnumerable<KeyValuePair<string, string>>>(), Arg.Any<string>(), Arg.Any<CancellationToken>())
                .Returns(new HttpResponse<string>() { StatusCode = 200 });

            var authnClient = new TesteableAuthnClient(mockRequestExecutor);

            await authnClient.VerifyFactorAsync(verifyFactorOptions);

            await mockRequestExecutor.Received().PostAsync(
                "/api/v1/authn/factors/ostf1fmaMGJLMNGNLIVG/verify",
                Arg.Any<IEnumerable<KeyValuePair<string, string>>>(),
                "{\"stateToken\":\"foo\",\"rememberDevice\":true,\"autoPush\":true}",
                CancellationToken.None);
        }

        [Fact]
        public async Task SendWellStructuredRequestForVerifyDuoFactor()
        {
            var verifyFactorOptions = new VerifyDuoFactorOptions()
            {
                FactorId = "ostf1fmaMGJLMNGNLIVG",
                StateToken = "foo",
            };

            var mockRequestExecutor = Substitute.For<IRequestExecutor>();
            mockRequestExecutor
                .PostAsync(Arg.Any<string>(), Arg.Any<IEnumerable<KeyValuePair<string, string>>>(), Arg.Any<string>(), Arg.Any<CancellationToken>())
                .Returns(new HttpResponse<string>() { StatusCode = 200 });

            var authnClient = new TesteableAuthnClient(mockRequestExecutor);

            await authnClient.VerifyFactorAsync(verifyFactorOptions);

            await mockRequestExecutor.Received().PostAsync(
                "/api/v1/authn/factors/ostf1fmaMGJLMNGNLIVG/verify",
                Arg.Any<IEnumerable<KeyValuePair<string, string>>>(),
                "{\"stateToken\":\"foo\"}",
                CancellationToken.None);
        }

        [Fact]
        public async Task SendWellStructuredRequestForVerifyU2fFactor()
        {
            var verifyFactorOptions = new VerifyU2FFactorOptions()
            {
                FactorId = "ostf1fmaMGJLMNGNLIVG",
                StateToken = "foo",
                ClientData = "bar",
                SignatureData = "baz",
                RememberDevice = true,
            };

            var mockRequestExecutor = Substitute.For<IRequestExecutor>();
            mockRequestExecutor
                .PostAsync(Arg.Any<string>(), Arg.Any<IEnumerable<KeyValuePair<string, string>>>(), Arg.Any<string>(), Arg.Any<CancellationToken>())
                .Returns(new HttpResponse<string>() { StatusCode = 200 });

            var authnClient = new TesteableAuthnClient(mockRequestExecutor);

            await authnClient.VerifyFactorAsync(verifyFactorOptions);

            await mockRequestExecutor.Received().PostAsync(
                "/api/v1/authn/factors/ostf1fmaMGJLMNGNLIVG/verify",
                Arg.Any<IEnumerable<KeyValuePair<string, string>>>(),
                "{\"stateToken\":\"foo\",\"clientData\":\"bar\",\"signatureData\":\"baz\",\"rememberDevice\":true}",
                CancellationToken.None);
        }

        [Theory]
        [InlineData("sms")]
        [InlineData("email")]
        public async Task SendWellStructuredRequestForUnlockAccount(string factorType)
        {
            var unlockAccountOptions = new UnlockAccountOptions()
            {
                FactorType = new FactorType(factorType),
                RelayState = "/myapp/some/deep/link/i/want/to/return/to",
                Username = "dade.murphy@example.com",
            };

            var mockRequestExecutor = Substitute.For<IRequestExecutor>();
            mockRequestExecutor
                .PostAsync(Arg.Any<string>(), Arg.Any<IEnumerable<KeyValuePair<string, string>>>(), Arg.Any<string>(), Arg.Any<CancellationToken>())
                .Returns(new HttpResponse<string>() { StatusCode = 200 });

            var authnClient = new TesteableAuthnClient(mockRequestExecutor);

            await authnClient.UnlockAccountAsync(unlockAccountOptions);

            await mockRequestExecutor.Received().PostAsync(
                "/api/v1/authn/recovery/unlock",
                Arg.Any<IEnumerable<KeyValuePair<string, string>>>(),
                $"{{\"factorType\":\"{factorType.ToLower()}\",\"relayState\":\"/myapp/some/deep/link/i/want/to/return/to\",\"username\":\"dade.murphy@example.com\"}}",
                CancellationToken.None);
        }

        [Theory]
        [InlineData("sms")]
        [InlineData("call")]
        public async Task SendWellStructuredRequestForVerifyRecoverFactor(string factorType)
        {
            var verifyFactorOptions = new VerifyRecoveryFactorOptions()
            {
                StateToken = "foo",
                PassCode = "bar",
                FactorType = new FactorType(factorType),
            };

            var mockRequestExecutor = Substitute.For<IRequestExecutor>();
            mockRequestExecutor
                .PostAsync(Arg.Any<string>(), Arg.Any<IEnumerable<KeyValuePair<string, string>>>(), Arg.Any<string>(), Arg.Any<CancellationToken>())
                .Returns(new HttpResponse<string>() { StatusCode = 200 });

            var authnClient = new TesteableAuthnClient(mockRequestExecutor);

            await authnClient.VerifyRecoveryFactorAsync(verifyFactorOptions);

            await mockRequestExecutor.Received().PostAsync(
                $"/api/v1/authn/recovery/factors/{factorType.ToLower()}/verify",
                Arg.Any<IEnumerable<KeyValuePair<string, string>>>(),
                "{\"stateToken\":\"foo\",\"passCode\":\"bar\"}",
                CancellationToken.None);
        }

        [Fact]
        public async Task SendWellStructuredRequestForVerifyRecoveryToken()
        {
            var verifyFactorOptions = new VerifyRecoveryTokenOptions()
            {
                RecoveryToken = "foo",
            };

            var mockRequestExecutor = Substitute.For<IRequestExecutor>();
            mockRequestExecutor
                .PostAsync(Arg.Any<string>(), Arg.Any<IEnumerable<KeyValuePair<string, string>>>(), Arg.Any<string>(), Arg.Any<CancellationToken>())
                .Returns(new HttpResponse<string>() { StatusCode = 200 });

            var authnClient = new TesteableAuthnClient(mockRequestExecutor);

            await authnClient.VerifyRecoveryTokenAsync(verifyFactorOptions);

            await mockRequestExecutor.Received().PostAsync(
                "/api/v1/authn/recovery/token",
                Arg.Any<IEnumerable<KeyValuePair<string, string>>>(),
                "{\"recoveryToken\":\"foo\"}",
                CancellationToken.None);
        }

        [Fact]
        public async Task SendWellStructuredRequestForAnswerRecoveryQuestion()
        {
            var answerRecoveryQuestionOptions = new AnswerRecoveryQuestionOptions()
            {
                StateToken = "foo",
                Answer = "bar",
            };

            var mockRequestExecutor = Substitute.For<IRequestExecutor>();
            mockRequestExecutor
                .PostAsync(Arg.Any<string>(), Arg.Any<IEnumerable<KeyValuePair<string, string>>>(), Arg.Any<string>(), Arg.Any<CancellationToken>())
                .Returns(new HttpResponse<string>() { StatusCode = 200 });

            var authnClient = new TesteableAuthnClient(mockRequestExecutor);

            await authnClient.AnswerRecoveryQuestionAsync(answerRecoveryQuestionOptions);

            await mockRequestExecutor.Received().PostAsync(
                "/api/v1/authn/recovery/answer",
                Arg.Any<IEnumerable<KeyValuePair<string, string>>>(),
                "{\"stateToken\":\"foo\",\"answer\":\"bar\"}",
                CancellationToken.None);
        }

        [Fact]
        public async Task SendWellStructuredRequestForVerifyCallFactor()
        {
            var verifyFactorOptions = new VerifyCallFactorOptions()
            {
                FactorId = "ostf1fmaMGJLMNGNLIVG",
                StateToken = "foo",
                PassCode = "bar",
                RememberDevice = true,
            };

            var mockRequestExecutor = Substitute.For<IRequestExecutor>();
            mockRequestExecutor
                .PostAsync(Arg.Any<string>(), Arg.Any<IEnumerable<KeyValuePair<string, string>>>(), Arg.Any<string>(), Arg.Any<CancellationToken>())
                .Returns(new HttpResponse<string>() { StatusCode = 200 });

            var authnClient = new TesteableAuthnClient(mockRequestExecutor);

            await authnClient.VerifyFactorAsync(verifyFactorOptions);

            await mockRequestExecutor.Received().PostAsync(
                "/api/v1/authn/factors/ostf1fmaMGJLMNGNLIVG/verify",
                Arg.Any<IEnumerable<KeyValuePair<string, string>>>(),
                "{\"stateToken\":\"foo\",\"passCode\":\"bar\",\"rememberDevice\":true}",
                CancellationToken.None);
        }

        [Fact]
        public async Task SendWellStructuredRequestForGetTransactionState()
        {
            var transactionStateOptions = new TransactionStateOptions()
            {
                StateToken = "foo",
            };

            var mockRequestExecutor = Substitute.For<IRequestExecutor>();
            mockRequestExecutor
                .PostAsync(Arg.Any<string>(), Arg.Any<IEnumerable<KeyValuePair<string, string>>>(), Arg.Any<string>(), Arg.Any<CancellationToken>())
                .Returns(new HttpResponse<string>() { StatusCode = 200 });

            var authnClient = new TesteableAuthnClient(mockRequestExecutor);

            await authnClient.GetTransactionStateAsync(transactionStateOptions);

            await mockRequestExecutor.Received().PostAsync(
                "/api/v1/authn",
                Arg.Any<IEnumerable<KeyValuePair<string, string>>>(),
                "{\"stateToken\":\"foo\"}",
                CancellationToken.None);
        }

        [Fact]
        public async Task SendWellStructuredRequestForGetPreviousTransactionState()
        {
            var transactionStateOptions = new TransactionStateOptions()
            {
                StateToken = "foo",
            };

            var mockRequestExecutor = Substitute.For<IRequestExecutor>();
            mockRequestExecutor
                .PostAsync(Arg.Any<string>(), Arg.Any<IEnumerable<KeyValuePair<string, string>>>(), Arg.Any<string>(), Arg.Any<CancellationToken>())
                .Returns(new HttpResponse<string>() { StatusCode = 200 });

            var authnClient = new TesteableAuthnClient(mockRequestExecutor);

            await authnClient.GetPreviousTransactionStateAsync(transactionStateOptions);

            await mockRequestExecutor.Received().PostAsync(
                "/api/v1/authn/previous",
                Arg.Any<IEnumerable<KeyValuePair<string, string>>>(),
                "{\"stateToken\":\"foo\"}",
                CancellationToken.None);
        }

        [Fact]
        public async Task SendWellStructuredRequestForSkipTransactionState()
        {
            var transactionStateOptions = new TransactionStateOptions()
            {
                StateToken = "foo",
            };

            var mockRequestExecutor = Substitute.For<IRequestExecutor>();
            mockRequestExecutor
                .PostAsync(Arg.Any<string>(), Arg.Any<IEnumerable<KeyValuePair<string, string>>>(), Arg.Any<string>(), Arg.Any<CancellationToken>())
                .Returns(new HttpResponse<string>() { StatusCode = 200 });

            var authnClient = new TesteableAuthnClient(mockRequestExecutor);

            await authnClient.SkipTransactionStateAsync(transactionStateOptions);

            await mockRequestExecutor.Received().PostAsync(
                "/api/v1/authn/skip",
                Arg.Any<IEnumerable<KeyValuePair<string, string>>>(),
                "{\"stateToken\":\"foo\"}",
                CancellationToken.None);
        }

        [Fact]
        public async Task SendWellStructuredRequestForCancelTransactionState()
        {
            var transactionStateOptions = new TransactionStateOptions()
            {
                StateToken = "foo",
            };

            var mockRequestExecutor = Substitute.For<IRequestExecutor>();
            mockRequestExecutor
                .PostAsync(Arg.Any<string>(), Arg.Any<IEnumerable<KeyValuePair<string, string>>>(), Arg.Any<string>(), Arg.Any<CancellationToken>())
                .Returns(new HttpResponse<string>() { StatusCode = 200 });

            var authnClient = new TesteableAuthnClient(mockRequestExecutor);

            await authnClient.CancelTransactionStateAsync(transactionStateOptions);

            await mockRequestExecutor.Received().PostAsync(
                "/api/v1/authn/cancel",
                Arg.Any<IEnumerable<KeyValuePair<string, string>>>(),
                "{\"stateToken\":\"foo\"}",
                CancellationToken.None);
        }

        [Fact]
        public async Task SendWellStructuredRequestForResendEnrollSmsFactor()
        {
            var enrollOptions = new EnrollSmsFactorOptions()
            {
                StateToken = "foo",
                PhoneNumber = "+1-555-415-1337",
                FactorId = "bar",
            };

            var mockRequestExecutor = Substitute.For<IRequestExecutor>();
            mockRequestExecutor
                .PostAsync(Arg.Any<string>(), Arg.Any<IEnumerable<KeyValuePair<string, string>>>(), Arg.Any<string>(), Arg.Any<CancellationToken>())
                .Returns(new HttpResponse<string>() { StatusCode = 200 });

            var authnClient = new TesteableAuthnClient(mockRequestExecutor);

            await authnClient.ResendSmsEnrollFactorAsync(enrollOptions);

            await mockRequestExecutor.Received().PostAsync(
                "/api/v1/authn/factors/bar/lifecycle/resend",
                Arg.Any<IEnumerable<KeyValuePair<string, string>>>(),
                "{\"stateToken\":\"foo\",\"factorType\":\"sms\",\"provider\":\"OKTA\",\"profile\":{\"phoneNumber\":\"+1-555-415-1337\"}}",
                CancellationToken.None);
        }

        [Fact]
        public async Task SendWellStructuredRequestForResendEnrollCallFactor()
        {
            var enrollOptions = new EnrollCallFactorOptions()
            {
                StateToken = "foo",
                PhoneNumber = "+1-555-415-1337",
                FactorId = "bar",
            };

            var mockRequestExecutor = Substitute.For<IRequestExecutor>();
            mockRequestExecutor
                .PostAsync(Arg.Any<string>(), Arg.Any<IEnumerable<KeyValuePair<string, string>>>(), Arg.Any<string>(), Arg.Any<CancellationToken>())
                .Returns(new HttpResponse<string>() { StatusCode = 200 });

            var authnClient = new TesteableAuthnClient(mockRequestExecutor);

            await authnClient.ResendCallEnrollFactorAsync(enrollOptions);

            await mockRequestExecutor.Received().PostAsync(
                "/api/v1/authn/factors/bar/lifecycle/resend",
                Arg.Any<IEnumerable<KeyValuePair<string, string>>>(),
                "{\"stateToken\":\"foo\",\"factorType\":\"call\",\"provider\":\"OKTA\",\"profile\":{\"phoneNumber\":\"+1-555-415-1337\"}}",
                CancellationToken.None);
        }

        [Fact]
        public async Task SendWellStructuredRequestForResendVerifyChallenge()
        {
            var verifyOptions = new ResendChallengeOptions()
            {
                StateToken = "foo",
                FactorId = "bar",
            };

            var mockRequestExecutor = Substitute.For<IRequestExecutor>();
            mockRequestExecutor
                .PostAsync(Arg.Any<string>(), Arg.Any<IEnumerable<KeyValuePair<string, string>>>(), Arg.Any<string>(), Arg.Any<CancellationToken>())
                .Returns(new HttpResponse<string>() { StatusCode = 200 });

            var authnClient = new TesteableAuthnClient(mockRequestExecutor);

            await authnClient.ResendVerifyChallengeAsync(verifyOptions);

            await mockRequestExecutor.Received().PostAsync(
                "/api/v1/authn/factors/bar/verify/resend",
                Arg.Any<IEnumerable<KeyValuePair<string, string>>>(),
                "{\"stateToken\":\"foo\"}",
                CancellationToken.None);
        }

        [Theory]
        [InlineData("sms")]
        [InlineData("call")]
        public async Task SendWellStructuredRequestForResendRecoveryChallenge(string factorType)
        {
            var recoveryOptions = new ResendRecoveryChallengeOptions()
            {
                StateToken = "foo",
                FactorType = new FactorType(factorType),
            };

            var mockRequestExecutor = Substitute.For<IRequestExecutor>();
            mockRequestExecutor
                .PostAsync(Arg.Any<string>(), Arg.Any<IEnumerable<KeyValuePair<string, string>>>(), Arg.Any<string>(), Arg.Any<CancellationToken>())
                .Returns(new HttpResponse<string>() { StatusCode = 200 });

            var authnClient = new TesteableAuthnClient(mockRequestExecutor);

            await authnClient.ResendRecoveryChallengeAsync(recoveryOptions);

            await mockRequestExecutor.Received().PostAsync(
                $"/api/v1/authn/recovery/factors/{recoveryOptions.FactorType}/resend",
                Arg.Any<IEnumerable<KeyValuePair<string, string>>>(),
                "{\"stateToken\":\"foo\"}",
                CancellationToken.None);
        }

        [Fact]
        public async Task ThrowWhenPasswordDoesNotMeetRequirementsInResetPassword()
        {
            #region raw response

            var rawResponse = @"
            {
                ""errorCode"": ""E0000014"",
                ""errorSummary"": ""The password does meet the complexity requirements of the current password policy."",
                ""errorLink"": ""E0000014"",
                ""errorId"": ""oaeS4O7BUp5Roefkk_y4Z2u8Q"",
                ""errorCauses"": [
                {
                    ""errorSummary"": ""Passwords must have at least 8 characters, a lowercase letter, an uppercase letter, a number, no parts of your username""
                }]
            }";

            #endregion

            var mockRequestExecutor = new MockedStringRequestExecutor(rawResponse, 403);
            var authnClient = new TesteableAuthnClient(mockRequestExecutor);

            var resetPasswordOptions = new ResetPasswordOptions()
            {
                NewPassword = "1234",
                StateToken = "00xdqXOE5qDXX8-PBR1bYv8AESqIEinDy3yul01tyh",
            };

            try
            {
                var authResponse = await authnClient.ResetPasswordAsync(resetPasswordOptions);
            }
            catch (OktaApiException apiException)
            {
                apiException.Message.Should()
                    .StartWith(
                        "The password does meet the complexity requirements of the current password policy. (403, E0000014)");
                apiException.ErrorCode.Should().Be("E0000014");
                apiException.ErrorSummary.Should()
                    .Be("The password does meet the complexity requirements of the current password policy.");
                apiException.ErrorLink.Should().Be("E0000014");
                apiException.ErrorId.Should().Be("oaeS4O7BUp5Roefkk_y4Z2u8Q");
            }
        }

        [Fact]
        public async Task ThrowWhenUsernameIsInvalidForForgotPassword()
        {
            #region raw response

            var rawResponse = @"
            {
                ""errorCode"": ""E0000095"",
                ""errorSummary"": ""Recovery not allowed for unknown user."",
                ""errorLink"": ""E0000095"",
                ""errorId"": ""oaeS4O7BUp5Roefkk_y4Z2u8Q"",
                ""errorCauses"": [
                ]
            }";

            #endregion

            var mockRequestExecutor = new MockedStringRequestExecutor(rawResponse, 403);
            var authnClient = new TesteableAuthnClient(mockRequestExecutor);

            var forgotPasswordOptions = new ForgotPasswordOptions()
            {
                UserName = "wrong",
                FactorType = FactorType.Email,
                RelayState = "foo",
            };

            try
            {
                var authResponse = await authnClient.ForgotPasswordAsync(forgotPasswordOptions);
            }
            catch (OktaApiException apiException)
            {
                apiException.Message.Should()
                    .StartWith(
                        "Recovery not allowed for unknown user. (403, E0000095)");
                apiException.ErrorCode.Should().Be("E0000095");
                apiException.ErrorSummary.Should()
                    .Be("Recovery not allowed for unknown user.");
                apiException.ErrorLink.Should().Be("E0000095");
                apiException.ErrorId.Should().Be("oaeS4O7BUp5Roefkk_y4Z2u8Q");
            }
        }

        [Fact]
        public async Task ThrowWhenOldPasswordIsInvalidForChangePassword()
        {
            #region raw response

            var rawResponse = @"
            {
                ""errorCode"": ""E0000014"",
                ""errorSummary"": ""Update of credentials failed"",
                ""errorLink"": ""E0000014"",
                ""errorId"": ""oaeS4O7BUp5Roefkk_y4Z2u8Q"",
                ""errorCauses"": [
                {
                    ""errorSummary"": ""oldPassword: The credentials provided were incorrect.""
                }]
            }";

            #endregion

            var mockRequestExecutor = new MockedStringRequestExecutor(rawResponse, 403);
            var authnClient = new TesteableAuthnClient(mockRequestExecutor);

            var changePasswordOptions = new ChangePasswordOptions()
            {
                NewPassword = "1234",
                OldPassword = "wrong",
                StateToken = "00xdqXOE5qDXX8-PBR1bYv8AESqIEinDy3yul01tyh",
            };

            try
            {
                var authResponse = await authnClient.ChangePasswordAsync(changePasswordOptions);
            }
            catch (OktaApiException apiException)
            {
                apiException.Message.Should()
                    .StartWith(
                        "Update of credentials failed (403, E0000014)");
                apiException.ErrorCode.Should().Be("E0000014");
                apiException.ErrorSummary.Should()
                    .Be("Update of credentials failed");
                apiException.ErrorLink.Should().Be("E0000014");
                apiException.ErrorId.Should().Be("oaeS4O7BUp5Roefkk_y4Z2u8Q");
            }
        }
        
        [Fact]
        public async Task ThrowWhenPassCodeIsInvalidForActivateFactor()
        {
            #region raw response

            var rawResponse = @"
            {
                ""errorCode"": ""E0000068"",
                ""errorSummary"": ""Invalid Passcode/Answer"",
                ""errorLink"": ""E0000068"",
                ""errorId"": ""oaeS4O7BUp5Roefkk_y4Z2u8Q"",
                ""errorCauses"": [
                {
                    ""errorSummary"": ""Your passcode doesn't match our records. Please try again.""
                }]
            }";

            #endregion

            var mockRequestExecutor = new MockedStringRequestExecutor(rawResponse, 403);
            var authnClient = new TesteableAuthnClient(mockRequestExecutor);

            var activateFactorOptions = new ActivateFactorOptions()
            {
                FactorId = "foo",
                PassCode = "wrong",
                StateToken = "00xdqXOE5qDXX8-PBR1bYv8AESqIEinDy3yul01tyh",
            };

            try
            {
                var authResponse = await authnClient.ActivateFactorAsync(activateFactorOptions);
            }
            catch (OktaApiException apiException)
            {
                apiException.Message.Should()
                    .StartWith(
                        "Invalid Passcode/Answer (403, E0000068)");
                apiException.ErrorCode.Should().Be("E0000068");
                apiException.ErrorSummary.Should()
                    .Be("Invalid Passcode/Answer");
                apiException.ErrorLink.Should().Be("E0000068");
                apiException.ErrorId.Should().Be("oaeS4O7BUp5Roefkk_y4Z2u8Q");
            }
        }

        [Fact]
        public async Task ActivateSmsFactor()
        {
            #region raw response

            var rawResponse = @"
            {
                ""expiresAt"": ""2015 - 11 - 03T10: 15:57.000Z"",
                ""status"": ""SUCCESS"",
                ""relayState"": ""/myapp/some/deep/link/i/want/to/return/to"",
                ""sessionToken"": ""00Fpzf4en68pCXTsMjcX8JPMctzN2Wiw4LDOBL_9pe"",
                ""_embedded"": {
                    ""user"": {
                        ""id"": ""00ub0oNGTSWTBKOLGLNR"",
                        ""passwordChanged"": ""2015-09-08T20:14:45.000Z"",
                        ""profile"": {
                            ""login"": ""dade.murphy@example.com"",
                            ""firstName"": ""Dade"",
                            ""lastName"": ""Murphy"",
                            ""locale"": ""en_US"",
                            ""timeZone"": ""America/Los_Angeles""
                        }
                    }
                }
            }";

            #endregion

            var mockRequestExecutor = new MockedStringRequestExecutor(rawResponse);
            var authnClient = new TesteableAuthnClient(mockRequestExecutor);

            var activateFactorOptions = new ActivateFactorOptions()
            {
                StateToken = "00xdqXOE5qDXX8-PBR1bYv8AESqIEinDy3yul01tyh",
                FactorId = "foo",
                PassCode = "bar",
            };

            var authResponse = await authnClient.ActivateFactorAsync(activateFactorOptions);
            authResponse.Should().NotBeNull();
            authResponse.AuthenticationStatus.Should().Be(AuthenticationStatus.Success);
            authResponse.RelayState.Should().Be("/myapp/some/deep/link/i/want/to/return/to");
            authResponse.SessionToken.Should().Be("00Fpzf4en68pCXTsMjcX8JPMctzN2Wiw4LDOBL_9pe");
        }

        [Fact]
        public async Task ActivatePushFactorWithWaitingStatus()
        {
            #region raw response

            var rawResponse = @"
            {
              ""stateToken"": ""007ucIX7PATyn94hsHfOLVaXAmOBkKHWnOOLG43bsb"",
              ""expiresAt"": ""2015-11-03T10:15:57.000Z"",
              ""status"": ""MFA_ENROLL_ACTIVATE"",
              ""relayState"": ""/myapp/some/deep/link/i/want/to/return/to"",
              ""factorResult"": ""WAITING"",
              ""_embedded"": {
                ""user"": {
                    ""id"": ""00ub0oNGTSWTBKOLGLNR"",
                    ""passwordChanged"": ""2015-09-08T20:14:45.000Z"",
                    ""profile"": {
                        ""login"": ""dade.murphy@example.com"",
                        ""firstName"": ""Dade"",
                        ""lastName"": ""Murphy"",
                        ""locale"": ""en_US"",
                        ""timeZone"": ""America/Los_Angeles""}
                },
                ""factor"": {
                    ""id"": ""opfh52xcuft3J4uZc0g3"",
                    ""factorType"": ""push"",
                    ""provider"": ""OKTA"",
                    ""_embedded"": {
                        ""activation"": {
                            ""expiresAt"": ""2015-11-03T10:15:57.000Z"",
                            ""_links"": {
                                ""qrcode"": {
                                ""href"": ""https://dotnet.oktapreview.com/api/v1/users/00ub0oNGTSWTBKOLGLNR/factors/opfh52xcuft3J4uZc0g3/qr/00fukNElRS_Tz6k-CFhg3pH4KO2dj2guhmaapXWbc4"",
                                ""type"": ""image/png""
                            },
                            ""send"": [
                              {
                                ""name"": ""email"",
                                ""href"": ""https://dotnet.oktapreview.com/api/v1/users/00ub0oNGTSWTBKOLGLNR/factors/opfh52xcuft3J4uZc0g3/lifecycle/activate/email"",
                                ""hints"": {
                                  ""allow"": [
                                    ""POST""
                                  ]}
                              },
                          {
                            ""name"": ""sms"",
                            ""href"": ""https://dotnet.oktapreview.com/api/v1/users/00ub0oNGTSWTBKOLGLNR/factors/opfh52xcuft3J4uZc0g3/lifecycle/activate/sms"",
                            ""hints"": {
                              ""allow"": [
                                ""POST""
                              ]
                            }
                          }
                        ]
                      }
                    }
                  }
                }
              },
              ""_links"": {
                ""next"": {
                  ""name"": ""poll"",
                  ""href"": ""https://dotnet.oktapreview.com/api/v1/authn/factors/opfh52xcuft3J4uZc0g3/lifecycle/activate/poll"",
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
                ""prev"": {
                  ""href"": ""https://dotnet.oktapreview.com/api/v1/authn/previous"",
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

            var activatePushFactorOptions = new ActivatePushFactorOptions()
            {
                StateToken = "foo",
                FactorId = "bar",
            };

            var authResponse = await authnClient.ActivateFactorAsync(activatePushFactorOptions);
            authResponse.Should().NotBeNull();
            authResponse.AuthenticationStatus.Should().Be(AuthenticationStatus.MfaEnrollActivate);
            authResponse.RelayState.Should().Be("/myapp/some/deep/link/i/want/to/return/to");
            authResponse.GetProperty<FactorResult>("factorResult").Should().Be(FactorResult.Waiting);
            authResponse.Embedded.GetProperty<Factor>("factor").Should().NotBeNull();
            authResponse.Embedded.GetProperty<Factor>("factor").Id.Should().Be("opfh52xcuft3J4uZc0g3");
            authResponse.Embedded.GetProperty<Factor>("factor").Provider.Should().Be(OktaDefaults.OktaProvider);

            var activation = authResponse.Embedded.GetProperty<Factor>("factor").Embedded.GetProperty<Resource>("activation");
            var activationLinks = activation.GetProperty<Resource>("_links");
            activationLinks.GetProperty<Resource>("qrcode").Should().NotBeNull();
            activationLinks.GetProperty<Resource>("qrcode").GetProperty<string>("href").Should().Be(
                "https://dotnet.oktapreview.com/api/v1/users/00ub0oNGTSWTBKOLGLNR/factors/opfh52xcuft3J4uZc0g3/qr/00fukNElRS_Tz6k-CFhg3pH4KO2dj2guhmaapXWbc4");

            var sendLinks = activationLinks.GetArrayProperty<Resource>("send");
            sendLinks.Should().HaveCount(2);
            sendLinks.First().GetProperty<string>("name").Should().Be("email");
            sendLinks.First().GetProperty<string>("href").Should().Be(
                "https://dotnet.oktapreview.com/api/v1/users/00ub0oNGTSWTBKOLGLNR/factors/opfh52xcuft3J4uZc0g3/lifecycle/activate/email");
            sendLinks.Last().GetProperty<string>("name").Should().Be("sms");
            sendLinks.Last().GetProperty<string>("href").Should().Be(
                "https://dotnet.oktapreview.com/api/v1/users/00ub0oNGTSWTBKOLGLNR/factors/opfh52xcuft3J4uZc0g3/lifecycle/activate/sms");

            authResponse.Links.Should().NotBeNull();
            var next = authResponse.Links.GetProperty<Resource>("next");
            next.GetProperty<string>("name").Should().Be("poll");
            next.GetProperty<string>("href").Should()
                .Be("https://dotnet.oktapreview.com/api/v1/authn/factors/opfh52xcuft3J4uZc0g3/lifecycle/activate/poll");

            var cancel = authResponse.Links.GetProperty<Resource>("cancel");
            cancel.GetProperty<string>("href").Should().Be("https://dotnet.oktapreview.com/api/v1/authn/cancel");

            var prev = authResponse.Links.GetProperty<Resource>("prev");
            prev.GetProperty<string>("href").Should().Be("https://dotnet.oktapreview.com/api/v1/authn/previous");
        }

        [Fact]
        public async Task VerifySmsChallengeOtp()
        {
            #region raw response

            var rawResponse = @"
            {
                ""stateToken"": ""007ucIX7PATyn94hsHfOLVaXAmOBkKHWnOOLG43bsb"",
                ""expiresAt"": ""2015-11-03T10:15:57.000Z"",
                ""status"": ""MFA_CHALLENGE"",
                ""relayState"": ""/myapp/some/deep/link/i/want/to/return/to"",
                ""_embedded"": {
                    ""user"": {
                        ""id"": ""00ub0oNGTSWTBKOLGLNR"",
                        ""passwordChanged"": ""2015-09-08T20:14:45.000Z"",
                        ""profile"": {
                            ""login"": ""dade.murphy@example.com"",
                            ""firstName"": ""Dade"",
                            ""lastName"": ""Murphy"",
                            ""locale"": ""en_US"",
                            ""timeZone"": ""America/Los_Angeles""
                        }
                    },
                    ""factor"": {
                        ""id"": ""sms193zUBEROPBNZKPPE"",
                        ""factorType"": ""sms"",
                        ""provider"": ""OKTA"",
                        ""profile"": {
                            ""phoneNumber"": ""+1 XXX-XXX-1337""
                        }
                    }
                },
                ""_links"": {
                    ""next"": {
                        ""name"": ""verify"",
                        ""href"": ""https://dotnet.oktapreview.com/api/v1/authn/factors/sms193zUBEROPBNZKPPE/verify"",
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
                    ""prev"": {
                        ""href"": ""https://dotnet.oktapreview.com/api/v1/authn/previous"",
                        ""hints"": {
                            ""allow"": [
                            ""POST""
                                ]
                        }
                    },
                    ""resend"": [
                    {
                        ""name"": ""sms"",
                        ""href"": ""https://dotnet.oktapreview.com/api/v1/authn/factors/sms193zUBEROPBNZKPPE/verify/resend"",
                        ""hints"": {
                            ""allow"": [
                            ""POST""
                                ]
                        }
                    }
                    ]
                }
            }";

            #endregion

            var mockRequestExecutor = new MockedStringRequestExecutor(rawResponse);
            var authnClient = new TesteableAuthnClient(mockRequestExecutor);

            var verifySmsOptions = new VerifySmsFactorOptions()
            {
                StateToken = "foo",
                FactorId = "bar",
            };

            var authResponse = await authnClient.VerifyFactorAsync(verifySmsOptions);
            authResponse.Should().NotBeNull();
            authResponse.AuthenticationStatus.Should().Be(AuthenticationStatus.MfaChallenge);
            authResponse.RelayState.Should().Be("/myapp/some/deep/link/i/want/to/return/to");

            authResponse.Embedded.GetProperty<Factor>("factor").Id.Should().Be("sms193zUBEROPBNZKPPE");
            authResponse.Embedded.GetProperty<Factor>("factor").Type.Should().Be(FactorType.Sms);
            authResponse.Embedded.GetProperty<Factor>("factor").Provider.Should().Be(OktaDefaults.OktaProvider);

            authResponse.Links.Should().NotBeNull();
            var next = authResponse.Links.GetProperty<Resource>("next");
            next.GetProperty<string>("name").Should().Be("verify");
            next.GetProperty<string>("href").Should()
                .Be("https://dotnet.oktapreview.com/api/v1/authn/factors/sms193zUBEROPBNZKPPE/verify");

            var cancel = authResponse.Links.GetProperty<Resource>("cancel");
            cancel.GetProperty<string>("href").Should().Be("https://dotnet.oktapreview.com/api/v1/authn/cancel");

            var prev = authResponse.Links.GetProperty<Resource>("prev");
            prev.GetProperty<string>("href").Should().Be("https://dotnet.oktapreview.com/api/v1/authn/previous");

            var resendList = authResponse.Links.GetArrayProperty<Resource>("resend");
            resendList.First().GetProperty<string>("name").Should().Be("sms");
            resendList.First().GetProperty<string>("href").Should()
                .Be("https://dotnet.oktapreview.com/api/v1/authn/factors/sms193zUBEROPBNZKPPE/verify/resend");
        }

        [Fact]
        public async Task NotFailIfAuthenticationStatusIsUnknown()
        {
            #region raw response

            var rawResponse = @"
            {
                ""expiresAt"": ""2015-11-03T10:15:57.000Z"",
                ""status"": ""UNKNOWN_STATUS"",
                ""relayState"": ""/myapp/some/deep/link/i/want/to/return/to"",
                ""sessionToken"": ""00Fpzf4en68pCXTsMjcX8JPMctzN2Wiw4LDOBL_9pe"",
                ""_embedded"": {
                    ""user"": {
                        ""id"": ""00ub0oNGTSWTBKOLGLNR"",
                        ""passwordChanged"": ""2015-09-08T20:14:45.000Z"",
                        ""profile"": {
                            ""login"": ""dade.murphy@example.com"",
                            ""firstName"": ""Dade"",
                            ""lastName"": ""Murphy"",
                            ""locale"": ""en_US"",
                            ""timeZone"": ""America/Los_Angeles""
                        }
                    }
                }
            }";

            #endregion

            var mockRequestExecutor = new MockedStringRequestExecutor(rawResponse);
            var authnClient = new TesteableAuthnClient(mockRequestExecutor);

            var authOptions = new AuthenticateOptions()
            {
                Username = "dade.murphy@example.com",
                Password = "foo",
            };

            var authnResponse = await authnClient.AuthenticateAsync(authOptions);
            authnResponse.Should().NotBeNull();
            authnResponse.SessionToken.Should().Be("00Fpzf4en68pCXTsMjcX8JPMctzN2Wiw4LDOBL_9pe");
            authnResponse.RelayState.Should().Be("/myapp/some/deep/link/i/want/to/return/to");
            authnResponse.AuthenticationStatus.Should().Be(new AuthenticationStatus("UNKNOWN_STATUS"));
        }

        [Fact]
        public async Task EnrollCallFactor()
        {
            #region raw response
            var rawResponse = @"
            {
                ""stateToken"": ""007ucIX7PATyn94hsHfOLVaXAmOBkKHWnOOLG43bsb"",
                ""expiresAt"": ""2015-11-03T10:15:57.000Z"",
                ""status"": ""MFA_ENROLL_ACTIVATE"",
                ""relayState"": ""/myapp/some/deep/link/i/want/to/return/to"",
                ""_embedded"": {
                    ""user"": {
                        ""id"": ""00ub0oNGTSWTBKOLGLNR"",
                        ""passwordChanged"": ""2015-09-08T20:14:45.000Z"",
                        ""profile"": {
                            ""login"": ""dade.murphy@example.com"",
                            ""firstName"": ""Dade"",
                            ""lastName"": ""Murphy"",
                            ""locale"": ""en_US"",
                            ""timeZone"": ""America/Los_Angeles""
                        }
                    },
                    ""factor"": {
                        ""id"": ""clf198rKSEWOSKRIVIFT"",
                        ""factorType"": ""call"",
                        ""provider"": ""OKTA"",
                        ""profile"": {
                            ""phoneNumber"": ""+1 XXX-XXX-1337""
                        }
                    }
                },
                ""_links"": {
                    ""next"": {
                        ""name"": ""activate"",
                        ""href"": ""https://dotnet.oktapreview.com/api/v1/authn/factors/clf198rKSEWOSKRIVIFT/lifecycle/activate"",
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
                    ""prev"": {
                        ""href"": ""https://dotnet.oktapreview.com/api/v1/authn/previous"",
                        ""hints"": {
                            ""allow"": [
                            ""POST""
                                ]
                        }
                    },
                    ""resend"": [
                    {
                        ""name"": ""call"",
                        ""href"": ""https://dotnet.oktapreview.com/api/v1/authn/factors/clf198rKSEWOSKRIVIFT/lifecycle/resend"",
                        ""hints"": {
                            ""allow"": [
                            ""POST""
                                ]
                        }
                    }
                    ]
                }
            }";
            #endregion

            var mockRequestExecutor = new MockedStringRequestExecutor(rawResponse);
            var authnClient = new TesteableAuthnClient(mockRequestExecutor);

            var enrollFactorOptions = new EnrollCallFactorOptions()
            {
                PhoneNumber = "+1-555-415-1337",
                StateToken = "007ucIX7PATyn94hsHfOLVaXAmOBkKHWnOOLG43bsb",
            };

            var authnResponse = await authnClient.EnrollFactorAsync(enrollFactorOptions);
            authnResponse.Should().NotBeNull();
            authnResponse.StateToken.Should().Be("007ucIX7PATyn94hsHfOLVaXAmOBkKHWnOOLG43bsb");
            authnResponse.AuthenticationStatus.Should().Be(AuthenticationStatus.MfaEnrollActivate);
            authnResponse.RelayState.Should().Be("/myapp/some/deep/link/i/want/to/return/to");
            authnResponse.Embedded.GetProperty<Factor>("factor").Should().NotBeNull();
            authnResponse.Embedded.GetProperty<Factor>("factor").Id.Should().Be("clf198rKSEWOSKRIVIFT");
            authnResponse.Embedded.GetProperty<Factor>("factor").Type.Should().Be(FactorType.Call);
            authnResponse.Embedded.GetProperty<Factor>("factor").Provider.Should().Be(OktaDefaults.OktaProvider);
        }

        [Fact]
        public async Task AllowSendingRawRequest()
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

            var authResponse = await authnClient.PostAsync<AuthenticationResponse>(new HttpRequest()
            {
                Uri = "/api/v1/authn/recovery/password",
                Payload = forgotPasswordOptions,
            });

            authResponse.Should().NotBeNull();
            authResponse.AuthenticationStatus.Should().Be(AuthenticationStatus.RecoveryChallenge);
            authResponse.GetProperty<FactorType>("factorType").Should().Be(FactorType.Email);
            authResponse.RelayState.Should().Be("/myapp/some/deep/link/i/want/to/return/to");
        }
    }
}
