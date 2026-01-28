# Employee Manager

A full-stack employee management system built with .NET 10 Web API and Angular 21.

## Architecture

- **Backend**: ASP.NET Core 10 Web API with Entity Framework Core (SQLite)
- **Frontend**: Angular 21 with standalone components and NG-ZORRO UI library
- **API Client**: Auto-generated TypeScript client from OpenAPI specs

## Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- [Node.js](https://nodejs.org/) (v18 or higher)
- [npm](https://www.npmjs.com/) (v11.6.2 or higher)

## Running Locally

### Backend (Web API)

1. Navigate to the backend directory:
   ```bash
   cd EmployeeManager.WebAPI
   ```

2. Restore dependencies (optional, runs automatically):
   ```bash
   dotnet restore
   ```

3. Run the API:
   ```bash
   dotnet run
   ```

The API will start on `http://localhost:5000`

- Swagger UI: `http://localhost:5000/swagger`
- OpenAPI spec: `http://localhost:5000/openapi/v1.json`

**Note**: Database migrations run automatically on startup. The SQLite database file (`employeemanager.db`) will be created in the project directory.

### Frontend (Angular)

1. Navigate to the frontend directory:
   ```bash
   cd EmployeeManager.ClientApp
   ```

2. Install dependencies:
   ```bash
   npm install
   ```

3. Start the development server:
   ```bash
   npm start
   ```

The app will start on `http://localhost:4200`

## NPM Scripts (Frontend)

| Command | Description |
|---------|-------------|
| `npm start` | Start development server on port 4200 |
| `npm run build` | Build production bundle |
| `npm run watch` | Build in watch mode (development) |
| `npm test` | Run tests |
| `npm run generate-api` | Regenerate TypeScript API client from OpenAPI spec (backend must be running) |

### Regenerating the API Client

When you make changes to backend controllers or DTOs, regenerate the frontend API client:

```bash
cd EmployeeManager.ClientApp
npm run generate-api
```

⚠️ **Important**: The backend must be running on `http://localhost:5000` before running this command.

## .NET CLI Commands (Backend)

| Command | Description |
|---------|-------------|
| `dotnet run` | Run the API in development mode |
| `dotnet build` | Build the project |
| `dotnet test` | Run tests |
| `dotnet ef migrations add <Name>` | Create a new migration |
| `dotnet ef database update` | Apply migrations (runs automatically on startup) |

## Makefile Targets (Deployment)

Both frontend and backend include Makefiles for Azure Container Apps deployment.

### Common Makefile Targets

| Target | Description |
|--------|-------------|
| `make help` | Show all available targets and configuration |
| `make build` | Build Docker image locally |
| `make push` | Push image to Azure Container Registry |
| `make build-push` | Build and push image |
| `make create-rg` | Create Azure Resource Group |
| `make create-env` | Create Container App Environment |
| `make create-app` | Create Container App |
| `make deploy` | Deploy to existing Container App |
| `make full-deploy` | Build, push, and deploy |

### Example Deployment

```bash
# Backend
cd EmployeeManager.WebAPI
make build
make push
make deploy

# Frontend
cd EmployeeManager.ClientApp
make build
make push
make deploy
```

Override default variables:

```bash
make deploy APP_NAME=my-employee-api RESOURCE_GROUP=my-rg
```

## Project Structure

```
EmployeeManager/
├── EmployeeManager.WebAPI/          # .NET Web API
│   ├── Controllers/                 # API endpoints
│   ├── Models/                      # Entity models
│   ├── DTOs/                        # Data transfer objects
│   ├── Data/                        # EF Core context & migrations
│   ├── Mappings/                    # AutoMapper profiles
│   └── Makefile                     # Azure deployment
│
└── EmployeeManager.ClientApp/       # Angular app
    ├── src/app/                     # Application code
    │   ├── api/                     # Auto-generated API client
    │   └── pages/                   # Feature components
    ├── public/                      # Static assets
    └── Makefile                     # Azure deployment
```

## Database

The application uses SQLite with the following key features:

- **Auto-migrations**: Database migrations run automatically on startup
- **Seed data**: Countries and job categories are seeded on first run
- **Optional seeding**: Employee data can be seeded from `Data/OptionalSeed/employees.json`

## Technologies

### Backend
- .NET 10
- Entity Framework Core (SQLite)
- AutoMapper
- OpenAPI/Swagger

### Frontend
- Angular 21 (standalone components)
- NG-ZORRO (Ant Design)
- Tailwind CSS
- RxJS

## Development Tips

1. **Backend changes require API regeneration**: After modifying controllers or DTOs, run `npm run generate-api` in the frontend
2. **CORS is enabled** for development (any origin allowed)
3. **Migrations auto-run** - no need to manually update the database
4. **Component pattern**: All Angular components use function-based DI with `inject()` and OnPush change detection

## License

MIT
