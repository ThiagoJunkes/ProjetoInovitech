using PedidoConsumer.Models;
using PedidoConsumer.Repositories;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace PedidoConsumer.Services
{
    public class PedidoConsumerService
    {
        public void Consumir()
        {
            var factory = new ConnectionFactory()
            {
                HostName = Environment.GetEnvironmentVariable("RABBITMQ_HOST") ?? "localhost",
                UserName = "guest",
                Password = "guest"
            };
            IConnection connection = null;
            for (int i = 1; i <= 6; i++)
            {
                try
                {
                    connection = factory.CreateConnection();
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro {i} ao conectar no RabbitMQ: {ex.Message}");
                    Thread.Sleep(3000);
                }
            }

            if (connection == null)
            {
                Console.WriteLine("Não foi possível conectar ao RabbitMQ");
                return;
            }
            var channel = connection.CreateModel();

            Console.WriteLine("RabbitMQ Conectado!");

            channel.QueueDeclare(queue: "pedidos", durable: false, exclusive: false, autoDelete: false);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += async (model, ea) =>
            {
                PedidoRepository _repository = null;
                try
                {
                    _repository = new PedidoRepository();
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Erro ao conectar com o banco de dados: {e.Message}");
                    return;
                }

                var body = ea.Body.ToArray();
                var descricao = Encoding.UTF8.GetString(body);

                Console.WriteLine($"Recebido pedido: {descricao}");

                var pedido = await _repository.BuscarPedidoPendentePorDescricaoAsync(descricao);

                if (pedido == null)
                {
                    Console.WriteLine($"Nenhum pedido pendente com descrição '{descricao}' encontrado.");
                    return;
                }

                Console.WriteLine($"Processando pedido: {pedido.Id}:{pedido.Descricao}");

                await Task.Delay(3500);

                await _repository.AtualizarStatusPedidoAsync(pedido.Id);

                Console.WriteLine($"Pedido {pedido.Id}:{pedido.Descricao} processado.");
            };

            channel.BasicConsume(queue: "pedidos", autoAck: true, consumer: consumer);
        }
    }
}