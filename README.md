# Sistema de Gestión Veterinaria 🏥

Sistema completo para la gestión de una clínica veterinaria, desarrollado con .NET 8 y React.

## Características Principales 🌟

### Gestión de Mascotas
- Registro completo de mascotas
- Historial médico
- Seguimiento de tratamientos

### Gestión de Citas
- Sistema de citas médicas
- Estados de cita (Pendiente, Confirmada, Cancelada, Completada)

### Gestión de Medicamentos
- Control de inventario
- Registro de medicamentos

### Gestión de Tratamientos
- Registro de tratamientos
- Asignación de medicamentos
- Seguimiento de dosis
- Historial de tratamientos

## Tecnologías Utilizadas 🛠️

### Backend
- .NET 8
- Entity Framework Core
- Postgre SQL
- AutoMapper
- MediatR (CQRS)

### Frontend
- React 18
- TypeScript
- Material-UI
- React Query
- React Router
- Axios

## Estructura del Proyecto 📁

```
Vet/
├── VetAPI/                 # Backend .NET
│   ├── Controllers/       # Controladores API
│   ├── Data/             # Contexto y Configuración de BD
│   ├── DTOs/             # Objetos de Transferencia de Datos
│   ├── Entities/         # Entidades del Dominio
│   ├── Interfaces/       # Interfaces y Repositorios
│   └── Services/         # Servicios de Negocio
│
└── VetUI/                # Frontend React
    ├── src/
    │   ├── components/   # Componentes Reutilizables
    │   ├── features/     # Módulos de Funcionalidad
    │   ├── lib/         # Utilidades y Hooks
    │   └── pages/       # Páginas de la Aplicación
```

## Requisitos del Sistema 📋

- .NET 9 SDK
- Node.js 18+
- Postgre SQL
- Visual Studio 2022 o VS Code
- Git

## Instalación 🚀

1. Clonar el repositorio:
```bash
git clone https://github.com/tu-usuario/vet.git
cd vet
```

2. Configurar el Backend:
```bash
cd API
dotnet restore
dotnet run
```

3. Configurar el Frontend:
```bash
cd VetUI
npm install
npm run dev
```

4. Configurar la base de datos:
- Ejecutar las migraciones de Entity Framework
- Configurar la cadena de conexión en `appsettings.json`

## Configuración 🔧

### Backend
1. Modificar `appsettings.json` con tus credenciales de base de datos
2. Configurar el secreto JWT en `appsettings.Development.json`
3. Ejecutar las migraciones:
```bash
dotnet ef database update
```

### Frontend
1. Configurar las variables de entorno en `.env`:
```
VITE_API_URL=http://localhost:[Your local port]
```

## Uso 💻

1. Navegar por el dashboard para acceder a las diferentes funcionalidades
2. Gestionar mascotas, citas, medicamentos


## Contacto 📧

Juan Diego Silva - juandsilva028@gmail.com
