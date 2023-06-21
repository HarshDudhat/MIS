// -----------------------------------------------------------------------
// <copyright file="EmployeeManagementAreaRegistration.cs" company="ASK-EHS">
// TODO: All copy rights reserved @ASK-EHS.
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
    public enum ConsumptionType : int
    {
        /// <summary>
        /// OPD 
        /// </summary>
        OPD = 1,

        /// <summary>
        /// Inventory
        /// </summary>
        Inventory = 2,
    }
}
