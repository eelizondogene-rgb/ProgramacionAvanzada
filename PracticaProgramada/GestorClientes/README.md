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
Solución por capas (.NET 10, ASP.NET Core MVC):

| Proyecto | Tipo | Responsabilidad |
|---|---|---|
| `GestorClientes.Domain` | Class Library | Entidades `Cliente` y `Telefono` y sus reglas de negocio |
| `GestorClientes.Application` | Class Library | Servicios, DTOs, `Respuesta<T>` y mapeo con AutoMapper |
| `GestorClientes.Infrastructure` | Class Library | `DbContext` (SQLite) y repositorios |
| `GestorClientes` | ASP.NET Core MVC | Controladores, vistas y configuración (`Program.cs`) |

Flujo: Vista -> Controlador -> Servicio -> Repositorio -> Base de datos.

### b. Librerías / paquetes NuGet
- AutoMapper 16.2.0 (Application)
- Microsoft.EntityFrameworkCore 10.0.12 (Infrastructure)
- Microsoft.EntityFrameworkCore.Sqlite 10.0.12 (Infrastructure)
- Microsoft.EntityFrameworkCore.Design y Tools 10.0.12
- Front end: Bootstrap, jQuery, DataTables, Bootstrap Icons

### c. Principios SOLID y patrones de diseño
_Persona 2: completar. Ideas: inversión de dependencias (interfaces `IClienteRepository` e `IClienteServicio`), responsabilidad única (métodos pequeños), segregación de interfaces. Patrones: Repository, Inyección de dependencias, DTO, Result (`Respuesta<T>`)._

---

## Cómo ejecutar
1. Abrir `GestorClientes.slnx` en Visual Studio y ejecutar el proyecto `GestorClientes`.
2. La base de datos `clientes.db` se crea sola la primera vez.
3. Si se cambian las entidades, borrar `clientes.db` para que se vuelva a crear.

## Cómo trabajamos en Git
- Una rama por persona: `persona1-datos`, `persona2-logica`, `persona3-web`, `persona4-pantallas`.
- No subir directo a `main`: se junta por pull request.
- Antes de empezar: `git pull origin main`.
