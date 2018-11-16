// <copyright file="VerifyRecoveryTokenRequest.cs" company="Okta, Inc">
// Copyright (c) 2018 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

using Okta.Auth.Sdk.Abstractions;

namespace Okta.Auth.Sdk.Models
{
    public class VerifyRecoveryTokenRequest : Resource
    {
        public string RecoveryToken
        {
            get => GetStringProperty("recoveryToken");
            set => this["recoveryToken"] = value;
        }
    }
}
