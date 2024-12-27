# 🚀 Call Tagging System

A web-based application built with ASP.NET Core MVC for tracking and managing customer service calls. This system allows agents to log call details, categorize issues, and generate reports.

## 📂 Features

- **Secure Login System**
  - Agent authentication with 6-digit agent numbers
  - Password encryption using BCrypt
  - Session management for logged-in users

- **Call Logging**
  - Record call details including:
    - Agent phone number
    - Connection type
    - Category (e.g., Broadband)
    - Problem types (multiple selections allowed)
    - Issue type
    - Timestamp
  - Automatic validation of required fields
  - Support for multiple problem tags per call

- **Search and Reporting**
  - Advanced search functionality with multiple filters:
    - Date range
    - Category
    - Connection type
  - Export search results to Excel
  - View and manage call records

## 📂 Technical Stack

- **Backend**: ASP.NET Core MVC
- **Database**: MySQL
- **ORM**: Entity Framework Core
- **Authentication**: Custom implementation with BCrypt.NET
- **Excel Export**: ClosedXML
- **Session Management**: ASP.NET Core Session

## 📂 Prerequisites

- .NET Core SDK
- MySQL Server
- Visual Studio (recommended) or any preferred IDE

## 📂 Database Configuration

The application uses MySQL as its database. Update the connection string in `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=calls;Uid=root;Pwd=your_password;"
  }
}
```

## 📂 Project Structure

- **Controllers/**
  - `LoginController.cs`: Handles user authentication
  - `CallsController.cs`: Manages call logging functionality
  - `SearchController.cs`: Handles search and export features

- **Models/**
  - `Call.cs`: Defines the call data structure

- **Data/**
  - Contains database context and configurations

## 📂 Key Features Implementation

### Authentication
- Uses BCrypt for password hashing
- Session-based authentication with configurable timeout
- Validates agent number format (6 digits)

### Call Logging
- Supports multiple problem categories
- Implements required field validation
- Handles both successful and error scenarios
- Default category fallback to "Broadband"

### Search and Export
- Flexible search criteria
- Excel export functionality

## 📂 Security Features

- HTTP-only cookies
- Session timeout management
- Password encryption
- Input validation and sanitization
- HTTPS redirection

## 📂 Getting Started

1. Clone the repository
2. Update the database connection string in `appsettings.json`
3. Run the following commands:
   ```bash
   dotnet restore
   dotnet ef database update
   dotnet run
   ```
4. Navigate to `https://localhost:5001` (or the configured port)

## 📂 Notes

- Session timeout is set to 365 days (configurable in Program.cs)
- Default category is set to "Broadband" if not specified
