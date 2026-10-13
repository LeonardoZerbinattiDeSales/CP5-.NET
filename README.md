# 🎮 CP5 — API de Catálogo de Jogos | .NET + MongoDB

## 📌 Sobre o Projeto

Projeto desenvolvido para o Checkpoint 5 da disciplina de .NET, com o objetivo de implementar uma API REST para gerenciamento de um catálogo de jogos de videogame.

A aplicação utiliza **.NET, C# e MongoDB**, permitindo realizar operações de CRUD (Create, Read, Update e Delete), além de consultas avançadas para filtrar jogos e gerar relatórios de estoque.


## 👥 Integrantes do Grupo

Projeto desenvolvido pelos alunos da **FIAP**, como parte da avaliação de Checkpoint 5 da disciplina de .NET.

| Nome | RM |
|---|---|
| Luis Guilherme Borges Silva | RM 566548|
| Leornardo Zerbinatti de Sales | RM 562992 |


## 🛠️ Tecnologias Utilizadas

- **C# / .NET 8** — Desenvolvimento da API REST
- **ASP.NET Core Web API** — Criação dos endpoints
- **MongoDB** — Banco de dados NoSQL
- **MongoDB.Driver** — Integração com o banco
- **Docker** — Execução do MongoDB em container
- **Swagger** — Documentação e testes dos endpoints
- **JetBrains Rider** — Ambiente de desenvolvimento

## 🏗️ Arquitetura

O projeto segue uma separação de responsabilidades em camadas:

- **Controller:** recebe as requisições HTTP e retorna as respostas.
- **Service:** concentra a lógica de negócio.
- **Repository:** realiza as operações de acesso ao MongoDB.
- **Model:** representa os documentos armazenados no banco.

A configuração da conexão com o MongoDB utiliza o padrão **Options Pattern**, por meio de `IOptions<MongoDbSettings>`.

## 📋 Funcionalidades

### CRUD de Jogos

| Método | Endpoint | Descrição |
|---|---|---|
| POST | `/api/Jogos` | Cadastrar um novo jogo |
| GET | `/api/Jogos` | Listar todos os jogos |
| GET | `/api/Jogos/{id}` | Buscar jogo pelo ID |
| PUT | `/api/Jogos/{id}` | Atualizar um jogo |
| DELETE | `/api/Jogos/{id}` | Excluir um jogo |

### Consultas Avançadas

| Método | Endpoint | Descrição |
|---|---|---|
| GET | `/api/Jogos/busca` | Filtrar jogos por plataforma e preço máximo |
| GET | `/api/Jogos/relatorio-estoque` | Agrupar jogos por plataforma e calcular o valor total em estoque |

O relatório de estoque apresenta a quantidade de títulos cadastrados por plataforma e o valor total do inventário, calculado pela multiplicação do preço pela quantidade em estoque.

## 🚀 Como Executar

### 1. Pré-requisitos

- .NET SDK 8
- Docker Desktop
- Git

### 2. Clonar o repositório

```bash
git clone <URL_DO_REPOSITORIO>
cd CP5-.NET
```

### 3. Iniciar o MongoDB no Docker

Com o Docker Desktop em execução, utilize:

```bash
docker run -d -p 27017:27017 --name mongo-local mongo
```

Para verificar se o container está funcionando:

```bash
docker ps
```

Caso o container já exista e esteja parado:

```bash
docker start mongo-local
```

### 4. Configurar a conexão

Verifique no arquivo `appsettings.json` se a string de conexão aponta para o MongoDB local:

```text
mongodb://localhost:27017
```

O nome do banco deve corresponder à configuração utilizada pela aplicação.

### 5. Executar a API

No diretório do projeto:

```bash
dotnet restore
dotnet build
dotnet run
```

### 6. Acessar o Swagger

Com a aplicação em execução, acesse:

```text
http://localhost:5102/swagger
```

A porta pode variar conforme a configuração do ambiente.

## 🧪 Testes Realizados

Os endpoints foram testados utilizando o Swagger, com o MongoDB executando em um container Docker.

Foram validadas as seguintes funcionalidades:

- Cadastro de jogos (POST).
- Listagem de jogos (GET).
- Consulta de jogo por ID (GET).
- Atualização de informações (PUT).
- Exclusão de jogos (DELETE).
- Filtro por plataforma e faixa de preço.
- Relatório de estoque agrupado por plataforma.

A persistência dos dados também foi verificada diretamente no MongoDB por meio do `mongosh`.

## 📦 Exemplo de Cadastro

Requisição `POST /api/Jogos`:

```json
{
  "titulo": "God of War",
  "plataforma": "PlayStation 3",
  "genero": "Ação",
  "preco": 99.90,
  "anoLancamento": 2013,
  "estoque": 10
}
```

```json
{
  "titulo": "Gran Turismo 5",
  "plataforma": "PlayStation 3",
  "genero": "Corrida",
  "preco": 150.00,
  "anoLancamento": 2010,
  "estoque": 5
}
```

A API realiza o cadastro e retorna o documento com seu identificador gerado pelo MongoDB.

## 📊 Relatório de Estoque

O endpoint `/api/Jogos/relatorio-estoque` permite consultar um resumo do inventário agrupado por plataforma.

O valor total é calculado pela soma de `Preço × Estoque` de cada jogo.

---

**Projeto acadêmico — Checkpoint 5 | .NET & MongoDB**
