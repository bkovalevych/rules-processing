using Microsoft.Extensions.Options;
using RulesExercise.Domain.Enums;
using Telegram.BotAPI;
using Telegram.BotAPI.AvailableMethods;
using Telegram.BotAPI.GettingUpdates;

namespace RulesExercise.Infrastructure.Senders.Telegram
{
    public class TelegramSender : BaseSender
    {
        private readonly BotClient _botClient;
        internal override Channel Channel => Channel.Telegram;

        public TelegramSender(IOptions<TelegramConfiguration> telegramConfiguration)
        {
            _botClient = new BotClient(telegramConfiguration.Value.AccessToken);
        }

        public override async Task SendMessageAsync(string header, string message)
        {
            var updates = await _botClient.GetUpdatesAsync();
            var chunks = updates.Select(update => update.Message.Chat.Id)
                .Distinct()
                .Chunk(32);
            foreach (var chunk in chunks)
            {
                var tasks = chunk.Select(id =>
                        _botClient.SendMessageAsync(id, header + "\n" + message));
                await Task.WhenAll(tasks);
            }
        }
    }
}
