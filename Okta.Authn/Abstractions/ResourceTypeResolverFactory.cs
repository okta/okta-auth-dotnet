// <copyright file="ResourceTypeResolverFactory.cs" company="Okta, Inc">
// Copyright (c) 2018 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Okta.Sdk.Abstractions;

namespace Okta.Authn.Abstractions
{
    public class ResourceTypeResolverFactory : AbstractResourceTypeResolverFactory
    {
        public override IEnumerable<TypeInfo> GetAllResourceDefinedTypes()
        {
            return typeof(Resource).GetTypeInfo().Assembly.DefinedTypes.ToArray();
        }
    }
}
