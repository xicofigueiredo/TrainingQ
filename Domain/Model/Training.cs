namespace Domain.Model;

using Domain.Factory;

public class Training : ITraining
{

	public long Id {get; set;}
	private long colaboratorId{get; set;}

	private TrainingPeriod _trainingPeriod;

	public TrainingPeriod TrainingPeriod
	{
		get { return _trainingPeriod; }
	}

	public Training(long ColabId)
	{
		if (ColabId != null)
		{
			colaboratorId = ColabId;
		}
		else
			throw new ArgumentException("Invalid argument: colaboratorId must be non null");
	}

	public Training(long id, long ColabId)
	{
		if (ColabId != null)
		{
			colaboratorId = ColabId;
			Id = id;
		}
		else
			throw new ArgumentException("Invalid argument: colaboratorId must be non null");
	}

	public Training(long id, long ColabId,TrainingPeriod trainingPeriod)
	{
		if (ColabId != null)
		{
			colaboratorId = ColabId;
			Id = id;
			_trainingPeriod = trainingPeriod;
		}
		else
			throw new ArgumentException("Invalid argument: colaboratorId must be non null");
	}

	public long GetColaborator()
	{
		return colaboratorId;
	}

	public bool HasColaborador(long colabId)
	{
		return colaboratorId == colabId;
	}
}