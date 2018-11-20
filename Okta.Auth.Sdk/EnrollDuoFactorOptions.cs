﻿// <copyright file="EnrollDuoFactorOptions.cs" company="Okta, Inc">
// Copyright (c) 2018 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

using Okta.Auth.Sdk.Models;

namespace Okta.Auth.Sdk
{
    public class EnrollDuoFactorOptions
    {
        public string StateToken { get; set; }

        public string Provider { get; set; } = OktaDefaults.DuoProvider;
    }
}