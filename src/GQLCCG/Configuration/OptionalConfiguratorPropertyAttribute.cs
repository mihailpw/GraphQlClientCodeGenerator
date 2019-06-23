using System;

namespace GQLCCG.Configuration
{
    [AttributeUsage(AttributeTargets.Property)]
    public class OptionalConfiguratorPropertyAttribute : ConfiguratorPropertyAttribute
    {
        public string OptionalQuestion { get; }


        public OptionalConfiguratorPropertyAttribute(
            string optionalQuestion,
            string setupQuestion,
            params object[] supportedValues)
            : base(setupQuestion, supportedValues)
        {
            OptionalQuestion = optionalQuestion;
        }
    }
}