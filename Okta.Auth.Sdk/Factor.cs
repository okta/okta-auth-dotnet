// <copyright file="Factor.cs" company="Okta, Inc">
// Copyright (c) 2018 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

namespace Okta.Auth.Sdk
{
    /// <summary>
    /// This class represents a factor
    /// </summary>
    public class Factor : Resource
    {
        /// <summary>
        /// Gets the factor id
        /// </summary>
        /// <value>The factor id</value>
        public string Id => GetStringProperty("id");

        /// <summary>
        /// Gets the factor type
        /// </summary>
        /// <value>The factor type</value>
        public string Type => GetStringProperty("factorType");

        /// <summary>
        /// Gets the provider
        /// </summary>
        /// <value>The provider</value>
        public string Provider => GetStringProperty("provider");

        /// <summary>
        /// Gets the vendor name
        /// </summary>
        /// <value>The vendor name</value>
        public string VendorName => GetStringProperty("vendorName");

        /// <summary>
        /// Gets the status
        /// </summary>
        /// <value>The status</value>
        public FactorStatus Status => GetEnumProperty<FactorStatus>("status");

        /// <summary>
        /// Gets the enrollment
        /// </summary>
        /// <value>The enrollment</value>
        public string Enrollment => GetStringProperty("enrollment");

        /// <summary>
        /// Gets the embedded
        /// </summary>
        /// <value>The embedded</value>
        public Resource Embedded => GetResourceProperty<Resource>("_embedded");

        /// <summary>
        /// Gets the links
        /// </summary>
        /// <value>The links</value>
        public Resource Links => GetResourceProperty<Resource>("_links");

        /// <summary>
        /// Gets the profile
        /// </summary>
        /// <value>The profile</value>
        public Resource Profile => GetResourceProperty<Resource>("profile");
    }
}
