using RulesExercise.Domain.Enums;

namespace RulesExercise.Application.Rules.Models
{
    public class RuleSetting
    {
        public LogicOperator Operator { get; set; }

        public List<RuleCondition> Conditions { get; set; } = new List<RuleCondition>();

        public List<Effect> Effects { get; set; } = new List<Effect>();
    }
}
