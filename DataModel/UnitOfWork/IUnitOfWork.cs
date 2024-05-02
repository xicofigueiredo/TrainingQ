using Domain.IRepository;

namespace UnitOfWork;

public interface IUnitOfWork : IDisposable
{
    IGenericRepository Generic { get; }

    ITrainingRepository TrainingRepository { get; }

    Task<int> CompleteAsync();
}
