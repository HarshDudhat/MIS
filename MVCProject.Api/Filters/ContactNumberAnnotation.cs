// -----------------------------------------------------------------------
// <copyright file="ContactNumberAnnotation.cs" company="ASK E-Sqaure">
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
    /// Contact Number Validation Annotation
    /// </summary>
    public class ContactNumberAnnotation : RegularExpressionAttribute
    {
        /// <summary>
        /// Initializes static members of the <see cref="ContactNumberAnnotation"/> class.
        /// register annotation attribute
        /// </summary>
        static ContactNumberAnnotation()
        {
            DataAnnotationsModelValidatorProvider.RegisterAdapter(typeof(EmailAnnotation), typeof(RegularExpressionAttributeAdapter));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ContactNumberAnnotation"/> class
        /// Contact Number regular expression
        /// </summary>
        public ContactNumberAnnotation()
            : base(@"^(\+\d{1,2}\s?)?\(?\d{3}\)?[\s.-]?\d{3}[\s.-]?\d{4}$")
        {
        }

        /// <summary>
        /// Error message
        /// </summary>
        /// <param name="name">Error message format</param>
        /// <returns>error message</returns>
        public override string FormatErrorMessage(string name)
        {
            return Resource.ContactNoInvalid;
        }
    }
}