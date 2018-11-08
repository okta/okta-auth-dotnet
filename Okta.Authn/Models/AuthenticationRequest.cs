using Okta.Authn.Abstractions;
using Okta.Sdk.Abstractions;

namespace Okta.Authn.Models
{
    public class AuthenticationRequest : Resource
    {
        public string Username
        {
            get => GetStringProperty("username");
            set => this["username"] = value;
        }

        public string Password
        {
            get => GetStringProperty("password");
            set => this["password"] = value;
        }

        public string Audience
        {
            get => GetStringProperty("audience");
            set => this["audience"] = value;
        }

        public string RelayState
        {
            get => GetStringProperty("relayState");
            set => this["relayState"] = value;
        }

        public AuthenticationRequestOptions Options
        {
            get => GetResourceProperty<AuthenticationRequestOptions>("options");
            set => this["options"] = value;
        }

        public AuthenticationRequestContext Context
        {
            get => GetResourceProperty<AuthenticationRequestContext>("context");
            set => this["context"] = value;
        }
    }
}
