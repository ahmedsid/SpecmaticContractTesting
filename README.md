# Specmatic Contract Testing - C# API Project

A complete example project demonstrating **contract testing** using **Specmatic** with a C# ASP.NET Core REST API.

## 📋 Overview

This project showcases how to:
- Build a simple REST API in **ASP.NET Core** (.NET 6.0+)
- Define an **OpenAPI 3.0 contract** specification
- Validate the API against the contract using **Specmatic**
- Perform **contract testing** to ensure API compliance

### What is Contract Testing?

Contract testing validates that your API behaves according to its specification. Instead of writing manual tests, **Specmatic automatically generates tests from your OpenAPI contract** and validates your running API.

**Benefits:**
- ✅ Automatic test generation from contracts
- ✅ Catch breaking changes early
- ✅ Consumer-driven contract testing
- ✅ Better documentation and communication
- ✅ Faster feedback loops

---

## 🏗️ Project Architecture

### Components

| Component | Purpose |
|-----------|---------|
| **EmployeesController.cs** | REST API endpoints (GET, POST, PUT, DELETE) |
| **Employee.cs** | Data model representing an employee |
| **employees-contract.yaml** | OpenAPI 3.0 contract specification |
| **specmatic.yaml** | Specmatic configuration |
| **Program.cs** | ASP.NET Core startup and Swagger setup |

### API Endpoints

```
GET    /api/employees          - Get all employees
GET    /api/employees/{id}     - Get employee by ID
POST   /api/employees          - Create new employee
PUT    /api/employees/{id}     - Update employee
DELETE /api/employees/{id}     - Delete employee
```

---

## 🚀 Quick Start (5 Minutes)

### Prerequisites
- **Visual Studio 2026** (with ASP.NET Core workload)
- **.NET 8.0 SDK** or later
- **Specmatic CLI** (via npm or Docker)
- **Java 21 LTS

### Steps

1. **Clone/Download this project**
   ```bash
   # Copy all files to your machine
   ```

2. **Open in Visual Studio**
   ```
   File → Open → Folder (select project folder)
   ```

3. **Build the project**
   ```
   Ctrl + Shift + B
   ```

4. **Run the API**
   ```
   F5
   ```
   API starts at `http://localhost:5000` with Swagger UI

5. **Install Specmatic** (in a separate terminal)
   ```bash
   npm install -g specmatic
   ```

6. **Run contract tests** (keep API running)
   ```bash
   cd C:\path\to\project
   specmatic test employees-contract.yaml --testBaseURL=https://localhost:50534
   ```

7. **View results** ✓
   All tests should pass!

---

## 📁 File Structure

```
SpecmaticAPIProject/
│
├── 📖 README.md                          (This file)
├── 📖 QUICK_START.md                     (5-minute guide)
├── 📖 SETUP_GUIDE.md                     (Detailed setup)
│
├── SpecmaticAPIProject.csproj            (Project configuration)
├── Program.cs                            (ASP.NET startup)
├── appsettings.json                      (App settings)
│
├── Models/
│   └── Employee.cs                       (Data model)
│
├── Controllers/
│   └── EmployeesController.cs            (API endpoints)
│
├── Properties/
│   └── launchSettings.json               (Visual Studio settings)
│
├── employees-contract.yaml               (OpenAPI 3.0 specification)
├── specmatic.yaml                        (Specmatic configuration)
│
└── .gitignore                            (Git ignore rules)
```

---

## 🔧 Installation & Setup

### Detailed Setup (See SETUP_GUIDE.md for more details)

#### 1. Create Project in Visual Studio
```
File → New → Project
Search: "ASP.NET Core Web API"
Name: SpecmaticAPIProject
Framework: .NET 6.0
```

#### 2. Add Project Files
Copy the following to your project:
- `SpecmaticAPIProject.csproj`
- `Program.cs`
- `appsettings.json`
- `specmatic.yaml`
- `employees-contract.yaml`

#### 3. Create Folders & Files
```
Models/
  └── Employee.cs

Controllers/
  └── EmployeesController.cs

Properties/
  └── launchSettings.json
```

#### 4. Build Project
```
Ctrl + Shift + B  (or Build > Build Solution)
```

#### 5. Run API
```
F5  (or Debug > Start Debugging)
```

Swagger UI opens automatically at `http://localhost:5000/swagger`

---

## 🧪 Running Tests

### Test the API Manually (Swagger UI)

1. API is running at `http://localhost:5000`
2. Swagger UI opens automatically
3. Expand endpoints and click "Try it out"
4. Execute requests and view responses

