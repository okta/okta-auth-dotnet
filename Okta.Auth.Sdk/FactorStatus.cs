// <copyright file="FactorStatus.cs" company="Okta, Inc">
// Copyright (c) 2018 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

using Okta.Sdk.Abstractions;

namespace Okta.Auth.Sdk
{
    /// <summary>
    /// This class represents the possible factor statuses
    /// </summary>
    public class FactorStatus : StringEnum
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FactorStatus"/> class.
        /// </summary>
        /// <param name="value">The factor status value</param>
        public FactorStatus(string value)
            : base(value)
        {
        }

        /// <summary>
        /// No setup
        /// </summary>
        public static readonly FactorStatus NoSetup = new FactorStatus("NOT_SETUP");

        /// <summary>
        /// Pending activation
        /// </summary>
        public static readonly FactorStatus PendingActivation = new FactorStatus("PENDING_ACTIVATION");

        /// <summary>
        /// Enrolled
        /// </summary>
        public static readonly FactorStatus Enrolled = new FactorStatus("ENROLLED");

        /// <summary>
        /// Active
        /// </summary>
        public static readonly FactorStatus Active = new FactorStatus("ACTIVE");

        /// <summary>
        /// Inactive
        /// </summary>
        public static readonly FactorStatus Inactive = new FactorStatus("INACTIVE");

        /// <summary>
        /// Expired
        /// </summary>
        public static readonly FactorStatus Expired = new FactorStatus("EXPIRED");
    }
}
