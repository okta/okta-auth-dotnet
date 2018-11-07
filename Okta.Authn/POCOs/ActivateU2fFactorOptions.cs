using System;
using System.Collections.Generic;
using System.Text;

namespace Okta.Authn.POCOs
{
    public class ActivateU2fFactorOptions
    {
        public string FactorId { get; set; }

        public string StateToken { get; set; }

        public string ClientData { get; set; }

        public string RegistrationData { get; set; }
    }
}
