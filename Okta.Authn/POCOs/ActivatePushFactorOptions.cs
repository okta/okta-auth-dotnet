using System;
using System.Collections.Generic;
using System.Text;

namespace Okta.Authn.POCOs
{
    public class ActivatePushFactorOptions
    {
        public string FactorId { get; set; }

        public string StateToken { get; set; }
    }
}
