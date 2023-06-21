// -----------------------------------------------------------------------
// <copyright file="EmployeeManagementAreaRegistration.cs" company="ASK E-Sqaure">
// TODO: All copy rights reserved @ASK E-Sqaure.
// </copyright>
// ----------------------------------------------------------------------- 

namespace MVCProject.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// 
    /// </summary>
    public enum CheckUpModule : int
    {
        /// <summary>
        /// External
        /// </summary>
        External = 1,

        /// <summary>
        /// Internal
        /// </summary>
        Internal = 2,

        /// <summary>
        /// Campaign
        /// </summary>
        Campaign = 3,

        /// <summary>
        /// PreEmployment
        /// </summary>
        PreEmployment = 4

    }

    /// <summary>
    /// 
    /// </summary>
    public enum Module : int
    {
        /// <summary>
        /// ATS
        /// </summary>
        ATS = 1,

    }

    /// <summary>
    /// 
    /// </summary>
    public enum DirectoryPath : int
    {
        /// <summary>
        /// ATS Directory Path
        /// </summary>
        ATS = 1,

    }


}
