    // <copyright file="VerifyWebAuthnFactorRequest.cs" company="Okta, Inc">

    // Copyright (c) 2018 - present Okta, Inc. All rights reserved.
    // Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
    // </copyright>

    namespace Okta.Auth.Sdk.Models
    {
    /// <summary>
    /// This class represents the body of the verify WebAuthn factor request
    /// </summary>
    public class VerifyWebAuthnFactorRequest : Resource
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
    }
}
