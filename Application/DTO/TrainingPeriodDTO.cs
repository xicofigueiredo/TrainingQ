namespace Application.DTO;

using Domain.Model;

public class TrainingPeriodDTO
{
	//pretende-se não exteriorizar o id de persistência
	//public long Id { get; set; }

	// atenção: embora possa ser chave única, email não deve servir de chave primária para foreign keys; está assim para servir de exemplo.
	public DateOnly StartDate { get; set; }
	public DateOnly EndDate { get; set; }
	
	public TrainingPeriodDTO() {
	}

	public TrainingPeriodDTO(DateOnly startDate, DateOnly endDate)
	{
		//Id = id;
		StartDate = startDate;
		EndDate = endDate;
	}

	static public TrainingPeriodDTO ToDTO(TrainingPeriod trainingPeriod) {

		TrainingPeriodDTO TrainingPeriodDTO = new TrainingPeriodDTO(trainingPeriod.StartDate, trainingPeriod.EndDate);

		return TrainingPeriodDTO;
	}

	static public IEnumerable<TrainingPeriodDTO> ToDTO(IEnumerable<TrainingPeriod> trainingPeriods)
	{
		List<TrainingPeriodDTO> TrainingPeriodDTOs = new List<TrainingPeriodDTO>();

		foreach( TrainingPeriod trainingPeriod in trainingPeriods ) {
			TrainingPeriodDTO TrainingPeriodDTO = ToDTO(trainingPeriod);

			TrainingPeriodDTOs.Add(TrainingPeriodDTO);
		}

		return TrainingPeriodDTOs;
	}

	static public TrainingPeriod ToDomain(TrainingPeriodDTO TrainingPeriodDTO) {
		
		TrainingPeriod trainingPeriod = new TrainingPeriod(TrainingPeriodDTO.StartDate, TrainingPeriodDTO.EndDate);

		return trainingPeriod;
	}

}