### Run Specmatic Contract Tests

**Terminal 1 (Keep running):**
```bash
# API is running with: F5 in Visual Studio
```

**Terminal 2 (New terminal):**
```bash
cd C:\path\to\SpecmaticAPIProject

# Run Specmatic tests
specmatic test --contractsPath employees-contract.yaml --host localhost --port 5000
```

### Expected Test Results

```
✓ GET /api/employees - Success (200)
✓ GET /api/employees/{id} - Success (200)
✓ GET /api/employees/{id} - Not Found (404)
✓ POST /api/employees - Created (201)
✓ POST /api/employees - Bad Request (400)
✓ PUT /api/employees/{id} - No Content (204)
✓ PUT /api/employees/{id} - Not Found (404)
✓ DELETE /api/employees/{id} - No Content (204)
✓ DELETE /api/employees/{id} - Not Found (404)

Tests Summary:
  Total: 11
  Passed: 11
  Failed: 0
```

---

## 📝 Understanding the Contract File

The `employees-contract.yaml` file is the heart of contract testing:

```yaml
# Define API paths and methods
paths:
  /api/employees:
    get:
      # Endpoint description
      summary: Get all employees
      
      # Response schema
      responses:
        '200':
          schema: [Employee]
          examples:
            GET_ALL_EMPLOYEES_SUCCESS:
              value:
                - id: 1
                  name: "John Doe"
                  # ...
    
    post:
      # Request body schema
      requestBody:
        schema: Employee
        examples:
          CREATE_EMPLOYEE_REQUEST:
            value:
              name: "Alice Cooper"
              # ...
      
      # Possible responses
      responses:
        '201':
          schema: Employee
        '400':
          schema: ErrorResponse

# Component schemas (reusable definitions)
components:
  schemas:
    Employee:
      type: object
      properties:
        id: integer
        name: string
        email: string
        # ... more properties
```

### How Specmatic Uses the Contract

1. **Reads** the contract file
2. **Generates** test scenarios from examples
3. **Sends** HTTP requests to your API
4. **Validates** responses match the contract
5. **Reports** results

---

## 🔄 Workflow

```
┌─────────────────────────────────────────────────────────┐
│ 1. Write API Code                                       │
│    (EmployeesController.cs)                             │
└────────────────────┬────────────────────────────────────┘
                     ↓
┌─────────────────────────────────────────────────────────┐
│ 2. Define Contract                                      │
│    (employees-contract.yaml - OpenAPI 3.0)             │
└────────────────────┬────────────────────────────────────┘
                     ↓
┌─────────────────────────────────────────────────────────┐
│ 3. Run API                                              │
│    (dotnet run or F5 in Visual Studio)                 │
└────────────────────┬────────────────────────────────────┘
                     ↓
┌─────────────────────────────────────────────────────────┐
│ 4. Run Specmatic Tests                                  │
│    (specmatic test command)                             │
└────────────────────┬────────────────────────────────────┘
                     ↓
┌─────────────────────────────────────────────────────────┐
│ 5. View Results                                         │
│    (All tests passed! ✓)                                │
└─────────────────────────────────────────────────────────┘
```

---

## 💡 Examples

### Example 1: Testing GET /api/employees

**Contract defines:**
```yaml
GET /api/employees:
  responses:
    '200':
      examples:
        GET_ALL_EMPLOYEES_SUCCESS:
          value:
            - id: 1
              name: "John Doe"
              email: "john.doe@company.com"
            - id: 2
              name: "Jane Smith"
              email: "jane.smith@company.com"
```

**Specmatic does:**
1. Makes GET request to `http://localhost:5000/api/employees`
2. Receives response with list of employees
3. Validates response matches the schema
4. Validates response status is 200
5. Reports: ✓ Test Passed

### Example 2: Testing GET /api/employees/{id} with 404

**Contract defines:**
```yaml
GET /api/employees/{id}:
  parameters:
    - name: id
      value: 999  # Non-existent ID
  responses:
    '404':
      examples:
        GET_EMPLOYEE_NOT_FOUND:
          value:
            message: "Employee with ID 999 not found"
```

**Specmatic does:**
1. Makes GET request to `http://localhost:5000/api/employees/999`
2. Receives 404 response
3. Validates error message format
4. Reports: ✓ Test Passed

---

## 🛠️ Extending the Project

### Add a New Endpoint

1. **Add controller method:**
   ```csharp
   [HttpGet("search")]
   public ActionResult<List<Employee>> SearchEmployees(string name)
   {
       var results = _employees.Where(e => e.Name.Contains(name)).ToList();
       return Ok(results);
   }
   ```

