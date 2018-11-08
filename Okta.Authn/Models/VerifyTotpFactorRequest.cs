using System;
using System.Collections.Generic;
using System.Text;
using Okta.Authn.Abstractions;

namespace Okta.Authn.Models
{
    public class VerifyTotpFactorRequest : Resource
    {
        public string StateToken
        {
            get => GetStringProperty("stateToken");
            set => this["stateToken"] = value;
        }

        public string PassCode
        {
            get => GetStringProperty("passCode");
            set => this["passCode"] = value;
        }

        public bool? RememberDevice
        {
            get => GetBooleanProperty("rememberDevice");
            set => this["rememberDevice"] = value;
        }
    }
}
