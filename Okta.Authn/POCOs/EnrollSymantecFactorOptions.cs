using Okta.Authn.Models;

namespace Okta.Authn.POCOs
{
    public class EnrollSymantecFactorOptions
    {
        public string StateToken { get; set; }

        public string CredentialId { get; set; }

        public string Provider { get; set; } = OktaDefaults.SymantecProvider;

        public string PassCode { get; set; }

        public string NextPassCode { get; set; }
    }
}