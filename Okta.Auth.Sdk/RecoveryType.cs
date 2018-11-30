// <copyright file="RecoveryType.cs" company="Okta, Inc">
// Copyright (c) 2018 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

using Okta.Sdk.Abstractions;

namespace Okta.Auth.Sdk
{
    /// <summary>
    /// This class represents the possible recovery types
    /// </summary>
    public class RecoveryType : StringEnum
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RecoveryType"/> class.
        /// </summary>
        /// <param name="value">The recovery type value</param>
        public RecoveryType(string value)
            : base(value)
        {
        }

        /// <summary>
        /// Password
        /// </summary>
        public static readonly RecoveryType Password = new RecoveryType("PASSWORD");

        /// <summary>
        /// Unlock
        /// </summary>
        public static readonly RecoveryType Unlock = new RecoveryType("UNLOCK");
    }
}
