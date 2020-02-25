using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Timesheet.Core.DTO;
using Timesheet.UI.Models;

namespace Timesheet.UI.Utilities
{
    public class Helper
    {
        /// <summary>
        /// Cloning object values to another object
        /// </summary>
        public static object CloneObject(object objSource)
        {
            Type typeSource = objSource.GetType();
            object objTarget = Activator.CreateInstance(typeSource);

            PropertyInfo[] propertyInfo = typeSource.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

            foreach (PropertyInfo property in propertyInfo)
            {
                if (property.CanWrite)
                {
                    if (property.PropertyType.IsValueType || property.PropertyType.IsEnum || property.PropertyType.Equals(typeof(System.String)))
                    {
                        property.SetValue(objTarget, property.GetValue(objSource, null), null);
                    }
                    else
                    {
                        object objPropertyValue = property.GetValue(objSource, null);

                        if (objPropertyValue == null)
                        {
                            property.SetValue(objTarget, null, null);
                        }
                        else
                        {
                            property.SetValue(objTarget, CloneObject(objPropertyValue), null);
                        }
                    }
                }
            }

            return objTarget;
        }

        /// <summary>
        /// Inserting string value at the beginning and in the end of all elements of a string list
        /// </summary>
        /// <param name="list"></param>
        /// <param name="wrapString"></param>
        /// <returns></returns>
        public static IEnumerable<string> WrapListElements(IEnumerable<string> list, string wrapString)
        {
            List<string> wrappedList = new List<string>();

            foreach (var item in list)
            {
                wrappedList.Add(Helper.WrapString(item, wrapString));
            }
   
            return wrappedList;
        }

        /// <summary>
        /// Inserting string value at the beginning and in the end of a string
        /// </summary>
        /// <param name="item"></param>
        /// <param name="wrapString"></param>
        /// <returns></returns>
        public static string WrapString(string item, string wrapString)
        {
            return wrapString + item + wrapString;
        }

        public static string EscapeQuote(string item)
        {
            return item.Replace("\"", "\"\"");
        }

        /// <summary>
        /// Create a csv string from a jagged array. It will generate column headers if we define them. 
        /// </summary>
        /// <param name="array"></param>
        /// <param name="headerList"></param>
        /// <returns></returns>
        public static string ArrayToCSV(string[][] array, List<string> headerList = null)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            if (headerList != null)
            {
                sb.AppendLine(String.Join(",", Helper.WrapListElements(headerList, "\"")));
            }

            for (int i = 0; i < array.Length; i++)
            {
                sb.AppendLine(String.Join(",", Helper.WrapListElements(array[i], "\"")));
            }

            return sb.ToString();
        }

        /// <summary>
        /// Compares two specified IEnumerable lists and returns a boolean that indicates their equality. Order of the list elements is irrelevant. 
        /// </summary>
        /// <param name="firstList">The first String list to compare</param>
        /// <param name="secondList">The second String list to compare</param>
        /// <returns>true if the two list are equal</returns>
        public static bool ListsEqual(IEnumerable<string> firstList, IEnumerable<string> secondList)
        {
            return firstList.Count() == secondList.Count() && !firstList.Except(secondList).Any() ;
        }

        /// <summary>
        /// Converts seconds to (hours*)hours:minutes:seconds format. 
        /// </summary>
        /// <param name="seconds"></param>
        /// <returns>Time in (hh*)hh:mm:ss format or empty string.</returns>
        public static string SecondsToHourMinSec(int secondInput)
        {
            if(secondInput <= 0 )
            {
                return "00:00:00";
            }

            int hours = secondInput / 3600;
            int remainder = secondInput % 3600;

            int minutes = remainder / 60;
            remainder = remainder % 60;      

            return string.Format("{0}:{1}:{2}", hours.ToString().PadLeft(2, '0'), 
                                                minutes.ToString().PadLeft(2,'0'),
                                                remainder.ToString().PadLeft(2, '0'));
        }
    }
}