using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;

namespace MVCProject.Api.Utilities.ExtensionMethods
{
    public static class EntityObjectExtensions
    {

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <returns></returns>
        public static DataTable ToDataTable<T>(this List<T> items) where T : class
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            if (Props != null && Props.Length > 0)
            {
                Props = Props.Where(p => (p.PropertyType.IsValueType == true || p.PropertyType == typeof(string)) && p.Name != "EntityState").ToArray();
            }
            foreach (PropertyInfo prop in Props)
            {
                //Setting column names as Property names
                if (prop.PropertyType == typeof(decimal?) || prop.PropertyType == typeof(decimal))
                {
                    dataTable.Columns.Add(columnName: prop.Name, type: typeof(decimal));
                }
                else
                {
                    dataTable.Columns.Add(prop.Name);
                }
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows

                    if ((Props[i].PropertyType == typeof(DateTime?) || Props[i].PropertyType == typeof(DateTime)) && Props[i].GetValue(item, null) != null)
                    {
                        values[i] = Convert.ToDateTime(Props[i].GetValue(item, null)).ToString("yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                    }
                    else if ((Props[i].PropertyType == typeof(decimal?) || Props[i].PropertyType == typeof(decimal)) && Props[i].GetValue(item, null) != null)
                    {
                        values[i] = Convert.ToDecimal(Props[i].GetValue(item, null));
                    }
                    else
                    {
                        values[i] = Props[i].GetValue(item, null);
                    }

                }
                dataTable.Rows.Add(values);
            }


            return dataTable;
        }

    }
}