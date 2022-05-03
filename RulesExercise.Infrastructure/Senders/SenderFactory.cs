using RulesExercise.Application.Interfaces.Senders;
using RulesExercise.Domain.Enums;
using System.Reflection;

namespace RulesExercise.Infrastructure.Senders
{
    public class SenderFactory : ISenderFactory
    {
        private readonly Dictionary<Channel, ISender> _senders = new Dictionary<Channel, ISender>();

        public SenderFactory(IServiceProvider serviceProvider)
        {
            var senderTypes = Assembly.GetAssembly(typeof(SenderFactory))
                .GetTypes()
                .Where(t =>
                    t.IsClass &&
                    !t.IsAbstract &&
                    t.IsAssignableTo(typeof(ISender)));
            foreach (var senderType in senderTypes)
            {
                if (serviceProvider.GetService(senderType) is BaseSender service)
                {
                    _senders[service.Channel] = service;
                }
            }
        }

        public ISender GetSenderForChannel(Channel channel)
        {
            return _senders[channel];
        }
    }
}
