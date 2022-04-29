namespace RulesExercise.Application.Interfaces.Senders
{
    public interface ISender
    {
        void SetSenderType(string senderType);

        Task SendToAsync(string email, string header, string message);
    }
}
