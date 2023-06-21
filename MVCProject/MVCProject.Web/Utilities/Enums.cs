// -----------------------------------------------------------------------
// <copyright file="Enums.cs" company="ASK-EHS">
// All copy rights reserved @ASK-EHS.
// </copyright>
// ----------------------------------------------------------------------- 
[module: System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1649:FileHeaderFileNameDocumentationMustMatchTypeName", Justification = "Reviewed.")]

namespace MVCProject.Utilities
{
    /// <summary>
    /// Permission Role level
    /// </summary>
    public enum PermissionLevel : int
    {
        /// <summary>
        /// Admin level
        /// </summary>
        Admin = 1,

        /// <summary>
        /// Corporate level
        /// </summary>
        Corporate = 2,

        /// <summary>
        /// Site level
        /// </summary>
        Site = 3,

        /// <summary>
        /// Department level
        /// </summary>
        Department = 4
    }

    /// <summary>
    /// Modules names
    /// </summary>
    public enum Module : int
    {
        /// <summary>
        /// Task module
        /// </summary>
        Task = 1,

        /// <summary>
        /// Near miss module
        /// </summary>
        Nearmiss = 2,

        /// <summary>
        /// For attachment of Investigation Near-miss
        /// </summary>
        InvestigationNearmiss = 3,

        /// <summary>
        /// 
        /// </summary>
        Employee = 4,

        /// <summary>
        /// 
        /// </summary>
        MultiTask = 5
    }
}