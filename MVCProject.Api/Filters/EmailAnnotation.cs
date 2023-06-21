// -----------------------------------------------------------------------
// <copyright file="EmailAnnotation.cs" company="ASK E-Sqaure">
// All copy rights reserved @ASK E-Sqaure.
// </copyright>
// ----------------------------------------------------------------------- 

namespace MVCProject.Api.Filters
{
    #region namespaces

    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;
    using MVCProject.Common.Resources;

    #endregion

    /// <summary>
    /// Email validation attribute
    /// </summary>
    public class EmailAnnotation : RegularExpressionAttribute
    {
        /// <summary>
        /// Initializes static members of the <see cref="EmailAnnotation"/> class.
        /// static constructor
        /// </summary>
        static EmailAnnotation()
        {
            DataAnnotationsModelValidatorProvider.RegisterAdapter(typeof(EmailAnnotation), typeof(RegularExpressionAttributeAdapter));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailAnnotation"/> class.
        /// </summary>
        public EmailAnnotation()
            : base(@"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*"
                + "@"
                + @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$")
        {
        }

        /// <summary>
        /// Email validation message
        /// </summary>
        /// <param name="name">name value</param>
        /// <returns>Error message</returns>
        public override string FormatErrorMessage(string name)
        {
            return Resource.EmailInvalid;
        }
    }
}