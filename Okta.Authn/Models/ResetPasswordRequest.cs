using System;
using System.Collections.Generic;
using System.Text;
using Okta.Authn.Abstractions;

namespace Okta.Authn.Models
{
    public class ResetPasswordRequest : Resource
    {
        public string StateToken
        {
            get => GetStringProperty("stateToken");
            set => this["stateToken"] = value;
        }

        public string NewPassword
        {
            get => GetStringProperty("newPassword");
            set => this["newPassword"] = value;
        }
    }
}
