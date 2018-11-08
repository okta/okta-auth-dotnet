using System.Threading;
using System.Threading.Tasks;
using Okta.Authn.POCOs;
using Okta.Sdk.Abstractions;

namespace Okta.Authn.Models
{
    public interface IAuthenticationClient : IBaseOktaClient
    {
        /// <summary>
        /// <see cref="https://developer.okta.com/docs/api/resources/authn#primary-authentication"/>
        /// </summary>
        /// <param name="authenticateOptions"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IAuthenticationResponse> AuthenticateAsync(AuthenticateOptions authenticateOptions, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// <see cref="https://developer.okta.com/docs/api/resources/authn#change-password"/>
        /// </summary>
        /// <param name="passwordOptions"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IAuthenticationResponse> ChangePasswordAsync(ChangePasswordOptions passwordOptions, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// <see cref="https://developer.okta.com/docs/api/resources/authn#forgot-password"/>
        /// </summary>
        /// <param name="forgotPasswordOptions"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IAuthenticationResponse> ForgotPasswordAsync(ForgotPasswordOptions forgotPasswordOptions, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// <see cref="https://developer.okta.com/docs/api/resources/authn#reset-password"/>
        /// </summary>
        /// <param name="resetPasswordOptions"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IAuthenticationResponse> ResetPasswordAsync(ResetPasswordOptions resetPasswordOptions, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// <see cref="https://developer.okta.com/docs/api/resources/authn#enroll-factor"/>
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IAuthenticationResponse> EnrollFactorAsync(EnrollFactorRequest request, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// <see cref="https://developer.okta.com/docs/api/resources/authn#enroll-okta-sms-factor"/>
        /// </summary>
        /// <param name="factorOptions"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IAuthenticationResponse> EnrollFactorAsync(EnrollSMSFactorOptions factorOptions, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// <see cref="https://developer.okta.com/docs/api/resources/authn#enroll-okta-security-question-factor"/>
        /// </summary>
        /// <param name="factorOptions"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IAuthenticationResponse> EnrollFactorAsync(EnrollSecurityQuestionFactorOptions factorOptions, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// <see cref="https://developer.okta.com/docs/api/resources/authn#enroll-okta-call-factor"/>
        /// </summary>
        /// <param name="factorOptions"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IAuthenticationResponse> EnrollFactorAsync(EnrollCallFactorOptions factorOptions, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// <see cref="https://developer.okta.com/docs/api/resources/authn#enroll-okta-verify-push-factor"/>
        /// </summary>
        /// <param name="factorOptions"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IAuthenticationResponse> EnrollFactorAsync(EnrollPushFactorOptions factorOptions, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// <see cref="https://developer.okta.com/docs/api/resources/authn#enroll-rsa-securid-factor"/>
        /// </summary>
        /// <param name="factorOptions"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IAuthenticationResponse> EnrollFactorAsync(EnrollRsaFactorOptions factorOptions, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// <see cref="https://developer.okta.com/docs/api/resources/authn#enroll-symantec-vip-factor"/>
        /// </summary>
        /// <param name="factorOptions"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IAuthenticationResponse> EnrollFactorAsync(EnrollSymantecFactorOptions factorOptions, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// <see cref="https://developer.okta.com/docs/api/resources/authn#enroll-yubikey-factor"/>
        /// </summary>
        /// <param name="factorOptions"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IAuthenticationResponse> EnrollFactorAsync(EnrollYubiKeyFactorOptions factorOptions, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// <see cref="https://developer.okta.com/docs/api/resources/authn#enroll-duo-factor"/>
        /// </summary>
        /// <param name="factorOptions"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IAuthenticationResponse> EnrollFactorAsync(EnrollDuoFactorOptions factorOptions, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// <see cref="https://developer.okta.com/docs/api/resources/authn#enroll-u2f-factor"/>
        /// </summary>
        /// <param name="factorOptions"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IAuthenticationResponse> EnrollFactorAsync(EnrollU2fFactorOptions factorOptions, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// <see cref="https://developer.okta.com/docs/api/resources/authn#activate-factor"/>
        /// For U2F use <see cref="ActivateU2fFactorAsync"/>
        /// </summary>
        /// <param name="activateFactorOptions"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IAuthenticationResponse> ActivateFactorAsync(ActivateFactorOptions activateFactorOptions, CancellationToken cancellationToken = default(CancellationToken));

        Task<IAuthenticationResponse> ActivateU2fFactorAsync(ActivateU2fFactorOptions activateFactorOptions, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// <see cref="https://developer.okta.com/docs/api/resources/authn#activate-factor"/>
        /// </summary>
        /// <param name="activateFactorRequest"></param>
        /// <param name="factorId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IAuthenticationResponse> ActivateFactorAsync(ActivateFactorRequest activateFactorRequest, string factorId, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// <see cref="https://developer.okta.com/docs/api/resources/authn#activate-u2f-factor"/>
        /// </summary>
        /// <param name="activateFactorRequest"></param>
        /// <param name="factorId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IAuthenticationResponse> ActivateFactorAsync(ActivateU2FFactorRequest activateFactorRequest, string factorId, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// <see cref="https://developer.okta.com/docs/api/resources/authn#answer-recovery-question"/>
        /// </summary>
        /// <param name="answerOptions"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IAuthenticationResponse> AnswerRecoveryQuestionAsync(AnswerRecoveryQuestionOptions answerOptions, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// <see cref="https://developer.okta.com/docs/api/resources/authn#verify-recovery-token"/>
        /// </summary>
        /// <param name="verifyRecoveryTokenOptions"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IAuthenticationResponse> VerifyRecoveryTokenAsync(VerifyRecoveryTokenOptions verifyRecoveryTokenOptions, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Verifies Recovery for a factor (SMS/Call).
        /// <see cref="https://developer.okta.com/docs/api/resources/authn#verify-recovery-factor"/>
        /// </summary>
        /// <param name="verifyFactorRecoveryOptions"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IAuthenticationResponse> VerifyRecoveryFactorAsync(VerifyRecoveryFactorOptions verifyFactorRecoveryOptions, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Re sends recovery challenge for a factor (SMS/Call)
        /// <see cref="https://developer.okta.com/docs/api/resources/authn#resend-sms-recovery-challenge"/>
        /// <seealso cref="https://developer.okta.com/docs/api/resources/authn#resend-call-recovery-challenge"/>
        /// </summary>
        /// <param name="resendRecoveryChallengeOptions"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IAuthenticationResponse> ResendRecoveryChallengeAsync(ResendRecoveryChallengeOptions resendRecoveryChallengeOptions, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Unlocks an account <see cref="https://developer.okta.com/docs/api/resources/authn#unlock-account"/>
        /// </summary>
        /// <param name="unlockAccountOptions"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IAuthenticationResponse> UnlockAccountAsync(UnlockAccountOptions unlockAccountOptions, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Unlocks an account <see cref="https://developer.okta.com/docs/api/resources/authn#unlock-account"/> 
        /// </summary>
        /// <param name="unlockAccountRequest"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IAuthenticationResponse> UnlockAccountAsync(UnlockAccountRequest unlockAccountRequest, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// <see cref="https://developer.okta.com/docs/api/resources/authn#get-transaction-state"/>
        /// </summary>
        /// <param name="transactionStateOptions"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IAuthenticationResponse> GetTransactionStateAsync(TransactionStateOptions transactionStateOptions, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// <see cref="https://developer.okta.com/docs/api/resources/authn#previous-transaction-state"/>
        /// </summary>
        /// <param name="transactionStateOptions"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IAuthenticationResponse> GetPreviousTransactionStateAsync(TransactionStateOptions transactionStateOptions, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// <see cref="https://developer.okta.com/docs/api/resources/authn#skip-transaction-state"/>
        /// </summary>
        /// <param name="transactionStateOptions"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IAuthenticationResponse> SkipTransactionStateAsync(TransactionStateOptions transactionStateOptions, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// <see cref="https://developer.okta.com/docs/api/resources/authn#cancel-transaction"/>
        /// </summary>
        /// <param name="transactionStateOptions"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IAuthenticationResponse> CancelTransactionStateAsync(TransactionStateOptions transactionStateOptions, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// <see cref="https://developer.okta.com/docs/api/resources/authn#verify-u2f-factor"/>
        /// </summary>
        /// <param name="transactionStateOptions"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IAuthenticationResponse> VerifyU2fFactorAsync(VerifyU2FFactorOptions verifyU2FFactorOptions, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// <see cref="https://developer.okta.com/docs/api/resources/authn#verify-duo-factor"/>
        /// </summary>
        /// <param name="verifyU2FFactorOptions"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IAuthenticationResponse> VerifyDuoFactorAsync(VerifyDuoFactorOptions verifyDuoFactorOptions, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// <see cref="https://developer.okta.com/docs/api/resources/authn#verify-call-factor"/>
        /// </summary>
        /// <param name="verifyCallFactorOptions"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IAuthenticationResponse> VerifyCallFactorAsync(VerifyCallFactorOptions verifyCallFactorOptions, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// <see cref="https://developer.okta.com/docs/api/resources/authn#verify-push-factor"/>
        /// </summary>
        /// <param name="verifyPushFactorOptions"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IAuthenticationResponse> VerifyPushFactorAsync(VerifyPushFactorOptions verifyPushFactorOptions, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// <see cref="https://developer.okta.com/docs/api/resources/authn#verify-totp-factor"/>
        /// </summary>
        /// <param name="verifyTotpFactorOptions"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IAuthenticationResponse> VerifyTotpFactorAsync(VerifyTotpFactorOptions verifyTotpFactorOptions, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// <see cref="https://developer.okta.com/docs/api/resources/authn#verify-sms-factor"/>
        /// </summary>
        /// <param name="verifySmsFactorOptions"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IAuthenticationResponse> VerifySmsFactorAsync(VerifySmsFactorOptions verifySmsFactorOptions, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// <see cref="https://developer.okta.com/docs/api/resources/authn#verify-security-question-factor"/>
        /// </summary>
        /// <param name="verifySecurityQuestionFactorOptions"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IAuthenticationResponse> VerifySecurityQuestionFactorAsync(VerifySecurityQuestionFactorOptions verifySecurityQuestionFactorOptions, CancellationToken cancellationToken = default(CancellationToken));



    }
}
