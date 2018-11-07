using System;
using System.Collections.Generic;
using System.Text;

namespace Okta.Authn.POCOs
{
    public class ResetPasswordOptions
    {
        public string StateToken { get; set; }

        public string NewPassword { get; set; }
    }
}
