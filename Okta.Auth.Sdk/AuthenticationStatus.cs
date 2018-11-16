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

        public static AuthenticationStatus Unauthenticated = new AuthenticationStatus("UNAUTHENTICATED");

        public static AuthenticationStatus PasswordExpired = new AuthenticationStatus("PASSWORD_EXPIRED");

        public static AuthenticationStatus PasswordWarn = new AuthenticationStatus("PASSWORD_WARN");

        public static AuthenticationStatus PasswordReset = new AuthenticationStatus("PASSWORD_RESET");

        public static AuthenticationStatus Success = new AuthenticationStatus("SUCCESS");

        public static AuthenticationStatus LockedOut = new AuthenticationStatus("LOCKED_OUT");

        public static AuthenticationStatus MfaRequired = new AuthenticationStatus("MFA_REQUIRED");

        public static AuthenticationStatus Unknown = new AuthenticationStatus("UNKNOWN");

        public static AuthenticationStatus MfaEnroll = new AuthenticationStatus("MFA_ENROLL");

        public static AuthenticationStatus MfaEnrollActivate = new AuthenticationStatus("MFA_ENROLL_ACTIVATE");

        public static AuthenticationStatus MfaChallenge = new AuthenticationStatus("MFA_CHALLENGE");

        public static AuthenticationStatus RecoveryChallenge = new AuthenticationStatus("RECOVERY_CHALLENGE");

        public static AuthenticationStatus Recovery = new AuthenticationStatus("RECOVERY");
    }
}
