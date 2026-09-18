# BookStore - Data Saturday
## Migração: ASP.NET MVC para Minimal API + Blazor (.NET 10)

Bem-vindo ao repositório do projeto `BookStore`. Este documento detalha a arquitetura, motivação e instruções para a nova versão da aplicação, que está sendo migrada de uma arquitetura legada em ASP.NET MVC para um modelo moderno utilizando **Minimal APIs** e **Blazor** no **.NET 10**.

---

## 🎯 Motivação da Migração

A transição do ASP.NET MVC tradicional para Minimal APIs e Blazor visa alcançar:
- **Separação de Responsabilidades:** Desacoplamento claro entre o backend (fornecimento de dados) e o frontend (interface do usuário).
- **Performance:** Aproveitamento da leveza e alta performance das Minimal APIs no .NET 10.
- **Interatividade Moderna:** Substituição das *Views* estáticas do MVC por componentes ricos e interativos (SPA) com Blazor, mantendo o ecossistema C# de ponta a ponta.
- **Manutenibilidade:** Código mais enxuto e organizado, facilitando a escalabilidade futura.

---

## 🏗️ Arquitetura do Projeto

O projeto foi reestruturado em **3 camadas principais** para manter a simplicidade e a separação de conceitos nesta fase inicial:

### 1. `Core` (Lógica de Negócios e Domínio)
Camada base e independente (Class Library). Contém o coração da aplicação.
- **Entities:** Modelos de domínio genéricos.
- **Interfaces:** Contratos para repositórios e serviços.
- **Services:** Lógica central da aplicação.
- *Não possui dependências das camadas `Api` ou `Web`.*

### 2. `Api` (Backend - Minimal APIs)
Responsável por expor os dados e a lógica de negócios para o mundo externo.
- **Endpoints:** Construídos utilizando Minimal APIs do .NET 10.
- **Injeção de Dependência:** Configuração de serviços e acesso a banco de dados.
- **Segurança:** Autenticação e Autorização.
- *Depende de: `Core`*

### 3. `Web` (Frontend - Blazor WebApp)
Interface de usuário moderna e interativa construída com Blazor WebAssembly.
- **Componentes:** Páginas e componentes de UI reaproveitáveis.
- **Serviços HTTP:** Clientes HTTP configurados para consumir a camada `Api`.
- **Gerenciamento de Estado:** Controle do estado da aplicação no lado do cliente.
- *Depende de: `Core` (para compartilhamento de DTOs) e consome a `Api`.*

---

## 🛠️ Tecnologias Utilizadas

- **Framework:** [.NET 10](https://dotnet.microsoft.com/)
- **Backend:** ASP.NET Core Minimal APIs
- **Frontend:** Blazor (Web App)
- **Linguagem:** C#

---

## 🚀 Como Executar o Projeto Localmente

### Pré-requisitos
- [SDK do .NET 10](https://dotnet.microsoft.com/download/dotnet/10.0) instalado.
- IDE de sua preferência (Visual Studio 2022, JetBrains Rider ou VS Code).

### Passos

1. **Clone o repositório:**
   ```bash
   git clone [https://github.com/mendel-dev/bookstore-data-saturday](https://github.com/mendel-dev/bookstore-data-saturday)
   cd bookstore-data-saturday
