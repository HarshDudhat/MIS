// -----------------------------------------------------------------------
// <copyright file="PagingParams.cs" company="ASK-EHS">
// All copy rights reserved @ASK-EHS.
// </copyright>
// -----------------------------------------------------------------------
namespace MVCProject.Api.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    /// <summary>
    /// Paging parameters 
    /// </summary>
    public class MunicipalSolidWasteReport
    {
        public int Year { get; set; }
        public int Compostable { get; set; }
        public int NonCompostable { get; set; }
        public int All { get; set; }
    }

}