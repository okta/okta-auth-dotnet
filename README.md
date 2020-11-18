
[![Support](https://img.shields.io/badge/support-Developer%20Forum-blue.svg)][devforum]


# Okta .NET Authentication SDK

* [Release status](#release-status)
* [Need help?](#need-help)
* [Getting started](#getting-started)
* [Usage guide](#usage-guide)
* [Configuration reference](#configuration-reference)
* [Building the SDK](#building-the-sdk)
* [Contributing](#contributing)

This repository contains the Okta Authentication SDK for .NET which is used in your server-side code to interact with the [Okta Authentication API](https://developer.okta.com/docs/api/resources/authn).

For high level information about the features of the Okta Authentication product see [Okta Authentication](https://developer.okta.com/product/authentication).
 
> :warning: **NOTE:** Using OAuth 2.0 or OpenID Connect to integrate your application instead of this library requires much less work, and has a smaller risk profile. Please see [this guide](https://developer.okta.com/use_cases/authentication/) to see if using this API is right for your use case. You could also use our [ASP.NET Integration](https://github.com/okta/okta-aspnet) out of the box.

Okta's Authentication API is built around a [state machine](https://developer.okta.com/docs/api/resources/authn#transaction-state). In order to use this library you will need to be familiar with the available states.

![State Model Diagram](https://developer.okta.com/img/auth-state-model1.png "State Model Diagram")

For more information about multifactor authentication see the [multifactor authentication guide](https://developer.okta.com/docs/guides/mfa/ga/prerequisites/).

We also publish these other libraries for .NET:
 
* [Okta ASP.NET middleware](https://github.com/okta/okta-aspnet)
* [Okta .NET management SDK](https://github.com/okta/okta-sdk-dotnet)

You can learn more on the [Okta + .NET][lang-landing] page in our documentation.

## Release status

This library uses semantic versioning and follows Okta's [library version policy](https://developer.okta.com/code/library-versions/).

:heavy_check_mark: The current stable major version series is: 2.x

| Version | Status                    |
| ------- | ------------------------- |
| 1.x | :warning: Retiring on 2021-03-29 |
| 2.x | :heavy_check_mark: Stable |

 
The latest release can always be found on the [releases page][github-releases].

## Need help?
 
If you run into problems using the SDK, you can
 
* Ask questions on the [Okta Developer Forums][devforum]
* Post [issues][github-issues] here on GitHub (for code errors)


## Getting Started

The SDK is compatible with:

* .NET Standard 2.0 and 2.1
* .NET Framework 4.6.1 or higher
* .NET Core 3.0 or higher
* .NET 5.0
 

### Install using Nuget Package Manager
 1. Right-click on your project in the Solution Explorer and choose **Manage Nuget Packages...**
 2. Search for Okta. Install the `Okta.Auth.Sdk` package.

### Install using The Package Manager Console
Simply run `install-package Okta.Auth.Sdk`. Done!

You'll also need:

* An Okta account, called an _organization_ (sign up for a free [developer organization](https://developer.okta.com/signup) if you need one)
 
Construct a client instance by passing it your Okta domain name:
 
``` csharp
var client = new AuthenticationClient(new OktaClientConfiguration
{
    OktaDomain = "https://{{yourOktaDomain}}",
});
```

Hard-coding the Okta domain works for quick tests, but for real projects you should use a more secure way of storing your Organization values (such as environment variables). This library supports a few different configuration sources, covered in the [configuration reference](#configuration-reference) section.
 
## Usage guide

These examples will help you understand how to use this library.

Once you initialize an `AuthenticationClient`, you can call methods to make requests to the Okta API.

### Primary Authentication

``` csharp
var authnOptions = new AuthenticateOptions()
{
    Username = $"darth.vader@imperial-senate.gov",
    Password = "D1sturB1ng!",
};

var authnResponse = await authClient.AuthenticateAsync(authnOptions);

```

### Primary Authentication with Activation Token

``` csharp
var authnOptions = new AuthenticateWithActivationTokenOptions()
{
    ActivationToken = "foo",
};

var authnResponse = await authClient.AuthenticateAsync(authnOptions);

```

### Change Password

``` csharp
var changePasswordOptions = new ChangePasswordOptions()
{
    StateToken = "foo",
    OldPassword = "Okta4321!",
    NewPassword = "Okta1234!",
};

var authnResponse = await authClient.ChangePasswordAsync(changePasswordOptions);
```

### Enroll Security Question Factor

```csharp
var enrollOptions = new EnrollSecurityQuestionFactorOptions()
{
    Question = "name_of_first_plush_toy",
    Answer = "blah",
    StateToken = "foo",
};

var authnResponse = await authnClient.EnrollFactorAsync(enrollOptions);
```

### Enroll SMS Factor

``` csharp
var enrollOptions = new EnrollSMSFactorOptions()
{
    PhoneNumber = "+1 415 555 5555",
    StateToken = "foo",
};

var authnResponse = await authnClient.EnrollFactorAsync(enrollOptions);
```

### Enroll Call Factor

``` csharp

var enrollFactorOptions = new EnrollCallFactorOptions()
{
    PhoneNumber = "+1-555-415-1337",
    StateToken = "foo",
};

var authnResponse = await authnClient.EnrollFactorAsync(enrollFactorOptions);
```

### Enroll Okta Verify TOTP Factor

``` csharp

var enrollOptions = new EnrollTotpFactorOptions()
{
    StateToken = "foo",
    Provider = OktaDefaults.GoogleProvider,
};

var authnResponse = await authnClient.EnrollFactorAsync(enrollOptions);
```

### Enroll Okta Verify Push Factor

``` csharp

var enrollOptions = new EnrollPushFactorOptions()
{
    StateToken = "foo",
};

var authnResponse = await authnClient.EnrollFactorAsync(enrollOptions);
```

### Enroll RSA SecurID Factor

``` csharp

var enrollOptions = new EnrollRsaFactorOptions()
{
    StateToken = "foo",
    CredentialId = "dade.murphy@example.com",
    PassCode = "bar",
};

var authnResponse = await authnClient.EnrollFactorAsync(enrollOptions);
```

### Enroll Symantec VIP Factor

``` csharp

var enrollOptions = new EnrollSymantecFactorOptions()
{
    StateToken = "foo",
    CredentialId = "dade.murphy@example.com",
    PassCode = "bar",
    NextPassCode = "baz",
};

var authnResponse = await authnClient.EnrollFactorAsync(enrollOptions);
```

### Enroll YubiKey Factor

``` csharp

var enrollOptions = new EnrollYubiKeyFactorOptions()
{
    PassCode = "bar",
    StateToken = "foo",
};

var authnResponse = await authnClient.EnrollFactorAsync(enrollOptions);
```

### Enroll Duo Factor

``` csharp

var enrollOptions = new EnrollDuoFactorOptions()
{
    StateToken = "foo",
};

var authnResponse = await authnClient.EnrollFactorAsync(enrollOptions);
``` 

### Enroll U2F Factor

``` csharp

var enrollOptions = new EnrollU2FFactorOptions()
{
    StateToken = "foo",
};

var authnResponse = await authnClient.EnrollFactorAsync(enrollOptions);
``` 

### Activate U2F Factor

``` csharp
var activateFactorOptions = new ActivateU2fFactorOptions()
{
    FactorId = "ostf1fmaMGJLMNGNLIVG",
    StateToken = "foo",
    RegistrationData = "bar",
    ClientData = "baz",
};

var authResponse = await authnClient.ActivateFactorAsync(activateFactorOptions);
```

### Activate Push Factor
``` csharp
var activatePushFactorOptions = new ActivatePushFactorOptions()
{
    StateToken = "foo",
    FactorId = "bar",
};

var authResponse = await authnClient.ActivateFactorAsync(activatePushFactorOptions);
```

### Activate Other Factors

``` csharp
var activateFactorOptions = new ActivateFactorOptions()
{
    StateToken = "00xdqXOE5qDXX8-PBR1bYv8AESqIEinDy3yul01tyh",
    FactorId = "foo",
    PassCode = "bar",
};

var authResponse = await authnClient.ActivateFactorAsync(activateFactorOptions);
```

### Verify SMS Factor

```csharp
var verifyFactorOptions = new VerifySmsFactorOptions()
{
    StateToken = "foo",
    FactorId = "bar",
};

var authResponse = await authnClient.VerifyFactorAsync(verifyFactorOptions);
```

### Verify Call Factor

```csharp

var verifyFactorOptions = new VerifyCallFactorOptions()
{
    FactorId = "ostf1fmaMGJLMNGNLIVG",
    StateToken = "foo",
    PassCode = "bar",
    RememberDevice = true,
};

var authResponse = await authnClient.VerifyFactorAsync(verifyFactorOptions);
```

### Verify Security Question Factor

```csharp

var verifyFactorOptions = new VerifySecurityQuestionFactorOptions()
{
    FactorId = "ostf1fmaMGJLMNGNLIVG",
    StateToken = "foo",
    Answer = "bar",
};

var authResponse = await authnClient.VerifyFactorAsync(verifyFactorOptions);
```

### Verify TOTP Factor

```csharp

var verifyFactorOptions = new VerifySmsFactorOptions()
{
    FactorId = "ostf1fmaMGJLMNGNLIVG",
    StateToken = "foo",
    PassCode = "bar",
    RememberDevice = true,
};

var authResponse = await authnClient.VerifyFactorAsync(verifyFactorOptions);
```

### Verify Push Factor

```csharp

var verifyFactorOptions = new VerifyPushFactorOptions()
{
    FactorId = "ostf1fmaMGJLMNGNLIVG",
    StateToken = "foo",
    AutoPush = true,
    RememberDevice = true,
};

var authResponse = await authnClient.VerifyFactorAsync(verifyFactorOptions);
```

### Verify Duo Factor

```csharp

var verifyFactorOptions = new VerifyDuoFactorOptions()
{
    FactorId = "ostf1fmaMGJLMNGNLIVG",
    StateToken = "foo",
};

var authResponse = await authnClient.VerifyFactorAsync(verifyFactorOptions);
```

### Verify U2F Factor

```csharp

var verifyFactorOptions = new VerifyU2FFactorOptions()
{
    FactorId = "ostf1fmaMGJLMNGNLIVG",
    StateToken = "foo",
    ClientData = "bar",
    SignatureData = "baz",
    RememberDevice = true,
};

var authResponse = await authnClient.VerifyFactorAsync(verifyFactorOptions);
```

### Unlock Account

```csharp

var unlockAccountOptions = new UnlockAccountOptions()
{
    FactorType = new FactorType(factorType),
    RelayState = "/myapp/some/deep/link/i/want/to/return/to",
    Username = "dade.murphy@example.com",
};

await authnClient.UnlockAccountAsync(unlockAccountOptions);

```

### Reset Password

```csharp

var resetPasswordOptions = new ResetPasswordOptions()
{
    NewPassword = "1234",
    StateToken = "00xdqXOE5qDXX8-PBR1bYv8AESqIEinDy3yul01tyh",
};

var authResponse = await authnClient.ResetPasswordAsync(resetPasswordOptions);

```

### Verify Recovery Token

```csharp

var verifyFactorOptions = new VerifyRecoveryTokenOptions()
{
    RecoveryToken = "foo",
};

await authnClient.VerifyRecoveryTokenAsync(verifyFactorOptions);

```

### Verify Recovery Factor

```csharp

var verifyFactorOptions = new VerifyRecoveryFactorOptions()
{
    StateToken = "foo",
    PassCode = "bar",
    FactorType = new FactorType(factorType),
};

await authnClient.VerifyRecoveryFactorAsync(verifyFactorOptions);

```

### Answer Recovery Question

```csharp

var answerRecoveryQuestionOptions = new AnswerRecoveryQuestionOptions()
{
    StateToken = "foo",
    Answer = "bar",
};

await authnClient.AnswerRecoveryQuestionAsync(answerRecoveryQuestionOptions);

```

### Get Transaction State

```csharp

var transactionStateOptions = new TransactionStateOptions()
{
    StateToken = "foo",
};

await authnClient.GetTransactionStateAsync(transactionStateOptions);

```

### Previous Transaction State

```csharp

var transactionStateOptions = new TransactionStateOptions()
{
    StateToken = "foo",
};

await authnClient.GetPreviousTransactionStateAsync(transactionStateOptions);

```

### Skip Transaction State

```csharp

var transactionStateOptions = new TransactionStateOptions()
{
    StateToken = "foo",
};

await authnClient.SkipTransactionStateAsync(transactionStateOptions);

```

### Cancel Transaction State

```csharp

var transactionStateOptions = new TransactionStateOptions()
{
    StateToken = "foo",
};

await authnClient.CancelTransactionStateAsync(transactionStateOptions);

```

## Send information via request's headers.

The scenarios described in this section require you to send additional information in the `user-agent`, `x-forwarded-for` and `x-device-fingerprinting` headers. This SDK allows you to define these headers via method parameters or construct and send custom requests using the `AuthenticationClient`.

### Primary authentication with trusted application

For more information on this feature and the underlying API call, see the related [developer documentation](https://developer.okta.com/docs/reference/api/authn/#request-example-for-unlock-account-with-sms-factor-trusted-application).

```csharp
var authOptions = new AuthenticateOptions()
{
    Username = $"darth.vader@imperial-senate.gov",
    Password = "D1sturB1ng!",
    MultiOptionalFactorEnroll = false,
    WarnBeforePasswordExpired = false,
    DeviceToken = "26q43Ak9Eh04p7H6Nnx0m69JqYOrfVBY",
    UserAgent = "Chrome/46.0.2490.86",
    XForwardedFor = "23.235.46.133",
};

await authnClient.AuthenticateAsync(authOptions);
```

### Primary authentication with activation token

For more information on this feature and the underlying API call, see the related [developer documentation](https://developer.okta.com/docs/reference/api/authn/#primary-authentication-with-activation-token).

```csharp
var authOptions = new AuthenticateOptions()
{
    ActivationToken = "o7AFoTGE9xjQiHQK6dAa",
    UserAgent = "Chrome/46.0.2490.86",
    XForwardedFor = "23.235.46.133",
};

await authnClient.AuthenticateAsync(authOptions);
```

### Primary authentication with device fingerprinting

For more information on this feature and the underlying API call, see the related [developer documentation](https://developer.okta.com/docs/reference/api/authn/#primary-authentication-with-device-fingerprinting).

```csharp
var authOptions = new AuthenticateOptions()
{
    Username = $"darth.vader@imperial-senate.gov",
    Password = "D1sturB1ng!",
    UserAgent = "Chrome/46.0.2490.86",
    XForwardedFor = "23.235.46.133",
    DeviceFingerprint = "device_fingerprint"
};

await authnClient.AuthenticateAsync(authOptions);
```

### Forgot password with trusted application

For more information on this feature and the underlying API call, see the related [developer documentation](https://developer.okta.com/docs/reference/api/authn/#forgot-password-with-trusted-application).

```csharp
var forgotPasswordOptions = new ForgotPasswordOptions()
{
    FactorType = FactorType.Call,
    RelayState = "/myapp/some/deep/link/i/want/to/return/to",
    UserName = "bob-user@test.com",
    UserAgent = "Chrome/46.0.2490.86",
    XForwardedFor = "23.235.46.133",
};

await authnClient.ForgotPasswordAsync(forgotPasswordOptions);
```

### Unlock account with trusted application

For more information on this feature and the underlying API call, see the related [developer documentation](https://developer.okta.com/docs/reference/api/authn/#unlock-account-with-trusted-application).

```csharp
var unlockAccountOptions = new UnlockAccountOptions()
{
    FactorType = new FactorType("sms"),
    RelayState = "/myapp/some/deep/link/i/want/to/return/to",
    Username = "dade.murphy@example.com",
    UserAgent = "Chrome/46.0.2490.86",
    XForwardedFor = "23.235.46.133",
};

await authnClient.UnlockAccountAsync(unlockAccountOptions);
```

The Authentication Client object allows you to send custom requests that you can construct and set your desired headers. Check out [Call other API endpoints](Call-other-API-endpoints) section for more details.

## Call other API endpoints

The Authentication Client object allows you to construct and send a request to an Authentication API endpoint that isn't represented by a method in the SDK.
Also, you can make calls to any Okta API (not just the endpoints officially supported by the SDK) via the `GetAsync`, `PostAsync`, `PutAsync` and `DeleteAsync` methods.

For example, to send a forgot password request using the `PostAsync` method (instead of `ForgotPasswordAsync`):

```csharp
var forgotPasswordOptions = new ForgotPasswordOptions()
{
    FactorType = FactorType.Email,
    RelayState = "/myapp/some/deep/link/i/want/to/return/to",
    UserName = "bob-user@test.com",
};

var request = new HttpRequest()
{
    Uri = "/api/v1/authn/recovery/password",
    Payload = forgotPasswordOptions,
};

request.Headers["User-Agent"] = "MyUserAgentInfo";

var authResponse = await authnClient.PostAsync<AuthenticationResponse>(request);
```

In this case, there is no benefit to using `PostAsync` instead of `ForgotPasswordAsync`. However, this approach can be used to call any endpoints that are not represented by methods in the SDK.

## Configuration reference
  
This library looks for configuration in the following sources:
 
1. An `okta.yaml` file in a `.okta` folder in the current user's home directory (`~/.okta/okta.yaml` or `%userprofile\.okta\okta.yaml`)
2. An `okta.yaml` file in a `.okta` folder in the application or project's root directory
3. Environment variables
4. Configuration explicitly passed to the constructor (see the example in [Getting started](#getting-started))
 
Higher numbers win. In other words, configuration passed via the constructor will override configuration found in environment variables, which will override configuration in `okta.yaml` (if any), and so on.
 
### YAML configuration
 
The full YAML configuration looks like:
 
```yaml
okta:
  client:
    connectionTimeout: 30 # seconds
    oktaDomain: "https://{yourOktaDomain}"
    proxy:
      port: null
      host: null
    token: {apiToken} # optional
```
 
### Environment variables
 
Each one of the configuration values above can be turned into an environment variable name with the `_` (underscore) character:
 
* `OKTA_CLIENT_CONNECTIONTIMEOUT`
* `OKTA_CLIENT_OKTADOMAIN`
* and so on

## Building the SDK
 
In most cases, you won't need to build the SDK from source. If you want to build it yourself just clone the repo and compile using Visual Studio.
 
## Contributing
 
We're happy to accept contributions and PRs! Please see the [contribution guide](CONTRIBUTING.md) to understand how to structure a contribution.

[devforum]: https://devforum.okta.com/
[lang-landing]: https://developer.okta.com/code/dotnet/
[github-issues]: https://github.com/okta/okta-auth-dotnet/issues
[github-releases]: https://github.com/okta/okta-auth-dotnet/releases
