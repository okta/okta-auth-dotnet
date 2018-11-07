using System;
using System.Collections.Generic;
using System.Text;
using Okta.Authn.Abstractions;

namespace Okta.Authn.Models
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
