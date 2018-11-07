using System;
using System.Collections.Generic;
using System.Text;
using Okta.Authn.Models;

namespace Okta.Authn.POCOs
{
    public class EnrollCallFactorOptions
    {
        public string StateToken { get; set; }

        public string Provider { get; set; } = OktaDefaults.OktaProvider;

        public string PhoneNumber { get; set; }

        public string PhoneExtension { get; set; }
    }
}
