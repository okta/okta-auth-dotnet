using System;
using System.Collections.Generic;
using System.Text;

namespace Okta.Authn.POCOs
{
    public class VerifyPushFactorOptions
    {
        public string FactorId { get; set; }

        public string StateToken { get; set; }

        public bool? RememberDevice { get; set; }

        public bool? AutoPush { get; set; }
    }
}
