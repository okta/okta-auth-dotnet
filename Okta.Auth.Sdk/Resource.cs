﻿// <copyright file="Resource.cs" company="Okta, Inc">
// Copyright (c) 2018 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.Logging;
using Okta.Auth.Sdk.Models;
using Okta.Sdk.Abstractions;

namespace Okta.Auth.Sdk
{
    public class Resource : BaseResource
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Resource"/> class.
        /// </summary>
        public Resource()
        {
            Initialize(null, null, null, null);
        }

        internal void Initialize(
            IAuthenticationClient client,
            ResourceFactory resourceFactory,
            IDictionary<string, object> data,
            ILogger logger)
        {
            resourceFactory = resourceFactory ?? new ResourceFactory(client, logger, new AbstractResourceTypeResolverFactory(Resource.AllDefinedTypes));
            base.Initialize(client, resourceFactory, data, logger);
        }

        /// <summary>
        /// Gets the <see cref="IOktaClient">OktaClient</see> that created this resource.
        /// </summary>
        /// <returns>The <see cref="IOktaClient">OktaClient</see> that created this resource.</returns>
        protected new IAuthenticationClient GetClient()
        {
            return (IAuthenticationClient)_client ?? throw new InvalidOperationException("Only resources retrieved or saved through a Client object cna call server-side methods.");
        }

        /// <summary>
        /// Gets All Resource defined types
        /// </summary>
        public static new IEnumerable<TypeInfo> AllDefinedTypes
        {
            get { return typeof(Resource).GetTypeInfo().Assembly.DefinedTypes.ToArray(); }
        }
    }
}