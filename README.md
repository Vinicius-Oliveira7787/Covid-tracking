<h1 align="center">
  Covid Tracking Project
</h1>

<p align="center">
  <a href="#%EF%B8%8F-about-the-project">About the project</a>&nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;&nbsp;
  <a href="#✨-Endpoints">Endpoints</a>&nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;&nbsp;
  <a href="#-technologies">Technologies</a>&nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;&nbsp;
  <a href="#-getting-started">Getting started</a>&nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;&nbsp;
  <a href="#-how-to-contribute">How to contribute</a>&nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;&nbsp;
  <a href="#-license">License</a>
</p>

## 💇🏻‍♂️ About the project

Esse Projeto consiste em consumir uma api publica com dados atualizados do covid onde podemos extrair dados de um pais especifico, também como extrair de todos os países do mundo.

## ✨ Endpoints


**Para acessar qualquer endpoint relacionado aos dados do covid é necessário possuir um cookie mostrando que você está logado.**

Vamos lá 😄

### Usuário
login do usuário.
- [https://localhost:5001/user/login POST](https://localhost:5001/user/login) Este endpoint não espera nenhum corpo ao fazer a requisição.

logout do usuário
- [https://localhost:5001/user/logout POST](https://localhost:5001/user/logout) Este endpoint não espera nenhum corpo ao fazer a requisição.

### Países
Pesquisar por um pais especifico e salva-lo em nosso banco de dados
- [https://localhost:5001/covidapi/country POST](https://localhost:5001/covidapi/country) Este endpoint espera um {"CountryName": "string"} ao fazer a requisição.

Buscar um pais do nosso banco de dados pelo nome
- [https://localhost:5001/covidapi/country GET](https://localhost:5001/covidapi/country) Este endpoint espera um {"CountryName": "string"} ao fazer a requisição.

Retornar todos os países salvos no banco de dados
- [https://localhost:5001/covidapi/countries GET](https://localhost:5001/covidapi/countries) Este endpoint não espera nenhum corpo ao fazer a requisição.

Países ordenados de acordo com o número de casos ativos do maior 
para o menor, apresentando a diferença em porcentagem de casos de um país para o 
outro.
- [https://localhost:5001/covidapi/country/percentage GET](https://localhost:5001/covidapi/country/percentage) Este endpoint não espera nenhum corpo ao fazer a requisição.

Atualizar os dados de um pais
- [https://localhost:5001/covidapi/update PUT](https://localhost:5001/covidapi/update) Este endpoint espera um {"CountryName": "string"} ao fazer a requisição.

Apagar um pais do banco de dados
- [https://localhost:5001/covidapi/delete DELETE](https://localhost:5001/covidapi/delete) Este endpoint espera um {"CountryName": "string"} ao fazer a requisição.

Ficou com alguma duvida? [Clique aqui](https://localhost:5001/swagger/index.html) com a api rodando para ver nossos endpoints detalhados com o swagger


## 🚀 Technologies

Tecnologias utilizadas na construção do projeto 

- [C#](https://docs.microsoft.com/pt-br/dotnet/csharp/)
- [Docker](https://docs.docker.com/get-started/)
- [MySql](https://dev.mysql.com/doc/dev/connector-net/8.0/html/connector-net-reference.htm)
- [Entity Framework Core](https://dev.mysql.com/doc/connector-net/en/connector-net-introduction.html)
- [Rest MVC](https://docs.microsoft.com/pt-br/aspnet/mvc/overview/getting-started/introduction/getting-started)
- [FluentValidation](https://docs.fluentvalidation.net/en/latest/aspnet.html)
- [Swagger](https://swagger.io/docs/)
- [JWT-token](https://github.com/AzureAD/azure-activedirectory-identitymodel-extensions-for-dotnet)
- [Xunit](https://xunit.net/)
- [Mock](https://github.com/Moq/moq4/wiki/Quickstart)

## 💻 Getting started

### Requirements

- [.Net SDK and Runtime](https://dotnet.microsoft.com/download)
- [Docker](https://www.docker.com/products/docker-desktop)

> Obs.: Recomendo usar docker para melhor praticidade do que o próprio banco de dados

1. execute o comando 'docker-compose up' na raiz do projeto
2. execute o comando 'dotnet ef migrations add nome da migration' dentro da pasta Infra
**Verifique se foi criado o banco de dados chamado db. Se não crie manualmente**
3. execute o comando 'dotnet run' dentro da pasta WebApi

Pronto 😊, já pode usufruir da aplicação

## 🤔 How to contribute


If you want to contribute to this starter, consider:

- Reporting bugs and errors
- Improve the documentation
- Creating new features and pull requests

All contributions are welcome!


## 📝 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

---

Feito com 💙 por Vinicius de Oliveira 👋 [linkedin](https://www.linkedin.com/in/vinicius-de-oliveira-2273821a0/)