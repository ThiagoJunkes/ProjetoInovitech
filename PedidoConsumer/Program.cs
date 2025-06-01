using PedidoConsumer.Services;

var consumerService = new PedidoConsumerService();

await Task.Delay(3000);

consumerService.Consumir();
await Task.Delay(Timeout.Infinite);