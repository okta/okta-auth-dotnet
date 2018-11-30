// <copyright file="ResourceFactory.cs" company="Okta, Inc">
// Copyright (c) 2018 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace Okta.Sdk.Abstractions
{
    /// <summary>
    /// Constructs <see cref="BaseResource"/>s based on deserialized dictionaries.
    /// </summary>
    public sealed class ResourceFactory
    {
        private readonly IOktaClient _client;
        private readonly ILogger _logger;
        private readonly AbstractResourceTypeResolverFactory _resourceTypeResolverFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceFactory"/> class.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="logger">The logging interface.</param>
        /// <param name="resourceTypeResolverFactory">The resource type resolver factory</param>
        public ResourceFactory(IOktaClient client, ILogger logger, AbstractResourceTypeResolverFactory resourceTypeResolverFactory)
        {
            _client = client;
            _logger = logger;
            _resourceTypeResolverFactory = resourceTypeResolverFactory ?? new AbstractResourceTypeResolverFactory(ResourceTypeHelper.AllDefinedTypes);
        }

        /// <summary>
        /// Creates a new dictionary with the specified behavior.
        /// </summary>
        /// <param name="existingData">The initial dictionary data.</param>
        /// <returns>A new dictionary with the specified behavior.</returns>
        public IDictionary<string, object> NewDictionary(IDictionary<string, object> existingData)
        {
            var initialData = existingData ?? new Dictionary<string, object>();

            return new Dictionary<string, object>(initialData, StringComparer.Ordinal);
        }

        /// <summary>
        /// Creates a new <see cref="BaseResource"/> from an existing dictionary.
        /// </summary>
        /// <typeparam name="T">The <see cref="BaseResource"/> type.</typeparam>
        /// <param name="existingDictionary">The existing dictionary.</param>
        /// <returns>The created <see cref="BaseResource"/>.</returns>
        public T CreateFromExistingData<T>(IDictionary<string, object> existingDictionary)
        {
            if (!BaseResource.ResourceTypeInfo.IsAssignableFrom(typeof(T).GetTypeInfo()))
            {
                throw new InvalidOperationException("Resources must inherit from the Resource class.");
            }

            var typeResolver = _resourceTypeResolverFactory.CreateResolver<T>();
            var resourceType = typeResolver.GetResolvedType(existingDictionary);

            var resource = Activator.CreateInstance(resourceType) as BaseResource;

            resource.Initialize(_client, this, existingDictionary, _logger);

            return (T)(object)resource;
        }

        /// <summary>
        /// Creates a new <see cref="BaseResource"/> with the specified data.
        /// </summary>
        /// <typeparam name="T">The <see cref="BaseResource"/> type.</typeparam>
        /// <param name="data">The initial data.</param>
        /// <returns>The created <see cref="BaseResource"/>.</returns>
        public T CreateNew<T>(IDictionary<string, object> data)
        {
            if (!BaseResource.ResourceTypeInfo.IsAssignableFrom(typeof(T).GetTypeInfo()))
            {
                throw new InvalidOperationException("Resources must inherit from the Resource class.");
            }

            var typeResolver = _resourceTypeResolverFactory.CreateResolver<T>();
            var resourceType = typeResolver.GetResolvedType(data);

            var resource = Activator.CreateInstance(resourceType) as BaseResource;

            var dictionary = NewDictionary(data);
            resource.Initialize(_client, this, dictionary, _logger);
            return (T)(object)resource;
        }
    }
}
