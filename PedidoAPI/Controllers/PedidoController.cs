using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using PedidoAPI.Models;
using PedidoAPI.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PedidoAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PedidosController : ControllerBase
    {
        private readonly IMongoCollection<Pedido> _pedidoCollection;
        private readonly RabbitMqService _rabbitMqService;

        public PedidosController(IConfiguration config, RabbitMqService rabbitMqService)
        {
            var client = new MongoClient(config.GetConnectionString("MongoDb"));
            var database = client.GetDatabase("PedidosDB");
            _pedidoCollection = database.GetCollection<Pedido>("Pedidos");
            _rabbitMqService = rabbitMqService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(Pedido pedido)
        {
            if (pedido == null)
            {
                return BadRequest("Pedido inválido.");
            }

            await _pedidoCollection.InsertOneAsync(pedido);
            _rabbitMqService.PublicarMensagem(pedido.Descricao);
            return Ok(pedido);
        }

        [HttpGet]
        public async Task<List<Pedido>> GetAll()
        {
            return await _pedidoCollection.Find(_ => true).ToListAsync();
        }
    }
}
