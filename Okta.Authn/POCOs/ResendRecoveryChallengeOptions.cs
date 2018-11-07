using System;
using System.Collections.Generic;
using System.Text;
using Okta.Authn.Models;

namespace Okta.Authn.POCOs
{
    public class ResendRecoveryChallengeOptions
    {
        public string StateToken { get; set; }

        public FactorType FactorType { get; set; }
    }
}
