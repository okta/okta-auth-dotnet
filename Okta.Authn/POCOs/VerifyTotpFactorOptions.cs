// <copyright file="VerifyTotpFactorOptions.cs" company="Okta, Inc">
// Copyright (c) 2018 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

namespace Okta.Authn.POCOs
{
    public class VerifyTotpFactorOptions
    {
        public string FactorId { get; set; }

        public string StateToken { get; set; }

        public string PassCode { get; set; }

        public bool? RememberDevice { get; set; }
    }
}
