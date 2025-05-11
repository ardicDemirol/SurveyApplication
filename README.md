# SurveyApplication

**SurveyApplication** is a robust application designed for managing and conducting surveys. It is built using modern technologies to ensure scalability, maintainability, and performance.

## Table of Contents
- [Features](#features)
- [Technologies Used](#technologies-used)
- [Architecture Diagram](#architecture-diagram)
- [Getting Started](#getting-started)
- [Contributing](#contributing)
- [License](#license)

## Features
- **Survey Management**: Create, manage, and analyze surveys with ease.
- **Authentication & Authorization**: Secure access using JWT.
- **Background Jobs**: Manage recurring tasks and background jobs with Hangfire.
- **Validation**: Ensure robust input handling with Fluent Validation.
- **Testing**: Comprehensive unit testing with XUnit and Moq.
- **Scalable Architecture**: Designed for scalability using MediatR and Dapper.

## Technologies Used
The project leverages the following technologies:
- **.NET Core 8**: For building high-performance, cross-platform applications.
- **PostgreSQL**: As the primary database for storing survey data.
- **Dapper**: A lightweight ORM for efficient database interactions.
- **JWT (JSON Web Token)**: For secure authentication and authorization.
- **MediatR**: For implementing the mediator pattern to decouple application layers.
- **Microsoft Garnet**: For enhanced dependency injection and modularity.
- **Hangfire**: For background job processing and scheduling recurring tasks.
- **Fluent Validation**: For input validation and enforcing business rules.
- **XUnit**: For unit testing the application logic.
- **Moq**: For creating mock objects in tests.

## Architecture Diagram
Below is a conceptual architecture diagram for the application:

```plaintext
+--------------------+           +----------------------+
|  Client (UI/UX)   |<--------->|  API Gateway (ASP.NET)|
+--------------------+           +----------------------+
                                        |
                                        v
                        +-------------------------------+
                        |     Application Layer         |
                        |  (Controllers, MediatR, etc.) |
                        +-------------------------------+
                                        |
                                        v
                        +-------------------------------+
                        |       Business Logic          |
                        | (Services, Fluent Validation) |
                        +-------------------------------+
                                        |
                                        v
                        +-------------------------------+
                        |       Data Access Layer       |
                        |   (Dapper, PostgreSQL)        |
                        +-------------------------------+
                                        |
                                        v
                        +-------------------------------+
                        |    Background Job Handling    |
                        |      (Hangfire, etc.)         |
                        +-------------------------------+
```

*Feel free to replace this with an actual image diagram if possible.*

## Getting Started

### Prerequisites
- **.NET Core 8 SDK**: Install from [here](https://dotnet.microsoft.com/).
- **PostgreSQL**: Install from [here](https://www.postgresql.org/).

### Installation
1. Clone the repository:
   ```bash
   git clone https://github.com/ardicDemirol/SurveyApplication.git
   ```
2. Navigate to the project directory:
   ```bash
   cd SurveyApplication
   ```
3. Install dependencies:
   ```bash
   dotnet restore
   ```
4. Update the `appsettings.json` file with your PostgreSQL connection string.

### Running the Application
1. Apply migrations to set up the database:
   ```bash
   dotnet ef database update
   ```
2. Run the application:
   ```bash
   dotnet run
   ```

### Running Tests
To execute unit tests:
```bash
dotnet test
```

## Contributing
Contributions are welcome! If you'd like to contribute, please follow these steps:

1. Fork this repository.
2. Create a new branch:
   ```bash
   git checkout -b feature/YourFeatureName
   ```
3. Commit your changes:
   ```bash
   git commit -m "Add your meaningful commit message"
   ```
4. Push to the branch:
   ```bash
   git push origin feature/YourFeatureName
   ```
5. Submit a pull request.

## License
This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.
