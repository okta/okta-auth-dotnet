// <copyright file="FactorType.cs" company="Okta, Inc">
// Copyright (c) 2018 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

using Okta.Sdk.Abstractions;

namespace Okta.Auth.Sdk
{
    public class FactorType : StringEnum
    {
        public FactorType(string value)
            : base(value)
        {
        }

        public static readonly FactorType Sms = new FactorType("sms");

        public static readonly FactorType Question = new FactorType("question");

        public static readonly FactorType Email = new FactorType("email");

        public static readonly FactorType Call = new FactorType("call");

        public static readonly FactorType Push = new FactorType("push");

        public static readonly FactorType Token = new FactorType("token");

        public static readonly FactorType TokenHardware = new FactorType("token:hardware");

        public static readonly FactorType TokenSoftware = new FactorType("token:software");

        public static readonly FactorType TokenSoftwareTotp = new FactorType("token:software:totp");

        public static readonly FactorType Web = new FactorType("web");

        public static readonly FactorType U2f = new FactorType("u2f");
    }
}
