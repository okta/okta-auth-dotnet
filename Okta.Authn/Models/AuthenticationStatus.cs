using Okta.Sdk.Abstractions;

namespace Okta.Authn.Models
{
    public class AuthenticationStatus : StringEnum
    {
        public AuthenticationStatus(string value) : base(value)
        {
        }

        public static AuthenticationStatus PasswordExpired = new AuthenticationStatus("PASSWORD_EXPIRED");

        public static AuthenticationStatus Success = new AuthenticationStatus("SUCCESS");

        public static AuthenticationStatus LockedOut = new AuthenticationStatus("LOCKED_OUT");

        public static AuthenticationStatus MfaRequired = new AuthenticationStatus("MFA_REQUIRED");

        public static AuthenticationStatus Unknown = new AuthenticationStatus("UNKNOWN");

        public static AuthenticationStatus MfaEnroll = new AuthenticationStatus("MFA_ENROLL");

        public static AuthenticationStatus MfaEnrollActivate = new AuthenticationStatus("MFA_ENROLL_ACTIVATE");

        public static AuthenticationStatus MfaChallenge = new AuthenticationStatus("MFA_CHALLENGE");

        public static AuthenticationStatus RecoveryChallenge = new AuthenticationStatus("RECOVERY_CHALLENGE");
                
    }
}