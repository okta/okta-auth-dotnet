// <copyright file="AuthenticationStatus.cs" company="Okta, Inc">
// Copyright (c) 2018 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

using Okta.Sdk.Abstractions;

namespace Okta.Auth.Sdk
{
    /// <summary>
    /// This class represents the possible authentication response statuses
    /// </summary>
    public class AuthenticationStatus : StringEnum
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationStatus"/> class.
        /// </summary>
        /// <param name="value">The status value</param>
        public AuthenticationStatus(string value)
            : base(value)
        {
        }

        /// <summary>
        /// Unauthenticated
        /// </summary>
        public static readonly AuthenticationStatus Unauthenticated = new AuthenticationStatus("UNAUTHENTICATED");

        /// <summary>
        /// Password expired
        /// </summary>
        public static readonly AuthenticationStatus PasswordExpired = new AuthenticationStatus("PASSWORD_EXPIRED");

        /// <summary>
        /// Password warning
        /// </summary>
        public static readonly AuthenticationStatus PasswordWarn = new AuthenticationStatus("PASSWORD_WARN");

        /// <summary>
        /// Password reset
        /// </summary>
        public static readonly AuthenticationStatus PasswordReset = new AuthenticationStatus("PASSWORD_RESET");

        /// <summary>
        /// Success
        /// </summary>
        public static readonly AuthenticationStatus Success = new AuthenticationStatus("SUCCESS");

        /// <summary>
        /// Locked out
        /// </summary>
        public static readonly AuthenticationStatus LockedOut = new AuthenticationStatus("LOCKED_OUT");

        /// <summary>
        /// MFA required
        /// </summary>
        public static readonly AuthenticationStatus MfaRequired = new AuthenticationStatus("MFA_REQUIRED");

        /// <summary>
        /// Unknown
        /// </summary>
        public static readonly AuthenticationStatus Unknown = new AuthenticationStatus("UNKNOWN");

        /// <summary>
        /// MFA enroll
        /// </summary>
        public static readonly AuthenticationStatus MfaEnroll = new AuthenticationStatus("MFA_ENROLL");

        /// <summary>
        /// MFA enroll activate
        /// </summary>
        public static readonly AuthenticationStatus MfaEnrollActivate = new AuthenticationStatus("MFA_ENROLL_ACTIVATE");

        /// <summary>
        /// MFA challenge
        /// </summary>
        public static readonly AuthenticationStatus MfaChallenge = new AuthenticationStatus("MFA_CHALLENGE");

        /// <summary>
        /// Recovery challenge
        /// </summary>
        public static readonly AuthenticationStatus RecoveryChallenge = new AuthenticationStatus("RECOVERY_CHALLENGE");

        /// <summary>
        /// Recovery
        /// </summary>
        public static readonly AuthenticationStatus Recovery = new AuthenticationStatus("RECOVERY");
    }
}
