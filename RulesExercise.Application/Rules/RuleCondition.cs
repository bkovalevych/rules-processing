﻿using RulesExercise.Domain.Enums;

namespace RulesExercise.Application.Rules
{
    public class RuleCondition
    {
        public string Key { get; set; } = default!;

        public Condition Condition { get; set; }

        public string Val { get; set; } = default!;
    }
}
