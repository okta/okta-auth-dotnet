// <copyright file="ResourceTypeHelper.cs" company="Okta, Inc">
// Copyright (c) 2018 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Okta.Sdk.Abstractions
{
    /// <summary>
    /// A utility class for resource types.
    /// </summary>
    public class ResourceTypeHelper
    {
        /// <summary>
        /// Gets all the defined types for a specific type
        /// </summary>
        /// <param name="type">The resource type</param>
        /// <returns>All the defined types</returns>
        public static IEnumerable<TypeInfo> GetAllDefinedTypes(Type type)
            => type.GetTypeInfo().Assembly.DefinedTypes.ToArray();

        /// <summary>
        /// Gets all the defined type for BaseResource
        /// </summary>
        /// <returns>All the defined types</returns>
        public static IEnumerable<TypeInfo> AllDefinedTypes
            => typeof(BaseResource).GetTypeInfo().Assembly.DefinedTypes.ToArray();
    }
}
