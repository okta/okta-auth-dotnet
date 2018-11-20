﻿// <copyright file="VerifyDuoFactorRequest.cs" company="Okta, Inc">
// Copyright (c) 2018 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

namespace Okta.Auth.Sdk.Models
{
    public class VerifyDuoFactorRequest : Resource
    {
        public string StateToken
        {
            get => GetStringProperty("stateToken");
            set => this["stateToken"] = value;
        }
    }
}