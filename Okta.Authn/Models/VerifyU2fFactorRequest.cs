// <copyright file="VerifyU2fFactorRequest.cs" company="Okta, Inc">
// Copyright (c) 2018 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

using Okta.Auth.Sdk.Abstractions;

namespace Okta.Auth.Sdk.Models
{
    public class VerifyU2fFactorRequest : Resource
    {
        public string StateToken
        {
            get => GetStringProperty("stateToken");
            set => this["stateToken"] = value;
        }

        public string ClientData
        {
            get => GetStringProperty("clientData");
            set => this["clientData"] = value;
        }

        public string SignatureData
        {
            get => GetStringProperty("signatureData");
            set => this["signatureData"] = value;
        }

        public bool? RememberDevice
        {
            get => GetBooleanProperty("rememberDevice");
            set => this["rememberDevice"] = value;
        }
    }
}
