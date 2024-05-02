namespace Domain.Factory;

using Domain.Model;

public interface ITrainingFactory
{
    Training NewTraining(long id,long colaboratorId, TrainingPeriod trainingPeriod);
}