// <copyright file="TestableOktaClient.cs" company="Okta, Inc">
// Copyright (c) 2018 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

using System;
using System.Reflection;
using Microsoft.Extensions.Logging.Abstractions;
using Okta.Sdk.Abstractions.Configuration;

namespace Okta.Sdk.Abstractions.UnitTests.Internal
{
    public class TestableOktaClient : BaseOktaClient
    {
        public static readonly OktaClientConfiguration DefaultFakeConfiguration = new OktaClientConfiguration
        {
            OktaDomain = "https://fake.example.com",
        };

        public TestableOktaClient(IRequestExecutor requestExecutor)
            : base(
                new DefaultDataStore(requestExecutor, new DefaultSerializer(), new ResourceFactory(null, null, null), NullLogger.Instance, new UserAgentBuilder("test", typeof(BaseOktaClient).GetTypeInfo().Assembly.GetName().Version)),
                DefaultFakeConfiguration,
                new RequestContext())
        {
        }
    }
}
