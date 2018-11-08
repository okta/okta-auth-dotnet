using System;
using System.Collections.Generic;
using System.Text;

namespace Okta.Authn.POCOs
{
    public class ResendChallengeOptions
    {
        public string StateToken { get; set; }

        public string FactorId { get; set; }
    }
}
