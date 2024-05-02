namespace Application.DTO;

using Domain.Factory;
using Domain.Model;

public class TrainingDTO
{
	public long Id { get; set; }
	public long _colabId{ get; set; }

	public TrainingPeriodDTO _trainingPeriod { get; set; }

    public TrainingDTO() {
	}

	public TrainingDTO(long colabId,long id,TrainingPeriodDTO trainingPeriod)
	{
		Id = id;
		_colabId = colabId;
		_trainingPeriod = trainingPeriod;
	}

	static public TrainingDTO ToDTO(Training training) {
		long idColab = training.GetColaborator();
		long id = training.Id;
		TrainingPeriodDTO TrainingPeriodDTO = TrainingPeriodDTO.ToDTO(training.TrainingPeriod);
		TrainingDTO trainingDTO = new TrainingDTO(idColab,id,TrainingPeriodDTO);

		return trainingDTO;
	}

	static public IEnumerable<TrainingDTO> ToDTO(IEnumerable<Training> trainings)
	{
		List<TrainingDTO> trainingsDTO = new List<TrainingDTO>();

		foreach( Training training in trainings ) {
			TrainingDTO trainingDTO = ToDTO(training);

			trainingsDTO.Add(trainingDTO);
		}

		return trainingsDTO;
	}

	static public Training ToDomain(TrainingDTO trainingDTO) 
	{
		if (trainingDTO == null) 
		{
			throw new ArgumentException("trainingDTO must not be null");
		}

		TrainingPeriod trainingPeriod = TrainingPeriodDTO.ToDomain(trainingDTO._trainingPeriod);

		Training training = new Training(trainingDTO.Id,trainingDTO._colabId,trainingPeriod);

		
		// foreach (var periodDTO in trainingDTO.trainingPeriods)
		// {
		//     trainingPeriod period = TrainingPeriodDTO.ToDomain(periodDTO);
		//     training.AddtrainingPeriod(period);
		// }

		return training;
	}
}