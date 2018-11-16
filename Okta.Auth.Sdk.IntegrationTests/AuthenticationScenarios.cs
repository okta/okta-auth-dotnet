using System;
using System.Threading.Tasks;
using FluentAssertions;
using Okta.Auth.Sdk.IntegrationTests.Internal;
using Okta.Sdk;
using Xunit;
using OktaApiException = Okta.Sdk.Abstractions.OktaApiException;

namespace Okta.Auth.Sdk.IntegrationTests
{
    public class AuthenticationScenarios
    {
        [Fact]
        public async Task AuthenticateUserWithExpiredPassword()
        {
            var oktaClient = new OktaClient();
            var guid = Guid.NewGuid();

            var createdUser = await oktaClient.Users.CreateUserAsync(new CreateUserWithPasswordOptions
            {
                Profile = new UserProfile
                {
                    FirstName = "John",
                    LastName = "Test",
                    Email = $"john-expired-pass-dotnet-authn-{guid}@example.com",
                    Login = $"john-expired-pass-dotnet-authn-{guid}@example.com",
                },
                Password = "Okta1234!",
                Activate = true,
            });

            await createdUser.ExpirePasswordAsync();

            var authClient = TestAuthenticationClient.Create();

            var authnOptions = new AuthenticateOptions()
            {
                Username = $"john-expired-pass-dotnet-authn-{guid}@example.com",
                Password = "Okta1234!",
                MultiOptionalFactorEnroll = true,
                WarnBeforePasswordExpired = true,
            };

            try
            {
                var authnResponse = await authClient.AuthenticateAsync(authnOptions);

                authnResponse.Should().NotBeNull();
                authnResponse.StateToken.Should().NotBeNullOrEmpty();
                authnResponse.AuthenticationStatus.Should().Be(AuthenticationStatus.PasswordExpired);
                authnResponse.Links.Should().NotBeNull();
            }
            finally
            {
                await createdUser.DeactivateAsync();
                await createdUser.DeactivateOrDeleteAsync();
            }
        }

        [Fact]
        public async Task AuthenticateUser()
        {
            var oktaClient = new OktaClient();
            var guid = Guid.NewGuid();
            
            var createdUser = await oktaClient.Users.CreateUserAsync(new CreateUserWithPasswordOptions
            {
                Profile = new UserProfile
                {
                    FirstName = "John",
                    LastName = "Test",
                    Email = $"john-authenticate-dotnet-authn-{guid}@example.com",
                    Login = $"john-authenticate-dotnet-authn-{guid}@example.com",
                },
                Password = "Okta4321!",
                Activate = true,
            });

            var authClient = TestAuthenticationClient.Create();

            var authnOptions = new AuthenticateOptions()
            {
                Username = $"john-authenticate-dotnet-authn-{guid}@example.com",
                Password = "Okta4321!",
                MultiOptionalFactorEnroll = true,
                WarnBeforePasswordExpired = true,
            };

            try
            { 
                var authnResponse = await authClient.AuthenticateAsync(authnOptions);

                authnResponse.Should().NotBeNull();
                authnResponse.SessionToken.Should().NotBeNullOrEmpty();
                authnResponse.AuthenticationStatus.Should().Be(AuthenticationStatus.Success);
            }
            finally
            {
                await createdUser.DeactivateAsync();
                await createdUser.DeactivateOrDeleteAsync();
            }
        }

