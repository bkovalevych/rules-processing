using RulesExercise.Application.Helpers;
using RulesExercise.Application.Rules;
using RulesExercise.Application.Rules.Models;
using RulesExercise.Domain.Entities;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using Xunit;

namespace RulesExercise.Tests
{
    public class RuleCheckerTests
    {
        [Theory]
        [InlineData("{" +
                "\"operator\": \"and\"," +
                "\"conditions\": [" +
                    "{" +
                        "\"condition\": \"inArray\"," +
                        "\"key\": \"categories\"," +
                        "\"val\": \"1\"" +
                    "}" +
                "]" +
            "}",
            "{\"categories\": [1, 2, 3]}", true)]
        [InlineData("{" +
                "\"operator\": \"and\"," +
                "\"conditions\": [" +
                    "{" +
                        "\"condition\": \"inArray\"," +
                        "\"key\": \"categories\"," +
                        "\"val\": \"5\"" +
                    "}" +
                "]" +
            "}",
            "{\"categories\": [1, 2, 3]}", false)]

        [InlineData("{" +
                "\"operator\": \"and\"," +
                "\"conditions\": [" +
                    "{" +
                        "\"condition\": \"equal\"," +
                        "\"key\": \"id\"," +
                        "\"val\": \"1\"" +
                    "}" +
                "]" +
            "}",
            "{\"id\": 1}", true)]
        [InlineData("{" +
                "\"operator\": \"and\"," +
                "\"conditions\": [" +
                    "{" +
                        "\"condition\": \"equal\"," +
                        "\"key\": \"id\"," +
                        "\"val\": \"1\"" +
                    "}" +
                "]" +
            "}",
            "{\"id\": 2}", false)]

        [InlineData("{" +
                "\"operator\": \"and\"," +
                "\"conditions\": [" +
                    "{" +
                        "\"condition\": \"equal\"," +
                        "\"key\": \"name\"," +
                        "\"val\": \"testName\"" +
                    "}" +
                "]" +
            "}",
            "{\"name\": \"testName\"}", true)]
        [InlineData("{" +
                "\"operator\": \"and\"," +
                "\"conditions\": [" +
                    "{" +
                        "\"condition\": \"equal\"," +
                        "\"key\": \"name\"," +
                        "\"val\": \"testName\"" +
                    "}" +
                "]" +
            "}",
            "{\"name\": \"testName that is not equal\"}", false)]

        [InlineData("{" +
                "\"operator\": \"and\"," +
                "\"conditions\": [" +
                    "{" +
                        "\"key\": \"createdAt\"," +
                        "\"condition\": \"lessThan\"," +
                        "\"val\": \"20\"" +
                    "}" +
                "]" +
            "}",
            "{\"createdAt\": 10}", true)]
        [InlineData("{" +
                "\"operator\": \"and\"," +
                "\"conditions\": [" +
                    "{" +
                        "\"key\": \"createdAt\"," +
                        "\"condition\": \"lessThan\"," +
                        "\"val\": \"20\"" +
                    "}" +
                "]" +
            "}",
            "{\"createdAt\": 20}", false)]

        [InlineData("{" +
                "\"operator\": \"and\"," +
                "\"conditions\": [" +
                    "{" +
                        "\"key\": \"modifiedAt\"," +
                        "\"condition\": \"moreThan\"," +
                        "\"val\": \"20\"" +
                    "}" +
                "]" +
            "}",
            "{\"modifiedAt\": 40}", true)]
        [InlineData("{" +
                "\"operator\": \"and\"," +
                "\"conditions\": [" +
                    "{" +
                        "\"key\": \"modifiedAt\"," +
                        "\"condition\": \"moreThan\"," +
                        "\"val\": \"20\"" +
                    "}" +
                "]" +
            "}",
            "{\"modifiedAt\": 20}", false)]

        [InlineData("{" +
                "\"operator\": \"and\"," +
                "\"conditions\": [" +
                    "{" +
                        "\"key\": \"modifiedAt\"," +
                        "\"condition\": \"moreThan\"," +
                        "\"val\": \"20\"" +
                    "}," +
                    "{" +
                        "\"key\": \"createdAt\"," +
                        "\"condition\": \"lessThan\"," +
                        "\"val\": \"20\"" +
                    "}" +
                "]" +
            "}",
            "{\"modifiedAt\": 40, \"createdAt\": 10}", true)]
        [InlineData("{" +
                "\"operator\": \"and\"," +
                "\"conditions\": [" +
                    "{" +
                        "\"key\": \"modifiedAt\"," +
                        "\"condition\": \"moreThan\"," +
                        "\"val\": \"20\"" +
                    "}," +
                    "{" +
                        "\"key\": \"modifiedAt\"," +
                        "\"condition\": \"lessThan\"," +
                        "\"val\": \"20\"" +
                    "}" +
                "]" +
            "}",
            "{\"modifiedAt\": 40}", false)]

        [InlineData("{" +
                "\"operator\": \"or\"," +
                "\"conditions\": [" +
                    "{" +
                        "\"key\": \"modifiedAt\"," +
                        "\"condition\": \"moreThan\"," +
                        "\"val\": \"20\"" +
                    "}," +
                    "{" +
                        "\"key\": \"modifiedAt\"," +
                        "\"condition\": \"lessThan\"," +
                        "\"val\": \"20\"" +
                    "}" +
                "]" +
            "}",
            "{\"modifiedAt\": 40}", true)]
        [InlineData("{" +
                "\"operator\": \"or\"," +
                "\"conditions\": [" +
                    "{" +
                        "\"key\": \"modifiedAt\"," +
                        "\"condition\": \"equal\"," +
                        "\"val\": \"20\"" +
                    "}," +
                    "{" +
                        "\"key\": \"modifiedAt\"," +
                        "\"condition\": \"lessThan\"," +
                        "\"val\": \"20\"" +
                    "}" +
                "]" +
            "}",
            "{\"modifiedAt\": 21}", false)]
        public void CheckProject_WithRuleSettings_ReturnsBool(string settingsJson, string projectJson, bool expectedResult)
        {
            // Arrange
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                Converters = {
                    new JsonStringEnumConverter(),
                    new CustomDateTimeOffsetConverter()
                },
                PropertyNameCaseInsensitive = true,
            };
            var rules = JsonSerializer.Deserialize<RuleSetting>(settingsJson, options);
            var project = JsonSerializer.Deserialize<Project>(projectJson, options);
            Assert.NotNull(rules);
            Assert.NotNull(project);

            
            var ruleChecker = new RuleChecker(rules);

            // Act

            var result = ruleChecker.CheckProject(project);

            // Assert

            Assert.Equal(expectedResult, result);
        }
    }
}