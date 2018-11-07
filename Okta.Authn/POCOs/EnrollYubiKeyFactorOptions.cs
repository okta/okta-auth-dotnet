using System;
using System.Collections.Generic;
using System.Text;
using Okta.Authn.Models;

namespace Okta.Authn.POCOs
{
    public class EnrollYubiKeyFactorOptions
    {
        public string StateToken { get; set; }

        public string CredentialId { get; set; }

        public string Provider { get; set; } = OktaDefaults.YubiKeyProvider;

        public string PassCode { get; set; }
    }
}
