using Okta.Authn.Abstractions;

namespace Okta.Authn.Models
{
    public class EnrollFactorRequest : Resource
    {
        public string StateToken
        {
            get => GetStringProperty("stateToken");
            set => this["stateToken"] = value;
        }

        public string Provider
        {
            get => GetStringProperty("provider");
            set => this["provider"] = value;
        }

        public FactorType FactorType
        {
            get => GetEnumProperty<FactorType>("factorType");
            set => this["factorType"] = value;
        }

        public string PassCode
        {
            get => GetStringProperty("passCode");
            set => this["passCode"] = value;
        }

        public string NextPassCode
        {
            get => GetStringProperty("nextPassCode");
            set => this["nextPassCode"] = value;
        }

        public Resource Profile
        {
            get => GetResourceProperty<Resource>("profile");
            set => this["profile"] = value;
        }
    }
}
