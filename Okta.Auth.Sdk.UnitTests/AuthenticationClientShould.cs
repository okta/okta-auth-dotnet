using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
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
            authResponse.FactorType.Should().Be(FactorType.Email);
            authResponse.RelayState.Should().Be("/myapp/some/deep/link/i/want/to/return/to");
            authResponse.RecoveryType.Should().Be(RecoveryType.Password);
            authResponse.FactorResult.Should().Be(FactorResult.Waiting);

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
            authResponse.RecoveryType.Should().Be(RecoveryType.Password);
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
        public async Task ActivateSMSFactor()
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
                FactorType = FactorType.Sms,
                FactorId = "foo",
                PassCode = "foo",
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
            authResponse.FactorResult.Should().Be(FactorResult.Waiting);
            authResponse.Factor.Should().NotBeNull();
            authResponse.Factor.Id.Should().Be("opfh52xcuft3J4uZc0g3");
            authResponse.Factor.Provider.Should().Be(OktaDefaults.OktaProvider);

            var activation = authResponse.Factor.Embedded.GetProperty<Resource>("activation");
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

            var authResponse = await authnClient.VerifySmsFactorAsync(verifySmsOptions);
            authResponse.Should().NotBeNull();
            authResponse.AuthenticationStatus.Should().Be(AuthenticationStatus.MfaChallenge);
            authResponse.RelayState.Should().Be("/myapp/some/deep/link/i/want/to/return/to");

            authResponse.Factor.Id.Should().Be("sms193zUBEROPBNZKPPE");
            authResponse.Factor.Type.Should().Be(FactorType.Sms);
            authResponse.Factor.Provider.Should().Be(OktaDefaults.OktaProvider);

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
            authnResponse.Factor.Should().NotBeNull();
            authnResponse.Factor.Id.Should().Be("clf198rKSEWOSKRIVIFT");
            authnResponse.Factor.Type.Should().Be(FactorType.Call);
            authnResponse.Factor.Provider.Should().Be(OktaDefaults.OktaProvider);
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
            authResponse.FactorType.Should().Be(FactorType.Email);
            authResponse.RelayState.Should().Be("/myapp/some/deep/link/i/want/to/return/to");
        }
    }
}
