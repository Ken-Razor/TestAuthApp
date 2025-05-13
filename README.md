# TestAuthApp

A complete authentication system with both frontend (Blazor) and backend components.

![Auth System](https://img.shields.io/badge/Authentication-System-blue)
![Blazor](https://img.shields.io/badge/Frontend-Blazor-purple)
![ASP.NET Core](https://img.shields.io/badge/Backend-ASP.NET_Core-green)

## Features

- **User Registration**
  - Email/username validation
  - Password strength requirements
  - Real-time feedback
- **User Login**
  - Secure credential handling
  - Session management
- **Responsive UI**
  - Clean, modern interface
  - Mobile-friendly design
- **Error Handling**
  - Clear error messages
  - User-friendly modals

## Project Structure
TestAuthApp/
├── AuthClient/ # Blazor Frontend
│ ├── Pages/ # Application pages
│ ├── Shared/ # Shared components and models
│ └── wwwroot/ # Static assets
│
├── AuthServer/ # ASP.NET Core Backend
│ ├── Controllers/ # API endpoints
│ ├── Models/ # Data models
│ └── Services/ # Business logic
│
├── .vscode/ # VS Code settings
├── .gitattributes # Git configuration
└── TestAuthApp.sln # Visual Studio solution file


## Getting Started

### Prerequisites

- .NET 8 SDK
- Visual Studio 2022 or VS Code

### Installation

1. Clone the repository:
    git clone https://github.com/yourusername/TestAuthApp.git
    cd TestAuthApp

2. Restore dependencies:
    dotnet restore

3. Configure the applications:
   - Update connection strings in appsettings.json
   - Set any required environment variables

### Running the Application

1. Start the backend:
    cd AuthServer
    dotnet run

2. Start the frontend:
    cd ../AuthClient
    dotnet run

3. Open in browser:
    https://localhost:5001 (or your configured port)


## 📚 API Documentation

| Endpoint               | Method | Description                     |
|------------------------|--------|---------------------------------|
| `/api/auth/register`   | POST   | Register new user               |
| `/api/auth/login`      | POST   | Authenticate user               |
| `/api/auth/validate`   | GET    | Validate authentication token   |

## 📧 Contact

- **Email**: ken.thea02@gmail.com  
- **GitHub**: [@kenthdev](https://github.com/Ken-Razor)  
- **LinkedIn**: [Your Name](https://www.linkedin.com/in/kenthea2)
