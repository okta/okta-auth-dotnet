// <copyright file="VerifyTotpFactorRequest.cs" company="Okta, Inc">
// Copyright (c) 2018 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

namespace Okta.Auth.Sdk.Models
{
    /// <summary>
    /// This class represents the body of the verify TOTP factor request
    /// </summary>
    public class VerifyTotpFactorRequest : Resource
    {
        /// <summary>
        /// Gets or sets the state token
        /// </summary>
        /// <value>The state token</value>
        public string StateToken
        {
            get => GetStringProperty("stateToken");
            set => this["stateToken"] = value;
        }

        /// <summary>
        /// Gets or sets the pass code
        /// </summary>
        /// <value>The pass code</value>
        public string PassCode
        {
            get => GetStringProperty("passCode");
            set => this["passCode"] = value;
        }

        /// <summary>
        /// Gets or sets the remember device flag
        /// </summary>
        /// <value>The remember device flag</value>
        public bool? RememberDevice
        {
            get => GetBooleanProperty("rememberDevice");
            set => this["rememberDevice"] = value;
        }
    }
}
