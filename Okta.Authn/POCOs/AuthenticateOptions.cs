// <copyright file="AuthenticateOptions.cs" company="Okta, Inc">
// Copyright (c) 2018 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

namespace Okta.Auth.Sdk.POCOs
{
    public class AuthenticateOptions
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public string Audience { get; set; }

        public string RelayState { get; set; }

        public bool MultiOptionalFactorEnroll { get; set; } = true;

        public bool WarnBeforePasswordExpired { get; set; } = true;

        public string DeviceToken { get; set; }
    }
}
