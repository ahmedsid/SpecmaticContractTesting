using Microsoft.AspNetCore.Mvc;
using SpecmaticAPIProject.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace SpecmaticAPIProject.Controllers
{
    /// <summary>
    /// API controller for managing Employee operations
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class EmployeesController : ControllerBase
    {
        // In-memory data store for demo purposes
        private static List<Employee> _employees = new()
        {
            new Employee
            {
                Id = 1,
                Name = "John Doe",
                Email = "john.doe@company.com",
                Department = "Engineering",
                Position = "Senior Software Engineer",
                Salary = 120000,
                Status = "Active",
                HireDate = new DateTime(2020, 01, 15)
            },
            new Employee
            {
                Id = 2,
                Name = "Jane Smith",
                Email = "jane.smith@company.com",
                Department = "Product",
                Position = "Product Manager",
                Salary = 110000,
                Status = "Active",
                HireDate = new DateTime(2019, 06, 20)
            },
            new Employee
            {
                Id = 3,
                Name = "Bob Johnson",
                Email = "bob.johnson@company.com",
                Department = "Sales",
                Position = "Sales Executive",
                Salary = 90000,
                Status = "Active",
                HireDate = new DateTime(2021, 03, 10)
            }
        };

        /// <summary>
        /// Get all employees
        /// </summary>
        /// <returns>List of all employees</returns>
        [HttpGet]
        [SwaggerOperation(
            Summary = "Get all employees",
            Description = "Retrieves a list of all employees in the system")]
        [SwaggerResponse(200, "Successfully retrieved employees", typeof(List<Employee>))]
        public ActionResult<List<Employee>> GetAllEmployees()
        {
            return Ok(_employees);
        }

        /// <summary>
        /// Get an employee by ID
        /// </summary>
        /// <param name="id">The employee ID</param>
        /// <returns>The employee details</returns>
        [HttpGet("{id}")]
        [SwaggerOperation(
            Summary = "Get employee by ID",
            Description = "Retrieves a specific employee by their unique identifier")]
        [SwaggerResponse(200, "Employee found", typeof(Employee))]
        [SwaggerResponse(404, "Employee not found")]
        public ActionResult<Employee> GetEmployeeById(int id)
        {
            var employee = _employees.FirstOrDefault(e => e.Id == id);

            if (employee == null)
            {
                return NotFound(new { message = $"Employee with ID {id} not found" });
            }

            return Ok(employee);
        }

        /// <summary>
        /// Create a new employee
        /// </summary>
        /// <param name="employee">The employee data</param>
        /// <returns>The created employee with ID</returns>
        [HttpPost]
        [SwaggerOperation(
            Summary = "Create a new employee",
            Description = "Creates a new employee record in the system")]
        [SwaggerResponse(201, "Employee created successfully", typeof(Employee))]
        [SwaggerResponse(400, "Invalid employee data")]
        public ActionResult<Employee> CreateEmployee([FromBody] Employee employee)
        {
            if (string.IsNullOrWhiteSpace(employee.Name))
            {
                return BadRequest(new { message = "Employee name is required" });
            }

            if (string.IsNullOrWhiteSpace(employee.Email))
            {
                return BadRequest(new { message = "Employee email is required" });
            }

            // Generate new ID
            employee.Id = _employees.Any() ? _employees.Max(e => e.Id) + 1 : 1;

            if (employee.HireDate == default(DateTime))
            {
                employee.HireDate = DateTime.UtcNow;
            }

            if (string.IsNullOrWhiteSpace(employee.Status))
            {
                employee.Status = "Active";
            }

            _employees.Add(employee);

            return CreatedAtAction(nameof(GetEmployeeById), new { id = employee.Id }, employee);
        }

        /// <summary>
        /// Update an existing employee
        /// </summary>
        /// <param name="id">The employee ID</param>
        /// <param name="employee">The updated employee data</param>
        /// <returns>No content on success</returns>
        [HttpPut("{id}")]
        [SwaggerOperation(
            Summary = "Update an employee",
            Description = "Updates an existing employee's information")]
        [SwaggerResponse(204, "Employee updated successfully")]
        [SwaggerResponse(404, "Employee not found")]
        [SwaggerResponse(400, "Invalid employee data")]
        public IActionResult UpdateEmployee(int id, [FromBody] Employee employee)
        {
            var existingEmployee = _employees.FirstOrDefault(e => e.Id == id);

            if (existingEmployee == null)
            {
                return NotFound(new { message = $"Employee with ID {id} not found" });
            }

            if (string.IsNullOrWhiteSpace(employee.Name))
            {
                return BadRequest(new { message = "Employee name is required" });
            }

            existingEmployee.Name = employee.Name;
            existingEmployee.Email = employee.Email ?? existingEmployee.Email;
            existingEmployee.Department = employee.Department ?? existingEmployee.Department;
            existingEmployee.Position = employee.Position ?? existingEmployee.Position;
            existingEmployee.Salary = employee.Salary > 0 ? employee.Salary : existingEmployee.Salary;
            existingEmployee.Status = employee.Status ?? existingEmployee.Status;

            return NoContent();
        }

        /// <summary>
        /// Delete an employee
        /// </summary>
        /// <param name="id">The employee ID</param>
        /// <returns>No content on success</returns>
        [HttpDelete("{id}")]
        [SwaggerOperation(
            Summary = "Delete an employee",
            Description = "Removes an employee record from the system")]
        [SwaggerResponse(204, "Employee deleted successfully")]
        [SwaggerResponse(404, "Employee not found")]
        public IActionResult DeleteEmployee(int id)
        {
            var employee = _employees.FirstOrDefault(e => e.Id == id);

            if (employee == null)
            {
                return NotFound(new { message = $"Employee with ID {id} not found" });
            }

            _employees.Remove(employee);
            return NoContent();
        }
    }
}
