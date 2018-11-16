// <copyright file="AuthenticationClient.cs" company="Okta, Inc">
// Copyright (c) 2018 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

using System.Net.Http;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Okta.Auth.Sdk.Models;
using Okta.Sdk.Abstractions;
using Okta.Sdk.Abstractions.Configuration;

namespace Okta.Auth.Sdk
{
    public class AuthenticationClient : BaseOktaClient, IAuthenticationClient
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationClient"/> class.
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
            var userAgentBuilder = new UserAgentBuilder("okta-auth-dotnet", typeof(AuthenticationClient).GetTypeInfo().Assembly.GetName().Version);

            _dataStore = new DefaultDataStore(requestExecutor, new DefaultSerializer(), resourceFactory, logger, userAgentBuilder);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationClient"/> class using the specified <see cref="HttpClient"/>.
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
            var userAgentBuilder = new UserAgentBuilder("okta-auth-dotnet", typeof(BaseOktaClient).GetTypeInfo().Assembly.GetName().Version);

            _dataStore = new DefaultDataStore(
                requestExecutor,
                new DefaultSerializer(),
                resourceFactory,
                logger,
                userAgentBuilder);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationClient"/> class.
        /// </summary>
        /// <param name="dataStore">The <see cref="IDataStore">DataStore</see> to use.</param>
        /// <param name="configuration">The client configuration.</param>
        /// <param name="requestContext">The request context, if any.</param>
        /// <remarks>This overload is used internally to create cheap copies of an existing client.</remarks>
        protected AuthenticationClient(IDataStore dataStore, OktaClientConfiguration configuration, RequestContext requestContext)
            : base(dataStore, configuration, requestContext)
        {
        }

        public async Task<IAuthenticationResponse> AuthenticateAsync(AuthenticateOptions authenticateOptions, CancellationToken cancellationToken = default(CancellationToken))
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

