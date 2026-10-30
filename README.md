# EPIK - Prueba Técnica | Desarrollador Backend / Frontend

<p align="center">
  <img src="https://img.shields.io/badge/C%23-.NET%2010-512BD4?logo=dotnet&logoColor=white" alt="C# .NET 10"/>
  <img src="https://img.shields.io/badge/ASP.NET%20Core-Web%20API-512BD4?logo=dotnet&logoColor=white" alt="ASP.NET Core"/>
  <img src="https://img.shields.io/badge/PostgreSQL-18-4169E1?logo=postgresql&logoColor=white" alt="PostgreSQL 18"/>
  <img src="https://img.shields.io/badge/Entity%20Framework-Core-512BD4" alt="Entity Framework Core"/>
  <img src="https://img.shields.io/badge/Status-Completed-success" alt="Completed"/>
</p>

<p align="center">
  <b>API REST para la gestión de personas mediante operaciones CRUD.</b>
</p>

<p align="center">
  <a href="https://github.com/J10GOF/Epik.Api">Repositorio en GitHub</a>
</p>

---

## Descripción

API REST desarrollada en **C# con ASP.NET Core Web API**, como solución a la prueba técnica de desarrollo de EPIK.

El proyecto permite administrar registros de personas mediante operaciones CRUD (Crear, Consultar, Actualizar y Eliminar).

Utiliza **PostgreSQL** como sistema de gestión de bases de datos y **Entity Framework Core** como ORM para gestionar el acceso y las operaciones sobre los datos.

La solución implementa cinco funcionalidades principales:

- Registro de personas.
- Consulta de personas mediante identificación.
- Listado de mujeres utilizando una vista SQL.
- Actualización exclusiva de la edad.
- Eliminación de registros mediante identificación.

## Tecnologías utilizadas

| Tecnología | Uso |
|---|---|
| C# | Lenguaje de programación |
| .NET 10 | Plataforma de desarrollo |
| ASP.NET Core Web API | Desarrollo de servicios REST |
| PostgreSQL 18 | Sistema de gestión de bases de datos |
| Entity Framework Core 10 | ORM para acceso a datos |
| Npgsql | Proveedor de PostgreSQL para Entity Framework |
| Thunder Client | Pruebas de endpoints HTTP |
| Git / GitHub | Control de versiones y alojamiento del código |

## Características principales

- Arquitectura organizada por capas.
- Separación entre controladores, servicios y acceso a datos.
- Inyección de dependencias.
- Operaciones asíncronas mediante `async/await`.
- Consultas de solo lectura mediante `AsNoTracking()`.
- Migraciones para administrar la estructura de la base de datos.
- Gestión segura de credenciales mediante User Secrets.
- Validación de datos de entrada.
- Consulta de mujeres mediante una vista SQL de PostgreSQL.

## Arquitectura del proyecto

El proyecto utiliza una arquitectura por capas para separar responsabilidades y facilitar el mantenimiento del código.

```text
Cliente HTTP
     |
     v
Controllers
     |
     v
Services
     |
     v
Data / Entity Framework Core
     |
     v
PostgreSQL
     |
     ├── Tabla Persona
     |
     └── Vista vw_mujeres
```

