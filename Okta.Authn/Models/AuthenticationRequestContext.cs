using Okta.Sdk.Abstractions;

namespace Okta.Authn.Models
{
    public class AuthenticationRequestContext : BaseResource
    {
        public string DeviceToken
        {
            get => GetStringProperty("deviceToken");
            set => this["deviceToken"] = value;
        }
    }
}
