using RulesExercise.Application.Rules;
using RulesExercise.Domain.Entities;
using RulesExercise.Domain.Enums;
using System.Linq.Expressions;

namespace RulesExercise.Application.Interfaces
{
    public class RulesManager
    {
        private readonly RulesSettings _rules;
        
        private readonly Func<Project, bool> _projectFilter;

        public RulesManager(RulesSettings rules)
        {
            _rules = rules;
            _projectFilter = BuildExpression();
        }

        public bool ProcessProject(Project project)
        {
            var result = _projectFilter(project);
            return result;
        }

        private Func<Project, bool> BuildExpression()
        {
            var parameter = Expression.Parameter(typeof(Project));
            Expression body;
            if (_rules.Operator == LogicOperator.Or)
            {
                body = _rules.Conditions
                    .Aggregate(
                        (Expression)Expression.Constant(false), 
                        (prev, next) => AggregateOrHandler(prev, next, parameter));
            }
            else
            {
                body = _rules.Conditions
                    .Aggregate(
                        (Expression)Expression.Constant(true), 
                        (prev, next) => AggregateAndHandler(prev, next, parameter));
            }
            var lambda = Expression.Lambda<Func<Project, bool>>(body, parameter);
            var result = lambda.Compile();
            return result;
        }

        private static Expression AggregateAndHandler(Expression prviousState, RuleCondition rule, ParameterExpression project)
        {
            var result = HandleRule(rule, project);

            return Expression.And(result, prviousState);
        }

        private static Expression AggregateOrHandler(Expression prviousState, RuleCondition rule, ParameterExpression project)
        {
            var result = HandleRule(rule, project);

            return Expression.Or(result, prviousState);
        }

        private static Expression HandleRule(RuleCondition rule, ParameterExpression project)
        {
            var field =  Expression.PropertyOrField(project, rule.Key);
            var rawVal = Expression.Constant(rule.Val);
            var val = Expression.Convert(rawVal, field.Type);
            return rule.Condition switch
            {
                Condition.Equal => Expression.Equal(field, val),
                Condition.LessThan => Expression.LessThan(field, val),
                Condition.MoreThan => Expression.GreaterThan(field, val),
                Condition.InArray => Expression.Constant(false),// TODO: lambda for Contains 
                _ => Expression.Constant(false),
            };
        }
    }
}
