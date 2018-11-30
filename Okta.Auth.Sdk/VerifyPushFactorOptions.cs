// <copyright file="VerifyPushFactorOptions.cs" company="Okta, Inc">
// Copyright (c) 2018 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

namespace Okta.Auth.Sdk
{
    /// <summary>
    /// This class contains all the request parameters for performing a verify push factor request
    /// </summary>
    public class VerifyPushFactorOptions
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
        /// Gets or sets the remember device flag
        /// </summary>
        /// <value>The remember device flag</value>
        public bool? RememberDevice { get; set; }

        /// <summary>
        /// Gets or sets the auto push flag
        /// </summary>
        /// <value>The auto push flag</value>
        public bool? AutoPush { get; set; }
    }
}
