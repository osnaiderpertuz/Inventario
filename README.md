# Inventario

REST API para inventario de productos

## Características

- API RESTful
- Compatible con .NET 8 y C#.

## Tecnologías Utilizadas

- **.NET 8**
- **C#**
- Base de datos: PostgreSQL
- Frameworks/Librerías adicionales:
  - Entity Framework Core
  - ASP.NET Core (Web API)
  - AutoMapper
  - Swashbuckle (Swagger)
  - FluentValidator

## Requisitos Previos

Antes de ejecutar el proyecto, asegúrate de tener instalados los siguientes componentes:

- [.NET SDK 8](https://dotnet.microsoft.com/download/dotnet/8.0)
- Un editor de código o IDE (Visual Studio, Visual Studio Code o Rider)
- Base de datos: (Indica el sistema de base de datos necesario, por ejemplo, SQL Server)

## Instalación

Sigue estos pasos para instalar y ejecutar el proyecto localmente:

1. **Clona el repositorio**:
   ```bash
   git clone https://github.com/osnaiderpertuz/Inventario.git
   cd Inventario
2. **Migrar BD**:
   Ubícate en el directorio: ..Inventario\Inventario.Solution\Inventario.WebApi
    ```bash
    dotnet ef migrations add InitialMigration --project ../Inventario.Infrastructure/Inventario.Infrastructure.csproj --startup-project . --context AppDbContext
    dotnet ef database update --project ../Inventario.Infrastructure/Inventario.Infrastructure.csproj --startup-project .
