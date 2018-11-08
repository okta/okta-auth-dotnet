using System;
using System.Collections.Generic;
using System.Text;
using Okta.Authn.Models;

namespace Okta.Authn.POCOs
{
    public class UnlockAccountOptions
    {
        public string UserName { get; set; }

        public string RelayState { get; set; }

        /// <summary>
        /// Email/SMS or null
        /// </summary>
        public FactorType FactorType { get; set; }
    }
}
