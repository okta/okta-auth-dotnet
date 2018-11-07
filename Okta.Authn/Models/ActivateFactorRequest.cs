using System;
using System.Collections.Generic;
using System.Text;
using Okta.Authn.Abstractions;

namespace Okta.Authn.Models
{
    public class ActivateFactorRequest : Resource
    {
        public string StateToken
        {
            get => GetStringProperty("stateToken");
            set => this["stateToken"] = value;
        }
        
        public string FactorType
        {
            get => GetStringProperty("factorType");
            set => this["factorType"] = value;
        }

        /// <summary>
        /// PassCode must be provided for TOTP, SMS & Call
        /// </summary>
        public string PassCode
        {
            get => GetStringProperty("passCode");
            set => this["passCode"] = value;
        }


    }
}
