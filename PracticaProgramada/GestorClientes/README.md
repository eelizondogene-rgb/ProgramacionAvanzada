# Gestor de Clientes

**Curso:** Programación Avanzada (SC-701)  
**Práctica:** Práctica Programada 1  
**Profesor:** Richard Arce Vargas

## 1. Integrantes finales del grupo

Integrantes a quienes se les asignará la nota del proyecto.

| Integrante             | Rol												      |
|------------------------|--------------------------------------------------------|
| Valeria Brenes Quesada    | Persona 1: Base de datos (Infrastructure)		      |
| Andreina Bolaños Valverde | Persona 2: Lógica de negocio (Domain y Application) |
| Gustavo Montoya Bolaños   | Persona 3: Controlador y armado (Web)			      |
| Genesis Gonzales Elizondo | Persona 4: Pantallas (Views y wwwroot)              |

## 2. Enlace del repositorio

**GitHub:** https://github.com/eelizondogene-rgb/ProgramacionAvanzada

## 3. Especificación básica del proyecto

### a. Arquitectura del proyecto

El sistema está desarrollado utilizando **.NET 10, ASP.NET Core MVC y una arquitectura por capas**, organizada en cuatro proyectos:

| Proyecto | Tipo | Responsabilidad |
|---|---|---|
| `GestorClientes.Domain` | Biblioteca de clases | Contiene las entidades `Cliente` y `Telefono` y las reglas del dominio. |
| `GestorClientes.Application` | Biblioteca de clases | Contiene los servicios, interfaces, DTOs, respuestas y mapeos de objetos. |
| `GestorClientes.Infrastructure` | Biblioteca de clases | Implementa el acceso a SQL Server mediante Entity Framework Core, repositorios y `ApplicationDbContext`. |
| `GestorClientes` | Aplicación web ASP.NET Core MVC | Contiene los controladores, vistas, configuración de dependencias y documentación de endpoints con Swagger. |

**Flujo de la arquitectura:**

Vista → Controlador → Servicio → Repositorio → Base de datos.

El sistema permite registrar, consultar, actualizar y eliminar clientes, incluyendo sus números telefónicos.

La relación entre clientes y teléfonos utiliza eliminación en cascada para eliminar los teléfonos asociados cuando se elimina un cliente.

### b. Libraries o paquetes NuGet utilizados

| Librería o paquete | Versión | Utilización |
|---|---|---|
| AutoMapper | 16.2.0 | Mapeo entre entidades y DTOs. |
| Microsoft.EntityFrameworkCore | 10.0.12 | Acceso y administración de datos mediante ORM. |
| Microsoft.EntityFrameworkCore.SqlServer | 10.0.12 | Conexión de Entity Framework Core con SQL Server. |
| Microsoft.EntityFrameworkCore.Sqlite | 10.0.12 | Proveedor incluido en la configuración original del proyecto. |
| Microsoft.EntityFrameworkCore.Design | 10.0.12 | Herramientas de diseño de Entity Framework Core. |
| Microsoft.EntityFrameworkCore.Tools | 10.0.12 | Herramientas de desarrollo y migraciones. |
| Swashbuckle.AspNetCore | 10.3.0 | Documentación y pruebas de los endpoints mediante Swagger. |

**Librerías de interfaz:** Bootstrap, jQuery, DataTables y Bootstrap Icons, contempladas para la presentación y administración visual de los clientes.

### c. Principios SOLID y patrones de diseño utilizados

**Principios SOLID**

- **Principio de responsabilidad única (SRP):** cada capa tiene una responsabilidad específica. Los controladores reciben solicitudes HTTP, los servicios implementan la lógica de negocio y los repositorios realizan las operaciones de acceso a datos.
- **Principio de inversión de dependencias (DIP):** los servicios trabajan mediante interfaces como `IClienteRepository`, evitando depender directamente de implementaciones concretas como `ClienteRepository`. El controlador también utiliza la interfaz `IClienteServicio`.

**Patrones de diseño**

- **Repository:** mediante `IClienteRepository` y `ClienteRepository`, se separa el acceso a datos de la lógica de negocio.
- **Inyección de dependencias:** las interfaces y sus implementaciones se registran en `Program.cs`, facilitando la comunicación entre las capas.
- **DTO (Data Transfer Object):** se utilizan `ClienteDto` y `TelefonoDto` para transferir información entre los componentes del sistema.
- **Result:** mediante `Respuesta<T>`, se devuelve información sobre el resultado de las operaciones, incluyendo el estado, mensaje y datos.
- **AutoMapper:** permite realizar el mapeo entre las entidades del dominio y los DTOs.

