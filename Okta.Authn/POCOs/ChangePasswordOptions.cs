namespace Okta.Authn.POCOs
{
    public class ChangePasswordOptions
    {
        public string StateToken { get; set; }

        public string OldPassword { get; set; }

        public string NewPassword { get; set; }
    }
}