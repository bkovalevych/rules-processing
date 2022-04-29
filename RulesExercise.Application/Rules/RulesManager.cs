using RulesExercise.Application.Interfaces.Senders;
using RulesExercise.Application.Rules.Models;
using RulesExercise.Domain.Entities;

namespace RulesExercise.Application.Rules
{
    public class RulesManager
    {
        private readonly List<RuleChecker> _ruleCheckers;
        private readonly List<RuleSetting> _ruleSettings;

        public RulesManager(List<RuleSetting> ruleSettings)
        {
            _ruleCheckers = ruleSettings.Select(rule => new RuleChecker(rule))
                .ToList();
            _ruleSettings = ruleSettings;
        }

        public List<Effect> GetEffectsByProject(Project project)
        {
            var effects = _ruleCheckers.Zip(_ruleSettings)
                .Where((it) => it.First.CheckProject(project))
                .SelectMany((it) => it.Second.Effects)
                .ToList();

            return effects;
        }
    }
}
