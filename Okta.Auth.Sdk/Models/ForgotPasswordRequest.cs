// <copyright file="ForgotPasswordRequest.cs" company="Okta, Inc">
// Copyright (c) 2018 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

namespace Okta.Auth.Sdk.Models
{
    public class ForgotPasswordRequest : Resource
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

        public string FactorType
        {
            get => GetStringProperty("factorType");
            set => this["factorType"] = value;
        }
    }
}
