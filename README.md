# GestaoOscAPI

API REST para gerenciamento de Ordens de Serviço Crítico (OSC), desenvolvida em C# com ASP.NET Core e Entity Framework.

---

## 📋 Descrição

Sistema de gerenciamento de Ordens de Serviço Crítico (OSC) com fluxo de criação, assinatura e validação. O sistema conta com 4 perfis de usuário:

- **Emitente** — cria OSCs e acompanha o status
- **Gerente** — assina as OSCs do seu setor (Qualidade, Engenharia ou Produção)
- **Administrador** — valida (conclui) ou cancela as OSCs e gerencia usuários
- **Executante** — visualiza todas as OSCs

**Fluxo da OSC:**
```
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

---

## ⚙️ Como rodar o projeto

### Pré-requisitos
- [.NET 8.0 SDK](https://dotnet.microsoft.com/download)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) ou [VS Code](https://code.visualstudio.com/)
- Conta no [Supabase](https://supabase.com/) com um projeto criado

### Passo a passo

**1. Clone o repositório:**
```bash
git clone https://github.com/carlitos9087/Gerenciador-De-Ordens-De-Servico.git
cd Gerenciador-De-Ordens-De-Servico
```

**2. Crie o arquivo `appsettings.Development.json`** dentro da pasta `GestaoOscAPI/GestaoOscAPI/`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "User Id=SEU_USER;Password=SUA_SENHA;Server=SEU_SERVER;Port=5432;Database=postgres;SSL Mode=Require;Trust Server Certificate=true"
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

---

## 📁 Estrutura do projeto

```
GestaoOscAPI/
├── Controllers/
│   ├── OscController.cs        # Endpoints das OSCs
│   └── UsuarioController.cs    # Endpoints dos usuários
├── Data/
│   └── AppDbContext.cs         # Configuração do banco de dados
├── Migrations/                 # Migrations do Entity Framework
├── Models/
│   ├── Entities/
│   │   ├── Osc.cs              # Entidade OSC
│   │   └── Usuario.cs          # Entidade Usuário
│   ├── Enums/
│   │   ├── PerfilUsuario.cs    # Emitente, Executante, Gerente, Administrador
│   │   ├── Setor.cs            # Qualidade, Engenharia, Producao, Nenhum
│   │   └── StatusOsc.cs        # AguardandoAssinaturas, AguardandoValidacao, Concluida, Cancelada
│   ├── Requests/
│   │   ├── LoginRequest.cs
│   │   ├── CriarOscRequest.cs
│   │   ├── CriarUsuarioRequest.cs
│   │   ├── AtualizarUsuarioRequest.cs
│   │   └── AdminOscRequest.cs
│   └── Responses/
│       ├── OscResponse.cs      # OSC sem dados sensíveis
│       └── UsuarioResponse.cs  # Usuário sem senha
├── Repositories/
│   ├── OscRepository.cs        # Acesso ao banco — OSCs
│   └── UsuarioRepository.cs    # Acesso ao banco — Usuários
├── Services/
│   ├── OscService.cs           # Regras de negócio das OSCs
│   └── UsuarioService.cs       # Regras de negócio dos usuários
├── appsettings.json            # Configurações gerais (sem credenciais)
└── Program.cs                  # Configuração da aplicação
```

---

## 🔗 Endpoints da API

### Autenticação e Usuários

| Método | Rota | Descrição |
|--------|------|-----------|
| POST | `/auth/login` | Login do usuário |
| GET | `/usuarios` | Listar todos os usuários |
| GET | `/usuarios/{id}` | Buscar usuário por ID |
| GET | `/usuarios/email?email=` | Buscar usuário por email |
| GET | `/usuarios/gerentes/{setor}` | Listar gerentes por setor |
| POST | `/usuarios` | Criar novo usuário (apenas Admin) |
| PUT | `/usuarios/{id}` | Atualizar usuário |
| DELETE | `/usuarios/{id}` | Deletar usuário |

### OSCs

| Método | Rota | Descrição |
|--------|------|-----------|
| GET | `/osc` | Listar todas as OSCs |
| GET | `/osc/{id}` | Buscar OSC por ID |
| GET | `/osc/emitente/{id}` | Listar OSCs criadas pelo emitente |
| GET | `/osc/gerente/{id}` | Listar OSCs pendentes de assinatura do gerente |
| POST | `/osc` | Criar nova OSC |
| DELETE | `/osc/{id}` | Deletar OSC |
| POST | `/osc/{id}/assinar` | Assinar OSC (apenas Gerente) |
| PUT | `/osc/{id}/concluir` | Concluir OSC (apenas Admin) |
| PUT | `/osc/{id}/cancelar` | Cancelar OSC (apenas Admin) |
