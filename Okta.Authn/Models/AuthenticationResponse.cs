using System;
using System.Collections.Generic;
using Okta.Authn.Abstractions;

namespace Okta.Authn.Models
{
    public class AuthenticationResponse : Resource, IAuthenticationResponse
    {
        /// <inheritdoc/>
        public string StateToken => GetStringProperty("stateToken");

        /// <inheritdoc/>
        public string RelayState => GetStringProperty("relayState");

        /// <inheritdoc/>
        public string SessionToken => GetStringProperty("sessionToken");

        /// <inheritdoc/>
        public DateTimeOffset? ExpiresAt => GetDateTimeProperty("expiresAt");

        /// <inheritdoc/>
        public AuthenticationStatus AuthenticationStatus => GetEnumProperty<AuthenticationStatus>("status");

        /// <inheritdoc/>
        public Resource Embedded => GetResourceProperty<Resource>("_embedded");

        /// <inheritdoc/>
        public Resource Links => GetResourceProperty<Resource>("_links");

        /// <inheritdoc/>
        public IList<Factor> Factors => Embedded.GetArrayProperty<Factor>("factors");

        /// <inheritdoc/>
        public Factor Factor => Embedded.GetProperty<Factor>("factor");

        /// <inheritdoc/>
        public string RecoveryToken => GetStringProperty("recoveryToken");

        /// <inheritdoc/>
        /// ///  TODO: Should it be a StringEnum?
        public string RecoveryType => GetStringProperty("recoveryType");

        /// <inheritdoc/>
        public FactorType FactorType => GetEnumProperty<FactorType>("factorType");

        /// <inheritdoc/>
        ///  TODO: Should it be a StringEnum?
        public string FactorResult => GetStringProperty("factorResult");
    }
}
