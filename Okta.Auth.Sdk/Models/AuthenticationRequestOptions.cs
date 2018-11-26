// <copyright file="AuthenticationRequestOptions.cs" company="Okta, Inc">
// Copyright (c) 2018 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

namespace Okta.Auth.Sdk.Models
{
    public class AuthenticationRequestOptions : Resource
    {
        public bool? MultiOptionalFactorEnroll
        {
            get => GetBooleanProperty("multiOptionalFactorEnroll");
            set => this["multiOptionalFactorEnroll"] = value;
        }

        public bool? WarnBeforePasswordExpired
        {
            get => GetBooleanProperty("warnBeforePasswordExpired");
            set => this["warnBeforePasswordExpired"] = value;
        }
    }
}
