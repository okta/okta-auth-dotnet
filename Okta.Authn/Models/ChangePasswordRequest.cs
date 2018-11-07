using Okta.Authn.Abstractions;

namespace Okta.Authn.Models
{
    public class ChangePasswordRequest : Resource
    {
        public string StateToken
        {
            get => GetStringProperty("stateToken");
            set => this["stateToken"] = value;
        }

        public string OldPassword
        {
            get => GetStringProperty("oldPassword");
            set => this["oldPassword"] = value;
        }

        public string NewPassword
        {
            get => GetStringProperty("newPassword");
            set => this["newPassword"] = value;
        }
    }
}
