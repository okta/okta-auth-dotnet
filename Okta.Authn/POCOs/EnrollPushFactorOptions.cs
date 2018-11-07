using System;
using System.Collections.Generic;
using System.Text;
using Okta.Authn.Models;

namespace Okta.Authn.POCOs
{
    public class EnrollPushFactorOptions
    {
        public string StateToken { get; set; }

        public string Provider { get; set; } = OktaDefaults.OktaProvider;
    }
}
