# Specmatic Contract Testing - Quick Start Guide

## 5-Minute Quick Start

### 1. Open Visual Studio 2022

### 2. Create New ASP.NET Core Web API Project
```
File → New → Project → "ASP.NET Core Web API" → Create
```
- Project name: `SpecmaticAPIProject`
- Framework: `.NET 6.0` or later

### 3. Add the Provided Files

**Replace/Add these files to your project:**
- `Program.cs`
- `SpecmaticAPIProject.csproj`
- `appsettings.json`
- `specmatic.yaml`
- `employees-contract.yaml`

**Create folders and add files:**
- `Models/Employee.cs`
- `Controllers/EmployeesController.cs`

### 4. Build the Project
```
Ctrl + Shift + B  (or Build → Build Solution)
```

### 5. Run the API
```
F5  (or Debug → Start Debugging)
```

The Swagger UI should open automatically at `http://localhost:5000/swagger`

**API is now running!** ✓

---

## Test the API with Swagger

1. In the Swagger UI, expand **GET /api/employees**
2. Click "Try it out" → "Execute"
3. You should see a list of 3 employees

Try these endpoints:
- **GET /api/employees** - Get all employees
- **GET /api/employees/1** - Get employee with ID 1
- **GET /api/employees/999** - Try getting non-existent employee (returns 404)
- **POST /api/employees** - Create new employee
- **PUT /api/employees/1** - Update employee
- **DELETE /api/employees/1** - Delete employee

---

## Install and Run Specmatic Tests

### Install Specmatic (if not already installed)

**Option 1: Using npm** (Recommended)
```bash
npm install -g specmatic
```

**Option 2: Using Docker**
- No installation needed, use Docker commands directly

### Run Specmatic Contract Tests

**Keep API running, open a new Terminal/PowerShell and run:**

```bash
cd C:\path\to\SpecmaticAPIProject

# Using npm
specmatic test --contractsPath employees-contract.yaml --host localhost --port 5000

# Or using Docker
docker run -v %CD%:/app -w /app znsio/specmatic test --contractsPath employees-contract.yaml --host localhost --port 5000
```

### Expected Output

```
┌────────────────────────────────────────────┐
│ Specmatic Contract Test Results            │
├────────────────────────────────────────────┤
│                                            │
│ GET /api/employees                         │
│   ✓ Success (200)                          │
│                                            │
│ POST /api/employees                        │
│   ✓ Success (201)                          │
│   ✓ Validation Error (400)                 │
│                                            │
│ GET /api/employees/{id}                    │
│   ✓ Success (200)                          │
│   ✓ Not Found (404)                        │
│                                            │
│ PUT /api/employees/{id}                    │
│   ✓ Success (204)                          │
│   ✓ Not Found (404)                        │
│                                            │
│ DELETE /api/employees/{id}                 │
│   ✓ Success (204)                          │
│   ✓ Not Found (404)                        │
│                                            │
├────────────────────────────────────────────┤
│ Total Scenarios: 11                        │
│ Passed: 11                                 │
│ Failed: 0                                  │
└────────────────────────────────────────────┘
```

---

## Project Structure Overview

```
SpecmaticAPIProject/
│
├── 📄 SpecmaticAPIProject.csproj         ← Project file with Swashbuckle dependency
├── 📄 Program.cs                         ← ASP.NET startup & Swagger config
├── 📄 appsettings.json                   ← App configuration
│
├── 📁 Models/
│   └── 📄 Employee.cs                    ← Employee data class
│
├── 📁 Controllers/
│   └── 📄 EmployeesController.cs         ← REST API endpoints (GET, POST, PUT, DELETE)
│
├── 📁 Properties/
│   └── 📄 launchSettings.json            ← Visual Studio launch settings
│
├── 📄 employees-contract.yaml            ← OpenAPI contract specification
├── 📄 specmatic.yaml                     ← Specmatic test configuration
│
└── 📄 SETUP_GUIDE.md                     ← Detailed setup instructions
```

---

## What is Happening?

### 1. **API Implementation** (Your Code)
```
Controllers/EmployeesController.cs
  ↓
  Implements REST endpoints: GET, POST, PUT, DELETE
```

### 2. **Contract Definition** (employees-contract.yaml)
```
employees-contract.yaml
  ↓
  Defines API contract using OpenAPI 3.0
  - Expected endpoints
  - Expected request/response formats
  - Example test data
```

### 3. **Contract Testing** (Specmatic)
```
Specmatic CLI
  ↓
  Reads contract
  ↓
  Makes HTTP requests to running API
  ↓
  Validates responses match contract
  ↓
  Reports test results
```

---

## Key Concepts

| Concept | Explanation |
|---------|-------------|
| **Contract** | Agreement between API and consumers about format/behavior |
| **OpenAPI** | Standard format for describing REST APIs (YAML/JSON) |
| **Specmatic** | Tool that validates API against contract |
| **Example** | Sample request/response data in the contract |
| **Scenario** | Test case generated from contract examples |

---

## Common Tasks

### Change API Port

1. Edit `launchSettings.json`:
   ```json
   "applicationUrl": "http://localhost:5000"
   ```

2. Edit `specmatic.yaml`:
   ```yaml
   port: 5000
   ```

### Add New Endpoint

1. Add method to `Controllers/EmployeesController.cs`
2. Add path to `employees-contract.yaml` with examples
3. Rebuild and rerun tests

### Check API Swagger Spec

Visit: `http://localhost:5000/swagger/v1/swagger.json`

---

## Troubleshooting

| Problem | Solution |
|---------|----------|
| API won't start | Check port is not in use, rebuild project |
| Swagger not showing | Ensure `Program.cs` has Swagger setup |
| Specmatic command not found | Install Node.js and npm, run `npm install -g specmatic` |
| Tests fail | Ensure API is running, check port in `specmatic.yaml` matches |
| Port already in use | Change port in `launchSettings.json` and `specmatic.yaml` |

---

## Next Steps

1. ✅ Get the project running (F5)
2. ✅ Test endpoints in Swagger UI
3. ✅ Run Specmatic tests
4. ✅ Explore the contract file (employees-contract.yaml)
5. ✅ Add a new endpoint and update the contract
6. ✅ Run tests again to validate

---

## Resources

- **Specmatic Docs**: https://specmatic.in
- **OpenAPI Guide**: https://swagger.io/resources/articles/best-practices-in-api-design/
- **ASP.NET Core**: https://docs.microsoft.com/dotnet/
- **GitHub**: https://github.com/znsio/specmatic

---

**Ready to test?** Start with F5! 🚀
