# Gestor de Clientes

**Curso:** Programación Avanzada (SC-701) - Práctica programada 1
**Profesor:** Richard Arce Vargas

## 1. Integrantes finales del grupo
(A ellos se les asigna la nota del proyecto)

| Integrante | Rol |
|---|---|
| _Nombre 1_ | Persona 1: Base de datos (Infrastructure) |
| _Nombre 2_ | Persona 2: Lógica de negocio (Domain y Application) |
| _Nombre 3_ | Persona 3: Controlador y armado (Web) |
| _Nombre 4_ | Persona 4: Pantallas (Views y wwwroot) |

## 2. Enlace del repositorio
_Pegar aquí el enlace de GitHub._



## 3. Especificación básica del proyecto

### a. Arquitectura

El proyecto utiliza una arquitectura por capas desarrollada con .NET 10 y ASP.NET Core MVC.

| Proyecto | Tipo | Responsabilidad |
|---|---|---|
| `GestorClientes.Domain` | Class Library | Entidades `Cliente` y `Telefono` y sus reglas de negocio |
| `GestorClientes.Application` | Class Library | Servicios, DTOs, `Respuesta<T>` y mapeo con AutoMapper |
| `GestorClientes.Infrastructure` | Class Library | Acceso a datos mediante Entity Framework Core, `ApplicationDbContext`, repositorios y relaciones entre clientes y teléfonos |
| `GestorClientes` | ASP.NET Core MVC | Controladores, vistas y configuración de la aplicación (`Program.cs`) |

**Flujo de la arquitectura:**

Vista → Controlador → Servicio → Repositorio → Base de datos.

En Infrastructure se implementan los cinco métodos del repositorio: consultar todos los clientes, consultar uno con sus teléfonos, crear, modificar y eliminar.

La relación entre clientes y teléfonos utiliza eliminación en cascada (`DeleteBehavior.Cascade`), permitiendo eliminar los teléfonos asociados cuando se borra un cliente.

### b. Librerías / paquetes NuGet

- **AutoMapper 16.2.0:** mapeo de objetos en Application.
- **Microsoft.EntityFrameworkCore 10.0.12:** acceso y administración de datos.
- **Microsoft.EntityFrameworkCore.SqlServer 10.0.12:** proveedor de Entity Framework Core para SQL Server.
- **Microsoft.EntityFrameworkCore.Sqlite 10.0.12:** proveedor para SQLite utilizado en la configuración original de la aplicación.
- **Microsoft.EntityFrameworkCore.Design 10.0.12:** herramientas de diseño y migraciones.
- **Microsoft.EntityFrameworkCore.Tools 10.0.12:** herramientas para administrar Entity Framework Core.
- **Front end:** Bootstrap, jQuery, DataTables y Bootstrap Icons.

### c. Principios SOLID y patrones de diseño
_Persona 2: completar. Ideas: inversión de dependencias (interfaces `IClienteRepository` e `IClienteServicio`), responsabilidad única (métodos pequeños), segregación de interfaces. Patrones: Repository, Inyección de dependencias, DTO, Result (`Respuesta<T>`)._

---
## Persona 2 - Lógica del negocio 
### Principios SOLID 
- Principio de dependencias: el servicio depende de la interfaz del IClienteRepository y no de Cliente repository.
- Principio de responsabilidad: el repositorio se encarga de acceder a distintos datos y el servicio funciona con las reglas de negocio.
### Patrones de diseño 
- Repositorio: Posee IClienteRepository y ClienteRepository .
- DTO: se utilizan ClienteDto y TelefonoDto para pasar los datos. 
- Resultado: se utiliza Respuesta para ver el resultado de las operaciones.
- AutoMapper: funciona por medio de las entidades y el DTO.


## Cómo ejecutar
1. Abrir `GestorClientes.slnx` en Visual Studio y ejecutar el proyecto `GestorClientes`.
2. La base de datos `clientes.db` se crea sola la primera vez.
3. Si se cambian las entidades, borrar `clientes.db` para que se vuelva a crear.

## Cómo trabajamos en Git
- Una rama por persona: `persona1-datos`, `persona2-logica`, `persona3-web`, `persona4-pantallas`.
- No subir directo a `main`: se junta por pull request.
- Antes de empezar: `git pull origin main`.
