# Sistema de Gestión Veterinaria 🏥

Sistema completo para la gestión de una clínica veterinaria, desarrollado con .NET 8 y React.

## Características Principales 🌟

### Gestión de Usuarios
- Registro y autenticación de usuarios
- Roles de administrador y veterinario
- Perfiles de usuario personalizables

### Gestión de Mascotas
- Registro completo de mascotas
- Historial médico
- Seguimiento de tratamientos
- Fotos y documentos adjuntos

### Gestión de Citas
- Sistema de citas médicas
- Estados de cita (Pendiente, Confirmada, Cancelada, Completada)
- Asignación de veterinarios
- Recordatorios automáticos

### Gestión de Medicamentos
- Control de inventario
- Registro de medicamentos
- Alertas de stock bajo
- Historial de administración

### Gestión de Tratamientos
- Registro de tratamientos
- Asignación de medicamentos
- Seguimiento de dosis
- Historial de tratamientos

## Tecnologías Utilizadas 🛠️

### Backend
- .NET 8
- Entity Framework Core
- SQL Server
- JWT Authentication
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

- .NET 8 SDK
- Node.js 18+
- SQL Server 2019+
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
cd VetAPI
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
VITE_API_URL=http://localhost:5000
```

## Uso 💻

1. Iniciar sesión con las credenciales de administrador
2. Navegar por el dashboard para acceder a las diferentes funcionalidades
3. Gestionar mascotas, citas, medicamentos y tratamientos
4. Generar reportes y estadísticas

## Contribución 🤝

1. Fork el proyecto
2. Crear una rama para tu feature (`git checkout -b feature/AmazingFeature`)
3. Commit tus cambios (`git commit -m 'Add some AmazingFeature'`)
4. Push a la rama (`git push origin feature/AmazingFeature`)
5. Abrir un Pull Request

## Licencia 📄

Este proyecto está bajo la Licencia MIT - ver el archivo [LICENSE.md](LICENSE.md) para más detalles.

## Contacto 📧

Tu Nombre - [@tutwitter](https://twitter.com/tutwitter) - email@ejemplo.com

Link del Proyecto: [https://github.com/tu-usuario/vet](https://github.com/tu-usuario/vet) 