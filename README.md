# EPIK - Prueba Técnica | Desarrollador Backend / Frontend

## Descripción

API REST desarrollada en **C# con ASP.NET Core Web API**, diseñada para administrar registros de personas mediante operaciones CRUD (Crear, Consultar, Actualizar y Eliminar).

El proyecto utiliza **PostgreSQL** como sistema de gestión de bases de datos y **Entity Framework Core** como ORM para gestionar el acceso y las operaciones sobre los datos.

La solución implementa cinco funcionalidades principales: registro de personas, consulta por identificación, listado de mujeres mediante una vista SQL, actualización exclusiva de la edad y eliminación de registros.

## Tecnologías utilizadas

| Tecnología | Descripción |
|---|---|
| C# | Lenguaje de programación |
| .NET 10 | Plataforma de desarrollo |
| ASP.NET Core Web API | Desarrollo de servicios REST |
| PostgreSQL 18 | Sistema de gestión de bases de datos |
| Entity Framework Core 10 | ORM para acceso a datos |
| Npgsql | Proveedor de PostgreSQL para Entity Framework |
| REST API | Arquitectura de comunicación HTTP |

## Requisitos previos

Para ejecutar el proyecto se necesita tener instalado:

- [.NET SDK 10.0](https://dotnet.microsoft.com/download/dotnet/10.0).
- [PostgreSQL 18](https://www.postgresql.org/download/).
- [pgAdmin 4](https://www.pgadmin.org/download/) (recomendado para administrar la base de datos).
- Git (opcional).

## Estructura del proyecto

```text
Epik.Api/
│
├── Controllers/
│   └── PersonaController.cs
│
├── Data/
│   └── ApplicationDbContext.cs
│
├── Dtos/
│   └── ActualizarEdadRequest.cs
│
├── Models/
│   ├── Persona.cs
│   └── MujerView.cs
│
├── Services/
│   ├── IPersonaService.cs
│   └── PersonaService.cs
│
├── Migrations/
│
├── Scripts/
│
├── Properties/
│
├── Program.cs
├── appsettings.json
├── Epik.Api.csproj
└── README.md
```

## Configuración de la base de datos

### 1. Crear la base de datos

Desde pgAdmin 4 o mediante una consulta SQL, crear una base de datos con el siguiente nombre:

```sql
CREATE DATABASE epik_db;
```

### 2. Configurar la cadena de conexión

El proyecto utiliza **User Secrets** para almacenar las credenciales de conexión fuera del código fuente.

Abrir una terminal en la carpeta raíz del proyecto y ejecutar:

```bash
dotnet user-secrets init
```

Configurar la cadena de conexión:

```bash
dotnet user-secrets set "ConnectionStrings:Postgres" "Host=localhost;Port=5432;Database=epik_db;Username=postgres;Password=CONTRASEÑA"
```

**Importante:**

- Reemplazar `postgres` por el nombre del usuario configurado en PostgreSQL, si es diferente.
- Reemplazar `CONTRASEÑA` por la contraseña correspondiente a ese usuario.

## Instalación y ejecución

### 1. Obtener las dependencias

Desde la terminal, ubicarse en la carpeta `Epik.Api` y ejecutar:

```bash
dotnet restore
```

### 2. Instalar Entity Framework Core CLI

Si la herramienta todavía no está instalada, ejecutar:

```bash
dotnet tool install --global dotnet-ef --version 10.0.14
```

### 3. Crear la estructura de la base de datos

Ejecutar las migraciones para generar automáticamente la tabla `Persona` y la vista `vw_mujeres`:

```bash
dotnet ef database update
```

### 4. Ejecutar la aplicación

Iniciar el servidor mediante:

```bash
dotnet run
```

La aplicación se ejecutará en la dirección local indicada en la terminal.

## Endpoints disponibles

La API dispone de los siguientes endpoints:

| Método HTTP | Endpoint | Descripción |
|---|---|---|
| POST | `/api/personas` | Registrar una nueva persona. |
| GET | `/api/personas/{identificacion}` | Consultar una persona por su identificación. |
| GET | `/api/personas/mujeres` | Listar todas las mujeres mediante la vista SQL. |
| PUT | `/api/personas/{identificacion}/edad` | Actualizar exclusivamente la edad de una persona. |
| DELETE | `/api/personas/{identificacion}` | Eliminar una persona por su identificación. |

## Ejemplos de solicitudes

### 1. Registrar una persona

**Método:** POST

**Endpoint:** `/api/personas`

**Body (JSON):**

```json
{
  "identificacion": "100001",
  "nombres": "Maria Fernanda",
  "apellidos": "Gonzalez Perez",
  "edad": 25,
  "genero": "Femenino"
}
```

### 2. Consultar una persona

**Método:** GET

**Endpoint:** `/api/personas/100001`

Permite obtener la información de una persona utilizando su número de identificación.

### 3. Listar mujeres

**Método:** GET

**Endpoint:** `/api/personas/mujeres`

Esta operación obtiene los registros exclusivamente desde la vista `vw_mujeres` de PostgreSQL.

### 4. Actualizar la edad

**Método:** PUT

**Endpoint:** `/api/personas/100001/edad`

**Body (JSON):**

```json
{
  "edad": 26
}
```

La operación modifica únicamente el campo `Edad`, manteniendo intactos los demás datos de la persona.

### 5. Eliminar una persona

**Método:** DELETE

**Endpoint:** `/api/personas/100001`

Permite eliminar un registro utilizando su número de identificación.

## Estructura de la base de datos

La base de datos `epik_db` contiene los siguientes objetos:

### Tabla: Persona

Almacena la información de las personas registradas.

| Campo | Tipo de dato | Restricción |
|---|---|---|
| Identificacion | VARCHAR(20) | Primary Key |
| Nombres | VARCHAR(100) | NOT NULL |
| Apellidos | VARCHAR(100) | NOT NULL |
| Edad | INTEGER | NOT NULL |
| Genero | VARCHAR(10) | NOT NULL |

### Vista: vw_mujeres

Vista SQL encargada de consultar exclusivamente los registros cuyo campo `Genero` tiene el valor `Femenino`.

La vista permite separar la consulta de mujeres de las operaciones realizadas directamente sobre la tabla `Persona`.

El endpoint `GET /api/personas/mujeres` utiliza esta vista mediante Entity Framework Core.

## Pruebas realizadas

Las funcionalidades de la API fueron verificadas utilizando **Thunder Client**.

Se realizaron pruebas para comprobar:

- Registro de personas.
- Consulta por identificación.
- Listado de mujeres mediante la vista SQL.
- Actualización exclusiva del campo edad.
- Eliminación de registros.
- Validación de datos.
- Manejo de identificaciones duplicadas.
- Consulta de registros inexistentes.
- Validación de edades y géneros permitidos.

## Autor

**Juan Diego Flórez Lora**

Prueba técnica de desarrollo Backend / Frontend - EPIK.