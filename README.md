# AgendaUVV — Sistema de Gestão de Consultas

## 📌 Sobre o projeto

Aplicação Web desenvolvida em C# com ASP.NET Core MVC para gerenciamento de usuários e registro de consultas médicas/profissionais. O sistema permite que cada usuário se cadastre, faça login e gerencie exclusivamente suas próprias consultas, com persistência de dados via Entity Framework Core e SQL Server.

Trabalho acadêmico desenvolvido para a disciplina de Desenvolvimento Web Back-end.

## 🌐 Aplicação publicada

🔗 Acesse o sistema em produção: https://agendauvv-thallis-fcgqcqfwfmfzf2ct.westus3-01.azurewebsites.net

## 🎯 Funcionalidades

- Cadastro de usuário (com validação de dados e senha criptografada)
- Login e logout com autenticação baseada em Cookie
- Dashboard com resumo das próximas consultas do usuário logado
- CRUD completo de consultas (criar, listar, editar, excluir), restrito ao usuário autenticado
- Seleção de especialidade médica por lista suspensa (20 especialidades pré-cadastradas)
- Seleção de data e hora por calendário visual, sempre no formato brasileiro (dd/mm/aaaa, 24h), independente da configuração do computador do usuário
- Proteção de rotas: um usuário nunca acessa, edita ou exclui consultas de outro usuário, mesmo digitando a URL manualmente

## 🛠️ Tecnologias utilizadas

- C# / .NET SDK 10.0.400
- ASP.NET Core MVC
- Entity Framework Core (Code First) com Microsoft.EntityFrameworkCore.SqlServer
- SQL Server 2025 (local) / Azure SQL Database (produção)
- Azure App Service (hospedagem)
- ASP.NET Core Cookie Authentication + PasswordHasher para hash de senha
- Bootstrap 5.3.3
- jQuery + jQuery Validation (validação client-side)
- Flatpickr (calendário com formato de data/hora brasileiro)
- Git / GitHub / GitHub Actions (deploy contínuo)

## 🏗️ Estrutura do projeto

AgendaUVV/
│
├── Controllers/
│ ├── HomeController.cs
│ ├── AccountController.cs
│ └── ConsultasController.cs
│
├── Models/
│ ├── Usuario.cs
│ ├── Consulta.cs
│ └── ErrorViewModel.cs
│
├── ViewModels/
│ ├── RegisterViewModel.cs
│ ├── LoginViewModel.cs
│ └── ConsultaViewModel.cs
│
├── Data/
│ └── AppDbContext.cs
│
├── Views/
│ ├── Home/
│ ├── Account/
│ ├── Consultas/
│ └── Shared/
│
├── wwwroot/
│ └── css/site.css
│
├── Migrations/
│
├── Program.cs
├── appsettings.json
└── README.md


## 🗄️ Banco de dados

O projeto utiliza SQL Server, acessado via Entity Framework Core (abordagem Code First). Em produção, é utilizado o Azure SQL Database.

### Entidades

**Usuario**
- Id, Nome, Email, SenhaHash, DataCadastro

**Consulta**
- Id, Especialidade, DataHora, Descricao, UsuarioId (chave estrangeira)

### Relacionamento

Usuario (1) ── (N) Consulta


Um usuário pode ter várias consultas; cada consulta pertence a exatamente um usuário.

## ⚙️ Pré-requisitos

Para executar o projeto localmente é necessário ter instalado:

- .NET SDK 10.0.400 ou compatível ([dotnet.microsoft.com/download](https://dotnet.microsoft.com/download))
- SQL Server (instância local padrão MSSQLSERVER) — Developer ou Express
- Git
- Visual Studio Code com extensão C# Dev Kit (ou Visual Studio)
- Ferramenta dotnet-ef instalada globalmente:

dotnet tool install --global dotnet-ef


## 🚀 Como executar o projeto localmente

**1. Clonar o repositório**

git clone https://github.com/thallisamorim/AgendaUVV.git
cd AgendaUVV


**2. Restaurar as dependências**

dotnet restore


**3. Configurar a Connection String**

Abra o arquivo `appsettings.json` e confirme (ou ajuste) a string de conexão de acordo com sua instância local do SQL Server:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=AgendaUVV;Trusted_Connection=True;TrustServerCertificate=True"
}
```

**4. Executar as migrations**

Isso cria o banco `AgendaUVV` e as tabelas necessárias:

dotnet ef database update


**5. Executar a aplicação**

dotnet run


Acesse no navegador o endereço exibido no terminal (geralmente algo como `https://localhost:7xxx`).

> Alternativamente, você pode acessar diretamente a versão publicada em produção, sem precisar instalar nada: https://agendauvv-thallis-fcgqcqfwfmfzf2ct.westus3-01.azurewebsites.net

## 🔐 Segurança

- Autenticação via **Cookie Authentication** nativo do ASP.NET Core
- Senhas armazenadas com hash através de `PasswordHasher<Usuario>`, nunca em texto puro
- Todas as rotas do Dashboard e de Consultas são protegidas com o atributo `[Authorize]`
- Ordem correta do pipeline de middleware: `app.UseAuthentication()` antes de `app.UseAuthorization()`
- Toda consulta ao banco de uma Consulta específica (detalhes, edição, exclusão) filtra simultaneamente por `Id` e `UsuarioId` do usuário logado, impedindo que um usuário acesse, edite ou exclua consultas de outro usuário mesmo manipulando a URL manualmente
- Validação de dados no servidor com Data Annotations (`[Required]`, `[EmailAddress]`, `[StringLength]`, `[Compare]`) em todos os formulários
- Credenciais de banco de dados de produção configuradas diretamente no Azure App Service, nunca expostas no repositório

## 🧪 Testes realizados

- Cadastro de usuário: campos válidos, campos em branco, e-mail inválido, senha curta, senhas divergentes, e-mail duplicado
- Login: credenciais corretas, senha incorreta, e-mail inexistente, tentativa de acesso sem autenticação
- CRUD de consultas: criação, listagem, edição, exclusão, com e sem descrição
- Segurança: teste manual de acesso via URL a consultas de outro usuário (`/Consultas/Details/{id}`, `/Consultas/Edit/{id}`, `/Consultas/Delete/{id}`), retornando 404 corretamente
- Isolamento de dados: dois usuários distintos cadastrados pela própria tela de Registro, cada um visualizando apenas suas próprias consultas

## 📸 Demonstração

🎥 Vídeo demonstrativo: **[LINK DO VÍDEO AQUI]**

O vídeo mostra: cadastro de usuário, login, dashboard, criação de consulta, listagem, edição, exclusão e logout.

## 👥 Equipe

| Nome | 
|---|
| Matheus Lopes Rosa |
| Thallis Silveira de Amorim |

## 📚 Projeto acadêmico

Projeto desenvolvido para a disciplina de **Desenvolvimento Web Back-end**, do curso de Análise e Desenvolvimento de Sistemas.

- **Instituição:** Universidade Vila Velha (UVV)
- **Semestre:** 3º semestre — Análise e Desenvolvimento de Sistemas
- **Professor:** Fabricio Ribeiro Ferreira

## 📄 Licença

Projeto desenvolvido exclusivamente para fins acadêmicos.