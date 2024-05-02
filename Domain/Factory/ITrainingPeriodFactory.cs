namespace Domain.Factory;

using Domain.Model;

public interface ITrainingPeriodFactory
{
    TrainingPeriod NewTrainingPeriod(DateOnly startDate, DateOnly endDate);
}