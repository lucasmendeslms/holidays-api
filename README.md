# Teste Técnico Backend - Treinamento de Skills 🚀

Este repositório foi criado para **praticar e aprimorar habilidades de desenvolvimento backend**, resolvendo o desafio técnico proposto pela [**Instruct**](https://github.com/instruct-br/teste-backend-remoto-2020-07). O objetivo é implementar uma **API REST** que gerencia feriados nacionais, estaduais e municipais no Brasil, permitindo consultas, cadastros e remoções.

## 🎯 Objetivo

Este projeto visa:
- Praticar **.NET 8.0**, explorando as novidades e melhorias.
- Implementar **Entity Framework Core** para manipulação eficiente de dados.
- Utilizar **PostgreSQL** como banco de dados.
- Melhorar a estruturação de código e boas práticas no desenvolvimento de APIs.

## 🛠 Tecnologias Utilizadas

- **.NET 8.0** – Framework principal para desenvolvimento da API.
- **Entity Framework Core** – ORM para facilitar a interação com o banco de dados.
- **PostgreSQL** – Banco de dados utilizado para armazenar os feriados.
- **ASP.NET Core** – Para criação da API REST.
- **Docker** – Para facilitar a configuração do ambiente.

## 📌 Descrição do Desafio

A **Corporação Colossal™** centraliza o atendimento ao cliente no Rio de Janeiro, pagando adicionais em feriados locais. O projeto busca distribuir os chamados entre outras filiais, reduzindo custos com atendimento em dias de feriado.

A API deve permitir:
- **Consulta de feriados por estado, município e nacionais**.
- **Cadastro de feriados estaduais, municipais e nacionais**.
- **Remoção de feriados**.

## 🔗 Endpoints da API

### 📅 Consultar feriado
```http
GET /feriados/{CODIGO-IBGE}/{ANO-MES-DIA}/
```

#### 🔄 Respostas da API

| **Status Code** | **Descrição**                                |
|-----------------|----------------------------------------------|
| **200 OK** | O feriado foi encontrado e retornado com sucesso. |
| **201 Created**   | O feriado foi cadastrado com sucesso. |


### ✏️ Cadastrar feriado
```http
PUT /feriados/{CODIGO-IBGE}/{MES-DIA}/
```

#### 🔄 Respostas da API

| **Status Code** | **Descrição**                                     |
|-----------------|---------------------------------------------------|
| **200 OK**      | O feriado já existia na data informada, então seu nome foi atualizado.                                                  |
| **201 Created**   | O feriado foi cadastrado com sucesso.           |


### ❌ Remover feriado
```http
DELETE /feriados/{CODIGO-IBGE}/{MES-DIA}/
```

#### 🔄 Respostas da API

| **Status Code**    | **Descrição**                                  |
|--------------------|------------------------------------------------|
| **204 No Content** | Removido com sucesso.                          |
| **404 Not Found**  | O feriado informado não existe no banco.       |
| **403 Forbidden**  | Tentativa de remover um feriado estadual em um Município.                                                            |
| **403 Forbidden**  | Tentativa de remover um feriado nacional em um município ou UF.                                                      |
