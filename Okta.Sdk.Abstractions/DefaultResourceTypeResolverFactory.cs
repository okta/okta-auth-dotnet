// <copyright file="ResourceTypeResolverFactory.cs" company="Okta, Inc">
// Copyright (c) 2014 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Okta.Sdk.Abstractions
{
    public class DefaultResourceTypeResolverFactory : AbstractResourceTypeResolverFactory
    {
        public override IEnumerable<TypeInfo> GetAllResourceDefinedTypes()
        {
            return typeof(BaseResource).GetTypeInfo().Assembly.DefinedTypes.ToArray();
        }
    }
}
