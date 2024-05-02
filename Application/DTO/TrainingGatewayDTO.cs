using System.Text.Json;

namespace Application.DTO
{
    public class TrainingGatewayDTO
    {
        public long colaboratorId {get; set;}

        public TrainingGatewayDTO(){}
        
        public TrainingGatewayDTO(long colabId)
        {
            colaboratorId = colabId;
        }

         public static string Serialize(TrainingDTO trainingDTO)
        {
            var jsonMessage = JsonSerializer.Serialize(trainingDTO);
            return jsonMessage;
        }

        public static TrainingDTO Deserialize(string jsonMessage)
        {
            var trainingAmqpDTO = JsonSerializer.Deserialize<TrainingDTO>(jsonMessage);
            return trainingAmqpDTO!;
        }
    }
}