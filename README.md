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

**URL:**  
http://localhost:5000/pedidos

**Headers:**  
Content-Type: application/json

**Body:**
```json
{
  "nomeCliente": "João da Silva",
  "descricao": "Pedido novo!",
  "valor": 57.80
}
```

**Response:**
```json
{
  "id": "a0c4c261-3e17-4344-920a-f4a18be943c7",
  "descricao": "Pedido novo!",
  "valor": 57.80,
  "cliente": "João da Silva",
  "status": "pendente"
}
```

---

### Listar Pedidos (GET /pedidos)

**URL:**  
http://localhost:5000/pedidos

**Response:**
```json
[
  {
    "id": "d6b01afd-859c-4785-b4a1-cf01bf58b89f",
    "descricao": "Pedido novo!",
    "valor": 57.80,
    "cliente": "João da Silva",
    "status": "processado"
  },
  {
    "id": "a0c4c261-3e17-4344-920a-f4a18be943c7",
    "descricao": "Pedido Atendimento",
    "valor": 60.00,
    "cliente": "João de Oliveira",
    "status": "pendente"
  }
]
```
