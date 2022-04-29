namespace RulesExercise.Application.Interfaces.Senders
{
    public interface ISender
    {
        Task SendToAsync(string email, string header, string message);
    }
}
