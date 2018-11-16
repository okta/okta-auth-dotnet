// <copyright file="UnlockAccountRequest.cs" company="Okta, Inc">
// Copyright (c) 2018 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

namespace Okta.Auth.Sdk.Models
{
    public class UnlockAccountRequest : Resource
    {
        public string Username
        {
            get => GetStringProperty("username");
            set => this["username"] = value;
        }

        public string RelayState
        {
            get => GetStringProperty("relayState");
            set => this["relayState"] = value;
        }

        public FactorType FactorType
        {
            get => GetEnumProperty<FactorType>("factorType");
            set => this["factorType"] = value;
        }
    }
}
