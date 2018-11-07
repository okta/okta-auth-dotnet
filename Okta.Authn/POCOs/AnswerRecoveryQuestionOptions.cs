using System;
using System.Collections.Generic;
using System.Text;

namespace Okta.Authn.POCOs
{
    public class AnswerRecoveryQuestionOptions
    {
        public string StateToken { get; set; }

        public string Answer { get; set; }
    }
}
