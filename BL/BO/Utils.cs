

using System.Collections;
using System.Reflection;

namespace BO;

/// <summary>
/// Utilities Class
/// </summary>
static internal class Utils
{

    /// <summary>
    /// Report an object description as a string. 
    /// </summary>
    /// <typeparam name="T">Object type of T.</typeparam>
    /// <param name="t">The object to be described.</param>
    /// <param name="tabs">Number of tabs at the beginnig of the string.</param>
    /// <returns>A string representing an object.</returns>
    public static string ToStringProperty<T>(this T t,int tabs=0)
    {
        string str = "";
        foreach (PropertyInfo item in t.GetType().GetProperties())
        {
            //if the property is a collection- print all of their members.
            if(!(item.GetValue(t, null) is string)&& item.GetValue(t,null) is IEnumerable)
            {
                str += "\n" + item.Name + ": ";
                foreach (var item2 in (IEnumerable)item.GetValue(t,null))
                {
                    str += "\n" + item2.ToStringProperty(tabs+1)+"\n";
                }
            }
            else
                str += "\n"+new string('\t',tabs) + item.Name +": " + item.GetValue(t, null);
        }
        return str;
    }


}
