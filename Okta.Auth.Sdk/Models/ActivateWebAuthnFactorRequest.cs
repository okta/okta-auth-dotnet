// <copyright file="ActivateU2FFactorRequest.cs" company="Okta, Inc">
// Copyright (c) 2018 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

namespace Okta.Auth.Sdk.Models
{
    /// <summary>
    /// This class represents the body of the activate WebAuthn factor request
    /// </summary>
    public class ActivateWebAuthnFactorRequest : Resource
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
        /// Gets or sets the attestation data
        /// </summary>
        /// <value>The registration data</value>
        public string Attestation
        {
            get => GetStringProperty("attestation");
            set => this["attestation"] = value;
        }

        /// <summary>
        /// Gets or sets the client data
        /// </summary>
        /// <value>The client data</value>
        public string ClientData
        {
            get => GetStringProperty("clientData");
            set => this["clientData"] = value;
        }
    }
}
