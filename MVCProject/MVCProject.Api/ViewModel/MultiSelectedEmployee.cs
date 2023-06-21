// -----------------------------------------------------------------------
// <copyright file="MultiSelectedEmployee.cs" company="ASK-EHS">
// All copy rights reserved @ASK-EHS.
// Extended properties of Tasks
// </copyright>
// -----------------------------------------------------------------------
namespace MVCProject.Api
{
    /// <summary>
    /// General class to get MultiTaskSelectedEmployees data
    /// </summary>
    public class MultiSelectedEmployee
    {
        /// <summary>
        /// Gets or sets EmployeeId
        /// </summary>
        public int EmployeeId { get; set; }

        /// <summary>
        /// Gets or sets Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets Designation Name
        /// </summary>
        public string DesignationName { get; set; }

        /// <summary>
        /// Gets or sets Department Name
        /// </summary>
        public string DepartmentName { get; set; }

        /// <summary>
        /// Gets or sets Site Name
        /// </summary>
        public string SiteName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether Is Active or not
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets serial number.
        /// </summary>
        public int SrNo { get; set; }
    }
}