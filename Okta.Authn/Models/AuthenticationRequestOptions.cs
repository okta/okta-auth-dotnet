// <copyright file="AuthenticationRequestOptions.cs" company="Okta, Inc">
// Copyright (c) 2018 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

using Okta.Sdk.Abstractions;

namespace Okta.Authn.Models
{
    public class AuthenticationRequestOptions : BaseResource
    {
        public bool? MultiOptionalFactorEnroll
        {
            get => GetBooleanProperty("multiOptionalFactorEnroll");
            set => this["multiOptionalFactorEnroll"] = value;
        }

        public bool? WarnBeforePasswordExpired
        {
            get => GetBooleanProperty("warnBeforePasswordExpired");
            set => this["multiOptionalFactorEnroll"] = value;
        }
    }
}
