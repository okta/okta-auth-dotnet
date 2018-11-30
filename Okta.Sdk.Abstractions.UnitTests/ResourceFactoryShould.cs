// <copyright file="ResourceFactoryShould.cs" company="Okta, Inc">
// Copyright (c) 2018 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

using System;
using System.Collections.Generic;
using System.Net.Mime;
using FluentAssertions;
using Okta.Sdk.Abstractions.UnitTests.Internal;
using Xunit;

namespace Okta.Sdk.Abstractions.UnitTests
{
    public class ResourceFactoryShould
    {
        public class SomeClass // Does not derive from Resource
        {
            public string Foo { get; set; }
        }

        [Fact]
        public void ThrowIfCreateNewTDoesNotDeriveFromResource()
        {
            var factory = new ResourceFactory(null, null, null);
            var fakeData = new Dictionary<string, object>();

            Assert.Throws<InvalidOperationException>(() => factory.CreateNew<string>(fakeData));
            Assert.Throws<InvalidOperationException>(() => factory.CreateNew<SomeClass>(fakeData));
        }

        [Fact]
        public void ThrowIfCreateFromExistingDataTDoesNotDeriveFromResource()
        {
            var factory = new ResourceFactory(null, null, null);
            var fakeData = new Dictionary<string, object>();

            Assert.Throws<InvalidOperationException>(() => factory.CreateFromExistingData<string>(fakeData));
            Assert.Throws<InvalidOperationException>(() => factory.CreateFromExistingData<SomeClass>(fakeData));
        }

        /// <summary>
        /// Tests whether resource types that do not require resolution are instantiated correctly.
        /// </summary>
        [Fact]
        public void CreateEntityFromExistingData()
        {
            var factory = new ResourceFactory(null, null, null);
            var fakeData = new Dictionary<string, object>
            {
                {"foo", "foobar"},
            };

            var user = factory.CreateFromExistingData<TestResource>(fakeData);

            user.Should().NotBeNull();
            user.Should().BeOfType<TestResource>();
            user.Foo.Should().Be("foobar");
        }
    }
}
