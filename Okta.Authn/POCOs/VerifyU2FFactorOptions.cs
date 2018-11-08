using System;

namespace Okta.Authn.POCOs
{
    public class VerifyU2FFactorOptions
    {
        public string FactorId { get; set; }

        public string StateToken { get; set; }

        public string ClientData { get; set; }

        public string SignatureData { get; set; }

        public bool? RememberDevice { get; set; }
    }
}
