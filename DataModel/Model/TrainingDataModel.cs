using DataModel.Mapper;
using DataModel.Model;
using Domain.Model;

public class TrainingDataModel
{
    public long Id { get; set; }
    public ColaboratorsIdDataModel colaboratorId { get; set; }
    public DateOnly _startDate { get; set; }
    public DateOnly _endDate { get; set; }

    public TrainingDataModel() {}

    public TrainingDataModel(Training training, ColaboratorsIdDataModel colaboratorsIdDataModel)
    {
        Id = training.Id;

        // Assuming you have a mapper that converts from the domain Colaborator model to the data model
        colaboratorId = colaboratorsIdDataModel;

        _startDate = training.TrainingPeriod.StartDate;

        _endDate = training.TrainingPeriod.EndDate;
    }
}
