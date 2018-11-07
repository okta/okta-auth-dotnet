using System;
using System.Collections.Generic;
using System.Text;
using Okta.Authn.Models;

namespace Okta.Authn.POCOs
{
    public class EnrollRsaFactorOptions
    {
        public string StateToken { get; set; }

        public string CredentialId { get; set; }

        public string Provider { get; set; } = OktaDefaults.OktaProvider;

        public string PassCode { get; set; }
    }
}
