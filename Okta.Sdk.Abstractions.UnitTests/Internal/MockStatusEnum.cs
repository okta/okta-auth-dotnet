namespace Okta.Sdk.Abstractions.UnitTests.Internal
{
    public class TestEnum : StringEnum
    {
        public static TestEnum Foo = new TestEnum("FOO");

        public static TestEnum Bar = new TestEnum("BAR");

        public static TestEnum Baz = new TestEnum("BAZ");

        public static implicit operator TestEnum(string value) => new TestEnum(value);

        public TestEnum(string value)
            : base(value)
        {
        }
    }
}
