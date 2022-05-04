using Microsoft.EntityFrameworkCore;
using Moq;
using RulesExercise.Infrastructure.Persistence;
using RulesExercise.Infrastructure.Templates;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Xunit;

namespace RulesExercise.Tests
{
    public class TemplateManagerTests
    {
        [Theory]
        [InlineData("{{ Value }} here", "{\"Value\": \"I am\"}", "I am here")]
        public void Some(string template, string rawDictionary, string expectedResult)
        {
            // Arrange
            var t = new DbContextOptions<ApplicationDbContext>();
            var fake_context = new Mock<ApplicationDbContext>(t);
            var dict = JsonSerializer.Deserialize<Dictionary<string, string>>(rawDictionary);
            var preparedDict = dict.ToDictionary(x => x.Key, x => (object)x.Value);
            Assert.NotNull(dict);

            var templateManager = new TemplateManager(fake_context.Object);

            // Act
            
            var result = templateManager.FormatFromTemplate(template, preparedDict);
            
            // Assert

            Assert.Equal(expectedResult, result);
        }
    }
}
