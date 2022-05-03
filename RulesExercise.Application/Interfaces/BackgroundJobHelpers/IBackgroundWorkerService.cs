using System.Linq.Expressions;

namespace RulesExercise.Application.Interfaces.BackgroundJobHelpers
{
    public interface IBackgroundWorkerService
    {
        void Enqueue<T>(Expression<Action<T>> serviceHandler);

        void Enqueue(Expression<Action> handler);
    }
}
