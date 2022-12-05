# API Restful .Net Core 6

### Treinamento para desenvolvimento de API em .Net Core 6

#### Sequência de passos do canal youtube Manual do Programador

```
https://www.youtube.com/watch?v=ufjRbiaoou4&list=PLP4r6dpm_h-vPhZ-OXz3B5dcKpohAjhUE&index=1
```

#### Características técnicas:

##### Entidades
1) Person
2) PersonImage
3) Product
4) Purchase
5) User

#### Estrutura em camadas:
1) Domain - Class Library
2) Application - Class Library
3) Data - Class Library
4) IoC - Class Library
5) API - Console Application

ORM => Entity Framework\
Banco de dados => Postgres\
Autenticação => JWT Bearer

#### Para testar:
1) Criar uma base de dados em Postgres
2) Criar tabelas das entidades
3) Substituir os dados de conexão em MP.ApiDotNet6.Api/appsettings.json em "DefaultConnections"
4) Rodar projeto e no Swagger criar o token para liberar rotas protegidas dos controllers Person/Purchase

## ENJOY
