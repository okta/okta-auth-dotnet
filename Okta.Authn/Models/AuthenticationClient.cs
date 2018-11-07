using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Okta.Authn.Abstractions;
using Okta.Authn.Models;
using Okta.Authn.POCOs;
using Okta.Sdk.Abstractions;
using Okta.Sdk.Abstractions.Configuration;
using VerifyRecoveryTokenRequest = Okta.Authn.Models.VerifyRecoveryTokenRequest;

namespace Okta.Authn
{
    public class AuthenticationClient : BaseOktaClient, IAuthenticationClient
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseOktaClient"/> class.
        /// </summary>
        /// <param name="apiClientConfiguration">
        /// The client configuration. If <c>null</c>, the library will attempt to load
        /// configuration from an <c>okta.yaml</c> file or environment variables.
        /// </param>
        /// <param name="logger">The logging interface to use, if any.</param>
        public AuthenticationClient(OktaClientConfiguration apiClientConfiguration = null, ILogger logger = null)
        {
            Configuration = GetConfigurationOrDefault(apiClientConfiguration);
            OktaClientConfigurationValidator.Validate(Configuration);

            logger = logger ?? NullLogger.Instance;

            var defaultClient = DefaultHttpClient.Create(
                Configuration.ConnectionTimeout,
                Configuration.Proxy,
                logger);

            var requestExecutor = new DefaultRequestExecutor(Configuration, defaultClient, logger);
            var resourceFactory = new ResourceFactory(this, logger, new ResourceTypeResolverFactory());

            _dataStore = new DefaultDataStore(
                requestExecutor,
                new DefaultSerializer(),
                resourceFactory,
                logger);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseOktaClient"/> class using the specified <see cref="HttpClient"/>.
        /// </summary>
        /// <param name="apiClientConfiguration">
        /// The client configuration. If <c>null</c>, the library will attempt to load
        /// configuration from an <c>okta.yaml</c> file or environment variables.
        /// </param>
        /// <param name="httpClient">The HTTP client to use for requests to the Okta API.</param>
        /// <param name="logger">The logging interface to use, if any.</param>
        public AuthenticationClient(OktaClientConfiguration apiClientConfiguration, HttpClient httpClient, ILogger logger = null)
        {
            Configuration = GetConfigurationOrDefault(apiClientConfiguration);
            OktaClientConfigurationValidator.Validate(Configuration);

            logger = logger ?? NullLogger.Instance;

            var requestExecutor = new DefaultRequestExecutor(Configuration, httpClient, logger);
            var resourceFactory = new ResourceFactory(this, logger, new ResourceTypeResolverFactory());
            _dataStore = new DefaultDataStore(
                requestExecutor,
                new DefaultSerializer(),
                resourceFactory,
                logger);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseOktaClient"/> class.
        /// </summary>
        /// <param name="dataStore">The <see cref="IDataStore">DataStore</see> to use.</param>
        /// <param name="configuration">The client configuration.</param>
        /// <param name="requestContext">The request context, if any.</param>
        /// <remarks>This overload is used internally to create cheap copies of an existing client.</remarks>
        protected AuthenticationClient(IDataStore dataStore, OktaClientConfiguration configuration, RequestContext requestContext)
            : base(dataStore, configuration, requestContext)
        {
        }
        
        public async Task<AuthenticationResponse> AuthenticateAsync(AuthenticateOptions authenticateOptions,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var authenticationRequest = new AuthenticationRequest()
            {
                Username = authenticateOptions.Username,
                Password = authenticateOptions.Password,
                Audience = authenticateOptions.Audience,
                RelayState = authenticateOptions.RelayState,
                Options = new AuthenticationRequestOptions()
                {
                    MultiOptionalFactorEnroll = authenticateOptions.MultiOptionalFactorEnroll,
                    WarnBeforePasswordExpired = authenticateOptions.WarnBeforePasswordExpired,
                },
            };
            
            return await PostAsync<AuthenticationResponse>(new HttpRequest
            {
                Uri = "/api/v1/authn",
                Payload = authenticationRequest,
            }, cancellationToken).ConfigureAwait(false);
        }

        public async Task<AuthenticationResponse> ChangePasswordAsync(ChangePasswordOptions passwordOptions, CancellationToken cancellationToken = default(CancellationToken))
        {
            var changePasswordRequest = new ChangePasswordRequest()
            {
                StateToken = passwordOptions.StateToken,
                NewPassword = passwordOptions.NewPassword,
                OldPassword = passwordOptions.OldPassword,
            };

            return await PostAsync<AuthenticationResponse>(new HttpRequest
            {
                Uri = "/api/v1/authn/credentials/change_password",
                Payload = changePasswordRequest,
            }, cancellationToken).ConfigureAwait(false);
        }

        public async Task<AuthenticationResponse> ForgotPasswordAsync(ForgotPasswordOptions forgotPasswordOptions, CancellationToken cancellationToken = default(CancellationToken))
        {
            var forgotPasswordRequest = new ForgotPasswordRequest()
            {
                Username = forgotPasswordOptions.UserName,
                RelayState = forgotPasswordOptions.RelayState,
                FactorType = forgotPasswordOptions.FactorType,
            };

            return await PostAsync<AuthenticationResponse>(new HttpRequest
            {
                Uri = "/api/v1/authn/recovery/password",
                Payload = forgotPasswordRequest,
            }, cancellationToken).ConfigureAwait(false);
        }

        public async Task<AuthenticationResponse> ResetPasswordAsync(ResetPasswordOptions resetPasswordOptions, CancellationToken cancellationToken = default(CancellationToken))
        {
            var resetPasswordRequest = new ResetPasswordRequest()
            {
                StateToken = resetPasswordOptions.StateToken,
                NewPassword = resetPasswordOptions.NewPassword,
            };

            return await PostAsync<AuthenticationResponse>(new HttpRequest
            {
                Uri = "/api/v1/authn/credentials/reset_password",
                Payload = resetPasswordRequest,
            }, cancellationToken).ConfigureAwait(false);
        }

        public async Task<AuthenticationResponse> EnrollFactorAsync(EnrollSMSFactorOptions factorOptions, CancellationToken cancellationToken = default(CancellationToken))
        {
            var profile = new Resource();
            profile.SetProperty("phoneNumber", factorOptions.PhoneNumber);
            profile.SetProperty("phoneExtension", factorOptions.PhoneExtension);

            var enrollSmsFactor = new EnrollFactorRequest()
            {
                StateToken = factorOptions.StateToken,
                FactorType = FactorType.Sms,
                Provider = factorOptions.Provider,
                Profile = profile,
            };

            return await EnrollFactorAsync(enrollSmsFactor, cancellationToken);
        }

        public async Task<AuthenticationResponse> EnrollFactorAsync(EnrollSecurityQuestionFactorOptions factorOptions, CancellationToken cancellationToken = default(CancellationToken))
        {
            var profile = new Resource();
            profile.SetProperty("question", factorOptions.Question);
            profile.SetProperty("questionText", factorOptions.QuestionText);
            profile.SetProperty("answer", factorOptions.Answer);

            var enrollQuestionFactor = new EnrollFactorRequest()
            {
                StateToken = factorOptions.StateToken,
                FactorType = FactorType.Question,
                Provider = factorOptions.Provider,
                Profile = profile,
            };

            return await EnrollFactorAsync(enrollQuestionFactor, cancellationToken);
        }

        public async Task<AuthenticationResponse> EnrollFactorAsync(EnrollCallFactorOptions factorOptions, CancellationToken cancellationToken = default(CancellationToken))
        {
            var profile = new Resource();
            profile.SetProperty("phoneNumber", factorOptions.PhoneNumber);
            profile.SetProperty("phoneExtension", factorOptions.PhoneExtension);

            var enrollCallFactor = new EnrollFactorRequest()
            {
                StateToken = factorOptions.StateToken,
                FactorType = FactorType.Call,
                Provider = factorOptions.Provider,
                Profile = profile,
            };

            return await EnrollFactorAsync(enrollCallFactor, cancellationToken);
        }

        public async Task<AuthenticationResponse> EnrollFactorAsync(EnrollPushFactorOptions factorOptions, CancellationToken cancellationToken = default(CancellationToken))
        {
            var enrollPushFactor = new EnrollFactorRequest()
            {
                StateToken = factorOptions.StateToken,
                FactorType = FactorType.Push,
                Provider = factorOptions.Provider,
            };

            return await EnrollFactorAsync(enrollPushFactor, cancellationToken);
        }

        public async Task<AuthenticationResponse> EnrollFactorAsync(EnrollRsaFactorOptions factorOptions, CancellationToken cancellationToken = default(CancellationToken))
        {
            var profile = new Resource();
            profile.SetProperty("credentialId", factorOptions.CredentialId);

            var enrollRsaFactor = new EnrollFactorRequest()
            {
                StateToken = factorOptions.StateToken,
                FactorType = FactorType.TokenHardware,
                Provider = factorOptions.Provider,
                PassCode = factorOptions.PassCode,
                Profile = profile,
            };

            return await EnrollFactorAsync(enrollRsaFactor, cancellationToken);
        }

        public async Task<AuthenticationResponse> EnrollFactorAsync(EnrollSymantecFactorOptions factorOptions,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var profile = new Resource();
            profile.SetProperty("credentialId", factorOptions.CredentialId);

            var enrollSymantecFactor = new EnrollFactorRequest()
            {
                StateToken = factorOptions.StateToken,
                FactorType = FactorType.Token,
                Provider = factorOptions.Provider,
                PassCode = factorOptions.PassCode,
                NextPassCode = factorOptions.NextPassCode,
                Profile = profile,
            };

            return await EnrollFactorAsync(enrollSymantecFactor, cancellationToken);
        }

        public async Task<AuthenticationResponse> EnrollFactorAsync(EnrollYubiKeyFactorOptions factorOptions, CancellationToken cancellationToken = default(CancellationToken))
        {
            var enrollYubiKeyFactor = new EnrollFactorRequest()
            {
                FactorType = FactorType.TokenHardware,
                Provider = factorOptions.Provider,
                PassCode = factorOptions.PassCode,
            };

            return await EnrollFactorAsync(enrollYubiKeyFactor, cancellationToken);
        }

        public async Task<AuthenticationResponse> EnrollFactorAsync(EnrollDuoFactorOptions factorOptions, CancellationToken cancellationToken = default(CancellationToken))
        {
            var enrollDuoFactor = new EnrollFactorRequest()
            {
                StateToken = factorOptions.StateToken,
                FactorType = FactorType.Web,
                Provider = factorOptions.Provider,
            };

            return await EnrollFactorAsync(enrollDuoFactor, cancellationToken);
        }

        public async Task<AuthenticationResponse> EnrollFactorAsync(EnrollU2fFactorOptions factorOptions, CancellationToken cancellationToken = default(CancellationToken))
        {
            var enrollU2Factor = new EnrollFactorRequest()
            {
                StateToken = factorOptions.StateToken,
                FactorType = FactorType.U2f,
                Provider = factorOptions.Provider,
            };

            return await EnrollFactorAsync(enrollU2Factor, cancellationToken);
        }

        public async Task<AuthenticationResponse> Enroll(EnrollTotpFactorOptions factorOptions, CancellationToken cancellationToken = default(CancellationToken))
        {
            var profile = new Resource();
            profile.SetProperty("credentialId", factorOptions.CredentialId);

            var enrollTotpFactor = new EnrollFactorRequest()
            {
                StateToken = factorOptions.StateToken,
                FactorType = FactorType.TokenSoftwareTotp,
                Provider = factorOptions.Provider,
                Profile = profile,
            };

            return await EnrollFactorAsync(enrollTotpFactor, cancellationToken);
        }

        public async Task<AuthenticationResponse> EnrollFactorAsync(EnrollFactorRequest request, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await PostAsync<AuthenticationResponse>(new HttpRequest
            {
                Uri = "/api/v1/authn/factors",
                Payload = request,
            }, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Activates factor. For U2F use <see cref="ActivateU2fFactor"/>
        /// </summary>
        /// <param name="activateFactorOptions">The Activate Factor options</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The Authentication Response</returns>
        public async Task<AuthenticationResponse> ActivateFactorAsync(ActivateFactorOptions activateFactorOptions, CancellationToken cancellationToken = default(CancellationToken))
        {
            var activateFactorRequest = new ActivateFactorRequest()
            {
                FactorType = activateFactorOptions.FactorType,
                StateToken = activateFactorOptions.StateToken,
                PassCode = activateFactorOptions.PassCode,
            };

            return await ActivateFactorAsync(activateFactorRequest, activateFactorOptions.FactorId, cancellationToken);
        }

        /// <summary>
        /// Activates U2F ActivateU2f factor
        /// </summary>
        /// <param name="activateFactorOptions">The Activate Factor options</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The Authentication Response</returns>
        public async Task<AuthenticationResponse> ActivateU2fFactorAsync(ActivateU2fFactorOptions activateFactorOptions, CancellationToken cancellationToken = default(CancellationToken))
        {
            var activateFactorRequest = new ActivateU2FFactorRequest()
            {
                StateToken = activateFactorOptions.StateToken,
                ClientData = activateFactorOptions.ClientData,
                RegistrationData = activateFactorOptions.RegistrationData,
            };

            return await ActivateFactorAsync(activateFactorRequest, activateFactorOptions.FactorId, cancellationToken);
        }

        public async Task<AuthenticationResponse> ActivateFactorAsync(ActivateFactorRequest activateFactorRequest, string factorId, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await PostAsync<AuthenticationResponse>(new HttpRequest
            {
                Uri = $"/api/v1/authn/factors/{factorId}/lifecycle/activate",
                Payload = activateFactorRequest,
            }, cancellationToken).ConfigureAwait(false);
        }

        public async Task<AuthenticationResponse> ActivateFactorAsync(ActivateU2FFactorRequest activateFactorRequest, string factorId, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await PostAsync<AuthenticationResponse>(new HttpRequest
            {
                Uri = $"/api/v1/authn/factors/{factorId}/lifecycle/activate",
                Payload = activateFactorRequest,
            }, cancellationToken).ConfigureAwait(false);
        }

        public async Task<AuthenticationResponse> AnswerRecoveryQuestionAsync(AnswerRecoveryQuestionOptions answerOptions, CancellationToken cancellationToken = default(CancellationToken))
        {
            var answerRecoveryRequest = new AnswerRecoveryQuestionRequest()
            {
                StateToken = answerOptions.StateToken,
                Answer = answerOptions.Answer,
            };

            return await PostAsync<AuthenticationResponse>(new HttpRequest
            {
                Uri = $"/api/v1/authn/recovery/answer",
                Payload = answerRecoveryRequest,
            }, cancellationToken).ConfigureAwait(false);
        }

        public async Task<AuthenticationResponse> VerifyRecoveryTokenAsync(VerifyRecoveryTokenOptions verifyRecoveryTokenOptions, CancellationToken cancellationToken = default(CancellationToken))
        {
            var recoveryTokenRequest = new VerifyRecoveryTokenRequest()
            {
                RecoveryToken = verifyRecoveryTokenOptions.RecoveryToken,
            };

            return await PostAsync<AuthenticationResponse>(new HttpRequest
            {
                Uri = $"/api/v1/authn/recovery/token",
                Payload = recoveryTokenRequest,
            }, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Verifies Recovery for a factor (SMS/Call)
        /// </summary>
        /// <param name="verifyRecoveryFactorOptions">The Verify Recovery Factor request options</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The Authentication Response</returns>
        public async Task<AuthenticationResponse> VerifyRecoveryFactorAsync(VerifyRecoveryFactorOptions verifyRecoveryFactorOptions, CancellationToken cancellationToken = default(CancellationToken))
        {
            var verifyRecoveryFactorRequest = new VerifyFactorRecoveryRequest()
            {
                StateToken = verifyRecoveryFactorOptions.StateToken,
                PassCode = verifyRecoveryFactorOptions.PassCode,
            };

            return await PostAsync<AuthenticationResponse>(new HttpRequest
            {
                Uri = $"/api/v1/authn/recovery/factors/{verifyRecoveryFactorOptions.FactorType.ToString().ToLower()}/verify",
                Payload = verifyRecoveryFactorRequest,
            }, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Re sends recovery challenge for a factor (SMS/Call)
        /// </summary>
        /// <param name="resendRecoveryChallengeOptions">The Recovery Request options</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The Authentication Response</returns>
        public async Task<AuthenticationResponse> ResendRecoveryChallengeAsync(ResendRecoveryChallengeOptions resendRecoveryChallengeOptions, CancellationToken cancellationToken = default(CancellationToken))
        {
            var recoveryChallengeRequest = new ResendRecoveryChallengeRequest()
            {
                StateToken = resendRecoveryChallengeOptions.StateToken,
            };

            return await PostAsync<AuthenticationResponse>(new HttpRequest
            {
                Uri = $"/api/v1/authn/recovery/factors/{resendRecoveryChallengeOptions.FactorType.ToString().ToLower()}/resend",
                Payload = recoveryChallengeRequest,
            }, cancellationToken).ConfigureAwait(false);
        }

        public async Task<AuthenticationResponse> UnlockAccountAsync(UnlockAccountOptions unlockAccountOptions, CancellationToken cancellationToken = default(CancellationToken))
        {
            var unlockAccountRequest = new UnlockAccountRequest()
            {
                FactorType = unlockAccountOptions.FactorType,
                RelayState = unlockAccountOptions.RelayState,
                Username = unlockAccountOptions.UserName,
            };

            return await PostAsync<AuthenticationResponse>(new HttpRequest
            {
                Uri = $"/api/v1/authn/recovery/unlock",
                Payload = unlockAccountRequest,
            }, cancellationToken).ConfigureAwait(false);
        }
    }
}
