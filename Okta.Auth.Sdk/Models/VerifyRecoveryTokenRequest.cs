// <copyright file="VerifyRecoveryTokenRequest.cs" company="Okta, Inc">
// Copyright (c) 2018 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

namespace Okta.Auth.Sdk.Models
{
    /// <summary>
    /// This class represents the body of the verify recovery token request
    /// </summary>
    public class VerifyRecoveryTokenRequest : Resource
    {
        /// <summary>
        /// Gets or sets the recovery token
        /// </summary>
        /// <value>The recovery token</value>
        public string RecoveryToken
        {
            get => GetStringProperty("recoveryToken");
            set => this["recoveryToken"] = value;
        }
    }
}
