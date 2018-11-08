using System;
using System.Collections.Generic;
using System.Text;
using Okta.Authn.Abstractions;

namespace Okta.Authn.Models
{
    public class VerifySmsFactorRequest : Resource
    {
        public string StateToken
        {
            get => GetStringProperty("stateToken");
            set => this["stateToken"] = value;
        }

        public bool? RememberDevice
        {
            get => GetBooleanProperty("rememberDevice");
            set => this["rememberDevice"] = value;
        }

        public string PassCode
        {
            get => GetStringProperty("passCode");
            set => this["passCode"] = value;
        }
    }
}