### Estructura de carpetas

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
├── Properties/
│
├── Program.cs
├── appsettings.json
├── appsettings.Development.json
├── Epik.Api.csproj
├── Epik.Api.http
├── .gitignore
└── README.md
```

## Requisitos previos

Para instalar y ejecutar el proyecto se necesita:

- [.NET SDK 10.0](https://dotnet.microsoft.com/download/dotnet/10.0).
- [PostgreSQL 18](https://www.postgresql.org/download/).
- [pgAdmin 4](https://www.pgadmin.org/download/) (recomendado).
- Git (opcional).

## Instalación y configuración

### 1. Clonar el repositorio

Abrir una terminal y ejecutar:

```bash
git clone https://github.com/J10GOF/Epik.Api.git
```

Ingresar a la carpeta del proyecto:

```bash
cd Epik.Api
```

### 2. Restaurar dependencias

Ejecutar:

```bash
dotnet restore
```

### 3. Crear la base de datos

Desde pgAdmin 4 o mediante una consulta SQL, crear una base de datos llamada `epik_db`.

```sql
CREATE DATABASE epik_db;
```

### 4. Configurar la cadena de conexión

El proyecto utiliza **User Secrets** para almacenar las credenciales de conexión fuera del código fuente.

Inicializar User Secrets:

```bash
dotnet user-secrets init
```

Configurar la conexión a PostgreSQL:

```bash
dotnet user-secrets set "ConnectionStrings:Postgres" "Host=localhost;Port=5432;Database=epik_db;Username=postgres;Password=TU_CONTRASEÑA"
```

**Importante:**

- Reemplazar `postgres` por el usuario configurado en PostgreSQL, si es diferente.
- Reemplazar `TU_CONTRASEÑA` por la contraseña correspondiente.
- No publicar las credenciales de conexión en GitHub.

### 5. Instalar Entity Framework Core CLI

Si la herramienta todavía no está instalada:

```bash
dotnet tool install --global dotnet-ef --version 10.0.14
```

### 6. Crear la estructura de la base de datos

Ejecutar las migraciones:

```bash
dotnet ef database update
```

Este comando crea automáticamente los objetos definidos en las migraciones del proyecto:

- Tabla `Persona`.
- Vista `vw_mujeres`.

### 7. Ejecutar la aplicación

Iniciar el servidor:

```bash
dotnet run
```

La API estará disponible en la dirección local indicada por la terminal.

## Endpoints disponibles

La API dispone de cinco endpoints para administrar los registros de personas.

| Método HTTP | Endpoint | Descripción |
|---|---|---|
| POST | `/api/personas` | Registrar una nueva persona. |
| GET | `/api/personas/{identificacion}` | Consultar una persona por identificación. |
| GET | `/api/personas/mujeres` | Listar mujeres mediante la vista SQL. |
| PUT | `/api/personas/{identificacion}/edad` | Actualizar exclusivamente la edad. |
| DELETE | `/api/personas/{identificacion}` | Eliminar una persona por identificación. |

## Ejemplos de solicitudes

### 1. Registrar una persona

**Método:** POST

**Endpoint:**

```http
POST /api/personas
```

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

**Respuesta esperada:** `201 Created`.

### 2. Consultar una persona

**Método:** GET

**Endpoint:**

```http
GET /api/personas/100001
```

Permite consultar la información de una persona mediante su identificación.

**Respuesta esperada:** `200 OK`.

### 3. Listar mujeres

**Método:** GET

**Endpoint:**

```http
GET /api/personas/mujeres
```

Obtiene los registros cuyo género es Femenino mediante la vista `vw_mujeres` de PostgreSQL.

**Respuesta esperada:** `200 OK`.

### 4. Actualizar la edad

**Método:** PUT

**Endpoint:**

```http
PUT /api/personas/100001/edad
```

**Body (JSON):**

```json
{
  "edad": 26
}
```

La operación modifica exclusivamente el campo `Edad`, manteniendo intactos los demás datos de la persona.

**Respuesta esperada:** `204 No Content`.

### 5. Eliminar una persona

**Método:** DELETE

**Endpoint:**

```http
DELETE /api/personas/100001
```

Permite eliminar un registro utilizando su número de identificación.

**Respuesta esperada:** `204 No Content`.

## Estructura de la base de datos

La base de datos `epik_db` contiene los siguientes objetos:

### Tabla: Persona

Almacena la información de las personas registradas.

| Campo | Tipo de dato | Restricción |
|---|---|---|
| Identificacion | VARCHAR(20) | PRIMARY KEY |
| Nombres | VARCHAR(100) | NOT NULL |
| Apellidos | VARCHAR(100) | NOT NULL |
| Edad | INTEGER | NOT NULL |
| Genero | VARCHAR(10) | NOT NULL |

### Vista: vw_mujeres

Vista SQL encargada de consultar exclusivamente los registros cuyo campo `Genero` tiene el valor `Femenino`.

Definición de la vista:

```sql
CREATE OR REPLACE VIEW public.vw_mujeres AS
SELECT
    "Identificacion",
    "Nombres",
    "Apellidos",
    "Edad",
    "Genero"
FROM public."Persona"
WHERE "Genero" = 'Femenino';
```

El endpoint `GET /api/personas/mujeres` utiliza esta vista mediante Entity Framework Core, evitando realizar la consulta directamente sobre la tabla desde el servicio.

## Validaciones y respuestas HTTP

La API contempla validaciones y respuestas para diferentes escenarios:

| Código HTTP | Descripción |
|---|---|
| 200 | Consulta realizada correctamente. |
| 201 | Registro creado correctamente. |
| 204 | Actualización o eliminación exitosa. |
| 400 | Solicitud con datos inválidos. |
| 404 | Persona no encontrada. |
| 409 | Identificación duplicada. |

Entre las validaciones implementadas se encuentran:

- Campos obligatorios.
- Longitud máxima de la identificación, nombres y apellidos.
- Edad dentro del rango permitido de 0 a 120.
- Género limitado a Masculino o Femenino.
- Control de identificaciones duplicadas.
- Verificación de existencia antes de actualizar o eliminar.

## Pruebas realizadas

Las funcionalidades de la API fueron verificadas utilizando **Thunder Client** desde Visual Studio Code.

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

## Repositorio

El código fuente completo se encuentra disponible en GitHub:

**[https://github.com/J10GOF/Epik.Api](https://github.com/J10GOF/Epik.Api)**

## Autor

**Juan Diego Flórez Lora**

Prueba técnica de desarrollo Backend / Frontend - EPIK.

---

<p align="center">
  Desarrollado con C#, ASP.NET Core y PostgreSQL.
</p>