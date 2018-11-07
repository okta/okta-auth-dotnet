using System;
using System.Collections.Generic;
using System.Text;
using Okta.Authn.Abstractions;
using Okta.Sdk.Abstractions;

namespace Okta.Authn.Models
{
    public class Factor : Resource
    {
        public string Type => GetStringProperty("factorType");

        public string Provider => GetStringProperty("provider");

        public string VendorName => GetStringProperty("vendorName");

        // TODO: FactorStatus enum?
        public string Status => GetStringProperty("status");

        public string Enrollment => GetStringProperty("enrollment");

        public Resource Links => GetResourceProperty<Resource>("_links");

        public Resource Profile => GetResourceProperty<Resource>("profile");
    }
}
