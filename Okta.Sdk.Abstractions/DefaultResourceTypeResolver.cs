// <copyright file="DefaultResourceTypeResolver.cs" company="Okta, Inc">
// Copyright (c) 2018 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

using System;
using System.Collections.Generic;
using Okta.Sdk.Abstractions.Internal;

namespace Okta.Sdk.Abstractions
{
    internal sealed class DefaultResourceTypeResolver<T> : AbstractResourceTypeResolver<T>
    {
        public override AbstractResourceTypeResolverFactory ResourceTypeResolverFactory
        {
            get
            {
                if (_resourceTypeResolverFactory == null)
                {
                    _resourceTypeResolverFactory = new AbstractResourceTypeResolverFactory(ResourceTypeHelper.AllDefinedTypes);
                }

                return _resourceTypeResolverFactory;
            }

            set
            {
                this._resourceTypeResolverFactory = value;
            }
        }

        protected override Type GetResolvedTypeInternal(IDictionary<string, object> data)
            => typeof(T);
    }
}
