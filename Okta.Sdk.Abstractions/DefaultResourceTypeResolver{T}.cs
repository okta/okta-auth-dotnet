// <copyright file="DefaultResourceTypeResolver{T}.cs" company="Okta, Inc">
// Copyright (c) 2014 - present Okta, Inc. All rights reserved.
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
            set { this._resourceTypeResolverFactory = value; }

            get
            {
                if (_resourceTypeResolverFactory == null)
                {
                    _resourceTypeResolverFactory = new DefaultResourceTypeResolverFactory();
                }

                return _resourceTypeResolverFactory;
            }
        }

        protected override Type GetResolvedTypeInternal(IDictionary<string, object> data)
            => typeof(T);

    }
}
