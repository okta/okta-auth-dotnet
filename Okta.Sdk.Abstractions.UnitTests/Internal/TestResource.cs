// <copyright file="TestResource.cs" company="Okta, Inc">
// Copyright (c) 2018 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

namespace Okta.Sdk.Abstractions.UnitTests.Internal
{
    /// <summary>
    /// Dummy resource that is used for unit tests that involve serialization/deserialization.
    /// </summary>
    public class TestResource : BaseResource
    {
        public TestResource()
            : base()
        {
        }

        public string Foo
        {
            get => GetStringProperty("foo");
            set => this["foo"] = value;
        }

        public string Baz
        {
            get => GetStringProperty("baz");
            set => this["baz"] = value;
        }

        public bool? Bar
        {
            get => GetBooleanProperty("bar");
            set => this["bar"] = value;
        }
    }
}
