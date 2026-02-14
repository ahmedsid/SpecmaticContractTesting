# Specmatic Contract Testing Project - C# Setup Guide

## Project Overview

This project demonstrates contract testing using Specmatic with a C# ASP.NET Core Web API. The Employee Management API serves as the service under test, and the `employees-contract.yaml` file defines the API contract that Specmatic will validate.

## Project Structure

```
SpecmaticAPIProject/
├── SpecmaticAPIProject.csproj       # Project file with dependencies
├── Program.cs                        # ASP.NET Core startup configuration
├── Controllers/
│   └── EmployeesController.cs       # REST API endpoints
├── Models/
│   └── Employee.cs                  # Employee data model
├── employees-contract.yaml           # API contract specification (OpenAPI 3.0)
├── specmatic.yaml                    # Specmatic configuration
└── Properties/
    └── launchSettings.json          # VS launch profile settings
```

## Prerequisites

1. **Visual Studio 2022** (Community, Professional, or Enterprise)
   - Install during setup: ASP.NET and web development workload
   
2. **.NET 6.0 SDK or later**
   - Download from https://dotnet.microsoft.com/download

3. **Specmatic CLI**
   - Download from: https://github.com/znsio/specmatic/releases
   - Or install via: `npm install -g specmatic` (requires Node.js)

4. **Java Runtime (Optional but recommended)**
   - Required if using Specmatic JAR directly
   - Download from https://www.oracle.com/java/technologies/downloads/

## Step-by-Step Setup Instructions

### Step 1: Create the Project in Visual Studio

1. Open Visual Studio 2022
2. Click **File** → **New** → **Project**
3. Search for "ASP.NET Core Web API"
4. Select **ASP.NET Core Web API** template
5. Click **Next**
6. Configure:
   - Project name: `SpecmaticAPIProject`
   - Location: Choose your preferred directory
   - Click **Next**
7. Additional information:
   - Framework: **.NET 6.0** (or latest)
   - Authentication type: **None**
   - Configure for HTTPS: **Uncheck** (for simplicity)
   - Click **Create**

### Step 2: Add Project Files

1. Replace the auto-generated files with the provided files:
   - Copy `SpecmaticAPIProject.csproj` → Replace existing .csproj
   - Copy `Program.cs` → Replace existing Program.cs
   - Replace/delete Controllers/WeatherForecastController.cs

2. Add new files:
   - Create folder: `Models/` and add `Employee.cs`
   - Create folder: `Controllers/` and add `EmployeesController.cs`
   - Add `employees-contract.yaml` to project root
   - Add `specmatic.yaml` to project root

### Step 3: Verify Project Structure in Solution Explorer

Your solution should look like:
```
SpecmaticAPIProject
├── Connected Services
├── Dependencies
├── Properties
│   └── launchSettings.json
├── Controllers
│   └── EmployeesController.cs
├── Models
│   └── Employee.cs
├── appsettings.json
├── appsettings.Development.json
├── employees-contract.yaml
├── specmatic.yaml
└── SpecmaticAPIProject.csproj
```

### Step 4: Build the Project

1. Right-click on project in Solution Explorer → **Build**
   OR
2. **Build** menu → **Build Solution** (or Ctrl+Shift+B)

3. Ensure no build errors appear in the Error List

### Step 5: Run the API

**Option A: Using Visual Studio Debug**

1. Press **F5** or **Ctrl+F5** to start the API
2. The API will launch at: `https://localhost:5001` or `http://localhost:5000`
3. Swagger UI should open automatically
4. Verify the API is running by visiting `/swagger` endpoint

**Option B: Using .NET CLI**

1. Open Command Prompt/PowerShell in project directory
2. Run: `dotnet run`
3. Note the port number displayed (usually 5000 or 5001)

### Step 6: Test the API with Swagger

1. When the API runs, Swagger UI opens automatically
2. Expand the `/api/employees` endpoints
3. Try the **GET** endpoint:
   - Click **Try it out**
   - Click **Execute**
   - Should return the list of pre-populated employees
4. Try the **POST** endpoint to create an employee
5. Try **GET by ID** with ID=1 to fetch a specific employee

### Step 7: Install Specmatic CLI

**Option A: Using npm (Recommended)**

1. Install Node.js from https://nodejs.org/
2. Open Command Prompt/PowerShell
3. Run: `npm install -g specmatic`
4. Verify: `specmatic --version`

**Option B: Download JAR**

1. Download the latest JAR from: https://github.com/znsio/specmatic/releases
2. Extract to a convenient location
3. Ensure Java is in your PATH

### Step 8: Configure Specmatic

1. Update `specmatic.yaml`:
   ```yaml
   sources:
     - provider: file
       path: employees-contract.yaml
   
   host: localhost
   port: 5000  # Update if your API runs on different port
   ```

2. Ensure `employees-contract.yaml` is in your project root

### Step 9: Run Specmatic Contract Tests

**Keep your API running (from Step 5)**

**Then in a new Command Prompt/PowerShell:**

