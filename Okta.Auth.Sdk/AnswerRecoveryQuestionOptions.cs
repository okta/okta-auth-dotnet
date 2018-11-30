// <copyright file="AnswerRecoveryQuestionOptions.cs" company="Okta, Inc">
// Copyright (c) 2018 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

namespace Okta.Auth.Sdk
{
    /// <summary>
    /// This class contains all the request parameters for answering a recovery question request
    /// </summary>
    public class AnswerRecoveryQuestionOptions
    {
        /// <summary>
        /// Gets or sets the state token
        /// </summary>
        /// <value>The state token</value>
        public string StateToken { get; set; }

        /// <summary>
        /// Gets or sets the answer
        /// </summary>
        /// <value>The answer</value>
        public string Answer { get; set; }
    }
}
