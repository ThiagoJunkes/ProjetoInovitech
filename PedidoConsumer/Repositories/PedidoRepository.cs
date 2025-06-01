using MongoDB.Driver;
using PedidoConsumer.Models;
using System.Threading.Tasks;

namespace PedidoConsumer.Repositories
{
    public class PedidoRepository
    {
        private readonly IMongoCollection<Pedido> _pedidoCollection;

        public PedidoRepository()
        {
            var client = new MongoClient("mongodb://mongo:27017");
            var db = client.GetDatabase("PedidosDB");
            _pedidoCollection = db.GetCollection<Pedido>("Pedidos");
        }

        public async Task<Pedido> BuscarPedidoPendentePorDescricaoAsync(string descricao)
        {
            var filtro = Builders<Pedido>.Filter.And(
                Builders<Pedido>.Filter.Eq(p => p.Descricao, descricao),
                Builders<Pedido>.Filter.Eq(p => p.Status, "pendente")
            );

            return await _pedidoCollection.Find(filtro).FirstOrDefaultAsync();
        }

        public async Task AtualizarStatusPedidoAsync(Guid id)
        {
            var filtro = Builders<Pedido>.Filter.Eq(p => p.Id, id);
            var atualizacao = Builders<Pedido>.Update.Set(p => p.Status, "processado");
            await _pedidoCollection.UpdateOneAsync(filtro, atualizacao);
        }
    }
}
