// <copyright file="EnrollFactorRequest.cs" company="Okta, Inc">
// Copyright (c) 2018 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

using Okta.Authn.Abstractions;

namespace Okta.Authn.Models
{
    public class EnrollFactorRequest : Resource
    {
        public string StateToken
        {
            get => GetStringProperty("stateToken");
            set => this["stateToken"] = value;
        }

        public string Provider
        {
            get => GetStringProperty("provider");
            set => this["provider"] = value;
        }

        public FactorType FactorType
        {
            get => GetEnumProperty<FactorType>("factorType");
            set => this["factorType"] = value;
        }

        public string PassCode
        {
            get => GetStringProperty("passCode");
            set => this["passCode"] = value;
        }

        public string NextPassCode
        {
            get => GetStringProperty("nextPassCode");
            set => this["nextPassCode"] = value;
        }

        public Resource Profile
        {
            get => GetResourceProperty<Resource>("profile");
            set => this["profile"] = value;
        }
    }
}
