// <copyright file="RecoveryType.cs" company="Okta, Inc">
// Copyright (c) 2018 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

using Okta.Sdk.Abstractions;

namespace Okta.Auth.Sdk
{
    public class RecoveryType : StringEnum
    {
        public RecoveryType(string value)
            : base(value)
        {
        }

        public static readonly RecoveryType Password = new RecoveryType("PASSWORD");

        public static readonly RecoveryType Unlock = new RecoveryType("UNLOCK");
    }
}
