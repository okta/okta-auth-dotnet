namespace Okta.Authn.POCOs
{
    public class AuthenticateOptions
    {
        public string Username { get; set; }
        
        public string Password { get; set; }

        public string Audience { get; set; }
        
        public string RelayState { get; set; }
        
        public bool MultiOptionalFactorEnroll { get; set; } = true;

        public bool WarnBeforePasswordExpired { get; set; } = true;
        
        public string DeviceToken { get; set; }
        
    }
}
