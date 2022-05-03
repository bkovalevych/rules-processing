namespace RulesExercise.Application.Helpers
{
    public static class SnakeCaseToCamelCaseConverter
    {
        public static string FromSnakeCaseToCamelCase(string input)
        {
            return input.Split(new[] { "_" },
                StringSplitOptions.RemoveEmptyEntries)
                .Select(s => char.ToUpperInvariant(s[0]) + s[1..])
                .Aggregate((s1, s2) => s1 + s2);
        }
    }
}
