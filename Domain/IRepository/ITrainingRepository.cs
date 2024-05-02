namespace Domain.IRepository;

using Domain.Model;

public interface ITrainingRepository : IGenericRepository<Training>
{
    Task<bool> TrainingExists(long id);
    Task<IEnumerable<Training>> GetTrainingsByIdAsync(long id);
    Task<IEnumerable<Training>> GetTrainingsAsync();

    Task<Training> AddTraining(Training training);
    Task<IEnumerable<Training>> GetTrainingsByColabIdAsync(long colabId);
    

    

}
