﻿# Task Manager API 
Este es el backend de la aplicación **Task Manager**, desarrollado con **ASP.NET Core 8** y **Entity Framework Core**. Gestiona las operaciones CRUD de tareas y se comunica con una base de datos SQL Server.  

🚀 Tecnologías Utilizadas  
- **Framework:** ASP.NET Core 7  
- **ORM:** Entity Framework Core  
- **Base de Datos:** SQL Server  
- **Herramientas:** Visual Studio Code, Postman  

📌 Características  
✔ API RESTful para gestión de tareas  
✔ Operaciones CRUD con Entity Framework  
✔ Migraciones con EF Core  
✔ Seguridad con validaciones  

 🔧 Instalación y Configuración  

1️⃣ Requisitos Previos**  
- .NET 8 SDK  
- SQL Server (o SQL Server Express)  

2️⃣ Instalación**  
bash
git clone https://github.com/tu-usuario/task-manager-app.git
cd task-manager-app/backend
dotnet restore

3️⃣ Configuración de la Base de Datos

Modificar el archivo appsettings.json:

"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=TaskDB;Trusted_Connection=True;"
}

4️⃣ Aplicar Migraciones y Ejecutar

dotnet ef database update
dotnet run

La API estará disponible en: http://localhost:5000

📌 Estructura del Proyecto

backend/
│── Controllers/           # Controladores API
│── Models/                # Modelos de datos
│── Data/                  # Contexto de base de datos
│── Program.cs             # Configuración de la API
│── appsettings.json       # Configuración de la aplicación
│── TaskManager.sln        # Solución .NET

📌 Endpoints de la API

Obtener tareas: GET /tasks

Obtener tarea por ID: GET /tasks/{id}

Crear tarea: POST /tasks

Actualizar tarea: PUT /tasks/{id}

Eliminar tarea: DELETE /tasks/{id}
