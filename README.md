# Sistema de Gestión de Reservas de Vehículos

Aplicación full-stack para la administración de reservas de vehículos, desarrollada en C# con ASP.NET Core. Incluye un sitio web (MVC) para la operación diaria y una API REST documentada para integraciones externas.

## Arquitectura

El proyecto está organizado en 3 capas, cada una en su propio proyecto de la solución:

- **`ObligatorioGustavoNunez.Dominio`** — Entidades del negocio (`Usuario`, `Vehiculo`, `Reserva`), interfaces de repositorios, DTOs y la capa de `Services` con las reglas de negocio.
- **`ObligatorioGustavoNunez.Persistencia`** — Acceso a datos con Entity Framework Core: implementación de los repositorios, `DbContext` y migraciones sobre SQL Server.
- **`ObligatorioGustavoNunez.SitioWeb`** — Capa de presentación: controladores y vistas MVC (Razor) para el sitio, y controladores de API REST documentados con Swagger.

## Tecnologías

- C# / .NET (ASP.NET Core MVC + Web API)
- Entity Framework Core (Code First + Migrations)
- SQL Server
- Autenticación por Cookies (sitio web) y JWT Bearer (API)
- Swagger / OpenAPI
- Bootstrap (frontend)

## Funcionalidades principales

- **Gestión de vehículos**: alta, baja, modificación y consulta, con baja lógica (se marca como *Inactivo* en lugar de eliminarse si tiene reservas asociadas).
- **Gestión de reservas**: creación con validación de disponibilidad (evita solapamiento de fechas para un mismo vehículo) y de capacidad de pasajeros.
- **Máquina de estados de reservas**: `Pendiente → Confirmada → En curso → Completada`, con `Cancelada` como salida válida desde los primeros estados. Las transiciones inválidas lanzan una excepción de dominio.
- **Autenticación y roles**: login con Cookie Authentication para el sitio y JWT para la API. Acceso restringido por rol (`Administrador`, `Cliente`, `Operador`) mediante `[Authorize(Roles = "...")]`.
- **Reportes**: reservas por rango de fechas (con total recaudado) y resumen estadístico (reservas activas, canceladas, monto promedio).

## Cómo ejecutarlo localmente

1. Cloná el repositorio y abrí `ObligatorioGustavoNunez.sln` en Visual Studio (o usá `dotnet` desde la terminal).
2. Configurá tu propia cadena de conexión a SQL Server en `appsettings.json` (dentro de `ObligatorioGustavoNunez.SitioWeb`), reemplazando el valor de `ConnectionStrings:miConexion` por el de tu instancia local.
3. Aplicá las migraciones para crear la base de datos:
   ```
   dotnet ef database update --project ObligatorioGustavoNunez.Persistencia --startup-project ObligatorioGustavoNunez.SitioWeb
   ```
4. Ejecutá el proyecto `ObligatorioGustavoNunez.SitioWeb`. Por defecto corre en `https://localhost:7117`.
5. En entorno de desarrollo, la documentación de la API está disponible en `/swagger`.

## Estado del proyecto

Proyecto desarrollado como trabajo obligatorio de la carrera Analista Programador (Instituto CTC Salto). Próximas mejoras posibles: mover los secretos de configuración (JWT Key, cadena de conexión) a variables de entorno o `user-secrets`, y agregar pruebas automatizadas sobre la capa de `Services`.
