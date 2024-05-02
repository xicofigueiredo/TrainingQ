namespace DataModel.Mapper;

using DataModel.Model;
using Domain.Model;
using Domain.Factory;
using System.Linq;
using System;

public class TrainingMapper
{
    private ITrainingFactory _trainingFactory;

    public TrainingMapper(
        ITrainingFactory trainingFactory)
    {
        _trainingFactory = trainingFactory;
    }


    public Training ToDomain(TrainingDataModel trainingDM)
    {
        long id = trainingDM.Id;
        ColaboratorsIdDataModel colabId = trainingDM.colaboratorId;

        long cId = colabId.Id;

        ITrainingPeriodFactory _trainingPeriodFactory = new TrainingPeriodFactory();
        TrainingPeriod trainingPeriod = _trainingPeriodFactory.NewTrainingPeriod(trainingDM._startDate, trainingDM._endDate);
        

        Training trainingDomain = _trainingFactory.NewTraining(id,cId,trainingPeriod);
        
        return trainingDomain;
    }
 
    public IEnumerable<Training> ToDomain(IEnumerable<TrainingDataModel> trainingsDM)
    {
        List<Training> trainingsDomain = new List<Training>();

        foreach(TrainingDataModel trainingDataModel in trainingsDM)
        {
            Training trainingDomain = ToDomain(trainingDataModel);

            trainingsDomain.Add(trainingDomain);
        }

        return trainingsDomain.AsEnumerable();
    }

    

    public TrainingDataModel ToDataModel(Training training, ColaboratorsIdDataModel colaboratorsIdDataModel)
    {
        var trainingDataModel = new TrainingDataModel
        {
            Id = training.Id,
            colaboratorId = colaboratorsIdDataModel,
            _startDate = training.TrainingPeriod.StartDate,
            _endDate = training.TrainingPeriod.EndDate
        };

        return trainingDataModel;
    }
   
}