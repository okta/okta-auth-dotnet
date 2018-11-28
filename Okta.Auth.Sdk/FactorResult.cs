// <copyright file="FactorResult.cs" company="Okta, Inc">
// Copyright (c) 2018 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

using Okta.Sdk.Abstractions;

namespace Okta.Auth.Sdk
{
    public class FactorResult : StringEnum
    {
        public FactorResult(string value)
            : base(value)
        {
        }

        public static readonly FactorResult Waiting = new FactorResult("WAITING");

        public static readonly FactorResult Cancelled = new FactorResult("CANCELLED");

        public static readonly FactorResult TimeOut = new FactorResult("TIMEOUT");

        public static readonly FactorResult TimeWindowExceeded = new FactorResult("TIME_WINDOW_EXCEEDED");

        public static readonly FactorResult PassCodeReplayed = new FactorResult("PASSCODE_REPLAYED");

        public static readonly FactorResult Error = new FactorResult("ERROR");
    }
}
