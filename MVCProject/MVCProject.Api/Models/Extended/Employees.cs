// -----------------------------------------------------------------------
// <copyright file="Employees.cs" company="ASK-EHS">
// All copy rights reserved @ASK-EHS.
// </copyright>
// ----------------------------------------------------------------------- 

namespace MVCProject.Api.Models
{
    #region namespaces

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.Web;

    #endregion

    /// <summary>
    /// Employees extended class
    /// </summary>
    public partial class Employees
    {
        /// <summary>
        /// Gets or sets photo of an employee
        /// </summary>
        [DataMember]
        public Attachments Photo { get; set; }

    }
}