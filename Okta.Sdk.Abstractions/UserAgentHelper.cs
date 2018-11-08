using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Okta.Sdk.Abstractions
{
    public static class UserAgentHelper
    {
        public static Version SdkVersion
        {
            get { return typeof(BaseOktaClient).GetTypeInfo().Assembly.GetName().Version; }
        }
    }
}
