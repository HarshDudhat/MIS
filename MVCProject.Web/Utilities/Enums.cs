// -----------------------------------------------------------------------
// <copyright file="Enums.cs" company="ASK E-Sqaure">
// All copy rights reserved @ASK E-Sqaure.
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
       
    }


    /// <summary>
    /// Page Access
    /// </summary>
    public enum PageAccess : int
    {
        /// <summary>
        /// Project Master  Page
        /// </summary>
        ProjectMaster = 1,

        /// <summary>
        /// Vertical Master Page
        /// </summary>
        VerticalMaster = 2,

        /// <summary>
        /// Dashboard Page
        /// </summary>
        Dashboard = 3,

        /// <summary>
        /// Approve Report Page
        /// </summary>
        ApproveReport = 4,

        /// <summary>
        /// Review Report Page
        /// </summary>
        ReviewReport = 5

     

    }


}