# Trabalho Controle de Medicamentos

![](https://i.imgur.com/bCKO9DI.gif)

## Introdução

- O projeto Controle de Medicamentos tem como finalidade gerenciar o estoque, o cadastro e a distribuição de medicamentos em instituições de saúde, promovendo segurança, agilidade e conformidade com normas sanitárias.
- O sistema é desenvolvido em C# utilizando o .NET 8.0, com uma interface de linha de comando (CLI) para facilitar a interação do usuário.
- O projeto é uma solução básica para o gerenciamento de medicamentos, abrangendo desde o cadastro de funcionários e pacientes até a prescrição médica e controle de estoque.
- O sistema é modular, permitindo a adição de novas funcionalidades e melhorias conforme necessário.

---
## Tecnologias

[![Tecnologias](https://skillicons.dev/icons?i=git,github,cs,dotnet,visualstudio)](https://skillicons.dev)

---
## Funcionalidades
- Funcionário: Cadastrar, editar, excluir e visualizar.
- Paciente: Cadastrar, editar, excluir e visualizar.
- Fornecedor: Cadastrar, editar, excluir e visualizar.
- Medicamento: Cadastrar, editar, excluir e visualizar.
- Prescrição Médica: Cadastrar, editar, excluir e visualizar.
- Estoque de Medicamentos: Registrar entrada e saída, visualizar movimentações.

---
## Como utilizar

1. Clone o repositório ou baixe o código fonte.
2. Abra o terminal ou o prompt de comando e navegue até a pasta raiz
3. Utilize o comando abaixo para restaurar as dependências do projeto.

```
dotnet restore
```

4. Em seguida, compile a solução utilizando o comando:
   
```
dotnet build --configuration Release
```

5. Para executar o projeto compilando em tempo real
   
```
dotnet run --project Calculadora.ConsoleApp
```

6. Para executar o arquivo compilado, navegue até a pasta `./Calculadora.ConsoleApp/bin/Release/net8.0/` e execute o arquivo:
   
```
Calculdora.ConsoleApp.exe
```

## Requisitos

- .NET SDK (recomendado .NET 8.0 ou superior) para compilação e execução do projeto.

- Visual Studio 2022 ou superior (opcional, para desenvolvimento).
