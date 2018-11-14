// <copyright file="EnrollSecurityQuestionFactorOptions.cs" company="Okta, Inc">
// Copyright (c) 2018 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

using Okta.Authn.Models;

namespace Okta.Authn.POCOs
{
    public class EnrollSecurityQuestionFactorOptions
    {
        public string StateToken { get; set; }

        public string Provider { get; set; } = OktaDefaults.OktaProvider;

        public string Question { get; set; }

        public string QuestionText { get; set; }

        public string Answer { get; set; }
    }
}