            return await PostAsync<AuthenticationResponse>(
                new HttpRequest
                {
                    Uri = "/api/v1/authn",
                    Payload = authenticationRequest,
                }, cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<IAuthenticationResponse> AuthenticateAsync(AuthenticateWithActivationTokenOptions authenticateOptions, CancellationToken cancellationToken = default(CancellationToken))
        {
            var authenticationRequest = new AuthenticationRequest()
            {
                ActivationToken = authenticateOptions.ActivationToken,
            };

            return await PostAsync<AuthenticationResponse>(
                new HttpRequest
                {
                    Uri = "/api/v1/authn",
                    Payload = authenticationRequest,
                }, cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<IAuthenticationResponse> ChangePasswordAsync(ChangePasswordOptions passwordOptions, CancellationToken cancellationToken = default(CancellationToken))
        {
            var changePasswordRequest = new ChangePasswordRequest()
            {
                StateToken = passwordOptions.StateToken,
                NewPassword = passwordOptions.NewPassword,
                OldPassword = passwordOptions.OldPassword,
            };

            return await PostAsync<AuthenticationResponse>(
                new HttpRequest
                {
                    Uri = "/api/v1/authn/credentials/change_password",
                    Payload = changePasswordRequest,
                }, cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<IAuthenticationResponse> ForgotPasswordAsync(ForgotPasswordOptions forgotPasswordOptions, CancellationToken cancellationToken = default(CancellationToken))
        {
            var forgotPasswordRequest = new ForgotPasswordRequest()
            {
                Username = forgotPasswordOptions.UserName,
                RelayState = forgotPasswordOptions.RelayState,
                FactorType = forgotPasswordOptions.FactorType,
            };

            return await PostAsync<AuthenticationResponse>(
                new HttpRequest
                {
                    Uri = "/api/v1/authn/recovery/password",
                    Payload = forgotPasswordRequest,
                }, cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<IAuthenticationResponse> ResetPasswordAsync(ResetPasswordOptions resetPasswordOptions, CancellationToken cancellationToken = default(CancellationToken))
        {
            var resetPasswordRequest = new ResetPasswordRequest()
            {
                StateToken = resetPasswordOptions.StateToken,
                NewPassword = resetPasswordOptions.NewPassword,
            };

            return await PostAsync<AuthenticationResponse>(
                new HttpRequest
                {
                    Uri = "/api/v1/authn/credentials/reset_password",
                    Payload = resetPasswordRequest,
                }, cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<IAuthenticationResponse> EnrollFactorAsync(EnrollSMSFactorOptions factorOptions, CancellationToken cancellationToken = default(CancellationToken))
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

        /// <inheritdoc/>
        public async Task<IAuthenticationResponse> ResendSmsEnrollFactorAsync(EnrollSMSFactorOptions factorOptions, CancellationToken cancellationToken = default(CancellationToken))
        {
            var profile = new Resource();
            profile.SetProperty("phoneNumber", factorOptions.PhoneNumber);
            profile.SetProperty("phoneExtension", factorOptions.PhoneExtension);

            var enrollSmsFactorRequest = new EnrollFactorRequest()
            {
                StateToken = factorOptions.StateToken,
                FactorType = FactorType.Sms,
                Provider = factorOptions.Provider,
                Profile = profile,
            };

            return await PostAsync<AuthenticationResponse>(
                new HttpRequest
                {
                    Uri = $"/api/v1/authn/factors/{factorOptions.FactorId}/lifecycle/resend",
                    Payload = enrollSmsFactorRequest,
                }, cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<IAuthenticationResponse> EnrollFactorAsync(EnrollSecurityQuestionFactorOptions factorOptions, CancellationToken cancellationToken = default(CancellationToken))
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

        /// <inheritdoc/>
        public async Task<IAuthenticationResponse> EnrollFactorAsync(EnrollCallFactorOptions factorOptions, CancellationToken cancellationToken = default(CancellationToken))
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

        /// <inheritdoc/>
        public async Task<IAuthenticationResponse> ResendCallEnrollFactorAsync(EnrollCallFactorOptions factorOptions, CancellationToken cancellationToken = default(CancellationToken))
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

            return await PostAsync<AuthenticationResponse>(
                new HttpRequest
                {
                    Uri = $"/api/v1/authn/factors/{factorOptions.FactorId}/lifecycle/resend",
                    Payload = enrollCallFactor,
                }, cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<IAuthenticationResponse> EnrollFactorAsync(EnrollPushFactorOptions factorOptions, CancellationToken cancellationToken = default(CancellationToken))
        {
            var enrollPushFactor = new EnrollFactorRequest()
            {
                StateToken = factorOptions.StateToken,
                FactorType = FactorType.Push,
                Provider = factorOptions.Provider,
            };

            return await EnrollFactorAsync(enrollPushFactor, cancellationToken);
        }

        /// <inheritdoc/>
        public async Task<IAuthenticationResponse> EnrollFactorAsync(EnrollRsaFactorOptions factorOptions, CancellationToken cancellationToken = default(CancellationToken))
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

        /// <inheritdoc/>
        public async Task<IAuthenticationResponse> EnrollFactorAsync(EnrollSymantecFactorOptions factorOptions, CancellationToken cancellationToken = default(CancellationToken))
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

        /// <inheritdoc/>
        public async Task<IAuthenticationResponse> EnrollFactorAsync(EnrollYubiKeyFactorOptions factorOptions, CancellationToken cancellationToken = default(CancellationToken))
        {
            var enrollYubiKeyFactor = new EnrollFactorRequest()
            {
                FactorType = FactorType.TokenHardware,
                Provider = factorOptions.Provider,
                PassCode = factorOptions.PassCode,
            };

            return await EnrollFactorAsync(enrollYubiKeyFactor, cancellationToken);
        }

        /// <inheritdoc/>
        public async Task<IAuthenticationResponse> EnrollFactorAsync(EnrollDuoFactorOptions factorOptions, CancellationToken cancellationToken = default(CancellationToken))
        {
            var enrollDuoFactor = new EnrollFactorRequest()
            {
                StateToken = factorOptions.StateToken,
                FactorType = FactorType.Web,
                Provider = factorOptions.Provider,
            };

            return await EnrollFactorAsync(enrollDuoFactor, cancellationToken);
        }

        /// <inheritdoc/>
        public async Task<IAuthenticationResponse> EnrollFactorAsync(EnrollU2FFactorOptions factorOptions, CancellationToken cancellationToken = default(CancellationToken))
        {
            var enrollU2Factor = new EnrollFactorRequest()
            {
                StateToken = factorOptions.StateToken,
                FactorType = FactorType.U2f,
                Provider = factorOptions.Provider,
            };

            return await EnrollFactorAsync(enrollU2Factor, cancellationToken);
        }

        /// <inheritdoc/>
        public async Task<IAuthenticationResponse> EnrollFactorAsync(EnrollTotpFactorOptions factorOptions, CancellationToken cancellationToken = default(CancellationToken))
        {
            var enrollTotpFactor = new EnrollFactorRequest()
            {
                StateToken = factorOptions.StateToken,
                FactorType = FactorType.TokenSoftwareTotp,
                Provider = factorOptions.Provider,
            };

            return await EnrollFactorAsync(enrollTotpFactor, cancellationToken);
        }

        /// <inheritdoc/>
        private async Task<IAuthenticationResponse> EnrollFactorAsync(EnrollFactorRequest request, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await PostAsync<AuthenticationResponse>(
                new HttpRequest
                {
                    Uri = "/api/v1/authn/factors",
                    Payload = request,
                }, cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<IAuthenticationResponse> ActivateFactorAsync(ActivateFactorOptions activateFactorOptions, CancellationToken cancellationToken = default(CancellationToken))
        {
            var activateFactorRequest = new ActivateFactorRequest()
            {
                FactorType = activateFactorOptions.FactorType,
                StateToken = activateFactorOptions.StateToken,
                PassCode = activateFactorOptions.PassCode,
            };

            return await ActivateFactorAsync(activateFactorRequest, activateFactorOptions.FactorId, cancellationToken);
        }

        /// <inheritdoc/>
        public async Task<IAuthenticationResponse> ActivateFactorAsync(ActivatePushFactorOptions activatePushFactorOptions, CancellationToken cancellationToken = default(CancellationToken))
        {
            var activateFactorRequest = new ActivateFactorRequest()
            {
                FactorType = FactorType.Push,
                StateToken = activatePushFactorOptions.StateToken,
            };

            return await PostAsync<AuthenticationResponse>(
                new HttpRequest
                {
                    Uri = $"/api/v1/authn/factors/{activatePushFactorOptions.FactorId}/lifecycle/activate",
                    Payload = activateFactorRequest,
                }, cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<IAuthenticationResponse> ActivateFactorAsync(ActivateU2fFactorOptions activateFactorOptions, CancellationToken cancellationToken = default(CancellationToken))
        {
            var activateFactorRequest = new ActivateU2FFactorRequest()
            {
                StateToken = activateFactorOptions.StateToken,
                ClientData = activateFactorOptions.ClientData,
                RegistrationData = activateFactorOptions.RegistrationData,
            };

            return await PostAsync<AuthenticationResponse>(
                new HttpRequest
                {
                    Uri = $"/api/v1/authn/factors/{activateFactorOptions.FactorId}/lifecycle/activate",
                    Payload = activateFactorRequest,
                }, cancellationToken).ConfigureAwait(false);
        }

        private async Task<IAuthenticationResponse> ActivateFactorAsync(ActivateFactorRequest activateFactorRequest, string factorId, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await PostAsync<AuthenticationResponse>(
                new HttpRequest
                {
                    Uri = $"/api/v1/authn/factors/{factorId}/lifecycle/activate",
                    Payload = activateFactorRequest,
                }, cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<IAuthenticationResponse> AnswerRecoveryQuestionAsync(AnswerRecoveryQuestionOptions answerOptions, CancellationToken cancellationToken = default(CancellationToken))
        {
            var answerRecoveryRequest = new AnswerRecoveryQuestionRequest()
            {
                StateToken = answerOptions.StateToken,
                Answer = answerOptions.Answer,
            };

            return await PostAsync<AuthenticationResponse>(
                new HttpRequest
                {
                    Uri = $"/api/v1/authn/recovery/answer",
                    Payload = answerRecoveryRequest,
                }, cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<IAuthenticationResponse> VerifyRecoveryTokenAsync(VerifyRecoveryTokenOptions verifyRecoveryTokenOptions, CancellationToken cancellationToken = default(CancellationToken))
        {
            var recoveryTokenRequest = new VerifyRecoveryTokenRequest()
            {
                RecoveryToken = verifyRecoveryTokenOptions.RecoveryToken,
            };

            return await PostAsync<AuthenticationResponse>(
                new HttpRequest
                {
                    Uri = $"/api/v1/authn/recovery/token",
                    Payload = recoveryTokenRequest,
                }, cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<IAuthenticationResponse> VerifyFactorAsync(VerifyRecoveryFactorOptions verifyRecoveryFactorOptions, CancellationToken cancellationToken = default(CancellationToken))
        {
            var verifyRecoveryFactorRequest = new VerifyFactorRecoveryRequest()
            {
                StateToken = verifyRecoveryFactorOptions.StateToken,
                PassCode = verifyRecoveryFactorOptions.PassCode,
            };

            return await PostAsync<AuthenticationResponse>(
                new HttpRequest
                {
                    Uri = $"/api/v1/authn/recovery/factors/{verifyRecoveryFactorOptions.FactorType.ToString().ToLower()}/verify",
                    Payload = verifyRecoveryFactorRequest,
                }, cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<IAuthenticationResponse> ResendRecoveryChallengeAsync(ResendRecoveryChallengeOptions resendRecoveryChallengeOptions, CancellationToken cancellationToken = default(CancellationToken))
        {
            var recoveryChallengeRequest = new ResendRecoveryChallengeRequest()
            {
                StateToken = resendRecoveryChallengeOptions.StateToken,
            };

            return await PostAsync<AuthenticationResponse>(
                new HttpRequest
                {
                    Uri = $"/api/v1/authn/recovery/factors/{resendRecoveryChallengeOptions.FactorType.ToString().ToLower()}/resend",
                    Payload = recoveryChallengeRequest,
                }, cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<IAuthenticationResponse> UnlockAccountAsync(UnlockAccountOptions unlockAccountOptions, CancellationToken cancellationToken = default(CancellationToken))
        {
            var unlockAccountRequest = new UnlockAccountRequest()
            {
                FactorType = unlockAccountOptions.FactorType,
                RelayState = unlockAccountOptions.RelayState,
                Username = unlockAccountOptions.UserName,
            };

            return await PostAsync<AuthenticationResponse>(
                new HttpRequest
                {
                    Uri = $"/api/v1/authn/recovery/unlock",
                    Payload = unlockAccountRequest,
                }, cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<IAuthenticationResponse> GetTransactionStateAsync(TransactionStateOptions transactionStateOptions, CancellationToken cancellationToken = default(CancellationToken))
        {
            var transactionStateRequest = new TransactionStateRequest()
            {
                StateToken = transactionStateOptions.StateToken,
            };

            return await PostAsync<AuthenticationResponse>(
                new HttpRequest
                {
                    Uri = $"/api/v1/authn",
                    Payload = transactionStateRequest,
                }, cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<IAuthenticationResponse> GetPreviousTransactionStateAsync(TransactionStateOptions transactionStateOptions, CancellationToken cancellationToken = default(CancellationToken))
        {
            var transactionStateRequest = new TransactionStateRequest()
            {
                StateToken = transactionStateOptions.StateToken,
            };

            return await PostAsync<AuthenticationResponse>(
                new HttpRequest
                {
                    Uri = $"/api/v1/authn/previous",
                    Payload = transactionStateRequest,
                }, cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<IAuthenticationResponse> SkipTransactionStateAsync(TransactionStateOptions transactionStateOptions, CancellationToken cancellationToken = default(CancellationToken))
        {
            var transactionStateRequest = new TransactionStateRequest()
            {
                StateToken = transactionStateOptions.StateToken,
            };

            return await PostAsync<AuthenticationResponse>(
                new HttpRequest
                {
                    Uri = $"/api/v1/authn/skip",
                    Payload = transactionStateRequest,
                }, cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<IAuthenticationResponse> CancelTransactionStateAsync(TransactionStateOptions transactionStateOptions, CancellationToken cancellationToken = default(CancellationToken))
        {
            var transactionStateRequest = new TransactionStateRequest()
            {
                StateToken = transactionStateOptions.StateToken,
            };

            return await PostAsync<AuthenticationResponse>(
                new HttpRequest
                {
                    Uri = $"/api/v1/authn/cancel",
                    Payload = transactionStateRequest,
                }, cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<IAuthenticationResponse> VerifyFactorAsync(VerifyU2FFactorOptions verifyU2FFactorOptions, CancellationToken cancellationToken = default(CancellationToken))
        {
            var verifyU2fFactorRequest = new VerifyU2fFactorRequest()
            {
                StateToken = verifyU2FFactorOptions.StateToken,
                ClientData = verifyU2FFactorOptions.ClientData,
                RememberDevice = verifyU2FFactorOptions.RememberDevice,
                SignatureData = verifyU2FFactorOptions.SignatureData,
            };

            return await VerifyFactorAsync(verifyU2fFactorRequest, verifyU2FFactorOptions.FactorId, cancellationToken);
        }

        /// <inheritdoc/>
        public async Task<IAuthenticationResponse> VerifyFactorAsync(VerifyDuoFactorOptions verifyDuoFactorOptions, CancellationToken cancellationToken = default(CancellationToken))
        {
            var verifyDuoFactorRequest = new VerifyDuoFactorRequest()
            {
                StateToken = verifyDuoFactorOptions.StateToken,
            };

            return await VerifyFactorAsync(verifyDuoFactorRequest, verifyDuoFactorOptions.FactorId, cancellationToken);
        }

        /// <inheritdoc/>
        public async Task<IAuthenticationResponse> VerifyFactorAsync(VerifyCallFactorOptions verifyCallFactorOptions, CancellationToken cancellationToken = default(CancellationToken))
        {
            var verifyCallFactorRequest = new VerifyCallFactorRequest()
            {
                StateToken = verifyCallFactorOptions.StateToken,
                PassCode = verifyCallFactorOptions.PassCode,
                RememberDevice = verifyCallFactorOptions.RememberDevice,
            };

            return await VerifyFactorAsync(verifyCallFactorRequest, verifyCallFactorOptions.FactorId, cancellationToken);
        }

        /// <inheritdoc/>
        public async Task<IAuthenticationResponse> VerifyFactorAsync(VerifyPushFactorOptions verifyPushFactorOptions, CancellationToken cancellationToken = default(CancellationToken))
        {
            var verifyPushFactorRequest = new VerifyPushFactorRequest()
            {
                StateToken = verifyPushFactorOptions.StateToken,
                RememberDevice = verifyPushFactorOptions.RememberDevice,
                AutoPush = verifyPushFactorOptions.AutoPush,
            };

            return await VerifyFactorAsync(verifyPushFactorRequest, verifyPushFactorOptions.FactorId, cancellationToken);
        }

        /// <inheritdoc/>
        public async Task<IAuthenticationResponse> VerifyFactorAsync(VerifyTotpFactorOptions verifyTotpFactorOptions, CancellationToken cancellationToken = default(CancellationToken))
        {
            var verifyTotpFactorRequest = new VerifyTotpFactorRequest()
            {
                StateToken = verifyTotpFactorOptions.StateToken,
                PassCode = verifyTotpFactorOptions.PassCode,
                RememberDevice = verifyTotpFactorOptions.RememberDevice,
            };

            return await VerifyFactorAsync(verifyTotpFactorRequest, verifyTotpFactorOptions.FactorId, cancellationToken);
        }

        /// <inheritdoc/>
        public async Task<IAuthenticationResponse> VerifyFactorAsync(VerifySmsFactorOptions verifySmsFactorOptions, CancellationToken cancellationToken = default(CancellationToken))
        {
            var verifySmsFactorRequest = new VerifySmsFactorRequest()
            {
                StateToken = verifySmsFactorOptions.StateToken,
                PassCode = verifySmsFactorOptions.PassCode,
                RememberDevice = verifySmsFactorOptions.RememberDevice,
            };

            return await VerifyFactorAsync(verifySmsFactorRequest, verifySmsFactorOptions.FactorId, cancellationToken);
        }

        /// <inheritdoc/>
        public async Task<IAuthenticationResponse> VerifyFactorAsync(VerifySecurityQuestionFactorOptions verifySecurityQuestionFactorOptions, CancellationToken cancellationToken = default(CancellationToken))
        {
            var verifySecurityQuestionFactorRequest = new VerifySecurityQuestionFactorRequest()
            {
                StateToken = verifySecurityQuestionFactorOptions.StateToken,
                RememberDevice = verifySecurityQuestionFactorOptions.RememberDevice,
                Answer = verifySecurityQuestionFactorOptions.Answer,
            };

            return await VerifyFactorAsync(verifySecurityQuestionFactorRequest, verifySecurityQuestionFactorOptions.FactorId, cancellationToken);
        }

        private async Task<IAuthenticationResponse> VerifyFactorAsync(Resource verifyFactorRequest, string factorId, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await PostAsync<AuthenticationResponse>(
                new HttpRequest
                {
                    Uri = $"/api/v1/authn/factors/{factorId}/verify",
                    Payload = verifyFactorRequest,
                }, cancellationToken).ConfigureAwait(false);
        }

        public async Task<IAuthenticationResponse> ResendChallengeAsync(ResendChallengeOptions resendChallengeOptions, CancellationToken cancellationToken = default(CancellationToken))
        {
            var resendChallengeRequest = new ResendChallengeRequest()
            {
                StateToken = resendChallengeOptions.StateToken,
            };

            return await PostAsync<AuthenticationResponse>(
                new HttpRequest
                {
                    Uri = $"/api/v1/authn/factors/{resendChallengeOptions.FactorId}/verify/resend",
                    Payload = resendChallengeRequest,
                }, cancellationToken).ConfigureAwait(false);
        }
    }
}
