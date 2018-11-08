using System;
using System.Collections.Generic;
using System.Text;

namespace Okta.Authn.POCOs
{
    public class VerifySecurityQuestionFactorOptions
    {
        public string FactorId { get; set; }

        public string StateToken { get; set; }

        public string Answer { get; set; }

        public bool? RememberDevice { get; set; }
    }
}
