# Okta .NET Auth SDK migration guide

This library uses semantic versioning and follows Okta's [library version policy](https://developer.okta.com/code/library-versions/). In short, we don't make breaking changes unless the major version changes!

## Migrating from .NET Auth SDK 2.x to .NET IDX SDK 1.x

This library, [Okta.Idx.Sdk](https://www.nuget.org/packages/Okta.Idx.Sdk) is a ground up rewrite of the previous version, [Okta.Auth.Sdk](https://www.nuget.org/packages/Okta.Auth.Sdk).  The new library takes advantage of the [OIE features](https://www.okta.com/platform/identity-engine/) available via the IDX API. 

The new library version is 1.0.0 because of the use of different APIs and patterns. The last published version of the Auth.SDK is 2.0.3.

## Getting started

The SDK is compatible with:

* .NET Standard 2.0 and 2.1
* .NET Framework 4.6.1 or higher
* .NET Core 3.0 or higher
* .NET 5.0

Visual Studio 2017 or newer is required as previous versions are not compatible with the above frameworks.

### Install using Nuget Package Manager

1. Right-click on your project in the Solution Explorer and choose Manage Nuget Packages...
2. Search for Okta. Install the Okta.Idx.Sdk package.

### Install using The Package Manager Console
Simply run install-package Okta.Idx.Sdk. Done!

For more information check out the [IDX SDK Repository](https://github.com/okta/okta-idx-dotnet).

## New configuration model

In order to use the `IdxClient`, you need to configure additional properties. For more information, check out our [embedded Auth guide](https://developer.okta.com/docs/guides/oie-embedded-sdk-overview/main/#get-started-with-the-sdk).

The simplest way to construct a client is via code:

```csharp
var client = new IdxClient(new IdxConfiguration()
            {
                Issuer = "{YOUR_ISSUER}", // e.g. https://foo.okta.com/oauth2/default, https://foo.okta.com/oauth2/ausar5vgt5TSDsfcJ0h7
                ClientId = "{YOUR_CLIENT_ID}", 
                ClientSecret = "{YOUR_CLIENT_SECRET}", //Required for confidential clients. 
                RedirectUri = "{YOUR_REDIRECT_URI}", // Must match the redirect uri in client app settings/console
                Scopes = "openid profile offline_access",
            });

```

> Note: For additional configuration sources check out the [IDX SDK configuration reference](https://github.com/oktaokta-idx-dotnet#configuration-reference).

## New methods
 
In the table below are the methods available in the IDX client and the equivalent from the Auth SDK. For guidance about usage, check out [here](https://github.com/okta/okta-idx-dotnet/#usage-guide).


| Before   |      Now      |   |
|----------|-------------|------|
| `AuthenticateAsync` |  `AuthenticateAsync` | Authenticates a user with username/password credentials |
| `ChangePasswordAsync` |    `ChangePasswordAsync`   |   Changes user''s password. In addition to the `changePasswordOptions`, you need to pass an `IdxContext`.|
| `ForgotPasswordAsync` | `RecoverPasswordAsync` |   Initiates the password recovery flow.  |
| `ResetPasswordAsync` | `ChangePasswordAsync` |   Changes user''s password. In addition to the `changePasswordOptions`, you need to pass an `IdxContext`.  |
|`EnrollFactorAsync`|`EnrollAuthenticatorAsync`| Triggers the authenticator enrollment flow. Previously, you had to have called `SelectEnrollAuthenticatorAsync`. <br /> In addition to the `enrollAuthenticatorOptions`, you aneed to pass an `IdxContext`. |
|`ResendSmsEnrollFactorAsync` <br /> `ResendCallEnrollFactorAsync` <br /> `ResendRecoveryChallengeAsync` <br /> `ResendVerifyChallengeAsync` | `ResendCodeAsync`| Resends Code. You need to pass an `IdxContext`. |
|`ActivateFactorAsync` <br /> `VerifyRecoveryFactorAsync`| `VerifyAuthenticatorAsync` | Verifies an authenticator. In addition to `verifyAuthenticatorOptions`, you need to pass an `IdxContext`.  |
|`SkipTransactionStateAsync`| `SkipAuthenticatorSelectionAsync`| Skips an optional authenticator during enrollment/verification. You need to pass an `IdxContext`. |
|`AnswerRecoveryQuestionAsync` <br /> `CancelTransactionStateAsync`| N/A| Out of Scope |
|`VerifyRecoveryTokenAsync` <br /> `GetTransactionStateAsync` <br /> `UnlockAccountAsync` <br /> `GetPreviousTransactionStateAsync`| N/A||
|`UnlockAccountAsync`| N/A||

## Authentication Response

Similar to the Auth SDK, the IDX client returns a response with an authentication status that indicates how to proceed with the authentication flow. Check out the Authentication Status section [here](https://github.com/okta/okta-idx-dotnet#authentication-status) for more details.

## Handling errors

The SDK throws an OktaException when the server responds with an invalid status code, or there is an internal error. Get more information by calling exception.Message.

## Getting help

If you have questions about this library or about the Okta APIs, post a question on our [Developer Forum](https://devforum.okta.com).

If you find a bug or have a feature request for the IDX library specifically, [post an issue](https://github.com/okta/okta-idx-dotnet/issues) here on GitHub.