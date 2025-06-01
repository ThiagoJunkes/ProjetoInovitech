using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;
using PedidoAPI.Models;

namespace PedidoAPI.Services
{
    public class RabbitMqService
    {
        private readonly IConfiguration _config;

        public RabbitMqService(IConfiguration config)
        {
            _config = config;
        }

        public void PublicarMensagem(String mensagem)
        {
            var factory = new ConnectionFactory() { HostName = _config["RabbitMQHost"] };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(queue: "pedidos", durable: false, exclusive: false, autoDelete: false);

            var body = Encoding.UTF8.GetBytes(mensagem);

            channel.BasicPublish(exchange: "", routingKey: "pedidos", basicProperties: null, body: body);

            Console.WriteLine("Mensagem Enviada!");
        }
    }
}