using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PedidoAPI.Models
{
    public class Pedido
    {
        [BsonId]
        public Guid Id { get; set; } = Guid.NewGuid();

        [BsonElement("descricao")]
        public string Descricao { get; set; }

        [BsonElement("valor")]
        [BsonRepresentation(BsonType.Decimal128)]
        public decimal Valor { get; set; }

        [BsonElement("nome_cliente")]
        public string Cliente { get; set; }

        [BsonElement("status")]
        public string Status { get; set; } = "pendente";
    }
}
