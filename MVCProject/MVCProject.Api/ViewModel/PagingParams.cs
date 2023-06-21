// -----------------------------------------------------------------------
// <copyright file="PagingParams.cs" company="ASK-EHS">
// All copy rights reserved @ASK-EHS.
// </copyright>
// -----------------------------------------------------------------------
namespace MVCProject.Api.ViewModel
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// Paging parameters 
    /// </summary>
    [DataContract]
    public class PagingParams
    {
        /// <summary>
        /// Gets or sets current page number for which records needs to be fetched.
        /// </summary>
        [DataMember]
        public short CurrentPageNumber { get; set; }

        /// <summary>
        /// Gets or sets records per page.
        /// </summary>
        [DataMember]
        public short PageSize { get; set; }

        /// <summary>
        /// Gets or sets order by property or column name.
        /// </summary>
        [DataMember]
        public string OrderByColumn { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to sort in ascending order or descending order.
        /// </summary>
        [DataMember]
        public bool IsAscending { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to get all records which include active and inactive records.
        /// </summary>
        [DataMember]
        public bool IsGetAll { get; set; }

        /// <summary>
        /// Gets or sets a value indicate for start date filter
        /// </summary>
        [DataMember]
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Gets or sets a value indicate for start date filter
        /// </summary>
        [DataMember]
        public DateTime EndDate { get; set; }
        
        /// <summary>
        /// Gets or sets a comma separated value for training status 
        /// </summary>
        [DataMember]
        public string StatusIds { get; set; }

        /// <summary>
        /// Gets or sets a Search string
        /// </summary>
        [DataMember]
        public string Search { get; set; }



        [DataMember]
        public int FunctionLevelId { get; set; }
        [DataMember]
        public int SiteLeveleId { get; set; }
        [DataMember]
        public int WasteTypeId { get; set; }
    }
}