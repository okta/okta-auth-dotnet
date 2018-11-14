// <copyright file="ChangePasswordRequest.cs" company="Okta, Inc">
// Copyright (c) 2018 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

using Okta.Authn.Abstractions;

namespace Okta.Authn.Models
{
    public class ChangePasswordRequest : Resource
    {
        public string StateToken
        {
            get => GetStringProperty("stateToken");
            set => this["stateToken"] = value;
        }

        public string OldPassword
        {
            get => GetStringProperty("oldPassword");
            set => this["oldPassword"] = value;
        }

        public string NewPassword
        {
            get => GetStringProperty("newPassword");
            set => this["newPassword"] = value;
        }
    }
}
