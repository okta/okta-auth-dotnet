// <copyright file="VerifyU2fFactorRequest.cs" company="Okta, Inc">
// Copyright (c) 2018 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

namespace Okta.Auth.Sdk.Models
{
    /// <summary>
    /// This class represents the body of the verify U2F factor request
    /// </summary>
    public class VerifyU2fFactorRequest : Resource
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
        /// Gets or sets the client data
        /// </summary>
        /// <value>The client data</value>
        public string ClientData
        {
            get => GetStringProperty("clientData");
            set => this["clientData"] = value;
        }

        /// <summary>
        /// Gets or sets the signature data
        /// </summary>
        /// <value>The signature data</value>
        public string SignatureData
        {
            get => GetStringProperty("signatureData");
            set => this["signatureData"] = value;
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
