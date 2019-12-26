using AutoFixture;
using VRFEngine.Model;

namespace VRFEngine.Test.Helper
{
    /// <summary>
    /// Used to generate entity for testing.
    /// </summary>
    public static class EntityFactory
    {
        public static Field GetField()
        {
            Fixture fixture = new Fixture();
            fixture.Customizations.Add(new PropertyNameOmitter("Id"));

            return fixture.Build<Field>()
                .Create();
        }

         public static FieldVersion GetFieldVersion()
        {
            Fixture fixture = new Fixture();
            fixture.Customizations.Add(new PropertyNameOmitter("Id", "Field", "FieldId"));

            return fixture.Build<FieldVersion>()
                .Create();
        }

        public static Form GetForm()
        {
            Fixture fixture = new Fixture();
            fixture.Customizations.Add(new PropertyNameOmitter("Id"));

            return fixture.Build<Form>()
                .Create();
        }

         public static FormVersion GetFormVersion()
        {
            Fixture fixture = new Fixture();
            fixture.Customizations.Add(new PropertyNameOmitter("Id", "Fields", "Rules"));

            return fixture.Build<FormVersion>()
                .Create();
        }

        public static Rule GetRule()
        {
            Fixture fixture = new Fixture();
            fixture.Customizations.Add(new PropertyNameOmitter("Id"));

            return fixture.Build<Rule>()
                .Create();
        }

         public static RuleVersion GetRuleVersion()
        {
            Fixture fixture = new Fixture();
            fixture.Customizations.Add(new PropertyNameOmitter("Id", "FieldVersionId", "FieldVersion", "FormVersionId", "FormVersion"));

            return fixture.Build<RuleVersion>()
                .Create();
        }
    }
}
