using System;
using System.Collections.Generic;
using System.Text;

namespace Okta.Authn.POCOs
{
    public class VerifySmsFactorOptions
    {
        public string FactorId { get; set; }

        public string StateToken { get; set; }

        public string PassCode { get; set; }

        public bool? RememberDevice { get; set; }
    }
}