        [Fact]
        public async Task AuthenticateUserWithInvalidCredentials()
        {
            var oktaClient = new OktaClient();
            var guid = Guid.NewGuid();

            var createdUser = await oktaClient.Users.CreateUserAsync(new CreateUserWithPasswordOptions
            {
                Profile = new UserProfile
                {
                    FirstName = "John",
                    LastName = "Test",
                    Email = $"john-invalid-cred-dotnet-authn-{guid}@example.com",
                    Login = $"john-invalid-cred-dotnet-authn-{guid}@example.com",
                },
                Password = "Okta4321!",
                Activate = true,
            });

            var authClient = TestAuthenticationClient.Create();

            var authnOptions = new AuthenticateOptions()
            {
                Username = $"john-invalid-cred-dotnet-authn-{guid}@example.com",
                Password = "Okta4321!!!",
                MultiOptionalFactorEnroll = true,
                WarnBeforePasswordExpired = true,
            };

            try
            {
                Func<Task> act = async () => await authClient.AuthenticateAsync(authnOptions);
                act.Should().Throw<OktaApiException>();
            }
            finally
            {
                await createdUser.DeactivateAsync();
                await createdUser.DeactivateOrDeleteAsync();
            }
        }

        [Fact]
        public async Task AuthenticateUserWithPendingEnroll()
        {
            var client = TestAuthenticationClient.Create();

            var authnOptions = new AuthenticateOptions()
            {
                Username = "tom-enroll@test.com",
                Password = "Okta4321!",
                MultiOptionalFactorEnroll = true,
                WarnBeforePasswordExpired = true,
            };

            var authnResponse = await client.AuthenticateAsync(authnOptions);

            authnResponse.Should().NotBeNull();
            authnResponse.Factors.Should().NotBeNull();
            authnResponse.Factors.Should().HaveCountGreaterThan(0);
            authnResponse.AuthenticationStatus.Should().Be(AuthenticationStatus.MfaEnroll);
        }

        [Fact]
        public async Task ChangePassword()
        {
            var oktaClient = new OktaClient();
            var guid = Guid.NewGuid();

            var createdUser = await oktaClient.Users.CreateUserAsync(new CreateUserWithPasswordOptions
            {
                Profile = new UserProfile
                {
                    FirstName = "John",
                    LastName = "Test",
                    Email = $"john-change-pass-dotnet-authn-{guid}@example.com",
                    Login = $"john-change-pass-dotnet-authn-{guid}@example.com",
                },
                Password = "Okta4321!",
                Activate = true,
            });

            var authClient = TestAuthenticationClient.Create();

            var authnOptions = new AuthenticateOptions()
            {
                Username = $"john-change-pass-dotnet-authn-{guid}@example.com",
                Password = "Okta4321!",
                MultiOptionalFactorEnroll = true,
                WarnBeforePasswordExpired = true,
            };

            try
            {
                var authnResponse = await authClient.AuthenticateAsync(authnOptions);
                authnResponse.Should().NotBeNull();
                authnResponse.AuthenticationStatus.Should().Be(AuthenticationStatus.Success);

                await createdUser.ExpirePasswordAsync();

                var authnResponse1 = await authClient.AuthenticateAsync(authnOptions);
                authnResponse1.Should().NotBeNull();
                authnResponse1.AuthenticationStatus.Should().Be(AuthenticationStatus.PasswordExpired);

                var changePasswordOptions = new ChangePasswordOptions()
                {
                    StateToken = authnResponse1.StateToken,
                    OldPassword = "Okta4321!",
                    NewPassword = "Okta1234!",
                };

                var authnResponse2 = await authClient.ChangePasswordAsync(changePasswordOptions);
                authnResponse2.Should().NotBeNull();
                authnResponse2.AuthenticationStatus.Should().Be(AuthenticationStatus.Success);
            }
            finally
            {
                await createdUser.DeactivateAsync();
                await createdUser.DeactivateOrDeleteAsync();
            }
        }
        
