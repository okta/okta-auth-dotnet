// <copyright file="EnrollFactorRequest.cs" company="Okta, Inc">
// Copyright (c) 2018 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

namespace Okta.Auth.Sdk.Models
{
    /// <summary>
    /// This class represents the body of the enroll factor request
    /// </summary>
    public class EnrollFactorRequest : Resource
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
        /// Gets or sets the provider
        /// </summary>
        /// <value>The provider</value>
        public string Provider
        {
            get => GetStringProperty("provider");
            set => this["provider"] = value;
        }

        /// <summary>
        /// Gets or sets the factor type
        /// </summary>
        /// <value>The factor type</value>
        public FactorType FactorType
        {
            get => GetEnumProperty<FactorType>("factorType");
            set => this["factorType"] = value;
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
        /// Gets or sets the next pass code
        /// </summary>
        /// <value>The next pass code</value>
        public string NextPassCode
        {
            get => GetStringProperty("nextPassCode");
            set => this["nextPassCode"] = value;
        }

        /// <summary>
        /// Gets or sets the profile
        /// </summary>
        /// <value>The profile</value>
        public Resource Profile
        {
            get => GetResourceProperty<Resource>("profile");
            set => this["profile"] = value;
        }
    }
}
