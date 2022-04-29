using RulesExercise.Domain.Enums;

namespace RulesExercise.Application.Rules
{
    public class RulesSettings
    {
        public LogicOperator Operator { get; set; }

        public List<RuleCondition> Conditions { get; set; } = new List<RuleCondition>();

        public List<Effect> Effects { get; set; } = new List<Effect>();
    }
}
