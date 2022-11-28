

using System.Collections;
using System.Reflection;

namespace BO;

static internal class Utils
{

    public static string ToStringProperty<T>(this T t,int tabs=0)
    {
        string str = "";
        foreach (PropertyInfo item in t.GetType().GetProperties())
        {
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
