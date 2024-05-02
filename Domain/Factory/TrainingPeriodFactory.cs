namespace Domain.Factory;

using Domain.Model;

public class TrainingPeriodFactory:ITrainingPeriodFactory
{
    public TrainingPeriod NewTrainingPeriod(DateOnly startDate, DateOnly endDate)
    {
        return new TrainingPeriod(startDate, endDate);
    }
}
