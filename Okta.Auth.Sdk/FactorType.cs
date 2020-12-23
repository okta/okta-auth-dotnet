// <copyright file="FactorType.cs" company="Okta, Inc">
// Copyright (c) 2018 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

using Okta.Sdk.Abstractions;

namespace Okta.Auth.Sdk
{
    /// <summary>
    /// This class represents the possible factor types
    /// </summary>
    public class FactorType : StringEnum
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FactorType"/> class.
        /// </summary>
        /// <param name="value">The factor type value</param>
        public FactorType(string value)
            : base(value)
        {
        }

        /// <summary>
        /// SMS
        /// </summary>
        public static readonly FactorType Sms = new FactorType("sms");

        /// <summary>
        /// Question
        /// </summary>
        public static readonly FactorType Question = new FactorType("question");

        /// <summary>
        /// Email
        /// </summary>
        public static readonly FactorType Email = new FactorType("email");

        /// <summary>
        /// Call
        /// </summary>
        public static readonly FactorType Call = new FactorType("call");

        /// <summary>
        /// Push
        /// </summary>
        public static readonly FactorType Push = new FactorType("push");

        /// <summary>
        /// Token
        /// </summary>
        public static readonly FactorType Token = new FactorType("token");

        /// <summary>
        /// token:hardware
        /// </summary>
        public static readonly FactorType TokenHardware = new FactorType("token:hardware");

        /// <summary>
        /// token:software
        /// </summary>
        public static readonly FactorType TokenSoftware = new FactorType("token:software");

        /// <summary>
        /// token:software:totp
        /// </summary>
        public static readonly FactorType TokenSoftwareTotp = new FactorType("token:software:totp");

        /// <summary>
        /// Web
        /// </summary>
        public static readonly FactorType Web = new FactorType("web");

        /// <summary>
        /// U2F
        /// </summary>
        public static readonly FactorType U2f = new FactorType("u2f");

        /// <summary>
        /// WebAuthn
        /// </summary>
        public static readonly FactorType WebAuthn = new FactorType("webauthn");    }
}
