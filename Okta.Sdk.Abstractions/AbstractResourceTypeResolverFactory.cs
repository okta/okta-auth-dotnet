// <copyright file="AbstractResourceTypeResolverFactory.cs" company="Okta, Inc">
// Copyright (c) 2018 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Okta.Sdk.Abstractions.Internal;

namespace Okta.Sdk.Abstractions
{
    /// <summary>
    /// This class encapsulates the logic to resolve resource types.
    /// </summary>
    public class AbstractResourceTypeResolverFactory
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractResourceTypeResolverFactory"/> class.
        /// </summary>
        /// <param name="resourceDefinedTypes">The resource types.</param>
        public AbstractResourceTypeResolverFactory(IEnumerable<TypeInfo> resourceDefinedTypes)
        {
            _resourceDefinedTypes = resourceDefinedTypes;
        }

        private IEnumerable<TypeInfo> _resourceDefinedTypes;

        private static readonly Type DefaultResolverType = typeof(DefaultResourceTypeResolver<>);

        private IEnumerable<(Type Resolver, Type For)> GetAllResolvers(IEnumerable<TypeInfo> allTypes)
        {
            return allTypes
                .Where(typeInfo =>
                {
                    if (typeInfo?.BaseType == null)
                    {
                        return false;
                    }

                    var baseTypeInfo = typeInfo.BaseType.GetTypeInfo();
                    var inheritsFromAbstractResolver = baseTypeInfo.IsGenericType
                                                       && baseTypeInfo.GetGenericTypeDefinition() == typeof(AbstractResourceTypeResolver<>);

                    return inheritsFromAbstractResolver;
                })
                .Select(typeInfo => (typeInfo.AsType(), typeInfo.BaseType.GenericTypeArguments[0]))
                .ToArray();
        }

        /// <summary>
        /// Gets all resource types.
        /// </summary>
        /// <returns>The enumeration of resource types. </returns>
        public IEnumerable<TypeInfo> GetAllResourceDefinedTypes()
        {
            return _resourceDefinedTypes;
        }

        /// <summary>
        /// Gets all resolvers.
        /// </summary>
        /// <returns>The anumeration of resolvers.</returns>
        protected IEnumerable<(Type Resolver, Type For)> GetAllResolvers()
        {
            var resolvers = GetAllResolvers(GetAllResourceDefinedTypes());

            return resolvers;
        }

        /// <summary>
        /// Indicates if a resource type requires resolution.
        /// </summary>
        /// <param name="resourceType">The resource type.</param>
        /// <returns> A bool value indicating if the resource type requires resolution.</returns>
        public bool RequiresResolution(Type resourceType)
            => GetResolver(resourceType) != null;

        /// <summary>
        /// Creates a new resolver.
        /// </summary>
        /// <typeparam name="T">The resolver type.</typeparam>
        /// <returns>The resolver.</returns>
        public IResourceTypeResolver CreateResolver<T>()
            => CreateResolver(forType: typeof(T));

        /// <summary>
        /// Creates a new resolver for a given type.
        /// </summary>
        /// <param name="forType">The resource type.</param>
        /// <returns>The resolver.</returns>
        public IResourceTypeResolver CreateResolver(Type forType)
        {
            var concreteType = GetConcreteForInterface(forType);
            var resolverType = GetResolver(concreteType);

            if (resolverType == null)
            {
                // equivalent to: new DefaultResourceTypeResolver<T>();
                var defaultResolverOfT = DefaultResolverType.MakeGenericType(concreteType);
                return (IResourceTypeResolver)Activator.CreateInstance(defaultResolverOfT);
            }

            return (IResourceTypeResolver)Activator.CreateInstance(resolverType);
        }

        private Type GetConcreteForInterface(Type possiblyInterface)
        {
            var typeInfo = possiblyInterface.GetTypeInfo();
            if (!typeInfo.IsInterface)
            {
                return possiblyInterface;
            }

            var foundConcrete = GetAllResourceDefinedTypes().FirstOrDefault(x =>
                x.IsClass == true
                && typeInfo.IsAssignableFrom(x)
                && x.BaseType.GetTypeInfo().IsSubclassOf(typeof(BaseResource)));
            return foundConcrete?.AsType() ?? possiblyInterface;
        }

        private Type GetResolver(Type resourceType)
        {
            return GetAllResolvers().FirstOrDefault(x => x.For == resourceType).Resolver;
        }
    }
}
