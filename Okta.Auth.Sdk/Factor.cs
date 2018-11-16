// <copyright file="Factor.cs" company="Okta, Inc">
// Copyright (c) 2018 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

namespace Okta.Auth.Sdk
{
    public class Factor : Resource
    {
        public string Id => GetStringProperty("id");

        public string Type => GetStringProperty("factorType");

        public string Provider => GetStringProperty("provider");

        public string VendorName => GetStringProperty("vendorName");

        // TODO: FactorStatus enum?
        public string Status => GetStringProperty("status");

        public string Enrollment => GetStringProperty("enrollment");

        public Resource Embedded => GetResourceProperty<Resource>("_embedded");

        public Resource Links => GetResourceProperty<Resource>("_links");

        public Resource Profile => GetResourceProperty<Resource>("profile");
    }
}
