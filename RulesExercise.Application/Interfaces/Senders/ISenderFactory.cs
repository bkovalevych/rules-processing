using RulesExercise.Domain.Enums;

namespace RulesExercise.Application.Interfaces.Senders
{
    public interface ISenderFactory
    {
        ISender GetSenderForChannel(Channel channel);
    }
}
