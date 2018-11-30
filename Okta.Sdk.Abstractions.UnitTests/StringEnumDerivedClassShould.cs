// <copyright file="StringEnumDerivedClassShould.cs" company="Okta, Inc">
// Copyright (c) 2018 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

using FluentAssertions;
using Okta.Sdk.Abstractions.UnitTests.Internal;
using Xunit;

namespace Okta.Sdk.Abstractions.UnitTests
{
    public class StringEnumDerivedClassShould
    {
        [Fact]
        public void UseCaseInsensitiveComparison()
        {
            var userStatus1 = new TestEnum("Foo");
            var userStatus2 = TestEnum.Foo;

            (userStatus1 == userStatus2).Should().BeTrue();
        }

        [Fact]
        public void UseCaseInsensitveComparisonForGetHashCode()
        {
            var userStatus1 = new TestEnum("Foo");
            var userStatus2 = TestEnum.Foo;

            userStatus1.GetHashCode().Should().Be(userStatus2.GetHashCode());
        }
    }
}
