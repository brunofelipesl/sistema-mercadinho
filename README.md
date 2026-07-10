# sistema-mercadinho


## Pré-requisitos

- [Docker](https://www.docker.com/)


É necessário subir o container do banco de dados antes de rodar a aplicação, o mesmo é inicializado com a criação das tabelas e o usuário utilizado para autenticar a API.

```bash
docker compose up --build
```

| Serviço  | URL                   |
| -------- | ----------------------|
| API      | http://localhost:5203 |
| Postgres | localhost:5432        |