2. **Update contract file:**
   ```yaml
   /api/employees/search:
     get:
       parameters:
         - name: name
           in: query
           schema: string
       responses:
         '200':
           schema: array[Employee]
           examples:
             SEARCH_SUCCESS:
               value: [...]
   ```

3. **Rebuild and retest:**
   ```bash
   # Rebuild
   Ctrl + Shift + B
   
   # Rerun tests
   specmatic test --contractsPath employees-contract.yaml
   ```

### Modify Employee Model

1. Add property to `Employee.cs`
2. Update schema in `employees-contract.yaml`
3. Update examples in the contract
4. Test changes

---

## 📊 Test Coverage

The contract includes scenarios for:

| Endpoint | Method | Status | Scenario |
|----------|--------|--------|----------|
| `/api/employees` | GET | 200 | Get all employees |
| `/api/employees` | POST | 201 | Create employee |
| `/api/employees` | POST | 400 | Invalid request |
| `/api/employees/{id}` | GET | 200 | Get existing employee |
| `/api/employees/{id}` | GET | 404 | Employee not found |
| `/api/employees/{id}` | PUT | 204 | Update employee |
| `/api/employees/{id}` | PUT | 404 | Update non-existent |
| `/api/employees/{id}` | DELETE | 204 | Delete employee |
| `/api/employees/{id}` | DELETE | 404 | Delete non-existent |

---

## 🐛 Troubleshooting

### Issue: "dotnet: command not found"
**Solution:** Install .NET SDK from https://dotnet.microsoft.com/download

### Issue: Port 5000 already in use
**Solution:** 
- Change port in `launchSettings.json`
- Update port in `specmatic.yaml`

### Issue: "specmatic: command not found"
**Solution:**
```bash
npm install -g specmatic
```

### Issue: API returns 500 error
**Solution:**
- Check Visual Studio output for errors
- Ensure all files are added to project
- Rebuild project (Ctrl+Shift+B)

### Issue: Tests fail with "schema mismatch"
**Solution:**
- Verify API response matches contract examples exactly
- Check data types match (string vs integer, etc.)
- Ensure field names match (case-sensitive)

---

## 📚 Resources

- **Specmatic Official**: https://specmatic.in
- **OpenAPI Specification**: https://swagger.io/specification/
- **ASP.NET Core Docs**: https://docs.microsoft.com/dotnet/
- **Swashbuckle/Swagger**: https://github.com/domaindrivendev/Swashbuckle.AspNetCore
- **Contract Testing Guide**: https://specmatic.in/contract-testing

---

## 🎯 Learning Objectives

After completing this project, you'll understand:

✅ How to build a REST API in ASP.NET Core  
✅ How to write OpenAPI 3.0 specifications  
✅ How contract testing works  
✅ How to use Specmatic for automatic test generation  
✅ How to validate API compliance with contracts  
✅ How to integrate contract tests in your workflow  

---

## 📋 Checklist

- [ ] Install .NET 6.0 SDK
- [ ] Install Visual Studio 2022
- [ ] Create ASP.NET Core Web API project
- [ ] Add all project files
- [ ] Build project (Ctrl+Shift+B)
- [ ] Run API (F5)
- [ ] Test endpoints in Swagger UI
- [ ] Install Specmatic (npm install -g specmatic)
- [ ] Run Specmatic tests
- [ ] All tests pass ✓

---

## 📝 Notes

- The API uses in-memory data storage (resets on restart)
- Swagger UI is available at `http://localhost:5000/swagger`
- OpenAPI spec is at `http://localhost:5000/swagger/v1/swagger.json`
- Contract file can be updated and tests re-run immediately
- For production, add database, authentication, error handling, etc.

---

## 🤝 Contributing

To extend this project:
1. Add new endpoints to controller
2. Update `employees-contract.yaml` with examples
3. Run `specmatic test` to validate
4. Ensure all tests pass

---

## 📄 License

This project is provided as an educational example.

---

## 🚀 Next Steps

1. **Run the project** - Press F5 in Visual Studio
2. **Test endpoints** - Use Swagger UI at localhost:5000/swagger
3. **Run Specmatic** - Execute `specmatic test` command
4. **Explore contract** - Read `employees-contract.yaml`
5. **Add endpoint** - Modify controller and update contract
6. **Learn more** - Visit https://specmatic.in

---

**Happy Contract Testing! 🎉**

For detailed setup instructions, see [SETUP_GUIDE.md](SETUP_GUIDE.md)  
For quick reference, see [QUICK_START.md](QUICK_START.md)
