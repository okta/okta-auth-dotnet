// <copyright file="VerifySecurityQuestionFactorRequest.cs" company="Okta, Inc">
// Copyright (c) 2018 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

using Okta.Authn.Abstractions;

namespace Okta.Authn.Models
{
    public class VerifySecurityQuestionFactorRequest : Resource
    {
        public string StateToken
        {
            get => GetStringProperty("stateToken");
            set => this["stateToken"] = value;
        }

        public bool? RememberDevice
        {
            get => GetBooleanProperty("rememberDevice");
            set => this["rememberDevice"] = value;
        }

        public string Answer
        {
            get => GetStringProperty("answer");
            set => this["answer"] = value;
        }
    }
}
