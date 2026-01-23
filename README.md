# Microsserviço Produção

## Descrição
O microsserviço **Produção** gerencia o fluxo de produção dos pedidos:
- Acompanhar fila de pedidos
- Atualizar status de cada etapa de produção
- Comunicação com microsserviços Pedido e Pagamento

## Observações

CI/CD configurado via GitHub Actions  
Branch main protegida, PR obrigatório  
Testes unitários com cobertura ≥80%  
<img width="1181" height="184" alt="image" src="https://github.com/user-attachments/assets/e921232c-530b-4d45-b961-393b5a0f2536" />


## Tecnologias
- .NET 8
- C#
- MongoDB
- GitHub Actions para CI/CD
- Docker para containerização

## Funcionalidades
- Listar pedidos em produção
- Atualizar status de produção
- Validar etapas concluídas
- Testes unitários com cobertura ≥80%

## Como executar
### Local
1. Clone o repositório
```bash
git clone <URL_DO_REPOSITORIO>
cd producao
```

2. Build e restore
```bash
dotnet restore
dotnet build
```

3. Rodar testes
```bash
dotnet test --collect:"XPlat Code Coverage"
```
3. Executar container Docker
```bash
docker build -t producao-service:latest .
docker run -d -p 5003:5000 --name producao-service producao-service:latest
```

## Endpoints
GET /api/producao → Listar pedidos em produção
PATCH /api/producao/{id} → Atualizar status de produção


