// <copyright file="FactorStatus.cs" company="Okta, Inc">
// Copyright (c) 2018 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

using Okta.Sdk.Abstractions;

namespace Okta.Auth.Sdk
{
    public class FactorStatus : StringEnum
    {
        public FactorStatus(string value)
            : base(value)
        {
        }

        public static readonly FactorStatus NoSetup = new FactorStatus("NOT_SETUP");

        public static readonly FactorStatus PendingActivation = new FactorStatus("PENDING_ACTIVATION");

        public static readonly FactorStatus Enrolled = new FactorStatus("ENROLLED");

        public static readonly FactorStatus Active = new FactorStatus("ACTIVE");

        public static readonly FactorStatus Inactive = new FactorStatus("INACTIVE");

        public static readonly FactorStatus Expired = new FactorStatus("EXPIRED");
    }
}
