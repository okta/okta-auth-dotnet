// <copyright file="FactorResult.cs" company="Okta, Inc">
// Copyright (c) 2018 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

using Okta.Sdk.Abstractions;

namespace Okta.Auth.Sdk
{
    /// <summary>
    /// This class represents the possible factor result statuses
    /// </summary>
    public class FactorResult : StringEnum
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FactorResult"/> class.
        /// </summary>
        /// <param name="value">The factor result value</param>
        public FactorResult(string value)
            : base(value)
        {
        }

        /// <summary>
        /// Waiting
        /// </summary>
        public static readonly FactorResult Waiting = new FactorResult("WAITING");

        /// <summary>
        /// Cancelled
        /// </summary>
        public static readonly FactorResult Cancelled = new FactorResult("CANCELLED");

        /// <summary>
        /// Timeout
        /// </summary>
        public static readonly FactorResult TimeOut = new FactorResult("TIMEOUT");

        /// <summary>
        /// Time window exceeded
        /// </summary>
        public static readonly FactorResult TimeWindowExceeded = new FactorResult("TIME_WINDOW_EXCEEDED");

        /// <summary>
        /// Pass code replayed
        /// </summary>
        public static readonly FactorResult PassCodeReplayed = new FactorResult("PASSCODE_REPLAYED");

        /// <summary>
        /// Error
        /// </summary>
        public static readonly FactorResult Error = new FactorResult("ERROR");
    }
}
