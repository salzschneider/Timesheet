using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Timesheet.UI.Utilities
{
    /// <summary>
    /// Cloning object values
    /// </summary>
    public class Utility
    {
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
    }
}