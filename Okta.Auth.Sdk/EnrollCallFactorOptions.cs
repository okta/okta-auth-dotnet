// <copyright file="EnrollCallFactorOptions.cs" company="Okta, Inc">
// Copyright (c) 2018 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

using Okta.Auth.Sdk.Models;

namespace Okta.Auth.Sdk
{
    public class EnrollCallFactorOptions
    {
        public string StateToken { get; set; }

        public string Provider { get; set; } = OktaDefaults.OktaProvider;

        public string PhoneNumber { get; set; }

        public string PhoneExtension { get; set; }

        /// <summary>
        /// Gets or sets the factor id. It is used for resend a call.
        /// </summary>
        public string FactorId { get; set; }
    }
}
