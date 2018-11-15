// <copyright file="EnrollSMSFactorOptions.cs" company="Okta, Inc">
// Copyright (c) 2018 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

using Okta.Authn.Models;

namespace Okta.Authn.POCOs
{
    public class EnrollSMSFactorOptions
    {
        public string StateToken { get; set; }

        public string Provider { get; set; } = OktaDefaults.OktaProvider;

        public string PhoneNumber { get; set; }

        public string PhoneExtension { get; set; }

        /// <summary>
        /// Gets or sets the factor id. It is used for resend a SMS.
        /// </summary>
        public string FactorId { get; set; }
    }
}
