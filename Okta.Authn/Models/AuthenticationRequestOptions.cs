
using Okta.Sdk.Abstractions;

namespace Okta.Authn.Models
{
    public class AuthenticationRequestOptions : BaseResource
    {
        public bool? MultiOptionalFactorEnroll
        {
            get => GetBooleanProperty("multiOptionalFactorEnroll");
            set => this["multiOptionalFactorEnroll"] = value;
        }

        public bool? WarnBeforePasswordExpired
        {
            get => GetBooleanProperty("warnBeforePasswordExpired");
            set => this["multiOptionalFactorEnroll"] = value;
        }
    }
}
