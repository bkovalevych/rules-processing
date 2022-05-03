namespace RulesExercise.Application.Interfaces.Senders
{
    public interface ISender
    {
        Task SendMessageAsync(string header, string message);
    }
}
