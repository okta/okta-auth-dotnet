using System;
using System.Collections.Generic;
using System.Text;
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
