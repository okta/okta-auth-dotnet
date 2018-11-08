using System;
using System.Collections.Generic;
using System.Text;
using Okta.Authn.Abstractions;

namespace Okta.Authn.Models
{
    public class VerifyU2fFactorRequest : Resource
    {
        public string StateToken
        {
            get => GetStringProperty("stateToken");
            set => this["stateToken"] = value;
        }

        public string ClientData
        {
            get => GetStringProperty("clientData");
            set => this["clientData"] = value;
        }

        public string SignatureData
        {
            get => GetStringProperty("signatureData");
            set => this["signatureData"] = value;
        }

        public bool? RememberDevice
        {
            get => GetBooleanProperty("rememberDevice");
            set => this["rememberDevice"] = value;
        }
    }
}
