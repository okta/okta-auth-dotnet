using System;
using System.Collections.Generic;
using System.Text;
using Okta.Authn.Models;

namespace Okta.Authn.POCOs
{
    public class ActivateFactorOptions
    {
        public string StateToken { get; set; }

        public string FactorId { get; set; }

        public FactorType FactorType { get; set; }

        public string PassCode { get; set; }
    }
}
