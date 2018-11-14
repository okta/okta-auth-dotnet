// <copyright file="ResourceTypeResolverFactory.cs" company="Okta, Inc">
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
    public abstract class AbstractResourceTypeResolverFactory
    {
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

        public abstract IEnumerable<TypeInfo> GetAllResourceDefinedTypes();

        protected IEnumerable<(Type Resolver, Type For)> GetAllResolvers()
        {
            var resolvers = GetAllResolvers(GetAllResourceDefinedTypes());
            
            return resolvers;
        }
         
        public bool RequiresResolution(Type resourceType)
            => GetResolver(resourceType) != null;

        public IResourceTypeResolver CreateResolver<T>()
            => CreateResolver(forType: typeof(T));

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
