// <copyright file="DefaultSerializerShould.cs" company="Okta, Inc">
// Copyright (c) 2018 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Okta.Sdk.Abstractions.UnitTests.Internal;
using Xunit;

namespace Okta.Sdk.Abstractions.UnitTests
{
    public class DefaultSerializerShould
    {
        [Fact]
        public void DeserializeDictionaries()
        {
            var json = @"
{
  ""foo"": ""bar"",
  ""baz"": 123
}";

            var serializer = new DefaultSerializer();
            var dict = serializer.Deserialize(json);

            dict["foo"].Should().Be("bar");
            dict["baz"].Should().Be(123L);
        }

        [Fact]
        public void DeserializeNestedDictionaries()
        {
            var json = @"
{
  ""foo"": ""bar"",
  ""baz"": {
    ""qux"": 123
  }
}";

            var serializer = new DefaultSerializer();
            var dict = serializer.Deserialize(json);

            dict["foo"].Should().Be("bar");
            dict["baz"].Should().BeAssignableTo<IReadOnlyDictionary<string, object>>();
            (dict["baz"] as IReadOnlyDictionary<string, object>)["qux"].Should().Be(123L);
        }

        [Fact]
        public void DeserializeNullInput()
        {
            var serializer = new DefaultSerializer();

            var dict = serializer.Deserialize(null);

            dict.Count.Should().Be(0);
        }

        [Fact]
        public void DeserializeEmptyInput()
        {
            var serializer = new DefaultSerializer();

            var dict = serializer.Deserialize(string.Empty);

            dict.Count.Should().Be(0);
        }

        [Fact]
        public void DeserializeArraysAsLists()
        {
            var json = @"
{
  ""things"": [
    ""foo"", ""bar"", ""baz""
  ]
}";

            var serializer = new DefaultSerializer();
            var dict = serializer.Deserialize(json);

            dict["things"].Should().NotBeNull();

            var things = dict["things"] as IList<object>;
            things.OfType<string>().Should().BeEquivalentTo("foo", "bar", "baz");
        }

        [Fact]
        public void SerializeObject()
        {
            var serializer = new DefaultSerializer();

            var json = serializer.Serialize(new { foo = "bar" });

            json.Should().Be("{\"foo\":\"bar\"}");
        }

        [Fact]
        public void SerializeEnum()
        {
            var serializer = new DefaultSerializer();

            var json = serializer.Serialize(new { test = TestEnum.Foo });

            json.Should().Be("{\"test\":\"FOO\"}");
        }

        [Fact]
        public void SerializeEmptyEnum()
        {
            var serializer = new DefaultSerializer();

            var json = serializer.Serialize(new { test = new TestEnum(string.Empty) });

            json.Should().Be("{\"test\":\"\"}");
        }

        [Fact]
        public void SerializeNullEnum()
        {
            var serializer = new DefaultSerializer();

            var json = serializer.Serialize(new { test = new TestEnum(null) });

            json.Should().Be("{\"test\":\"\"}");
        }

        [Fact]
        public void SerializeObjectAndPreserveCase()
        {
            var serializer = new DefaultSerializer();

            var json = serializer.Serialize(new { Foo = "bar" });

            json.Should().Be("{\"Foo\":\"bar\"}");
        }

        [Fact]
        public void SerializeResource()
        {
            var serializer = new DefaultSerializer();

            var nestedResource = new TestNestedResource()
            {
                Nested = new TestNestedResource()
                {
                    Nested = new TestNestedResource()
                    {
                        Foo = "foobar",
                    }
                }
            };

            var json = serializer.Serialize(nestedResource);

            json.Should().Be("{\"nested\":{\"nested\":{\"foo\":\"foobar\"}}}");
        }

        [Fact]
        public void SerializeResourceWithCustomProperties()
        {
            var serializer = new DefaultSerializer();

            var customResource = new TestNestedResource()
            {
                Foo = "foobar",
            };

            customResource["Custom"] = "Bar";

            var foo = new TestNestedResource()
            {
                Nested = customResource,
            };
            

            var json = serializer.Serialize(foo);

            json.Should().Be("{\"nested\":{\"foo\":\"foobar\",\"Custom\":\"Bar\"}}");
        }
    }
}
