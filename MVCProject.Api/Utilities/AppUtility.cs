// -----------------------------------------------------------------------
// <copyright file="AppUtility.cs" company="ASK E-Sqaure">
// All copy rights reserved @ASK E-Sqaure.
// </copyright>
// -----------------------------------------------------------------------

namespace MVCProject.Api.Utilities
{
    #region Namespaces

    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Web;
    using System.Web.Hosting;
    using System.Web.Http.ModelBinding;
    using Elmah;
    using MVCProject.Api.Models;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.Data.SqlClient;
    using MVCProject.Common;

    #endregion

    /// <summary>
    /// Application utility class for common methods.
    /// </summary>
    public static class AppUtility 
    {
        readonly static string USP_ConfigLevel_Advance_Search = "USP_ConfigLevel_Advance_Search";

        /// <summary>
        /// Divides list in chunks. 
        /// </summary>
        /// <typeparam name="T">Generic type.</typeparam>
        /// <param name="source">Source type.</param>
        /// <param name="chunkSize">Chunk size.</param>
        /// <returns>Returns list of type <see cref="List"/> class.</returns>
        public static List<List<T>> ChunkBy<T>(this List<T> source, int chunkSize)
        {
            return source
                .Select((x, i) => new { Index = i, Value = x })
                .GroupBy(x => x.Index / chunkSize)
                .Select(x => x.Select(v => v.Value).ToList())
                .ToList();
        }

