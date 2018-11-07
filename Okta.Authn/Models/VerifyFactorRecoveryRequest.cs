using System;
using System.Collections.Generic;
using System.Text;
using Okta.Authn.Abstractions;

namespace Okta.Authn.Models
{
    public class VerifyFactorRecoveryRequest : Resource
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
    }
}
