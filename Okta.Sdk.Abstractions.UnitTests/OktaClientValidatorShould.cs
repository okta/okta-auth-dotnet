// <copyright file="OktaClientValidatorShould.cs" company="Okta, Inc">
// Copyright (c) 2018 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

using System;
using FluentAssertions;
using Okta.Sdk.Abstractions.Configuration;
using Xunit;

namespace Okta.Sdk.Abstractions.UnitTests
{
    public class OktaClientValidatorShould
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void FailIfOktaDomainIsNullOrEmpty(string oktaDomain)
        {
            var configuration = new OktaClientConfiguration();
            configuration.OktaDomain = oktaDomain;

            Action action = () => OktaClientConfigurationValidator.Validate(configuration);
            action.Should().Throw<ArgumentNullException>().Where(e => e.ParamName == nameof(configuration.OktaDomain));
        }

        [Theory]
        [InlineData("https://{Youroktadomain}")]
        [InlineData("https://{yourOktaDomain}")]
        [InlineData("https://{YourOktaDomain}")]
        public void FailIfOktaDomainIsNotDefined(string oktaDomain)
        {
            var configuration = new OktaClientConfiguration();
            configuration.OktaDomain = oktaDomain;

            Action action = () => OktaClientConfigurationValidator.Validate(configuration);
            action.Should().Throw<ArgumentException>().Where(e => e.ParamName == nameof(configuration.OktaDomain));
        }

        [Theory]
        [InlineData("https://foo-admin.okta.com")]
        [InlineData("https://foo-admin.oktapreview.com")]
        [InlineData("https://https://foo-admin.okta-emea.com")]
        public void FailIfOktaDomainContainsAdminKeyword(string oktaDomain)
        {
            var configuration = new OktaClientConfiguration();
            configuration.OktaDomain = oktaDomain;

            Action action = () => OktaClientConfigurationValidator.Validate(configuration);
            action.Should().Throw<ArgumentException>().Where(e => e.ParamName == nameof(configuration.OktaDomain));
        }

        [Theory]
        [InlineData("https://foo.oktapreview.com://foo")]
        [InlineData("https://foo.oktapreview.com.com")]
        public void FailIfOktaDomainHasTypo(string oktaDomain)
        {
            var configuration = new OktaClientConfiguration();
            configuration.OktaDomain = oktaDomain;

            Action action = () => OktaClientConfigurationValidator.Validate(configuration);
            action.Should().Throw<ArgumentException>().Where(e => e.ParamName == nameof(configuration.OktaDomain));
        }

        [Theory]
        [InlineData("http://myOktaDomain.oktapreview.com")]
        [InlineData("httsp://myOktaDomain.oktapreview.com")]
        [InlineData("invalidOktaDomain")]
        public void FailIfOktaDomainIsNotStartingWithHttps(string oktaDomain)
        {
            var configuration = new OktaClientConfiguration();
            configuration.OktaDomain = oktaDomain;
            configuration.DisableHttpsCheck = false;

            Action action = () => OktaClientConfigurationValidator.Validate(configuration);
            action.Should().Throw<ArgumentException>().Where(e => e.ParamName == nameof(configuration.OktaDomain));
        }

        [Theory]
        [InlineData("http://myOktaDomain.oktapreview.com")]
        [InlineData("https://myOktaDomain.oktapreview.com")]
        public void NotFailIfOktaDomainIsNotStartingWithHttpsAndDisableHttpsCheckIsTrue(string oktaDomain)
        {
            var configuration = new OktaClientConfiguration();
            configuration.OktaDomain = oktaDomain;
            configuration.DisableHttpsCheck = true;

            Action action = () => OktaClientConfigurationValidator.Validate(configuration);
            action.Should().NotThrow<ArgumentException>();
        }
    }
}
