# ProjetoInovitech
Projeto teste Inovitech

## Como executar

```bash
docker-compose up --build
```

- Acesse a API em: http://localhost:5000
- Interface do RabbitMQ: http://localhost:15672 (usuario: guest / senha: guest)

## Endpoints

### Criar Pedido (POST /pedidos)
http://localhost:5000/pedidos
Headers:Content-Type = application/json
```json
{
  "nomeCliente": "João da Silva",
  "descricao": "Pedido de 2 pizzas",
  "valor": 59.90
}
```

### Listar Pedidos (GET /pedidos)
http://localhost:5000/pedidos

Retorna todos os pedidos com seus respectivos status.
