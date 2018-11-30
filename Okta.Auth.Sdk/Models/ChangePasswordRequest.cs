// <copyright file="ChangePasswordRequest.cs" company="Okta, Inc">
// Copyright (c) 2018 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

namespace Okta.Auth.Sdk.Models
{
    /// <summary>
    /// This class represents the body of the change password request
    /// </summary>
    public class ChangePasswordRequest : Resource
    {
        /// <summary>
        /// Gets or sets the state token
        /// </summary>
        /// <value>The state token</value>
        public string StateToken
        {
            get => GetStringProperty("stateToken");
            set => this["stateToken"] = value;
        }

        /// <summary>
        /// Gets or sets the old password
        /// </summary>
        /// <value>The old password</value>
        public string OldPassword
        {
            get => GetStringProperty("oldPassword");
            set => this["oldPassword"] = value;
        }

        /// <summary>
        /// Gets or sets the new password
        /// </summary>
        /// <value>The new password</value>
        public string NewPassword
        {
            get => GetStringProperty("newPassword");
            set => this["newPassword"] = value;
        }
    }
}
