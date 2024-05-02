namespace Domain.Factory;

using Domain.Model;

public class TrainingFactory: ITrainingFactory 
{
    public Training NewTraining(long id, long colaboratorId,TrainingPeriod trainingPeriod)
    {
        return new Training(id,colaboratorId,trainingPeriod);
    }
}