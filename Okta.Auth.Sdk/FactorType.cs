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

        public static FactorType Sms = new FactorType("sms");

        public static FactorType Question = new FactorType("question");

        public static FactorType Email = new FactorType("email");

        public static FactorType Call = new FactorType("call");

        public static FactorType Push = new FactorType("push");

        public static FactorType Token = new FactorType("token");

        public static FactorType TokenHardware = new FactorType("token:hardware");

        public static FactorType TokenSoftware = new FactorType("token:software");

        public static FactorType TokenSoftwareTotp = new FactorType("token:software:totp");

        public static FactorType Web = new FactorType("web");

        public static FactorType U2f = new FactorType("u2f");
    }
}
