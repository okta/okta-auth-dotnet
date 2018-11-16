// <copyright file="AnswerRecoveryQuestionRequest.cs" company="Okta, Inc">
// Copyright (c) 2018 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

namespace Okta.Auth.Sdk.Models
{
    public class AnswerRecoveryQuestionRequest : Resource
    {
        public string StateToken
        {
            get => GetStringProperty("stateToken");
            set => this["stateToken"] = value;
        }

        public string Answer
        {
            get => GetStringProperty("answer");
            set => this["answer"] = value;
        }
    }
}
