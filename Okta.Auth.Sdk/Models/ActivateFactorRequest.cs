// <copyright file="ActivateFactorRequest.cs" company="Okta, Inc">
// Copyright (c) 2018 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

namespace Okta.Auth.Sdk.Models
{
    public class ActivateFactorRequest : Resource
    {
        public string StateToken
        {
            get => GetStringProperty("stateToken");
            set => this["stateToken"] = value;
        }

        public string FactorType
        {
            get => GetStringProperty("factorType");
            set => this["factorType"] = value;
        }

        /// <summary>
        /// Gets or sets the PassCode
        /// </summary>
        public string PassCode
        {
            get => GetStringProperty("passCode");
            set => this["passCode"] = value;
        }
    }
}