1. Navigate to your project root directory
2. Run one of the following commands:

   **Using npm (if installed globally):**
   ```bash
   specmatic test --contractsPath employees-contract.yaml
   ```

   **Using Docker:**
   ```bash
   docker run -v %CD%:/app -w /app znsio/specmatic test --contractsPath employees-contract.yaml
   ```

   **Full command with host/port:**
   ```bash
   specmatic test --contractsPath employees-contract.yaml --host localhost --port 5000
   ```

### Step 10: View Test Results

After running Specmatic tests, you should see output like:

```
Scenario: GET /api/employees
  ✓ Response status is 200
  ✓ Response body matches schema
  
Scenario: POST /api/employees
  ✓ Request created successfully
  ✓ Response status is 201
  
Scenario: GET /api/employees/{id}
  ✓ Successfully retrieved employee with ID 1
  
Tests Summary:
  Total: 12
  Passed: 12
  Failed: 0
```

## How Specmatic Contract Testing Works

1. **Contract Definition**: The `employees-contract.yaml` file defines the API contract
2. **Test Generation**: Specmatic automatically generates tests from the contract examples
3. **Validation**: Specmatic makes HTTP requests to your API and validates:
   - Response status codes match the contract
   - Response bodies conform to the defined schemas
   - Request/response examples are correct
4. **Reporting**: Generates a report showing which endpoints were tested

## Understanding the Contract File (employees-contract.yaml)

The contract file defines:

- **Paths**: All API endpoints (/api/employees, /api/employees/{id})
- **Methods**: GET, POST, PUT, DELETE
- **Request/Response Schemas**: Structure of data
- **Examples**: Actual test data for each scenario
- **Status Codes**: Expected HTTP status codes (200, 201, 404, etc.)

Example scenario from contract:
```yaml
GET /api/employees/{id}:
  Request: id = 1
  Response: 
    Status: 200
    Body: { id: 1, name: "John Doe", ... }
```

## Testing Different Scenarios

### Scenario 1: Success Case
- **Endpoint**: GET /api/employees/1
- **Expected**: 200 OK with employee data
- **Test**: Specmatic verifies response matches contract

### Scenario 2: Not Found Case
- **Endpoint**: GET /api/employees/999
- **Expected**: 404 Not Found with error message
- **Test**: Specmatic verifies the error response format

### Scenario 3: Create Employee
- **Endpoint**: POST /api/employees
- **Expected**: 201 Created with new employee data
- **Test**: Specmatic validates request and response

## Troubleshooting

### Issue: "Connection refused" error
- **Solution**: Ensure API is running with `dotnet run` or F5 in Visual Studio

### Issue: Port already in use
- **Solution**: Change port in `launchSettings.json` or `specmatic.yaml`

### Issue: "specmatic command not found"
- **Solution**: Verify Node.js and npm are installed, reinstall specmatic: `npm install -g specmatic`

### Issue: Tests fail with "Response schema mismatch"
- **Solution**: Ensure API returns data in exact format specified in contract YAML

### Issue: Cannot find contract file
- **Solution**: Ensure `employees-contract.yaml` is in the same directory where you run the `specmatic test` command

## Extending the Project

### Add New Endpoints:

1. Add controller method in `EmployeesController.cs`
2. Add the endpoint to `employees-contract.yaml` with:
   - Path definition
   - Request/Response schemas
   - Examples for each scenario
3. Re-run Specmatic tests

### Modify Employee Model:

1. Add new property to `Employee.cs`
2. Update the schema in `employees-contract.yaml`
3. Update example data in the contract file

### Change API Port:

1. Edit `Properties/launchSettings.json`
2. Update port in `specmatic.yaml`

## Key Files Explained

| File | Purpose |
|------|---------|
| `SpecmaticAPIProject.csproj` | Project configuration and NuGet dependencies |
| `Program.cs` | ASP.NET Core startup, Swagger configuration |
| `Controllers/EmployeesController.cs` | API endpoints implementation |
| `Models/Employee.cs` | Data model definition |
| `employees-contract.yaml` | OpenAPI 3.0 specification with contract details |
| `specmatic.yaml` | Specmatic testing configuration |

## Next Steps

1. **Run the project successfully** with F5
2. **Test endpoints** with Swagger UI
3. **Run Specmatic tests** to validate the contract
4. **Add more endpoints** and update the contract
5. **Integrate into CI/CD** pipeline for automated testing

## Additional Resources

- **Specmatic Documentation**: https://specmatic.in
- **OpenAPI Specification**: https://swagger.io/specification/
- **ASP.NET Core Documentation**: https://docs.microsoft.com/dotnet/
- **Swagger/Swashbuckle**: https://github.com/domaindrivendev/Swashbuckle.AspNetCore

## Quick Reference Commands

```bash
# Build the project
dotnet build

# Run the API
dotnet run

# Run Specmatic tests
specmatic test --contractsPath employees-contract.yaml --host localhost --port 5000

# Generate OpenAPI spec from running API
# Visit: http://localhost:5000/swagger/v1/swagger.json

# Clean build artifacts
dotnet clean
```

---

Happy Contract Testing! 🎉
