using System.Linq.Expressions;

namespace RulesExercise.Domain.Entities
{
    public class Project
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }
        
        public string? Stage { get; set; }

        public List<int> Categories { get; set; } = new List<int>();

        public DateTimeOffset CreatedAt { get; set; }

        public DateTimeOffset ModifiedAt { get; set; }

        private Dictionary<string, string> _values;
        private static Dictionary<string, Func<string, Expression>> _parsers = new Dictionary<string, Func<string, Expression>>()
        {
            [nameof(Project.Id)] = val => Expression.Constant(int.Parse(val)),
            [nameof(Project.Name)] = val => Expression.Constant(val),
            [nameof(Project.Description)] = val => Expression.Constant(val),
            [nameof(Project.Stage)] = val => Expression.Constant(val),
            [nameof(Project.CreatedAt)] = val => Expression.Constant(DateTimeOffset.FromUnixTimeSeconds(long.Parse(val))),
            [nameof(Project.ModifiedAt)] = val => Expression.Constant(DateTimeOffset.FromUnixTimeSeconds(long.Parse(val))),
            [nameof(Project.Categories)] = val => Expression.Constant(int.Parse(val))
        };

        public string GetField(string key)
        {
            _values = new Dictionary<string, string>()
            {
                [nameof(Project.Id)] = Id.ToString(),
                [nameof(Project.Name)] = Name,
                [nameof(Project.Description)] = Description,
                [nameof(Project.Stage)] = Stage,
                [nameof(Project.CreatedAt)] = CreatedAt.ToString(),
                [nameof(Project.ModifiedAt)] = ModifiedAt.ToString()
            };

            return _values[key];
        }
        
        public static Expression GetParsedValue(string name, string rawValue)
        {
            return _parsers[name](rawValue);
        }

        public Project()
        {
            
        }
    }
}
