# Projeto de Orquestração de Pedidos com Azure Durable Functions

Este projeto demonstra o uso de Azure Durable Functions para orquestrar um fluxo de trabalho de aprovação de pedidos. Utilizando o padrão de design "Function Chaining", o projeto simula um processo de negócio que inclui o recebimento de um pedido, processamento de pagamento, abertura de ticket de envio, e finalização do pedido.

## Características

- **Azure Durable Functions**: Utiliza Azure Durable Functions para orquestrar um processo de negócio de ponta a ponta.
- **Padrão Function Chaining**: Demonstra o uso do padrão "Function Chaining" para encadear várias funções de atividade em uma sequência lógica.
- **Trigger HTTP**: Inicia o fluxo de trabalho com um trigger HTTP que aceita um objeto de pedido como input.
- **Geração de Ticket Único**: Usa GUIDs para gerar um número de ticket único para cada pedido processado.
- **Logs Enriquecidos**: Incorpora logs detalhados em cada etapa do processo para facilitar o monitoramento e debugging.
- **Simulação de Atraso**: Inclui simulações de atraso entre as funções para representar o processamento do tempo real e tornar a visualização dos logs mais interessante.

## Tecnologias Utilizadas

- Azure Functions
- Azure Durable Functions
- C# (.NET Core)

## Como Iniciar

Para iniciar o processo de orquestração de pedidos, faça uma requisição POST para o endpoint exposto pela Azure Function com o seguinte formato de payload:

```json
{
  "CustomerName": "Nome do Cliente",
  "DeliveryAddress": "Endereço de Entrega",
  "Items": [
    {
      "ProductName": "Nome do Produto",
      "Quantity": 1,
      "ProductValue": 100.00
    }
  ],
  "TotalValue": 100.00
}
```

## Estrutura do Projeto

A estrutura de pastas e arquivos do projeto é organizada da seguinte maneira:

```
FIAP.Function/
│
├── Models/                   # Pasta para modelos de dados
│   ├── Order.cs              # Definição da classe Order
│   └── OrderItem.cs          # Definição da classe OrderItem
│
├── Activities/               # Pasta para funções de atividade
│   └── OrderActivities.cs    # Todas as funções de atividade combinadas
│
├── Orchestrators/            # Pasta para funções orquestradoras
│   └── OrderApprovalOrchestrator.cs
│
└── Starters/                 # Pasta para funções iniciadoras
    └── HttpStart.cs

```

Esta estrutura facilita a organização do código e melhora a manutenibilidade do projeto.
