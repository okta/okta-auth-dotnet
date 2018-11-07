using System.Threading;
using System.Threading.Tasks;
using Okta.Authn.POCOs;
using Okta.Sdk.Abstractions;

namespace Okta.Authn.Models
{
    public interface IAuthenticationClient : IBaseOktaClient
    {
        Task<AuthenticationResponse> AuthenticateAsync(AuthenticateOptions authenticateOptions, CancellationToken cancellationToken = default(CancellationToken));

        Task<AuthenticationResponse> ChangePasswordAsync(ChangePasswordOptions passwordOptions, CancellationToken cancellationToken = default(CancellationToken));

        Task<AuthenticationResponse> ForgotPasswordAsync(ForgotPasswordOptions forgotPasswordOptions, CancellationToken cancellationToken = default(CancellationToken));

        Task<AuthenticationResponse> ResetPasswordAsync(ResetPasswordOptions resetPasswordOptions, CancellationToken cancellationToken = default(CancellationToken));

        Task<AuthenticationResponse> EnrollFactorAsync(EnrollFactorRequest request, CancellationToken cancellationToken = default(CancellationToken));

        Task<AuthenticationResponse> EnrollFactorAsync(EnrollSMSFactorOptions factorOptions, CancellationToken cancellationToken = default(CancellationToken));

        Task<AuthenticationResponse> EnrollFactorAsync(EnrollSecurityQuestionFactorOptions factorOptions, CancellationToken cancellationToken = default(CancellationToken));

        Task<AuthenticationResponse> EnrollFactorAsync(EnrollCallFactorOptions factorOptions, CancellationToken cancellationToken = default(CancellationToken));

        Task<AuthenticationResponse> EnrollFactorAsync(EnrollPushFactorOptions factorOptions, CancellationToken cancellationToken = default(CancellationToken));

        Task<AuthenticationResponse> EnrollFactorAsync(EnrollRsaFactorOptions factorOptions, CancellationToken cancellationToken = default(CancellationToken));

        Task<AuthenticationResponse> EnrollFactorAsync(EnrollSymantecFactorOptions factorOptions, CancellationToken cancellationToken = default(CancellationToken));

        Task<AuthenticationResponse> EnrollFactorAsync(EnrollYubiKeyFactorOptions factorOptions, CancellationToken cancellationToken = default(CancellationToken));

        Task<AuthenticationResponse> EnrollFactorAsync(EnrollDuoFactorOptions factorOptions, CancellationToken cancellationToken = default(CancellationToken));

        Task<AuthenticationResponse> EnrollFactorAsync(EnrollU2fFactorOptions factorOptions, CancellationToken cancellationToken = default(CancellationToken));

        Task<AuthenticationResponse> ActivateFactorAsync(ActivateFactorOptions activateFactorOptions, CancellationToken cancellationToken = default(CancellationToken));

        Task<AuthenticationResponse> ActivateU2fFactorAsync(ActivateU2fFactorOptions activateFactorOptions, CancellationToken cancellationToken = default(CancellationToken));

        Task<AuthenticationResponse> ActivateFactorAsync(ActivateFactorRequest activateFactorRequest, string factorId, CancellationToken cancellationToken = default(CancellationToken));

        Task<AuthenticationResponse> ActivateFactorAsync(ActivateU2FFactorRequest activateFactorRequest, string factorId, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// <see cref="https://developer.okta.com/docs/api/resources/authn#answer-recovery-question"/>
        /// </summary>
        /// <param name="answerOptions"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<AuthenticationResponse> AnswerRecoveryQuestionAsync(AnswerRecoveryQuestionOptions answerOptions, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// <see cref="https://developer.okta.com/docs/api/resources/authn#verify-recovery-token"/>
        /// </summary>
        /// <param name="verifyRecoveryTokenOptions"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<AuthenticationResponse> VerifyRecoveryTokenAsync(VerifyRecoveryTokenOptions verifyRecoveryTokenOptions, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// <see cref="https://developer.okta.com/docs/api/resources/authn#verify-recovery-factor"/>
        /// </summary>
        /// <param name="verifyFactorRecoveryOptions"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<AuthenticationResponse> VerifyRecoveryFactorAsync(VerifyRecoveryFactorOptions verifyFactorRecoveryOptions, CancellationToken cancellationToken = default(CancellationToken));

        Task<AuthenticationResponse> ResendRecoveryChallengeAsync(ResendRecoveryChallengeOptions resendRecoveryChallengeOptions, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Unlock account (Use only for Email/SMS) <see cref="https://developer.okta.com/docs/api/resources/authn#unlock-account"/>
        /// </summary>
        /// <param name="unlockAccountOptions"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<AuthenticationResponse> UnlockAccountAsync(UnlockAccountOptions unlockAccountOptions, CancellationToken cancellationToken = default(CancellationToken));
    }
}
