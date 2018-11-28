// <copyright file="AuthenticationStatus.cs" company="Okta, Inc">
// Copyright (c) 2018 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

using Okta.Sdk.Abstractions;

namespace Okta.Auth.Sdk
{
    public class AuthenticationStatus : StringEnum
    {
        public AuthenticationStatus(string value)
            : base(value)
        {
        }

        public static readonly AuthenticationStatus Unauthenticated = new AuthenticationStatus("UNAUTHENTICATED");

        public static readonly AuthenticationStatus PasswordExpired = new AuthenticationStatus("PASSWORD_EXPIRED");

        public static readonly AuthenticationStatus PasswordWarn = new AuthenticationStatus("PASSWORD_WARN");

        public static readonly AuthenticationStatus PasswordReset = new AuthenticationStatus("PASSWORD_RESET");

        public static readonly AuthenticationStatus Success = new AuthenticationStatus("SUCCESS");

        public static readonly AuthenticationStatus LockedOut = new AuthenticationStatus("LOCKED_OUT");

        public static readonly AuthenticationStatus MfaRequired = new AuthenticationStatus("MFA_REQUIRED");

        public static readonly AuthenticationStatus Unknown = new AuthenticationStatus("UNKNOWN");

        public static readonly AuthenticationStatus MfaEnroll = new AuthenticationStatus("MFA_ENROLL");

        public static readonly AuthenticationStatus MfaEnrollActivate = new AuthenticationStatus("MFA_ENROLL_ACTIVATE");

        public static readonly AuthenticationStatus MfaChallenge = new AuthenticationStatus("MFA_CHALLENGE");

        public static readonly AuthenticationStatus RecoveryChallenge = new AuthenticationStatus("RECOVERY_CHALLENGE");

        public static readonly AuthenticationStatus Recovery = new AuthenticationStatus("RECOVERY");
    }
}
