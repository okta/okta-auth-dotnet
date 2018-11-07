using System;
using System.Collections.Generic;
using System.Text;
using Okta.Authn.Abstractions;

namespace Okta.Authn.Models
{
    public class ActivateU2FFactorRequest : Resource
    {
        public string StateToken
        {
            get => GetStringProperty("stateToken");
            set => this["stateToken"] = value;
        }

        public string RegistrationData
        {
            get => GetStringProperty("registrationData");
            set => this["registrationData"] = value;
        }

        public string ClientData
        {
            get => GetStringProperty("clientData");
            set => this["clientData"] = value;
        }
    }
}
