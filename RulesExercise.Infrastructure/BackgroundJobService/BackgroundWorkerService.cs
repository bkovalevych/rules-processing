using Hangfire;
using RulesExercise.Application.Interfaces.BackgroundJobHelpers;
using System.Linq.Expressions;

namespace RulesExercise.Infrastructure.BackgroundJobService
{
    public class BackgroundWorkerService : IBackgroundWorkerService
    {
        private readonly IBackgroundJobClient _jobClient;

        public BackgroundWorkerService(IBackgroundJobClient backgroundJobClient)
        {
            _jobClient = backgroundJobClient;
        }
        public void Enqueue<T>(Expression<Action<T>> serviceHandler)
        {
            _jobClient.Enqueue(serviceHandler);
        }

        public void Enqueue(Expression<Action> handler)
        {
            _jobClient.Enqueue(handler);
        }
    }
}
