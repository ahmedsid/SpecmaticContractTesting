namespace SpecmaticAPIProject.Models
{
    /// <summary>
    /// Represents an Employee entity
    /// </summary>
    public class Employee
    {
        /// <summary>
        /// Unique identifier for the employee
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Full name of the employee
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Email address of the employee
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// Department where the employee works
        /// </summary>
        public string? Department { get; set; }

        /// <summary>
        /// Job position/title
        /// </summary>
        public string? Position { get; set; }

        /// <summary>
        /// Annual salary
        /// </summary>
        public decimal Salary { get; set; }

        /// <summary>
        /// Employee status (Active, Inactive, On Leave)
        /// </summary>
        public string? Status { get; set; }

        /// <summary>
        /// Date when employee was hired
        /// </summary>
        public DateTime HireDate { get; set; }
    }
}
