using System;
using System.Collections.Generic;
using System.Text;
using Okta.Sdk.Abstractions;

namespace Okta.Authn.Models
{
    public class FactorType : StringEnum
    {
        public FactorType(string value) : base(value)
        {
        }

        public static FactorType Sms = new FactorType("SMS");

        public static FactorType Question = new FactorType("question");

        public static FactorType Call = new FactorType("call");
        
        public static FactorType Push = new FactorType("push");

        public static FactorType Token = new FactorType("token");

        public static FactorType TokenHardware = new FactorType("token:hardware");

        public static FactorType TokenSoftware = new FactorType("token:software");

        public static FactorType TokenSoftwareTotp = new FactorType("token:software:totp");

        public static FactorType Web = new FactorType("web");

        public static FactorType U2f = new FactorType("u2f");
    }
}
