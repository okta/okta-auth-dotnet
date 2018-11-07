using Okta.Authn.Models;

namespace Okta.Authn.POCOs
{
    public class EnrollSMSFactorOptions
    {
        public string StateToken { get; set; }
        
        public string Provider { get; set; } = OktaDefaults.OktaProvider;

        public string PhoneNumber { get; set; }

        public string PhoneExtension { get; set; }
    }
}