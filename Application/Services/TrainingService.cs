namespace Application.Services;

using Domain.Model;
using Application.DTO;

using Microsoft.EntityFrameworkCore;
using DataModel.Repository;
using Domain.IRepository;
using Domain.Factory;
using DataModel.Model;
using Gateway;
using System;
using System.Collections;
using Microsoft.IdentityModel.Tokens;


public class TrainingService {

    private readonly AbsanteeContext _context;
    private readonly ITrainingRepository _trainingRepository;
    private readonly IColaboratorsIdRepository _colaboratorsIdRepository;
    private readonly ITrainingPeriodFactory _trainingPeriodFactory;
    private readonly TrainingAmpqGateway _trainingAmqpGateway;


    
    public TrainingService(ITrainingRepository trainingRepository, ITrainingPeriodFactory trainingPeriodFactory, TrainingAmpqGateway trainingAmqpGateway,IColaboratorsIdRepository colaboratorsIdRepository) {
        _trainingRepository = trainingRepository;
        _trainingPeriodFactory = trainingPeriodFactory;
        _trainingAmqpGateway=trainingAmqpGateway;
        _colaboratorsIdRepository = colaboratorsIdRepository;
    }

    public async Task<IEnumerable<TrainingDTO>> GetAll()
    {    
        IEnumerable<Training> trainings = await _trainingRepository.GetTrainingsAsync();

        IEnumerable<TrainingDTO> trainingsDTO = TrainingDTO.ToDTO(trainings);

        return trainingsDTO;
    }

    public async Task<IEnumerable<TrainingDTO>> GetTrainingById(long id)
    {    
        IEnumerable<Training> trainings = await _trainingRepository.GetTrainingsByIdAsync(id);

        IEnumerable<TrainingDTO> trainingsDTO = TrainingDTO.ToDTO(trainings);

        return trainingsDTO;
    }

    public async Task<TrainingDTO> Add(TrainingDTO trainingDto, List<string> errorMessages)
    {
        bool bExists = await _trainingRepository.TrainingExists(trainingDto.Id);
        bool colabExists = await _colaboratorsIdRepository.ColaboratorExists(trainingDto._colabId);
        if(bExists) {
            errorMessages.Add("Training already exists");
            return null;
        }
        if(!colabExists) {
            errorMessages.Add("Colab doesn't exist");
            return null;
        }

        Training training = TrainingDTO.ToDomain(trainingDto);

        training = await _trainingRepository.AddTraining(training);

        TrainingDTO trainingDTO = TrainingDTO.ToDTO(training);

        return trainingDTO;
    }


    public async Task<IEnumerable<TrainingPeriodDTO>> GetTrainingPeriodsOnTrainingById(long colabId, DateOnly startDate, DateOnly endDate,List<string> errorMessages)
    {

        IEnumerable<Training> trainings = await _trainingRepository.GetTrainingsByColabIdAsync(colabId);

        List<TrainingPeriod> trainingPeriods = new List<TrainingPeriod>();
        
        if(trainings.IsNullOrEmpty()) {
            errorMessages.Add("No trainings found for this colaborator.");
            return null;
        }

        foreach(Training training in trainings){
            TrainingPeriod trainingPeriod = training.TrainingPeriod;
            if(trainingPeriod.EndDate > startDate && trainingPeriod.StartDate< endDate){
                trainingPeriods.Add(trainingPeriod);
            }
        }
        
        return TrainingPeriodDTO.ToDTO(trainingPeriods);


    }
    //fazer em vez disto, um get do repositório dos trainingPeriods, gettrainingPeriodsByColabId no repo?,linha 133
    //fazer o foreach todo no repo, passar tudo para o repositório da trainingPeriod?
    public async Task<List<long>> GetColabsComFeriasSuperioresAXDias(long xDias,List<string> errorMessages)
    {
        IEnumerable<ColaboratorId> lista = await _colaboratorsIdRepository.GetColaboratorsIdAsync();

        List<long> colabsComFeriasSuperioresAXDias = new List<long>();

        foreach(ColaboratorId colabId in lista){
            IEnumerable<Training> trainings = await _trainingRepository.GetTrainingsByColabIdAsync(colabId.colabId);

            foreach(Training training in trainings){
                long days = training.TrainingPeriod.GetNumberOfDays();
                if(days>xDias){
                    colabsComFeriasSuperioresAXDias.Add(colabId.colabId);
                    break;
                }
            }
        }
        return colabsComFeriasSuperioresAXDias;

    }

    

    

}