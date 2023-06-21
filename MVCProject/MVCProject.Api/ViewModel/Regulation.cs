// -----------------------------------------------------------------------
// <copyright file="Regulation.cs" company="ASK-EHS">
// All copy rights reserved @ASK-EHS.
// </copyright>
// -----------------------------------------------------------------------
namespace MVCProject.Api.ViewModel
{
    using System.Collections.Generic;

    /// <summary>
    /// To get Regulation tree in renewal
    /// </summary>
    public class Regulation
    {
        /// <summary>
        /// Gets or sets Regulation Id
        /// </summary>
        public int RegulationId { get; set; }

        /// <summary>
        /// Gets or sets Requirements
        /// </summary>
        public string Requirements { get; set; }

        /// <summary>
        /// Gets or sets Subject Serial Number
        /// </summary>
        public string SubjectSrNo { get; set; }

        /// <summary>
        /// Gets or sets list of child level Regulation
        /// </summary>
        public List<Regulation> Child { get; set; }
    }
}