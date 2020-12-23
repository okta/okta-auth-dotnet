﻿// <copyright file="ActivateU2fFactorOptions.cs" company="Okta, Inc">
// Copyright (c) 2018 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

namespace Okta.Auth.Sdk
{
    /// <summary>
    /// This class contains all the request parameters for performing an activate U2F factor request
    /// </summary>
    public class ActivateU2fFactorOptions
    {
        /// <summary>
        /// Gets or sets the factor id
        /// </summary>
        /// <value>The factor id</value>
        public string FactorId { get; set; }

        /// <summary>
        /// Gets or sets the state token
        /// </summary>
        /// <value>The state token</value>
        public string StateToken { get; set; }

        /// <summary>
        /// Gets or sets the client data
        /// </summary>
        /// <value>The client data</value>
        public string ClientData { get; set; }

        /// <summary>
        /// Gets or sets the registration data
        /// </summary>
        /// <value>The registration data</value>
        public string RegistrationData { get; set; }
    }
}