        [Fact]
        public async Task EnrollSMSFactor()
        {
            var oktaClient = new OktaClient();
            var guid = Guid.NewGuid();
            // MFA Group w/ SMS policy
            var groupId = "{groupId}";
            var createdUser = await oktaClient.Users.CreateUserAsync(new CreateUserWithPasswordOptions
            {
                Profile = new UserProfile
                {
                    FirstName = "John",
                    LastName = "Enroll-SMS",
                    Email = $"john-enroll-sms-dotnet-authn-{guid}@example.com",
                    Login = $"john-enroll-sms-dotnet-authn-{guid}@example.com",
                },
                Password = "Okta4321!",
                Activate = true,
            });

            await oktaClient.Groups.AddUserToGroupAsync(groupId, createdUser.Id);

            var authnClient = TestAuthenticationClient.Create();
            try
            {
                var authnOptions = new AuthenticateOptions()
                {
                    Username = $"john-enroll-sms-dotnet-authn-{guid}@example.com",
                    Password = "Okta4321!",
                    MultiOptionalFactorEnroll = true,
                    WarnBeforePasswordExpired = true,
                };

                var authnResponse = await authnClient.AuthenticateAsync(authnOptions);

                authnResponse.Should().NotBeNull();
                authnResponse.Factors.Should().NotBeNull();
                authnResponse.Factors.Should().HaveCountGreaterThan(0);
                authnResponse.AuthenticationStatus.Should().Be(AuthenticationStatus.MfaEnroll);

                var enrollOptions = new EnrollSMSFactorOptions()
                {
                    PhoneNumber = "+1 415 555 5555",
                    StateToken = authnResponse.StateToken,
                };

                authnResponse = await authnClient.EnrollFactorAsync(enrollOptions);
                authnResponse.Should().NotBeNull();
                authnResponse.AuthenticationStatus.Should().Be(AuthenticationStatus.MfaEnrollActivate);
                authnResponse.Factor.Should().NotBeNull();
                authnResponse.Factor.Profile.GetProperty<string>("phoneNumber").Should().NotBeNullOrEmpty();

            }
            finally
            {
                await createdUser.DeactivateAsync();
                await createdUser.DeactivateOrDeleteAsync();
            }
        }

        [Fact]
        public async Task EnrollSecurityQuestionFactor()
        {
            var oktaClient = new OktaClient();
            var guid = Guid.NewGuid();
            // MFA Group w/ Security Question policy
            var groupId = "{groupId}";
            var createdUser = await oktaClient.Users.CreateUserAsync(new CreateUserWithPasswordOptions
            {
                Profile = new UserProfile
                {
                    FirstName = "John",
                    LastName = "Enroll-Security-Question",
                    Email = $"john-enroll-question-dotnet-authn-{guid}@example.com",
                    Login = $"john-enroll-question-dotnet-authn-{guid}@example.com",
                },
                Password = "Okta4321!",
                Activate = true,
            });

            await oktaClient.Groups.AddUserToGroupAsync(groupId, createdUser.Id);

            var authnClient = TestAuthenticationClient.Create();
            try
            {
                var authnOptions = new AuthenticateOptions()
                {
                    Username = $"john-enroll-question-dotnet-authn-{guid}@example.com",
                    Password = "Okta4321!",
                    MultiOptionalFactorEnroll = true,
                    WarnBeforePasswordExpired = true,
                };

                var authnResponse = await authnClient.AuthenticateAsync(authnOptions);

                authnResponse.Should().NotBeNull();
                authnResponse.Factors.Should().NotBeNull();
                authnResponse.Factors.Should().HaveCountGreaterThan(0);
                authnResponse.AuthenticationStatus.Should().Be(AuthenticationStatus.MfaEnroll);

                var enrollOptions = new EnrollSecurityQuestionFactorOptions()
                {
                    Question = "name_of_first_plush_toy",
                    Answer = "blah",
                    StateToken = authnResponse.StateToken,
                };

                authnResponse = await authnClient.EnrollFactorAsync(enrollOptions);
                authnResponse.Should().NotBeNull();
                authnResponse.AuthenticationStatus.Should().Be(AuthenticationStatus.MfaEnroll);

                // Authenticate after enroll
                authnResponse = await authnClient.AuthenticateAsync(authnOptions);
                authnResponse.Should().NotBeNull();
                authnResponse.AuthenticationStatus.Should().Be(AuthenticationStatus.Success);
            }
            finally
            {
                await createdUser.DeactivateAsync();
                await createdUser.DeactivateOrDeleteAsync();
            }
        }
    }
}
