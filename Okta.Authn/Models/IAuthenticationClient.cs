// <copyright file="IAuthenticationClient.cs" company="Okta, Inc">
// Copyright (c) 2018 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

using System.Threading;
using System.Threading.Tasks;
using Okta.Auth.Sdk.POCOs;
using Okta.Sdk.Abstractions;

namespace Okta.Auth.Sdk.Models
{
    public interface IAuthenticationClient : IBaseOktaClient
    {
        /// <summary>
        /// Authenticates a user with username/password credentials
        /// <see cref="https://developer.okta.com/docs/api/resources/authn#primary-authentication"/>
        /// </summary>
        /// <param name="authenticateOptions">The authentication options</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The <see cref="IAuthenticationResponse"/> response</returns>
        Task<IAuthenticationResponse> AuthenticateAsync(AuthenticateOptions authenticateOptions, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Authenticates a user with activation token
        /// <see cref="https://developer.okta.com/docs/api/resources/authn#primary-authentication-with-activation-token"/>
        /// </summary>
        /// <param name="authenticateOptions">The authentication options</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The <see cref="IAuthenticationResponse"/> response</returns
        Task<IAuthenticationResponse> AuthenticateAsync(AuthenticateWithActivationTokenOptions authenticateOptions, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Changes the user password
        /// <see cref="https://developer.okta.com/docs/api/resources/authn#change-password"/>
        /// </summary>
        /// <param name="passwordOptions">The Change Password options</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The <see cref="IAuthenticationResponse"/> response</returns>
        Task<IAuthenticationResponse> ChangePasswordAsync(ChangePasswordOptions passwordOptions, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Starts a new password recovery transaction for a given user and issues a recovery token that can be used to reset a user’s password.
        /// <see cref="https://developer.okta.com/docs/api/resources/authn#forgot-password"/>
        /// </summary>
        /// <param name="forgotPasswordOptions">The forgot password options</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The <see cref="IAuthenticationResponse"/> response</returns>
        Task<IAuthenticationResponse> ForgotPasswordAsync(ForgotPasswordOptions forgotPasswordOptions, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Resets user password
        /// <see cref="https://developer.okta.com/docs/api/resources/authn#reset-password"/>
        /// </summary>
        /// <param name="resetPasswordOptions">The reset password options</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The <see cref="IAuthenticationResponse"/> response</returns>
        Task<IAuthenticationResponse> ResetPasswordAsync(ResetPasswordOptions resetPasswordOptions, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Enrolls a user with a factor assigned by their MFA Policy
        /// <see cref="https://developer.okta.com/docs/api/resources/authn#enroll-factor"/>
        /// </summary>
        /// <param name="request">The enroll factor request payload</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The <see cref="IAuthenticationResponse"/> response</returns>
        Task<IAuthenticationResponse> EnrollFactorAsync(EnrollFactorRequest request, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Enrolls a user with a SMS factor
        /// <see cref="https://developer.okta.com/docs/api/resources/authn#enroll-okta-sms-factor"/>
        /// </summary>
        /// <param name="factorOptions">The enroll SMS factor options</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The <see cref="IAuthenticationResponse"/> response</returns>
        Task<IAuthenticationResponse> EnrollFactorAsync(EnrollSMSFactorOptions factorOptions, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Resend a SMS challenge.
        /// <see cref="https://developer.okta.com/docs/api/resources/authn#resend-sms-as-part-of-enrollment"/>
        /// </summary>
        /// <param name="factorOptions">The enroll SMS factor options</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The <see cref="IAuthenticationResponse"/> response</returns>
        Task<IAuthenticationResponse> ResendSmsEnrollFactorAsync(EnrollSMSFactorOptions factorOptions, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Enrolls a user with a security question factor
        /// <see cref="https://developer.okta.com/docs/api/resources/authn#enroll-okta-security-question-factor"/>
        /// </summary>
        /// <param name="factorOptions">The enroll security question factor options</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The <see cref="IAuthenticationResponse"/> response</returns>
        Task<IAuthenticationResponse> EnrollFactorAsync(EnrollSecurityQuestionFactorOptions factorOptions, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Enrolls a user with a call factor
        /// <see cref="https://developer.okta.com/docs/api/resources/authn#enroll-okta-call-factor"/>
        /// </summary>
        /// <param name="factorOptions">The enroll call factor options</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The <see cref="IAuthenticationResponse"/> response</returns>
        Task<IAuthenticationResponse> EnrollFactorAsync(EnrollCallFactorOptions factorOptions, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Resend a Call challenge.
        /// <see cref="https://developer.okta.com/docs/api/resources/authn#request-example-for-resend-voice-call"/>
        /// </summary>
        /// <param name="factorOptions">The enroll call factor options</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The <see cref="IAuthenticationResponse"/> response</returns>
        Task<IAuthenticationResponse> ResendCallEnrollFactorAsync(EnrollCallFactorOptions factorOptions, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Enrolls a user with a push factor
        /// <see cref="https://developer.okta.com/docs/api/resources/authn#enroll-okta-verify-push-factor"/>
        /// </summary>
        /// <param name="factorOptions">The enroll push factor options</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The <see cref="IAuthenticationResponse"/> response</returns>
        Task<IAuthenticationResponse> EnrollFactorAsync(EnrollPushFactorOptions factorOptions, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Enrolls a user with a RSA factor
        /// <see cref="https://developer.okta.com/docs/api/resources/authn#enroll-rsa-securid-factor"/>
        /// </summary>
        /// <param name="factorOptions">The enroll RSA factor options</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The <see cref="IAuthenticationResponse"/> response</returns>
        Task<IAuthenticationResponse> EnrollFactorAsync(EnrollRsaFactorOptions factorOptions, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Enrolls a user with a Symantec factor
        /// <see cref="https://developer.okta.com/docs/api/resources/authn#enroll-symantec-vip-factor"/>
        /// </summary>
        /// <param name="factorOptions">The enroll Symantec factor options</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The <see cref="IAuthenticationResponse"/> response</returns>
        Task<IAuthenticationResponse> EnrollFactorAsync(EnrollSymantecFactorOptions factorOptions, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Enrolls a user with a YubiKey factor
        /// <see cref="https://developer.okta.com/docs/api/resources/authn#enroll-yubikey-factor"/>
        /// </summary>
        /// <param name="factorOptions">The enroll YubiKey factor options</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The <see cref="IAuthenticationResponse"/> response</returns>
        Task<IAuthenticationResponse> EnrollFactorAsync(EnrollYubiKeyFactorOptions factorOptions, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Enrolls a user with a Duo factor
        /// <see cref="https://developer.okta.com/docs/api/resources/authn#enroll-duo-factor"/>
        /// </summary>
        /// <param name="factorOptions">The enroll Duo factor options</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The <see cref="IAuthenticationResponse"/> response</returns>
        Task<IAuthenticationResponse> EnrollFactorAsync(EnrollDuoFactorOptions factorOptions, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Enrolls a user with a U2F factor
        /// <see cref="https://developer.okta.com/docs/api/resources/authn#enroll-u2f-factor"/>
        /// </summary>
        /// <param name="factorOptions">The enroll U2F factor options</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The <see cref="IAuthenticationResponse"/> response</returns>
        Task<IAuthenticationResponse> EnrollFactorAsync(EnrollU2FFactorOptions factorOptions, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Enrolls a user with a TOTP factor
        /// <see cref="https://developer.okta.com/docs/api/resources/authn#enroll-okta-verify-totp-factor"/>
        /// </summary>
        /// <param name="factorOptions">The enroll TOTP factor options</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The <see cref="IAuthenticationResponse"/> response</returns>
        Task<IAuthenticationResponse> EnrollFactorAsync(EnrollTotpFactorOptions factorOptions, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Activates a factor
        /// <see cref="https://developer.okta.com/docs/api/resources/authn#activate-factor"/>
        /// For U2F use <see cref="ActivateU2fFactorAsync"/>
        /// </summary>
        /// <param name="activateFactorOptions">The activate factor options</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The <see cref="IAuthenticationResponse"/> response</returns>
        Task<IAuthenticationResponse> ActivateFactorAsync(ActivateFactorOptions activateFactorOptions, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Activates a push factor
        /// <see cref="https://developer.okta.com/docs/api/resources/authn#activate-push-factor"/>
        /// </summary>
        /// <param name="activatePushFactorOptions">The activate push factor options</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The <see cref="IAuthenticationResponse"/> response</returns>
        Task<IAuthenticationResponse> ActivateFactorAsync(ActivatePushFactorOptions activatePushFactorOptions, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Activates a U2F factor
        /// <see cref="https://developer.okta.com/docs/api/resources/authn#activate-u2f-factor"/>
        /// </summary>
        /// <param name="activateFactorOptions">The activate U2F factor options</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The <see cref="IAuthenticationResponse"/> response</returns>
        Task<IAuthenticationResponse> ActivateFactorAsync(ActivateU2fFactorOptions activateFactorOptions, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Activates a factor
        /// <see cref="https://developer.okta.com/docs/api/resources/authn#activate-factor"/>
        /// </summary>
        /// <param name="activateFactorRequest">The activate factor request payload</param>
        /// <param name="factorId">The factor Id</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The <see cref="IAuthenticationResponse"/> response</returns>
        Task<IAuthenticationResponse> ActivateFactorAsync(ActivateFactorRequest activateFactorRequest, string factorId, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Activates a U2F factor
        /// <see cref="https://developer.okta.com/docs/api/resources/authn#activate-u2f-factor"/>
        /// </summary>
        /// <param name="activateFactorRequest">The activate factor request payload</param>
        /// <param name="factorId">The factor Id</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The <see cref="IAuthenticationResponse"/> response</returns>
        Task<IAuthenticationResponse> ActivateFactorAsync(ActivateU2FFactorRequest activateFactorRequest, string factorId, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Answers the user’s recovery question
        /// <see cref="https://developer.okta.com/docs/api/resources/authn#answer-recovery-question"/>
        /// </summary>
        /// <param name="answerOptions">The answer recovery question options</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The <see cref="IAuthenticationResponse"/> response</returns>
        Task<IAuthenticationResponse> AnswerRecoveryQuestionAsync(AnswerRecoveryQuestionOptions answerOptions, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Validates a recovery token that was distributed to the end user to continue the recovery transaction.
        /// <see cref="https://developer.okta.com/docs/api/resources/authn#verify-recovery-token"/>
        /// </summary>
        /// <param name="verifyRecoveryTokenOptions">The verify recovery token options</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The <see cref="IAuthenticationResponse"/> response</returns>
        Task<IAuthenticationResponse> VerifyRecoveryTokenAsync(VerifyRecoveryTokenOptions verifyRecoveryTokenOptions, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Verifies Recovery for a factor (SMS/Call).
        /// <see cref="https://developer.okta.com/docs/api/resources/authn#verify-recovery-factor"/>
        /// </summary>
        /// <param name="verifyFactorRecoveryOptions">The verify recovery factor options</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The <see cref="IAuthenticationResponse"/> response</returns>
        Task<IAuthenticationResponse> VerifyRecoveryFactorAsync(VerifyRecoveryFactorOptions verifyFactorRecoveryOptions, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Resend a recovery challenge for a factor (SMS/Call)
        /// <see cref="https://developer.okta.com/docs/api/resources/authn#resend-sms-recovery-challenge"/>
        /// <seealso cref="https://developer.okta.com/docs/api/resources/authn#resend-call-recovery-challenge"/>
        /// </summary>
        /// <param name="resendRecoveryChallengeOptions">The resend recovery challenge options</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The <see cref="IAuthenticationResponse"/> response</returns>
        Task<IAuthenticationResponse> ResendRecoveryChallengeAsync(ResendRecoveryChallengeOptions resendRecoveryChallengeOptions, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Unlocks an account <see cref="https://developer.okta.com/docs/api/resources/authn#unlock-account"/>
        /// </summary>
        /// <param name="unlockAccountOptions">The unlock account options</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The <see cref="IAuthenticationResponse"/> response</returns>
        Task<IAuthenticationResponse> UnlockAccountAsync(UnlockAccountOptions unlockAccountOptions, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Unlocks an account <see cref="https://developer.okta.com/docs/api/resources/authn#unlock-account"/>
        /// </summary>
        /// <param name="unlockAccountRequest">The unlock account request payload</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The <see cref="IAuthenticationResponse"/> response</returns>
        Task<IAuthenticationResponse> UnlockAccountAsync(UnlockAccountRequest unlockAccountRequest, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Retrieves the current transaction state for a state token.
        /// <see cref="https://developer.okta.com/docs/api/resources/authn#get-transaction-state"/>
        /// </summary>
        /// <param name="transactionStateOptions">The transaction state options</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The <see cref="IAuthenticationResponse"/> response</returns>
        Task<IAuthenticationResponse> GetTransactionStateAsync(TransactionStateOptions transactionStateOptions, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Moves the current transaction state back to the previous state.
        /// <see cref="https://developer.okta.com/docs/api/resources/authn#previous-transaction-state"/>
        /// </summary>
        /// <param name="transactionStateOptions">The transaction state options</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The <see cref="IAuthenticationResponse"/> response</returns>
        Task<IAuthenticationResponse> GetPreviousTransactionStateAsync(TransactionStateOptions transactionStateOptions, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Sends a skip link to skip the current transaction state and advance to the next state.
        /// <see cref="https://developer.okta.com/docs/api/resources/authn#skip-transaction-state"/>
        /// </summary>
        /// <param name="transactionStateOptions">The transaction state options</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The <see cref="IAuthenticationResponse"/> response</returns>
        Task<IAuthenticationResponse> SkipTransactionStateAsync(TransactionStateOptions transactionStateOptions, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Cancels the current transaction and revokes the state token.
        /// <see cref="https://developer.okta.com/docs/api/resources/authn#cancel-transaction"/>
        /// </summary>
        /// <param name="transactionStateOptions">The transaction state options</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The <see cref="IAuthenticationResponse"/> response</returns>
        Task<IAuthenticationResponse> CancelTransactionStateAsync(TransactionStateOptions transactionStateOptions, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Verifies a U2F factor
        /// <see cref="https://developer.okta.com/docs/api/resources/authn#verify-u2f-factor"/>
        /// </summary>
        /// <param name="verifyU2FFactorOptions">The verify U2F factor options</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The <see cref="IAuthenticationResponse"/> response</returns>
        Task<IAuthenticationResponse> VerifyU2fFactorAsync(VerifyU2FFactorOptions verifyU2FFactorOptions, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Verifies a Duo factor
        /// <see cref="https://developer.okta.com/docs/api/resources/authn#verify-duo-factor"/>
        /// </summary>
        /// <param name="verifyDuoFactorOptions">The verify Duo factor options</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The <see cref="IAuthenticationResponse"/> response</returns>
        Task<IAuthenticationResponse> VerifyDuoFactorAsync(VerifyDuoFactorOptions verifyDuoFactorOptions, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Verifies a call factor
        /// <see cref="https://developer.okta.com/docs/api/resources/authn#verify-call-factor"/>
        /// </summary>
        /// <param name="verifyCallFactorOptions">The verify call factor options</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The <see cref="IAuthenticationResponse"/> response</returns>
        Task<IAuthenticationResponse> VerifyCallFactorAsync(VerifyCallFactorOptions verifyCallFactorOptions, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Verifies a push factor
        /// <see cref="https://developer.okta.com/docs/api/resources/authn#verify-push-factor"/>
        /// </summary>
        /// <param name="verifyPushFactorOptions">The verify push factor options</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The <see cref="IAuthenticationResponse"/> response</returns>
        Task<IAuthenticationResponse> VerifyPushFactorAsync(VerifyPushFactorOptions verifyPushFactorOptions, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Verifies a TOTP factor
        /// <see cref="https://developer.okta.com/docs/api/resources/authn#verify-totp-factor"/>
        /// </summary>
        /// <param name="verifyTotpFactorOptions">The verify TOTP factor options</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The <see cref="IAuthenticationResponse"/> response</returns>
        Task<IAuthenticationResponse> VerifyTotpFactorAsync(VerifyTotpFactorOptions verifyTotpFactorOptions, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Verifies a sms factor.
        /// <see cref="https://developer.okta.com/docs/api/resources/authn#verify-sms-factor"/>
        /// </summary>
        /// <param name="verifySmsFactorOptions">The verify SMS factor options</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The <see cref="IAuthenticationResponse"/> response</returns>
        Task<IAuthenticationResponse> VerifySmsFactorAsync(VerifySmsFactorOptions verifySmsFactorOptions, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Verifies an answer to a question factor.
        /// <see cref="https://developer.okta.com/docs/api/resources/authn#verify-security-question-factor"/>
        /// </summary>
        /// <param name="verifySecurityQuestionFactorOptions">The verify security question factor options</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The <see cref="IAuthenticationResponse"/> response</returns>
        Task<IAuthenticationResponse> VerifySecurityQuestionFactorAsync(VerifySecurityQuestionFactorOptions verifySecurityQuestionFactorOptions, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Resend challenge
        /// </summary>
        /// <param name="resendChallengeOptions">The resend challnege options</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The <see cref="IAuthenticationResponse"/> response</returns>
        Task<IAuthenticationResponse> ResendChallengeAsync(ResendChallengeOptions resendChallengeOptions, CancellationToken cancellationToken = default(CancellationToken));
    }
}
