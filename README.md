# AgendaUVV — Sistema de Gestão de Consultas

## Sobre o projeto

O AgendaUVV é um sistema web para gerenciar consultas médicas e profissionais, feito em C# com ASP.NET Core MVC. Cada usuário cria sua conta, faz login e gerencia só as próprias consultas — criar, editar, ver detalhes e excluir. Os dados ficam salvos em um banco SQL Server via Entity Framework Core.

Trabalho da disciplina de Desenvolvimento Web Back-end.

## Aplicação publicada

O sistema está no ar, rodando no Azure:

🔗 https://agendauvv-thallis-fcgqcqfwfmfzf2ct.westus3-01.azurewebsites.net

## Vídeo demonstrativo

🎥 https://youtu.be/Yl-FfhZ0j_A

No vídeo mostramos cadastro de usuário, login, dashboard, criação de consulta, listagem, edição, exclusão e logout.

## Funcionalidades

- Cadastro de usuário, com validação dos dados e senha protegida por hash
- Login e logout com autenticação por Cookie
- Dashboard mostrando as próximas consultas do usuário
- CRUD completo de consultas — só o dono da consulta pode ver, editar ou excluir
- Especialidade escolhida em uma lista suspensa com 20 opções, pra evitar erro de digitação
- Data e hora escolhidas em um calendário, sempre no formato brasileiro (dia/mês/ano, 24h), não importa a configuração do computador de quem acessa
- Nenhum usuário consegue acessar consulta de outro, nem digitando a URL na mão

## Tecnologias

- C# / .NET SDK 10.0.400
- ASP.NET Core MVC
- Entity Framework Core (Code First)
- SQL Server (local) e Azure SQL Database (produção)
- Azure App Service, pra hospedar o site
- Cookie Authentication + PasswordHasher, pra login e senha
- Bootstrap 5.3.3
- jQuery Validation, pra validação nos formulários
- Flatpickr, pro calendário
- Git, GitHub e GitHub Actions (deploy automático)

## Estrutura do projeto

```
AgendaUVV/
│
├── Controllers/
│   ├── HomeController.cs
│   ├── AccountController.cs
│   └── ConsultasController.cs
│
├── Models/
│   ├── Usuario.cs
│   ├── Consulta.cs
│   └── ErrorViewModel.cs
│
├── ViewModels/
│   ├── RegisterViewModel.cs
│   ├── LoginViewModel.cs
│   └── ConsultaViewModel.cs
│
├── Data/
│   └── AppDbContext.cs
│
├── Views/
│   ├── Home/
│   ├── Account/
│   ├── Consultas/
│   └── Shared/
│
├── wwwroot/
│   └── css/site.css
│
├── Migrations/
│
├── Program.cs
├── appsettings.json
└── README.md
```

## Banco de dados

Duas entidades principais:

**Usuario** — Id, Nome, Email, SenhaHash, DataCadastro

**Consulta** — Id, Especialidade, DataHora, Descricao, UsuarioId

Um usuário pode ter várias consultas, e cada consulta pertence a um único usuário (relação 1 para N).

## Como rodar o projeto localmente

Precisa ter instalado:

- .NET SDK 10.0.400 ou compatível
- SQL Server local (Developer ou Express)
- Git
- VS Code com C# Dev Kit
- dotnet-ef instalado globalmente (`dotnet tool install --global dotnet-ef`)

Passos:

git clone https://github.com/thallisamorim/AgendaUVV.git
cd AgendaUVV
dotnet restore


Confira a Connection String no `appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=AgendaUVV;Trusted_Connection=True;TrustServerCertificate=True"
}
```

Rode as migrations pra criar o banco:

dotnet ef database update


E execute o projeto:

dotnet run


Depois é só acessar o endereço que aparece no terminal. Se preferir não instalar nada, o site já está publicado no link lá em cima.

## Segurança

- Login com Cookie Authentication do próprio ASP.NET Core
- Senha guardada com hash (PasswordHasher), nunca em texto puro
- Rotas do Dashboard e das Consultas protegidas com `[Authorize]`
- Middleware na ordem certa: autenticação antes da autorização
- Toda busca de consulta no banco confere `Id` e `UsuarioId` juntos, então ninguém acessa consulta de outro usuário mesmo tentando pela URL
- Validação com Data Annotations em todos os formulários
- A senha do banco de produção fica só nas configurações do Azure, nunca no código

## Testes que fizemos

- Cadastro: dados válidos, campos vazios, e-mail inválido, senha curta, senhas diferentes, e-mail repetido
- Login: certo, senha errada, e-mail que não existe, tentativa sem estar logado
- CRUD de consultas completo, com e sem descrição preenchida
- Tentativa de acessar consulta de outro usuário digitando a URL — deu 404 como esperado
- Dois usuários diferentes, cada um só vendo as próprias consultas

## Equipe

- Matheus Lopes Rosa
- Thallis Silveira de Amorim

## Sobre o trabalho

Desenvolvido para a disciplina de Desenvolvimento Web Back-end, curso de Análise e Desenvolvimento de Sistemas, 3º semestre, na UVV. Professor Fabricio Ribeiro Ferreira.

## Licença

Projeto feito só pra fins acadêmicos.