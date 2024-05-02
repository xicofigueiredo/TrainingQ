namespace DataModel.Repository;

using Microsoft.EntityFrameworkCore;

using DataModel.Model;
using DataModel.Mapper;

using Domain.Model;
using Domain.IRepository;
using Microsoft.EntityFrameworkCore.ChangeTracking;

public class TrainingRepository : GenericRepository<Training>, ITrainingRepository
{    
    TrainingMapper _trainingMapper;
    ColaboratorsIdMapper _colaboratorsIdMapper;
    public TrainingRepository(AbsanteeContext context, TrainingMapper mapper,ColaboratorsIdMapper colaboratorsIdMapper) : base(context!)
    {
        _trainingMapper = mapper;
        _colaboratorsIdMapper = colaboratorsIdMapper;
    }

    public async Task<IEnumerable<Training>> GetTrainingsAsync()
    {
        try {
            IEnumerable<TrainingDataModel> trainingsDataModel = await _context.Set<TrainingDataModel>()
                    .Include(c => c.colaboratorId)
                    .ToListAsync();

            IEnumerable<Training> trainings = _trainingMapper.ToDomain(trainingsDataModel);

            return trainings;
        }
        catch
        {
            throw;
        }
    }

    public async Task<IEnumerable<Training>> GetTrainingsByIdAsync(long id)
    {
        try {
            IEnumerable<TrainingDataModel> trainingDataModel = await _context.Set<TrainingDataModel>()
                    .Include(c => c.colaboratorId)
                    .Where(c => c.Id==id)
                    .ToListAsync();

            IEnumerable<Training> trainings = _trainingMapper.ToDomain(trainingDataModel);

            return trainings;
        }
        catch
        {
            throw;
        }
    }
    public async Task<IEnumerable<Training>> GetTrainingsByColabIdAsync(long colabId)
    {
        try {
            IEnumerable<TrainingDataModel> trainingsDataModel = await _context.Set<TrainingDataModel>()
                    .Include(c => c.colaboratorId)
                    .Where(c => c.colaboratorId.Id==colabId)
                    .ToListAsync();

            if(trainingsDataModel== null){
                return null;
            }

            IEnumerable<Training> trainings = _trainingMapper.ToDomain(trainingsDataModel);

            return trainings;
        }
        catch
        {
            throw;
        }
    }

    public async Task<Training> AddTraining(Training training)
    {
        try {

            ColaboratorsIdDataModel colaboratorDataModel = await _context.Set<ColaboratorsIdDataModel>()
                .FirstAsync(c => c.Id == training.GetColaborator());
            TrainingDataModel trainingDataModel = _trainingMapper.ToDataModel(training,colaboratorDataModel);

            EntityEntry<TrainingDataModel> trainingDataModelEntityEntry = _context.Set<TrainingDataModel>().Add(trainingDataModel);
            
            await _context.SaveChangesAsync();

            TrainingDataModel trainingDataModelSaved = trainingDataModelEntityEntry.Entity;

            Training trainingSaved = _trainingMapper.ToDomain(trainingDataModelSaved);

            return trainingSaved;    
        }
        catch
        {
            throw;
        }
    }

    public async Task<bool> TrainingExists(long id)
    {
        return await _context.Set<TrainingDataModel>().AnyAsync(e => e.Id == id);
    }
}