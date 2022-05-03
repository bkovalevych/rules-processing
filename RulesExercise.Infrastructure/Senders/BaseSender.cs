using RulesExercise.Application.Interfaces.Senders;
using RulesExercise.Domain.Enums;

namespace RulesExercise.Infrastructure.Senders
{
    public abstract class BaseSender : ISender
    {
        internal abstract Channel Channel { get; }
        public abstract Task SendMessageAsync(string header, string message);
    }
}