        /// <summary>
        /// Convert file to base 64.
        /// </summary>
        /// <param name="fullFileName">Absolute path.</param>
        /// <returns>Returns base 64 string.</returns>
        public static string ConvertToBase64(this string fullFileName)
        {
            FileInfo filePath = new FileInfo(fullFileName);
            if (filePath.Exists)
            {
                byte[] bytes = File.ReadAllBytes(fullFileName);
                return Convert.ToBase64String(bytes);
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Converts date time to date or time.
        /// </summary>
        /// <param name="value">Date time value.</param>
        /// <param name="formateProvider">IFormat provider.</param>
        /// <returns>
        /// Returns date string if time is not present.
        /// Returns date and time if time is present.
        /// Returns empty string if value is null.
        /// </returns>
        public static string ConvertToDateTime(DateTime? value, CultureInfo formateProvider)
        {
            if (value.HasValue)
            {
                if (value.Value.TimeOfDay.Ticks > 0)
                {
                    return value.Value.ToString(formateProvider.DateTimeFormat.FullDateTimePattern);
                }
                else
                {
                    return value.Value.ToString(formateProvider.DateTimeFormat.ShortDatePattern);
                }
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Dynamic Elma error log.
        /// </summary>
        /// <param name="exception">Exception object.</param>
        public static void ElmahErrorLog(Exception exception)
        {
            if (HttpContext.Current.Items["elmah-express-connection"] != null)
            {
                string connectionString = HttpContext.Current.Items["elmah-express-connection"].ToString();
                var errorLog = new SqlErrorLog(connectionString);
                Error sqlError = new Error(exception);
                //errorLog.Log(new Error(exception));
            }
        }

        /// <summary>
        /// Make absolute path.
        /// </summary>
        /// <param name="directoryName">Directory name.</param>
        /// <param name="filename">File name.</param>
        /// <returns>Returns absolute path.</returns>
        public static string GetAbsolutepath(this string directoryName, string filename)
        {
            string hostingPath = HostingEnvironment.MapPath("~");
            return string.Format("{0}{1}{2}", hostingPath, directoryName, filename);
        }

        /// <summary>
        /// Get Directory Path
        /// </summary>
        /// <param name="path">Directory path enumeration with description.</param>
        /// <param name="databaseName">Database name.</param>
        /// <param name="isUrl">A value indicates whether its URL or not.</param>
        /// <returns>Returns path of particular enumeration.</returns>
        public static string GetDirectoryPath(DirectoryPath path, string databaseName, bool isUrl = false, string FileURL = "")
        {
            string root = string.Empty;
            string directory = string.Format(AppUtility.GetEnumDescription(path), databaseName);
            if (true)
            {
                if (isUrl)
                {
                    directory = directory.Replace(@"\", "/");
                    Uri uri = HttpContext.Current.Request.Url;
                    root = uri.OriginalString.Replace(uri.PathAndQuery, "/");
                }
                else
                {
                    root = HttpContext.Current.Server.MapPath("~");
                }
            }
            else
            {
                root = FileURL + @"\";
            }

            return string.Format("{0}{1}", root, directory);
        }

        public static string GetCompanyLogo()
        {
            string root = HttpContext.Current.Server.MapPath("~");
            string logo = @"Content\images\AART-Hazard-Reporting-Logo.jpg";

            return string.Format("{0}{1}", root, logo);
        }

        /// <summary>
        /// Get description of enumeration from enumeration description attribute.
        /// </summary>
        /// <param name="value">value is enumeration value.</param>
        /// <returns>returns description of enumeration value.</returns>
        public static string GetEnumDescription(Enum value)
        {
            // Get the Description attribute value for the enumeration value
            FieldInfo fieldInfo = value.GetType().GetField(value.ToString());
            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fieldInfo.GetCustomAttributes(
                    typeof(DescriptionAttribute), false);

            if (attributes.Length > 0)
            {
                return attributes[0].Description;
            }
            else
            {
                return value.ToString();
            }
        }

        /// <summary>
        /// Gets image file extensions.
        /// </summary>
        /// <returns>Returns list of extensions.</returns>
        public static List<string> GetImageExtensions()
        {
            List<string> imageFileExtension = new List<string> { "png", "jpeg", "jpg", "bmp" };
            return imageFileExtension;
        }

        /// <summary>
        /// Gets IP address of current request.
        /// </summary>
        /// <param name="request">Request object.</param>
        /// <returns>Returns IP address.</returns>
        public static string GetIP(HttpRequestBase request)
        {
            string ip = request.Headers["X-Forwarded-For"]; // AWS compatibility

            if (string.IsNullOrEmpty(ip))
            {
                ip = request.UserHostAddress;
            }

            return ip;
        }

        
        /// <summary>
        /// Get request URL.
        /// </summary>
        /// <returns>Returns request URL.</returns>
        public static string GetRequestUrl()
        {
            Uri uri = HttpContext.Current.Request.UrlReferrer;
            if (uri == null)
            {
                return string.Empty;
            }

            if (uri.PathAndQuery == "/")
            {
                return uri.OriginalString.Substring(0, uri.OriginalString.Length - 1);
            }
            else
            {
                return uri.OriginalString.Replace(uri.PathAndQuery, string.Empty);
            }
        }

        /// <summary>
        /// Extension method to get individual flag from bitwise enumeration.
        /// </summary>
        /// <param name="flags">Flagged enumeration.</param>
        /// <returns>Returns individual </returns>
        public static IEnumerable<Enum> GetUniqueFlags(this Enum flags)
        {
            var flag = 1ul;
            foreach (var value in Enum.GetValues(flags.GetType()).Cast<Enum>())
            {
                ulong bits = Convert.ToUInt64(value);
                while (flag < bits)
                {
                    flag <<= 1;
                }

                if (flag == bits && flags.HasFlag(value))
                {
                    yield return value;
                }
            }
        }

        /// <summary>
        /// Extension methods for sorting dynamic field ascending or descending.
        /// </summary>
        /// <typeparam name="T">Generic Type.</typeparam>
        /// <param name="query">Query is generic type of <see cref="IQueryable"/> interface.</param>
        /// <param name="sortField">Sort field is dynamic field name to sorting.</param>
        /// <param name="isAscending">IsAscending or not.</param>
        /// <returns>Returns sorted query.</returns>
        public static IQueryable<T> OrderByField<T>(this IQueryable<T> query, string sortField, bool isAscending)
        {
            var param = Expression.Parameter(typeof(T), "p");
            var prop = Expression.Property(param, sortField);
            var exp = Expression.Lambda(prop, param);
            string method = isAscending ? "OrderBy" : "OrderByDescending";
            Type[] types = new Type[] { query.ElementType, exp.Body.Type };
            var mce = Expression.Call(typeof(Queryable), method, types, query.Expression, exp);
            return query.Provider.CreateQuery<T>(mce);
        }

        /// <summary>
        /// Convert local date to UTC date by given time zone minutes
        /// </summary>
        /// <param name="date">date to convert</param>
        /// <returns>UTC date time.</returns>
        public static DateTime ToUtcDate(this DateTime date)
        {
            UserContext userContext = SecurityUtility.ExtractUserContext(HttpContext.Current.Request);
            return date.AddMinutes(-userContext.TimeZoneMinutes);
        }

        /// <summary>
        /// Get Model Error Message
        /// </summary>
        /// <param name="modelState">Model State Dictionary</param>
        /// <returns>List of Error Message</returns>
        public static string[] GetModelErrorMessage(ModelStateDictionary modelState)
        {
            return modelState.Values.SelectMany(v => v.Errors).Select(e => string.IsNullOrEmpty(e.ErrorMessage) ? (e.Exception == null ? string.Empty : e.Exception.Message) : e.ErrorMessage).ToArray();
        }

        /// <summary>
        /// Create thumbnail image
        /// </summary>
        public static string CreateThumbnail(string fileName, int width, int height)
        {
            Bitmap bitmap = null;
            byte[] imageBytes = null;
            try
            {
                Bitmap loBMP = new Bitmap(fileName);
                ImageFormat loFormat = loBMP.RawFormat;

                decimal lnRatio;
                int lnNewWidth = 0;
                int lnNewHeight = 0;

                // If the image is smaller than a thumbnail just return it
                if (loBMP.Width < width && loBMP.Height < height)
                    return string.Empty;

                if (loBMP.Width > loBMP.Height)
                {
                    lnRatio = (decimal)width / loBMP.Width;
                    lnNewWidth = width;
                    decimal lnTemp = loBMP.Height * lnRatio;
                    lnNewHeight = (int)lnTemp;
                }
                else
                {
                    lnRatio = (decimal)height / loBMP.Height;
                    lnNewHeight = height;
                    decimal lnTemp = loBMP.Width * lnRatio;
                    lnNewWidth = (int)lnTemp;
                }
                bitmap = new Bitmap(lnNewWidth, lnNewHeight);
                Graphics g = Graphics.FromImage(bitmap);
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.FillRectangle(Brushes.White, 0, 0, lnNewWidth, lnNewHeight);
                g.DrawImage(loBMP, 0, 0, lnNewWidth, lnNewHeight);

                loBMP.Dispose();


                System.IO.MemoryStream stream = new System.IO.MemoryStream();
                bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Bmp);
                imageBytes = stream.ToArray();
            }
            catch (System.Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }

            if (imageBytes != null)
                return Convert.ToBase64String(imageBytes);
            else
                return string.Empty;
        }

        public static Boolean checkForSQLInjection(string userInput)
        {

            bool isSQLInjection = false;

            string[] sqlCheckList = { "'","--",";--", ";",
                                       "/*","*/",
                                        "@@","@","%","like",
                                        "char","nchar",
                                       "varchar","nvarchar",
                                       "alter","begin",
                                       "cast","create",
                                       "cursor","waitfor","truncate",
                                       "declare","delete",
                                       "drop","end",
                                       "exec","execute",
                                       "fetch","insert",
                                       "kill","select","merge","union","trigger","function",
                                        "sys","sysobjects",
                                        "syscolumns","table",
                                           "update"
                                       };

            string CheckString = userInput.Replace("'", "''");

            for (int i = 0; i <= sqlCheckList.Length - 1; i++)
            {

                if ((CheckString.IndexOf(sqlCheckList[i], StringComparison.OrdinalIgnoreCase) >= 0))
                {
                    isSQLInjection = true;
                }
            }
            return isSQLInjection;
        }
    }

    /// <summary>
    /// Added extention to the enum class for adding extra description to enum values
    /// </summary>
    public static class EnumExtensions
    {
        public static TAttribute GetAttribute<TAttribute>(this Enum value)
            where TAttribute : Attribute
        {
            var type = value.GetType();
            var name = Enum.GetName(type, value);
            return type.GetField(name)
                .GetCustomAttributes(false)
                .OfType<TAttribute>()
                .SingleOrDefault();
        }

        /// <summary>
        /// Get Description of the Enum
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>       
        public static String GetDescription(this Enum value)
        {
            var description = GetAttribute<DescriptionAttribute>(value);
            return description != null ? description.Description : null;
        }
    }
}