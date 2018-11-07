using System;
using System.Collections.Generic;
using System.Text;
using Okta.Authn.Abstractions;

namespace Okta.Authn.Models
{
    public class VerifyRecoveryTokenRequest : Resource
    {
        public string RecoveryToken
        {
            get => GetStringProperty("recoveryToken");
            set => this["recoveryToken"] = value;
        }
    }
}
