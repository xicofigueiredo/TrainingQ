using RabbitMQ.Client;

using Application.DTO;
using RabbitMQ.Client.Events;
using System.Text;
using Application.Services;
using Newtonsoft.Json;
 
namespace WebApi.Controllers
{
    public class RabbitMQConsumerController : IRabbitMQConsumerController
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ConnectionFactory _factory;
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private string _queueName;
        private List<string> _errorMessages = new List<string>();
 
        public RabbitMQConsumerController(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
            _factory = new ConnectionFactory { HostName = "localhost" };
            _connection = _factory.CreateConnection();
            _channel = _connection.CreateModel();
 
            _channel.ExchangeDeclare(exchange: "training_logs", type: ExchangeType.Fanout);
 
            Console.WriteLine(" [*] Waiting for messages from training.");
        }

        public void ConfigQueue(string queueName)
        {
            _queueName = queueName;

            _channel.QueueDeclare(queue: _queueName,
                                            durable: true,
                                            exclusive: false,
                                            autoDelete: false,
                                            arguments: null);

            _channel.QueueBind(queue: _queueName,
                  exchange: "training_logs",
                  routingKey: string.Empty);
        }

        public void StartConsuming()
        {
            Console.WriteLine("trainings");
           
            var consumer = new EventingBasicConsumer(_channel);
           
            consumer.Received += async (model, ea) =>
            {
                byte[] body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                //_colaboratorIdService.Add(colaborador);
                var trainingResult = JsonConvert.DeserializeObject<trainingDTO>(message);
                var trainingDTO = new trainingDTO
                {
                    Id = trainingResult.Id,
                    _colabId = trainingResult._colabId,
                    _trainingPeriod = trainingResult._trainingPeriod
                };
           
 
 
                using (var scope = _scopeFactory.CreateScope()){
                    var trainingService = scope.ServiceProvider.GetRequiredService<trainingService>();
                    await trainingService.Add(trainingDTO, _errorMessages);
                    Console.WriteLine("training criada");
                };
            };
            _channel.BasicConsume(queue: _queueName,
                                autoAck: true,
                                consumer: consumer);
       
        }
 
        public void StopConsuming()
        {
            throw new NotImplementedException();
        }
    }
}