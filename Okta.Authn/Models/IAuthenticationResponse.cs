using System;
using System.Collections.Generic;
using Okta.Authn.Abstractions;
using Okta.Sdk.Abstractions;

namespace Okta.Authn.Models
{
    public interface IAuthenticationResponse : IResource
    {
        string StateToken { get; }

        string SessionToken { get; }

        string RelayState { get; }

        DateTimeOffset? ExpiresAt { get; }

        AuthenticationStatus AuthenticationStatus { get; }

        Resource Embedded { get; }

        Resource Links { get; }

        IList<Factor> Factors { get; }

        /// <summary>
        /// Some times the response has a single factor instead of a list
        /// </summary>
        Factor Factor { get; }
    }
}
