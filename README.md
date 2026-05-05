# GestaoOsAPI

API REST para gerenciamento de Ordens de Serviço (OS), desenvolvida em C# com ASP.NET Core e Entity Framework.

---

## 📋 Descrição

Sistema de gerenciamento de Ordens de Serviço (OS) com fluxo de criação, assinatura e validação. O sistema conta com 4 perfis de usuário:

- **Emitente** — cria OS e acompanha o status
- **Gerente** — assina as OS do seu setor (Qualidade, Engenharia ou Produção)
- **Administrador** — valida (conclui) ou cancela as OSs e gerencia usuários
- **Executante** — visualiza todas as OSs

**Fluxo da OS:**
```text
AguardandoAssinaturas → AguardandoValidacao → Concluida
                                            → Cancelada
```

---

## 🚀 Tecnologias

- [.NET 8.0](https://dotnet.microsoft.com/)
- [ASP.NET Core Web API](https://learn.microsoft.com/en-us/aspnet/core/)
- [Entity Framework Core 8.0](https://learn.microsoft.com/en-us/ef/core/)
- [Npgsql (PostgreSQL)](https://www.npgsql.org/)
- [Supabase](https://supabase.com/) — banco de dados PostgreSQL na nuvem
- [Swagger / Swashbuckle](https://swagger.io/)
- [BCrypt.Net-Next](https://github.com/BcryptNet/bcrypt.net) — hash de senhas
- [Microsoft.AspNetCore.Authentication.JwtBearer](https://learn.microsoft.com/en-us/aspnet/core/security/authentication/jwt-authn) — autenticação JWT

---

## 🔐 Segurança

- **Senhas** — armazenadas com hash BCrypt (irreversível)
- **Autenticação** — JWT Bearer Token com expiração de 8 horas
- **Endpoints protegidos** — todos os endpoints exceto `/auth/login` exigem token válido

**Fluxo de autenticação:**
```text
POST /auth/login → recebe token JWT
Todas as requisições → Header: Authorization: Bearer {token}
```

---

## ⚙️ Como rodar o projeto

### Pré-requisitos
- [.NET 8.0 SDK](https://dotnet.microsoft.com/download)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) ou [VS Code](https://code.visualstudio.com/)
- Conta no [Supabase](https://supabase.com/) com um projeto criado

### Passo a passo

**1. Clone o repositório:**
```bash
git clone https://github.com/JoaoVGomees/Gerenciador-De-Ordens-De-Servico.git
cd Gerenciador-De-Ordens-De-Servico
```

**2. Crie o arquivo `appsettings.Development.json`** dentro da pasta `GestaoOscAPI/GestaoOscAPI/`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "User Id=SEU_USER;Password=SUA_SENHA;Server=SEU_SERVER;Port=5432;Database=postgres;SSL Mode=Require;Trust Server Certificate=true"
  },
  "Jwt": {
    "Key": "SUA_CHAVE_SECRETA_MINIMO_32_CARACTERES",
    "Issuer": "GestaoOscAPI",
    "Audience": "GestaoOscAPIUsers"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  }
}
```

> ⚠️ Este arquivo está no `.gitignore` e nunca deve ser commitado.

**3. Instale a ferramenta do Entity Framework:**
```bash
dotnet tool install --global dotnet-ef --version 8.0.0
```

**4. Aplique as migrations no banco:**
```bash
cd GestaoOscAPI/GestaoOscAPI
dotnet ef database update
```

**5. Rode o projeto:**
```bash
dotnet run
```

O Swagger estará disponível em: `https://localhost:{porta}/swagger`

**6. Para testar no Swagger com autenticação:**
- Faça o `POST /auth/login` e copie o token retornado
- Clique em **Authorize** 🔒 no topo do Swagger
- Digite `Bearer {seu token}` e clique em Authorize

---

## 📁 Estrutura do projeto

```text
GestaoOscAPI/
├── Controllers/
│   ├── OsController.cs         # Endpoints das OSs
│   └── UsuarioController.cs    # Endpoints dos usuários
├── Data/
│   └── AppDbContext.cs         # Configuração do banco de dados
├── Migrations/                 # Migrations do Entity Framework
├── Models/
│   ├── Entities/
│   │   ├── Os.cs               # Entidade OS
│   │   └── Usuario.cs          # Entidade Usuário
│   ├── Enums/
│   │   ├── PerfilUsuario.cs    # Emitente, Executante, Gerente, Administrador
│   │   ├── Setor.cs            # Qualidade, Engenharia, Producao, Nenhum
│   │   └── StatusOs.cs         # AguardandoAssinaturas, AguardandoValidacao, Concluida, Cancelada
│   ├── Requests/
│   │   ├── LoginRequest.cs
│   │   ├── CriarOsRequest.cs
│   │   ├── CriarUsuarioRequest.cs
│   │   ├── AtualizarUsuarioRequest.cs
│   │   └── AdminOsRequest.cs
│   └── Responses/
│       ├── LoginResponse.cs    # Token JWT + dados do usuário
│       ├── OsResponse.cs       # OS sem dados sensíveis
│       └── UsuarioResponse.cs  # Usuário sem senha
├── Repositories/
│   ├── OsRepository.cs         # Acesso ao banco — OSs
│   └── UsuarioRepository.cs    # Acesso ao banco — Usuários
├── Services/
│   ├── OsService.cs            # Regras de negócio das OSs
│   ├── TokenService.cs         # Geração do token JWT
│   └── UsuarioService.cs       # Regras de negócio dos usuários
├── appsettings.json            # Configurações gerais (sem credenciais)
└── Program.cs                  # Configuração da aplicação
```

---

## 🔗 Endpoints da API

> ⚠️ Todos os endpoints exceto `/auth/login` exigem o header `Authorization: Bearer {token}`

### Autenticação e Usuários

| Método | Rota | Descrição |
|--------|------|-----------|
| POST | `/auth/login` | Login — retorna token JWT + dados do usuário |
| GET | `/usuarios` | Listar todos os usuários |
| GET | `/usuarios/{id}` | Buscar usuário por ID |
| GET | `/usuarios/email?email=` | Buscar usuário por email |
| GET | `/usuarios/gerentes/{setor}` | Listar gerentes por setor |
| POST | `/usuarios` | Criar novo usuário (apenas Admin) |
| PUT | `/usuarios/{id}` | Atualizar usuário |
| DELETE | `/usuarios/{id}` | Deletar usuário |

### OSs

| Método | Rota | Descrição |
|--------|------|-----------|
| GET | `/os` | Listar todas as OSs |
| GET | `/os/{id}` | Buscar OS por ID |
| GET | `/os/emitente/{id}` | Listar OSs criadas pelo emitente |
| GET | `/os/gerente/{id}` | Listar OSs pendentes de assinatura do gerente |
| POST | `/os` | Criar nova OS |
| DELETE | `/os/{id}` | Deletar OS |
| POST | `/os/{id}/assinar` | Assinar OS (qualquer gerente do setor) |
| PUT | `/os/{id}/concluir` | Concluir OS (apenas Admin, todos devem ter assinado) |
| PUT | `/os/{id}/cancelar` | Cancelar OS (apenas Admin) |
