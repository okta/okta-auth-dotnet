using System;
using System.Collections.Generic;
using System.Text;
using Okta.Authn.Models;

namespace Okta.Authn.POCOs
{
    public class ForgotPasswordOptions
    {
        public string UserName { get; set; }

        public string RelayState { get; set; }

        /// <summary>
        /// Not required for Trusted Applications
        /// </summary>
        public FactorType FactorType { get; set; }
    }
}
